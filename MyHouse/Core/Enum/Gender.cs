using System.ComponentModel;

namespace Core.Enum
{
    public enum Gender
    {
        [Description("Female")]
        Female,
        [Description("Male")]
        Male,
        [Description("Unknown")]
        Unknown
    }
}