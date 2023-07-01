using System;
using System.Text.RegularExpressions;

namespace SupportLibrary.RegularExpressions.PasswordValidation
{
    /// <summary>
    /// Internal class to validate a password for decremental secuences.<para/>
    /// </summary>
    sealed class ValidationMaxDecrementalChars : ValidationBase
    {
        private int maxCount = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="args">Validation arguments.</param>
        public ValidationMaxDecrementalChars(params object[] args)
            : base(args)
        {
            base.ValidationType = ValidationType.MaxDecrementalChars;
            this.maxCount = Convert.ToInt32(args[0]);
        }

        /// <summary>
        /// Validates the current password.
        /// </summary>
        /// <param name="password">Text to validate.</param>
        /// <returns>Returns the result of validation.</returns>
        public override bool Validate(string password)
        {
            int secuenceCount = 0;

            for (int i = 1; i < password.Length; i++)
            {
                char prevChar = password[i-1];
                char currentChar = password[i];

                // los caracteres especiales no se consideran incrementales
                if (IsSpecialChar(currentChar)) { secuenceCount = 0; }

                // chequear si el caracter actual está en secuencia respecto al caracter anterior
                if (AreDecrementalChars(prevChar, currentChar)) { secuenceCount++; } else { secuenceCount = 0; }

                // chequear si se alcanzó la máxima secuencia de incrementos permitida
                if (secuenceCount == maxCount) { return false; }
            }

            return true;
        }

        private bool IsSpecialChar(char c)
        {
            return Regex.IsMatch(c.ToString(), @"[^a-zA-Z\d]{1}"); // busca dígitos no alfabéticos ni numéricos
        }

        private bool AreDecrementalChars(char a, char b)
        {
            return ((a - b) == 1) ? true : false;
        }
    }
}
