using System;
using System.Text.RegularExpressions;
using SupportLibrary.Text;

namespace SupportLibrary.RegularExpressions
{
    /// <summary>
    /// Miscellaneous regular expressions.
    /// </summary>
    public sealed class Miscellaneous
    {
        internal Miscellaneous() { }

        /// <summary>
        /// Regular expression to validate a Credit Card number.
        /// </summary>
        public readonly string CREDIT_CARD = @"^((67\d{2})|(4\d{3})|(5[1-5]\d{2})|(6011))(-?\s?\d{4}){3}|(3[4,7])\ d{2}-?\s?\d{6}-?\s?\d{5}$";

        /// <summary>
        /// Regular expression to validate a CUIT number.
        /// </summary>
        public readonly string CUIT_NUMBER = @"^[\d]{2}-[\d]{8}-[\d]{1}$";

        /// <summary>
        /// Regular expression to validate a CUIT number without dashes.
        /// </summary>
        public readonly string CUIT_NUMBER_NO_DASHES = @"^[\d]{11}$";

        /// <summary>
        /// Regular expression to validate an Email address.
        /// </summary>
        public readonly string EMAIL = @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,10})$"; // \\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*

        /// <summary>
        /// Regular expression to validate an IP Address.
        /// </summary>
        public readonly string IP = @"/^(([1-9]?[0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]).){3}([1-9]?[0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$/";

        /// <summary>
        /// Regular expression to validate a Numeric text.
        /// </summary>
        public readonly string NUMERIC = @"^\d+$";

        /// <summary>
        /// Regular expression to validate a Strong Password.
        /// Requirements: 1+ uppercase letter, 1+ lowercase letter, 1+ number o special character, 8+ minimum length
        /// </summary>
        // TODO: revisar implementacion + tests unitarios
        public readonly string STRONG_PASSWORD = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

        /// <summary>
        /// Regular expression to validate a Telephone number.
        /// </summary>
        /// <remarks>Allowed pattern is ''.</remarks>
        // TODO: revisar implementacion + tests unitarios
        public readonly string TELEPHONE = @"^\+?\d{1,3}?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$";

        /// <summary>
        /// Regular expression to validate an URL address.
        /// </summary>
        public readonly string URL = @"/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \?=.-]*)*\/?$/";

        /// <summary>
        /// Regular expression to validate a UserName.
        /// Requirements: length between 4 and 20 characters.
        /// </summary>
        public readonly string USER_NAME = @"/^[a-z\d_]{4,20}$/i";

        /// <summary>
        /// Regular expression to validate a Zip Code.
        /// </summary>
        // TODO: revisar implementacion + tests unitarios
        public readonly string ZIP_CODE = @"^([1-9]{2}|[0-9][1-9]|[1-9][0-9])[0-9]{3}$";

        /// <summary>
        /// Validate if the given text is all numbers.
        /// </summary>
        /// <param name="value">Text to validate.</param>
        /// <returns>True if the value is all numbers.</returns>
        public bool IsNumeric(string value)
        {
            if (value.IsNullOrEmpty()) { return false; }

            return Regex.IsMatch(value, this.NUMERIC);
        }

        /// <summary>
        /// Validate if the given text is a valid CUIT number. The text must include the separator dashes.
        /// </summary>
        /// <param name="value">Text to validate.</param>
        /// <param name="dashIncluded">True if the text to validate includes dashes.</param>
        /// <returns>True if the value is a valid CUIT number; otherwise false.</returns>
        /// <remarks>Allowed patterns are '00-00000000-0' (with dashes) or '00000000000' (without dashes).</remarks>
        public bool IsValidCuitNumber(string value, bool dashIncluded = true)
        {
            if (value.IsNullOrEmpty())                                      { return false; }
            if (!this.ValidateLength(value, dashIncluded))                  { return false; }
            if (!this.ValidatePatternMatch(value, dashIncluded))            { return false; }
            if (!this.ValidateCuitNumberCheckDigit(value.Replace("-", ""))) { return false; }

            return true;
        }

        private bool ValidateLength(string value, bool dashIncluded)
        {
            try
            {
                return (dashIncluded && value.Length == 13) || (!dashIncluded && value.Length == 11);
            }
            catch (Exception) { throw; }
        }

        private bool ValidatePatternMatch(string value, bool dashIncluded)
        {
            try
            {
                if (dashIncluded)
                    return Regex.IsMatch(value, this.CUIT_NUMBER, RegexOptions.IgnoreCase);
                else
                    return Regex.IsMatch(value, this.CUIT_NUMBER_NO_DASHES, RegexOptions.IgnoreCase);
            }
            catch (Exception) { throw; }
        }

        private bool ValidateCuitNumberCheckDigit(string value)
        {
            try
            {
                int total = 0;
                byte culculatedCheckDigit = 0;
                byte receivedCheckDigit = 0;
                byte[] correlatedDigits = new byte[10];

                // verificar longitud del texto
                if (value.Length != 11) { return false; }

                // individualizar y multiplicar los dígitos por su peso especifico
                correlatedDigits[0] = Convert.ToByte(Convert.ToByte(value.Substring(0, 1)) * 5);
                correlatedDigits[1] = Convert.ToByte(Convert.ToByte(value.Substring(1, 1)) * 4);
                correlatedDigits[2] = Convert.ToByte(Convert.ToByte(value.Substring(2, 1)) * 3);
                correlatedDigits[3] = Convert.ToByte(Convert.ToByte(value.Substring(3, 1)) * 2);
                correlatedDigits[4] = Convert.ToByte(Convert.ToByte(value.Substring(4, 1)) * 7);
                correlatedDigits[5] = Convert.ToByte(Convert.ToByte(value.Substring(5, 1)) * 6);
                correlatedDigits[6] = Convert.ToByte(Convert.ToByte(value.Substring(6, 1)) * 5);
                correlatedDigits[7] = Convert.ToByte(Convert.ToByte(value.Substring(7, 1)) * 4);
                correlatedDigits[8] = Convert.ToByte(Convert.ToByte(value.Substring(8, 1)) * 3);
                correlatedDigits[9] = Convert.ToByte(Convert.ToByte(value.Substring(9, 1)) * 2);
                receivedCheckDigit = Convert.ToByte(value.Substring(10, 1));

                // sumar los resultantes
                foreach (int val in correlatedDigits) { total += val; }

                // calcular el digito de control
                culculatedCheckDigit = Convert.ToByte((11 - (total % 11)) % 11);

                // comparar el digito calculado con el recibido
                return (receivedCheckDigit == culculatedCheckDigit);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
