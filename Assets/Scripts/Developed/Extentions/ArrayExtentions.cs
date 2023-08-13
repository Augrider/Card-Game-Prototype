using System;
using System.Linq;
using System.Collections.Generic;


namespace Developed.Extentions
{
    /// <summary>
    /// Various functions for arrays and dictionaries
    /// </summary>
    public static class ArrayExtentions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> elements)
        {
            var random = new Random();
            return new List<T>(elements.OrderBy(x => random.Next(elements.Count())));
        }

        public static Stack<T> Shuffle<T>(this Stack<T> elements)
        {
            var random = new Random();
            return new Stack<T>(elements.OrderBy(x => random.Next(elements.Count())));
        }


        /// <summary>
        /// Get string that contains all items of array
        /// </summary>
        /// <param name="array">Provided array</param>
        /// <returns>Formatted string of array content</returns>
        public static string AllContent<T>(this IEnumerable<T> array)
        {
            string s = string.Format("{0} items: ", array.Count());
            s += string.Join(", ", array);
            return s;
        }
    }
}