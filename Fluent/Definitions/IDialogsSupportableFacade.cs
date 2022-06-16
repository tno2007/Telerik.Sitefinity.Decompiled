// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.IDialogsSupportableFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Provides all members that should be impelemented by facades supporting dialogs
  /// </summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  public interface IDialogsSupportableFacade<TParentFacade> where TParentFacade : class
  {
    /// <summary>Adds a dialog.</summary>
    /// <typeparam name="TDialog">The type of the dialog.</typeparam>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddDialog<TDialog>(
      string openOnCommandName)
      where TDialog : DialogBase;

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddInsertDialog(string viewName);

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddInsertDialog(
      string viewName,
      string defName);

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddInsertDialog(
      string viewName,
      string defName,
      string backText);

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddInsertDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType);

    /// <summary>Adds an insert dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddInsertDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType,
      string openOnCommandName);

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddEditDialog(string viewName);

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddEditDialog(
      string viewName,
      string defName);

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddEditDialog(
      string viewName,
      string defName,
      string backText);

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddEditDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType);

    /// <summary>Adds an edit dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddEditDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType,
      string openOnCommandName);

    /// <summary>Adds a preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddPreviewDialog(string viewName);

    /// <summary>Adds a preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddPreviewDialog(
      string viewName,
      string defName);

    /// <summary>Adds a preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddPreviewDialog(
      string viewName,
      string defName,
      string backText);

    /// <summary>Adds a preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddPreviewDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType);

    /// <summary>Adds a permissions dialog.</summary>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddPermissionsDialog();

    /// <summary>Adds a permissions dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddPermissionsDialog(
      string backText,
      string title);

    /// <summary>Adds a permissions dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddPermissionsDialog(
      string backText,
      string title,
      string permissionSetName);

    /// <summary>Adds a permissions dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddPermissionsDialog(
      string backText,
      string title,
      string permissionSetName,
      string openOnCommandName);

    /// <summary>Adds a permissions dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    /// <param name="securedObjectType">Type of the secured object.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddPermissionsDialog(
      string backText,
      string title,
      string permissionSetName,
      string openOnCommandName,
      Type securedObjectType);

    /// <summary>Adds a custom fields dialog.</summary>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddCustomFieldsDialog();

    /// <summary>Adds a custom fields dialog.</summary>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="itemsName">Name of multiple items of the related type.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddCustomFieldsDialog(
      string backText,
      string title,
      string itemsName);

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryComparisonDialog(
      string viewName);

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryComparisonDialog(
      string viewName,
      string defName);

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryComparisonDialog(
      string viewName,
      string defName,
      string backText);

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryComparisonDialog(
      string viewName,
      string defName,
      string backText,
      string title);

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryComparisonDialog(
      string viewName,
      string defName,
      string backText,
      string title,
      string parentId,
      Type parentType);

    /// <summary>Adds a history comparison dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="editCommandName">Name of the edit command.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryComparisonDialog(
      string viewName,
      string defName,
      string backText,
      string title,
      string parentId,
      Type parentType,
      string editCommandName);

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryGridDialog(string viewName);

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryGridDialog(
      string viewName,
      string defName);

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryGridDialog(
      string viewName,
      string defName,
      string backText);

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryGridDialog(
      string viewName,
      string defName,
      string backText,
      string title);

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryGridDialog(
      string viewName,
      string defName,
      string backText,
      string title,
      string parentId,
      Type parentType);

    /// <summary>Adds a history grid dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="editCommandName">Name of the edit command.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryGridDialog(
      string viewName,
      string defName,
      string backText,
      string title,
      string parentId,
      Type parentType,
      string editCommandName);

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryPreviewDialog(
      string viewName);

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryPreviewDialog(
      string viewName,
      string defName);

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryPreviewDialog(
      string viewName,
      string defName,
      string backText);

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryPreviewDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType);

    /// <summary>Adds a history preview dialog.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="defName">Name of the definition.</param>
    /// <param name="backText">The text to display in the back label of the dialog.</param>
    /// <param name="parentId">ID of the parent.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="editCommandName">Name of the edit command.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddHistoryPreviewDialog(
      string viewName,
      string defName,
      string backText,
      string parentId,
      Type parentType,
      string editCommandName);

    /// <summary>Adds an upload dialog.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemName">Localizable string that represents the name of the item in singular.</param>
    /// <param name="itemNames">Localizable string that represents the name of the item in plural.</param>
    /// <param name="parentItemName">Localizable string that represents the name of the parent item in singular.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="childServiceUrl">The child service URL.</param>
    /// <param name="parentServiceUrl">The parent service URL.</param>
    /// <returns></returns>
    DialogDefinitionFacade<TParentFacade> AddUploadDialog(
      string providerName,
      string itemName,
      string itemsName,
      string parentItemName,
      Type parentType,
      string childServiceUrl,
      string parentServiceUrl);

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
    DialogDefinitionFacade<TParentFacade> AddUploadDialog(
      string providerName,
      string itemName,
      string itemsName,
      string parentItemName,
      Type parentType,
      string childServiceUrl,
      string parentServiceUrl,
      string openOnCommandName);

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
    DialogDefinitionFacade<TParentFacade> AddUploadDialog(
      string providerName,
      string itemName,
      string itemsName,
      string parentItemName,
      Type parentType,
      string childServiceUrl,
      string parentServiceUrl,
      string openOnCommandName,
      Type cntType);
  }
}
