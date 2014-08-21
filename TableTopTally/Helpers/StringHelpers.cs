/* StringHelpers.cs
 * 
 * Purpose: A string extension method for creating Url slugs from strings
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Implemented
 *      Drew Matheson, 2014.08.11: Added an overload that accepts an ObjectId and appends part of it
 */ 

using System.Text.RegularExpressions;
using MongoDB.Bson;

namespace TableTopTally.Helpers
{
    /// <summary>
    /// String extension methods
    /// </summary>
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

        /// <summary>
        /// Creates a safe URL by combining phase with part of id
        /// </summary>
        /// <param name="phrase">The string phrase to make a URL out of</param>
        /// <param name="id">The ObjectId to use as a way of making the URL essentially unique</param>
        /// <returns>The string URL slug</returns>
        public static string GenerateSlug(this string phrase, ObjectId id)
        {
            string str = phrase.RemoveAccent().ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars          
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space  
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); // cut and trim it  
            str = Regex.Replace(str, @"\s", "-"); // hyphens  
            str = str + "-" + id.CreationTime.Ticks.ToString("X"); // Add portion of object ID to make it essentially unique

            return str;
        }

        // Taken from http://predicatet.blogspot.ca/2009/04/improved-c-slug-generator-or-how-to.html
        /// <summary>
        /// Replaces the characters with accent in a string with their ASCII equivalent character
        /// </summary>
        /// <param name="txt">The string to remove accents from</param>
        /// <returns>The string with accents removed</returns>
        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}