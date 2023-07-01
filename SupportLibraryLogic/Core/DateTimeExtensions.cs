using System;

namespace SupportLibrary.Core
{
    /// <summary>
    /// Extension Methods for DateTime related tasks.<para/>
    /// For example: LastDayOfCurrentMonth(), GetFirstDayNextMonth().
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the last day of the current month.
        /// </summary>
        /// <param name="value">DateTime object</param>
        /// <returns>DateTime of the calculated day</returns>
        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return DateTimeEx.LastDayOfMonth(value);
        }

        /// <summary>
        /// Returns the first day of the next month.
        /// </summary>
        /// <param name="value">DateTime object</param>
        /// <returns>DateTime of the calculated day</returns>
        public static DateTime FirstDayOfNextMonth(this DateTime value)
        {
            return DateTimeEx.FirstDayOfNextMonth(value);
        }
    }
}
