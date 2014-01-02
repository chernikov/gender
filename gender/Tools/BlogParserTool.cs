using gender.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace gender.Tools
{
    public class BlogParserTool
    {

        public static string RegexLJContent = "b-singlepost-body[\",']>(?<content>.*?)</article>";
        public static string RegexLJTitle = "\"b-singlepost-title\">(?<content>.*?)</h1>";
        public static string RegexLJDate = "\"b-singlepost-author-date\">(?<content>.*?)</span>";

        public static string RegexWPContent = "class=\"entry-content\".*?>(?<content>.*?)</?div";
        public static string RegexWPTitle = "class=\"entry-title\".*?>(?<content>.*?)</h";
        public static string RegexWPDateOld = "\"entry-date\">(?<content>.*?)</span>";
        public static string RegexWPDateNew = "\"datePublished\">(?<content>.*?)</time>"; 

        public static string RegexRssLink = "type=\"application/rss\\+xml\".*?title=\"RSS\".*?href=\"(?<content>.*?)\".*?/>";

        public string LastError;

        public ParseBlogPostView ParsePost(string url)
        {
            
            var webClient = new WebClient();
            try
            {
                var data = webClient.DownloadData(url);
                var str = Encoding.UTF8.GetString(data);
                if (url.Contains("livejournal"))
                {
                    return ParseLJ(str);
                }
                //wordpress

                return ParseWP(str);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    LastError = "Материал ({0}) не добавлен. Ошибка: недействительный адрес.";
                }
                else
                {
                    LastError = "Материал ({0}) не добавлен. Ошибка: запись в блоге не распознана. Ошибка: сервер недоступен. Попробуйте повторить попытку позже.";
                }
            }
            catch (Exception ex)
            {
                LastError = "Материал ({0}) не добавлен. Ошибка: запись в блоге не распознана. Проверьте, не является ли она приватной.";
            }
            return null;
        }


        private ParseBlogPostView ParseLJ(string str)
        {
            var contentMatch = Regex.Matches(str, RegexLJContent, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var titleMatch = Regex.Matches(str, RegexLJTitle, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var dateMatch = Regex.Matches(str, RegexLJDate, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (contentMatch.Count > 0 && titleMatch.Count > 0 && dateMatch.Count > 0)
            {
                var content = contentMatch[0].Groups["content"].Value;
                var title = titleMatch[0].Groups["content"].Value;
                var date = dateMatch[0].Groups["content"].Value;
                date = date.StripTags();

                DateTime realDateTime;

                if (DateTime.TryParse(date, out realDateTime))
                {
                    var blogPostView = new ParseBlogPostView()
                    {
                        Header = title.Trim(),
                        Content = content.Trim(),
                        AddedDate = realDateTime
                    };
                    return blogPostView;
                }
                else
                {
                    var blogPostView = new ParseBlogPostView()
                    {
                        Header = title.Trim(),
                        Content = content.Trim(),
                        AddedDate = DateTime.Now
                    };
                    return blogPostView;
                }
            }
            else
            {
                LastError = "Материал ({0}) не добавлен. Ошибка: запись в блоге не распознана. Проверьте, не является ли она приватной.";
            }
            return null;
        }

        private ParseBlogPostView ParseWP(string str)
        {
            var contentMatch = Regex.Matches(str, RegexWPContent, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var titleMatch = Regex.Matches(str, RegexWPTitle, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var dateMatch = Regex.Matches(str, RegexWPDateOld, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (dateMatch.Count == 0)
            {
                dateMatch = Regex.Matches(str, RegexWPDateNew, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            if (contentMatch.Count > 0 && titleMatch.Count > 0 && dateMatch.Count > 0)
            {
                var content = contentMatch[0].Groups["content"].Value;
                var title = titleMatch[0].Groups["content"].Value;
                var date = dateMatch[0].Groups["content"].Value;
                date = date.StripTags();

                title = HttpUtility.HtmlDecode(title.StripTags());

                DateTime realDateTime;

                if (DateTime.TryParse(date, out realDateTime))
                {
                    var blogPostView = new ParseBlogPostView()
                    {
                        Header = title.Trim(),
                        Content = content.Trim(),
                        AddedDate = realDateTime
                    };
                    return blogPostView;
                }
                else
                {
                    var blogPostView = new ParseBlogPostView()
                    {
                        Header = title.Trim(),
                        Content = content.Trim(),
                        AddedDate = DateTime.Now
                    };
                    return blogPostView;
                }
            }
            else
            {
                LastError = "Материал ({0}) не добавлен. Ошибка: запись в блоге не распознана. Проверьте, не является ли она приватной.";
            }
            return null;
        }

        public string FindRss(string url)
        {
            var webClient = new WebClient();
            try
            {
                var data = webClient.DownloadData(url);
                var str = Encoding.UTF8.GetString(data);
                var match = Regex.Matches(str, RegexRssLink, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                if (match.Count > 0)
                {
                    var content = match[0].Groups["content"].Value;
                    return content;
                }
            }
            catch (Exception ex)
            {
                LastError = "RSS-канал не найден";
            }
            return null;
        }

    }
}
