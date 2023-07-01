using System;

namespace SupportLibrary.Web
{
    /// <summary>
    /// Interface for Page.Session access related tasks.<para/>
    /// </summary>
    public interface ISessionHelper
    {
        /// <summary>
        /// Gets a session value by name.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="name">The key name of the session value.</param>
        /// <returns>Session value converted to T-Type.</returns>
        T Retrieve<T>(string name);

        /// <summary>
        /// Gets a session value by name, plus a error message in case that session key do not exists.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="name">The key name of the session value.</param>
        /// <param name="errorMessage">Error message in case that the session key do not exists.</param>
        /// <returns>Session value converted to T-Type.</returns>
        T RetrieveWithMessage<T>(string name, string errorMessage);

        /// <summary>
        /// Gets a session value by name, plus a resource key in case that session key do not exists.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="name">The key name of the session value.</param>
        /// <param name="errorResourceKey">Local resource key with the error message in case that the session key do not exists.</param>
        /// <returns>Session value converted to T-Type.</returns>
        T RetrieveWithRecourceKey<T>(string name, string errorResourceKey);

        /// <summary>
        /// Try to retrieve a session value by name.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="name">The key name of the session value.</param>
        /// <param name="result">The found value, otherwise null.</param>
        /// <returns>True if the given value was found.</returns>
        bool TryRetrieve<T>(string name, ref T result);
    }
}
