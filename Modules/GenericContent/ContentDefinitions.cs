// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.ContentDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI.Views;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Web.UI.Config;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// This is a static class used to initialize the properties for all ContentView control views
  /// of supplied by default for the generic content module.
  /// </summary>
  public class ContentDefinitions
  {
    /// <summary>Definition name for the frontend</summary>
    public static readonly string FrontendDefinitionName = "GenericContentFrontend";
    /// <summary>Default name for the frontend master view</summary>
    public static readonly string FrontendMasterViewName = "GenericContentMasterView";
    /// <summary>Default name fo the frontend details view</summary>
    public static readonly string FrontendDetailViewName = "GenericContentDetailView";
    /// <summary>Name of the module.</summary>
    public const string ModuleName = "GenericContent";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for generic content module on the backend.
    /// </summary>
    public static string BackendDefinitionName = "ContentBackend";
    /// <summary>
    /// Name of the view used to display content items in a list in the generic content module
    /// on the backend.
    /// </summary>
    public static string BackendListViewName = "ContentBackendList";
    /// <summary>
    /// Name of the view used to edit content item in the generic content module on the backend.
    /// </summary>
    public static string BackendEditDetailsViewName = "ContentBackendEdit";
    /// <summary>
    /// Name of the view used to insert content item in the generic content module on the backend.
    /// </summary>
    public static string BackendInsertDetailsViewName = "ContentBackendInsert";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for generic content module comments on the backend.
    /// </summary>
    public static string BackendCommentsDefinitionName = "ContentCommentsBackend";
    /// <summary>
    /// Name of the view used to preview history version of a content item.
    /// </summary>
    public const string BackendVersionPreviewViewName = "ContentBackendVersionPreview";
    /// <summary>Name of the view used to preview a content item.</summary>
    public const string BackendPreviewViewName = "ContentBackendPreview";
    /// <summary>Name of the view used to duplicate a content item.</summary>
    public const string BackendDuplicateViewName = "ContentBackendDuplicate";
    /// <summary>Definition name for the frontend commments list.</summary>
    public static readonly string FrontendCommentsDefinitionName = "ContentCommentsFrontend";
    private const string ComparisonViewHistoryScreenQueryParameter = "VersionComparisonView";
    /// <summary>Version Comparison View Name</summary>
    public const string VersionComparisonView = "NewsBackendVersionComparisonView";
    public static readonly string ResourceClassId = typeof (ContentResources).Name;

    /// <summary>
    /// Defines the ContentView control for Generic Content on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineContentBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("GenericContent").DefineContainer(parent, ContentDefinitions.BackendDefinitionName, typeof (ContentItem)).DoNotUseWorkflow();
      ContentViewControlElement backendContentView = fluentContentView.Get();
      backendContentView.ViewsConfig.AddLazy((object) ContentDefinitions.BackendListViewName, (Func<ConfigElement>) (() => ContentDefinitions.DefineContentBackendListView(backendContentView, fluentContentView)));
      backendContentView.ViewsConfig.AddLazy((object) ContentDefinitions.BackendEditDetailsViewName, (Func<ConfigElement>) (() => ContentDefinitions.DefineContentBackendEditView(fluentContentView)));
      backendContentView.ViewsConfig.AddLazy((object) ContentDefinitions.BackendInsertDetailsViewName, (Func<ConfigElement>) (() => ContentDefinitions.DefineContentBackendInsertView(fluentContentView)));
      backendContentView.ViewsConfig.AddLazy((object) "ContentBackendPreview", (Func<ConfigElement>) (() => ContentDefinitions.DefineContentBackendPreviewView(fluentContentView)));
      backendContentView.ViewsConfig.AddLazy((object) "ContentBackendDuplicate", (Func<ConfigElement>) (() => ContentDefinitions.DefineContentBackendDuplicateView(fluentContentView)));
      backendContentView.ViewsConfig.AddLazy((object) "ContentBackendVersionPreview", (Func<ConfigElement>) (() => ContentDefinitions.DefineContentBackendVersionPreviewView(fluentContentView)));
      backendContentView.ViewsConfig.AddLazy((object) "NewsBackendVersionComparisonView", (Func<ConfigElement>) (() => ContentDefinitions.DefineContentBackendVersionComparisonView(backendContentView)));
      return backendContentView;
    }

    private static ConfigElement DefineContentBackendListView(
      ContentViewControlElement backendContentView,
      ContentViewControlDefinitionFacade fluentContentView)
    {
      MasterViewDefinitionFacade fluentFacade = fluentContentView.AddMasterView(ContentDefinitions.BackendListViewName).LocalizeUsing<ContentResources>().SetTitle("ContentBlocksShared").SetServiceBaseUrl("~/Sitefinity/Services/Content/ContentItemService.svc/").SetDeleteSingleConfirmationMessage("DeleteSingleConfirmationMessage").SetDeleteMultipleConfirmationMessage("DeleteMultipleConfirmationMessage");
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary.Add("Telerik.Sitefinity.Modules.GenericContent.Web.UI.Scripts.ContentBlockGridExtensions.js, Telerik.Sitefinity", "OnMasterViewLoaded");
      MasterGridViewElement masterGridViewElement = fluentFacade.Get();
      masterGridViewElement.ExternalClientScripts = dictionary;
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "CreateContentWidget";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "create";
      element2.Text = "CreateNewItem";
      element2.ResourceClassId = typeof (ContentResources).Name;
      element2.CssClass = "sfMainAction";
      element2.WidgetType = typeof (CommandWidget);
      element2.PermissionSet = "General";
      element2.ActionName = "Create";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "DeleteContentWidget";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "groupDelete";
      element3.Text = "Delete";
      element3.ResourceClassId = typeof (ContentResources).Name;
      element3.WidgetType = typeof (CommandWidget);
      element3.CssClass = "sfGroupBtn";
      items2.Add((WidgetElement) element3);
      element1.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element1.Items, typeof (ContentItem)));
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (ContentItem), false, masterGridViewElement.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (ContentItem), true, masterGridViewElement.Section);
      DynamicCommandWidgetElement commandWidgetElement1 = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "EditCustomSorting";
      commandWidgetElement1.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement1.HeaderText = "Sort";
      commandWidgetElement1.PageSize = 10;
      commandWidgetElement1.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement1.WidgetType = typeof (SortWidget);
      commandWidgetElement1.ResourceClassId = ContentDefinitions.ResourceClassId;
      commandWidgetElement1.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement1.ContentType = typeof (ContentItem);
      DynamicCommandWidgetElement element4 = commandWidgetElement1;
      element1.Items.Add((WidgetElement) element4);
      foreach (SortingExpressionElement expressionElement in expressionSettings1)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element4.Items, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element4.Items.Add(dynamicItemElement);
        element4.DesignTimeItems.Add(dynamicItemElement.GetKey());
      }
      foreach (SortingExpressionElement expressionElement in expressionSettings2)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element4.CustomItems, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element4.CustomItems.Add(dynamicItemElement);
      }
      masterGridViewElement.ToolbarConfig.Sections.Add(element1);
      LocalizationWidgetBarSectionElement barSectionElement1 = new LocalizationWidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections);
      barSectionElement1.Name = "Languages";
      barSectionElement1.Title = "Languages";
      barSectionElement1.ResourceClassId = typeof (LocalizationResources).Name;
      barSectionElement1.CssClass = "sfFirst sfSeparator sfLangSelector";
      barSectionElement1.WrapperTagId = "languagesSection";
      LocalizationWidgetBarSectionElement element5 = barSectionElement1;
      ConfigElementList<WidgetElement> items3 = element5.Items;
      LanguagesDropDownListWidgetElement element6 = new LanguagesDropDownListWidgetElement((ConfigElement) element5.Items);
      element6.Name = "Languages";
      element6.Text = "Languages";
      element6.ResourceClassId = typeof (LocalizationResources).Name;
      element6.CssClass = "";
      element6.WidgetType = typeof (LanguagesDropDownListWidget);
      element6.IsSeparator = false;
      element6.LanguageSource = LanguageSource.Frontend;
      element6.AddAllLanguagesOption = false;
      element6.CommandName = "changeLanguage";
      items3.Add((WidgetElement) element6);
      WidgetBarSectionElement barSectionElement2 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "Filter",
        Title = "FilterContentItems",
        ResourceClassId = typeof (ContentResources).Name,
        CssClass = "sfWidgetsList sfFirst sfSeparator sfModules",
        WrapperTagId = "filterSection"
      };
      ConfigElementList<WidgetElement> items4 = barSectionElement2.Items;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element7.Name = "AllItems";
      element7.CommandName = "showAllItems";
      element7.ButtonType = CommandButtonType.SimpleLinkButton;
      element7.ButtonCssClass = "sfSel";
      element7.Text = "AllItems";
      element7.ResourceClassId = typeof (ContentResources).Name;
      element7.CssClass = "";
      element7.WidgetType = typeof (CommandWidget);
      element7.IsSeparator = false;
      items4.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> items5 = barSectionElement2.Items;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element8.Name = "MyItems";
      element8.CommandName = "showMyItems";
      element8.ButtonType = CommandButtonType.SimpleLinkButton;
      element8.Text = "MyItems";
      element8.ResourceClassId = typeof (ContentResources).Name;
      element8.CssClass = "";
      element8.WidgetType = typeof (CommandWidget);
      element8.IsSeparator = false;
      items5.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> items6 = barSectionElement2.Items;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element9.Name = "NotUsedItems";
      element9.CommandName = "showNotUsedItems";
      element9.ButtonType = CommandButtonType.SimpleLinkButton;
      element9.Text = "NotUsedItems";
      element9.ResourceClassId = typeof (ContentResources).Name;
      element9.CssClass = "";
      element9.WidgetType = typeof (CommandWidget);
      element9.IsSeparator = false;
      element9.IsFilterCommand = true;
      items6.Add((WidgetElement) element9);
      DefinitionsHelper.AppendStatusFilters((ConfigElementCollection) barSectionElement2.Items);
      ConfigElementList<WidgetElement> items7 = barSectionElement2.Items;
      LiteralWidgetElement element10 = new LiteralWidgetElement((ConfigElement) barSectionElement2.Items);
      element10.Name = "Separator";
      element10.WrapperTagKey = HtmlTextWriterTag.Li;
      element10.WidgetType = typeof (LiteralWidget);
      element10.CssClass = "sfSeparator";
      element10.Text = "&nbsp;";
      element10.IsSeparator = true;
      items7.Add((WidgetElement) element10);
      WidgetBarSectionElement element11 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ByCategory",
        Title = "ContentItemsByCategory",
        ResourceClassId = typeof (ContentResources).Name,
        CssClass = "sfFilterBy sfSeparator",
        WrapperTagId = "categoryFilterSection",
        Visible = new bool?(false)
      };
      masterGridViewElement.SidebarConfig.Sections.Add(element11);
      WidgetBarSectionElement element12 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ByTag",
        Title = "ContentItemsByTag",
        ResourceClassId = typeof (ContentResources).Name,
        CssClass = "sfFilterBy sfSeparator",
        WrapperTagId = "tagFilterSection",
        Visible = new bool?(false)
      };
      masterGridViewElement.SidebarConfig.Sections.Add(element12);
      WidgetBarSectionElement element13 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "LastUpdated",
        Title = "DisplayLastUpdatedItems",
        ResourceClassId = typeof (ContentResources).Name,
        CssClass = "sfFilterBy sfFilterByDate sfSeparator",
        WrapperTagId = "dateFilterSection",
        Visible = new bool?(false)
      };
      masterGridViewElement.SidebarConfig.Sections.Add(element13);
      ConfigElementList<WidgetElement> items8 = element11.Items;
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element11.Items);
      commandWidgetElement2.Name = "CloseCategories";
      commandWidgetElement2.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement2.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element11.WrapperTagId, element12.WrapperTagId, element13.WrapperTagId);
      commandWidgetElement2.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement2.Text = "CloseCategories";
      commandWidgetElement2.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement2.CssClass = "sfCloseFilter";
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      commandWidgetElement2.IsSeparator = false;
      CommandWidgetElement element14 = commandWidgetElement2;
      items8.Add((WidgetElement) element14);
      DynamicCommandWidgetElement commandWidgetElement3 = new DynamicCommandWidgetElement((ConfigElement) element11.Items);
      commandWidgetElement3.Name = "CategoryFilter";
      commandWidgetElement3.CommandName = "filterByCategory";
      commandWidgetElement3.PageSize = 0;
      commandWidgetElement3.WidgetType = typeof (DynamicCommandWidget);
      commandWidgetElement3.IsSeparator = false;
      commandWidgetElement3.BindTo = BindCommandListTo.HierarchicalData;
      commandWidgetElement3.BaseServiceUrl = string.Format("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/{0}/", (object) TaxonomyManager.CategoriesTaxonomyId);
      commandWidgetElement3.ChildItemsServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/subtaxa/";
      commandWidgetElement3.PredecessorServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/predecessor/";
      commandWidgetElement3.ClientItemTemplate = "<a href='javascript:void(0);' class='sf_binderCommand_filterByCategory'>{{ Title.htmlEncode() }}</a> <span class='sfCount'>({{ItemsCount}})</span>";
      DynamicCommandWidgetElement element15 = commandWidgetElement3;
      element15.UrlParameters.Add("itemType", typeof (ContentItem).AssemblyQualifiedName);
      element11.Items.Add((WidgetElement) element15);
      ConfigElementList<WidgetElement> items9 = element12.Items;
      CommandWidgetElement commandWidgetElement4 = new CommandWidgetElement((ConfigElement) element12.Items);
      commandWidgetElement4.Name = "CloseTags";
      commandWidgetElement4.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement4.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element12.WrapperTagId, element11.WrapperTagId, element13.WrapperTagId);
      commandWidgetElement4.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement4.Text = "CloseTags";
      commandWidgetElement4.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement4.CssClass = "sfCloseFilter";
      commandWidgetElement4.WidgetType = typeof (CommandWidget);
      commandWidgetElement4.IsSeparator = false;
      CommandWidgetElement element16 = commandWidgetElement4;
      items9.Add((WidgetElement) element16);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<a href=\"javascript:void(0);\" class=\"sf_binderCommand_filterByTag");
      stringBuilder.Append("\">{{Title.htmlEncode()}}</a> <span class='sfCount'>({{ItemsCount}})</span>");
      DynamicCommandWidgetElement commandWidgetElement5 = new DynamicCommandWidgetElement((ConfigElement) element12.Items);
      commandWidgetElement5.Name = "TagFilter";
      commandWidgetElement5.CommandName = "filterByTag";
      commandWidgetElement5.PageSize = 30;
      commandWidgetElement5.WidgetType = typeof (DynamicCommandWidget);
      commandWidgetElement5.IsSeparator = false;
      commandWidgetElement5.BindTo = BindCommandListTo.Client;
      commandWidgetElement5.BaseServiceUrl = string.Format("~/Sitefinity/Services/Taxonomies/FlatTaxon.svc/{0}/", (object) TaxonomyManager.TagsTaxonomyId);
      commandWidgetElement5.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement5.MoreLinkText = "ShowMoreTags";
      commandWidgetElement5.MoreLinkCssClass = "sfShowMore";
      commandWidgetElement5.LessLinkText = "ShowLessTags";
      commandWidgetElement5.LessLinkCssClass = "sfShowMore";
      commandWidgetElement5.SelectedItemCssClass = "sfSel";
      commandWidgetElement5.ClientItemTemplate = stringBuilder.ToString();
      DynamicCommandWidgetElement element17 = commandWidgetElement5;
      element17.UrlParameters.Add("itemType", typeof (ContentItem).AssemblyQualifiedName);
      element12.Items.Add((WidgetElement) element17);
      DefinitionsHelper.CreateTaxonomyLink(TaxonomyManager.CategoriesTaxonomyId, "hideSectionsExcept", DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element11.WrapperTagId), barSectionElement2);
      DefinitionsHelper.CreateTaxonomyLink(TaxonomyManager.TagsTaxonomyId, "hideSectionsExcept", DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element12.WrapperTagId), barSectionElement2);
      CommandWidgetElement commandWidgetElement6 = new CommandWidgetElement((ConfigElement) element13.Items);
      commandWidgetElement6.Name = "CloseDateFilter";
      commandWidgetElement6.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement6.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element12.WrapperTagId, element11.WrapperTagId, element13.WrapperTagId);
      commandWidgetElement6.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement6.Text = "CloseDateFilter";
      commandWidgetElement6.ResourceClassId = typeof (ContentResources).Name;
      commandWidgetElement6.CssClass = "sfCloseFilter";
      commandWidgetElement6.WidgetType = typeof (CommandWidget);
      commandWidgetElement6.IsSeparator = false;
      CommandWidgetElement element18 = commandWidgetElement6;
      element13.Items.Add((WidgetElement) element18);
      DateFilteringWidgetDefinitionElement definitionElement = new DateFilteringWidgetDefinitionElement((ConfigElement) element13.Items);
      definitionElement.Name = "DateFilter";
      definitionElement.WidgetType = typeof (DateFilteringWidget);
      definitionElement.IsSeparator = false;
      definitionElement.PropertyNameToFilter = "LastModified";
      DateFilteringWidgetDefinitionElement element19 = definitionElement;
      DefinitionsHelper.GetPredefinedDateFilteringRanges(element19.PredefinedFilteringRanges);
      element13.Items.Add((WidgetElement) element19);
      ConfigElementList<WidgetElement> items10 = barSectionElement2.Items;
      CommandWidgetElement commandWidgetElement7 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      commandWidgetElement7.Name = "FilterByDate";
      commandWidgetElement7.CommandName = "hideSectionsExcept";
      commandWidgetElement7.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element13.WrapperTagId);
      commandWidgetElement7.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement7.Text = "ByDateModified";
      commandWidgetElement7.ResourceClassId = typeof (ContentResources).Name;
      commandWidgetElement7.WidgetType = typeof (CommandWidget);
      commandWidgetElement7.IsSeparator = false;
      CommandWidgetElement element20 = commandWidgetElement7;
      items10.Add((WidgetElement) element20);
      WidgetBarSectionElement element21 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ContentBlocksSettings",
        Title = "SettingsForContentBlocks",
        ResourceClassId = typeof (ContentResources).Name,
        CssClass = "sfWidgetsList sfSettings",
        WrapperTagId = "settingsSection"
      };
      ConfigElementList<WidgetElement> items11 = element21.Items;
      CommandWidgetElement element22 = new CommandWidgetElement((ConfigElement) element21.Items);
      element22.Name = "ContentPermissions";
      element22.CommandName = "permissions";
      element22.ButtonType = CommandButtonType.SimpleLinkButton;
      element22.Text = "PermissionsForContentBlocks";
      element22.ResourceClassId = typeof (ContentResources).Name;
      element22.WidgetType = typeof (CommandWidget);
      element22.IsSeparator = false;
      items11.Add((WidgetElement) element22);
      ConfigElementList<WidgetElement> items12 = element21.Items;
      CommandWidgetElement element23 = new CommandWidgetElement((ConfigElement) element21.Items);
      element23.Name = "Settings";
      element23.CommandName = "settings";
      element23.ButtonType = CommandButtonType.SimpleLinkButton;
      element23.Text = "Settings";
      element23.ResourceClassId = typeof (ContentResources).Name;
      element23.WidgetType = typeof (CommandWidget);
      element23.IsSeparator = false;
      items12.Add((WidgetElement) element23);
      masterGridViewElement.SidebarConfig.Title = "ManageContentBlocks";
      masterGridViewElement.SidebarConfig.ResourceClassId = typeof (ContentResources).Name;
      masterGridViewElement.SidebarConfig.Sections.Add((WidgetBarSectionElement) element5);
      masterGridViewElement.SidebarConfig.Sections.Add(barSectionElement2);
      masterGridViewElement.SidebarConfig.Sections.Add(element21);
      LocalizationWidgetBarSectionElement barSectionElement3 = new LocalizationWidgetBarSectionElement((ConfigElement) masterGridViewElement.ContextBarConfig.Sections);
      barSectionElement3.Name = "contextBar";
      barSectionElement3.WrapperTagKey = HtmlTextWriterTag.Div;
      barSectionElement3.CssClass = "sfContextWidgetWrp";
      barSectionElement3.MinLanguagesCountTreshold = new int?(6);
      LocalizationWidgetBarSectionElement element24 = barSectionElement3;
      ConfigElementList<WidgetElement> items13 = element24.Items;
      CommandWidgetElement element25 = new CommandWidgetElement((ConfigElement) element24.Items);
      element25.Name = "ShowMoreTranslations";
      element25.CommandName = "showMoreTranslations";
      element25.ButtonType = CommandButtonType.SimpleLinkButton;
      element25.Text = "ShowAllTranslations";
      element25.ResourceClassId = typeof (LocalizationResources).Name;
      element25.WidgetType = typeof (CommandWidget);
      element25.IsSeparator = false;
      element25.CssClass = "sfShowHideLangVersions";
      element25.WrapperTagKey = HtmlTextWriterTag.Div;
      items13.Add((WidgetElement) element25);
      ConfigElementList<WidgetElement> items14 = element24.Items;
      CommandWidgetElement element26 = new CommandWidgetElement((ConfigElement) element24.Items);
      element26.Name = "HideMoreTranslations";
      element26.CommandName = "hideMoreTranslations";
      element26.ButtonType = CommandButtonType.SimpleLinkButton;
      element26.Text = "ShowBasicTranslationsOnly";
      element26.ResourceClassId = typeof (LocalizationResources).Name;
      element26.WidgetType = typeof (CommandWidget);
      element26.IsSeparator = false;
      element26.CssClass = "sfDisplayNone sfShowHideLangVersions";
      element26.WrapperTagKey = HtmlTextWriterTag.Div;
      items14.Add((WidgetElement) element26);
      masterGridViewElement.ContextBarConfig.Sections.Add((WidgetBarSectionElement) element24);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) masterGridViewElement.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element27 = gridViewModeElement;
      masterGridViewElement.ViewModesConfig.Add((ViewModeElement) element27);
      ConfigElementDictionary<string, ColumnElement> columnsConfig1 = element27.ColumnsConfig;
      DataColumnElement element28 = new DataColumnElement((ConfigElement) element27.ColumnsConfig);
      element28.Name = "Title";
      element28.HeaderText = "Title";
      element28.ResourceClassId = typeof (Labels).Name;
      element28.HeaderCssClass = "sfTitleCol";
      element28.ItemCssClass = "sfTitleCol";
      element28.ClientTemplate = "<a sys:href='javascript:void(0);' sys:class=\"{{ 'sf_binderCommand_edit sfItemTitle sf' + UIStatus.toLowerCase() + ' ' + (AdditionalStatus ? ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() : '')}}\">\r\n                    <strong>{{Title.htmlEncode()}}</strong>\r\n                    <span sys:if='AdditionalStatus' class='sfStatusAdditional'>{{AdditionalStatus.Text}}</span>\r\n                    <span class='sfStatusLocation'>\r\n                        {{Status}}\r\n                    </span></a>";
      columnsConfig1.Add((ColumnElement) element28);
      DynamicColumnElement dynamicColumnElement1 = new DynamicColumnElement((ConfigElement) element27.ColumnsConfig);
      dynamicColumnElement1.Name = "Translations";
      dynamicColumnElement1.HeaderText = "Translations";
      dynamicColumnElement1.ResourceClassId = typeof (LocalizationResources).Name;
      dynamicColumnElement1.DynamicMarkupGenerator = typeof (LanguagesColumnMarkupGenerator);
      dynamicColumnElement1.ItemCssClass = "sfLanguagesCol";
      dynamicColumnElement1.HeaderCssClass = "sfLanguagesCol";
      DynamicColumnElement dynamicColumnElement2 = dynamicColumnElement1;
      dynamicColumnElement2.GeneratorSettingsElement = (DynamicMarkupGeneratorElement) new LanguagesColumnMarkupGeneratorElement((ConfigElement) dynamicColumnElement2)
      {
        LanguageSource = LanguageSource.Frontend,
        ItemsInGroupCount = 6,
        ContainerTag = "div",
        GroupTag = "div",
        ItemTag = "div",
        ContainerClass = string.Empty,
        GroupClass = string.Empty,
        ItemClass = string.Empty
      };
      element27.ColumnsConfig.Add((ColumnElement) dynamicColumnElement2);
      ActionMenuColumnElement menuColumnElement1 = new ActionMenuColumnElement((ConfigElement) element27.ColumnsConfig);
      menuColumnElement1.Name = "Actions";
      menuColumnElement1.HeaderText = "Actions";
      menuColumnElement1.ResourceClassId = typeof (Labels).Name;
      menuColumnElement1.HeaderCssClass = "sfMoreActions";
      menuColumnElement1.ItemCssClass = "sfMoreActions";
      ActionMenuColumnElement menuColumnElement2 = menuColumnElement1;
      ContentDefinitions.FillActionMenuItems(menuColumnElement2.MenuItems, (ConfigElement) menuColumnElement2, typeof (ContentResources).Name);
      element27.ColumnsConfig.Add((ColumnElement) menuColumnElement2);
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element27.ColumnsConfig;
      DataColumnElement element29 = new DataColumnElement((ConfigElement) element27.ColumnsConfig);
      element29.Name = "UsedOn";
      element29.HeaderText = "ContentBlocksUsedColumnHeaderText";
      element29.ResourceClassId = typeof (ContentResources).Name;
      element29.ClientTemplate = "<span class=\"sf_binderCommand_viewContentPages\">{{PagesCountUIString}}</span>";
      element29.HeaderCssClass = "sfUsedOnPage";
      element29.ItemCssClass = "sfUsedOnPage";
      columnsConfig2.Add((ColumnElement) element29);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) element27.ColumnsConfig);
      dataColumnElement1.Name = "Owner";
      dataColumnElement1.HeaderText = "Owner";
      dataColumnElement1.ResourceClassId = typeof (Labels).Name;
      dataColumnElement1.ClientTemplate = "<span class='sfLine'>{{Owner ? Owner : ''}}</span>";
      dataColumnElement1.HeaderCssClass = "sfRegular";
      dataColumnElement1.ItemCssClass = "sfRegular";
      DataColumnElement element30 = dataColumnElement1;
      element27.ColumnsConfig.Add((ColumnElement) element30);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) element27.ColumnsConfig);
      dataColumnElement2.Name = "Date";
      dataColumnElement2.HeaderText = "Date";
      dataColumnElement2.ResourceClassId = typeof (Labels).Name;
      dataColumnElement2.ClientTemplate = "<span>{{ (LastModified) ? LastModified.sitefinityLocaleFormat('dd MMM, yyyy hh:mm') : '-' }}</span>";
      dataColumnElement2.HeaderCssClass = "sfDateAndHour";
      dataColumnElement2.ItemCssClass = "sfDateAndHour";
      DataColumnElement element31 = dataColumnElement2;
      element27.ColumnsConfig.Add((ColumnElement) element31);
      DecisionScreenElement element32 = new DecisionScreenElement((ConfigElement) masterGridViewElement.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "WhatDoYouWantToDoNow",
        MessageText = "NoContentItems",
        ResourceClassId = typeof (ContentResources).Name
      };
      ConfigElementList<CommandWidgetElement> actions = element32.Actions;
      CommandWidgetElement element33 = new CommandWidgetElement((ConfigElement) element32.Actions);
      element33.Name = "Create";
      element33.ButtonType = CommandButtonType.Create;
      element33.CommandName = "create";
      element33.Text = "CreateNewItem";
      element33.ResourceClassId = typeof (ContentResources).Name;
      element33.CssClass = "sfCreateItem";
      element33.PermissionSet = "General";
      element33.ActionName = "Create";
      actions.Add(element33);
      masterGridViewElement.DecisionScreensConfig.Add(element32);
      ContentDefinitions.CreateDialogs<MasterViewDefinitionFacade>((IDialogsSupportableFacade<MasterViewDefinitionFacade>) fluentFacade);
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "viewSettings",
        CommandName = "settings",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.AdvancedSettingsNodeId) + "/Content"
      });
      return (ConfigElement) masterGridViewElement;
    }

    private static ConfigElement DefineContentBackendEditView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView(ContentDefinitions.BackendEditDetailsViewName).SetTitle("EditItem").LocalizeUsing<ContentResources>().SetServiceBaseUrl("~/Sitefinity/Services/Content/ContentItemService.svc/").RenderTranslationView().SetAlternativeTitle("CreateNewItem");
      DetailFormViewElement detailView = fluentDetailView.Get();
      ContentViewSectionElement viewSectionElement = fluentDetailView.AddReadOnlySection("SidebarSection").Get();
      ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement.Fields;
      LinkFieldDefinitionElement element = new LinkFieldDefinitionElement((ConfigElement) viewSectionElement.Fields);
      element.ID = "RevisionHistoryLink1";
      element.Title = "ReviewHistory";
      element.CommandName = "history";
      element.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element.CssClass = "openRevisionHistoryButton";
      element.ResourceClassId = typeof (ContentResources).Name;
      element.WrapperTag = HtmlTextWriterTag.Li;
      element.FieldName = "RevisionHistoryField1";
      element.FieldType = typeof (LinkField);
      fields.Add((FieldDefinitionElement) element);
      ContentDefinitions.CreateBackendSections(fluentDetailView);
      ContentDefinitions.CreateBackendFormToolbar(detailView, false);
      return (ConfigElement) detailView;
    }

    private static ConfigElement DefineContentBackendInsertView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView(ContentDefinitions.BackendInsertDetailsViewName).SetTitle("CreateNewItem").LocalizeUsing<ContentResources>().SetServiceBaseUrl("~/Sitefinity/Services/Content/ContentItemService.svc/");
      DetailFormViewElement detailView = fluentDetailView.Get();
      ContentDefinitions.CreateBackendSections(fluentDetailView);
      ContentDefinitions.CreateBackendFormToolbar(detailView, true);
      return (ConfigElement) detailView;
    }

    private static ConfigElement DefineContentBackendPreviewView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Versioning.Web.UI.Scripts.VersionHistoryExtender.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView("ContentBackendPreview").SetTitle("View").SetDisplayMode(FieldDisplayMode.Read).LocalizeUsing<ContentResources>().SetExternalClientScripts(extenalClientScripts).HideNavigation().SetServiceBaseUrl("~/Sitefinity/Services/Content/ContentItemService.svc/");
      DetailFormViewElement detailFormViewElement = fluentDetailView.Get();
      ContentDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Read);
      return (ConfigElement) detailFormViewElement;
    }

    private static ConfigElement DefineContentBackendDuplicateView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView("ContentBackendDuplicate").SetTitle("CreateNewItem").LocalizeUsing<ContentResources>().SetServiceBaseUrl("~/Sitefinity/Services/Content/ContentItemService.svc/").SetAlternativeTitle("CreateNewItem");
      DetailFormViewElement detailView = fluentDetailView.Get();
      ContentDefinitions.CreateBackendSections(fluentDetailView);
      ContentDefinitions.CreateBackendFormToolbar(detailView, false);
      return (ConfigElement) detailView;
    }

    private static ConfigElement DefineContentBackendVersionPreviewView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> localization = new Dictionary<string, string>()
      {
        {
          "ItemVersionOfClientTemplate",
          Res.Get<VersionResources>().ItemVersionOfClientTemplate
        },
        {
          "PreviouslyPublished",
          Res.Get<VersionResources>().PreviouslyPublishedBrackets
        },
        {
          "CannotDeleteLastPublishedVersion",
          Res.Get<VersionResources>().CannotDeleteLastPublishedVersion
        }
      };
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Versioning.Web.UI.Scripts.VersionHistoryExtender.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView("ContentBackendVersionPreview").SetTitle("EditItem").SetDisplayMode(FieldDisplayMode.Read).LocalizeUsing<ContentResources>().SetExternalClientScripts(extenalClientScripts).ShowNavigation().SetServiceBaseUrl("~/Sitefinity/Services/Content/ContentItemService.svc/").SetLocalization(localization);
      DetailFormViewElement detailView = fluentDetailView.Get();
      ContentDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Read);
      DefinitionsHelper.CreateHistoryPreviewToolbar(detailView, typeof (ContentResources).Name);
      return (ConfigElement) detailView;
    }

    private static ConfigElement DefineContentBackendVersionComparisonView(
      ContentViewControlElement backendContentView)
    {
      ComparisonViewElement comparisonViewElement1 = new ComparisonViewElement((ConfigElement) backendContentView.ViewsConfig);
      comparisonViewElement1.Title = "VersionComparison";
      comparisonViewElement1.ViewName = "NewsBackendVersionComparisonView";
      comparisonViewElement1.ViewType = typeof (Telerik.Sitefinity.Versioning.Web.UI.Views.VersionComparisonView);
      comparisonViewElement1.DisplayMode = FieldDisplayMode.Read;
      comparisonViewElement1.ResourceClassId = typeof (ContentResources).Name;
      ComparisonViewElement comparisonViewElement2 = comparisonViewElement1;
      ConfigElementDictionary<string, ComparisonFieldElement> fields1 = comparisonViewElement2.Fields;
      ComparisonFieldElement element1 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element1.FieldName = "Title";
      element1.Title = "Title";
      element1.ResourceClassId = typeof (ContentResources).Name;
      fields1.Add(element1);
      ConfigElementDictionary<string, ComparisonFieldElement> fields2 = comparisonViewElement2.Fields;
      ComparisonFieldElement element2 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element2.FieldName = "Content";
      element2.Title = "Content";
      element2.ResourceClassId = typeof (ContentResources).Name;
      fields2.Add(element2);
      ConfigElementDictionary<string, ComparisonFieldElement> fields3 = comparisonViewElement2.Fields;
      ComparisonFieldElement element3 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element3.FieldName = "Category";
      element3.Title = "Category";
      element3.ResourceClassId = typeof (ContentResources).Name;
      fields3.Add(element3);
      ConfigElementDictionary<string, ComparisonFieldElement> fields4 = comparisonViewElement2.Fields;
      ComparisonFieldElement element4 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element4.FieldName = "Tags";
      element4.Title = "Tags";
      element4.ResourceClassId = typeof (ContentResources).Name;
      fields4.Add(element4);
      ConfigElementDictionary<string, ComparisonFieldElement> fields5 = comparisonViewElement2.Fields;
      ComparisonFieldElement element5 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element5.FieldName = "UrlName";
      element5.Title = "UrlName";
      element5.ResourceClassId = typeof (ContentResources).Name;
      fields5.Add(element5);
      return (ConfigElement) comparisonViewElement2;
    }

    /// <summary>Defines the generic content frontend view.</summary>
    /// <param name="parent">The parent.</param>
    /// <returns>Default config</returns>
    internal static ContentViewControlElement DefineGenericContentFrontendView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentFacade = App.WorkWith().Module("GenericContent").DefineContainer(parent, ContentDefinitions.FrontendDefinitionName, typeof (ContentItem));
      ContentViewControlElement frontendView = fluentFacade.Get();
      string resourceClassId = typeof (ContentResources).Name;
      frontendView.ViewsConfig.AddLazy(ContentDefinitions.FrontendMasterViewName, (Func<ContentViewDefinitionElement>) (() =>
      {
        return (ContentViewDefinitionElement) new MasterGridViewElement((ConfigElement) frontendView.ViewsConfig)
        {
          Title = "MasterView",
          ViewName = ContentDefinitions.FrontendMasterViewName,
          ViewType = typeof (MasterView),
          DisplayMode = FieldDisplayMode.Read,
          ResourceClassId = resourceClassId,
          FilterExpression = "Visible = true AND Status = Live",
          AllowPaging = new bool?(true),
          ItemsPerPage = new int?(20)
        };
      }));
      frontendView.ViewsConfig.AddLazy(ContentDefinitions.FrontendDetailViewName, (Func<ContentViewDefinitionElement>) (() =>
      {
        return (ContentViewDefinitionElement) new DetailFormViewElement((ConfigElement) frontendView.ViewsConfig)
        {
          Title = "DetailsView",
          ViewName = ContentDefinitions.FrontendDetailViewName,
          ViewType = typeof (Telerik.Sitefinity.Modules.GenericContent.Web.UI.Views.DetailsView),
          ShowSections = new bool?(true),
          DisplayMode = FieldDisplayMode.Read,
          ResourceClassId = resourceClassId
        };
      }));
      ContentDefinitions.CreateDialogs<ContentViewControlDefinitionFacade>((IDialogsSupportableFacade<ContentViewControlDefinitionFacade>) fluentFacade);
      return frontendView;
    }

    /// <summary>Creates the backend sections.</summary>
    private static void CreateBackendSections(DetailViewDefinitionFacade fluentDetailView) => ContentDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Write);

    /// <summary>Creates the backend sections.</summary>
    /// <param name="detailView">The detail view.</param>
    /// <param name="displayMode">The display mode.</param>
    private static void CreateBackendSections(
      DetailViewDefinitionFacade fluentDetailView,
      FieldDisplayMode displayMode)
    {
      if (displayMode == FieldDisplayMode.Read)
        fluentDetailView.AddReadOnlySection("Sidebar").SetWrapperTag(HtmlTextWriterTag.Div).AddVersionNoteControl();
      if (fluentDetailView.Get().ViewName == ContentDefinitions.BackendEditDetailsViewName)
        fluentDetailView.AddSection("toolbarSection").AddLanguageListField();
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade = fluentDetailView.AddFirstSection("MainSection");
      definitionFacade.AddLocalizedTextField("Title").SetId("urlName").SetTitle("Title").SetCssClass("sfTitleField").AddValidation().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").Done();
      if (fluentDetailView.Get().ViewName == ContentDefinitions.BackendEditDetailsViewName || fluentDetailView.Get().ViewName == ContentDefinitions.BackendInsertDetailsViewName)
        definitionFacade.AddLanguageChoiceFieldAndContinue("AvailableLanguages");
      definitionFacade.AddLocalizedHtmlField("Content").Done();
      ContentViewSectionElement viewSectionElement = fluentDetailView.AddExpandableSection("TaxonSection").SetTitle("CategoriesAndTags").Get();
      HierarchicalTaxonFieldDefinitionElement element1 = DefinitionTemplates.CategoriesFieldWriteMode((ConfigElement) viewSectionElement.Fields);
      element1.DisplayMode = new FieldDisplayMode?(displayMode);
      viewSectionElement.Fields.Add((FieldDefinitionElement) element1);
      FlatTaxonFieldDefinitionElement element2 = DefinitionTemplates.TagsFieldWriteMode((ConfigElement) viewSectionElement.Fields);
      element2.DisplayMode = new FieldDisplayMode?(displayMode);
      element2.CssClass = "sfFormSeparator";
      element2.ExpandableDefinition.Expanded = new bool?(true);
      element2.Description = "TagsFieldInstructions";
      element2.ResourceClassId = typeof (TaxonomyResources).Name;
      viewSectionElement.Fields.Add((FieldDefinitionElement) element2);
    }

    private static void CreateDialogs<TParentFacade>(
      IDialogsSupportableFacade<TParentFacade> fluentFacade)
      where TParentFacade : class
    {
      fluentFacade.AddInsertDialog(ContentDefinitions.BackendInsertDetailsViewName, ContentDefinitions.BackendDefinitionName, Res.Get<ContentResources>().BackToItems);
      fluentFacade.AddEditDialog(ContentDefinitions.BackendEditDetailsViewName, ContentDefinitions.BackendDefinitionName, Res.Get<ContentResources>().BackToItems);
      fluentFacade.AddInsertDialog("ContentBackendDuplicate", ContentDefinitions.BackendDefinitionName, Res.Get<ContentResources>().BackToItems, "{{Id}}", (Type) null, "duplicate").AddParameters("&SuppressBackToButtonLabelModify=true");
      fluentFacade.AddHistoryComparisonDialog("NewsBackendVersionComparisonView", ContentDefinitions.BackendDefinitionName, Res.Get<ContentResources>().BackToItems);
      fluentFacade.AddHistoryGridDialog("NewsBackendVersionComparisonView", ContentDefinitions.BackendDefinitionName, Res.Get<ContentResources>().BackToItems);
      fluentFacade.AddHistoryPreviewDialog("ContentBackendVersionPreview", ContentDefinitions.BackendDefinitionName, Res.Get<Labels>().BackToRevisionHistory);
      fluentFacade.AddPreviewDialog("ContentBackendPreview", ContentDefinitions.BackendDefinitionName, Res.Get<ContentResources>().BackToItems);
      fluentFacade.AddDialog<ContentPagesDialog>("viewContentPages").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).DisplayTitleBar().MakeModal().ReloadOnShow();
      fluentFacade.AddPermissionsDialog(Res.Get<ContentResources>().BackToItems, Res.Get<ContentResources>().PermissionsForContentBlocks);
    }

    private static void CreateBackendFormToolbar(
      DetailFormViewElement detailView,
      bool isCreateMode)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) detailView.Toolbar.Sections)
      {
        Name = "BackendForm",
        WrapperTagKey = HtmlTextWriterTag.Div,
        CssClass = "sfWorkflowMenuWrp"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "SaveChangesWidgetElement";
      element2.ButtonType = CommandButtonType.Save;
      element2.CommandName = "save";
      element2.Text = isCreateMode ? "CreateThisItem" : "SaveChanges";
      element2.ResourceClassId = typeof (ContentResources).Name;
      element2.WrapperTagKey = HtmlTextWriterTag.Span;
      element2.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "CancelWidgetElement";
      element3.ButtonType = CommandButtonType.Cancel;
      element3.CommandName = "cancel";
      element3.Text = "BackToItems";
      element3.ResourceClassId = typeof (ContentResources).Name;
      element3.WrapperTagKey = HtmlTextWriterTag.Span;
      element3.WidgetType = typeof (CommandWidget);
      items2.Add((WidgetElement) element3);
      detailView.Toolbar.Sections.Add(element1);
    }

    public static void FillActionMenuItems(
      ConfigElementList<WidgetElement> menuItems,
      ConfigElement parent,
      string resourceClassId)
    {
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "ViewProperties", HtmlTextWriterTag.Li, "viewProperties", "ViewProperties", resourceClassId));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Delete", HtmlTextWriterTag.Li, "delete", "Delete", resourceClassId, "sfDeleteItm"));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Publish", HtmlTextWriterTag.Li, "publish", "Publish", resourceClassId, "sfPublishItm"));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Duplicate", HtmlTextWriterTag.Li, "duplicate", "Duplicate", resourceClassId, "sfDuplicateItm"));
      menuItems.Add(DefinitionsHelper.CreateActionMenuSeparator((ConfigElement) menuItems, "Separator", HtmlTextWriterTag.Li, "sfSeparator", "Edit", resourceClassId));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Content", HtmlTextWriterTag.Li, "edit", "Content", resourceClassId));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Permissions", HtmlTextWriterTag.Li, "permissions", "Permissions", resourceClassId));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "History", HtmlTextWriterTag.Li, "historygrid", "HistoryMenuItemTitle", "VersionResources"));
    }
  }
}
