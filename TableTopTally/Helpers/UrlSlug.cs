/* UrlSlug.cs
 * 
 * Purpose: A string extension method for creating Url slugs from strings
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Implemented
 *      Drew Matheson, 2014.08.11: Added an overload that accepts an ObjectId and appends part of it
 */

using System.Text;
using MongoDB.Bson;

namespace TableTopTally.Helpers
{
    /// <summary>
    /// String extension methods
    /// </summary>
    public static class UrlSlug
    {
        /// <summary>
        /// Produces optional, URL-friendly version of a title, "like-this-one". 
        /// 
        /// Modified from StackOverflow: http://stackoverflow.com/a/25486
        /// </summary>
        /// <param name="title">The string phrase to make a URL out of</param>
        /// <returns>The URL slug</returns>
        public static string URLFriendly(this string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return string.Empty;
            }
                
            const int MAX_LENGTH = 45;

            int length = title.Length;
            bool prevDash = false;

            var sb = new StringBuilder(length);

            char c;

            for (int i = 0; i < length; i++)
            {
                c = title[i];

                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevDash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    sb.Append(char.ToLower(c));
                    prevDash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevDash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevDash = true;
                    }
                }
                else if (c >= 128)
                {
                    int prevLength = sb.Length;

                    sb.Append(RemapInternationalCharToAscii(c));

                    if (prevLength != sb.Length)
                    {
                        prevDash = false;
                    }  
                }

                if (sb.Length >= MAX_LENGTH)
                {
                    break;
                }
                    
            }

            if (prevDash)
            {
                sb.Length -= 1;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates a safe URL by combining phase with part of id
        /// </summary>
        /// <param name="title">The string phrase to make a URL out of</param>
        /// <param name="id">The ObjectId to use as a way of making the URL essentially unique</param>
        /// <returns>The URL slug</returns>
        public static string URLFriendly(this string title, ObjectId id)
        {
            // Add portion of object ID to make it essentially unique
            return URLFriendly(title) + "-" + id.CreationTime.Ticks.ToString("X"); 
        }

        // Modified from StackOverflow: http://meta.stackexchange.com/a/7696
        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();

            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else if (c == 'æ')
            {
                return "ae";
            }
            else if (c == 'œ')
            {
                return "oe";
            }
            else
            {
                return "";
            }
        }
    }
}