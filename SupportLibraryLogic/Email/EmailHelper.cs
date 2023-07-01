using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

using SupportLibrary.RegularExpressions;

namespace SupportLibrary.Email
{
    /// <summary>
    /// Helper class for Mailing related tasks.<para/>
    /// For example: Split a string with address into a list, or validation of strings as valid email address.
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// Validate if the given text is a valid Email address.
        /// </summary>
        /// <param name="text">Text to validate.</param>
        /// <returns>True if the text parameter is a valid Email address; otherwise false.</returns>
        public static bool IsValidEmailAddress(string text)
        {
            if (text == null || text == "")     { return false; }
            if (text.Length > 255)              { return false; }
            return Regex.IsMatch(text, RegexHelper.Miscellaneous.EMAIL, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Parse the given text, spliting and validating email addresses separated by a specific character.
        /// </summary>
        /// <param name="text">Text to parse.</param>
        /// <param name="delimiter">Delimiter character.</param>
        /// <returns>A List&lt;string&gt; that contains the addresses in the text parameter.</returns>
        public static List<string> SplitAddresses(string text, char delimiter)
        {
            return SplitAddresses(text, new char[] { delimiter });
        }

        /// <summary>
        /// Parse the given text, spliting and validating email addresses separated by the specific characters.
        /// </summary>
        /// <param name="text">Text to parse.</param>
        /// <param name="delimiters">Delimiter characters. Default values are ',' and ';'.</param>
        /// <returns>A List&lt;String&gt; that contains the addresses in the text parameter.</returns>
        public static List<string> SplitAddresses(string text, char[] delimiters = null)
        {
            text = text ?? "";
            delimiters = delimiters ?? new char[] { ',', ';' };
            List<string> lstResults = new List<string>();

            string[] arrayAddresses = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < arrayAddresses.Length; i++)
            {
                arrayAddresses[i] = arrayAddresses[i].Trim();
                if (arrayAddresses[i] == "") { continue; }

                if (!EmailHelper.IsValidEmailAddress(arrayAddresses[i]))
                    throw new ArgumentException(String.Format("Error on processing the text '{0}'. {1}The email address '{2}' is invalid.", text, Environment.NewLine, arrayAddresses[i]), "text");

                lstResults.Add(arrayAddresses[i]);
            }

            return lstResults;
        }
    }
}
