using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GirafRest.Models.DTOs;

namespace GirafRest.Models
{
    /// <summary>
    /// An enum defining all days of the week.
    /// </summary>
    public enum Days { Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6, Sunday = 7};
    /// <summary>
    /// A weekday displays what a citizen should do in the course of the day. A weekday may be populated with
    /// a series of Pictograms and choices. They make up the building blocks of Weeks.
    /// </summary>
    public class Weekday
    {
        /// <summary>
        /// The id of the weekday.
        /// </summary>
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        /// <summary>
        /// An enum defining which day of the week this instance represents.
        /// </summary>
        public Days Day { get; set; }

        /// <summary>
        /// A collection of elements that make up the week.
        /// </summary>
        public ICollection<Activity> Activities { get; set; }

        /// <summary>
        /// Empty contr required for testing framework.
        /// </summary>
        public Weekday()
        {
            this.Activities = new List<Activity>();
        }

        /// <summary>
        /// Creates a new weekday.
        /// </summary>
        /// <param name="day">The day of the week which the new weekday should represent.</param>
        /// <param name="activityIcons">The icons of the activities that should be added to the weekday.</param>
        /// <param name="activityStates">The states of the activities that should be added to the weekday.</param>
        public Weekday(Days day, List<Pictogram> activityIcons, List<ActivityState> activityStates) : this()
        {
            if (activityIcons.Count != activityStates.Count)
            {
                throw new ArgumentException($"{activityIcons.Count} elements are in activityicons, " +
                                            $"but {activityStates.Count} elements are in activityStates. " +
                                            $"The numbers must match.");
            }
            
            this.Day = day;
            for (int i = 0; i < activityIcons.Count; i++)
            {
                this.Activities.Add(new Activity(this, activityIcons[i], i, activityStates[i]));
            }
        }
        
        /// <summary>
        /// Creates a new weekday from the given WeekdayDTO.
        /// </summary>
        /// <param name="day">A weekday DTO that should be used to create a Weekday from.</param>
        public Weekday(WeekdayDTO day) : this()
        {
            Day = day.Day;
            this.Activities = new List<Activity>();
        }
    }
}