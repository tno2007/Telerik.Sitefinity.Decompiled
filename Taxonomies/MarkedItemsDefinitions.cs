// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.MarkedItemsDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Taxonomies.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>
  /// Configures the backend UI for displaying marked items - items that are classified by taxa
  /// </summary>
  public static class MarkedItemsDefinitions
  {
    /// <summary>
    /// Name used int ContentViewConfig to identify the backend UI for marked items
    /// </summary>
    public static readonly string Name = nameof (MarkedItemsDefinitions);
    public static readonly string ListViewName = "MarkedItemsListView";

    /// <summary>
    /// Defines the root of the backend content view for managing marked items
    /// </summary>
    /// <param name="config">ContentView configuration to use</param>
    /// <returns></returns>
    public static ContentViewControlElement DefineContentView(
      ConfigElementDictionary<string, ContentViewControlElement> config)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module().DefineContainer((ConfigElement) config, MarkedItemsDefinitions.Name).DoNotUseWorkflow().SetManagerType(typeof (TaxonomyManager));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      viewControlElement.ViewsConfig.AddLazy((object) MarkedItemsDefinitions.ListViewName, (Func<ConfigElement>) (() => MarkedItemsDefinitions.ConfigureMarkedItemsList(fluentContentView)));
      return viewControlElement;
    }

    private static ConfigElement ConfigureMarkedItemsList(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.MarkedItemsListExtensions.js, Telerik.Sitefinity", "MarkedItemsListExtensions_ViewLoaded");
      MasterViewDefinitionFacade fluentFacade = fluentContentView.AddMasterView(MarkedItemsDefinitions.ListViewName).SetViewType(typeof (MarkedItemsMasterGridView)).SetServiceBaseUrl("~/Sitefinity/Services/Taxonomies/MarkedItems.svc/items/").SetExternalClientScripts(extenalClientScripts).DoNotBindOnClientWhenPageIsLoaded();
      MasterGridViewElement masterGridViewElement = fluentFacade.Get();
      ConfigElementList<WidgetElement> titleWidgetsConfig = masterGridViewElement.TitleWidgetsConfig;
      CommandWidgetElement element = new CommandWidgetElement((ConfigElement) masterGridViewElement.TitleWidgetsConfig);
      element.Name = "BackTo";
      element.CommandName = "backTo";
      element.ButtonType = CommandButtonType.SimpleLinkButton;
      element.WidgetType = typeof (CommandWidget);
      element.Text = "NotUsed";
      element.WrapperTagKey = HtmlTextWriterTag.Span;
      titleWidgetsConfig.Add((WidgetElement) element);
      MarkedItemsDefinitions.ConfigureToolbar(masterGridViewElement);
      MarkedItemsDefinitions.ConfigureGridMode(masterGridViewElement);
      MarkedItemsDefinitions.ConfigureDialogs(fluentFacade);
      MarkedItemsDefinitions.ConfigureLinks(masterGridViewElement);
      return (ConfigElement) masterGridViewElement;
    }

    private static void ConfigureLinks(MasterGridViewElement gridView) => gridView.LinksConfig.Add(new LinkElement((ConfigElement) gridView.LinksConfig)
    {
      Name = "BackTo",
      CommandName = "backTo",
      NavigateUrl = ""
    });

    private static void ConfigureToolbar(MasterGridViewElement gridView)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) gridView.ToolbarConfig.Sections)
      {
        Name = "Toolbar"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "GroupRemove";
      element2.ButtonType = CommandButtonType.Standard;
      element2.CommandName = "groupRemove";
      element2.Text = "Remove";
      element2.ResourceClassId = "";
      element2.CssClass = "";
      element2.WrapperTagKey = HtmlTextWriterTag.Li;
      element2.WidgetType = typeof (CommandWidget);
      element2.PermissionSet = "Taxonomies";
      element2.ActionName = "DeleteTaxonomy";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "Edit";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "edit";
      element3.Text = "Edit Taxon";
      element3.ResourceClassId = "";
      element3.CssClass = "";
      element3.WrapperTagKey = HtmlTextWriterTag.Li;
      element3.WidgetType = typeof (CommandWidget);
      element3.PermissionSet = "Taxonomies";
      element3.ActionName = "DeleteTaxonomy";
      items2.Add((WidgetElement) element3);
      gridView.ToolbarConfig.Sections.Add(element1);
    }

    private static void ConfigureGridMode(MasterGridViewElement masterGridView)
    {
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) masterGridView.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element1 = gridViewModeElement;
      masterGridView.ViewModesConfig.Add((ViewModeElement) element1);
      ConfigElementDictionary<string, ColumnElement> columnsConfig1 = element1.ColumnsConfig;
      DataColumnElement element2 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element2.Name = "Title";
      element2.HeaderText = "MarkedItemTitle";
      element2.ResourceClassId = "TaxonomyResources";
      element2.ClientTemplate = "<span>{{Title}}</span>";
      columnsConfig1.Add((ColumnElement) element2);
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element1.ColumnsConfig;
      DataColumnElement element3 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element3.Name = "Owner";
      element3.HeaderText = "Owner";
      element3.ResourceClassId = "SecurityResources";
      element3.ItemCssClass = "sfRegular";
      element3.HeaderCssClass = "sfRegular";
      element3.ClientTemplate = "<span>{{Owner || \"&ndash;\"}}</span>";
      columnsConfig2.Add((ColumnElement) element3);
      ConfigElementDictionary<string, ColumnElement> columnsConfig3 = element1.ColumnsConfig;
      DataColumnElement element4 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element4.Name = "DateCreated";
      element4.HeaderText = "Date";
      element4.ResourceClassId = "Labels";
      element4.ItemCssClass = "sfRegular";
      element4.HeaderCssClass = "sfRegular";
      element4.ClientTemplate = "<span>{{DateCreated.sitefinityLocaleFormat('dd MMM, yyyy')}}</span>";
      columnsConfig3.Add((ColumnElement) element4);
    }

    private static void ConfigureDialogs(MasterViewDefinitionFacade fluentFacade) => fluentFacade.AddEditDialog("NotUsed");
  }
}
