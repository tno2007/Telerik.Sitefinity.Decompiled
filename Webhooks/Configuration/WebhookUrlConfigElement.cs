// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Webhooks.Configuration.WebhookUrlConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Webhooks.Configuration
{
  internal class WebhookUrlConfigElement : ConfigElement
  {
    public WebhookUrlConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the post url for the event.</summary>
    [ConfigurationProperty("url", IsKey = true)]
    [ObjectInfo(typeof (WebhookResources), Description = "UrlDescription", Title = "UrlTitle")]
    public string Url
    {
      get => (string) this["url"];
      set => this["url"] = this.IsUrlValid(value) ? (object) value : throw new ArgumentException(string.Format(Res.Get<WebhookResources>().EventUrlErrorMessage, (object) value));
    }

    /// <summary>Gets or sets the secret for the request</summary>
    [ConfigurationProperty("secret")]
    [ObjectInfo(typeof (WebhookResources), Description = "SecretDescription", Title = "SecretTitle")]
    [SecretData]
    public string Secret
    {
      get => (string) this["secret"];
      set => this["secret"] = (object) value;
    }

    private bool IsUrlValid(string value)
    {
      bool flag = false;
      if (!string.IsNullOrEmpty(value))
      {
        try
        {
          Uri uri = new Uri(value, UriKind.RelativeOrAbsolute);
          if (!(uri.Scheme == "http"))
          {
            if (!(uri.Scheme == "https"))
              goto label_5;
          }
          flag = true;
        }
        catch (Exception ex)
        {
        }
      }
label_5:
      return flag;
    }
  }
}
