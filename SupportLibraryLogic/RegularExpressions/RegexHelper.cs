using System;

namespace SupportLibrary.RegularExpressions
{
    /// <summary>
    /// Helper class for Regular Expressions related tasks.<para/>
    /// For example: string validations against business document number formats.
    /// </summary>
    public static class RegexHelper
    {
        /// <summary>
        /// Maritime business related regular expressions.
        /// </summary>
        public static readonly Business Business = new Business();

        /// <summary>
        /// Miscellaneous regular expressions.
        /// </summary>
        public static readonly Miscellaneous Miscellaneous = new Miscellaneous();
    }
}
