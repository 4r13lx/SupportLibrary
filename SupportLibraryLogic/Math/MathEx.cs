using System;

namespace SupportLibrary.Math
{
    /// <summary>
    /// Helper class for Math related tasks.<para/>
    /// For example: Truncate fractional numbers to a given decimal places.
    /// </summary>
    public static class MathEx
    {
        /// <summary>
        /// Truncates a float value to a specified number of fractional digits.
        /// </summary>
        /// <param name="value">A float number to be truncated</param>
        /// <param name="decimals">The number of decimal places in the return value. Maximum value = 2.</param>
        /// <returns>The value parameter truncated up to the expecified number of decimal places.</returns>
        public static float Truncate(float value, byte decimals)
        {
            if (decimals > 2) { throw new ArgumentOutOfRangeException("decimals"); }

            return (float)Truncate((decimal)value, decimals);
        }

        /// <summary>
        /// Truncates a double value to a specified number of fractional digits.
        /// </summary>
        /// <param name="value">A double number to be truncated</param>
        /// <param name="decimals">The number of decimal places in the return value. Maximum value = 10.</param>
        /// <returns>The value parameter truncated up to the expecified number of decimal places.</returns>
        public static double Truncate(double value, byte decimals)
        {
            if (decimals > 10) { throw new ArgumentOutOfRangeException("decimals"); }

            return (double)Truncate((decimal)value, decimals);
        }

        /// <summary>
        /// Truncates a decimal value to a specified number of fractional digits.
        /// </summary>
        /// <param name="value">A decimal number to be truncated</param>
        /// <param name="decimals">The number of decimal places in the return value</param>
        /// <returns>The value parameter truncated up to the expecified number of decimal places.</returns>
        public static decimal Truncate(decimal value, byte decimals)
        {
            try
            {
                decimal multiplier = (decimal)System.Math.Pow(10, decimals);
                return System.Math.Truncate(value * multiplier) / multiplier;
            }
            catch (Exception) { throw; }
        }
    }
}
