// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Basic.UrlShorteningSettingsModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Web.UrlShorteners;

namespace Telerik.Sitefinity.Configuration.Basic
{
  /// <summary>Represents the model for basic twitter settings.</summary>
  [DataContract]
  public class UrlShorteningSettingsModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Basic.UrlShorteningSettingsModel" /> class.
    /// </summary>
    public UrlShorteningSettingsModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Basic.UrlShorteningSettingsModel" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public UrlShorteningSettingsModel(UrlShortenerElement element)
    {
      this.ShortenningServiceUrl = element.ShortenerServiceUrl;
      this.AccountName = element.Parameters["login"];
      this.ApiKey = element.Parameters["apiKey"];
      this.ProviderName = element.Parameters["providerName"];
    }

    public UrlShorteningSettingsModel(UrlShorteningSettingsModel provider)
    {
      this.ShortenningServiceUrl = provider.ShortenningServiceUrl;
      this.AccountName = provider.AccountName;
      this.ApiKey = provider.ApiKey;
      this.ProviderName = provider.ProviderName;
    }

    /// <summary>Gets or sets the shortenning service URL.</summary>
    /// <value>The shortenning service URL.</value>
    [DataMember]
    public string ShortenningServiceUrl { get; set; }

    /// <summary>Gets or sets the name of the account.</summary>
    /// <value>The name of the account.</value>
    [DataMember]
    public string AccountName { get; set; }

    /// <summary>
    /// Gets or sets the name of the shortening service provider.
    /// </summary>
    /// <value>The name of the shortening service provider.</value>
    [DataMember]
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the API key.</summary>
    /// <value>The API key.</value>
    [DataMember]
    public string ApiKey { get; set; }
  }
}
