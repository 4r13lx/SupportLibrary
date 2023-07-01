using System;
using System.Text.RegularExpressions;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Internal class to validate a password for consecutive chars.<para/>
    /// </summary>
    sealed class ValidationMaxConsecutiveChars : ValidationBase
    {
        private int maxCount = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="args">Validation arguments.</param>
        public ValidationMaxConsecutiveChars(params object[] args)
            : base(args)
        {
            base.ValidationType = ValidationType.MaxConsecutiveChars;
            this.maxCount = Convert.ToInt32(args[0]);
        }

        /// <summary>
        /// Validates the current password.
        /// </summary>
        /// <param name="password">Text to validate.</param>
        /// <returns>Returns the result of validation.</returns>
        public override bool Validate(string password)
        {
            string repetition = "";
            for (int i = 0; i < this.maxCount; i++) { repetition += @"\1"; }

            string pattern = @"^(?!.*(.)" + repetition + ")";
            return Regex.IsMatch(password, pattern);
        }
    }
}
