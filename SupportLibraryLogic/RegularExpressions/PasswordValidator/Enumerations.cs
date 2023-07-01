using System;
using System.ComponentModel;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Identifies a password validation. For example: 'MinLenght', 'MinNumericChars' or 'MinSpecialChars'.
    /// </summary>
    [DefaultValue(None)]
    public enum ValidationType
    {
        /// <summary>
        /// Means an unidentified option.
        /// </summary>
        None = 0,

        /// <summary>
        /// Means a minimium lenght. For example: 6.
        /// </summary>
        MinLenght = 1,

        /// <summary>
        /// Means a maximium lenght. For example: 12.
        /// </summary>
        MaxLenght = 2,

        /// <summary>
        /// Means a minimium count of numeric chars. For example: 1.
        /// </summary>
        MinNumericChars = 3,

        /// <summary>
        /// Means a minimium count of special chars. For example: 1.
        /// </summary>
        MinSpecialChars = 4,

        /// <summary>
        /// Means the same char repeated. For example: 'aaaaaa'.
        /// </summary>
        MaxConsecutiveChars = 5,

        /// <summary>
        /// Means increment secuences. For example: 'abcdef'.
        /// </summary>
        MaxIncrementalChars = 6,

        /// <summary>
        /// Means decrements secuences. For example: 'fedcba'.
        /// </summary>
        MaxDecrementalChars = 7
    }
}
