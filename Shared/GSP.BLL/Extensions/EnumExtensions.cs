using System;
using System.Collections.Generic;
using System.Linq;

namespace GSP.BLL.Extensions
{
    public static class EnumExtensions
    {
        public static Dictionary<T, string> EnumToDictionary<T>()
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(t => t, t => t.ToString());
        }
    }
}