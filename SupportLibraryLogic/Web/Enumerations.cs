using System;
using System.ComponentModel;

namespace SupportLibrary.Web
{
    /// <summary>
    /// Identifies a file MIME type.
    /// </summary>
    [DefaultValue(None)]
    public enum FileType
    {
        /// <summary>
        /// Means an unknown or unsupported MIME type.
        /// </summary>
        None = 0,

        /// <summary>
        /// Text file MIME-type.
        /// </summary>
        TEXT = 1,

        /// <summary>
        /// Comma-Separated Variables file MIME type.
        /// </summary>
        CSV = 2,

        /// <summary>
        /// Binary file MIME type.
        /// </summary>
        BINARY = 3,

        /// <summary>
        /// Portable Document Format file MIME type.
        /// </summary>
        PDF = 4,

        /// <summary>
        /// MS-Excel file MIME type.
        /// </summary>
        XLS = 5,

        /// <summary>
        /// MS-Excel Open XML Format file MIME type.
        /// </summary>
        XLSX = 6,

        /// <summary>
        /// MS-Word file MIME type.
        /// </summary>
        DOC = 7,

        /// <summary>
        /// MS-Word Open XML Format file MIME type.
        /// </summary>
        DOCX = 8,

        /// <summary>
        /// JPEG Image file MIME type.
        /// </summary>
        JPG = 9,

        /// <summary>
        /// PNG Image file MIME type.
        /// </summary>
        PNG = 10
    }

    /*
    /// <summary>
    /// HTML element. For example: 'BODY', 'DIV', 'SPAN' or 'BR'.
    /// </summary>
    [DefaultValue(None)]
    public enum Tag
    {
        /// <summary>
        /// Means an unknown or unsupported HTML tag.
        /// </summary>
        None = 0,

        /// <summary>
        /// The HTML tag.
        /// </summary>
        Html,

        /// <summary>
        /// The header tag.
        /// </summary>
        Header,

        /// <summary>
        /// The  
        /// </summary>
        CheckBox
    }
    */
}