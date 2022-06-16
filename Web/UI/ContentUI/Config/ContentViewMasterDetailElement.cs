// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewMasterDetailElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Config
{
  /// <summary>
  /// The configuration element for ContentViewMasterDetailDefinition.
  /// </summary>
  public class ContentViewMasterDetailElement : 
    ContentViewDefinitionElement,
    IContentViewMasterDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewMasterDetailElement" /> class.
    /// </summary>
    /// <param name="element">The configuration element.</param>
    public ContentViewMasterDetailElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ContentViewMasterDetailDefinition((ConfigElement) this);

    /// <summary>Gets or sets the master definition.</summary>
    /// <value>The master definition.</value>
    [ConfigurationProperty("MasterDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MasterDefinitionDescription", Title = "MasterDefinitionCaption")]
    public ContentViewMasterElement MasterDefinitionConfig
    {
      get => (ContentViewMasterElement) this["MasterDefinition"];
      set => this["MasterDefinition"] = (object) value;
    }

    /// <summary>Gets or sets the detail definition.</summary>
    /// <value>The detail definition.</value>
    [ConfigurationProperty("DetailDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DetailDefinitionDescription", Title = "DetailDefinitionCaption")]
    public ContentViewDetailElement DetailDefinitionConfig
    {
      get => (ContentViewDetailElement) this["DetailDefinition"];
      set => this["DetailDefinition"] = (object) value;
    }

    /// <summary>Gets or sets the detail definition.</summary>
    /// <value>The detail definition.</value>
    public IContentViewDetailDefinition DetailDefinition => (IContentViewDetailDefinition) this.DetailDefinitionConfig;

    /// <summary>Gets or sets the detail definition.</summary>
    /// <value>The detail definition.</value>
    public IContentViewMasterDefinition MasterDefinition => (IContentViewMasterDefinition) this.MasterDefinitionConfig;

    /// <summary>
    /// Constants to hold the string keys for configuration properties of ContentViewMasterElement
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ContentViewMasterDetailProps
    {
      public const string MasterDefinition = "MasterDefinition";
      public const string DetailDefinition = "DetailDefinition";
    }
  }
}
