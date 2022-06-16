// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators.SidebarGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators
{
  /// <summary>
  /// This class generates the toolbar definition for the given dynamic module type.
  /// </summary>
  internal class SidebarGenerator
  {
    private const string GoBackToContentTypesCommandName = "goBackToContentTypes";
    internal const string SettingsWidgetBarSectionName = "Settings";
    internal const string ManageContentLocationsCommandWidgetElementName = "ManageContentLocations";
    internal const string PermissionsCommandWidgetElementName = "Permissions";

    /// <summary>
    /// Generates the manage also sidebar section element for the given dynamic module type and returns it.
    /// </summary>
    /// <param name="moduleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the toolbar ought to be
    /// generated.
    /// </param>
    /// <param name="parentElement">
    /// The parent element of the toolbar element.
    /// </param>
    /// <remarks>
    /// The sidebar element will not be added to the parent element.
    /// </remarks>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetBarSectionElement" /> representing the manage also sidebar section.
    /// </returns>
    public static WidgetBarSectionElement GenerateManageAlso(
      DynamicModuleType moduleType,
      ConfigElement parentElement)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      return parentElement != null ? new WidgetBarSectionElement(parentElement)
      {
        Name = "ManageAlso",
        Title = "ManageAlso",
        ResourceClassId = typeof (ModuleBuilderResources).Name,
        CssClass = "sfWidgetsList sfSeparator"
      } : throw new ArgumentNullException(nameof (parentElement));
    }

    /// <summary>
    /// Generates the filter section element for the given dynamic module type and returns it.
    /// </summary>
    /// <param name="moduleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the filter section ought to
    /// be generated.
    /// </param>
    /// <param name="parentElement">
    /// The sidebar element to which the section ought to be added.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetBarSectionElement" /> representing the filter sidebar section.
    /// </returns>
    public static WidgetBarSectionElement GenerateFilterSection(
      IDynamicModuleType moduleType,
      ConfigElement parentElement)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      if (parentElement == null)
        throw new ArgumentNullException(nameof (parentElement));
      WidgetBarSectionElement filterSection = new WidgetBarSectionElement(parentElement)
      {
        Name = "Filter",
        Title = string.Format(SidebarGenerator.GetResForFilterSectionTitle(), (object) moduleType.DisplayName),
        CssClass = "sfFirst sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "filterSection"
      };
      ConfigElementList<WidgetElement> items1 = filterSection.Items;
      CommandWidgetElement element1 = new CommandWidgetElement((ConfigElement) filterSection.Items);
      element1.Name = "AllItems";
      element1.CommandName = "showAllItems";
      element1.ButtonType = CommandButtonType.SimpleLinkButton;
      element1.Text = string.Format(SidebarGenerator.GetResForAllItemsFormated(), (object) PluralsResolver.Instance.ToPlural(moduleType.DisplayName));
      element1.WidgetType = typeof (CommandWidget);
      element1.IsSeparator = false;
      element1.ButtonCssClass = "sfSel";
      items1.Add((WidgetElement) element1);
      ConfigElementList<WidgetElement> items2 = filterSection.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) filterSection.Items);
      element2.Name = "MyItems";
      element2.CommandName = "showMyItems";
      element2.ButtonType = CommandButtonType.SimpleLinkButton;
      element2.Text = string.Format(SidebarGenerator.GetResForMyItemsFormated(), (object) moduleType.DisplayName);
      element2.CssClass = "";
      element2.WidgetType = typeof (CommandWidget);
      element2.IsSeparator = false;
      items2.Add((WidgetElement) element2);
      SidebarGenerator.CreateStatusFilterSection(moduleType, filterSection);
      ConfigElementList<WidgetElement> items3 = filterSection.Items;
      LiteralWidgetElement element3 = new LiteralWidgetElement((ConfigElement) filterSection.Items);
      element3.Name = "Separator";
      element3.WrapperTagKey = HtmlTextWriterTag.Li;
      element3.WidgetType = typeof (LiteralWidget);
      element3.CssClass = "sfSeparator";
      element3.Text = "&nbsp;";
      element3.IsSeparator = true;
      items3.Add((WidgetElement) element3);
      return filterSection;
    }

    private static void CreateStatusFilterSection(
      IDynamicModuleType moduleType,
      WidgetBarSectionElement filterSection)
    {
      ConfigElementList<WidgetElement> items1 = filterSection.Items;
      CommandWidgetElement element1 = new CommandWidgetElement((ConfigElement) filterSection.Items);
      element1.Name = string.Format("Draft{0}", (object) moduleType.DisplayName);
      element1.CommandName = "showMasterItems";
      element1.ButtonType = CommandButtonType.SimpleLinkButton;
      element1.Text = "Draft";
      element1.ResourceClassId = typeof (ModuleBuilderResources).Name;
      element1.CssClass = "";
      element1.WidgetType = typeof (CommandWidget);
      element1.IsSeparator = false;
      items1.Add((WidgetElement) element1);
      ConfigElementList<WidgetElement> items2 = filterSection.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) filterSection.Items);
      element2.Name = string.Format("Published{0}", (object) moduleType.DisplayName);
      element2.CommandName = "showPublishedItems";
      element2.ButtonType = CommandButtonType.SimpleLinkButton;
      element2.Text = "Published";
      element2.ResourceClassId = typeof (ModuleBuilderResources).Name;
      element2.CssClass = "";
      element2.WidgetType = typeof (CommandWidget);
      element2.IsSeparator = false;
      items2.Add((WidgetElement) element2);
      DefinitionsHelper.AppendStatusFilters((ConfigElementCollection) filterSection.Items);
    }

    internal static void AddClassificationFilterSections(
      IDynamicModuleType moduleType,
      WidgetBarElement widgetBar,
      WidgetBarSectionElement filterSection)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      if (widgetBar == null)
        throw new ArgumentNullException(nameof (widgetBar));
      if (filterSection == null)
        throw new ArgumentNullException("parentSection");
      ConfigElementList<WidgetBarSectionElement> sections = widgetBar.Sections;
      List<IDynamicModuleField> list = moduleType.Fields.Where<IDynamicModuleField>((Func<IDynamicModuleField, bool>) (c => c.FieldType == FieldType.Classification && c.FieldStatus != DynamicModuleFieldStatus.Removed)).OrderBy<IDynamicModuleField, string>((Func<IDynamicModuleField, string>) (x => x.Title)).ToList<IDynamicModuleField>();
      string[] array = list.Select<IDynamicModuleField, string>((Func<IDynamicModuleField, string>) (c => string.Format("{0}FilterSection", (object) c.Name.ToLower()))).ToArray<string>();
      if (list.Count <= 0)
        return;
      TaxonomyManager manager = TaxonomyManager.GetManager();
      foreach (IDynamicModuleField dynamicModuleField in list)
      {
        WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) sections)
        {
          Name = dynamicModuleField.Name,
          Title = string.Format("{0} by {1}", (object) moduleType.TypeName, (object) dynamicModuleField.Name),
          CssClass = "sfFilterBy sfSeparator",
          WrapperTagId = string.Format("{0}FilterSection", (object) dynamicModuleField.Name.ToLower()),
          Visible = new bool?(false)
        };
        sections.Add(element1);
        ConfigElementList<WidgetElement> items = element1.Items;
        CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
        element2.Name = string.Format("Close{0}", (object) dynamicModuleField.Name);
        element2.CommandName = "showSectionsExceptAndResetFilter";
        element2.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(array);
        element2.ButtonType = CommandButtonType.SimpleLinkButton;
        element2.Text = string.Format("Close {0}", (object) dynamicModuleField.Name);
        element2.CssClass = "sfCloseFilter";
        element2.WidgetType = typeof (CommandWidget);
        element2.IsSeparator = false;
        items.Add((WidgetElement) element2);
        Type type = manager.GetTaxonomy(dynamicModuleField.ClassificationId).GetType();
        DynamicCommandWidgetElement commandWidgetElement = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
        commandWidgetElement.Name = string.Format("{0}Filter", (object) dynamicModuleField.Name);
        commandWidgetElement.CommandName = string.Format("filterBy_Classification_{0}", (object) dynamicModuleField.Name);
        commandWidgetElement.WidgetType = typeof (DynamicCommandWidget);
        commandWidgetElement.IsSeparator = false;
        commandWidgetElement.SelectedItemCssClass = "sfSel";
        DynamicCommandWidgetElement element3 = commandWidgetElement;
        element3.ClientItemTemplate = "<a href='javascript:void(0);' class='sf_binderCommand_" + element3.CommandName + "'>{{ Title }}</a> <span class='sfCount'>({{ItemsCount}})</span>";
        if (type == typeof (FlatTaxonomy))
        {
          element3.BindTo = BindCommandListTo.Client;
          element3.BaseServiceUrl = string.Format("~/Sitefinity/Services/Taxonomies/FlatTaxon.svc/{0}/", (object) dynamicModuleField.ClassificationId);
          element3.PageSize = 30;
          element3.MoreLinkText = string.Format("Show more {0}", (object) dynamicModuleField.Name.ToLower());
          element3.MoreLinkCssClass = "sfShowMore";
          element3.LessLinkText = string.Format("Show less {0}", (object) dynamicModuleField.Name.ToLower());
          element3.LessLinkCssClass = "sfShowMore";
        }
        else if (type == typeof (HierarchicalTaxonomy))
        {
          element3.BindTo = BindCommandListTo.HierarchicalData;
          element3.BaseServiceUrl = string.Format("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/{0}/", (object) dynamicModuleField.ClassificationId);
          element3.ChildItemsServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/subtaxa/";
          element3.PredecessorServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/predecessor/";
          element3.PageSize = 0;
        }
        element3.UrlParameters.Add("itemType", moduleType.GetFullTypeName());
        element1.Items.Add((WidgetElement) element3);
        DefinitionsHelper.CreateTaxonomyLink(dynamicModuleField.ClassificationId, "hideSectionsExcept", DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element1.WrapperTagId), filterSection);
      }
    }

    /// <summary>
    /// Generates the settings sidebar section element for the given dynamic module type and returns it.
    /// </summary>
    /// <param name="moduleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the toolbar ought to be
    /// generated.
    /// </param>
    /// <param name="parentElement">
    /// The parent element of the toolbar element.
    /// </param>
    /// <param name="linksConfig">The config file where we should add LinkElement</param>
    /// <remarks>
    /// The sidebar element will not be added to the parent element.
    /// </remarks>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetBarSectionElement" /> representing the settings sidebar section.
    /// </returns>
    public static WidgetBarSectionElement GenerateSettings(
      IDynamicModuleType moduleType,
      ConfigElement parentElement,
      ConfigElementDictionary<string, LinkElement> linksConfig)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      WidgetBarSectionElement settings = parentElement != null ? new WidgetBarSectionElement(parentElement) : throw new ArgumentNullException(nameof (parentElement));
      settings.Name = "Settings";
      settings.Title = "Settings";
      settings.ResourceClassId = typeof (ModuleBuilderResources).Name;
      settings.CssClass = "sfWidgetsList sfSettings sfSeparator";
      settings.WrapperTagId = "settingsSection";
      LinkElement element1 = new LinkElement((ConfigElement) linksConfig)
      {
        Name = "NavigateToContentTypesLink",
        CommandName = "goBackToContentTypes",
        NavigateUrl = SidebarGenerator.GetContentTypesPageUrl(moduleType.ModuleId)
      };
      linksConfig.Add(element1);
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) settings.Items);
      element2.Name = "NavigateToContentTypes";
      element2.CommandName = "goBackToContentTypes";
      element2.ButtonType = CommandButtonType.SimpleLinkButton;
      element2.Text = SidebarGenerator.GetResForNavigateLink();
      element2.WidgetType = typeof (CommandWidget);
      element2.WrapperTagKey = HtmlTextWriterTag.Span;
      element2.IsSeparator = false;
      settings.Items.Add((WidgetElement) element2);
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) settings.Items);
      element3.Name = "ManageContentLocations";
      element3.CommandName = "manageContentLocations";
      element3.ButtonType = CommandButtonType.SimpleLinkButton;
      element3.Text = "PagesWhereTheseItemsArePublished";
      element3.ResourceClassId = typeof (ModuleBuilderResources).Name;
      element3.WidgetType = typeof (CommandWidget);
      settings.Items.Add((WidgetElement) element3);
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) settings.Items);
      element4.Name = "Permissions";
      element4.CommandName = "permissions";
      element4.ButtonType = CommandButtonType.SimpleLinkButton;
      element4.Text = "Permissions";
      element4.ResourceClassId = typeof (ModuleBuilderResources).Name;
      element4.WidgetType = typeof (CommandWidget);
      element4.IsSeparator = false;
      settings.Items.Add((WidgetElement) element4);
      return settings;
    }

    /// <summary>Generates the languages section element</summary>
    /// <param name="parentElement">
    /// The sidebar element to which the section ought to be added.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.Localization.Configuration.LocalizationWidgetBarSectionElement" /> representing the languages sidebar section.
    /// </returns>
    public static LocalizationWidgetBarSectionElement GenerateLanguagesSection(
      ConfigElement parentElement)
    {
      LocalizationWidgetBarSectionElement barSectionElement = parentElement != null ? new LocalizationWidgetBarSectionElement(parentElement) : throw new ArgumentNullException(nameof (parentElement));
      barSectionElement.Name = "Languages";
      barSectionElement.Title = "Languages";
      barSectionElement.ResourceClassId = typeof (LocalizationResources).Name;
      barSectionElement.CssClass = "sfFirst sfSeparator sfLangSelector";
      barSectionElement.WrapperTagId = "languagesSection";
      LocalizationWidgetBarSectionElement languagesSection = barSectionElement;
      ConfigElementList<WidgetElement> items = languagesSection.Items;
      LanguagesDropDownListWidgetElement element = new LanguagesDropDownListWidgetElement((ConfigElement) languagesSection.Items);
      element.Name = "Languages";
      element.Text = "Languages";
      element.ResourceClassId = typeof (LocalizationResources).Name;
      element.CssClass = "";
      element.WidgetType = typeof (LanguagesDropDownListWidget);
      element.IsSeparator = false;
      element.LanguageSource = LanguageSource.Frontend;
      element.AddAllLanguagesOption = false;
      element.CommandName = "changeLanguage";
      items.Add((WidgetElement) element);
      return languagesSection;
    }

    /// <summary>Resolves the parent link of the code-reference</summary>
    /// <returns>Parent url</returns>
    private static string GetContentTypesPageUrl(Guid moduleID) => BackendSiteMap.FindSiteMapNode(ModuleBuilderModule.contentTypeDashboardPageId, false) == null ? string.Empty : RouteHelper.CreateNodeReference(ModuleBuilderModule.contentTypeDashboardPageId) + "/" + (object) moduleID;

    private static string GetResForNavigateLink() => Res.Get<ModuleBuilderResources>("NavigatetoContentTypes");

    private static string GetResForFilterSectionTitle() => Res.Get<ModuleBuilderResources>().FilterSectionTitle;

    private static string GetResForAllItemsFormated() => Res.Get<ModuleBuilderResources>().AllItemsFormated;

    private static string GetResForMyItemsFormated() => Res.Get<ModuleBuilderResources>().MyItemsFormatted;
  }
}
