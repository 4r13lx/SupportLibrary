using System;
using System.Text.RegularExpressions;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Internal class to validate a password for a minimium numeric char count.<para/>
    /// </summary>
    sealed class ValidationMinNumericChars : ValidationBase
    {
        private int minCount = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="args">Validation arguments.</param>
        public ValidationMinNumericChars(params object[] args)
            : base(args)
        {
            base.ValidationType = ValidationType.MinNumericChars;
            this.minCount = Convert.ToInt32(args[0]);
        }

        /// <summary>
        /// Validates the current password.
        /// </summary>
        /// <param name="password">Text to validate.</param>
        /// <returns>Returns the result of validation.</returns>
        public override bool Validate(string password)
        {
            //string pattern = @"[0-9]{" + this.minCount + ",}"; // al menos N dígitos numéricos
            string pattern = @"(?:\d.*){" + this.minCount + ",}"; // al menos N dígitos numéricos
            return Regex.IsMatch(password, pattern);
        }
    }
}
