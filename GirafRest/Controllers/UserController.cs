﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GirafRest.Models;
using GirafRest.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GirafRest.Models.DTOs;
using GirafRest.Models.DTOs.UserDTOs;
using System.Collections.Generic;

namespace GirafRest.Controllers
{
    /// <summary>
    /// The user controller allows the user to change his information as well as add and remove applications
    /// and resources to users.
    /// </summary>
    [Authorize]
    [Route("[controller]")]
    public class UserController : Controller
    {
        /// <summary>
        /// An email sender that can be used to send emails to users that have lost their password. (DOES NOT WORK YET!)
        /// </summary>
        private readonly IEmailSender _emailSender;
        /// <summary>
        /// A reference to GirafService, that defines common functionality for all controllers.
        /// </summary>
        private readonly IGirafService _giraf;

        private readonly RoleManager<GirafUser> _roleManager;

        public UserController(
            IGirafService giraf,
          IEmailSender emailSender,
          ILoggerFactory loggerFactory,
          RoleManager<GirafUser> roleManager)
        {
            _giraf = giraf;
            _giraf._logger = loggerFactory.CreateLogger("User");
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Find information on the user with the username supplied as a url query parameter or the current user.
        /// </summary>
        /// <returns>NotFound either if there is no user with the given username or the user is not authorized to see the user
        /// or Ok and a serialized version of the sought-after user.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            //Declare needed variables
            GirafUser user;
            string usernameQuery = HttpContext.Request.Query["username"];

            //Check if the caller has supplied a query, find the user with the given name if so,
            //else find the user with the given username.
            if (!string.IsNullOrEmpty(usernameQuery))
            {
                //First attempt to fetch the user and check that he exists
                user = await _giraf._userManager.FindByNameAsync(usernameQuery);
                if (user == null)
                    return NotFound();

                //Get the current user and check if he is a guardian in the same department as the user
                //or an Admin, in which cases the user is allowed to see the user.
                var currentUser = await _giraf._userManager.GetUserAsync(HttpContext.User);
                if (await _giraf._userManager.IsInRoleAsync(currentUser, GirafRole.Guardian))
                {
                    //Check if the guardian is in the same department as the user
                    if (user.DepartmentKey != currentUser.DepartmentKey)
                        //We do not reveal if a user with the given username exists
                        return NotFound();
                }
                else if (await _giraf._userManager.IsInRoleAsync(currentUser, GirafRole.Admin))
                {
                    //No additional checks required, simply skip to Ok.
                }
                else
                    //We do not reveal if a user with the given username exists
                    return NotFound();
            }
            else
            {
                user = await _giraf.LoadUserAsync(HttpContext.User);
                if(await _giraf._userManager.IsInRoleAsync(user, GirafRole.Guardian))
                {
                    var users = new List<GirafUserDTO>();
                    foreach(var member in user.Department.Members)
                    {
                        users.Add(new GirafUserDTO(member));
                    }
                    var guardian = new GirafUserDTO(user);
                    return Ok(new {Guardian = guardian, members = users});
                }
            }

            return Ok(new GirafUserDTO(user));
        }

        /// <summary>
        /// Allows the user to upload an icon for his profile.
        /// </summary>
        /// <returns>Ok if the upload was succesful and BadRequest if not.</returns>
        [HttpPost("icon")]
        public async Task<IActionResult> CreateUserIcon() {
            var usr = await _giraf._userManager.GetUserAsync(HttpContext.User);
            if(usr.UserIcon != null) return BadRequest("The user already has an icon - please PUT instead.");
            byte[] image = await _giraf.ReadRequestImage(HttpContext.Request.Body);
            usr.UserIcon = image;
            await _giraf._context.SaveChangesAsync();

            return Ok(new GirafUserDTO(usr));
        }
        /// <summary>
        /// Allows the user to update his profile icon.
        /// </summary>
        /// <returns>Ok on success and BadRequest if the user already has an icon.</returns>
        [HttpPut("icon")]
        public async Task<IActionResult> UpdateUserIcon() {
            var usr = await _giraf._userManager.GetUserAsync(HttpContext.User);
            if(usr.UserIcon == null) return BadRequest("The user does not have an icon - please POST instead.");
            byte[] image = await _giraf.ReadRequestImage(HttpContext.Request.Body);
            usr.UserIcon = image;
            await _giraf._context.SaveChangesAsync();

            return Ok(new GirafUserDTO(usr));
        }
        /// <summary>
        /// Allows the user to delete his profile icon.
        /// </summary>
        /// <returns>Ok.</returns>
        [HttpDelete("icon")]
        public async Task<IActionResult> DeleteUserIcon() {
            var usr = await _giraf._userManager.GetUserAsync(HttpContext.User);
            usr.UserIcon = null;
            await _giraf._context.SaveChangesAsync();

            return Ok(new GirafUserDTO(usr));
        }
        
        /// <summary>
        /// Adds an application to the specified user's list of applications.
        /// </summary>
        /// <param name="userId">The id of the user to add the application for.</param>
        /// <param name="application">Information on the new application to add, must be serialized
        /// and in the request body. Please specify ApplicationName and ApplicationPackage.</param>
        /// <returns>BadRequest in no application is specified,
        /// NotFound if no user with the given id exists or
        /// Ok and a serialized version of the user to whom the application was added.</returns>
        [HttpPost("applications/{userId}")]
        public async Task<IActionResult> AddApplication(string userId, [FromBody] ApplicationOption application)
        {
            //Check that an application has been specified
            if (application == null)
                return BadRequest("No application was specified in the request body.");

            //Fetch the target user and check that he exists
            var user = await _giraf._context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.AvailableApplications)
                .FirstOrDefaultAsync();
            if (user == null)
                return NotFound($"There is no user with id: {userId}");

            //Add the application for the user to see
            user.AvailableApplications.Add(application);
            await _giraf._context.SaveChangesAsync();
            return Ok(new GirafUserDTO(user));
        }
        /// <summary>
        /// Delete an application from the given user's list of applications.
        /// </summary>
        /// <param name="userId">The id of the user to delete the application from.</param>
        /// <param name="application">The application to delete (its ID is sufficient).</param>
        /// <returns>BadRequest if no application is specified,
        /// NotFound if no user or applications with the given ids exist
        /// or Ok and the user if everything went well.</returns>
        [HttpDelete("applications/{userId}")]
        public async Task<IActionResult> DeleteApplication(string userId, [FromBody] ApplicationOption application)
        {
            //Check if the caller has specified an application to remove
            if (application == null)
                return BadRequest("No application was specified in the request body.");

            //Fetch the user and check that he exists
            var user = await _giraf._context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.AvailableApplications)
                .FirstOrDefaultAsync();
            if (user == null)
                return NotFound($"There is no user with id: {userId}");

            //Check if the given application was previously available to the user
            var app = user.AvailableApplications.Where(a => a.Id == application.Id).FirstOrDefault();
            if (app == null)
                return NotFound("The user did not have an ApplicationOption with id " + application.Id);

            //Remove it and save changes
            user.AvailableApplications.Remove(app);
            await _giraf._context.SaveChangesAsync();
            return Ok(new GirafUserDTO(user));
        }

        /// <summary>
        /// Updates the display name of the current user.
        /// </summary>
        /// <param name="displayName">The new display name of the user.</param>
        /// <returns>BadRequest if no display name was specified or Ok and the user.</returns>
        [HttpPut("display-name")]
        public async Task<IActionResult> UpdateDisplayName([FromBody] string displayName)
        {
            if (string.IsNullOrEmpty(displayName))
                return BadRequest("You need to specify a new display name");

            var user = await _giraf.LoadUserAsync(HttpContext.User);

            user.DisplayName = displayName;
            await _giraf._context.SaveChangesAsync();
            return Ok(new GirafUserDTO(user));
        }

        /// <summary>
        /// Adds a resource to the given user's list of resources.
        /// </summary>
        /// <param name="userId">The id of the user to add it to.</param>
        /// <param name="resourceId">The id of the resource to add.</param>
        /// <returns>
        /// BadRequest if either of the two ids are missing or the resource is not PRIVATE, NotFound
        /// if either the user or the resource does not exist or Ok if everything went well.
        /// </returns>
        [HttpPost("resource/{userId}")]
        public async Task<IActionResult> AddUserResource(string userId, [FromBody] ResourceIdDTO resourceIdDTO)
        {
            //Check if valid parameters have been specified in the call
            if (string.IsNullOrEmpty(userId))
                return BadRequest("You need to specify an id of a user.");
            if (resourceIdDTO == null)
                return BadRequest("You need to specify a resourceId in the body of the request.");

            //Find the resource and check that it actually does exist - also verify that the resource is private
            var resource = await _giraf._context.Pictograms
                .Where(pf => pf.Id == resourceIdDTO.ResourceId)
                .FirstOrDefaultAsync();
            if (resource == null)
                return NotFound("The is no resource with id " + resourceIdDTO.ResourceId);
            if (resource.AccessLevel != AccessLevel.PRIVATE)
                return BadRequest("Resources must be PRIVATE (2) in order for users to own them.");

            //Check that the currently authenticated user owns the resource
            var curUsr = await _giraf.LoadUserAsync(HttpContext.User);
            var resourceOwnedByCaller = await _giraf.CheckPrivateOwnership(resource, curUsr);
            if (!resourceOwnedByCaller)
                return Unauthorized();

            //Attempt to find the target user and check that he exists
            var user = await _giraf._context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Resources)
                .FirstOrDefaultAsync();
            if (user == null)
                return NotFound("There is no user with id " + userId);

            //Check if the target user already owns the resource
            if (user.Resources.Where(ur => ur.ResourceKey == resourceIdDTO.ResourceId).Any())
                return BadRequest("The user already owns the resource.");

            //Create the relation and save changes.
            var userResource = new UserResource(user, resource);
            await _giraf._context.UserResources.AddAsync(userResource);
            await _giraf._context.SaveChangesAsync();
            return Ok(new GirafUserDTO(user));
        }

        /// <summary>
        /// Deletes a resource with the specified id from the given user's list of resources.
        /// </summary>
        /// <param name="userId">The id of the user to add the resource to.</param>
        /// <param name="resourceId">The id of the resource to add.</param>
        /// <returns>
        /// BadRequest if either of the two ids are missing or the resource is not PRIVATE, NotFound
        /// if either the user or the resource does not exist or Ok if everything went well.
        /// </returns>
        [HttpDelete("resource/{userId}")]
        public async Task<IActionResult> RemoveResource(string userId, [FromBody] ResourceIdDTO resourceIdDTO)
        {
            //Check that valid parameters have been specified in the call
            if (resourceIdDTO == null)
                return BadRequest("The body of the request must contain a resourceId");
            if (string.IsNullOrEmpty(userId))
                return BadRequest("Please specify a userId to add the pictogram to");

            //Fetch the user and check that it exists.
            var user = await _giraf._context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null) return NotFound($"There is no department with Id {userId}.");
            
            //Fetch the resource with the given id, check that it exists.
            var resource = await _giraf._context.Pictograms
                .Where(f => f.Id == resourceIdDTO.ResourceId)
                .FirstOrDefaultAsync();
            if (resource == null) return NotFound($"There is no resource with id {resourceIdDTO.ResourceId}.");

            //Check if the caller owns the resource
            var curUsr = await _giraf.LoadUserAsync(HttpContext.User);
            var resourceOwned = await _giraf.CheckPrivateOwnership(resource, curUsr);
            if (!resourceOwned) return Unauthorized();

            //Check if the user already owns the resource and remove if so.
            var drrelation = await _giraf._context.UserResources
                .Where(ur => ur.ResourceKey == resource.Id && ur.OtherKey == user.Id)
                .FirstOrDefaultAsync();
            if (drrelation == null) return BadRequest("The user does not own the given resource.");
            user.Resources.Remove(drrelation);
            await _giraf._context.SaveChangesAsync();

            //Return Ok and the user - the resource is now visible in user.Resources
            return Ok(new GirafUserDTO(user));
        }
        
        /// <summary>
        /// Enables or disables grayscale mode for the currently authenticated user.
        /// </summary>
        /// <param name="enabled">A bool indicating whether grayscale should be enabled or not.</param>
        /// <returns>Ok and a serialized version of the current user.</returns>
        [HttpPost("grayscale/{enabled}")]
        public async Task<IActionResult> ToggleGrayscale(bool enabled)
        {
            var user = await _giraf.LoadUserAsync(HttpContext.User);

            user.UseGrayscale = enabled;
            await _giraf._context.SaveChangesAsync();
            return Ok(new GirafUserDTO(user));
        }

        /// <summary>
        /// Enables or disables launcher animations for the currently authenticated user.
        /// </summary>
        /// <param name="enabled">A bool indicating whether launcher animations should be enabled or not.</param>
        /// <returns>Ok and a serialized version of the current user.</returns>
        [HttpPost("launcher_animations/{enabled}")]
        public async Task<IActionResult> ToggleAnimations(bool enabled)
        {
            var user = await _giraf.LoadUserAsync(HttpContext.User);

            user.DisplayLauncherAnimations = enabled;
            await _giraf._context.SaveChangesAsync();
            return Ok(new GirafUserDTO(user));
        }
        #region Helpers
        /// <summary>
        /// Writes all errors found by identity to the logger.
        /// </summary>
        /// <param name="result">The result from Identity when executing user-related actions.</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                _giraf._logger.LogError(error.Description);
            }
        }

        #endregion
    }
}
