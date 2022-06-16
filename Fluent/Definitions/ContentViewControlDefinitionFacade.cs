// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.ContentViewControlDefinitionFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Fluent API facade that defines a definition for ContentViewControl.
  /// </summary>
  public class ContentViewControlDefinitionFacade : 
    IDialogsSupportableFacade<ContentViewControlDefinitionFacade>
  {
    private ContentViewControlElement contentViewControl;
    private string moduleName;
    private ConfigElement parentElement;
    private string definitionName;
    private Type contentType;
    private ModuleFacade parentFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewControlDefinitionFacade" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public ContentViewControlDefinitionFacade(
      string moduleName,
      ConfigElement parentElement,
      string definitionName,
      ModuleFacade parentFacade)
    {
      if (parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (parentElement == null)
        throw new ArgumentNullException(nameof (parentElement));
      if (string.IsNullOrEmpty(definitionName))
        throw new ArgumentNullException(nameof (definitionName));
      this.moduleName = moduleName;
      this.parentElement = parentElement;
      this.definitionName = definitionName;
      this.parentFacade = parentFacade;
      this.contentViewControl = new ContentViewControlElement(this.parentElement)
      {
        ControlDefinitionName = definitionName
      };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewControlDefinitionFacade" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public ContentViewControlDefinitionFacade(
      string moduleName,
      ConfigElement parentElement,
      string definitionName,
      Type contentType,
      ModuleFacade parentFacade)
      : this(moduleName, parentElement, definitionName, parentFacade)
    {
      this.contentType = !(contentType == (Type) null) ? contentType : throw new ArgumentNullException(nameof (contentType));
      this.contentViewControl.ContentType = contentType;
    }

    /// <summary>Returns the current content view control element.</summary>
    /// <returns></returns>
    public ContentViewControlElement Get() => this.contentViewControl;

    /// <summary>Sets the name of the content type.</summary>
    /// <param name="typeName">Name of the type.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewControlDefinitionFacade" />.
    /// </returns>
    public ContentViewControlDefinitionFacade SetContentTypeName(
      string typeName)
    {
      this.contentViewControl.ContentTypeName = !string.IsNullOrEmpty(typeName) ? typeName : throw new ArgumentNullException(nameof (typeName));
      return this;
    }

    /// <summary>Sets the type of the manager.</summary>
    /// <param name="type">The manager type.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewControlDefinitionFacade" />.
    /// </returns>
    public ContentViewControlDefinitionFacade SetManagerType(
      Type type)
    {
      this.contentViewControl.ManagerType = !(type == (Type) null) ? type : throw new ArgumentNullException(nameof (type));
      return this;
    }

    /// <summary>Sets the name of the provider.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewControlDefinitionFacade" />.
    /// </returns>
    public ContentViewControlDefinitionFacade SetProviderName(
      string providerName)
    {
      this.contentViewControl.ProviderName = providerName != null ? providerName : throw new ArgumentNullException(nameof (providerName));
      return this;
    }

    /// <summary>The control will use workflow.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewControlDefinitionFacade" />.</returns>
    public ContentViewControlDefinitionFacade UseWorkflow()
    {
      this.contentViewControl.UseWorkflow = new bool?(true);
      return this;
    }

    /// <summary>The control will not use workflow.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewControlDefinitionFacade" />.</returns>
    public ContentViewControlDefinitionFacade DoNotUseWorkflow()
    {
      this.contentViewControl.UseWorkflow = new bool?(false);
      return this;
    }

    /// <summary>Returns to parent facade.</summary>
    /// <returns>The parent facade which initialized this facade.</returns>
    public ModuleFacade Done() => this.parentFacade;

    /// <summary>Adds the master view.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public MasterViewDefinitionFacade AddMasterView(string viewName)
    {
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentNullException(nameof (viewName));
      return new MasterViewDefinitionFacade(this.moduleName, this.definitionName, this.contentType, this.contentViewControl.ViewsConfig, viewName, this);
    }

    /// <summary>Adds the detail view.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DetailViewDefinitionFacade AddDetailView(string viewName)
    {
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentNullException(nameof (viewName));
      return new DetailViewDefinitionFacade(this.moduleName, this.definitionName, this.contentType, this.contentViewControl.ViewsConfig, viewName, this);
    }

    /// <summary>Adds a dialog.</summary>
    /// <typeparam name="TDialog">The type of the dialog.</typeparam>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddDialog<TDialog>(
      string openOnCommandName)
      where TDialog : DialogBase
    {
      if (string.IsNullOrEmpty(openOnCommandName))
        throw new ArgumentNullException(nameof (openOnCommandName));
      return new DialogDefinitionFacade<ContentViewControlDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.contentViewControl.DialogsConfig, this, openOnCommandName, typeof (TDialog));
    }

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddInsertDialog(
      string viewName)
    {
      return this.AddInsertDialog(viewName, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddInsertDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddInsertDialog(
      string viewName,
      string defName,
      string backText)
    {
      return this.AddInsertDialog(viewName, defName, backText, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddInsertDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddInsertDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType,
      string openOnCommandName)
    {
      if (viewName.IsNullOrWhitespace())
        throw new ArgumentNullException(nameof (viewName));
      return new DialogDefinitionFacade<ContentViewControlDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.contentViewControl.DialogsConfig, this, viewName, CommonDialog.Insert, backText, string.Empty, parentId, parentType, defName, openOnCommandName, string.Empty);
    }

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddEditDialog(
      string viewName)
    {
      return this.AddEditDialog(viewName, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddEditDialog(
      string viewName,
      string defName)
    {
      return this.AddEditDialog(viewName, defName, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddEditDialog(
      string viewName,
      string defName,
      string backText)
    {
      return this.AddEditDialog(viewName, defName, backText, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddEditDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddEditDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType,
      string openOnCommandName)
    {
      if (viewName.IsNullOrWhitespace())
        throw new ArgumentNullException(nameof (viewName));
      return new DialogDefinitionFacade<ContentViewControlDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.contentViewControl.DialogsConfig, this, viewName, CommonDialog.Edit, backText, string.Empty, parentId, parentType, defName, openOnCommandName, string.Empty);
    }

    /// <summary>Adds a preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddPreviewDialog(
      string viewName)
    {
      return this.AddPreviewDialog(viewName, string.Empty, string.Empty, string.Empty, (Type) null);
    }

    /// <summary>Adds a preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddPreviewDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddPreviewDialog(
      string viewName,
      string defName,
      string backText)
    {
      return this.AddPreviewDialog(viewName, defName, backText, string.Empty, (Type) null);
    }

    /// <summary>Adds a preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddPreviewDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType)
    {
      if (viewName.IsNullOrWhitespace())
        throw new ArgumentNullException(nameof (viewName));
      return new DialogDefinitionFacade<ContentViewControlDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.contentViewControl.DialogsConfig, this, viewName, CommonDialog.Preview, backText, string.Empty, parentId, parentType, defName, string.Empty, string.Empty);
    }

    /// <summary>Adds a permissions dialog.</summary>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddPermissionsDialog() => this.AddPermissionsDialog(string.Empty, string.Empty, string.Empty, string.Empty, (Type) null);

    /// <summary>Adds a permissions dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddPermissionsDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddPermissionsDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddPermissionsDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddPermissionsDialog(
      string backText,
      string title,
      string permissionSetName,
      string openOnCommandName,
      Type securedObjectType)
    {
      return new DialogDefinitionFacade<ContentViewControlDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.contentViewControl.DialogsConfig, this, CommonDialog.Permissions, backText, title, permissionSetName, openOnCommandName, securedObjectType);
    }

    /// <summary>Adds a custom fields dialog.</summary>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddCustomFieldsDialog() => this.AddCustomFieldsDialog(string.Empty, string.Empty, string.Empty);

    /// <summary>Adds a custom fields dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="itemsName">Name of multiple items of the related type.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddCustomFieldsDialog(
      string backText,
      string title,
      string itemsName)
    {
      return new DialogDefinitionFacade<ContentViewControlDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.contentViewControl.DialogsConfig, this, CommonDialog.CustomFields, backText, title, itemsName);
    }

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryComparisonDialog(
      string viewName)
    {
      return this.AddHistoryComparisonDialog(viewName, string.Empty, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryComparisonDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryComparisonDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryComparisonDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryComparisonDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryComparisonDialog(
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
      return new DialogDefinitionFacade<ContentViewControlDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.contentViewControl.DialogsConfig, this, viewName, CommonDialog.HistoryComparison, backText, title, parentId, parentType, defName, string.Empty, editCommandName);
    }

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryGridDialog(
      string viewName)
    {
      return this.AddHistoryGridDialog(viewName, string.Empty, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryGridDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryGridDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryGridDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryGridDialog(
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
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryGridDialog(
      string viewName,
      string defName,
      string backText,
      string title,
      string parentId,
      Type parentType,
      string editCommandName)
    {
      return new DialogDefinitionFacade<ContentViewControlDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.contentViewControl.DialogsConfig, this, viewName, CommonDialog.HistoryGrid, backText, title, parentId, parentType, defName, string.Empty, editCommandName);
    }

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryPreviewDialog(
      string viewName)
    {
      return this.AddHistoryPreviewDialog(viewName, string.Empty, string.Empty, string.Empty, (Type) null, string.Empty);
    }

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryPreviewDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryPreviewDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryPreviewDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddHistoryPreviewDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType,
      string editCommandName)
    {
      return new DialogDefinitionFacade<ContentViewControlDefinitionFacade>(this.moduleName, this.definitionName, this.contentType, (ConfigElementCollection) this.contentViewControl.DialogsConfig, this, viewName, CommonDialog.HistoryPreview, backText, string.Empty, parentId, parentType, defName, string.Empty, editCommandName);
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddUploadDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddUploadDialog(
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
    public DialogDefinitionFacade<ContentViewControlDefinitionFacade> AddUploadDialog(
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
      return new DialogDefinitionFacade<ContentViewControlDefinitionFacade>(this.moduleName, this.definitionName, cntType, (ConfigElementCollection) this.contentViewControl.DialogsConfig, this, CommonDialog.Upload, providerName, itemName, itemsName, parentItemName, parentType, childServiceUrl, parentServiceUrl, openOnCommandName);
    }
  }
}
