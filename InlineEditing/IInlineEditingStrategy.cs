// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.IInlineEditingStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services.InlineEditing;

namespace Telerik.Sitefinity.InlineEditing
{
  /// <summary>Base interface for all inline editing strategies</summary>
  public interface IInlineEditingStrategy
  {
    /// <summary>
    /// Executes specified workflow operation to the item from specified type with specified id.
    /// After the operation is executed returns status which is used from the service to determine whether
    /// a page workflow operation requires to be executed
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="itmeType">The item type.</param>
    /// <param name="provider">The provider name.</param>
    /// <param name="operation">The workflow operation.</param>
    WorkflowOperationResultModel ExecuteOperation(
      Guid itemId,
      Type itmeType,
      string provider,
      InlineEditingOperation operation,
      string customOperation = null);

    /// <summary>
    /// If an item supports a lifecyle then this method gets the temp version of the item from specified type with specified id.
    /// If the item already has a temp item it is returned, otherwise a new temp item is created.
    /// If the item doesnot support lifecycle this method returns just the id of the item which will be edited.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="itmeType">The item type.</param>
    /// <param name="provider">The provider name.</param>
    /// <returns>Returns item id</returns>
    Guid GetEditableItemId(Guid itemId, Type itmeType, string provider);

    /// <summary>
    /// Applies modified fields to the item which is edited from specified type with specified id.
    /// </summary>
    /// <param name="fields">Collection of modified fields and values.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="itmeType">The item type.</param>
    /// <param name="provider">The provider name.</param>
    void SaveEditableItemFields(
      FieldValueModel[] fields,
      Guid itemId,
      Type itmeType,
      string provider);

    /// <summary>
    /// Deletes the item from specified type with specified id. This operation is needed only for lifecycle item.
    /// On exit editing when this operation is called it should clear the temp item which is created on enter edit mode(if any).
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="itmeType">The item type.</param>
    /// <param name="provider">The provider name.</param>
    void DeleteEditableItem(Guid itemId, Type itmeType, string provider);

    /// <summary>
    /// Gets the status of the item (Published, Unpublished, Awaiting approval)
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="itmeType">The item type.</param>
    /// <param name="provider">The provider name.</param>
    LifecycleStatusModel GetItemStatus(
      Guid itemId,
      Type itmeType,
      string provider);

    /// <summary>
    /// Gets the status of the item (Published, Unpublished, Awaiting approval)
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    LifecycleStatusModel GetItemStatus(object item);

    /// <summary>
    /// Gets the item with the specified id, type and provider.
    /// </summary>
    /// <param name="itemId">The id.</param>
    /// <param name="itmeType">The type.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Returns the item itself</returns>
    object GetItem(Guid itemId, Type itmeType, string provider);

    /// <summary>Gets the URL for editing item in the backend.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageNode">Current page node.</param>
    /// <returns></returns>
    string GetDetailsViewUrl(Type itemType, PageNode pageNode);
  }
}
