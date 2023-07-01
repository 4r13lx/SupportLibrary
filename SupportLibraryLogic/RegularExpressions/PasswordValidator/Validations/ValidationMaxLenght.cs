using System;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Internal class to validate a password for a maximium lenght.<para/>
    /// </summary>
    sealed class ValidationMaxLenght : ValidationBase
    {
        private int lenght = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="args">Validation arguments.</param>
        public ValidationMaxLenght(params object[] args)
            : base(args)
        {
            try 
	        {	        
                base.ValidationType = ValidationType.MaxLenght;
                this.lenght = Convert.ToInt32(args[0]);
	        }
	        catch (Exception) { throw; }
        }

        /// <summary>
        /// Validates the current password.
        /// </summary>
        /// <param name="password">Text to validate.</param>
        /// <returns>Returns the result of validation.</returns>
        public override bool Validate(string password)
        {
            return (password.Length <= this.lenght) ? true : false;
        }
    }
}
