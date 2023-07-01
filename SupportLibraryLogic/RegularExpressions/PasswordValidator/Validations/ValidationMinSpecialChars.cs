using System;
using System.Text.RegularExpressions;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Internal class to validate a password for a minimium special char count.<para/>
    /// </summary>
    sealed class ValidationMinSpecialChars : ValidationBase
    {
        private int minCount = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="args">Validation arguments.</param>
        public ValidationMinSpecialChars(params object[] args)
            : base(args)
        {
            base.ValidationType = ValidationType.MinSpecialChars;
            this.minCount = Convert.ToInt32(args[0]);
        }

        /// <summary>
        /// Validates the current password.
        /// </summary>
        /// <param name="password">Text to validate.</param>
        /// <returns>Returns the result of validation.</returns>
        public override bool Validate(string password)
        {
            //string pattern = @"[^a-zA-Z\d]{" + this.minCount + ",}"; // al menos N dígitos no alfabéticos ni numéricos
            string pattern = @"(?:[^a-zA-Z0-9].*){" + this.minCount + ",}"; // al menos N dígitos no alfabéticos ni numéricos
            return Regex.IsMatch(password, pattern);
        }
    }
}
