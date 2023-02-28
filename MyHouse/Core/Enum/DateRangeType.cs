using System.ComponentModel;

namespace Core.Enum
{
    public enum DateRangeType
    {
        [Description("Today")]
        Today,
        [Description("Yesterday")]
        Yesterday,
        [Description("Last 7 Days")]
        Last7Days,
        [Description("Last 30 Days")]
        Last30Days,
        [Description("Last Month")]
        LastMonth,
        [Description("This Month")]
        ThisMonth,
        [Description("3 Month Ago")]
        _3MonthsAgo,
        [Description("Last Year")]
        LastYear
    }
}