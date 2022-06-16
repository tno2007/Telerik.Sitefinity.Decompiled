// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators.MasterListViewGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.DynamicModules.Web.UI.Backend;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Web.UI.Views;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators
{
  /// <summary>
  /// This class generates the master listview definition for the given dynamic module type.
  /// </summary>
  internal static class MasterListViewGenerator
  {
    private const string webServiceBaseUrl = "~/Sitefinity/Services/DynamicModules/Data.svc/";
    private const int itemsPerPage = 50;
    private static readonly PluralsResolver pluralsResover = PluralsResolver.Instance;
    private const string BackToContentTypeParentCommandName = "backToContentTypeParent";
    public static readonly string LabelsResClassId = typeof (Labels).Name;
    /// <summary>
    /// Name of the command used by the client side components to indicate that a new blog post
    /// should be created from the MasterGridView link
    /// </summary>
    public const string CreateGridPostCommandName = "createChild";
    private const string ViewChildItemsCommandName = "viewChildItems";
    internal const string ManageContentLocationsLinkName = "manageContentLocations";
    internal const string MasterGridViewGridElementName = "Grid";
    internal const string MasterGridViewActionsColumnName = "Actions";
    internal const string MasterGridViewActionsColumnPropertiesAction = "Properties";
    internal const string MGVActionsMenuEditPropetiesText = "Edit";

    /// <summary>
    /// Generates the toolbar element for the given dynamic module type and returns it.
    /// </summary>
    public static MasterGridViewElement Generate(
      IDynamicModuleType moduleType,
      ConfigElement parentElement,
      string mainFieldName,
      IDynamicModule module)
    {
      return MasterListViewGenerator.GenerateMasterListView(moduleType, parentElement, mainFieldName, module);
    }

    /// <summary>
    /// Generates the toolbar element for the given dynamic module type which is a parent of another module type and returns it.
    ///  </summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="childModuleType">Type of the child module.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="mainFieldName">Name of the main field.</param>
    /// <param name="moduleTitle">The module title.</param>
    /// <returns></returns>
    public static MasterGridViewElement GenerateHierarchy(
      IDynamicModuleType moduleType,
      IEnumerable<IDynamicModuleType> childModuleTypes,
      ConfigElement parentElement,
      string mainFieldName,
      IDynamicModule module)
    {
      return MasterListViewGenerator.GenerateMasterListView(moduleType, parentElement, mainFieldName, module, childModuleTypes);
    }

    /// <summary>
    /// Generates the toolbar element for the given dynamic module type which is self-referencing.
    /// </summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="mainFieldName">Name of the main field.</param>
    /// <param name="moduleTitle">The module title.</param>
    public static MasterGridViewElement GenerateSelfreferencingHierarchy(
      IDynamicModuleType moduleType,
      ConfigElement parentElement,
      string mainFieldName,
      IDynamicModule module)
    {
      return MasterListViewGenerator.GenerateMasterListView(moduleType, parentElement, mainFieldName, module, isSelfReferencing: true);
    }

    private static MasterGridViewElement GenerateMasterListView(
      IDynamicModuleType moduleType,
      ConfigElement parentElement,
      string mainFieldName,
      IDynamicModule module,
      IEnumerable<IDynamicModuleType> childModuleTypes = null,
      bool isSelfReferencing = false)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      if (parentElement == null)
        throw new ArgumentNullException("parent element");
      if (moduleType.Fields == null)
        throw new ArgumentException("Module Fields Null");
      string str = moduleType.Fields.Count<IDynamicModuleField>() != 0 ? MasterListViewGenerator.CreateSearchFieldsFromModule(moduleType.Fields) : throw new ArgumentException("Module fields count");
      MasterGridViewElement masterGridViewElement = new MasterGridViewElement(parentElement);
      masterGridViewElement.ViewName = ModuleNamesGenerator.GenerateListViewName(moduleType.DisplayName);
      masterGridViewElement.ViewType = typeof (DynamicContentMasterGridView);
      masterGridViewElement.AllowPaging = new bool?(true);
      masterGridViewElement.DisplayMode = FieldDisplayMode.Read;
      masterGridViewElement.ItemsPerPage = new int?(50);
      masterGridViewElement.SearchFields = str;
      masterGridViewElement.SortExpression = "LastModified DESC";
      masterGridViewElement.DisableSorting = new bool?(false);
      masterGridViewElement.Title = PluralsResolver.Instance.ToPlural(moduleType.DisplayName);
      masterGridViewElement.WebServiceBaseUrl = "~/Sitefinity/Services/DynamicModules/Data.svc/";
      masterGridViewElement.UseWorkflow = new bool?(true);
      masterGridViewElement.GridCssClass = "sfPagesTreeview";
      MasterGridViewElement dynamicModuleGridView = masterGridViewElement;
      GridViewModeElement gridElement = MasterListViewGenerator.GetGridElement(isSelfReferencing, dynamicModuleGridView, moduleType.Id.ToString());
      dynamicModuleGridView.ViewModesConfig.Add((ViewModeElement) gridElement);
      foreach (IDynamicModuleField dynamicModuleField in (IEnumerable<IDynamicModuleField>) moduleType.Fields.Where<IDynamicModuleField>((Func<IDynamicModuleField, bool>) (f => !f.IsHiddenField && f.ShowInGrid)).OrderBy<IDynamicModuleField, int>((Func<IDynamicModuleField, int>) (f => f.GridColumnOrdinal)))
      {
        if ((dynamicModuleField.FieldStatus != DynamicModuleFieldStatus.Removed || dynamicModuleField.SpecialType != FieldSpecialType.None) && dynamicModuleField.SpecialType != FieldSpecialType.ParentId)
        {
          ColumnElement fieldColumnElement = (ColumnElement) null;
          ColumnElement element;
          if (dynamicModuleField.Name == mainFieldName)
            element = MasterListViewGenerator.CreateMainColumnElement(childModuleTypes, gridElement, dynamicModuleField, fieldColumnElement);
          else if (dynamicModuleField.SpecialType == FieldSpecialType.UrlName)
            element = (ColumnElement) MasterListViewGenerator.CreateUrlColumnElement((ConfigElement) gridElement.ColumnsConfig);
          else if (dynamicModuleField.SpecialType == FieldSpecialType.Author)
            element = (ColumnElement) MasterListViewGenerator.CreateAuthorColumnElement((ConfigElement) gridElement.ColumnsConfig);
          else if (dynamicModuleField.SpecialType == FieldSpecialType.PublicationDate)
            element = (ColumnElement) MasterListViewGenerator.CreateDateColumnElement((ConfigElement) gridElement.ColumnsConfig, "PublicationDate");
          else if (dynamicModuleField.SpecialType == FieldSpecialType.LastModified)
            element = (ColumnElement) MasterListViewGenerator.CreateDateColumnElement((ConfigElement) gridElement.ColumnsConfig, "LastModified");
          else if (dynamicModuleField.SpecialType == FieldSpecialType.DateCreated)
            element = (ColumnElement) MasterListViewGenerator.CreateDateColumnElement((ConfigElement) gridElement.ColumnsConfig, "DateCreated");
          else if (dynamicModuleField.SpecialType == FieldSpecialType.Actions)
            element = MasterListViewGenerator.CreateActionMenuColumnElement(childModuleTypes, gridElement, fieldColumnElement, isSelfReferencing);
          else if (dynamicModuleField.SpecialType == FieldSpecialType.Translations)
            element = (ColumnElement) MasterListViewGenerator.CreateTranslationsColumn((ConfigElement) gridElement.ColumnsConfig);
          else if ((dynamicModuleField.FieldType == FieldType.Media || dynamicModuleField.FieldType == FieldType.RelatedMedia) && (dynamicModuleField.MediaType == "image" || dynamicModuleField.MediaType == "video"))
            element = (ColumnElement) MasterListViewGenerator.CreateDataColumnThumbnailElement((ConfigElement) gridElement.ColumnsConfig, dynamicModuleField);
          else if (dynamicModuleField.FieldType != FieldType.Media && dynamicModuleField.FieldType != FieldType.RelatedMedia || !(dynamicModuleField.MediaType == "file"))
            element = dynamicModuleField.FieldType != FieldType.DateTime ? (dynamicModuleField.FieldType != FieldType.LongText || !dynamicModuleField.WidgetTypeName.EndsWith("TextField") ? (dynamicModuleField.FieldType != FieldType.LongText || !dynamicModuleField.WidgetTypeName.EndsWith("HtmlField") ? (dynamicModuleField.FieldType != FieldType.Number ? (dynamicModuleField.FieldType != FieldType.Choices ? (dynamicModuleField.FieldType != FieldType.YesNo ? (dynamicModuleField.FieldType != FieldType.Address ? MasterListViewGenerator.GridViewColumnCreator(dynamicModuleField, (ConfigElement) gridElement.ColumnsConfig) : (ColumnElement) MasterListViewGenerator.CreateAddressColumnElement(dynamicModuleField, (ConfigElement) gridElement.ColumnsConfig)) : MasterListViewGenerator.CreateYesNoColumnElement(dynamicModuleField, (ConfigElement) gridElement.ColumnsConfig)) : MasterListViewGenerator.CreateChoicesColumnElement(dynamicModuleField, (ConfigElement) gridElement.ColumnsConfig)) : MasterListViewGenerator.CreateNumberColumnElement(dynamicModuleField, (ConfigElement) gridElement.ColumnsConfig)) : MasterListViewGenerator.CreateLongTextColumnTypeHTML(dynamicModuleField, (ConfigElement) gridElement.ColumnsConfig)) : MasterListViewGenerator.CreateLongTextColumnTypeNormal(dynamicModuleField, (ConfigElement) gridElement.ColumnsConfig)) : (ColumnElement) MasterListViewGenerator.CreateDatetimeColumnElement((ConfigElement) gridElement.ColumnsConfig, dynamicModuleField);
          else
            continue;
          gridElement.ColumnsConfig.Add(element);
        }
      }
      DefinitionsHelper.AddDynamicContentStatsColumn(gridElement, moduleType.GetFullTypeName());
      if (isSelfReferencing)
        MasterListViewGenerator.AddCreateChildElements(moduleType, dynamicModuleGridView);
      if (moduleType.ParentTypeId != Guid.Empty)
      {
        ConfigElementList<WidgetElement> titleWidgetsConfig = dynamicModuleGridView.TitleWidgetsConfig;
        CommandWidgetElement element = new CommandWidgetElement((ConfigElement) dynamicModuleGridView.TitleWidgetsConfig);
        element.Name = "backToContentTypeParent";
        element.CommandName = "backToContentTypeParent";
        element.ButtonType = CommandButtonType.SimpleLinkButton;
        element.WidgetType = typeof (CommandWidget);
        element.Text = string.Format(MasterListViewGenerator.GetDynamicModuleBackToParentText(), (object) PluralsResolver.Instance.ToPlural(moduleType.ParentType.DisplayName));
        element.WrapperTagKey = HtmlTextWriterTag.Span;
        titleWidgetsConfig.Add((WidgetElement) element);
        dynamicModuleGridView.LinksConfig.Add(new LinkElement((ConfigElement) dynamicModuleGridView.LinksConfig)
        {
          Name = "backToContentTypeParent",
          CommandName = "backToContentTypeParent",
          NavigateUrl = RouteHelper.CreateNodeReference(moduleType.ParentType.PageId) + "{{SystemParentUrl}}"
        });
      }
      string parameters1 = "?ControlDefinitionName=" + ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName()) + "&ViewName=" + ModuleNamesGenerator.GenerateBackendInsertViewName(moduleType.DisplayName);
      DialogElement dialogElement1 = DefinitionsHelper.CreateDialogElement((ConfigElement) dynamicModuleGridView.DialogsConfig, "create", typeof (ContentViewInsertDialog).Name, parameters1);
      dynamicModuleGridView.DialogsConfig.Add(dialogElement1);
      MasterListViewGenerator.AddDuplicateItemDialog(moduleType, dynamicModuleGridView);
      MasterListViewGenerator.AddEditItemDialog(moduleType, dynamicModuleGridView);
      string parameters2 = "?ControlDefinitionName=" + ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName()) + "&ViewName=" + ModuleNamesGenerator.GenerateBackendPreviewViewName(moduleType.DisplayName) + "&Id={{Item.Id}}";
      DialogElement dialogElement2 = DefinitionsHelper.CreateDialogElement((ConfigElement) dynamicModuleGridView.DialogsConfig, "viewProperties", typeof (ContentViewEditDialog).Name, parameters2);
      dynamicModuleGridView.DialogsConfig.Add(dialogElement2);
      MasterListViewGenerator.AddPermissionsDialogForDynamicModuleType(module, moduleType, dynamicModuleGridView);
      MasterListViewGenerator.AddPermissionsDialogForDynamicContent(module, moduleType, dynamicModuleGridView);
      dynamicModuleGridView.ToolbarConfig.Sections.Add(ToolbarGenerator.Generate(moduleType, (ConfigElement) dynamicModuleGridView.ToolbarConfig.Sections));
      dynamicModuleGridView.SidebarConfig.Title = string.Format("{0} {1}", (object) MasterListViewGenerator.GetDynamicModuleSideBarTitle(), (object) PluralsResolver.Instance.ToPlural(moduleType.DisplayName));
      if (ModuleInstallerHelper.ContainsLocalizableFields(moduleType.Fields))
      {
        if (!dynamicModuleGridView.SidebarConfig.Sections.Contains("Languages"))
          dynamicModuleGridView.SidebarConfig.Sections.Add((WidgetBarSectionElement) SidebarGenerator.GenerateLanguagesSection((ConfigElement) dynamicModuleGridView.SidebarConfig.Sections));
      }
      else if (dynamicModuleGridView.SidebarConfig.Sections.Contains("Languages"))
      {
        object section = dynamicModuleGridView.SidebarConfig.Sections["Languages"];
        dynamicModuleGridView.SidebarConfig.Sections.Remove((WidgetBarSectionElement) section);
      }
      WidgetBarSectionElement filterSection = SidebarGenerator.GenerateFilterSection(moduleType, (ConfigElement) dynamicModuleGridView.SidebarConfig.Sections);
      dynamicModuleGridView.SidebarConfig.Sections.Add(filterSection);
      SidebarGenerator.AddClassificationFilterSections(moduleType, dynamicModuleGridView.SidebarConfig, filterSection);
      dynamicModuleGridView.SidebarConfig.Sections.Add(SidebarGenerator.GenerateSettings(moduleType, (ConfigElement) dynamicModuleGridView.SidebarConfig.Sections, dynamicModuleGridView.LinksConfig));
      DefinitionsHelper.CreateRecycleBinLink(dynamicModuleGridView.SidebarConfig.Sections, moduleType.GetFullTypeName());
      DecisionScreenElement decisionScreenElement = MasterListViewGenerator.CreateDecisionScreenElement((ConfigElement) dynamicModuleGridView.DecisionScreensConfig, moduleType.DisplayName);
      CommandWidgetElement commandWidgetElement = MasterListViewGenerator.CreateCommandWidgetElement((ConfigElement) decisionScreenElement.Actions, moduleType);
      decisionScreenElement.Actions.Add(commandWidgetElement);
      dynamicModuleGridView.DecisionScreensConfig.Add(decisionScreenElement);
      if (moduleType.IsSelfReferencing)
      {
        PromptDialogElement parentItemDialog = MasterListViewGenerator.GetCannotDeleteParentItemDialog(dynamicModuleGridView.PromptDialogsConfig);
        dynamicModuleGridView.PromptDialogsConfig.Add(parentItemDialog);
      }
      if (childModuleTypes != null && childModuleTypes.Count<IDynamicModuleType>() != 0)
      {
        dynamicModuleGridView.LinksConfig.Add(new LinkElement((ConfigElement) dynamicModuleGridView.LinksConfig)
        {
          Name = "viewType",
          CommandName = "viewChildItems",
          NavigateUrl = RouteHelper.CreateNodeReference(childModuleTypes.First<IDynamicModuleType>().PageId) + "{{SystemUrl}}/?provider={{ProviderName}}"
        });
        foreach (IDynamicModuleType childModuleType in childModuleTypes)
          dynamicModuleGridView.LinksConfig.Add(new LinkElement((ConfigElement) dynamicModuleGridView.LinksConfig)
          {
            Name = "viewType" + childModuleType.TypeName,
            CommandName = childModuleType.GenerateChiltTypeCommandName(),
            NavigateUrl = RouteHelper.CreateNodeReference(childModuleType.PageId) + "{{SystemUrl}}/?provider={{ProviderName}}"
          });
      }
      string fullTypeName = moduleType.GetFullTypeName();
      dynamicModuleGridView.LinksConfig.Add(new LinkElement((ConfigElement) dynamicModuleGridView.LinksConfig)
      {
        Name = "manageContentLocations",
        CommandName = "manageContentLocations",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.ContentLocationsPageId) + "?item_type=" + fullTypeName
      });
      dynamicModuleGridView.Scripts.Add(new ClientScriptElement((ConfigElement) dynamicModuleGridView.Scripts)
      {
        ScriptLocation = "Telerik.Sitefinity.Resources.Scripts.jquery.shorten.js, Telerik.Sitefinity.Resources"
      });
      dynamicModuleGridView.Scripts.Add(new ClientScriptElement((ConfigElement) dynamicModuleGridView.Scripts)
      {
        ScriptLocation = "Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.MasterGridViewGeneratorExtensions.js, Telerik.Sitefinity",
        LoadMethodName = "OnModuleMasterViewLoaded"
      });
      if (moduleType.IsSelfReferencing)
        dynamicModuleGridView.Scripts.Add(new ClientScriptElement((ConfigElement) dynamicModuleGridView.Scripts)
        {
          ScriptLocation = "Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentsMasterGridViewExtensions.js, Telerik.Sitefinity",
          LoadMethodName = "OnModuleMasterViewLoaded"
        });
      MasterListViewGenerator.AddHistoryVersionPreviewDialog(moduleType, dynamicModuleGridView.DialogsConfig);
      MasterListViewGenerator.AddHistoryVersionComparerDialog(moduleType, dynamicModuleGridView.DialogsConfig);
      return dynamicModuleGridView;
    }

    /// <summary>
    /// Gets comma separated couples of field id and field permission set for all the fields of the dynamic module type
    /// </summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <returns></returns>
    internal static string GetFieldPermissionSets(IDynamicModuleType moduleType)
    {
      List<string> values = new List<string>();
      foreach (IDynamicModuleField field in moduleType.Fields)
      {
        if (!field.IsTransient && field.FieldStatus != DynamicModuleFieldStatus.Removed && field.FieldType != FieldType.RelatedData && field.FieldType != FieldType.RelatedMedia)
          values.Add(field.Id.ToString() + ":" + field.GetPermissionSetName());
      }
      return string.Join(",", (IEnumerable<string>) values);
    }

    /// <summary>
    /// Register a version history preview dialog for a specific module version.
    /// </summary>
    internal static void AddHistoryVersionPreviewDialog(
      IDynamicModuleType moduleType,
      ConfigElementList<DialogElement> parent)
    {
      string str = string.Format("?ControlDefinitionName={0}&ViewName={1}&backLabelText={2}&SuppressBackToButtonLabelModify=true", (object) ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName()), (object) ModuleNamesGenerator.GenerateBackendVersionPreview(moduleType.DisplayName), (object) MasterListViewGenerator.BackToRevisionHistory());
      DialogElement element = new DialogElement((ConfigElement) parent)
      {
        Behaviors = WindowBehaviors.None,
        InitialBehaviors = WindowBehaviors.Maximize,
        Width = Unit.Percentage(100.0),
        Height = Unit.Percentage(100.0),
        VisibleTitleBar = false,
        VisibleStatusBar = false,
        IsModal = false,
        CssClass = "sfMaximizedWindow",
        Parameters = str,
        OpenOnCommandName = "versionPreview",
        Name = typeof (ContentViewEditDialog).Name
      };
      parent.Add(element);
    }

    /// <summary>
    /// Register a version comparison history preview dialog for a specific module version.
    /// </summary>
    internal static void AddHistoryVersionComparerDialog(
      IDynamicModuleType moduleType,
      ConfigElementList<DialogElement> parent)
    {
      string str = string.Format("?ControlDefinitionName={0}&VersionComparisonView={1}&moduleName={2}&typeName={3}&backLabelText={4}", (object) ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName()), (object) ModuleNamesGenerator.GenerateBackendVersionComparerPreview(moduleType.DisplayName), (object) moduleType.GetFullTypeName(), (object) moduleType.GetFullTypeName(), (object) MasterListViewGenerator.BackToRevisionHistory());
      DialogElement element = new DialogElement((ConfigElement) parent)
      {
        Behaviors = WindowBehaviors.None,
        InitialBehaviors = WindowBehaviors.Maximize,
        Width = Unit.Percentage(100.0),
        Height = Unit.Percentage(100.0),
        VisibleTitleBar = false,
        VisibleStatusBar = false,
        IsModal = false,
        CssClass = "sfMaximizedWindow",
        Parameters = str,
        OpenOnCommandName = "historygrid",
        Name = typeof (VersionHistoryDialog).Name
      };
      parent.Add(element);
    }

    private static void AddEditItemDialog(
      IDynamicModuleType moduleType,
      MasterGridViewElement dynamicModuleGridView)
    {
      string parameters = "?ControlDefinitionName=" + ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName()) + "&ViewName=" + ModuleNamesGenerator.GenerateBackendEditViewName(moduleType.DisplayName) + "&Id={{Id}}";
      if (moduleType.IsSelfReferencing)
        parameters += "&parentId={{SystemParentId}}&parentType={{SystemParentType}}&overrideParent=false";
      DialogElement dialogElement = DefinitionsHelper.CreateDialogElement((ConfigElement) dynamicModuleGridView.DialogsConfig, "edit", typeof (ContentViewEditDialog).Name, parameters);
      if (moduleType.CheckFieldPermissions)
        dialogElement.ReloadOnShow = new bool?(true);
      dynamicModuleGridView.DialogsConfig.Add(dialogElement);
    }

    internal static void AddDuplicateItemDialog(
      IDynamicModuleType moduleType,
      MasterGridViewElement dynamicModuleGridView)
    {
      string parameters = "?ControlDefinitionName=" + ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName()) + "&ViewName=" + ModuleNamesGenerator.GenerateBackendDuplicateViewName(moduleType.DisplayName);
      DialogElement dialogElement = DefinitionsHelper.CreateDialogElement((ConfigElement) dynamicModuleGridView.DialogsConfig, "duplicate", typeof (ContentViewInsertDialog).Name, parameters);
      dynamicModuleGridView.DialogsConfig.Add(dialogElement);
    }

    internal static bool RemoveDialog(
      MasterGridViewElement dynamicModuleGridView,
      string dialogName,
      string permissionsCommandName)
    {
      string dialogElementId = DialogElement.ConstructDialogIDFrom(dialogName, permissionsCommandName);
      IConfigElementItem configElementItem = dynamicModuleGridView.DialogsConfig.Items.SingleOrDefault<IConfigElementItem>((Func<IConfigElementItem, bool>) (d => d.Key == dialogElementId));
      return configElementItem != null && dynamicModuleGridView.DialogsConfig.Remove(configElementItem.Element);
    }

    internal static void AddPermissionsDialogForDynamicModuleType(
      IDynamicModule module,
      IDynamicModuleType moduleType,
      MasterGridViewElement dynamicModuleGridView)
    {
      MasterListViewGenerator.AddPermissionsDialog(dynamicModuleGridView, moduleType, module.Name, typeof (DynamicModuleType).FullName, moduleType.Id.ToString(), "permissions", "General", "SitemapGeneration");
    }

    internal static void AddPermissionsDialogForDynamicContent(
      IDynamicModule module,
      IDynamicModuleType moduleType,
      MasterGridViewElement dynamicModuleGridView)
    {
      MasterListViewGenerator.AddPermissionsDialog(dynamicModuleGridView, moduleType, module.Name, moduleType.GetFullTypeName(), "{{Id}}", "permissionsDynamicContent", "General");
    }

    private static void AddPermissionsDialog(
      MasterGridViewElement dynamicModuleGridView,
      IDynamicModuleType moduleType,
      string moduleName,
      string typeFullName,
      string securedObjectId,
      string openOnCommandName,
      params string[] permissionSetsNames)
    {
      string str = permissionSetsNames != null ? string.Join(",", permissionSetsNames) : string.Empty;
      string parameters = "?moduleName=" + moduleName + "&typeName=" + typeFullName + "&securedObjectId=" + securedObjectId + "&backLabelText=" + "Back to items" + "&title=" + "Permissions" + "&permissionSetName=" + str;
      if (moduleType.CheckFieldPermissions)
        parameters = parameters + "&relatedSecuredObjectTypeName=" + typeof (DynamicModuleField).FullName + "&relatedSecuredObjects=" + MasterListViewGenerator.GetFieldPermissionSets(moduleType);
      DialogElement dialogElement = DefinitionsHelper.CreateDialogElement((ConfigElement) dynamicModuleGridView.DialogsConfig, openOnCommandName, typeof (ModulePermissionsDialog).Name, parameters);
      dynamicModuleGridView.DialogsConfig.Add(dialogElement);
    }

    private static GridViewModeElement GetGridElement(
      bool isSelfReferencing,
      MasterGridViewElement dynamicModuleGridView,
      string id)
    {
      GridViewModeElement gridElement;
      if (isSelfReferencing)
      {
        GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) dynamicModuleGridView.ViewModesConfig);
        gridViewModeElement.Name = "TreeTable";
        gridViewModeElement.EnableDragAndDrop = new bool?(false);
        gridViewModeElement.EnableInitialExpanding = new bool?(true);
        gridViewModeElement.ExpandedNodesCookieName = "ExpandItems" + id;
        gridElement = gridViewModeElement;
      }
      else
      {
        GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) dynamicModuleGridView.ViewModesConfig);
        gridViewModeElement.Name = "Grid";
        gridElement = gridViewModeElement;
      }
      return gridElement;
    }

    private static DecisionScreenElement CreateDecisionScreenElement(
      ConfigElement parent,
      string displayName)
    {
      return new DecisionScreenElement(parent)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        MessageText = string.Format(MasterListViewGenerator.GetMessageTextForDecisionScreenElement(), (object) MasterListViewGenerator.pluralsResover.ToPlural(displayName).ToLower())
      };
    }

    private static CommandWidgetElement CreateCommandWidgetElement(
      ConfigElement parentElement,
      IDynamicModuleType moduleType)
    {
      IndefiniteArticleResolver indefiniteArticleResolver = new IndefiniteArticleResolver();
      indefiniteArticleResolver.ResolveModuleTypeName(moduleType);
      CommandWidgetElement commandWidgetElement = new CommandWidgetElement(parentElement);
      commandWidgetElement.Name = "Create";
      commandWidgetElement.ButtonType = CommandButtonType.Create;
      commandWidgetElement.CommandName = "create";
      commandWidgetElement.Text = string.Format(MasterListViewGenerator.GetTextForCreateItemWidget(), (object) indefiniteArticleResolver.Prefix, (object) moduleType.DisplayName.ToLower());
      commandWidgetElement.CssClass = "sfCreateItem";
      commandWidgetElement.PermissionSet = "General";
      commandWidgetElement.ActionName = "Create";
      commandWidgetElement.RelatedSecuredObjectId = moduleType.Id.ToString();
      commandWidgetElement.RelatedSecuredObjectTypeName = typeof (DynamicModuleType).FullName;
      return commandWidgetElement;
    }

    /// <summary>Gets back to revision history label.</summary>
    /// <returns></returns>
    private static string BackToRevisionHistory() => Res.Get<Labels>().BackToRevisionHistory;

    private static ColumnElement GridViewColumnCreator(
      IDynamicModuleField moduleField,
      ConfigElement parentElement)
    {
      string name = moduleField.Name;
      string title = moduleField.Title;
      string headerCssClass = "sfRegular";
      string itemCssClass = "sfRegular";
      string clientTemplate = !moduleField.IsLocalizable ? "<span>{{" + moduleField.Name + "}}</span>" : "<span>{{" + moduleField.Name + ".PersistedValue}}</span>";
      return (ColumnElement) MasterListViewGenerator.CreateDataColumnElement(parentElement, name, title, headerCssClass, itemCssClass, clientTemplate);
    }

    private static ColumnElement CreateNumberColumnElement(
      IDynamicModuleField moduleField,
      ConfigElement parentElement)
    {
      string empty = string.Empty;
      string clientTemplate;
      if (string.IsNullOrEmpty(moduleField.NumberUnit))
        clientTemplate = "<span>{{" + moduleField.Name + "}}</span>";
      else
        clientTemplate = "<span>{{" + moduleField.Name + "}}</span> <span class=\"sfUnit\">" + moduleField.NumberUnit + "</span>";
      return MasterListViewGenerator.CreateColumnElementWithCustomClientTemplate(moduleField, parentElement, clientTemplate);
    }

    private static ColumnElement CreateChoicesColumnElement(
      IDynamicModuleField moduleField,
      ConfigElement parentElement)
    {
      string empty = string.Empty;
      string clientTemplate;
      if (moduleField.CanSelectMultipleItems)
        clientTemplate = "<span>{{joinArray(" + moduleField.Name + ", 'Text')}}</span>";
      else
        clientTemplate = "<span>{{" + moduleField.Name + " ? " + moduleField.Name + ".Text : ''}}</span>";
      return MasterListViewGenerator.CreateColumnElementWithCustomClientTemplate(moduleField, parentElement, clientTemplate);
    }

    private static ColumnElement CreateYesNoColumnElement(
      IDynamicModuleField moduleField,
      ConfigElement parentElement)
    {
      string empty = string.Empty;
      string clientTemplate = "<span>{{getBool(" + moduleField.Name + ",'" + Res.Get<Labels>().Yes + "','" + Res.Get<Labels>().No + "')}}</span>";
      return MasterListViewGenerator.CreateColumnElementWithCustomClientTemplate(moduleField, parentElement, clientTemplate);
    }

    private static ColumnElement CreateLongTextColumnTypeHTML(
      IDynamicModuleField moduleField,
      ConfigElement parentElement)
    {
      StringBuilder stringBuilder = new StringBuilder("<div class=\"dmDescription dmDescriptionHTML\">{{");
      stringBuilder.Append(moduleField.Name);
      if (moduleField.IsLocalizable)
        stringBuilder.Append(".PersistedValue");
      stringBuilder.Append("}}</div>");
      return MasterListViewGenerator.CreateColumnElementWithCustomClientTemplate(moduleField, parentElement, stringBuilder.ToString());
    }

    private static ColumnElement CreateLongTextColumnTypeNormal(
      IDynamicModuleField moduleField,
      ConfigElement parentElement)
    {
      StringBuilder stringBuilder = new StringBuilder("<div class=\"dmDescription\">{{");
      stringBuilder.Append(moduleField.Name);
      if (moduleField.IsLocalizable)
        stringBuilder.Append(".PersistedValue");
      stringBuilder.Append("}}</div>");
      return MasterListViewGenerator.CreateColumnElementWithCustomClientTemplate(moduleField, parentElement, stringBuilder.ToString());
    }

    private static ColumnElement CreateColumnElementWithCustomClientTemplate(
      IDynamicModuleField moduleField,
      ConfigElement parentElement,
      string clientTemplate)
    {
      string name = moduleField.Name;
      string title = moduleField.Title;
      string headerCssClass = "sfRegular";
      string itemCssClass = "sfRegular";
      return (ColumnElement) MasterListViewGenerator.CreateDataColumnElement(parentElement, name, title, headerCssClass, itemCssClass, clientTemplate);
    }

    private static DataColumnElement CreateDatetimeColumnElement(
      ConfigElement parentElement,
      IDynamicModuleField moduleField)
    {
      DataColumnElement datetimeColumnElement = new DataColumnElement(parentElement);
      datetimeColumnElement.Name = moduleField.Name;
      datetimeColumnElement.HeaderText = moduleField.Title;
      datetimeColumnElement.ClientTemplate = "<span>{{ (" + moduleField.Name + ") ? " + moduleField.Name + ".sitefinityLocaleFormat('dd MMM, yyyy hh:mm:ss'): '-' }}</span>";
      datetimeColumnElement.HeaderCssClass = "sfRegular";
      datetimeColumnElement.ItemCssClass = "sfRegular";
      return datetimeColumnElement;
    }

    private static ColumnElement CreateActionMenuColumnElement(
      IEnumerable<IDynamicModuleType> childModuleTypes,
      GridViewModeElement itemsGrid,
      ColumnElement fieldColumnElement,
      bool isSelfReferencing)
    {
      if (childModuleTypes != null)
      {
        childModuleTypes.Count<IDynamicModuleType>();
        fieldColumnElement = (ColumnElement) MasterListViewGenerator.CreateActionMenuColumnElement((ConfigElement) itemsGrid.ColumnsConfig);
        fieldColumnElement = (ColumnElement) MasterListViewGenerator.AddPropertiesActionsMenuItem((ActionMenuColumnElement) fieldColumnElement);
      }
      else
        fieldColumnElement = (ColumnElement) MasterListViewGenerator.CreateActionMenuColumnElement((ConfigElement) itemsGrid.ColumnsConfig);
      if (isSelfReferencing)
        fieldColumnElement = (ColumnElement) MasterListViewGenerator.AddChildItemActionsMenuItem((ActionMenuColumnElement) fieldColumnElement);
      return fieldColumnElement;
    }

    private static ColumnElement CreateMainColumnElement(
      IEnumerable<IDynamicModuleType> childModuleTypes,
      GridViewModeElement itemsGrid,
      IDynamicModuleField field,
      ColumnElement fieldColumnElement)
    {
      fieldColumnElement = childModuleTypes == null || childModuleTypes.Count<IDynamicModuleType>() == 0 ? (ColumnElement) MasterListViewGenerator.CreateMainColumnElement((ConfigElement) itemsGrid.ColumnsConfig, field) : (ColumnElement) MasterListViewGenerator.CreateMainColumnElementHierarchy((ConfigElement) itemsGrid.ColumnsConfig, field, childModuleTypes);
      return fieldColumnElement;
    }

    private static DataColumnElement CreateMainColumnElement(
      ConfigElement parentElement,
      IDynamicModuleField moduleField)
    {
      string columnClientTemplate = MasterListViewGenerator.CreateMainColumnClientTemplate(moduleField);
      DataColumnElement mainColumnElement = new DataColumnElement(parentElement);
      mainColumnElement.Name = moduleField.Name;
      mainColumnElement.HeaderText = moduleField.Title;
      mainColumnElement.ItemCssClass = "sfTitleCol";
      mainColumnElement.HeaderCssClass = "sfTitleCol";
      mainColumnElement.ClientTemplate = columnClientTemplate;
      return mainColumnElement;
    }

    internal static string CreateMainColumnClientTemplate(IDynamicModuleField moduleField)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<a sys:href='javascript:void(0);' sys:class=\"{{ 'sf_binderCommand_edit sfItemTitle sf' + Lifecycle.WorkflowStatus.replace(' ','').toLowerCase() + (Lifecycle.StatusProviderName ? ' sf' + Lifecycle.StatusProviderName.toLowerCase() : '') }}\">");
      stringBuilder.Append("<strong>{{");
      stringBuilder.Append(moduleField.Name);
      if (moduleField.IsLocalizable)
        stringBuilder.Append(".PersistedValue");
      stringBuilder.Append("}}</strong>");
      stringBuilder.Append("<span sys:if='Lifecycle.AdditionalStatusText' class='sfStatusAdditional'>{{Lifecycle.AdditionalStatusText}}</span>");
      stringBuilder.Append("<span class='sfStatusLocation'><span sys:if='Lifecycle.AdditionalStatusText'>| </span>{{Lifecycle.Message}}</span></a>");
      return stringBuilder.ToString();
    }

    private static DataColumnElement CreateMainColumnElementHierarchy(
      ConfigElement parentElement,
      IDynamicModuleField moduleField,
      IEnumerable<IDynamicModuleType> childModuleTypes)
    {
      string templateHierarchy = MasterListViewGenerator.CreateMainColumnClientTemplateHierarchy(moduleField, childModuleTypes);
      DataColumnElement elementHierarchy = new DataColumnElement(parentElement);
      elementHierarchy.Name = moduleField.Name;
      elementHierarchy.HeaderText = moduleField.Title;
      elementHierarchy.ItemCssClass = "sfTitleCol";
      elementHierarchy.HeaderCssClass = "sfTitleCol";
      elementHierarchy.ClientTemplate = templateHierarchy;
      return elementHierarchy;
    }

    internal static string CreateMainColumnClientTemplateHierarchy(
      IDynamicModuleField moduleField,
      IEnumerable<IDynamicModuleType> childModuleTypes)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<a sys:href='javascript:void(0);' sys:class=\"{{'sf_binderCommand_viewChildItems sfItemTitle' + (Lifecycle.StatusProviderName ? ' sf' + Lifecycle.StatusProviderName.toLowerCase() + ' sf' + Lifecycle.StatusProviderName.toLowerCase() + 'SmStatus'  : '')}}\">");
      stringBuilder.Append("<strong class='sfParentTitle'>{{");
      stringBuilder.Append(moduleField.Name);
      if (moduleField.IsLocalizable)
        stringBuilder.Append(".PersistedValue");
      stringBuilder.Append("}}</strong>");
      stringBuilder.Append("</a>");
      stringBuilder.Append("<span sys:if='Lifecycle.AdditionalStatusText' class='sfStatusAdditional'>{{Lifecycle.AdditionalStatusText}}</span>");
      stringBuilder.Append(string.Format("<span class='sfSecondaryTxt sfSmallerTxt sfMLeft5'>(<a sys:href='javascript:void(0);' class='sf_binderCommand_edit'>{0}</a>)</span>", (object) MasterListViewGenerator.GetParentItemEditLinkText()));
      stringBuilder.Append("<span class='sfItemTitle'><span class='sfStatusLocation sfMTop2'>{{Lifecycle.Message}}</span></span>");
      stringBuilder.Append("<ul class='sfMTop5 sfChildrenList'>");
      foreach (IDynamicModuleType childModuleType in childModuleTypes)
        stringBuilder.Append(string.Format("<li><a sys:href='javascript:void(0);' sys:class=\"{{{{ 'sf_binderCommand_{0}' }}}}\" title=\"{1}\">{1}</a></li>", (object) childModuleType.GenerateChiltTypeCommandName(), (object) PluralsResolver.Instance.ToPlural(childModuleType.DisplayName)));
      stringBuilder.Append("</ul>");
      return stringBuilder.ToString();
    }

    internal static string GenerateChiltTypeCommandName(this IDynamicModuleType dynamicModuleType) => "viewChildItems" + dynamicModuleType.TypeName;

    private static DataColumnElement CreateDataColumnElement(
      ConfigElement parentElement,
      string name,
      string headerText,
      string headerCssClass,
      string itemCssClass,
      string clientTemplate)
    {
      DataColumnElement dataColumnElement = new DataColumnElement(parentElement);
      dataColumnElement.Name = name;
      dataColumnElement.HeaderText = headerText;
      dataColumnElement.HeaderCssClass = headerCssClass;
      dataColumnElement.ItemCssClass = itemCssClass;
      dataColumnElement.ClientTemplate = clientTemplate;
      return dataColumnElement;
    }

    private static DataColumnElement CreateDataColumnThumbnailElement(
      ConfigElement parentElement,
      IDynamicModuleField moduleField)
    {
      string str = string.Format("({0}.length > 0) ? {0}[0].ChildItemAdditionalInfo : ''", (object) moduleField.Name);
      DataColumnElement thumbnailElement = new DataColumnElement(parentElement);
      thumbnailElement.Name = moduleField.Name;
      thumbnailElement.HeaderText = moduleField.Title;
      thumbnailElement.ClientTemplate = "<img sys:src=\"{{" + str + "}}\" />";
      thumbnailElement.HeaderCssClass = "sfImgTmb";
      thumbnailElement.ItemCssClass = "sfImgTmb";
      return thumbnailElement;
    }

    private static ActionMenuColumnElement CreateActionMenuColumnElement(
      ConfigElement parentElement)
    {
      ActionMenuColumnElement menuColumnElement = new ActionMenuColumnElement(parentElement);
      menuColumnElement.Name = "Actions";
      menuColumnElement.HeaderText = MasterListViewGenerator.GetHeaderTextForActionMenuColumnElement();
      menuColumnElement.HeaderCssClass = "sfMoreActions";
      menuColumnElement.ItemCssClass = "sfMoreActions";
      menuColumnElement.MenuItems.Add((WidgetElement) ActionsMenuGenerator.CreateActionMenuCommand((ConfigElement) menuColumnElement.MenuItems, "Delete", HtmlTextWriterTag.Li, "delete", MasterListViewGenerator.GetTextForDeleteActionsWidget(), "sfDeleteItm"));
      menuColumnElement.MenuItems.Add((WidgetElement) ActionsMenuGenerator.CreateActionMenuCommand((ConfigElement) menuColumnElement.MenuItems, "Permissions", HtmlTextWriterTag.Li, "permissionsDynamicContent", MasterListViewGenerator.GetTextForPermissionsActionsWidget(), "sfPermissionsItm"));
      menuColumnElement.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement.MenuItems, "History", HtmlTextWriterTag.Li, "historygrid", "HistoryMenuItemTitle", typeof (VersionResources).Name));
      menuColumnElement.MenuItems.Add((WidgetElement) ActionsMenuGenerator.CreateActionMenuCommand((ConfigElement) menuColumnElement.MenuItems, "Duplicate", HtmlTextWriterTag.Li, "duplicate", MasterListViewGenerator.GetTextForDuplicateItemWidget(), "sfDuplicateItm"));
      return menuColumnElement;
    }

    private static DynamicColumnElement CreateTranslationsColumn(
      ConfigElement parentElement)
    {
      DynamicColumnElement parent = new DynamicColumnElement(parentElement);
      parent.Name = "Translations";
      parent.HeaderText = "Translations";
      parent.ResourceClassId = typeof (LocalizationResources).Name;
      parent.DynamicMarkupGenerator = typeof (LanguagesColumnMarkupGenerator);
      parent.ItemCssClass = "sfLanguagesCol";
      parent.HeaderCssClass = "sfLanguagesCol";
      parent.GeneratorSettingsElement = (DynamicMarkupGeneratorElement) new LanguagesColumnMarkupGeneratorElement((ConfigElement) parent)
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
      return parent;
    }

    private static ActionMenuColumnElement AddPropertiesActionsMenuItem(
      ActionMenuColumnElement actionsColumn)
    {
      actionsColumn.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) actionsColumn.MenuItems, "Properties", HtmlTextWriterTag.Li, "edit", "Edit", MasterListViewGenerator.LabelsResClassId));
      return actionsColumn;
    }

    private static ActionMenuColumnElement AddChildItemActionsMenuItem(
      ActionMenuColumnElement actionsColumn)
    {
      ConfigElementList<WidgetElement> menuItems = actionsColumn.MenuItems;
      CommandWidgetElement element = new CommandWidgetElement((ConfigElement) actionsColumn.MenuItems);
      element.Name = "CreateChildItem";
      element.WrapperTagKey = HtmlTextWriterTag.Li;
      element.CommandName = "createChild";
      element.Text = "CreateChildItem";
      element.ResourceClassId = "ModuleBuilderResources";
      element.WidgetType = typeof (CommandWidget);
      element.CssClass = "sfCreateChild";
      menuItems.Add((WidgetElement) element);
      return actionsColumn;
    }

    private static void AddCreateChildElements(
      IDynamicModuleType moduleType,
      MasterGridViewElement dynamicModuleGridView)
    {
      string parameters = "?ControlDefinitionName=" + ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName()) + "&ViewName=" + ModuleNamesGenerator.GenerateBackendInsertViewName(moduleType.DisplayName) + "&parentId={{Id}}&parentType=" + moduleType.GetFullTypeName() + "&overrideParent=false";
      DialogElement dialogElement = DefinitionsHelper.CreateDialogElement((ConfigElement) dynamicModuleGridView.DialogsConfig, "createChild", typeof (ContentViewInsertDialog).Name, parameters);
      dynamicModuleGridView.DialogsConfig.Add(dialogElement);
    }

    private static DataColumnElement CreateUrlColumnElement(
      ConfigElement parentElement)
    {
      DataColumnElement urlColumnElement = new DataColumnElement(parentElement);
      urlColumnElement.Name = "UrlName";
      urlColumnElement.HeaderText = "UrlName";
      urlColumnElement.ClientTemplate = "<span>{{UrlName.Value}}</span>";
      urlColumnElement.HeaderCssClass = "sfRegular";
      urlColumnElement.ItemCssClass = "sfRegular";
      return urlColumnElement;
    }

    private static DataColumnElement CreateAuthorColumnElement(
      ConfigElement parentElement)
    {
      DataColumnElement authorColumnElement = new DataColumnElement(parentElement);
      authorColumnElement.Name = "Author";
      authorColumnElement.HeaderText = "Author";
      authorColumnElement.ResourceClassId = typeof (Labels).Name;
      authorColumnElement.ClientTemplate = "<span>{{Author}}</span>";
      authorColumnElement.HeaderCssClass = "sfAuthor";
      authorColumnElement.ItemCssClass = "sfAuthor";
      return authorColumnElement;
    }

    private static DataColumnElement CreateDateColumnElement(
      ConfigElement parentElement,
      string name)
    {
      string str = string.Format("<span>{{{{ ({0}) ? {0}.sitefinityLocaleFormat('dd MMM, yyyy hh:mm:ss'): '-' }}}}</span>", (object) name);
      DataColumnElement dateColumnElement = new DataColumnElement(parentElement);
      dateColumnElement.Name = name;
      dateColumnElement.HeaderText = name;
      dateColumnElement.ResourceClassId = typeof (ModuleBuilderResources).Name;
      dateColumnElement.ClientTemplate = str;
      dateColumnElement.HeaderCssClass = "sfDateAndHour";
      dateColumnElement.ItemCssClass = "sfDateAndHour";
      return dateColumnElement;
    }

    private static DataColumnElement CreateAddressColumnElement(
      IDynamicModuleField moduleField,
      ConfigElement parentElement)
    {
      string name = moduleField.Name;
      string title = moduleField.Title;
      string headerCssClass = "sfRegular";
      string itemCssClass = "sfRegular";
      StringBuilder stringBuilder = new StringBuilder("<div class=\"sfAddressField\">");
      switch (moduleField.AddressFieldMode)
      {
        case AddressFieldMode.FormOnly:
        case AddressFieldMode.Hybrid:
          stringBuilder.Append("<span class=\"sfLine\">{{" + moduleField.Name + " ? (" + moduleField.Name + ".City ? " + moduleField.Name + ".City : '') : ''}} {{" + moduleField.Name + " ? (" + moduleField.Name + ".CountryCode ? " + moduleField.Name + ".CountryCode : '') : ''}}</span>");
          stringBuilder.Append("<span class=\"sfLine\">{{" + moduleField.Name + " ? (" + moduleField.Name + ".Street ? " + moduleField.Name + ".Street : '') : ''}}</span>");
          break;
        case AddressFieldMode.MapOnly:
          stringBuilder.Append("<span class=\"sfLine\">{{" + moduleField.Name + " ? (" + moduleField.Name + ".Latitude ? " + moduleField.Name + ".Latitude : '') : ''}}</span>");
          stringBuilder.Append("<span class=\"sfLine\">{{" + moduleField.Name + " ? (" + moduleField.Name + ".Longitude ? " + moduleField.Name + ".Longitude : '') : ''}}</span>");
          break;
      }
      stringBuilder.Append("</div>");
      return MasterListViewGenerator.CreateDataColumnElement(parentElement, name, title, headerCssClass, itemCssClass, stringBuilder.ToString());
    }

    private static PromptDialogElement GetCannotDeleteParentItemDialog(
      ConfigElementList<PromptDialogElement> parent)
    {
      PromptDialogElement parentItemDialog = new PromptDialogElement((ConfigElement) parent)
      {
        DialogName = "cannotDeleteParentItemDialog",
        ResourceClassId = typeof (DynamicModuleResources).Name,
        Title = "PromptTitleItemCannotDeleteChildren",
        Message = "PromptMessageItemCannotDeleteChildren",
        Width = 300,
        Height = 300,
        ShowOnLoad = false
      };
      parentItemDialog.CommandsConfig.Add(DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) parentItemDialog.CommandsConfig, "Ok", "ok", CommandType.NormalButton, "LI", (string) null));
      return parentItemDialog;
    }

    private static string GetTextForCreateItemWidget() => Res.Get<ModuleBuilderResources>("CreateItem");

    private static string GetTextForDeleteActionsWidget() => Res.Get<ModuleBuilderResources>("Delete");

    private static string GetTextForPermissionsActionsWidget() => Res.Get<ModuleBuilderResources>("Permissions");

    private static string GetTextForDuplicateItemWidget() => Res.Get<ModuleBuilderResources>("DuplicateDetailsViewTitle");

    private static string GetMessageTextForDecisionScreenElement() => Res.Get<ModuleBuilderResources>("NoDynamicModuleItemsHaveBeenCreatedYet");

    private static string GetHeaderTextForActionMenuColumnElement() => Res.Get<ModuleBuilderResources>("Actions");

    private static string GetDynamicModuleSideBarTitle() => Res.Get<ModuleBuilderResources>("ManageDynamicModuleSideBarTitle");

    private static string GetDynamicModuleBackToParentText() => Res.Get<ModuleBuilderResources>("BackToParent");

    private static string GetParentItemEditLinkText() => Res.Get<DynamicModuleResources>().ParentItemsEditLink;

    private static string CreateSearchFieldsFromModule(IEnumerable<IDynamicModuleField> moduleFields)
    {
      List<IDynamicModuleField> dynamicModuleFieldList = new List<IDynamicModuleField>();
      foreach (IDynamicModuleField moduleField in moduleFields)
      {
        if (moduleField.FieldType == FieldType.ShortText || moduleField.FieldType == FieldType.LongText)
          dynamicModuleFieldList.Add(moduleField);
      }
      StringBuilder stringBuilder = new StringBuilder();
      string empty = string.Empty;
      if (dynamicModuleFieldList.Count == 1)
        return dynamicModuleFieldList[0].Name;
      for (int index = 0; index < dynamicModuleFieldList.Count - 1; ++index)
        stringBuilder.Append(dynamicModuleFieldList[index].Name + ",");
      stringBuilder.Append(dynamicModuleFieldList[dynamicModuleFieldList.Count - 1].Name);
      return stringBuilder.ToString();
    }
  }
}
