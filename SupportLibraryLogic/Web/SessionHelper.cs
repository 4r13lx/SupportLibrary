using System;
using System.Web;
using System.Web.SessionState;

using SupportLibrary.Text;

namespace SupportLibrary.Web
{
    /// <summary>
    /// Helper class for Page.Session access related tasks.<para/>
    /// For example: Read values from session in a typed way.
    /// </summary>
    public sealed class SessionHelper : ISessionHelper
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HttpSessionStateBase Session { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">HttpContext from wich to use the session.</param>
        public SessionHelper(HttpContext context)
        {
            if (context == null) { throw new ArgumentNullException(nameof(context), $"{ nameof(context) } is null."); }
            this.Session = new HttpSessionStateWrapper(context.Session);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">HttpContextBase from wich to use the session. Intended for testing purposes.</param>
        public SessionHelper(HttpContextBase context)
        {
            this.Session = context.Session;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session">HttpSessionState from wich to use the session.</param>
        public SessionHelper(HttpSessionState session)
        {
            this.Session = new HttpSessionStateWrapper(session);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session">HttpSessionStateBase from wich to use the session. Intended for testing purposes.</param>
        public SessionHelper(HttpSessionStateBase session)
        {
            this.Session = session;
        }

        /// <summary>
        /// Gets a session value by name.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="name">The key name of the session value.</param>
        /// <returns>Session value converted to T-Type.</returns>
        public T Retrieve<T>(string name)
        {
            try
            {
                if (name.IsNullOrEmpty()) { throw new ArgumentNullException(nameof(name), $"{ nameof(name) } is null"); }

                string errorMessage = "Invalid Session. Please re-start the application.";
                return this.RetrieveWithMessage<T>(name, errorMessage);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Gets a session value by name, plus a error message in case that session key do not exists.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="name">The key name of the session value.</param>
        /// <param name="errorMessage">Error message in case that the session key do not exists.</param>
        /// <returns>Session value converted to T-Type.</returns>
        public T RetrieveWithMessage<T>(string name, string errorMessage)
        {
            try
            {
                if (name.IsNullOrEmpty()) { throw new ArgumentNullException(nameof(name), $"{ nameof(name) } is null"); }
                if (errorMessage.IsNullOrEmpty()) { throw new ArgumentNullException(nameof(errorMessage), $"{ nameof(errorMessage) } is null"); }

                //if (HttpContext.Current.Session[name] == null || HttpContext.Current.Session[name].GetType() != typeof(T))
                if (this.Session[name] == null || this.Session[name].GetType() != typeof(T))
                    throw new ArgumentException(errorMessage);

                //return (T)HttpContext.Current.Session[name];
                return (T)this.Session[name];
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Gets a session value by name, plus a resource key in case that session key do not exists.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="name">The key name of the session value.</param>
        /// <param name="errorResourceKey">Local resource key with the error message in case that the session key do not exists.</param>
        /// <returns>Session value converted to T-Type.</returns>
        public T RetrieveWithRecourceKey<T>(string name, string errorResourceKey)
        {
            try
            {
                if (name.IsNullOrEmpty()) { throw new ArgumentNullException(nameof(name), $"{ nameof(name) } is null"); }
                if (errorResourceKey.IsNullOrEmpty()) { throw new ArgumentNullException(nameof(errorResourceKey), $"{ nameof(errorResourceKey) } is null"); }

                string virtualPath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
                string errorMessage = HttpContext.GetLocalResourceObject(virtualPath, errorResourceKey).ToString();

                return this.RetrieveWithMessage<T>(name, errorMessage);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Try to retrieve a session value by name.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="name">The key name of the session value.</param>
        /// <param name="result">The found value, otherwise null.</param>
        /// <returns>True if the given value was found.</returns>
        public bool TryRetrieve<T>(string name, ref T result)
        {
            try
            {
                if (name.IsNullOrEmpty()) { throw new ArgumentNullException(nameof(name), $"{ nameof(name) } is null"); }

                // if (HttpContext.Current.Session[name] != null)
                if (this.Session[name] != null)
                {
                    //result = (T)HttpContext.Current.Session[name];
                    result = (T)this.Session[name];
                    return true;
                }
                else
                {
                    result = default(T);
                    return false;
                }
            }
            catch (Exception) { throw; }
        }
    }
}
