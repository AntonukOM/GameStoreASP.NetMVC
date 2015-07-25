using GameStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.HtmlHelpers
{
    public static class PageHelper
    {
        public static MvcHtmlString PageLinks(
            this HtmlHelper html, PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            StringBuilder res = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                res.Append(tag.ToString());
            }
            return MvcHtmlString.Create(res.ToString());
        }

    }
}