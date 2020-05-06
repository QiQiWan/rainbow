using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace rainbow
{
    class Program
    {
        static void Main(string[] args)
        {
            string origin = FileHelper.ReadFile(@"resource/reading.yml");
            string pattern = "\\-\\s\\{[\\S\\s]*?(?=\\})";
            List<string> matchList = MatchObj(pattern, origin);

            Console.WriteLine(origin);

            foreach (string item in  matchList)
                Console.WriteLine(item);
        }
        static List<string> MatchObj(string pattern, string origin)
        {
            List<string> matchList = new List<string>();

            foreach (Match item in Regex.Matches(origin, pattern))
            {
                matchList.Add(item.Value);
            }
            return matchList;
        }
    }
}
