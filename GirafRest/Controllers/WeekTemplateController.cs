﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GirafRest.Extensions;
using GirafRest.Models;
using GirafRest.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GirafRest.Services;
using GirafRest.Models.Responses;
using Microsoft.AspNetCore.Identity;
using static GirafRest.Controllers.SharedMethods;

namespace GirafRest.Controllers
{
    [Route("v1/[controller]")]
    public class WeekTemplateController : Controller
    {
        /// <summary>
        /// A reference to GirafService, that contains common functionality for all controllers.
        /// </summary>
        private readonly IGirafService _giraf;
        
        /// <summary>
        /// A reference to the role manager for the project.
        /// </summary>
        private readonly RoleManager<GirafRole> _roleManager;


        /// <summary>
        /// Constructor is called by the asp.net runtime.
        /// </summary>
        /// <param name="giraf">A reference to the GirafService.</param>
        /// <param name="roleManager">A reference to the... no, wait, just take a guess, eh?</param>
        /// <param name="loggerFactory">A reference to an implementation of ILoggerFactory. Used to create a logger.</param>
        public WeekTemplateController(IGirafService giraf, RoleManager<GirafRole> roleManager, ILoggerFactory loggerFactory)
        {
            _giraf = giraf;
            _roleManager = roleManager;
            _giraf._logger = loggerFactory.CreateLogger("WeekTemplate");
        }

        /// <summary>
        /// Gets all schedule templates for the currently authenticated user.
        /// Available to all users.
        /// </summary>
        /// <returns>NoWeekTemplateFound if there are no templates in the user's department.
        /// OK otherwise.</returns>
        [HttpGet("")]
        [Authorize]
        public async Task<Response<IEnumerable<WeekTemplateNameDTO>>> GetWeekTemplates()
        {
            var user = await _giraf.LoadUserAsync(HttpContext.User);
            if (user == null)
            {
                return new ErrorResponse<IEnumerable<WeekTemplateNameDTO>>(ErrorCode.UserNotFound);
            }
            
            var result = _giraf._context.WeekTemplates
                             .Where(t => t.DepartmentKey == user.DepartmentKey)
                             .Select(w => new WeekTemplateNameDTO(w.Name, w.Id)).ToArray();
            
            if (result.Length < 1)
            {
                return new ErrorResponse<IEnumerable<WeekTemplateNameDTO>>(ErrorCode.NoWeekTemplateFound);
            }
            else
            {
                return new Response<IEnumerable<WeekTemplateNameDTO>>(result);
            }
        }

        /// <summary>
        /// Gets the week template with the specified id.
        /// Available to all users.
        /// </summary>
        /// <param name="id">The id of the week template to fetch.</param>
        /// <returns>WeekTemplateNotFound if there is no template in the authenticated user's department by that ID.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<Response<WeekTemplateDTO>> GetWeekTemplate(long id)
        {
            var user = await _giraf.LoadUserAsync(HttpContext.User);
            
            var week = await (_giraf._context.WeekTemplates
                .Include(w => w.Thumbnail)
                .Include(u => u.Weekdays)
                    .ThenInclude(wd => wd.Activities)
                        .ThenInclude(a => a.Pictogram)
                .Where(t => t.DepartmentKey == user.DepartmentKey)
                .FirstOrDefaultAsync(w => w.Id == id));
            
            if (week != null && user.DepartmentKey == week.DepartmentKey)
                return new Response<WeekTemplateDTO>(new WeekTemplateDTO(week));
            else
                return new ErrorResponse<WeekTemplateDTO>(ErrorCode.WeekTemplateNotFound);
        }

        /// <summary>
        /// Creates new week template in the department of the given user. 
        /// Available to Supers, Departments and Guardians.
        /// </summary>
        /// <param name="templateDTO">After successful execution, a new week template will be created with the same values as this DTO.</param>
        /// <returns>UserMustBeInDepartment if user has no associated department.
        /// MissingProperties if properties are missing.
        /// ResourceNotFound if any pictogram id is invalid.
        /// A DTO containing the full information on the created template otherwise.</returns>
        [HttpPost("")]
        [Authorize (Roles = GirafRole.Department + "," + GirafRole.Guardian + "," + GirafRole.SuperUser)]
        public async Task<Response<WeekTemplateDTO>> CreateWeekTemplate([FromBody] WeekTemplateDTO templateDTO)
        {
            var user = await _giraf.LoadUserAsync(HttpContext.User);
            if (user == null) return new ErrorResponse<WeekTemplateDTO>(ErrorCode.UserNotFound);

            if(templateDTO == null) return new ErrorResponse<WeekTemplateDTO>(ErrorCode.MissingProperties);

            Department department = _giraf._context.Departments.FirstOrDefault(d => d.Key == user.DepartmentKey);
            if(department == null)
                return new ErrorResponse<WeekTemplateDTO>(ErrorCode.UserMustBeInDepartment);
            
            var newTemplate = new WeekTemplate(department);

            var errorCode = await SetWeekFromDTO(templateDTO, newTemplate, _giraf);
            if (errorCode != null)
                return new ErrorResponse<WeekTemplateDTO>(errorCode.ErrorCode, errorCode.ErrorProperties);

            _giraf._context.WeekTemplates.Add(newTemplate);
            await _giraf._context.SaveChangesAsync();
            return new Response<WeekTemplateDTO>(new WeekTemplateDTO(newTemplate));
        }
        
        /// <summary>
        /// Overwrite the information of a week template.
        /// Available to all Supers, and to Departments and Guardians of the same department as the template.
        /// </summary>
        /// <param name="id">Id of the template to overwrite.</param>
        /// <param name="templateDTO">After successful execution, specified template will have the same values as this DTO.</param>
        /// <returns> WeekTemplateNotFound if no template exists with the given id.
        /// NotAuthorized if not available to authenticated user(see summary).
        /// MissingProperties if properties are missing.
        /// ResourceNotFound if any pictogram id is invalid.
        /// A DTO containing the full information on the created template otherwise.</returns>
        [HttpPut("{id}")]
        [Authorize (Roles = GirafRole.Department + "," + GirafRole.Guardian + "," + GirafRole.SuperUser)]
        public async Task<Response<WeekTemplateDTO>> UpdateWeekTemplate(long id, [FromBody] WeekTemplateDTO newValuesDTO)
        {
            var user = await _giraf.LoadUserAsync(HttpContext.User);
            if (user == null) return new ErrorResponse<WeekTemplateDTO>(ErrorCode.UserNotFound);

            if(newValuesDTO == null) return new ErrorResponse<WeekTemplateDTO>(ErrorCode.MissingProperties);

            var template = _giraf._context.WeekTemplates
                .Include(w => w.Thumbnail)
                .Include(u => u.Weekdays)
                    .ThenInclude(wd => wd.Activities)
                .FirstOrDefault(t => id == t.Id);
            
            if(template == null)
                return new ErrorResponse<WeekTemplateDTO>(ErrorCode.WeekTemplateNotFound);
            
            if(!(await HasEditRights(user, template.DepartmentKey)))
                return new ErrorResponse<WeekTemplateDTO>(ErrorCode.NotAuthorized);
            
            var errorCode = await SetWeekFromDTO(newValuesDTO, template, _giraf);
            if (errorCode != null)
                return new ErrorResponse<WeekTemplateDTO>(errorCode.ErrorCode, errorCode.ErrorProperties);

            _giraf._context.WeekTemplates.Update(template);
            await _giraf._context.SaveChangesAsync();
            return new Response<WeekTemplateDTO>(new WeekTemplateDTO(template));
        }

        /// <summary>
        /// Deletes the template of the given ID.
        /// Available to all Supers, and to Departments and Guardians of the same department as the template.
        /// </summary>
        /// <param name="id">Id of the template that will be deleted.</param>
        /// <returns> WeekTemplateNotFound if no template exists with the given id.
        /// NotAuthorized if not available to authenticated user(see summary).
        /// OK otherwise. </returns>
        [HttpDelete("{id}")]
        [Authorize (Roles = GirafRole.Department + "," + GirafRole.Guardian + "," + GirafRole.SuperUser)]
        public async Task<Response> DeleteTemplate(long id)
        {
            var user = await _giraf.LoadUserAsync(HttpContext.User);
            if (user == null) return new ErrorResponse<WeekTemplateDTO>(ErrorCode.UserNotFound);

            var template = _giraf._context.WeekTemplates
                .Include(w => w.Weekdays)
                    .ThenInclude(w => w.Activities)
                .FirstOrDefault(t => id == t.Id);
            
            if(template == null)
                return new ErrorResponse<WeekTemplateDTO>(ErrorCode.WeekTemplateNotFound);
            
            if(await HasEditRights(user, template.DepartmentKey))
                return new ErrorResponse(ErrorCode.NotAuthorized);

            _giraf._context.WeekTemplates.Remove(template);
            await _giraf._context.SaveChangesAsync();
            return new Response();
        }
        
        private async Task<bool> HasEditRights(GirafUser user, long templateDepartmentKey)
        {
            
            GirafRoles userRole = await _roleManager.findUserRole(_giraf._userManager, user);

            if (user.DepartmentKey != templateDepartmentKey || userRole == GirafRoles.SuperUser)
                return false;
            else
                return true;
        }
    }
}
