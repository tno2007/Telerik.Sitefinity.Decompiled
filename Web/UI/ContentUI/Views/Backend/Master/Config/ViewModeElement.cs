// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.ViewModeElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>The configuration element for ViewModeDefinition</summary>
  public abstract class ViewModeElement : DefinitionConfigElement, IViewModeDefinition, IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ViewModeElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the view mode.</summary>
    /// <value></value>
    /// <remarks>
    /// This name has to be unique inside of a collection of view modes.
    /// </remarks>
    [ConfigurationProperty("Name", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
    public string Name
    {
      get => (string) this[nameof (Name)];
      set => this[nameof (Name)] = (object) value;
    }

    /// <summary>When set to true enables drag-and-drop functionality</summary>
    /// <value></value>
    [ConfigurationProperty("EnableDragAndDrop", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableDragAndDropDescription", Title = "EnableDragAndDropCaption")]
    public bool? EnableDragAndDrop
    {
      get => (bool?) this[nameof (EnableDragAndDrop)];
      set => this[nameof (EnableDragAndDrop)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to store the expansion of the tree per user.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("EnableInitialExpanding", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableInitialExpandingDescription", Title = "EnableInitialExpandingCaption")]
    public bool? EnableInitialExpanding
    {
      get => (bool?) this[nameof (EnableInitialExpanding)];
      set => this[nameof (EnableInitialExpanding)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the cookie that will contain the information of the expanded nodes.
    /// </summary>
    [ConfigurationProperty("ExpandedNodesCookieName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ExpandedNodesCookieNameDescription", Title = "ExpandedNodesCookieNameCaption")]
    public string ExpandedNodesCookieName
    {
      get => (string) this[nameof (ExpandedNodesCookieName)];
      set => this[nameof (ExpandedNodesCookieName)] = (object) value;
    }
  }
}
