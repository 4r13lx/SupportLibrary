using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

using SupportLibrary.Logging;

namespace SupportLibrary.Core
{
    /// <summary>
    /// Helper class for Debug related tasks.<para/>
    /// For example: retrieves a variable name or determine if a type is a subclass of another type.
    /// </summary>
    public static class Debug
    {
        /// <summary>
        /// Retrieve the Name of the given Variable inside the Expression.<para/>
        /// Usage: DebugHelper.GetVariableName(() => myVar).
        /// <remarks>Same functionality of C# 6.0 'nameof()' operator.</remarks>
        /// </summary>
        /// <typeparam name="T">The T-Type of the varibale.</typeparam>
        /// <param name="expr">Expression with the variable.</param>
        /// <returns>The variable name.</returns>
        public static string GetVarName<T>(Expression<Func<T>> expr)
        {
            try
            {
                if (expr == null) { throw new ArgumentNullException(nameof(expr), $"{ nameof(expr) } is null."); }

                if (expr.Body is MemberExpression)
                    return (expr.Body as MemberExpression).Member.Name;
                else
                    throw new ArgumentException(String.Format("Can't identify the received expression '{0}' as a variable.", expr), "expr");
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Logs and executes the given Action.
        /// </summary>
        /// <param name="expr">Action to execute.</param>
        /// <param name="logger">Logger to use for the logging.</param>
        //public static void LoggedExecution(Action action, LogHelper logger)
        public static void LoggedExecution(Expression<Action> expr, LogHelper logger)
        {
            try
            {
                if (expr == null)   { throw new ArgumentNullException(nameof(expr), $"{ nameof(expr) } is null."); }
                if (logger == null) { throw new ArgumentNullException(nameof(logger), $"{ nameof(logger) } is null."); }

                throw new NotImplementedException();

                if (expr.Body is MethodCallExpression)
                {
                    // log input
                    string xmlInput = ""; // = Serializer.SerializeToXml(parameterInfo, XmlSerialization.XmlSerializer);
                    logger.Log(EntryType.Info, xmlInput);

                    // execution
                    expr.Compile().Invoke(); // action.DynamicInvoke()

                    // log output
                    string xmlResult = "";
                    logger.Log(EntryType.Info, xmlResult);
                }
                else
                {
                    throw new ArgumentException(String.Format("Can't identify the received expression '{0}' as a function.", expr), "expr");
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Logs and executes the given Function.
        /// </summary>
        /// <typeparam name="TResult">T-Type for returning value.</typeparam>
        /// <param name="expr">Function to execute.</param>
        /// <param name="logger">Logger to use for the logging.</param>
        /// <returns>Returns the result of the Function execution.</returns>
        //public static TResult LoggedExecution<TResult>(Func<TResult> func, LogHelper logger)
        public static TResult LoggedExecution<TResult>(Expression<Func<TResult>> expr, LogHelper logger)
        {
            try
            {
                if (expr == null)   { throw new ArgumentNullException(nameof(expr), $"{ nameof(expr) } is null."); }
                if (logger == null) { throw new ArgumentNullException(nameof(logger), $"{ nameof(logger) } is null."); }

                throw new NotImplementedException();

                if (expr.Body is MethodCallExpression)
                {
                    // log input
                    MethodInfo methodInfo = (expr.Body as MethodCallExpression).Method;
                    ParameterInfo[] parameterInfo = methodInfo.GetParameters();
                    string xmlInput = Serializer.SerializeToXml(parameterInfo, XmlSerialization.XmlSerializer);
                    logger.Log(EntryType.Info, xmlInput);

                    // execution
                    TResult result = expr.Compile().Invoke(); // DynamicInvoke()

                    // log output
                    string xmlResult = Serializer.SerializeToXml(result, XmlSerialization.XmlSerializer);
                    logger.Log(EntryType.Info, xmlResult);

                    return result;
                }
                else
                {
                    throw new ArgumentException(String.Format("Can't identify the received expression '{0}' as a function.", expr), "expr");
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Measures the execution time for the given Action.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <returns>Elapsed execution time.</returns>
        public static TimeSpan TimedExecution(Action action)
        {
            try
            {
                if (action == null) { throw new ArgumentNullException(nameof(action), $"{ nameof(action) } is null."); }

                Stopwatch watch = Stopwatch.StartNew();

                action.Invoke();

                watch.Stop();
                return watch.Elapsed;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Measures the execution time for the given Func.
        /// </summary>
        /// <typeparam name="TResult">T-Type for returning value.</typeparam>
        /// <param name="func">Function to execute.</param>
        /// <returns>Elapsed execution time.</returns>
        public static TimeSpan TimedExecution<TResult>(Func<TResult> func)
        {
            try
            {
                if (func == null) { throw new ArgumentNullException(nameof(func), $"{ nameof(func) } is null."); }

                Stopwatch watch = Stopwatch.StartNew();

                func.Invoke();

                watch.Stop();
                return watch.Elapsed;
            }
            catch (Exception) { throw; }
        }
    }
}
