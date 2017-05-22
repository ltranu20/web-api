using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using GirafRest.Models.DTOs;

namespace GirafRest.Models
{
    /// <summary>
    /// A week defines the schedule of some citizen in the course of the week.
    /// </summary>
    public class Week
    {
        /// <summary>
        /// The id of the week.
        /// </summary>
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// A collection of weekdays for each day of the week.
        /// </summary>
        public IList<Weekday> Weekdays { get; set; }
        
        /// <summary>
        /// The key of the weeks Thumbnail.
        /// </summary>
        public long ThumbnailKey { get; set; }
        [ForeignKey("ThumbnailKey")]

        /// <summary>
        /// The thumbnail for the week.
        /// </summary>
        public virtual Pictogram Thumbnail { get; set; }

        /// <summary>
        /// DO NOT DELETE THIS.
        /// </summary>
        public Week()
        {
        }

        /// <summary>
        /// A constructor for week setting only the thumbnail.
        /// </summary>
        public Week(Pictogram thumbnail)
        {
            this.Thumbnail = thumbnail;
            initWeek();
        }
        /// <summary>
        /// Creates a new Week from the given WeekDTO.
        /// </summary>
        /// <param name="weekDTO">The data transfer object to create a new week from.</param>
        public Week(WeekDTO weekDTO)
        {
            initWeek();
            if(weekDTO.Days != null){
                foreach (var day in weekDTO.Days)
                {
                    UpdateDay(new Weekday(day));
                }
            }
            this.ThumbnailKey = weekDTO.Thumbnail.Id;
        }

        /// <summary>
        /// Updates the given weekday of the Week with the new information found in 'day'.
        /// </summary>
        /// <param name="day">A day instance to update the week with - the old one is completely overridden.</param>
        public void UpdateDay(Weekday day)
        {
            Weekdays[(int)day.Day] = day;
        }
        
        /// <summary>
        /// Initialises the week. Must be initialised like this, otherwise the Weekdays will not receive a key
        /// </summary>
        public void initWeek()
        {
            this.Weekdays = new Weekday[7] 
            { 
                // Each day must be set individually, otherwise all days will simply be monday
                new Weekday() { Day = Days.Monday }, 
                new Weekday() { Day = Days.Tuesday }, 
                new Weekday() { Day = Days.Wednesday }, 
                new Weekday() { Day = Days.Thursday }, 
                new Weekday() { Day = Days.Friday }, 
                new Weekday() { Day = Days.Saturday }, 
                new Weekday() { Day = Days.Sunday }};
        }
    }
}