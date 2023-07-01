using System;

namespace SupportLibrary.Core
{
    /// <summary>
    /// Supports shallow/deep cloning, which creates a new instance of a class with the same value as an existing instance.
    /// </summary>
    public interface ICloneableExtended
    {
        /// <summary>
        /// Creates a new object that is a shallow copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a shallow copy of this instance.</returns>
        object ShallowClone();

        /// <summary>
        /// Creates a new object that is a deep copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a deep copy of this instance.</returns>
        object DeepClone();
    }
}
