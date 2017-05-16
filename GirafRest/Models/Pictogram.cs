using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GirafRest.Models.DTOs;

namespace GirafRest.Models {

    /// <summary>
    /// Stores the file type for each pictogram
    /// </summary>
    public enum PictogramImageFormat
    {
        none, png, jpg
    }

    /// <summary>
    /// A pictogram is an image with an associated title. They are used by Guardians and Citizens and so on to 
    /// communicate visually.
    /// </summary>
    public class Pictogram : Resource{
        /// <summary>
        /// The title of the Pictogram.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The accesslevel, PRIVATE means only the owner can see it, PROTECTED means everyone in the owning department and PUBLIC is everyone.
        /// </summary>
        [Required]
        public AccessLevel AccessLevel { get; set; }

        /// <summary>
        /// Overrides the information of this Pictogram with new information found in the DTO.
        /// </summary>
        /// <param name="other">The new information.</param>
        public virtual void Merge(PictogramDTO other) {
            base.Merge(other);
            this.AccessLevel = other.AccessLevel;
        }

        /// <summary>
        /// A byte array containing the pictogram's image.
        /// </summary>
        [Column("Image")]
        public byte[] Image { get; set; }
        
        /// <summary>
        /// Defines the file type of the pictogram's image.
        /// </summary>
        public PictogramImageFormat ImageFormat { get; set; }

        /// <summary>
        /// Creates a new pictogram with the given title and access level.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="accessLevel"></param>
        public Pictogram(string title, AccessLevel accessLevel)
        {
            this.Title = title;
            this.AccessLevel = accessLevel;
            ImageFormat = PictogramImageFormat.none;
        }

        /// <summary>
        /// DO NOT DELETE THIS.
        /// It is required by Newtonsoft
        /// </summary>
        public Pictogram()
        {
            ImageFormat = PictogramImageFormat.none;
        }
    }
}