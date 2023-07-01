using System;
using SupportLibrary.Text;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Helper class for Password Validator related tasks.<para/>
    /// </summary>
    public sealed class PasswordValidator
    {
        /// <summary>
        /// Gets/Sets current password text to validate.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets the list of validations for the password.
        /// </summary>
        public Validations Validations { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PasswordValidator() : this("")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="password">Value to validate.</param>
        public PasswordValidator(string password)
        {
            this.Password = password;
            this.Validations = new Validations();
        }

        /// <summary>
        /// Validates the current password.
        /// </summary>
        /// <returns>Returns the result of validation.</returns>
        public bool Validate()
        {
            try
            {
                return this.ValidateVerbose().Result;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Validates the current password. Detailed mode.
        /// </summary>
        /// <returns>Returns a ValidationResult object with the detailed result of validation.</returns>
        public ValidationResult ValidateVerbose()
        {
            try
            {
                if (this.Password.IsNullOrEmpty())              { throw new ArgumentNullException("Password", "Password is null."); }
                if (this.Validations.ValidationList.Count == 0) { throw new ArgumentNullException("ValidationList", "Validation list is empty."); }

                ValidationResult validationResult = new ValidationResult() { Result = true };

                foreach (ValidationBase validation in this.Validations.ValidationList)
                {
                    bool result = validation.Validate(this.Password);

                    if (result == false)
                    {
                        validationResult.Result = false;
                        validationResult.WithErrors.Add(validation.ValidationType);
                        validationResult.DetailedMessage += (validationResult.DetailedMessage != "") ? Environment.NewLine : String.Empty;
                        validationResult.DetailedMessage += String.Format("- {0} validation failed.", validation.ValidationType);
                    }
                }

                return validationResult;
            }
            catch (Exception) { throw; }
        }
    }
}
