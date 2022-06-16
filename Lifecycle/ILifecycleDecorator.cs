// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.ILifecycleDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;

namespace Telerik.Sitefinity.Lifecycle
{
  public interface ILifecycleDecorator
  {
    /// <summary>
    /// Checks in the item in the "temp" state. Item becomes master after the check in.
    /// </summary>
    /// <param name="item">Item in temp state that is to be checked in.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>An item in master state.</returns>
    ILifecycleDataItem CheckIn(
      ILifecycleDataItem item,
      CultureInfo culture = null,
      bool deleteTemp = true);

    /// <summary>
    /// Checks out the content in master state. Item becomes temp after the check out.
    /// </summary>
    /// <param name="item">Item in master state that is to be checked out.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>A content that was checked out in temp state.</returns>
    ILifecycleDataItem CheckOut(ILifecycleDataItem item, CultureInfo culture = null);

    /// <summary>
    /// Edits the content in live state. Item becomes master after the edit.
    /// </summary>
    /// <param name="item">Item in live state that is to be edited.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>A content that was edited in master state.</returns>
    ILifecycleDataItem Edit(ILifecycleDataItem item, CultureInfo culture = null);

    /// <summary>
    /// Publishes the content in master state. Item becomes live after the publish.
    /// </summary>
    /// <param name="item">Item in master state that is to be published.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>Published content item</returns>
    ILifecycleDataItem Publish(ILifecycleDataItem item, CultureInfo culture = null);

    /// <summary>
    /// Publishes the content in master state. Item becomes live after the publish with the specified publication date.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="publicationDate">The publication date.</param>
    /// <param name="culture">The culture in which to perform the operation.</param>
    /// <returns>Published content item</returns>
    ILifecycleDataItem PublishWithSpecificDate(
      ILifecycleDataItem item,
      DateTime publicationDate,
      CultureInfo culture = null);

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Item in master state that is to be published.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>Master (draft) state.</returns>
    ILifecycleDataItem Unpublish(ILifecycleDataItem item, CultureInfo culture = null);

    /// <summary>
    /// Discards the temp item in the specified culture for the specified master item. All changes in
    /// the discarded item are lost.
    /// </summary>
    /// <param name="liveItem">The master item to clear temp item from.</param>
    /// <param name="culture">The culture of the temp item to discard.</param>
    void DiscardTemp(ILifecycleDataItem masterItem, CultureInfo culture = null);

    /// <summary>
    /// Discards all temp item items for the specified master item. All changes in the discarded items
    /// are lost.
    /// </summary>
    /// <param name="liveItem">The master item to clear temp items from.</param>
    void DiscardAllTemps(ILifecycleDataItem masterItem);

    /// <summary>
    /// Returns ID of the user that checked out the master item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <returns>ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.</returns>
    Guid GetCheckedOutBy(ILifecycleDataItem item, CultureInfo culture = null);

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    bool IsCheckedOut(ILifecycleDataItem item, CultureInfo culture = null);

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    ///     
    ///             /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>True if it was checked out by a user with the specified id, false otherwize</returns>
    bool IsCheckedOutBy(ILifecycleDataItem item, Guid userId, CultureInfo culture = null);

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>Public (live) version of <paramref name="cnt" />, if it exists</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    ILifecycleDataItem GetLive(ILifecycleDataItem cnt, CultureInfo culture = null);

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Item to get a temp for</param>
    /// <returns>Temp version of <paramref name="cnt" />, if it exists.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    ILifecycleDataItem GetTemp(ILifecycleDataItem cnt, CultureInfo culture = null);

    /// <summary>
    /// Accepts a content item and returns an item in master state
    /// </summary>
    /// <param name="cnt">Item item whose master to get</param>
    /// <returns>
    /// If <paramref name="cnt" /> is master itself, returns cnt.
    /// Otherwize, looks up the master associated with <paramref name="cnt" /> and returns it.
    /// When there is no master, an exception will be thrown.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="cnt" />.</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    ILifecycleDataItem GetMaster(ILifecycleDataItem cnt);

    /// <summary>
    /// Copies the source item properties to the destination item ones
    /// </summary>
    /// <param name="source">source item</param>
    /// <param name="destination">destination item</param>
    /// <param name="culture">If no culture is specified the current thread UI culture will be used in multilingual mode</param>
    void CopyProperties(
      ILifecycleDataItem source,
      ILifecycleDataItem destination,
      CultureInfo culture = null);

    /// <summary>
    /// Gets the or create language data for the specified item in the specified culture
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    LanguageData GetOrCreateLanguageData(
      ILifecycleDataItem dataItem,
      CultureInfo culture = null);
  }
}
