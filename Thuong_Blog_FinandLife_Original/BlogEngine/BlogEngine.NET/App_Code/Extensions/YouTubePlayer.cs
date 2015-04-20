using System;
using BlogEngine.Core;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using BlogEngine.Core.Web.Controls;
using System.IO;
using BlogEngine.Core.Web.Extensions;


/// <summary> 
/// Adds a YouTube video player to the post.  I modified Sean Blakemore's Silverlight Extension for this project.  Find his code here: http://www.flawlesscode.com 
/// </summary> 
[Extension("Adds a YouTube video player to the post.", "1.1", "<a href=\"http://al.bsharah.com/\">Al Bsharah</a>")]
public class YouTubePlayer
{
    private static ExtensionSettings settings;

    /// <summary> 
    /// Adds a YouTube video player to the post and page. 
    /// </summary> 
    public YouTubePlayer()
    {
        Post.Serving += Post_Serving;
        BlogEngine.Core.Page.Serving += Page_Serving;

        ExtensionSettings initialSettings = new ExtensionSettings(GetType().Name);
        initialSettings.Help = "For Border, 0=No 1=Yes.<br>Border colors must be entered in Hexidecimal format (i.e. FF3300)";
        initialSettings.AddParameter("width", "Width");
        initialSettings.AddValue("width", "425");
        initialSettings.AddParameter("height", "Height");
        initialSettings.AddValue("height", "355");
        initialSettings.AddParameter("border", "Show Border");
        initialSettings.AddValue("border", "0");
        initialSettings.AddParameter("color1", "Primary Color");
        initialSettings.AddValue("color1", "000000");
        initialSettings.AddParameter("color2", "Secondary Color");
        initialSettings.AddValue("color2", "000000");
        initialSettings.IsScalar = true;
        ExtensionManager.ImportSettings(initialSettings);
        settings = ExtensionManager.GetSettings(GetType().Name);
    }

    private void Post_Serving(object sender, ServingEventArgs e)
    {
        if (e.Location != ServingLocation.PostList && e.Location != ServingLocation.SinglePost)
            return;

        string regex__1 = @"\[youtube:.*?\]";
        MatchCollection matches = Regex.Matches(e.Body, regex__1);

        if (matches.Count == 0)
            return;

        Int32 width = default(Int32);
        Int32 height = default(Int32);
        Int32 border = default(Int32);
        String color1 = default(String);
        String color2 = default(String);
        String BaseURL = default(String);

        try
        {
            width = Int32.Parse(settings.GetSingleValue("width"));
            height = Int32.Parse(settings.GetSingleValue("height"));
            border = Int32.Parse(settings.GetSingleValue("border"));
            color1 = settings.GetSingleValue("color1");
            color2 = settings.GetSingleValue("color2");
            BaseURL = "http://www.youtube.com/v/";
        }
        catch
        { return; } 

        for (int i = 0; i < matches.Count; i++)
        {
            Int32 length = "[youtube:".Length;
            string mediaFile = matches[i].Value.Substring(length, matches[i].Value.Length - length - 1);

            string player = @"<div id=""YouTubePlayer_{0}"" style=""width:{2}px; height:{3}px;"">
                                 <object width=""{2}"" height=""{3}"">
                                 <param name=""movie"" value=""{7}{1}&fs=1&border={4}&color1=0x{5}&color2=0x{6}""></param>
                                 <param name=""allowFullScreen"" value=""true""></param>
                                 <embed src=""{7}{1}&fs=1&border={4}&color1=0x{5}&color2=0x{6}""
                                 type = ""application/x-shockwave-flash""
                                 width=""{2}"" height=""{3}"" allowfullscreen=""true""></embed>
                                 </object>
                             </div>";

            e.Body = e.Body.Replace(matches[i].Value, String.Format(player, i, mediaFile, width, height, border, color1, color2, BaseURL));
           
        }
    }

    private void Page_Serving(object sender, ServingEventArgs e)
    {
        if (e.Location != ServingLocation.SinglePage)
            return;

        string regex__1 = @"\[youtube:.*?\]";
        MatchCollection matches = Regex.Matches(e.Body, regex__1);

        if (matches.Count == 0)
            return;

        Int32 width = default(Int32);
        Int32 height = default(Int32);
        Int32 border = default(Int32);
        String color1 = default(String);
        String color2 = default(String);
        String BaseURL = default(String);

        try
        {
            width = Int32.Parse(settings.GetSingleValue("width"));
            height = Int32.Parse(settings.GetSingleValue("height"));
            border = Int32.Parse(settings.GetSingleValue("border"));
            color1 = settings.GetSingleValue("color1");
            color2 = settings.GetSingleValue("color2");
            BaseURL = "http://www.youtube.com/v/";
        }
        catch
        { return; }

        for (int i = 0; i < matches.Count; i++)
        {
            Int32 length = "[youtube:".Length;
            string mediaFile = matches[i].Value.Substring(length, matches[i].Value.Length - length - 1);

            string player = @"<div id=""YouTubePlayer_{0}"" style=""width:{2}px; height:{3}px;"">
                                 <object width=""{2}"" height=""{3}"">
                                 <param name=""movie"" value=""{7}{1}&fs=1&border={4}&color1=0x{5}&color2=0x{6}""></param>
                                 <param name=""allowFullScreen"" value=""true""></param>
                                 <embed src=""{7}{1}&fs=1&border={4}&color1=0x{5}&color2=0x{6}""
                                 type = ""application/x-shockwave-flash""
                                 width=""{2}"" height=""{3}"" allowfullscreen=""true""></embed>
                                 </object>
                             </div>";

            e.Body = e.Body.Replace(matches[i].Value, String.Format(player, i, mediaFile, width, height, border, color1, color2, BaseURL));

        }
    }

}