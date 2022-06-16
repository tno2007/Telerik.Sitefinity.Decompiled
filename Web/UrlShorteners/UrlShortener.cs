// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlShorteners.UrlShortener
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UrlShorteners
{
  /// <summary>
  /// Serves like a manager for <see cref="T:Telerik.Sitefinity.Web.UrlShorteners.IUrlShortener" />
  /// </summary>
  public class UrlShortener : IUrlShortener
  {
    /// <summary>Regex to use for searching Url-s.</summary>
    private const string UrlRegex = "(http|https|ftp)\\://[a-zA-Z0-9\\-\\.]+\\.[a-zA-Z]{2,3,4}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\\-\\._\\?\\,\\'/\\\\\\+&amp;%\\$#\\=~])*[^\\.\\,\\)\\(\\s]";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UrlShorteners.UrlShortener" /> class.
    /// </summary>
    private UrlShortener()
    {
    }

    /// <summary>Gets/sets</summary>
    protected internal virtual IUrlShortener Provider { get; set; }

    /// <summary>
    /// Get the default instance of <see cref="T:Telerik.Sitefinity.Web.UrlShorteners.UrlShortener" />
    /// </summary>
    /// <returns>An instance of the shortener provider</returns>
    public static UrlShortener Get()
    {
      UrlShortener defaultInstance = UrlShortener.GetDefaultInstance();
      UrlShortener.ConfigureShortener(defaultInstance);
      return defaultInstance;
    }

    internal static void ConfigureShortener(UrlShortener instance)
    {
      IUrlShortener instance1 = (IUrlShortener) Activator.CreateInstance(UrlShortener.GetUrlShortenerConfiguration().ProviderType);
      instance.Provider = instance1;
      instance.Provider.Initialize();
    }

    internal static UrlShortenerElement GetUrlShortenerConfiguration() => Config.Get<SystemConfig>().UrlShortenerSettings.Cast<UrlShortenerElement>().Where<UrlShortenerElement>((Func<UrlShortenerElement, bool>) (element => element.Enabled)).FirstOrDefault<UrlShortenerElement>();

    private static UrlShortener GetDefaultInstance() => new UrlShortener();

    /// <summary>
    /// Get an instance of <see cref="T:Telerik.Sitefinity.Web.UrlShorteners.UrlShortener" /> with the specified <paramref name="name" />
    /// </summary>
    /// <param name="name">Name that the provider was registerd with.</param>
    /// <returns>An instance of the shortener provider</returns>
    public static UrlShortener Get(string name)
    {
      UrlShortener defaultInstance = UrlShortener.GetDefaultInstance();
      defaultInstance.Provider = (IUrlShortener) Activator.CreateInstance(Config.Get<SystemConfig>().UrlShortenerSettings[name].ProviderType);
      defaultInstance.Provider.Initialize();
      return defaultInstance;
    }

    /// <summary>Shortens givven long Url</summary>
    /// <param name="longUrl">The Url to be shortened</param>
    /// <returns>The shortened Url</returns>
    public string ShortenUrl(string longUrl) => this.Provider.ShortenUrl(longUrl);

    /// <summary>Called only once to initialize the UrlShortener.</summary>
    public void Initialize() => this.Provider.Initialize();

    /// <summary>Shortens givven long Url-s in given text</summary>
    /// <param name="text">The text with Url-s to be shortened</param>
    /// <returns>The text with shortened Url-s</returns>
    public string ShortenAllUrls(string text)
    {
      foreach (Match match in UrlShortener.GetDefaultRegex().Matches(text))
        text = text.Replace(match.Value, this.ShortenUrl(match.Value));
      return text;
    }

    private static Regex GetDefaultRegex() => new Regex("(http|https|ftp)\\://[a-zA-Z0-9\\-\\.]+\\.[a-zA-Z]{2,3,4}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\\-\\._\\?\\,\\'/\\\\\\+&amp;%\\$#\\=~])*[^\\.\\,\\)\\(\\s]", RegexOptions.IgnoreCase);

    /// <summary>Get/sets for Description</summary>
    public string Description { get; set; }

    /// <summary>Get/sets for Name</summary>
    public string Name { get; set; }
  }
}
