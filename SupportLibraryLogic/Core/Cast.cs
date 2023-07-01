using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SupportLibrary.Core
{
    /// <summary>
    /// Helper class for Casting related tasks.<para/>
    /// For example: Cast.To(source, dest) or source.CastTo&lt;dest&gt;().
    /// </summary>
    public class Cast
    {
        /// <summary>
        /// Cast an object to TResult.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="source">Object to cast into another type.</param>
        /// <param name="recursive">Flag to cast composite types too.</param>
        /// <returns>A T-Type instance with values from source entity.</returns>
        public static TResult To<TResult>(object source, bool recursive = true) where TResult : new()
        {
            try
            {
                return Cast.To<TResult>(source, new TResult(), recursive);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Cast an object to TResult.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="source">Object to cast into another type.</param>
        /// <param name="dest">A T-Type instance for the return value.</param>
        /// <param name="recursive">Flag to cast composite types too.</param>
        /// <returns>A T-Type instance with values from source entity.</returns>
        public static TResult To<TResult>(object source, TResult dest, bool recursive = true) where TResult : new()
        {
            try
            {
                if (source == null) { throw new ArgumentNullException(nameof(source), $"{ nameof(source) } is null."); }
                if (dest == null)   { throw new ArgumentNullException(nameof(dest), $"{ nameof(dest) } is null."); }

                PropertyDescriptorCollection sourceProperties = TypeDescriptor.GetProperties(source);
                PropertyDescriptorCollection destProperties = TypeDescriptor.GetProperties(dest);

                foreach (PropertyDescriptor sourceProp in sourceProperties)
                {
                    PropertyDescriptor destProp = destProperties.Find(sourceProp.Name, true);
                    if (destProp == null) { continue; }

                    if (sourceProp.PropertyType.IsClass == false || sourceProp.PropertyType == typeof(String))
                    {
                        //if(Nullable.GetUnderlyingType(sourceProp.PropertyType) != null)
                        try
                        {
                            destProp.SetValue(dest, Convert.ChangeType(sourceProp.GetValue(source), destProp.PropertyType));
                        }
                        catch 
                        {
                            destProp.SetValue(dest, sourceProp.GetValue(source));
                        }
                    }
                    
                    if (sourceProp.PropertyType.IsClass == true && sourceProp.PropertyType != typeof(String) && recursive)
                    {
                        object destCompositeProp = destProp.PropertyType.GetConstructor(Type.EmptyTypes).Invoke(null);
                        destProp.SetValue(dest, Cast.To(sourceProp.GetValue(source), destCompositeProp, true));
                    }
                }

                return dest;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Cast a list of objects to list of TResult.
        /// </summary>
        /// <typeparam name="T">T-Type for the source value.</typeparam>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="source">Object to cast into another type.</param>
        /// <param name="recursive">Flag to cast composite types too.</param>
        /// <returns>A T-Type instance with values from source entity.</returns>
        public static List<TResult> CollectionTo<T, TResult>(IList<T> source, bool recursive = true) where TResult : new()
        {
            try
            {
                return Cast.CollectionTo<T, TResult>(source, new TResult(), recursive);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Cast a list of objects to list of TResult.
        /// </summary>
        /// <typeparam name="T">T-Type for the source value.</typeparam>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="source">Object to cast into another type.</param>
        /// <param name="dest">A T-Type instance for the return value.</param>
        /// <param name="recursive">Flag to cast composite types too.</param>
        /// <returns>A T-Type instance with values from source entity.</returns>
        public static List<TResult> CollectionTo<T, TResult>(IList<T> source, TResult dest, bool recursive = true) where TResult : new()
        {
            try
            {
                List<TResult> lstResult = new List<TResult>();

                foreach (T item in source)
                    lstResult.Add(Cast.To<TResult>(item, new TResult(), recursive));

                return lstResult;
            }
            catch (Exception) { throw; }
        }
    }
}
