using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RedisPlay.Lib.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Uses reflection to get the value of the specified property
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetPropertyValue<I, T>(this I obj, string propertyName)
        {
            Type type = typeof(I);
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null)
                return default;

            var result = (T)propertyInfo.GetValue(obj);
            return result;
        }
    }
}
