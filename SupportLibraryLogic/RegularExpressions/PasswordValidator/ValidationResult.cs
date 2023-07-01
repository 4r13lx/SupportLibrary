using System;
using System.Collections.Generic;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Represent a detailed results of a password validation.
    /// </summary>
    public sealed class ValidationResult
    {
        /// <summary>
        /// Gets the validation result.
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// Gets a list of not passed validations.
        /// </summary>
        public List<ValidationType> WithErrors { get; set; }

        /// <summary>
        /// Gets a descriptive message with the results of validations.
        /// </summary>
        public string DetailedMessage { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ValidationResult()
        {
            this.Result = true;
            this.WithErrors = new List<ValidationType>();
            this.DetailedMessage = "";
        }
    }
}
