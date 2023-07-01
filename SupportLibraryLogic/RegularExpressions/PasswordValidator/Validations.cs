using System;
using System.Collections.Generic;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Encapsulates a list of validations for the current password.
    /// </summary>
    public sealed class Validations
    {
        /// <summary>
        /// Gets a list of active validations for the current password.
        /// </summary>
        public List<IValidation> ValidationList { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Validations()
        {
            this.ValidationList = new List<IValidation>();
        }

        /// <summary>
        /// Validates a password for a minimium lenght.
        /// </summary>
        /// <param name="lenght">Minimium lenght.</param>
        public void SetMinLenght(int lenght)
        {
            IValidation validation = new ValidationMinLenght(lenght);
            this.ValidationList.Add(validation);
        }

        /// <summary>
        /// Validates a password for a maximium lenght.
        /// </summary>
        /// <param name="lenght">Maximium lenght.</param>
        public void SetMaxLenght(int lenght)
        {
            IValidation validation = new ValidationMaxLenght(lenght);
            this.ValidationList.Add(validation);
        }

        /// <summary>
        /// Validates a password for a minimium numeric char count.
        /// </summary>
        /// <param name="count">Minimium numeric char count.</param>
        public void SetMinNumericChars(int count)
        {
            IValidation validation = new ValidationMinNumericChars(count);
            this.ValidationList.Add(validation);
        }

        /// <summary>
        /// Validates a password for a minimium special char count.
        /// </summary>
        /// <param name="count">Minimium special char count.</param>
        public void SetMinSpecialChars(int count)
        {
            IValidation validation = new ValidationMinSpecialChars(count);
            this.ValidationList.Add(validation);
        }

        /// <summary>
        /// Validates a password for consecutive chars.
        /// </summary>
        /// <param name="count">Maximium consecutive chars.</param>
        public void SetMaxConsecutiveChars(int count)
        {
            IValidation validation = new ValidationMaxConsecutiveChars(count);
            this.ValidationList.Add(validation);
        }

        /// <summary>
        /// Validates a password for incremental secuences.
        /// </summary>
        /// <param name="count">Maximium incremental secuence.</param>
        public void SetMaxIncrementalChars(int count)
        {
            IValidation validation = new ValidationMaxIncrementalChars(count);
            this.ValidationList.Add(validation);
        }

        /// <summary>
        /// Validates a password for decremental secuences.
        /// </summary>
        /// <param name="count">Maximium decremental secuence.</param>
        public void SetMaxDecrementalChars(int count)
        {
            IValidation validation = new ValidationMaxDecrementalChars(count);
            this.ValidationList.Add(validation);
        }
    }
}
