// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design.DynamicContentViewDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.ControlDesign.ContentView;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design
{
  /// <summary>
  /// Control designer for the <see cref="T:Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentView" /> widget.
  /// </summary>
  public class DynamicContentViewDesigner : ContentViewDesignerBase
  {
    private ModuleBuilderManager moduleBuilderMngr;

    public string DataItemTypeFullName { get; private set; }

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ContentViewDesignerBase).FullName;

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" />.
    /// </summary>
    protected virtual ModuleBuilderManager ModuleBuilderMngr
    {
      get
      {
        if (this.moduleBuilderMngr == null)
          this.moduleBuilderMngr = ModuleBuilderManager.GetManager();
        return this.moduleBuilderMngr;
      }
    }

    /// <summary>Adds the designer views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      DynamicContentView control = (DynamicContentView) this.PropertyEditor.Control;
      if (control.CanResolveDynamicContentType())
      {
        DynamicModuleType dynamicModuleType = this.ModuleBuilderMngr.GetDynamicModuleType(control.DynamicContentTypeName);
        this.ModuleBuilderMngr.LoadDynamicModuleTypeGraph(dynamicModuleType, false);
        this.PropertyEditor.TitleLiteralText = PluralsResolver.Instance.ToPlural(dynamicModuleType.DisplayName);
        this.DataItemTypeFullName = dynamicModuleType.GetFullTypeName();
        string plural1 = PluralsResolver.Instance.ToPlural(dynamicModuleType.DisplayName.ToLower());
        string lower1 = dynamicModuleType.DisplayName.ToLower();
        Labels labels = Res.Get<Labels>();
        ModuleBuilderResources builderResources = Res.Get<ModuleBuilderResources>();
        DynamicContentSelectorsDesignerView selectorsDesignerView1;
        if (dynamicModuleType.ParentModuleType != null)
        {
          selectorsDesignerView1 = (DynamicContentSelectorsDesignerView) new DynamicChildContentSelectorsDesignerView();
          DynamicChildContentSelectorsDesignerView selectorsDesignerView2 = selectorsDesignerView1 as DynamicChildContentSelectorsDesignerView;
          string lower2 = dynamicModuleType.ParentModuleType.DisplayName.ToLower();
          string plural2 = PluralsResolver.Instance.ToPlural(lower2);
          selectorsDesignerView2.ChooseAllParentsFilterText = string.Format(labels.FilterByAllParents, (object) plural2);
          selectorsDesignerView2.ChooseCurrentlyOpenParentFilterText = string.Format(labels.FilterByCurrentlyOpenParent, (object) lower2);
          selectorsDesignerView2.ChooseSelectedParentsFilterText = string.Format(labels.FilterBySelectedParents, (object) plural2);
          selectorsDesignerView2.NoParentHaveBeenSelectedText = string.Format(builderResources.NoItemsHaveBeenSelectedYet, (object) plural2);
          selectorsDesignerView2.ParentSelectorButtonText = string.Format(builderResources.SelectItems, (object) plural2);
          selectorsDesignerView2.NarrowSelectionTitleText = labels.NarrowSelection;
          selectorsDesignerView2.ParentSelectorTitle = string.Format(builderResources.SelectItems, (object) plural2);
        }
        else
          selectorsDesignerView1 = new DynamicContentSelectorsDesignerView();
        selectorsDesignerView1.ContentTitleText = string.Format(builderResources.WhichItemsToDisplay, (object) plural1);
        selectorsDesignerView1.ChooseAllText = string.Format(builderResources.AllPublishedItems, (object) plural1);
        selectorsDesignerView1.ChooseSingleText = string.Format(builderResources.OneParticularItemOnly, (object) lower1);
        selectorsDesignerView1.ChooseSimpleFilterText = string.Format(builderResources.SelectionOfItems, (object) plural1);
        selectorsDesignerView1.ChooseAdvancedFilterText = labels.AdvancedSelection;
        selectorsDesignerView1.NoContentToSelectText = string.Format(builderResources.NoItemsHaveBeenCreatedYet, (object) plural1);
        selectorsDesignerView1.SingleSelectorButtonText = string.Format(builderResources.SelectItems, (object) plural1);
        selectorsDesignerView1.SelectedContentTitleText = string.Format(builderResources.NoItemsHaveBeenSelectedYet, (object) plural1);
        selectorsDesignerView1.ContentSelectorTitle = string.Format(builderResources.SelectItems, (object) plural1);
        selectorsDesignerView1.ContentSelectorWebServiceUrl = "~/Sitefinity/Services/DynamicModules/Data.svc/";
        selectorsDesignerView1.ContentSelectorItemTypeName = control.DynamicContentTypeName;
        selectorsDesignerView1.ContentSelectorFilter = " ";
        selectorsDesignerView1.ModuleType = dynamicModuleType;
        DynamicContentListSettingsDesignerView settingsDesignerView1 = new DynamicContentListSettingsDesignerView();
        settingsDesignerView1.SortItemsText = string.Format(builderResources.SortItems, (object) plural1);
        settingsDesignerView1.DesignedMasterViewType = typeof (DynamicContentViewMaster).FullName;
        settingsDesignerView1.DynamicContentMainShortTextFieldName = dynamicModuleType.MainShortTextFieldName;
        SingleItemSettingsDesignerView settingsDesignerView2 = new SingleItemSettingsDesignerView();
        settingsDesignerView2.DesignedDetailViewType = typeof (DynamicContentViewDetail).FullName;
        views.Add(selectorsDesignerView1.ViewName, (ControlDesignerView) selectorsDesignerView1);
        views.Add(settingsDesignerView1.ViewName, (ControlDesignerView) settingsDesignerView1);
        views.Add(settingsDesignerView2.ViewName, (ControlDesignerView) settingsDesignerView2);
      }
      else
        this.HandleInvalidContentType();
    }

    private void HandleInvalidContentType()
    {
      this.Message.RemoveAfter = -1;
      this.Message.ShowNegativeMessage(Res.Get<DynamicModuleResources>().DeletedModuleWarning);
    }
  }
}
