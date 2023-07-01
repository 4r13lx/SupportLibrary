using System;
using System.Text;
using System.Security.Cryptography;
using SupportLibrary.Text;

namespace SupportLibrary.Security
{
    /// <summary>
    /// Helper class for Cryptography related tasks.<para/>
    /// For example: Cryptography.ComputeHashSha1().
    /// </summary>
    public class Cryptography
    {
        /// <summary>
        /// Compute the SHA1 hash for the given text
        /// </summary>
        /// <param name="value">Value to compute.</param>
        /// <returns>The SHA1 hash of the input text.</returns>
        public static string ComputeHashSHA1(string value)
        {
            try
            {
                if (value.IsNullOrEmpty()) { throw new ArgumentNullException(nameof(value), "Text to compute its hash is empty."); }

                byte[] stream = SHA1Managed.Create().ComputeHash(new ASCIIEncoding().GetBytes(value)); // compute hash

                StringBuilder sb = new StringBuilder(); // to string
                for (int i = 0; i < stream.Length; i++) { sb.AppendFormat("{0:x2}", stream[i]); }

                return sb.ToString().ToUpper();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Compute the MD5 hash for the given text
        /// </summary>
        /// <param name="value">Value to compute.</param>
        /// <returns>The MD5 hash of the input text.</returns>
        public static string ComputeHashMD5(string value)
        {
            try
            {
                if (value.IsNullOrEmpty()) { throw new ArgumentNullException(nameof(value), "Text to compute its hash is empty."); }

                byte[] stream = MD5.Create().ComputeHash(new ASCIIEncoding().GetBytes(value));

                StringBuilder sb = new StringBuilder(); // to string
                for (int i = 0; i < stream.Length; i++) { sb.AppendFormat("{0:x2}", stream[i]); }

                return sb.ToString().ToUpper();
            }
            catch (Exception) { throw; }
        }
    }
}
