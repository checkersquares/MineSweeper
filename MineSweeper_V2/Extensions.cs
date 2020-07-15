using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper_V2
{
    public static class Extensions
    {
        /// <summary>
        /// "IndexOf" for 2-dimensinal Arrays
        /// </summary>
        /// <typeparam name="T">Class of Objects in Array</typeparam>
        /// <param name="matrix">2-Dimensional Array</param>
        /// <param name="value">Object to look for</param>
        /// <returns>Returns Coordinates item1(X) and item2(Y) as Integers. Returns (-1/-1) if Object wasn't found in Array</returns>
        public static Tuple<int, int> CoordinatesOf<T>(this T[,] matrix, T value)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (matrix[x, y].Equals(value))
                    {
                        return Tuple.Create(y, x);
                    }
                }
            }
            return Tuple.Create(-1, -1);
        }
        /// <summary>
        /// Shuffle any List with simple Fisher-Yates shuffle
        /// </summary>
        /// <typeparam name="T">Class of objects in List</typeparam>
        /// <param name="list">List to be shuffled</param>
        public static void Shuffle<T>(this List<T> list)
        {
            Random _random = new Random();

            int total = list.Count;
            for (int i = 0; i < total; i++)
            {
                int r = i + _random.Next(total - i);
                T temp = list[r];
                list[r] = list[i];
                list[i] = temp;
            }
        }
        /// <summary>
        /// Extension for Boolean values to return a string
        /// </summary>
        /// <param name="input">bool</param>
        /// <param name="ifTrue"></param>
        /// <param name="ifFalse"></param>
        /// <returns></returns>
        public static string ConvertBoolToString(this bool input, string ifTrue, string ifFalse)
        {
            if (input)
            {
                return ifTrue;
            }
            else
            {
                return ifFalse;
            }
        }
        public static void Switch(this bool input)
        {
            if (input)
            {
                input = false;
            }
            else
            {
                input = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Description(this Enum value)
        {
            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any())
                return (attributes.First() as DescriptionAttribute).Description;

            // If no description is found, the least we can do is replace underscores with spaces
            // You can add your own custom default formatting logic here
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(ti.ToLower(value.ToString().Replace("_", " ")));
        }
        public static IEnumerable<string> GetValues(this Enum t)
        {
            return Enum.GetValues(t.GetType()).Cast<Enum>().Select((e) => e.Description()).ToList();
        }
        public static IEnumerable<KeyValuePair<object, object>> GetValuesAndDescriptions(this Enum t)
        {
            return Enum.GetValues(t.GetType()).Cast<Enum>().Select((e) => new KeyValuePair<object, object>(e, e.Description())).ToList();
        }
    }
}
