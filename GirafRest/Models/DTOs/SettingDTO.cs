using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GirafRest.Models.DTOs
{
    /// <summary>
    /// Screen orientation
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// Portrait mode
        /// </summary>
        portrait = 1,

        /// <summary>
        /// Landscape mode
        /// </summary>
        landscape = 2
    }
    /// <summary>
    /// Mark used for "Complete"
    /// </summary>
    public enum CompleteMark
    {
        /// <summary>
        /// Removed X
        /// </summary>
        Removed = 1,
        /// <summary>
        /// Checkmark
        /// </summary>
        Checkmark = 2,
        /// <summary>
        /// Moved right
        /// </summary>
        MovedRight = 3
    }

    /// <summary>
    /// Mark used for Cancel
    /// </summary>
    public enum CancelMark
    {
        /// <summary>
        /// Removed when cancelled
        /// </summary>
        Removed = 1,
        /// <summary>
        /// X when cancelled
        /// </summary>
        Cross = 2
    }

    /// <summary>
    /// Default timer type
    /// </summary>
    public enum DefaultTimer
    {
        /// <summary>
        /// Hourglass model
        /// </summary>
        hourglass = 1,
        /// <summary>
        /// Piechart counting down
        /// </summary>
        pieChart = 2,
        /// <summary>
        /// Numeric Clock counting down
        /// </summary>
        numeric = 3
    }

    /// <summary>
    /// Timer Theme
    /// </summary>
    public enum Theme
    {
        /// <summary>
        /// Yellow as Giraf Theme
        /// </summary>
        girafYellow = 1,
        /// <summary>
        /// The theme Green
        /// </summary>
        girafGreen = 2,
        /// <summary>
        /// The Giraf Red Color
        /// </summary>
        girafRed = 3,
        /// <summary>
        /// Generic blue Android
        /// </summary>
        androidBlue = 4
    }

    /// <summary>
    /// A Data Transfer Object for the user settings used by the launcher
    /// </summary>
    public class SettingDTO
    {
        /// <summary>
        /// Preferred orientation of device/screen
        /// </summary>
        [Required]
        public Orientation Orientation { get; set; }
        /// <summary>
        /// Preferred appearence of checked resources
        /// </summary>
        [Required]
        public CompleteMark CompleteMark { get; set; }
        /// <summary>
        /// Preferred appearence of cancelled resources
        /// </summary>
        [Required]
        public CancelMark CancelMark { get; set; }
        /// <summary>
        /// Preferred appearence of timer
        /// </summary>
        [Required]
        public DefaultTimer DefaultTimer { get; set; }
        /// <summary>
        /// Number of seconds for timer
        /// </summary>
        public int? TimerSeconds { get; set; }
        /// <summary>
        /// Number of activities
        /// </summary>
        public int? ActivitiesCount { get; set; }
        /// <summary>
        /// The preferred theme
        /// </summary>
        [Required]
        public Theme Theme { get; set; }
        /// <summary>
        /// defines the number of days to display for a user in a weekschedule
        /// </summary>
        public int? NrOfDaysToDisplay { get; set; }
        /// <summary>
        /// Flag for indicating whether or not greyscale is enabled
        /// </summary>
        public bool GreyScale { get; set; }
        /// <summary>
        /// Flag for indicating whether or not timer buttons are enabled
        /// </summary>
        public bool LockTimerControl { get; set; }
        /// <summary>
        /// Flag for indicating whether or not pictogram text is enabled
        /// </summary>
        public bool PictogramText { get; set; }
        /// <summary>
        /// Flag for indicating whether or not popup is enabled
        /// </summary>
        public bool ShowPopup { get; set; }


        /// <summary>
        /// List of weekday colors
        /// </summary>
        public List<WeekDayColorDTO> WeekDayColors { get; set; }
        /// <summary>
        /// Constructor to create a DTO based on the actual object
        /// </summary>
        /// <param name="options">The launcher options in need of transfer</param>
        public SettingDTO(Setting options)
        {
            this.Orientation = options.Orientation;
            this.CompleteMark = options.CompleteMark;
            this.CancelMark = options.CancelMark;
            this.DefaultTimer = options.DefaultTimer;
            this.TimerSeconds = options.TimerSeconds;
            this.ActivitiesCount = options.ActivitiesCount;
            this.Theme = options.Theme;
            this.NrOfDaysToDisplay = options.NrOfDaysToDisplay;
            this.GreyScale = options.GreyScale;
            this.LockTimerControl = options.LockTimerControl;
            this.PictogramText = options.PictogramText;
            this.WeekDayColors = SetWeekDayColorsFromModel(options.WeekDayColors);
        }

        /// <summary>
        /// Empty Constructor used for JSON Generation
        /// </summary>
        public SettingDTO()
        {
            Orientation = Orientation.portrait;
            CompleteMark = CompleteMark.Checkmark;
            CancelMark = CancelMark.Cross;
            DefaultTimer = DefaultTimer.pieChart;
            Theme = Theme.girafYellow;
        }

        private List<WeekDayColorDTO> SetWeekDayColorsFromModel(List<WeekDayColor> weekDayColors)
        {
            if (weekDayColors != null)
            {
                var WeekDayColorDTOs = new List<WeekDayColorDTO>();
                foreach (var weekDayColor in weekDayColors)
                {
                    WeekDayColorDTOs.Add(new WeekDayColorDTO()
                    {
                        Day = weekDayColor.Day,
                        HexColor = weekDayColor.HexColor
                    });
                }

                return WeekDayColorDTOs;
            }

            return null;
        }
    }
}
