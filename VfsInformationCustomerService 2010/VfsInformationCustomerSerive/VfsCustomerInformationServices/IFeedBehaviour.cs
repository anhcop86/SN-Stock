using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace VfsCustomerInformationServices
{
    public interface IFeedBehaviour
    {
        List<HtmlNode> ParseTableCell();
        List<HtmlNode> ParseSpan(HtmlNode node);
        HtmlNode ParseLink(HtmlNode node);
        HtmlNode ParseNewsDate(HtmlNode node);
        HtmlNode ParseContent();
        HtmlNode ParseNewsDescription();
    }
}
