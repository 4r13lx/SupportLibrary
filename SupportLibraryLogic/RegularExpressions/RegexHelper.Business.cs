using System;
using System.Text.RegularExpressions;
using SupportLibrary.Text;

namespace SupportLibrary.RegularExpressions
{
    /// <summary>
    /// Maritime business related regular expressions.
    /// </summary>
    public sealed class Business
    {
        internal Business() { }

        /// <summary>
        /// Regular expression to validate a Bill of Ladding number.
        /// </summary>
        /// <remarks>Allowed patterns are 'MMCXX0000000' or 'MMDXXX000000'.</remarks>
        public readonly string BILL_OF_LADDING = @"^(MMC|MMD)([a-zA-Z]{2}[0-9]{7}|[a-zA-Z]{3}[0-9]{6})$";

        /// <summary>
        /// Regular expression to validate a Booking number.
        /// </summary>
        /// <remarks>Allowed patterns are 'AR0000000', 'IAR0000000', '123AR0000000', '123IAR0000000', '123IMZ0000000' or 'EBK00000000'.</remarks>
        public readonly string BOOKING = @"^((AR|IAR|123AR|123IAR|123IMZ)[0-9]{7}|^((EBK)[0-9]{8}))$";

        /// <summary>
        /// Regular expression to validate a Container number.
        /// <remarks>Allowed pattern is 'XXXX0000000'.</remarks>
        /// </summary>
        public readonly string CONTAINER = @"^[a-zA-Z]{4}[0-9]{7}$";

        /// <summary>
        /// Regular expression to validate a Shipping Instruction number.
        /// <remarks>Allowed pattern is '000000000'.</remarks>
        /// </summary>
        public readonly string SHIPPING_INSTRUCTION = @"^[0-9]{9}$";

        /// <summary>
        /// Regular expression to validate a Financial FormNumber.
        /// </summary>
        public readonly string FORM_NUMBER = @"^[0-9]{1,8}$";

        /// <summary>
        /// Validate if the given text is a valid Bill of Ladding number.
        /// </summary>
        /// <param name="text">Text to validate. Allowed patterns are 'MMCXX0000000' or 'MMDXXX000000'.</param>
        /// <returns>True if the text parameter is a valid Bill of Ladding number; otherwise false.</returns>
        public bool IsValidBillOfLaddingNumber(string text)
        {
            try
            {
                if (text.IsNullOrEmpty()) { return false; }
                return Regex.IsMatch(text, this.BILL_OF_LADDING, RegexOptions.IgnoreCase);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Validate if the given text is a valid Booking number.
        /// </summary>
        /// <param name="text">Text to validate. Allowed patterns are 'AR0000000', 'IAR0000000', '123AR0000000' or '123IAR0000000'.</param>
        /// <returns>True if the text parameter is a valid Booking number; otherwise false.</returns>
        public bool IsValidBookingNumber(string text)
        {
            try
            {
                if (text.IsNullOrEmpty()) { return false; }
                return Regex.IsMatch(text, this.BOOKING, RegexOptions.IgnoreCase);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Validate if the given text is a valid Container number.
        /// </summary>
        /// <param name="text">Text to validate. Allowed pattern is 'XXXX0000000'.</param>
        /// <returns>True if the text parameter is a valid Container number; otherwise false.</returns>
        public bool IsValidContainerNumber(string text)
        {
            try
            {
                if (text.IsNullOrEmpty()) { return false; }
                return Regex.IsMatch(text, this.CONTAINER, RegexOptions.IgnoreCase);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Validate if the given text is a valid Shipping Instruction.
        /// </summary>
        /// <param name="text">Text to validate. Allowed pattern is '000000000'.</param>
        /// <returns>True if the text parameter is a valid Shipping Instruction number; otherwise false.</returns>
        public bool IsValidShippingInstruction(string text)
        {
            try
            {
                if (text.IsNullOrEmpty()) { return false; }
                return Regex.IsMatch(text, this.SHIPPING_INSTRUCTION, RegexOptions.IgnoreCase);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Validate if the given text is a valid Form Number.
        /// </summary>
        /// <param name="text">Text to validate. Allowed pattern is '00000000'.</param>
        /// <returns>True if the text parameter is a valid FormNumber; otherwise false.</returns>
        public bool IsValidFormNumber(string text)
        {
            try
            {
                if (text.IsNullOrEmpty()) { return false; }
                return Regex.IsMatch(text, this.FORM_NUMBER, RegexOptions.IgnoreCase);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Identify if the given text corresponds to a BL, BK, CNTR or SI.
        /// </summary>
        /// <param name="text">Text to evaluate.</param>
        /// <returns>A BusinessDocument enumeration value of the matching business object; otherwise BusinessDocument.Unknown.</returns>
        public BusinessDocument IdentifyDocument(string text)
        {
            if (IsValidBillOfLaddingNumber(text))       { return BusinessDocument.BillOfLadding; }
            else if (IsValidBookingNumber(text))        { return BusinessDocument.Booking; }
            else if (IsValidContainerNumber(text))      { return BusinessDocument.Container; }
            else if (IsValidShippingInstruction(text))  { return BusinessDocument.ShippingInstruction; }
            else                                        { return BusinessDocument.None; }
        }
    }
}
