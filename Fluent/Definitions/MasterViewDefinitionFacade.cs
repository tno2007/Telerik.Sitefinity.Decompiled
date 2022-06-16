// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Fluent API facade that defines a definition for MasterGridView.
  /// </summary>
  public class MasterViewDefinitionFacade : 
    BaseMasterDefinitionFacade<MasterGridViewElement, MasterViewDefinitionFacade>,
    IDialogsSupportableFacade<MasterViewDefinitionFacade>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public MasterViewDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementDictionary<string, ContentViewDefinitionElement> parentElement,
      string viewName,
      ContentViewControlDefinitionFacade parentFacade)
      : base(moduleName, definitionName, contentType, parentElement, viewName, parentFacade)
    {
    }

    /// <summary>
    /// Sets comma separated list of fields (properties) on which the search ought to be performed
    /// </summary>
    /// <param name="searchFields">The search fields.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    public MasterViewDefinitionFacade SetSearchFields(string searchFields)
    {
      this.ContentView.SearchFields = !string.IsNullOrEmpty(searchFields) ? searchFields : throw new ArgumentNullException(nameof (searchFields));
      return this;
    }

    /// <summary>
    /// Sets comma separated list of fields (properties) of type <see cref="!:Lstring" /> on which the search ought to be performed
    /// </summary>
    /// <param name="extendedSearchFields">The extended search fields.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    public MasterViewDefinitionFacade SetExtendedSearchFields(
      string extendedSearchFields)
    {
      this.ContentView.ExtendedSearchFields = !string.IsNullOrEmpty(extendedSearchFields) ? extendedSearchFields : throw new ArgumentNullException(nameof (extendedSearchFields));
      return this;
    }

    /// <summary>Sets the css class of the master view.</summary>
    /// <param name="cssClass">The CssClass that should be applied to the master view.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    public MasterViewDefinitionFacade SetCssClass(string cssClass)
    {
      this.ContentView.GridCssClass = !string.IsNullOrEmpty(cssClass) ? cssClass : throw new ArgumentNullException(nameof (cssClass));
      return this;
    }

    /// <summary>
    /// Sets filter expression that will show only items that aren't published or scheduled
    /// </summary>
    /// <param name="filter">The draft filter.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    public MasterViewDefinitionFacade SetDraftFilter(string filter)
    {
      this.ContentView.DraftFilter = !string.IsNullOrEmpty(filter) ? filter : throw new ArgumentNullException(nameof (filter));
      return this;
    }

    /// <summary>
    /// Sets filter expression that will show only items that are published (live)
    /// </summary>
    /// <param name="filter">The published filter.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    public MasterViewDefinitionFacade SetPublishedFilter(string filter)
    {
      this.ContentView.PublishedFilter = !string.IsNullOrEmpty(filter) ? filter : throw new ArgumentNullException(nameof (filter));
      return this;
    }

    /// <summary>
    /// Sets filter expresion that will show only items that are scheduled
    /// </summary>
    /// <param name="filter">The scheduled filter.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    public MasterViewDefinitionFacade SetScheduledFilter(string filter)
    {
      this.ContentView.ScheduledFilter = !string.IsNullOrEmpty(filter) ? filter : throw new ArgumentNullException(nameof (filter));
      return this;
    }

    /// <summary>
    /// Sets filter expresion that will show only items that are pending approval
    /// </summary>
    /// <param name="filter">The pending approval filter.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    public MasterViewDefinitionFacade SetPendingApprovalFilter(
      string filter)
    {
      this.ContentView.PendingApprovalFilter = !string.IsNullOrEmpty(filter) ? filter : throw new ArgumentNullException(nameof (filter));
      return this;
    }

    /// <summary>
    /// Sets the message to be shown when propting the user if they are sure they want to delete single item.
    /// </summary>
    /// <param name="message">The delete confirmation message.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    public MasterViewDefinitionFacade SetDeleteSingleConfirmationMessage(
      string message)
    {
      this.ContentView.DeleteSingleConfirmationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message to be shown when propting the user if they are sure they want to delete multiple items.
    /// </summary>
    /// <param name="message">The delete confirmation message.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    public MasterViewDefinitionFacade SetDeleteMultipleConfirmationMessage(
      string message)
    {
      this.ContentView.DeleteMultipleConfirmationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>Sets the landing page id.</summary>
    /// <param name="landingPageId">The landing page id.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    internal MasterViewDefinitionFacade SetLandingPageId(
      Guid landingPageId)
    {
      this.ContentView.LandingPageId = !(landingPageId == Guid.Empty) ? new Guid?(landingPageId) : throw new ArgumentException("id cannot be empty guid.");
      return this;
    }

    /// <summary>
    /// The master view will not bind the current ItemsListBase on the client when the page is loaded.
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    public MasterViewDefinitionFacade DoNotBindOnClientWhenPageIsLoaded()
    {
      this.ContentView.DoNotBindOnClientWhenPageIsLoaded = true;
      return this;
    }

    /// <summary>
    /// The master view will bind the current ItemsListBase on the client when the page is loaded.
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.MasterViewDefinitionFacade" />.</returns>
    public MasterViewDefinitionFacade BindOnClientWhenPageIsLoaded()
    {
      this.ContentView.DoNotBindOnClientWhenPageIsLoaded = false;
      return this;
    }

    /// <summary>Defines the content view.</summary>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    protected override void DefineContentView(
      ConfigElementDictionary<string, ContentViewDefinitionElement> parentElement,
      string viewName)
    {
      MasterGridViewElement masterGridViewElement = new MasterGridViewElement((ConfigElement) parentElement);
      masterGridViewElement.ViewName = viewName;
      masterGridViewElement.ViewType = typeof (MasterGridView);
      masterGridViewElement.DisplayMode = FieldDisplayMode.Read;
      masterGridViewElement.ItemsPerPage = new int?(50);
      masterGridViewElement.ExtendedSearchFields = "Title";
      masterGridViewElement.SortExpression = "LastModified DESC";
      this.ContentView = masterGridViewElement;
    }

    /// <summary>Adds a dialog.</summary>
    /// <typeparam name="TDialog">The type of the dialog.</typeparam>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddDialog<TDialog>(
      string openOnCommandName)
      where TDialog : DialogBase
    {
      if (string.IsNullOrEmpty(openOnCommandName))
        throw new ArgumentNullException(nameof (openOnCommandName));
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, openOnCommandName, typeof (TDialog));
    }

    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddDialog(
      string dialogType,
      string openOnCommandName)
    {
      if (string.IsNullOrEmpty(openOnCommandName))
        throw new ArgumentNullException(nameof (openOnCommandName));
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, openOnCommandName, TypeResolutionService.ResolveType(dialogType));
    }

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddInsertDialog(
      string viewName)
    {
      return this.AddInsertDialog(viewName, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddInsertDialog(
      string viewName,
      string defName)
    {
      return this.AddInsertDialog(viewName, defName, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddInsertDialog(
      string viewName,
      string defName,
      string backText)
    {
      return this.AddInsertDialog(viewName, defName, backText, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddInsertDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType)
    {
      return this.AddInsertDialog(viewName, defName, backText, parentId, parentType, string.Empty);
    }

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddInsertDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType,
      string openOnCommandName)
    {
      if (viewName.IsNullOrWhitespace())
        throw new ArgumentNullException(nameof (viewName));
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, viewName, CommonDialog.Insert, backText, string.Empty, parentId, parentType, defName, openOnCommandName, string.Empty);
    }

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddEditDialog(
      string viewName)
    {
      return this.AddEditDialog(viewName, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddEditDialog(
      string viewName,
      string defName)
    {
      return this.AddEditDialog(viewName, defName, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddEditDialog(
      string viewName,
      string defName,
      string backText)
    {
      return this.AddEditDialog(viewName, defName, backText, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddEditDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType)
    {
      return this.AddEditDialog(viewName, defName, backText, parentId, parentType, string.Empty);
    }

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddEditDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType,
      string openOnCommandName)
    {
      if (viewName.IsNullOrWhitespace())
        throw new ArgumentNullException(nameof (viewName));
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, viewName, CommonDialog.Edit, backText, string.Empty, parentId, parentType, defName, openOnCommandName, string.Empty);
    }

    /// <summary>Adds a preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddPreviewDialog(
      string viewName)
    {
      return this.AddPreviewDialog(viewName, string.Empty, string.Empty, string.Empty, (Type) null);
    }

    /// <summary>Adds a preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddPreviewDialog(
      string viewName,
      string defName)
    {
      return this.AddPreviewDialog(viewName, defName, string.Empty, string.Empty, (Type) null);
    }

    /// <summary>Adds a preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddPreviewDialog(
      string viewName,
      string defName,
      string backText)
    {
      return this.AddPreviewDialog(viewName, this.definitionName, backText, string.Empty, (Type) null);
    }

    /// <summary>Adds a preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddPreviewDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType)
    {
      if (viewName.IsNullOrWhitespace())
        throw new ArgumentNullException(nameof (viewName));
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, viewName, CommonDialog.Preview, backText, string.Empty, parentId, parentType, defName, string.Empty, string.Empty);
    }

    /// <summary>Adds a permissions dialog.</summary>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddPermissionsDialog() => this.AddPermissionsDialog(string.Empty, string.Empty, string.Empty, string.Empty, (Type) null);

    /// <summary>Adds a permissions dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddPermissionsDialog(
      string backText,
      string title)
    {
      return this.AddPermissionsDialog(backText, title, string.Empty, string.Empty, (Type) null);
    }

    /// <summary>Adds a permissions dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddPermissionsDialog(
      string backText,
      string title,
      string permissionSetName)
    {
      return this.AddPermissionsDialog(backText, title, permissionSetName, string.Empty, (Type) null);
    }

    /// <summary>Adds a permissions dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddPermissionsDialog(
      string backText,
      string title,
      string permissionSetName,
      string openOnCommandName)
    {
      return this.AddPermissionsDialog(backText, title, permissionSetName, openOnCommandName, (Type) null);
    }

    /// <summary>Adds a permissions dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <param name="securedObjectType">Type of the secured object.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddPermissionsDialog(
      string backText,
      string title,
      string permissionSetName,
      string openOnCommandName,
      Type securedObjectType)
    {
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, CommonDialog.Permissions, backText, title, permissionSetName, openOnCommandName, securedObjectType);
    }

    /// <summary>Adds a custom fields dialog.</summary>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddCustomFieldsDialog() => this.AddCustomFieldsDialog(string.Empty, string.Empty, string.Empty);

    /// <summary>Adds a custom fields dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="itemsName">Name of multiple items of the related type.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddCustomFieldsDialog(
      string backText,
      string title,
      string itemsName)
    {
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, CommonDialog.CustomFields, backText, title, itemsName);
    }

    /// <summary>Adds a custom fields dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="itemsName">Name of multiple items of the related type.</param>
    /// <param name="contentType">The content type.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddCustomFieldsDialog(
      string backText,
      string title,
      string itemsName,
      Type contentType)
    {
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, contentType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, CommonDialog.CustomFields, backText, title, itemsName);
    }

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryComparisonDialog(
      string viewName)
    {
      return this.AddHistoryComparisonDialog(viewName, string.Empty, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryComparisonDialog(
      string viewName,
      string defName)
    {
      return this.AddHistoryComparisonDialog(viewName, defName, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryComparisonDialog(
      string viewName,
      string defName,
      string backText)
    {
      return this.AddHistoryComparisonDialog(viewName, defName, backText, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryComparisonDialog(
      string viewName,
      string defName,
      string backText,
      string title)
    {
      return this.AddHistoryComparisonDialog(viewName, defName, backText, title, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryComparisonDialog(
      string viewName,
      string defName,
      string backText,
      string title,
      string parentId,
      Type parentType)
    {
      return this.AddHistoryComparisonDialog(viewName, defName, backText, title, parentId, parentType, string.Empty);
    }

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="editCommandName">Name of the edit command.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryComparisonDialog(
      string viewName,
      string defName,
      string backText,
      string title,
      string parentId,
      Type parentType,
      string editCommandName)
    {
      if (viewName.IsNullOrWhitespace())
        throw new ArgumentNullException(nameof (viewName));
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, viewName, CommonDialog.HistoryComparison, backText, title, parentId, parentType, defName, string.Empty, editCommandName);
    }

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryGridDialog(
      string viewName)
    {
      return this.AddHistoryGridDialog(viewName, string.Empty, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryGridDialog(
      string viewName,
      string defName)
    {
      return this.AddHistoryGridDialog(viewName, defName, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryGridDialog(
      string viewName,
      string defName,
      string backText)
    {
      return this.AddHistoryGridDialog(viewName, defName, backText, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryGridDialog(
      string viewName,
      string defName,
      string backText,
      string title)
    {
      return this.AddHistoryGridDialog(viewName, defName, backText, title, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryGridDialog(
      string viewName,
      string defName,
      string backText,
      string title,
      string parentId,
      Type parentType)
    {
      return this.AddHistoryGridDialog(viewName, defName, backText, title, parentId, parentType, string.Empty);
    }

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="editCommandName">Name of the edit command.</param>
    /// \
    ///             <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryGridDialog(
      string viewName,
      string defName,
      string backText,
      string title,
      string parentId,
      Type parentType,
      string editCommandName)
    {
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, viewName, CommonDialog.HistoryGrid, backText, title, parentId, parentType, defName, string.Empty, editCommandName);
    }

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryPreviewDialog(
      string viewName)
    {
      return this.AddHistoryPreviewDialog(viewName, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryPreviewDialog(
      string viewName,
      string defName)
    {
      return this.AddHistoryPreviewDialog(viewName, defName, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryPreviewDialog(
      string viewName,
      string defName,
      string backText)
    {
      return this.AddHistoryPreviewDialog(viewName, defName, backText, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryPreviewDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType)
    {
      return this.AddHistoryPreviewDialog(viewName, defName, backText, parentId, parentType, string.Empty);
    }

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="editCommandName">Name of the edit command.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddHistoryPreviewDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType,
      string editCommandName)
    {
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, viewName, CommonDialog.HistoryPreview, backText, string.Empty, parentId, parentType, defName, string.Empty, editCommandName);
    }

    /// <summary>Adds an upload dialog.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemName">Localizable string that represents the name of the item in singular.</param>
    /// <param name="itemNames">Localizable string that represents the name of the item in plural.</param>
    /// <param name="parentItemName">Localizable string that represents the name of the parent item in singular.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="childServiceUrl">The child service URL.</param>
    /// <param name="parentServiceUrl">The parent service URL.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddUploadDialog(
      string providerName,
      string itemName,
      string itemsName,
      string parentItemName,
      Type parentType,
      string childServiceUrl,
      string parentServiceUrl)
    {
      return this.AddUploadDialog(providerName, itemName, itemsName, parentItemName, parentType, childServiceUrl, parentServiceUrl, string.Empty, this.contentType);
    }

    /// <summary>Adds an upload dialog.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemName">Localizable string that represents the name of the item in singular.</param>
    /// <param name="itemNames">Localizable string that represents the name of the item in plural.</param>
    /// <param name="parentItemName">Localizable string that represents the name of the parent item in singular.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="childServiceUrl">The child service URL.</param>
    /// <param name="parentServiceUrl">The parent service URL.</param>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddUploadDialog(
      string providerName,
      string itemName,
      string itemsName,
      string parentItemName,
      Type parentType,
      string childServiceUrl,
      string parentServiceUrl,
      string openOnCommandName)
    {
      return this.AddUploadDialog(providerName, itemName, itemsName, parentItemName, parentType, childServiceUrl, parentServiceUrl, openOnCommandName, this.contentType);
    }

    /// <summary>Adds an upload dialog.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemName">Localizable string that represents the name of the item in singular.</param>
    /// <param name="itemsName">Name of the items.</param>
    /// <param name="parentItemName">Localizable string that represents the name of the parent item in singular.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="childServiceUrl">The child service URL.</param>
    /// <param name="parentServiceUrl">The parent service URL.</param>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <param name="cntType">Type of the content.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<MasterViewDefinitionFacade> AddUploadDialog(
      string providerName,
      string itemName,
      string itemsName,
      string parentItemName,
      Type parentType,
      string childServiceUrl,
      string parentServiceUrl,
      string openOnCommandName,
      Type cntType)
    {
      if (parentType == (Type) null)
        throw new ArgumentNullException(nameof (parentType));
      if (childServiceUrl.IsNullOrWhitespace())
        throw new ArgumentNullException(nameof (childServiceUrl));
      if (parentServiceUrl.IsNullOrWhitespace())
        throw new ArgumentNullException(nameof (parentServiceUrl));
      if (cntType == (Type) null)
        throw new ArgumentNullException(nameof (cntType));
      return new DialogDefinitionFacade<MasterViewDefinitionFacade>(this.moduleName, this.definitionName, cntType, (ConfigElementCollection) this.ContentView.DialogsConfig, this, CommonDialog.Upload, providerName, itemName, itemsName, parentItemName, parentType, childServiceUrl, parentServiceUrl, openOnCommandName);
    }
  }
}
