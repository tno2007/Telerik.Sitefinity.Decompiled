// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Config.ClientScriptElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Config
{
  /// <summary>
  /// Configuration element that represents a client script attached to the view.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ClientScriptDescription", Title = "ClientScriptTitle")]
  public class ClientScriptElement : ConfigElement, IClientScriptExtension
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ClientScriptElement(ConfigElement parent)
      : base(parent)
    {
    }

    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptLocationDescription", Title = "ScriptLocationTitle")]
    [ConfigurationProperty("scriptLocation", IsKey = true, IsRequired = true)]
    public string ScriptLocation
    {
      get => (string) this["scriptLocation"];
      set => this["scriptLocation"] = (object) value;
    }

    [ObjectInfo(typeof (ConfigDescriptions), Description = "LoadMethodNameDescription", Title = "LoadMethodNameTitle")]
    [ConfigurationProperty("loadMethodName", IsRequired = true)]
    public string LoadMethodName
    {
      get => (string) this["loadMethodName"];
      set => this["loadMethodName"] = (object) value;
    }
  }
}
