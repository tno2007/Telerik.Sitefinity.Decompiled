// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.ExternalScriptElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>Represents an external script configuration element.</summary>
  public class ExternalScriptElement : ConfigElement
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
    public ExternalScriptElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the script.</summary>
    /// <value>The name of the script.</value>
    [ConfigurationProperty("scriptName", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string ScriptName
    {
      get => (string) this["scriptName"];
      set => this["scriptName"] = (object) value;
    }

    /// <summary>Gets or sets the script path.</summary>
    /// <value>The script path.</value>
    [ConfigurationProperty("scriptPath", DefaultValue = "", IsKey = false, IsRequired = true)]
    public string ScriptPath
    {
      get => (string) this["scriptPath"];
      set => this["scriptPath"] = (object) value;
    }
  }
}
