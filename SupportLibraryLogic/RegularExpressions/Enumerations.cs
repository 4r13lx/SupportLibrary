using System;
using System.ComponentModel;

namespace SupportLibrary.RegularExpressions
{
    /// <summary>
    /// Identifies a business document. For example: 'BillOfLadding', 'Booking' or 'Container'.
    /// </summary>
    [DefaultValue(None)]
    public enum BusinessDocument
    {
        /// <summary>
        /// Means an unidentified business document.
        /// </summary>
        None = 0,

        /// <summary>
        /// Means a Bill of Ladding document.
        /// </summary>
        BillOfLadding = 1,

        /// <summary>
        /// Means a Booking document.
        /// </summary>
        Booking = 2,

        /// <summary>
        /// Means a Container document.
        /// </summary>
        Container = 3,

        /// <summary>
        /// Means a Shipping Instruction document.
        /// </summary>
        ShippingInstruction = 4
    }
}
