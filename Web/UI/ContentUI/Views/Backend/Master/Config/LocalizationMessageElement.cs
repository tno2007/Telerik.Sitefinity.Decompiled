// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.LocalizationMessageElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>Represents an localization element</summary>
  public class LocalizationMessageElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.ExternalScriptElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public LocalizationMessageElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the message key</summary>
    /// <value>Key of the message.</value>
    [ConfigurationProperty("key", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string MessageKey
    {
      get => (string) this["key"];
      set => this["key"] = (object) value;
    }

    /// <summary>Gets or sets the message translation</summary>
    /// <value>The message translation.</value>
    [ConfigurationProperty("translation", DefaultValue = "", IsKey = false, IsRequired = true)]
    public string Translation
    {
      get => (string) this["translation"];
      set => this["translation"] = (object) value;
    }
  }
}
