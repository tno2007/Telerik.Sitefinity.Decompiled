// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.GenericContentView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI
{
  /// <summary>
  /// Container for the public views for the GenericContent module
  /// </summary>
  [RequireScriptManager]
  public class GenericContentView : ContentView
  {
    /// <summary>
    /// Get/set the data item associated with this control in details view
    /// </summary>
    public ContentItem Item => this.DetailItem as ContentItem;

    /// <summary>
    /// Gets or sets the name of the module which initialization should be ensured prior to rendering this control.
    /// </summary>
    /// <value>The name of the module.</value>
    public override string ModuleName
    {
      get => string.IsNullOrEmpty(base.ModuleName) ? "GenericContent" : base.ModuleName;
      set => base.ModuleName = value;
    }

    /// <summary>
    /// Gets or sets the name of the configuration definition for the whole control. From this definition
    /// control can find out all other configurations needed in order to construct views.
    /// </summary>
    /// <value>The name of the control definition.</value>
    public override string ControlDefinitionName
    {
      get => string.IsNullOrEmpty(base.ControlDefinitionName) ? ContentDefinitions.FrontendDefinitionName : base.ControlDefinitionName;
      set => base.ControlDefinitionName = value;
    }

    /// <summary>
    /// Gets or sets the name of the master view to be loaded when
    /// control is in the ContentViewDisplayMode.Master
    /// </summary>
    /// <value></value>
    public override string MasterViewName
    {
      get => string.IsNullOrEmpty(base.MasterViewName) ? ContentDefinitions.FrontendMasterViewName : base.MasterViewName;
      set => base.MasterViewName = value;
    }

    /// <summary>
    /// Gets or sets the name of the detail view to be loaded when
    /// control is in the ContentViewDisplayMode.Detail
    /// </summary>
    /// <value></value>
    public override string DetailViewName
    {
      get => string.IsNullOrEmpty(base.DetailViewName) ? ContentDefinitions.FrontendDetailViewName : base.DetailViewName;
      set => base.DetailViewName = value;
    }

    /// <summary>
    /// Gets or sets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public override string EmptyLinkText => Res.Get<ContentResources>().EditGenericContentListSettings;
  }
}
