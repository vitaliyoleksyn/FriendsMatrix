using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendsMatrix
{
    public static class Extensions
    {
        public static void LogObject<T>(this T obj)
        {
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("log.txt", true))
            {
                var line = string.Format("{0}: {1}", DateTime.Now.ToShortTimeString(), JsonConvert.SerializeObject(obj, Formatting.Indented));
                file.WriteLine(line);
            }
        }
        public static void LogThis(this string str)
        {
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"C:\GitHub\log.txt", true))
            {
                var line = string.Format("{0}: {1}", DateTime.Now.ToShortTimeString(), str);
                file.WriteLine(line);
            }
        }

        public static void WriteToFile(this string line)
        {
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("result.txt", true))
            {
                file.WriteLine(line);
            }
        }

        public static void WriteToFile<T>(this IEnumerable<T> collection)
        {
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("result.txt", true))
            {
                foreach (var line in collection)
                {
                    file.WriteLine(line.ToString());
                }
            }
        }
    }
}