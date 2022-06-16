// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.ListViewModeElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>The configuration element for GridViewModeDefinition</summary>
  public class ListViewModeElement : 
    ViewModeElement,
    IListViewModeDefinition,
    IViewModeDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ListViewModeElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new LisViewModeDefinition((ConfigElement) this);

    /// <summary>Gets or sets the client template.</summary>
    /// <value>The client template.</value>
    [ConfigurationProperty("clientTemplate", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ClientTemplateDescription", Title = "ClientTemplateCaption")]
    public string ClientTemplate
    {
      get => (string) this["clientTemplate"];
      set => this["clientTemplate"] = (object) value;
    }
  }
}
