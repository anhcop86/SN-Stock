using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

/// <summary>
/// Constants for Sponsored Links BlogEngine.NET WidgetControl
/// </summary>
public static class AdManagemenConstants
{
    public static string CurrentIndex = "CurrentIndex";
    public static string AdCollectionKey = "adsCollection";
    public static string DefaultAdText = "<span style='font-family: Verdana;font-size: x-small'><b>Technical Interview Patterns</b><br> Logically sound approach to technical interview... <br/><A Href='http://www.interviewpattern.com'>www.interviewpattern.com</A></span>";
    public static string DefaultAdName = "Sample Ad Provider";
    public static int    DefaultAdWeight = 1;
    public static string HideAdsForAuthZUsersKey = "hideForAdmin";
    public static string SerializedAdsKey = "ads";
    public static string WidgetSettingsKey = "widget_SponsoredLinks";
    public static string EmptyAdName = "Script #";
    public static string EmptyAdText = "Paste script here...";
    public static int    EmptyAdWeight = 1;
    public static string AuthZUserMessage = "<b style='font-size:10px;'>Ads are hidded for authenticated user</B>";
    public static string WidgetName = "Sponsored Links";


 
}

/// <summary>
/// Ad base class
/// </summary>
[Serializable()]
public class Ad
{
    public Ad()
    {
        _id = Guid.NewGuid();
        _script = string.Empty;
        _tagName = string.Empty;
        _weight = 1;
    }
    public Ad(Guid id, string content, string name, int weight)
    {
        _id = id;
        _script = content;
        _tagName = name;
        _weight = weight;
    }

    private Guid _id;
    public Guid ID
    {
        get { return _id; }
        set { _id = value; }
    }

    private string _script;
    public string Script
    {
        get { return _script; }
        set { _script = value == null ? value : value.Replace(">", "&gt;").Replace("<", "&lt;"); }
    }

    private String _tagName;
    public String TagName
    {
        get { return _tagName; }
        set { _tagName = value; }
    }

    private int _weight;
    public int RWeight
    {
        get { return _weight; }
        set { _weight = value; }
    }

}

/// <summary>
/// AdManagementBase
/// </summary>
/// 
public class AdManagementBase
{
    private ICache _cache;
    private List<Ad>  _ads;
    
    public AdManagementBase(ICache cache,List<Ad> ads)
    {
        _cache = cache;
        _ads = ads;
    }

    public Ad GetNextRandomAd()
    {
        if (_ads.Count < 1) return null;
        int seed = (int)DateTime.Now.Ticks;
        Random rnd = new Random(seed);
        int index = rnd.Next(_ads.Count);

        return _ads[index];

    }

    public Ad GetNextRandomAdWithWeightedRatio(int weight)
    {
        if (_ads.Count < 1) return null;
        int seed = (int)DateTime.Now.Ticks;
        Random rnd = new Random(seed);
        Boolean found = false;
        Ad finalCandidate = null;

        while (found == false)
        {
            finalCandidate = GetNextRandomAd();
            int toss= rnd.Next(weight);
            found = toss <= finalCandidate.RWeight;
            
        }

        return finalCandidate;

    }

    public List<Ad> GetAds()
    {
        return _ads;
    }

    public static string SerializeAds(List<Ad> ads)
    {
        if ((ads == null) || (ads.Count == 0)) return string.Empty;

        XmlSerializer serializer = new XmlSerializer(typeof(List<Ad>));
        StringWriter stream = new StringWriter();
        serializer.Serialize(stream, ads);
        return stream.ToString();
    }

    public static List<Ad> DeSerializeAds(string serializedStream)
    {
        if (string.IsNullOrEmpty(serializedStream)) return new List<Ad>();

        XmlSerializer serializer = new XmlSerializer(typeof(List<Ad>));
        return (List<Ad>)serializer.Deserialize(new StringReader(serializedStream));
    }
}

/// <summary>
/// Generic Adapter Interface for Cache Object
/// </summary>
public interface ICache
{
     void Insert<T>(string key, T obj);
     T GetObject<T>(string key);
}
