using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace gender.Tools
{
    public static class StringExtension
    {
        /// <summary>
        /// The regex strip html.
        /// </summary>
        private static readonly Regex RegexStripHtml = new Regex("<[^>]*>", RegexOptions.Compiled);


        /// <summary>
        /// Remove all tags from html-text 
        /// </summary>
        /// <param name="source">html-text</param>
        /// <returns></returns>
        public static string StripTags(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            source = source.Replace("<br/>", Environment.NewLine);
            source = source.Replace("<br>", Environment.NewLine);
            char[] chArray = new char[source.Length];
            int index = 0;
            bool flag = false;
            for (int i = 0; i < source.Length; i++)
            {
                char ch = source[i];
                if (ch == '<')
                {
                    flag = true;
                }
                else if (ch == '>')
                {
                    flag = false;
                }
                else if (!flag)
                {
                    chArray[index] = ch;
                    index++;
                }
            }
            source = new string(chArray, 0, index);
            chArray = new char[source.Length];
            index = 0;
            flag = false;
            for (int i = 0; i < source.Length; i++)
            {
                char ch = source[i];
                if (ch == '&')
                {
                    flag = true;
                }
                else if (ch == ';')
                {
                    flag = false;
                }
                else if (!flag)
                {
                    chArray[index] = ch;
                    index++;
                }
            }
            return new string(chArray, 0, index);

        }

        /// <summary>
        /// Generate new guid-based name
        /// </summary>
        /// <returns></returns>
        public static string GenerateNewFile()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Check on valid html string (not null StripTags or start with image
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsValidHtmlString(this string source)
        {
            return 
                !string.IsNullOrWhiteSpace(source) && 
                ((!string.IsNullOrWhiteSpace(source.StripTags()) ||
                (source.IndexOf("<img ", StringComparison.InvariantCultureIgnoreCase) != -1)));
        }

       
        /// <summary>
        /// Check if is email correct string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmail(this string source)
        {
            Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);
            Match match = regex.Match(source);
            return (match.Success && match.Length == source.Length);
        }

        /// <summary>
        /// Check if is numeric string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string source)
        {
            Regex regex = new Regex(@"[0-9]+", RegexOptions.Compiled);
            Match match = regex.Match(source);
            return (match.Success && match.Length == source.Length);
        }

        /// <summary>
        /// Check if is numeric string with need length
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string str, int length)
        {
            Regex regex = new Regex(@"[0-9]+", RegexOptions.Compiled);
            Match match = regex.Match(str);
            return (match.Success && match.Length == str.Length && str.Length == length);
        }

        /// <summary>
        /// Check if is can be ICQ number (just numbers, -, and space)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsICQ(this string source)
        {
            Regex regex = new Regex(@"[0-9,\-,\x20]+", RegexOptions.Compiled);
            Match match = regex.Match(source);
            return (match.Success && match.Length == source.Length);
        }

        /// <summary>
        /// Check if is can be correct phone number
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsPhone(this string source)
        {
            Regex regex = new Regex(@"[0-9,\+,\(,\),\-,\x20]+", RegexOptions.Compiled);
            Match match = regex.Match(source);
            return (match.Success && match.Length == source.Length);
        }

        public static bool IsSkype(this string source)
        {
            Regex regex = new Regex(@"[a-zA-Z][a-zA-Z0-9\.,\-_]{5,31}", RegexOptions.Compiled);
            Match match = regex.Match(source);
            return (match.Success && match.Length == source.Length);
        }

        /// <summary>
        /// Check if is can be correct url 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsUrl(this string source)
        {
            Regex regex = new Regex(@"[a-z,A-Z,\-,_,0-9,\.,\/,?,=,%,а-я,А-Я,:,;,&]+", RegexOptions.Compiled);
            Match match = regex.Match(source);
            return (match.Success && match.Length == source.Length);
        }

        /// <summary>
        /// \n to br tags 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string NlToBr(this string source)
        {
            if (source == null)
            {
                return string.Empty;
            }
            return source.Replace(Environment.NewLine, "<br />");
        }

        /// <summary>
        /// \n to br tags 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string NlToP(this string source)
        {
            if (source == null)
            {
                return string.Empty;
            }
            return string.Format("<p>{0}</p>", source.Replace(Environment.NewLine, "</p><p>"));
        }

        /// <summary>
        /// \n to br tags 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string NlToPBr(this string source)
        {
            if (source == null)
            {
                return string.Empty;
            }
            var result = source.Replace(Environment.NewLine + Environment.NewLine, "</p><p>");
            result = result.Replace(Environment.NewLine, "<br />");
            result = "<p>" + result+ "</p>";
            result = result.Replace("</p><br />", "</p>");
            result = result.Replace("<p><br />", "<p>");
            result = result.Replace("<br /></p>", "</p>");
            result = result.Replace("<br /><p>", "<p>");
            return result;
        }

        public static string PBrToNl(this string source)
        {
            var result = source.Replace("</p><p>", Environment.NewLine);
            result = result.Replace("<br />", Environment.NewLine);
            return result;
        }

        public static string HttpToHref(this string source)
        {
            if (source == null)
            {
                return string.Empty;
            }

            var matchAhref = Regex.Matches(source, "http://(?<link>.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (matchAhref.Count > 0)
            {
                foreach (Match match in matchAhref)
                {
                    var value = match.Groups[1].Value.Trim();
                    var other = match.Groups[1].Value.Substring(value.Length);
                    source = source.Replace(match.ToString(), "<a href=\"http://" + value + "\">http://" +value + "</a>" + other);
                }
            }
            return source;
        }
        
        /// <summary>
        /// Create random password
        /// </summary>
        /// <param name="passwordLength">length of password</param>
        /// <returns></returns>
        public static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        /// <summary>
        /// To Base64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBase64(this string str)
        {
            byte[] byt = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(byt);
        }

        /// <summary>
        /// From base64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FromBase64(this string str)
        {
            byte[] b = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(b);
        }

        /// <summary>
        /// Clear content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="removeHtml"></param>
        /// <returns></returns>
        public static string CleanContent(string content, bool removeHtml)
        {
            if (removeHtml)
            {
                content = StripHtml(content);
            }

            content =
                content.Replace("\\", string.Empty).Replace("|", string.Empty).Replace("(", string.Empty).Replace(
                    ")", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty).Replace("*", string.Empty).
                    Replace("?", string.Empty).Replace("}", string.Empty).Replace("{", string.Empty).Replace(
                        "^", string.Empty).Replace("+", string.Empty);

            var words = content.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var word in
                words.Select(t => t.ToLowerInvariant().Trim()).Where(word => word.Length > 1))
            {
                sb.AppendFormat("{0} ", word);
            }

            return sb.ToString();
        }

        private static string StripHtml(string html)
        {
            return StringIsNullOrWhitespace(html) ? string.Empty : RegexStripHtml.Replace(html, string.Empty).Trim();
        }

        private static bool StringIsNullOrWhitespace(string value)
        {
            return ((value == null) || (value.Trim().Length == 0));
        }


        public static string ClearHeader(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if ((value[i] >= '0' && value[i] <= '9')
                    || (value[i] >= 'A' && value[i] <= 'z')
                    || (value[i] >= 'А' && value[i] <= 'я'))
                {
                    sb.Append(value[i]);
                }
            }
            return sb.ToString();
        }

       
        /// <summary>
        /// Make correct phone numbers to one-view base (use russian locale (8 -> 7))
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string ClearPhone(this string phone)
        {
            var str = "";
            foreach (var @char in phone)
            {
                if (@char >= '0' && @char <= '9')
                {
                    str = str + @char;
                }
            }
            if (str.StartsWith("8"))
            {
                str = "7" + str.Substring(1);
            }
            return str;
        }

        /// <summary>
        /// Make teaser (start string) from content
        /// </summary>
        /// <param name="content">string</param>
        /// <param name="length">need length</param>
        /// <param name="more">more chars</param>
        /// <returns></returns>
        public static string Teaser(this string content, int length, string more = "...")
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return string.Empty;
            }

            if (content.Length < length)
            {
                return content;
            }

            return content.Substring(0, length) + more;
        }

        public static string Domain(this string url)
        {
            var uri = new Uri(url);
            var result = uri.Authority;
            result = result.Substring(0, result.LastIndexOf(".")).ToUpper() + result.Substring(result.LastIndexOf("."));

            return result;
        }

        public static string CountWord(this int count, string first, string second, string five)
        {
            if (count % 10 == 1 && (int)(count / 10) != 1)
            {
                return first;
            }
            if (count % 10 > 1 && count % 10 < 5 && ((int)(count / 10) % 10) != 1)
            {
                return second;
            }
            return five;
        }
    }
}