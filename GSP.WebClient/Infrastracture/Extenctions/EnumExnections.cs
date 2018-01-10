using System;
using System.Collections.Generic;
using System.Linq;

namespace GSP.WebClient.Infrastracture.Extenctions
{
    public static class EnumExnections
    {
        public static Dictionary<int, string> ConvertEnumValuesToDictionary<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type must be an enum");

            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(t => (int)(object)t, t => t.ToString());
        }
    }
}
