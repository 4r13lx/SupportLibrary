using System;

namespace SupportLibrary.Testing
{
    /// <summary>
    /// Helper class for software Testing over exceptions related tasks.<para/>
    /// For example: Assert that a given operation throws an exception.
    /// </summary>
    public sealed class Exceptions
    {
        internal Exceptions() { }

        /// <summary>
        /// Assert if the given Action throws an exception.
        /// </summary>
        /// <typeparam name="TException">T-Type of expected exception</typeparam>
        /// <param name="action">Action to execute</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        /// <returns>The expected exception, if thrown.</returns>
        public TException Throws<TException>(Action action, string message = "") where TException : Exception
        {
            try
            {
                action.Invoke();
            }
            catch (TException ex)
            {
                if (ex.GetType() == typeof(TException))
                {
                    return ex;
                }
                else
                {
                    string message1 = String.Format("Expected exception is '{0}'. Instead a exception of type '{1}' was thrown. {2}", typeof(TException).FullName, ex.GetType(), message);
                    throw new AssertFailedExceptionEx(AssertFailedExceptionCause.DiferentExceptionThrown, message1);
                }
            }
            catch (Exception ex)
            {
                string message2 = String.Format("Expected exception is '{0}'. Instead a exception of type '{1}' was thrown. {2}", typeof(TException).FullName, ex.GetType(), message);
                throw new AssertFailedExceptionEx(AssertFailedExceptionCause.DiferentExceptionThrown, message2);
            }

            string message3 = String.Format("Expected exception '{0}' was not thrown. {1}", typeof(TException).FullName, message);
            throw new AssertFailedExceptionEx(AssertFailedExceptionCause.NoExceptionThrown, message3);
        }
    }
}
