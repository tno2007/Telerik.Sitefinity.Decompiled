// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlShorteners.BitLyUrlShortener
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UrlShorteners
{
  /// <summary>Implements IUrlShortener via the BitLy services</summary>
  public class BitLyUrlShortener : IUrlShortener
  {
    /// <summary>User Name for authentication.</summary>
    private string userName;
    /// <summary>Api Key for authentication.</summary>
    private string apiKey;
    private string bitLyRESTSvcURL;

    /// <summary>Shortens givven long Url</summary>
    /// <param name="longUrl">The Url to be shortened</param>
    /// <returns>The shortened Url</returns>
    public string ShortenUrl(string longUrl)
    {
      string str = "";
      try
      {
        using (HttpWebResponse response = (WebRequest.Create(string.Format(this.bitLyRESTSvcURL, (object) this.userName, (object) this.apiKey, (object) longUrl)) as HttpWebRequest).GetResponse() as HttpWebResponse)
          str = new BitLyUrlShortener.ResponseElement(BitLyUrlShortener.GetStreamReader(response).ReadToEnd()).ShortUrl;
      }
      catch (Exception ex)
      {
        return longUrl;
      }
      return !string.IsNullOrEmpty(str) ? str : longUrl;
    }

    private static StreamReader GetStreamReader(HttpWebResponse response) => new StreamReader(response.GetResponseStream());

    /// <summary>Called only once to initialize the BitLyUrlShortener.</summary>
    public void Initialize()
    {
      UrlShortenerElement shortenerConfiguration = BitLyUrlShortener.GetUrlShortenerConfiguration();
      try
      {
        this.userName = shortenerConfiguration.Parameters["login"];
        this.apiKey = shortenerConfiguration.Parameters["apiKey"];
        this.bitLyRESTSvcURL = shortenerConfiguration.ShortenerServiceUrl;
      }
      catch (Exception ex)
      {
        throw new ArgumentException("Invalid arguments! Required arguments with keys \"login\" and \"apiKey\" were incorrect!");
      }
    }

    private static UrlShortenerElement GetUrlShortenerConfiguration() => Config.Get<SystemConfig>().UrlShortenerSettings.Cast<UrlShortenerElement>().Where<UrlShortenerElement>((Func<UrlShortenerElement, bool>) (element => element.Enabled)).FirstOrDefault<UrlShortenerElement>();

    /// <summary>Get/sets for Description</summary>
    public string Description { get; set; }

    /// <summary>Get/sets for Name</summary>
    public string Name { get; set; }

    /// <summary>
    /// Element to keep deselialized XML from bit.ly responce.
    /// </summary>
    private class ResponseElement
    {
      /// <summary>Status code for the request.</summary>
      public string StatusCode { get; set; }

      /// <summary>Status text for the request.</summary>
      public string StatusText { get; set; }

      /// <summary>The givven long URL.</summary>
      public string LongUrl { get; set; }

      /// <summary>The returned short URL.</summary>
      public string ShortUrl { get; set; }

      /// <summary>
      /// Designates if this is the first time this long URL was shortened.
      /// </summary>
      public string NewHash { get; set; }

      /// <summary>
      /// A bit.ly identifier for long URL which is unique to the given account.
      /// </summary>
      public string Hash { get; set; }

      /// <summary>
      /// A bit.ly identifier for long URL which can be used to track aggregate stats across all matching bit.ly links.
      /// </summary>
      public string GlobalHash { get; set; }

      /// <summary>Constructor.</summary>
      /// <param name="xmlResponse">The XML respose string from bit.ly</param>
      public ResponseElement(string xmlResponse)
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.XmlResolver = (XmlResolver) null;
        xmlDocument.LoadXml(xmlResponse);
        this.StatusCode = xmlDocument.GetElementsByTagName("status_code")[0].InnerText;
        this.StatusText = xmlDocument.GetElementsByTagName("status_txt")[0].InnerText;
        this.ShortUrl = xmlDocument.GetElementsByTagName("url")[0].InnerText;
        this.LongUrl = xmlDocument.GetElementsByTagName("long_url")[0].InnerText;
        this.NewHash = xmlDocument.GetElementsByTagName("new_hash")[0].InnerText;
        this.Hash = xmlDocument.GetElementsByTagName("hash")[0].InnerText;
        this.GlobalHash = xmlDocument.GetElementsByTagName("global_hash")[0].InnerText;
      }
    }
  }
}
