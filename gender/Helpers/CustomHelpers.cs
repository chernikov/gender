using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Text;
using gender.Global;

namespace gender.Helpers
{
    public static class CustomHelpers
    {

        public static MvcHtmlString Nl2Br(this HtmlHelper html, string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new MvcHtmlString(string.Empty);
            }
            return new MvcHtmlString(input.Replace("\r\n", "<br />\r\n"));
        }

        public static MvcHtmlString PageLinks(this HtmlHelper html, int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            var builder = new StringBuilder();
            for (int i = 1; i <= totalPages; i++)
            {
                if (((i <= 3) || (i > (totalPages - 3))) || ((i > (currentPage - 2)) && (i < (currentPage + 2))))
                {
                    var subBuilder = new TagBuilder("a");
                    subBuilder.MergeAttribute("href", pageUrl.Invoke(i));
                    subBuilder.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                    if (i == currentPage)
                    {
                        subBuilder.AddCssClass("selected");
                    }
                    builder.AppendLine(subBuilder.ToString());
                }
                else if ((i == 4) && (currentPage > 5))
                {
                    builder.AppendLine(" ... ");
                }
                else if ((i == (totalPages - 3)) && (currentPage < (totalPages - 4)))
                {
                    builder.AppendLine(" ... ");
                }
            }
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString PageLinksBootstrap(this HtmlHelper html, int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i <= totalPages; i++)
            {
                if (((i <= 3) || (i > (totalPages - 3))) || ((i > (currentPage - 2)) && (i < (currentPage + 2))))
                {
                    var subBuilder = new TagBuilder("a");
                    subBuilder.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                    if (i == currentPage)
                    {
                        subBuilder.MergeAttribute("href", "#");
                        builder.AppendLine("<li class=\"active\">" + subBuilder.ToString() + "</li>");
                    }
                    else
                    {
                        subBuilder.MergeAttribute("href", pageUrl.Invoke(i));
                        builder.AppendLine("<li>" + subBuilder.ToString() + "</li>");
                    }
                    
                }
                else if ((i == 4) && (currentPage > 5))
                {
                    builder.AppendLine("<li class=\"disabled\"> <a href=\"#\">...</a> </li>");
                }
                else if ((i == (totalPages - 3)) && (currentPage < (totalPages - 4)))
                {
                    builder.AppendLine("<li class=\"disabled\"> <a href=\"#\">...</a> </li>");
                }
            }
            return new MvcHtmlString("<ul>" + builder.ToString() + "</ul>");
        }

        public static MvcHtmlString PageLinksMessage(this HtmlHelper html, int currentPage, int totalPages, int itemPerPage, Func<int, string> pageUrl)
        {
            var builder = new StringBuilder();
            for (int i = 1; i <= totalPages; i++)
            {
                if (((i <= 3) || (i > (totalPages - 3))) || ((i > (currentPage - 2)) && (i < (currentPage + 2))))
                {
                    var subBuilder = new TagBuilder("a");
                    subBuilder.MergeAttribute("href", pageUrl.Invoke(i));
                    subBuilder.InnerHtml = string.Format("{0}-{1}", (i - 1) * itemPerPage + 1, i * itemPerPage);
                    if (i == currentPage)
                    {
                        subBuilder.AddCssClass("selected");
                    }
                    builder.AppendLine(subBuilder.ToString());
                }
                else if ((i == 4) && (currentPage > 5))
                {
                    builder.AppendLine(" ... ");
                }
                else if ((i == (totalPages - 3)) && (currentPage < (totalPages - 4)))
                {
                    builder.AppendLine(" ... ");
                }
            }
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString PageLinkLancer(this HtmlHelper html, int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            var sb = new StringBuilder();

            // Previous
            if (currentPage > 1)
            {
                var subBuilder = new TagBuilder("a");
                subBuilder.MergeAttribute("href", pageUrl.Invoke(1));
                subBuilder.InnerHtml = "Предыдущая";
                sb.AppendFormat("<div class=\"prev\">← {0}</div>", subBuilder);
            }
            else
            {
                sb.Append("<div class=\"empty-prev\">&nbsp;</div>");
            }
            sb.Append("<div class=\"current\">");
            for (int i = 1; i <= totalPages; i++)
            {
                if (((i <= 3) || (i > (totalPages - 3))) || ((i > (currentPage - 2)) && (i < (currentPage + 2))))
                {

                    if (i == currentPage)
                    {
                        var subBuilder = new TagBuilder("span");
                        subBuilder.MergeAttribute("style", "font-weight: bold;");
                        subBuilder.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                        sb.AppendLine(subBuilder.ToString());
                    }
                    else
                    {
                        var subBuilder = new TagBuilder("a");
                        subBuilder.MergeAttribute("href", pageUrl.Invoke(i));
                        subBuilder.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                        sb.AppendLine(subBuilder.ToString());
                    }
                }
                else if ((i == 4) && (currentPage > 5))
                {
                    sb.AppendLine(" ... ");
                }
                else if ((i == (totalPages - 3)) && (currentPage < (totalPages - 4)))
                {
                    sb.AppendLine(" ... ");
                }
            }
            sb.Append("</div>");
            // Next
            if (currentPage < totalPages)
            {
                var subBuilder = new TagBuilder("a");
                subBuilder.MergeAttribute("href", pageUrl.Invoke(totalPages));
                subBuilder.InnerHtml = "Следующая";
                sb.AppendFormat("<div class=\"next\">{0} →</div>", subBuilder);
            }
            else
            {
                sb.Append("<div class=\"empty-next\">&nbsp;</div>");
            }

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString PageLinksLogoden(this HtmlHelper html, int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            var builder = new StringBuilder();

            var subLeftArrowBuilderA = new TagBuilder("a");
            if (currentPage - 1 > 0)
            {
                subLeftArrowBuilderA.MergeAttribute("href", pageUrl.Invoke(currentPage - 1));
                subLeftArrowBuilderA.AddCssClass("left-arrow");
            }
            else
            {
                subLeftArrowBuilderA.AddCssClass("left-arrow-d");
            }
            builder.AppendLine(subLeftArrowBuilderA.ToString());
            for (int i = 1; i <= totalPages; i++)
            {
                if (((i <= 3) || (i > (totalPages - 3))) || ((i > (currentPage - 2)) && (i < (currentPage + 2))))
                {
                    var subBuilder = new TagBuilder("a")
                    {
                        InnerHtml = i.ToString(CultureInfo.InvariantCulture)
                    };
                    subBuilder.MergeAttribute("href", pageUrl.Invoke(i));
                    if (i == currentPage)
                    {
                        subBuilder.AddCssClass("selected");
                    }
                    builder.AppendLine(subBuilder.ToString());
                }
                else if ((i == 4) && (currentPage > 5))
                {
                    builder.AppendLine(" ... ");
                }
                else if ((i == (totalPages - 3)) && (currentPage < (totalPages - 4)))
                {
                    builder.AppendLine(" ... ");
                }
            }
            var subRightArrowBuilderA = new TagBuilder("a");
            if (currentPage < totalPages)
            {
                subRightArrowBuilderA.MergeAttribute("href", pageUrl.Invoke(currentPage + 1));
                subRightArrowBuilderA.AddCssClass("right-arrow");
            }
            else
            {
                subRightArrowBuilderA.AddCssClass("right-arrow-d");
            }
            builder.AppendLine(subRightArrowBuilderA.ToString());

            return new MvcHtmlString(builder.ToString());
        }

    }
}