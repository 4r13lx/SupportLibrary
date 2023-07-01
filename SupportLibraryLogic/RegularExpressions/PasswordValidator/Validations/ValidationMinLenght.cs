using System;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Internal class to validate a password for a minimium lenght.<para/>
    /// </summary>
    sealed class ValidationMinLenght : ValidationBase
    {
        private int lenght = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="args">Validation arguments.</param>
        public ValidationMinLenght(params object[] args)
            : base(args)
        {
            base.ValidationType = ValidationType.MinLenght;
            this.lenght = Convert.ToInt32(args[0]);
        }

        /// <summary>
        /// Validates the current password.
        /// </summary>
        /// <param name="password">Text to validate.</param>
        /// <returns>Returns the result of validation.</returns>
        public override bool Validate(string password)
        {
            return (password.Length >= this.lenght) ? true : false;
        }
    }
}
