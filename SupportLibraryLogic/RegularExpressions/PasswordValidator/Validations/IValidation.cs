using System;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Interface for Password Validator related tasks.<para/>
    /// </summary>
    public interface IValidation
    {
        /// <summary>
        /// Validates the given password 
        /// </summary>
        /// <param name="password">Text to validate.</param>
        /// <returns>Returns the result of the validation.</returns>
        bool Validate(string password);
    }
}
