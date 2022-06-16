// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.IAnyContentManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Fluent.AnyContent
{
  public interface IAnyContentManager : 
    IContentLifecycleManager,
    IManager,
    IDisposable,
    IProviderResolver,
    IContentManager
  {
    void Initialize(IManager managerToWrap, Type itemType);

    /// <summary>
    /// Checks in the item in the "temp" state. Item becomes master after the check in.
    /// </summary>
    /// <param name="item">Item in temp state that is to be checked in.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>An item in master state.</returns>
    Content CheckIn(Content item, CultureInfo culture, bool deleteTemp = true);

    /// <summary>
    /// Checks out the content in master state. Item becomes temp after the check out.
    /// </summary>
    /// <param name="item">Item in master state that is to be checked out.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>A content that was checked out in temp state.</returns>
    Content CheckOut(Content item, CultureInfo culture);

    /// <summary>
    /// Edits the content in live state. Item becomes master after the edit.
    /// </summary>
    /// <param name="item">Item in live state that is to be edited.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>A content that was edited in master state.</returns>
    Content Edit(Content item, CultureInfo culture);

    /// <summary>
    /// Publishes the content in master state. Item becomes live after the publish.
    /// </summary>
    /// <param name="item">Item in master state that is to be published.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>Published content item</returns>
    Content Publish(Content item, CultureInfo culture);

    Content Publish(Content item, DateTime publicationDate);

    Content Publish(Content item, DateTime? publicationDate, CultureInfo culture);

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>Master (draft) state.</returns>
    Content Unpublish(Content item, CultureInfo culture);

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
    Content GetLive(Content cnt, CultureInfo culture);

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Item to get a temp for</param>
    /// <returns>Temp version of <paramref name="cnt" />, if it exists.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    Content GetTemp(Content cnt, CultureInfo culture);

    /// <summary>
    /// Returns ID of the user that checked out the master item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <returns>ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.</returns>
    Guid GetCheckedOutBy(Content item, CultureInfo culture);

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    bool IsCheckedOut(Content item, CultureInfo culture);

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
    bool IsCheckedOutBy(Content item, Guid userId, CultureInfo culture);

    /// <summary>
    /// Copis all proeprties from the source item to the destination using the specified culture
    /// </summary>
    /// <param name="source">source item</param>
    /// <param name="destination">destination item</param>
    /// <param name="culture"></param>
    void Copy(Content source, Content destination, CultureInfo culture);
  }
}
