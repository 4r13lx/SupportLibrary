using System;

namespace SupportLibrary.Core
{
    /// <summary>
    /// Helper class for DateTime related tasks.<para/>
    /// For example: LastDayOfCurrentMonth(), GetFirstDayNextMonth().
    /// </summary>
    public class DateTimeEx
    {
        /// <summary>
        /// Returns the last day of the current month.
        /// </summary>
        /// <returns>Calculated day</returns>
        public static DateTime LastDayOfMonth()
        {
            return LastDayOfMonth(DateTime.Today);
        }

        /// <summary>
        /// Returns the last day of the month for the given DateTime.
        /// </summary>
        /// <param name="value">DateTime object</param>
        /// <returns>Calculated day</returns>
        public static DateTime LastDayOfMonth(DateTime value)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value), "value is null."); }

            return new DateTime(value.Year, value.Month, DateTime.DaysInMonth(value.Year, value.Month));
        }

        /// <summary>
        /// Returns the first day of the next month.
        /// </summary>
        /// <returns>Calculated day</returns>
        public static DateTime FirstDayOfNextMonth()
        {
            return FirstDayOfNextMonth(DateTime.Today);
        }

        /// <summary>
        /// Returns the first day of the month for the given DateTime.
        /// </summary>
        /// <param name="value">DateTime object</param>
        /// <returns>Calculated day</returns>
        public static DateTime FirstDayOfNextMonth(DateTime value)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value), "value is null."); }

            return new DateTime(value.AddMonths(1).Year, value.AddMonths(1).Month, 1);
        }
    }
}
