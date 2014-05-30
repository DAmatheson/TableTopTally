/* StringHelpers.cs
* 
* Purpose: A string extension method for creating Url slugs from strings
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Implemented
*/ 

using System.Text.RegularExpressions;

namespace TableTopTally.Helpers
{
    public static class StringHelpers
    {
        // Taken from http://predicatet.blogspot.ca/2009/04/improved-c-slug-generator-or-how-to.html
        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars          
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space  
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); // cut and trim it  
            str = Regex.Replace(str, @"\s", "-"); // hyphens  

            return str;
        }

        // Taken from http://predicatet.blogspot.ca/2009/04/improved-c-slug-generator-or-how-to.html
        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}