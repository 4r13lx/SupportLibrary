using System.Data;

namespace SupportLibrary.Testing
{
    /// <summary>
    /// Helper class for software Testing related tasks.<para/>
    /// For example: new Asserts operations for Unit Testing.
    /// </summary>
    public static class AssertEx
    {
        /// <summary>
        /// Asserts for Collections related operations.
        /// </summary>
        public static readonly Collections Collections = new Collections();

        /// <summary>
        /// Asserts for Conditional related operations.
        /// </summary>
        public static readonly Condition Condition = new Condition();

        /// <summary>
        /// Asserts for Data related operations.
        /// </summary>
        public static readonly Data Data = new Data();

        /// <summary>
        /// Asserts for Exceptions related operations.
        /// </summary>
        public static readonly Exceptions Exceptions = new Exceptions();

        /// <summary>
        /// Asserts for Text related operations.
        /// </summary>
        public static readonly Text Text = new Text();
    }
}
