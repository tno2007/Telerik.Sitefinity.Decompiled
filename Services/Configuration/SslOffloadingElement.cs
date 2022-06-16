// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SslOffloadingElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services
{
  /// <summary>SSL Offloading settings for Sitefinity</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "SSLOffloadingConfigDescription", Title = "SSLOffloadingConfigCaption")]
  public class SslOffloadingElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.SslOffloadingElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public SslOffloadingElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether to turn the SSL Offloading on or off.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "enableSslOffloading")]
    [ConfigurationProperty("EnableSslOffloading", DefaultValue = false)]
    public bool EnableSslOffloading
    {
      get => this[nameof (EnableSslOffloading)] as bool? ?? false;
      set => this[nameof (EnableSslOffloading)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the HTTP Header Field Name used for SSL Acceleration.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "HttpHeaderFieldName")]
    [ConfigurationProperty("HttpHeaderFieldName", DefaultValue = "X-Forwarded-Proto")]
    public string HttpHeaderFieldName
    {
      get => this[nameof (HttpHeaderFieldName)] is string str ? str : (string) null;
      set => this[nameof (HttpHeaderFieldName)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the HTTP Header Field Name Value used for SSL Acceleration.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "HttpHeaderFieldValue")]
    [ConfigurationProperty("HttpHeaderFieldValue", DefaultValue = "https")]
    public string HttpHeaderFieldValue
    {
      get => this[nameof (HttpHeaderFieldValue)] is string str ? str : (string) null;
      set => this[nameof (HttpHeaderFieldValue)] = (object) value;
    }

    /// <summary>
    /// Constants for the names of the configuration properties
    /// </summary>
    internal static class Names
    {
      internal const string EnableSslOffloading = "EnableSslOffloading";
      internal const string HttpHeaderFieldName = "HttpHeaderFieldName";
      internal const string HttpHeaderFieldValue = "HttpHeaderFieldValue";
    }
  }
}
