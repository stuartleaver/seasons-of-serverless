using System.ComponentModel;

namespace PerfectHolidayTurkey.Api.Entities
{
    public enum Units
    {
        [Description("lbs")]
        lbs,

        [Description("Cups")]
        Cups,

        [Description("Gallons")]
        Gallons,

        [Description("Tablespoons")]
        Tablespoons,

        [Description("Hours")]
        Hours,

        [Description("Minutes")]
        Minutes,
    }
}