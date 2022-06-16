// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormsDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Fluent.Definitions.Fields;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.Components;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>
  /// This is a static class used to initialize the properties for all ContentView control views
  /// of supplied by default for the forms module.
  /// </summary>
  internal static class FormsDefinitions
  {
    public const string BackendDefinitionName = "FormsBackend";
    public const string BackendListViewName = "FormsBackendList";
    public const string BackendEditViewName = "FormsBackendEdit";
    public const string BackendInsertViewName = "FormsBackendInsert";
    public const string BackendPreviewViewName = "FormsBackendPreview";
    public const string BackendDuplicateViewName = "FormsBackendDuplicate";
    public const string BackendListDetailViewName = "FormsBackendListDetail";
    public const string FrontendDefinitionName = "FormsFrontend";
    public const string FrontendListViewName = "FormsFrontendList";
    public const string FrontendDetailViewName = "FormsFrontendDetails";
    public const string FormEntriesToolbar = "FormEntriesToolbar";
    public const string FormEntriesSorting = "FormEntriesSorting";
    public const string ModuleName = "Forms";
    public static readonly string ResourceClassId = typeof (FormsResources).Name;
    internal static readonly string FormAttributesPropertyName = "Attributes";
    internal static readonly string DataMappingPropertyName = "ConnectorsDataMapping";
    internal static readonly string ExpandableDataMappingPropertyName = "ExpandableDataMapping";
    internal static readonly string ConnectorsSettingsSection = nameof (ConnectorsSettingsSection);
    /// <summary>
    /// A CSS class used as a selector in order to show/hide fields in MVCOnly/WebFormsOnly Framework mode of the objects of the FormDescrition type.
    /// </summary>
    internal static readonly string MVCOnlyFrameworkFieldsCssClass = "MVCOnlyFrameworkFieldsCss";

    static FormsDefinitions() => SystemManager.GetApplicationModule("Forms");

    /// <summary>
    /// Defines the ContentView control for Forms on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineFormsBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Forms").DefineContainer(parent, "FormsBackend", typeof (FormDescription)).DoNotUseWorkflow();
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      MasterViewDefinitionFacade definitionFacade = fluentContentView.AddMasterView("FormsBackendList").LocalizeUsing<FormsResources>().SetTitle("FormsTitle").SetServiceBaseUrl("~/Sitefinity/Services/Forms/FormsService.svc/").SetPublishedFilter("Status = Live OR (Status = Master AND ContentState = \"PUBLISHED\")").SetDraftFilter("Status = Master").SetDeleteSingleConfirmationMessage("FormIsAboutToBeDeleted").SetDeleteMultipleConfirmationMessage("FormsAreAboutToBeDeleted");
      MasterGridViewElement element1 = definitionFacade.Get();
      WidgetBarSectionElement element2 = new WidgetBarSectionElement((ConfigElement) element1.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element2.Items);
      commandWidgetElement1.Name = "CreateFormsWidget";
      commandWidgetElement1.ButtonType = CommandButtonType.Create;
      commandWidgetElement1.CommandName = "create";
      commandWidgetElement1.Text = "CreateItem";
      commandWidgetElement1.ResourceClassId = typeof (FormsResources).Name;
      commandWidgetElement1.CssClass = "sfMainAction";
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element3 = commandWidgetElement1;
      element2.Items.Add((WidgetElement) element3);
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element2.Items);
      commandWidgetElement2.Name = "DeleteFormsWidget";
      commandWidgetElement2.ButtonType = CommandButtonType.Standard;
      commandWidgetElement2.CommandName = "groupDelete";
      commandWidgetElement2.Text = "Delete";
      commandWidgetElement2.ResourceClassId = typeof (FormsResources).Name;
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      commandWidgetElement2.CssClass = "sfGroupBtn";
      CommandWidgetElement element4 = commandWidgetElement2;
      element2.Items.Add((WidgetElement) element4);
      ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element2.Items);
      menuWidgetElement.Name = "MoreActionsFormsWidget";
      menuWidgetElement.Text = "MoreActions";
      menuWidgetElement.ResourceClassId = typeof (FormsResources).Name;
      menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
      ActionMenuWidgetElement element5 = menuWidgetElement;
      ConfigElementList<WidgetElement> menuItems1 = element5.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element6.Name = "Publish";
      element6.Text = "Publish";
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "batchPublishPage";
      element6.WidgetType = typeof (CommandWidget);
      element6.ResourceClassId = typeof (Labels).Name;
      element6.CssClass = "sfPublishItm";
      menuItems1.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> menuItems2 = element5.MenuItems;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element7.Name = "Unpublish";
      element7.Text = "Unpublish";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "batchUnpublishPage";
      element7.WidgetType = typeof (CommandWidget);
      element7.ResourceClassId = typeof (Labels).Name;
      element7.CssClass = "sfUnpublishItm";
      menuItems2.Add((WidgetElement) element7);
      element2.Items.Add((WidgetElement) element5);
      element2.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element2.Items, typeof (FormsDefinitions)));
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Form), false, element1.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Form), true, element1.Section);
      DynamicCommandWidgetElement commandWidgetElement3 = new DynamicCommandWidgetElement((ConfigElement) element2.Items);
      commandWidgetElement3.Name = "EditCustomSorting";
      commandWidgetElement3.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement3.HeaderText = "Sort";
      commandWidgetElement3.PageSize = 10;
      commandWidgetElement3.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement3.WidgetType = typeof (SortWidget);
      commandWidgetElement3.ResourceClassId = FormsDefinitions.ResourceClassId;
      commandWidgetElement3.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement3.ContentType = typeof (Form);
      DynamicCommandWidgetElement element8 = commandWidgetElement3;
      element2.Items.Add((WidgetElement) element8);
      foreach (SortingExpressionElement expressionElement in expressionSettings1)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element8.Items, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element8.Items.Add(dynamicItemElement);
        element8.DesignTimeItems.Add(dynamicItemElement.GetKey());
      }
      foreach (SortingExpressionElement expressionElement in expressionSettings2)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element8.CustomItems, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element8.CustomItems.Add(dynamicItemElement);
      }
      element1.ToolbarConfig.Sections.Add(element2);
      LocalizationWidgetBarSectionElement barSectionElement1 = new LocalizationWidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections);
      barSectionElement1.Name = "Languages";
      barSectionElement1.Title = "Languages";
      barSectionElement1.ResourceClassId = typeof (LocalizationResources).Name;
      barSectionElement1.CssClass = "sfFirst sfSeparator sfLangSelector";
      barSectionElement1.WrapperTagId = "languagesSection";
      LocalizationWidgetBarSectionElement element9 = barSectionElement1;
      ConfigElementList<WidgetElement> items1 = element9.Items;
      LanguagesDropDownListWidgetElement element10 = new LanguagesDropDownListWidgetElement((ConfigElement) element9.Items);
      element10.Name = "Languages";
      element10.Text = "Languages";
      element10.ResourceClassId = typeof (LocalizationResources).Name;
      element10.CssClass = "";
      element10.WidgetType = typeof (LanguagesDropDownListWidget);
      element10.IsSeparator = false;
      element10.LanguageSource = LanguageSource.Frontend;
      element10.AddAllLanguagesOption = false;
      element10.CommandName = "changeLanguage";
      items1.Add((WidgetElement) element10);
      WidgetBarSectionElement element11 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "Filter",
        WrapperTagId = "FilterFormsSection",
        Title = "FilterForms",
        ResourceClassId = typeof (FormsResources).Name,
        CssClass = "sfFirst sfWidgetsList sfSeparator sfModules"
      };
      ConfigElementList<WidgetElement> items2 = element11.Items;
      CommandWidgetElement element12 = new CommandWidgetElement((ConfigElement) element11.Items);
      element12.Name = "AllForms";
      element12.CommandName = "showAllItems";
      element12.ButtonType = CommandButtonType.SimpleLinkButton;
      element12.ButtonCssClass = "sfSel";
      element12.Text = "AllForms";
      element12.ResourceClassId = typeof (FormsResources).Name;
      element12.CssClass = "";
      element12.WidgetType = typeof (CommandWidget);
      element12.IsSeparator = false;
      items2.Add((WidgetElement) element12);
      ConfigElementList<WidgetElement> items3 = element11.Items;
      CommandWidgetElement element13 = new CommandWidgetElement((ConfigElement) element11.Items);
      element13.Name = "MyForms";
      element13.CommandName = "showMyItems";
      element13.ButtonType = CommandButtonType.SimpleLinkButton;
      element13.Text = "MyForms";
      element13.ResourceClassId = typeof (FormsResources).Name;
      element13.CssClass = "";
      element13.WidgetType = typeof (CommandWidget);
      element13.IsSeparator = false;
      items3.Add((WidgetElement) element13);
      ConfigElementList<WidgetElement> items4 = element11.Items;
      CommandWidgetElement element14 = new CommandWidgetElement((ConfigElement) element11.Items);
      element14.Name = "Published";
      element14.CommandName = "showPublishedItems";
      element14.ButtonType = CommandButtonType.SimpleLinkButton;
      element14.Text = "Published";
      element14.ResourceClassId = typeof (Labels).Name;
      element14.CssClass = "";
      element14.WidgetType = typeof (CommandWidget);
      element14.IsSeparator = false;
      items4.Add((WidgetElement) element14);
      ConfigElementList<WidgetElement> items5 = element11.Items;
      CommandWidgetElement element15 = new CommandWidgetElement((ConfigElement) element11.Items);
      element15.Name = "Drafts";
      element15.CommandName = "showMasterItems";
      element15.ButtonType = CommandButtonType.SimpleLinkButton;
      element15.Text = "Drafts";
      element15.ResourceClassId = typeof (Labels).Name;
      element15.CssClass = "";
      element15.WidgetType = typeof (CommandWidget);
      element15.IsSeparator = false;
      items5.Add((WidgetElement) element15);
      DefinitionsHelper.AppendStatusFilters((ConfigElementCollection) element11.Items);
      ConfigElementList<WidgetElement> items6 = element11.Items;
      MultisiteCommandWidgetElement element16 = new MultisiteCommandWidgetElement((ConfigElement) element11.Items);
      element16.Name = "NotSharedWithAnySite";
      element16.CommandName = "showNotShared";
      element16.ButtonType = CommandButtonType.SimpleLinkButton;
      element16.Text = "NotSharedWithAnySite";
      element16.ResourceClassId = typeof (MultisiteResources).Name;
      element16.CssClass = "";
      element16.WidgetType = typeof (CommandWidget);
      element16.IsSeparator = false;
      element16.ModuleName = "MultisiteInternal";
      items6.Add((WidgetElement) element16);
      WidgetBarSectionElement element17 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "ManageAlso",
        Title = "ManageAlso",
        ResourceClassId = typeof (FormsResources).Name,
        CssClass = "sfWidgetsList sfSeparator",
        WrapperTagId = "manageAlsoSection"
      };
      if (SystemManager.IsModuleEnabled("Comments"))
      {
        CommandWidgetElement commandWidgetElement4 = new CommandWidgetElement((ConfigElement) element17.Items);
        commandWidgetElement4.Name = "FormsComments";
        commandWidgetElement4.CommandName = "comments";
        commandWidgetElement4.ButtonType = CommandButtonType.SimpleLinkButton;
        commandWidgetElement4.Text = "CommentsForForms";
        commandWidgetElement4.ResourceClassId = typeof (FormsResources).Name;
        commandWidgetElement4.CssClass = "sfComments";
        commandWidgetElement4.WidgetType = typeof (CommandWidget);
        commandWidgetElement4.IsSeparator = false;
        CommandWidgetElement element18 = commandWidgetElement4;
        element17.Items.Add((WidgetElement) element18);
      }
      WidgetBarSectionElement element19 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "Settings",
        Title = "Settings",
        ResourceClassId = typeof (FormsResources).Name,
        CssClass = "sfWidgetsList sfSettings"
      };
      CommandWidgetElement commandWidgetElement5 = new CommandWidgetElement((ConfigElement) element19.Items);
      commandWidgetElement5.Name = "FormsPermissions";
      commandWidgetElement5.CommandName = "permissions";
      commandWidgetElement5.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement5.Text = "PermissionsForForms";
      commandWidgetElement5.ResourceClassId = typeof (FormsResources).Name;
      commandWidgetElement5.WidgetType = typeof (CommandWidget);
      commandWidgetElement5.IsSeparator = false;
      CommandWidgetElement element20 = commandWidgetElement5;
      element19.Items.Add((WidgetElement) element20);
      element1.SidebarConfig.ResourceClassId = typeof (FormsResources).Name;
      element1.SidebarConfig.Title = "ManageForms";
      element1.SidebarConfig.Sections.Add((WidgetBarSectionElement) element9);
      element1.SidebarConfig.Sections.Add(element11);
      if (element17.Items.Count == 0)
        element17.Visible = new bool?(false);
      element1.SidebarConfig.Sections.Add(element17);
      element1.SidebarConfig.Sections.Add(element19);
      LocalizationWidgetBarSectionElement barSectionElement2 = new LocalizationWidgetBarSectionElement((ConfigElement) element1.ContextBarConfig.Sections);
      barSectionElement2.Name = "contextBar";
      barSectionElement2.WrapperTagKey = HtmlTextWriterTag.Div;
      barSectionElement2.CssClass = "sfContextWidgetWrp";
      barSectionElement2.MinLanguagesCountTreshold = new int?(6);
      LocalizationWidgetBarSectionElement element21 = barSectionElement2;
      ConfigElementList<WidgetElement> items7 = element21.Items;
      CommandWidgetElement element22 = new CommandWidgetElement((ConfigElement) element21.Items);
      element22.Name = "ShowMoreTranslations";
      element22.CommandName = "showMoreTranslations";
      element22.ButtonType = CommandButtonType.SimpleLinkButton;
      element22.Text = "ShowAllTranslations";
      element22.ResourceClassId = typeof (LocalizationResources).Name;
      element22.WidgetType = typeof (CommandWidget);
      element22.IsSeparator = false;
      element22.CssClass = "sfShowHideLangVersions";
      element22.WrapperTagKey = HtmlTextWriterTag.Div;
      items7.Add((WidgetElement) element22);
      ConfigElementList<WidgetElement> items8 = element21.Items;
      CommandWidgetElement element23 = new CommandWidgetElement((ConfigElement) element21.Items);
      element23.Name = "HideMoreTranslations";
      element23.CommandName = "hideMoreTranslations";
      element23.ButtonType = CommandButtonType.SimpleLinkButton;
      element23.Text = "ShowBasicTranslationsOnly";
      element23.ResourceClassId = typeof (LocalizationResources).Name;
      element23.WidgetType = typeof (CommandWidget);
      element23.IsSeparator = false;
      element23.CssClass = "sfDisplayNone sfShowHideLangVersions";
      element23.WrapperTagKey = HtmlTextWriterTag.Div;
      items8.Add((WidgetElement) element23);
      element1.ContextBarConfig.Sections.Add((WidgetBarSectionElement) element21);
      GridViewModeElement gridViewModeElement1 = new GridViewModeElement((ConfigElement) element1.ViewModesConfig);
      gridViewModeElement1.Name = "Grid";
      GridViewModeElement gridViewModeElement2 = gridViewModeElement1;
      element1.ViewModesConfig.Add((ViewModeElement) gridViewModeElement2);
      DataColumnElement dataColumnElement = new DataColumnElement((ConfigElement) gridViewModeElement2.ColumnsConfig);
      dataColumnElement.Name = "Title";
      dataColumnElement.HeaderText = "Title";
      dataColumnElement.ResourceClassId = typeof (Labels).Name;
      dataColumnElement.HeaderCssClass = "sfTitleCol";
      dataColumnElement.ItemCssClass = "sfTitleCol";
      dataColumnElement.ClientTemplate = "<a sys:href=\"javascript:void(0);\" sys:class=\"{{ (IsEditable ? 'sf_binderCommand_editFormContent sfItemTitle' : 'sf_binderCommand_void sfItemTitle sfDisabled') + (AdditionalStatus ? ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() : '') + ' sf' + Status.toLowerCase() }}\">\r\n                <strong>{{Title.htmlEncode()}}</strong>\r\n                <span sys:if='AdditionalStatus' class='sfStatusAdditional'>{{AdditionalStatus.Text}}</span>\r\n                <span class='sfStatusLocation'>\r\n                    <span sys:if='AdditionalStatus' class='sfSep'>| </span>\r\n\t                {{StatusText}}\r\n                </span>\r\n                </a>";
      DataColumnElement element24 = dataColumnElement;
      gridViewModeElement2.ColumnsConfig.Add((ColumnElement) element24);
      DynamicColumnElement dynamicColumnElement1 = new DynamicColumnElement((ConfigElement) gridViewModeElement2.ColumnsConfig);
      dynamicColumnElement1.Name = "Translations";
      dynamicColumnElement1.HeaderText = "Translations";
      dynamicColumnElement1.ResourceClassId = typeof (LocalizationResources).Name;
      dynamicColumnElement1.DynamicMarkupGenerator = typeof (LanguagesColumnMarkupGenerator);
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
      gridViewModeElement2.ColumnsConfig.Add((ColumnElement) dynamicColumnElement2);
      FormsDefinitions.AddViewColumn(gridViewModeElement2);
      FormsDefinitions.AddActionsMenuColumn(gridViewModeElement2);
      FormsDefinitions.AddOwnerColumn(gridViewModeElement2);
      FormsDefinitions.AddLastModifiedColumn(gridViewModeElement2);
      DecisionScreenElement element25 = new DecisionScreenElement((ConfigElement) element1.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "NoFormsHaveBeenCreatedYet",
        MessageText = "NoFormsHaveBeenCreatedYet",
        ResourceClassId = typeof (FormsResources).Name
      };
      ConfigElementList<CommandWidgetElement> actions = element25.Actions;
      CommandWidgetElement element26 = new CommandWidgetElement((ConfigElement) element25.Actions);
      element26.Name = "Create";
      element26.ButtonType = CommandButtonType.Create;
      element26.CommandName = "create";
      element26.Text = "CreateForm";
      element26.ResourceClassId = typeof (FormsResources).Name;
      element26.CssClass = "sfCreateItem";
      actions.Add(element26);
      element1.DecisionScreensConfig.Add(element25);
      element1.ExternalClientScripts = new Dictionary<string, string>()
      {
        {
          "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormsMasterGridViewExtensions.js, Telerik.Sitefinity",
          "OnMasterViewLoaded"
        }
      };
      definitionFacade.AddInsertDialog("FormsBackendInsert").Done().AddInsertDialog("FormsBackendDuplicate", "", "", "", (Type) null, "duplicate").AddParameters("&SuppressBackToButtonLabelModify=true");
      definitionFacade.AddEditDialog("FormsBackendEdit").Done().AddPreviewDialog("FormsBackendPreview").Done().AddPermissionsDialog(Res.Get<FormsResources>().BackToForms, Res.Get<FormsResources>().PermissionsForForms).Done().AddDialog<SiteSelectorDialog>("shareWith").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters(string.Format("?ItemId={0}", (object) "{{Id}}")).ReloadOnShow().Done();
      string parameters = "?ShowUnlock=true&LockedByUsername={{LockedByUsername}}&ItemName={{Title}}&Title=Forms&ViewUrl={{ViewUrl}}&UnlockServiceUrl=" + RouteHelper.ResolveUrl("~/Sitefinity/Services/Pages/ZoneEditorService.svc/Form/UnlockForm/{{Id}}", UrlResolveOptions.Rooted);
      definitionFacade.AddDialog<LockingDialog>("unlockForm").SetParameters(parameters).MakeFullScreen().ReloadOnShow();
      element1.LinksConfig.Add(new LinkElement((ConfigElement) element1.LinksConfig)
      {
        Name = "viewComments",
        CommandName = "comments",
        NavigateUrl = RouteHelper.CreateNodeReference(CommentsModule.CommentsPageId) + "?threadType=" + typeof (Form).FullName
      });
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      DetailFormViewElement element27 = FormsDefinitions.DefineBackendDetailsView(fluentContentView, true, "FormsBackendInsert", FieldDisplayMode.Write);
      DetailFormViewElement element28 = FormsDefinitions.DefineBackendDetailsView(fluentContentView, false, "FormsBackendEdit", FieldDisplayMode.Write);
      DetailFormViewElement element29 = FormsDefinitions.DefineBackendDetailsView(fluentContentView, true, "FormsBackendDuplicate", FieldDisplayMode.Write, true);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element27);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element28);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element29);
      ContentViewMasterDetailElement masterDetailElement1 = new ContentViewMasterDetailElement((ConfigElement) viewControlElement.ViewsConfig);
      masterDetailElement1.ViewName = "FormsBackendListDetail";
      masterDetailElement1.Title = "";
      masterDetailElement1.ViewType = typeof (FormsMasterDetailView);
      masterDetailElement1.DisplayMode = FieldDisplayMode.Write;
      masterDetailElement1.ResourceClassId = typeof (FormsResources).Name;
      ContentViewMasterDetailElement masterDetailElement2 = masterDetailElement1;
      MasterGridViewElement masterGridViewElement = FormsDefinitions.DefineEntriesMasterView(masterDetailElement2);
      FormsDetailViewElement detailViewElement1 = new FormsDetailViewElement((ConfigElement) masterDetailElement2);
      detailViewElement1.Title = "";
      detailViewElement1.ViewName = "FormsBackendEdit";
      detailViewElement1.WebServiceBaseUrl = "~/Sitefinity/Services/Forms/FormsService.svc/entry/";
      detailViewElement1.ViewType = typeof (FormsDetailView);
      detailViewElement1.DisplayMode = FieldDisplayMode.Write;
      detailViewElement1.ResourceClassId = typeof (FormsResources).Name;
      FormsDetailViewElement detailViewElement2 = detailViewElement1;
      masterDetailElement2.MasterDefinitionConfig = (ContentViewMasterElement) masterGridViewElement;
      masterDetailElement2.DetailDefinitionConfig = (ContentViewDetailElement) detailViewElement2;
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) masterDetailElement2);
      return viewControlElement;
    }

    private static MasterGridViewElement DefineEntriesMasterView(
      ContentViewMasterDetailElement formsResponsesMasterDetailView)
    {
      MasterGridViewElement masterGridViewElement = new MasterGridViewElement((ConfigElement) formsResponsesMasterDetailView);
      masterGridViewElement.ViewName = "FormsBackendList";
      masterGridViewElement.WebServiceBaseUrl = "~/Sitefinity/Services/Forms/FormsService.svc/entries/";
      masterGridViewElement.ViewType = typeof (MasterGridView);
      masterGridViewElement.AllowPaging = new bool?(true);
      masterGridViewElement.DisplayMode = FieldDisplayMode.Read;
      masterGridViewElement.ItemsPerPage = new int?(50);
      masterGridViewElement.ResourceClassId = typeof (FormsResources).Name;
      masterGridViewElement.SortExpression = "SubmittedOn ASC";
      masterGridViewElement.Title = "FormsTitle";
      MasterGridViewElement entriesBackendMaster = masterGridViewElement;
      GridViewModeElement gridViewModeElement1 = new GridViewModeElement((ConfigElement) entriesBackendMaster.ViewModesConfig);
      gridViewModeElement1.Name = "Grid";
      GridViewModeElement gridViewModeElement2 = gridViewModeElement1;
      entriesBackendMaster.ViewModesConfig.Add((ViewModeElement) gridViewModeElement2);
      DialogElement dialogElement1 = DefinitionsHelper.CreateDialogElement((ConfigElement) entriesBackendMaster.DialogsConfig, "create", typeof (FormEntryEditDialog).Name, (string) null);
      entriesBackendMaster.DialogsConfig.Add(dialogElement1);
      DialogElement dialogElement2 = DefinitionsHelper.CreateDialogElement((ConfigElement) entriesBackendMaster.DialogsConfig, "edit", typeof (FormEntryEditDialog).Name, (string) null);
      entriesBackendMaster.DialogsConfig.Add(dialogElement2);
      DecisionScreenElement element1 = new DecisionScreenElement((ConfigElement) entriesBackendMaster.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "NoResponsesHaveBeenSubmittedYet",
        MessageText = "NoResponsesHaveBeenSubmittedYet",
        ResourceClassId = typeof (FormsResources).Name
      };
      ConfigElementList<CommandWidgetElement> actions = element1.Actions;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Actions);
      element2.Name = "Create";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "create";
      element2.Text = "CreateResponse";
      element2.ResourceClassId = typeof (FormsResources).Name;
      element2.CssClass = "sfCreateItem";
      actions.Add(element2);
      entriesBackendMaster.DecisionScreensConfig.Add(element1);
      WidgetBarSectionElement element3 = new WidgetBarSectionElement((ConfigElement) entriesBackendMaster.ToolbarConfig.Sections)
      {
        Name = "FormEntriesToolbar"
      };
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (FormEntry), false, entriesBackendMaster.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (FormEntry), true, entriesBackendMaster.Section);
      FormsResources formsResources = Res.Get<FormsResources>();
      DynamicCommandWidgetElement commandWidgetElement = new DynamicCommandWidgetElement((ConfigElement) element3.Items);
      commandWidgetElement.Name = "FormEntriesSorting";
      commandWidgetElement.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement.HeaderText = formsResources.SortEntries;
      commandWidgetElement.PageSize = 10;
      commandWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement.WidgetType = typeof (SortWidget);
      commandWidgetElement.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement.ContentType = typeof (FormEntry);
      DynamicCommandWidgetElement element4 = commandWidgetElement;
      element3.Items.Add((WidgetElement) element4);
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
      entriesBackendMaster.ToolbarConfig.Sections.Add(element3);
      FormsDefinitions.DefineEntriesToolbar(entriesBackendMaster);
      FormsDefinitions.AddFormEntryColumns(gridViewModeElement2);
      return entriesBackendMaster;
    }

    /// <summary>
    /// Defines the ContentView control for Forms on the frontend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineFormsFrontendContentView(
      ConfigElement parent)
    {
      return App.WorkWith().Module("Forms").DefineContainer(parent, "FormsFrontend", typeof (FormDraft)).Get();
    }

    private static DetailFormViewElement DefineBackendDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      bool isCreateMode,
      string viewName,
      FieldDisplayMode displayMode,
      bool isDuplicateMode = false)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormsDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle(isDuplicateMode ? "DuplicateForm" : (isCreateMode ? "CreateForm" : "EditForm")).HideTopToolbar().LocalizeUsing<FormsResources>().SetServiceBaseUrl("~/Sitefinity/Services/Forms/FormsService.svc/").SetExternalClientScripts(extenalClientScripts).DoNotUseWorkflow();
      if (!isCreateMode && !isDuplicateMode)
        definitionFacade1.SetAlternativeTitle("CreateForm");
      DetailFormViewElement detailFormViewElement = definitionFacade1.Get();
      string dataFieldName = isDuplicateMode ? "DuplicateTitle" : "Title";
      string fieldName = isDuplicateMode ? "DuplicateName" : "Name";
      if (detailFormViewElement.ViewName == "FormsBackendEdit")
        definitionFacade1.AddSection("toolbarSection").AddLanguageListField();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade3 = definitionFacade2.AddTextField("Title").SetId("titleFieldControl").SetDataFieldName(dataFieldName).SetCssClass("sfTitleField").SetExample("NameExample").SetTitle("Title").AddValidation().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").Done();
      definitionFacade2.AddLanguageChoiceFieldAndContinue("AvailableLanguages");
      if (isCreateMode | isDuplicateMode)
        definitionFacade2.AddMirrorTextField(fieldName).SetTitle("NameForDevs").SetId("formName").SetMirroredControlId(definitionFacade3.Get().ID).SetRegularExpressionFilter("^[^A-Za-z]+|[^\\w_]+").SetReplaceWithValue("").SetPrefixText("sf_").SetCssClass("sfFormSeparator").AddValidation().MakeRequired().SetRegularExpression("^[a-zA-Z]+[\\w|_]*$").SetRequiredViolationMessage("NameCannotBeEmpty").SetRegularExpressionViolationMessage("FormNameNotValid");
      if (!isCreateMode && !isDuplicateMode)
        definitionFacade2.AddTextField(fieldName).SetId("formName").SetDataFieldName("Name").SetTitle("NameForDevs").SetCssClass("sfTitleField").AddValidation().MakeRequired().SetRequiredViolationMessage("NameCannotBeEmpty").Done();
      if (!isCreateMode && !isDuplicateMode)
      {
        ConfigElementDictionary<string, ContentViewSectionElement> sections = definitionFacade1.Get().Sections;
        sections.AddLazy((object) FormsDefinitions.ConnectorsSettingsSection, (Func<ConfigElement>) (() => FormsDefinitions.AddConnectorsSettingsSectionLazy(sections, isCreateMode, isDuplicateMode)));
      }
      if (displayMode == FieldDisplayMode.Write)
      {
        ContentViewSectionElement viewSectionElement = definitionFacade1.AddReadOnlySection("SidebarSection").Get();
        ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement.Fields;
        ContentWorkflowStatusInfoFieldElement element = new ContentWorkflowStatusInfoFieldElement((ConfigElement) viewSectionElement.Fields);
        element.DisplayMode = new FieldDisplayMode?(displayMode);
        element.FieldName = "FormsWorkflowStatusInfoField";
        element.ResourceClassId = typeof (FormsResources).Name;
        element.WrapperTag = HtmlTextWriterTag.Li;
        element.FieldType = typeof (ContentWorkflowStatusInfoField);
        fields.Add((FieldDefinitionElement) element);
      }
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) detailFormViewElement.Toolbar.Sections)
      {
        Name = "Main",
        WrapperTagKey = HtmlTextWriterTag.Div
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "SaveChangesWidgetElement";
      element2.ButtonType = CommandButtonType.Save;
      element2.CommandName = isCreateMode ? "create" : "save";
      element2.Text = isCreateMode ? "CreateAndAddContent" : "SaveChanges";
      element2.ResourceClassId = FormsDefinitions.ResourceClassId;
      element2.WrapperTagKey = HtmlTextWriterTag.Span;
      element2.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element2);
      if (isCreateMode)
      {
        ConfigElementList<WidgetElement> items2 = element1.Items;
        CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
        element3.Name = "CreateAndReturnWidgetElement";
        element3.ButtonType = CommandButtonType.Standard;
        element3.CommandName = "save";
        element3.Text = "CreateAndReturnToForms";
        element3.ResourceClassId = FormsDefinitions.ResourceClassId;
        element3.WrapperTagKey = HtmlTextWriterTag.Span;
        element3.WidgetType = typeof (CommandWidget);
        items2.Add((WidgetElement) element3);
      }
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "CancelWidgetElement";
      element4.ButtonType = CommandButtonType.Cancel;
      element4.CommandName = "cancel";
      element4.Text = "Cancel";
      element4.ResourceClassId = typeof (FormsResources).Name;
      element4.WrapperTagKey = HtmlTextWriterTag.Span;
      element4.WidgetType = typeof (CommandWidget);
      items3.Add((WidgetElement) element4);
      detailFormViewElement.Toolbar.Sections.Add(element1);
      return detailFormViewElement;
    }

    private static ConfigElement AddConnectorsSettingsSectionLazy(
      ConfigElementDictionary<string, ContentViewSectionElement> sectionFields,
      bool isCreateMode,
      bool isDuplicate = false)
    {
      string str = "sfExpandableForm";
      IEnumerable<FormsConnectorDefinitionsExtender> source1 = ObjectFactory.Container.ResolveAll<FormsConnectorDefinitionsExtender>();
      IEnumerable<ConnectorDataMappingExtender> source2 = ObjectFactory.Container.ResolveAll<ConnectorDataMappingExtender>();
      if (!source1.Any<FormsConnectorDefinitionsExtender>() && !source2.Any<ConnectorDataMappingExtender>())
        str += " sfDisplayNone";
      ContentViewSectionElement viewSectionElement = new ContentViewSectionElement((ConfigElement) sectionFields)
      {
        CssClass = str,
        Title = "ConnectorsSettings",
        Name = FormsDefinitions.ConnectorsSettingsSection,
        ResourceClassId = FormsDefinitions.ResourceClassId,
        DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write),
        ExpandableDefinitionConfig = {
          Expanded = new bool?(false)
        }
      };
      if (source1.Any<FormsConnectorDefinitionsExtender>())
      {
        ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement.Fields;
        TextFieldDefinitionElement definitionElement = new TextFieldDefinitionElement((ConfigElement) fields);
        definitionElement.Value = (object) Res.Get<FormsResources>().StepOneConfigureConnectionsTo;
        definitionElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
        definitionElement.FieldName = "StepOneConfigureConnectionsTo";
        definitionElement.FieldType = typeof (TextField);
        definitionElement.ID = "StepOneConfigureConnectionsTo";
        definitionElement.CssClass = FormsDefinitions.MVCOnlyFrameworkFieldsCssClass;
        definitionElement.ResourceClassId = FormsDefinitions.ResourceClassId;
        definitionElement.ServerSideOnly = true;
        TextFieldDefinitionElement element = definitionElement;
        fields.Add((FieldDefinitionElement) element);
        foreach (FormsConnectorDefinitionsExtender definitionsExtender in (IEnumerable<FormsConnectorDefinitionsExtender>) source1.OrderByDescending<FormsConnectorDefinitionsExtender, int>((Func<FormsConnectorDefinitionsExtender, int>) (e => e.Ordinal)))
          definitionsExtender.AddConnectorSettings(fields);
      }
      if (source2.Any<ConnectorDataMappingExtender>())
      {
        string dataMappingFormat = Res.Get<FormsResources>().StepTwoSetDataMappingFormat;
        TextFieldDefinitionElement definitionElement = new TextFieldDefinitionElement((ConfigElement) viewSectionElement.Fields);
        definitionElement.Value = source1.Any<FormsConnectorDefinitionsExtender>() ? (object) string.Format(dataMappingFormat, (object) Res.Get<FormsResources>().StepTwoSetDataMappingFormatTwoNumber) : (object) string.Format(dataMappingFormat, (object) "");
        definitionElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
        definitionElement.FieldName = "StepTwoSetDataMapping";
        definitionElement.FieldType = typeof (TextField);
        definitionElement.ID = "StepTwoSetDataMapping";
        definitionElement.CssClass = FormsDefinitions.MVCOnlyFrameworkFieldsCssClass;
        definitionElement.ResourceClassId = FormsDefinitions.ResourceClassId;
        definitionElement.ServerSideOnly = true;
        TextFieldDefinitionElement element = definitionElement;
        viewSectionElement.Fields.Add((FieldDefinitionElement) element);
        FormsDefinitions.AddConnectorDataMappingField(viewSectionElement.Fields);
      }
      return (ConfigElement) viewSectionElement;
    }

    private static void AddConnectorDataMappingField(
      ConfigElementDictionary<string, FieldDefinitionElement> sectionFields)
    {
      GenericFieldDefinitionElement definitionElement = new GenericFieldDefinitionElement((ConfigElement) sectionFields);
      definitionElement.DataFieldName = string.Format("{0}.{1}", (object) FormsDefinitions.FormAttributesPropertyName, (object) FormsDefinitions.DataMappingPropertyName);
      definitionElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      definitionElement.FieldName = FormsDefinitions.DataMappingPropertyName;
      definitionElement.CssClass = FormsDefinitions.MVCOnlyFrameworkFieldsCssClass + " sfMLeft35";
      definitionElement.Title = "DataMapping";
      definitionElement.FieldType = typeof (ConnectorDataMappingField);
      definitionElement.ResourceClassId = FormsDefinitions.ResourceClassId;
      definitionElement.ID = "ConnectorDataMappingField";
      GenericFieldDefinitionElement element = definitionElement;
      sectionFields.Add((FieldDefinitionElement) element);
    }

    private static void AddViewColumn(GridViewModeElement parent)
    {
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) parent.ColumnsConfig);
      dataColumnElement1.Name = "ViewColumn";
      dataColumnElement1.HeaderCssClass = "sfView";
      dataColumnElement1.ItemCssClass = "sfView";
      dataColumnElement1.ClientTemplate = "<a sys:href=\"{{ViewUrl}}\" target=\"_blank\">" + Res.Get<Labels>().View + "</a>";
      DataColumnElement element1 = dataColumnElement1;
      parent.ColumnsConfig.Add((ColumnElement) element1);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) parent.ColumnsConfig);
      dataColumnElement2.Name = "ResponsesColumn";
      dataColumnElement2.HeaderCssClass = "sfShort";
      dataColumnElement2.ItemCssClass = "sfShort";
      dataColumnElement2.ClientTemplate = "<a sys:if='ResponsesUrl' sys:href=\"{{ResponsesUrl}}\" class=\"sfResponsesCount sfGoto\" >{{EntriesCount}} " + Res.Get<FormsResources>().Responses + "</a>";
      DataColumnElement element2 = dataColumnElement2;
      parent.ColumnsConfig.Add((ColumnElement) element2);
    }

    private static void AddActionsMenuColumn(GridViewModeElement parent)
    {
      ActionMenuColumnElement menuColumnElement = new ActionMenuColumnElement((ConfigElement) parent.ColumnsConfig);
      menuColumnElement.Name = "ActionsLinkText";
      menuColumnElement.HeaderCssClass = "sfMoreActions";
      menuColumnElement.ItemCssClass = "sfMoreActions";
      menuColumnElement.TitleText = "ActionsLinkText";
      menuColumnElement.ResourceClassId = typeof (FormsResources).Name;
      ActionMenuColumnElement element1 = menuColumnElement;
      ConfigElementList<WidgetElement> menuItems1 = element1.MenuItems;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element2.Name = "Delete";
      element2.WrapperTagKey = HtmlTextWriterTag.Li;
      element2.CommandName = "delete";
      element2.Text = "Delete";
      element2.ResourceClassId = typeof (Labels).Name;
      element2.WidgetType = typeof (CommandWidget);
      element2.CssClass = "sfDeleteItm";
      menuItems1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> menuItems2 = element1.MenuItems;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element3.Name = "Publish";
      element3.WrapperTagKey = HtmlTextWriterTag.Li;
      element3.CommandName = "publishForm";
      element3.Text = "Publish";
      element3.ResourceClassId = typeof (Labels).Name;
      element3.WidgetType = typeof (CommandWidget);
      element3.CssClass = "sfPublishItm";
      menuItems2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> menuItems3 = element1.MenuItems;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element4.Name = "Unpublish";
      element4.WrapperTagKey = HtmlTextWriterTag.Li;
      element4.CommandName = "unpublishForm";
      element4.Text = "Unpublish";
      element4.ResourceClassId = typeof (Labels).Name;
      element4.WidgetType = typeof (CommandWidget);
      element4.CssClass = "sfUnpublishItm";
      menuItems3.Add((WidgetElement) element4);
      ConfigElementList<WidgetElement> menuItems4 = element1.MenuItems;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element5.Name = "Unlock";
      element5.WrapperTagKey = HtmlTextWriterTag.Li;
      element5.CommandName = "unlockForm";
      element5.Text = "Unlock";
      element5.ResourceClassId = typeof (Labels).Name;
      element5.WidgetType = typeof (CommandWidget);
      menuItems4.Add((WidgetElement) element5);
      ConfigElementList<WidgetElement> menuItems5 = element1.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element6.Name = "Subscribe";
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "subscribe";
      element6.Text = "Subscribe";
      element6.ResourceClassId = typeof (Labels).Name;
      element6.WidgetType = typeof (CommandWidget);
      menuItems5.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> menuItems6 = element1.MenuItems;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element7.Name = "Unsubscribe";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "unsubscribe";
      element7.Text = "Unsubscribe";
      element7.ResourceClassId = typeof (Labels).Name;
      element7.WidgetType = typeof (CommandWidget);
      menuItems6.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems7 = element1.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element8.Name = "Duplicate";
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "duplicate";
      element8.Text = "Duplicate";
      element8.ResourceClassId = typeof (Labels).Name;
      element8.WidgetType = typeof (CommandWidget);
      element8.CssClass = "sfDuplicateItm";
      menuItems7.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems8 = element1.MenuItems;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element9.Name = "ShareWith";
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.CommandName = "shareWith";
      element9.Text = "ShareWith";
      element9.ResourceClassId = typeof (Labels).Name;
      element9.WidgetType = typeof (CommandWidget);
      element9.CssClass = "sfShareLinkItm";
      element9.ModuleName = "MultisiteInternal";
      menuItems8.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> menuItems9 = element1.MenuItems;
      LiteralWidgetElement element10 = new LiteralWidgetElement((ConfigElement) element1.MenuItems);
      element10.Name = "Edit";
      element10.WrapperTagKey = HtmlTextWriterTag.Li;
      element10.Text = "EditEllipsis";
      element10.ResourceClassId = typeof (Labels).Name;
      element10.IsSeparator = true;
      menuItems9.Add((WidgetElement) element10);
      ConfigElementList<WidgetElement> menuItems10 = element1.MenuItems;
      CommandWidgetElement element11 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element11.Name = "Content";
      element11.WrapperTagKey = HtmlTextWriterTag.Li;
      element11.CommandName = "editFormContent";
      element11.Text = "Content";
      element11.ResourceClassId = typeof (Labels).Name;
      element11.WidgetType = typeof (CommandWidget);
      menuItems10.Add((WidgetElement) element11);
      ConfigElementList<WidgetElement> menuItems11 = element1.MenuItems;
      CommandWidgetElement element12 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element12.Name = "Properties";
      element12.WrapperTagKey = HtmlTextWriterTag.Li;
      element12.CommandName = "edit";
      element12.Text = "Properties";
      element12.ResourceClassId = typeof (Labels).Name;
      element12.WidgetType = typeof (CommandWidget);
      menuItems11.Add((WidgetElement) element12);
      ConfigElementList<WidgetElement> menuItems12 = element1.MenuItems;
      CommandWidgetElement element13 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element13.Name = "Permissions";
      element13.WrapperTagKey = HtmlTextWriterTag.Li;
      element13.CommandName = "permissions";
      element13.Text = "Permissions";
      element13.ResourceClassId = typeof (Labels).Name;
      element13.WidgetType = typeof (CommandWidget);
      menuItems12.Add((WidgetElement) element13);
      parent.ColumnsConfig.Add((ColumnElement) element1);
    }

    private static void AddLastModifiedColumn(GridViewModeElement parent)
    {
      DataColumnElement dataColumnElement = new DataColumnElement((ConfigElement) parent.ColumnsConfig);
      dataColumnElement.Name = "Date";
      dataColumnElement.HeaderText = "Date";
      dataColumnElement.ResourceClassId = typeof (Labels).Name;
      dataColumnElement.ClientTemplate = "<span>{{ (LastModified) ? LastModified.sitefinityLocaleFormat('dd MMM, yyyy hh:mm') : '-' }}</span>";
      dataColumnElement.HeaderCssClass = "sfDateAndHour";
      dataColumnElement.ItemCssClass = "sfDateAndHour";
      DataColumnElement element = dataColumnElement;
      parent.ColumnsConfig.Add((ColumnElement) element);
    }

    private static void AddOwnerColumn(GridViewModeElement parent)
    {
      DataColumnElement dataColumnElement = new DataColumnElement((ConfigElement) parent.ColumnsConfig);
      dataColumnElement.Name = "Owner";
      dataColumnElement.HeaderText = "Owner";
      dataColumnElement.ResourceClassId = typeof (Labels).Name;
      dataColumnElement.ClientTemplate = "<span class='sfLine'>{{Owner ? Owner : ''}}</span>";
      dataColumnElement.HeaderCssClass = "sfRegular";
      dataColumnElement.ItemCssClass = "sfRegular";
      DataColumnElement element = dataColumnElement;
      parent.ColumnsConfig.Add((ColumnElement) element);
    }

    private static void AddFormEntryColumns(GridViewModeElement entriesGridMode)
    {
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) entriesGridMode.ColumnsConfig);
      dataColumnElement1.BoundPropertyName = "ReferralCode";
      dataColumnElement1.Name = "ReferralCode";
      dataColumnElement1.HeaderText = "Response";
      dataColumnElement1.ResourceClassId = typeof (FormsResources).Name;
      dataColumnElement1.ClientTemplate = "{{ReferralCode}}";
      DataColumnElement element1 = dataColumnElement1;
      entriesGridMode.ColumnsConfig.Add((ColumnElement) element1);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) entriesGridMode.ColumnsConfig);
      dataColumnElement2.BoundPropertyName = "SourceSiteDisplayName";
      dataColumnElement2.Name = "Source";
      dataColumnElement2.HeaderText = "Source";
      dataColumnElement2.HeaderCssClass = "sfRegular";
      dataColumnElement2.ItemCssClass = "sfRegular";
      dataColumnElement2.ResourceClassId = typeof (Labels).Name;
      dataColumnElement2.ClientTemplate = "{{SourceSiteDisplayName}}";
      dataColumnElement2.ModuleName = "MultisiteInternal";
      DataColumnElement element2 = dataColumnElement2;
      entriesGridMode.ColumnsConfig.Add((ColumnElement) element2);
      DataColumnElement dataColumnElement3 = new DataColumnElement((ConfigElement) entriesGridMode.ColumnsConfig);
      dataColumnElement3.Name = "SubmittedOn";
      dataColumnElement3.HeaderText = "SubmittedOn";
      dataColumnElement3.ResourceClassId = typeof (FormsResources).Name;
      dataColumnElement3.HeaderCssClass = "sfRegular";
      dataColumnElement3.ItemCssClass = "sfRegular";
      dataColumnElement3.ClientTemplate = "{{ SubmittedOn ? SubmittedOn.sitefinityLocaleFormat('dd MMM, yyyy') : '-' }}";
      DataColumnElement element3 = dataColumnElement3;
      entriesGridMode.ColumnsConfig.Add((ColumnElement) element3);
      DataColumnElement dataColumnElement4 = new DataColumnElement((ConfigElement) entriesGridMode.ColumnsConfig);
      dataColumnElement4.Name = "Username";
      dataColumnElement4.HeaderText = "Username";
      dataColumnElement4.ResourceClassId = typeof (FormsResources).Name;
      dataColumnElement4.HeaderCssClass = "sfRegular";
      dataColumnElement4.ItemCssClass = "sfRegular";
      dataColumnElement4.ClientTemplate = "{{ Username }}";
      DataColumnElement element4 = dataColumnElement4;
      entriesGridMode.ColumnsConfig.Add((ColumnElement) element4);
      DataColumnElement dataColumnElement5 = new DataColumnElement((ConfigElement) entriesGridMode.ColumnsConfig);
      dataColumnElement5.Name = "Language";
      dataColumnElement5.HeaderText = Res.Get<FormsResources>().Language;
      dataColumnElement5.HeaderCssClass = "sfRegular";
      dataColumnElement5.ItemCssClass = "sfRegular";
      dataColumnElement5.ClientTemplate = "{{ Language ? Language : '-' }}";
      DataColumnElement element5 = dataColumnElement5;
      entriesGridMode.ColumnsConfig.Add((ColumnElement) element5);
    }

    internal static void DefineEntriesToolbar(MasterGridViewElement entriesBackendMaster)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) entriesBackendMaster.ToolbarConfig.Sections)
      {
        Name = "Entries"
      };
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "CreateEntryWidget";
      commandWidgetElement1.ButtonType = CommandButtonType.Create;
      commandWidgetElement1.CommandName = "create";
      commandWidgetElement1.Text = "CreateResponse";
      commandWidgetElement1.ResourceClassId = typeof (FormsResources).Name;
      commandWidgetElement1.CssClass = "sfMainAction";
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element2 = commandWidgetElement1;
      element1.Items.Add((WidgetElement) element2);
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement2.Name = "ExportWidget";
      commandWidgetElement2.ButtonType = CommandButtonType.Standard;
      commandWidgetElement2.CommandName = "export";
      commandWidgetElement2.Text = "ExportAsXLSX";
      commandWidgetElement2.ResourceClassId = typeof (FormsResources).Name;
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element3 = commandWidgetElement2;
      element1.Items.Add((WidgetElement) element3);
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement3.Name = "DeleteEntriesWidget";
      commandWidgetElement3.ButtonType = CommandButtonType.Standard;
      commandWidgetElement3.CommandName = "groupDelete";
      commandWidgetElement3.Text = "Delete";
      commandWidgetElement3.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      commandWidgetElement3.CssClass = "sfGroupBtn";
      CommandWidgetElement element4 = commandWidgetElement3;
      element1.Items.Add((WidgetElement) element4);
      element1.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element1.Items, typeof (FormEntry)));
      entriesBackendMaster.ToolbarConfig.Sections.Add(element1);
    }
  }
}
