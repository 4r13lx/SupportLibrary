using System;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Internal base class to validate a password for a given criteria.
    /// </summary>
    abstract class ValidationBase : IValidation
    {
        /// <summary>
        /// Identifies a password validation. For example: 'MinLenght', 'MinNumericChars' or 'MinSpecialChars'.
        /// </summary>
        public ValidationType ValidationType { get; set; }

        /// <summary>
        /// Arguments for a password validation.
        /// </summary>
        public object[] ValidationParams { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="args">Abstract arguments.</param>
        public ValidationBase(params object[] args)
        {
            this.ValidationType = ValidationType.None;
            this.ValidationParams = args;
        }

        /// <summary>
        /// Validates the current password.
        /// </summary>
        /// <param name="password">Text to validate.</param>
        /// <returns>Returns the result of validation.</returns>
        public abstract bool Validate(string password);
    }
}
