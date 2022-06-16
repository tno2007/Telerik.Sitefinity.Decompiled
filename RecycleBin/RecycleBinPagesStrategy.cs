// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.RecycleBinPagesStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RecycleBin.Conflicts;
using Telerik.Sitefinity.RecycleBin.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>
  /// Recycle Bin strategy for <see cref="T:Telerik.Sitefinity.Modules.Pages.PageManager" /> managers.
  /// Responsible for <see cref="T:Telerik.Sitefinity.Pages.Model.PageData" /> and <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> recycling.
  /// </summary>
  public class RecycleBinPagesStrategy : IRecycleBinStrategy
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RecycleBin.RecycleBinPagesStrategy" /> class.
    /// </summary>
    public RecycleBinPagesStrategy()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RecycleBin.RecycleBinPagesStrategy" /> class.
    /// </summary>
    /// <param name="manager">The decorated manager of the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" />.</param>
    /// <param name="eventRegistry">A component that will be used to register the occurrence of
    /// Recycle Bin related events like 'move to' and 'restore from'.</param>
    /// <param name="urlValidator">The UrlValidator.</param>
    /// <param name="permissionsAuthorizer">The component responsible for authorizing Recycle Bin related actions.</param>
    /// <exception cref="T:System.Exception">The recycling manager should be a PageManager.</exception>
    public RecycleBinPagesStrategy(
      ISupportRecyclingManager manager,
      IRecycleBinEventRegistry eventRegistry,
      IRecycleBinUrlValidator<PageNode> urlValidator,
      IRecycleBinActionsAuthorizer permissionsAuthorizer)
    {
      this.RecyclingPageManager = manager is PageManager pageManager ? pageManager : throw new Exception("The recycling manager should be a PageManager.");
      this.EventRegistry = eventRegistry;
      this.UrlValidator = urlValidator;
      this.PermissionsAuthorizer = permissionsAuthorizer;
    }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.RecycleBin.ISupportRecyclingManager" /> manager to be used when moving items to the Recycle Bin, restoring or deleting them.
    /// </summary>
    /// <value>The manager used by the strategy.</value>
    protected PageManager RecyclingPageManager { get; set; }

    /// <summary>
    /// Gets or sets a component that will be used to register the occurrence of
    /// Recycle Bin related events like 'move to' and 'restore from'.
    /// </summary>
    /// <value>The event register.</value>
    protected IRecycleBinEventRegistry EventRegistry { get; set; }

    /// <summary>Gets or sets the UrlValidator.</summary>
    /// <value>The UrlValidator.</value>
    protected IRecycleBinUrlValidator<PageNode> UrlValidator { get; set; }

    /// <summary>
    /// Gets or sets the component responsible for permissions checks.
    /// </summary>
    /// <value>The component responsible for permissions checks.</value>
    protected IRecycleBinActionsAuthorizer PermissionsAuthorizer { get; set; }

    /// <summary>
    /// Marks the specified <paramref name="dataItem" /> as deleted.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="language">The deleted language. If this parameter is null, the item will be mark as deleted in all languages.</param>
    /// <exception cref="T:System.ArgumentNullException">The specified <paramref name="dataItem" /> must have a value.</exception>
    /// <exception cref="T:System.Exception">The specified <paramref name="dataItem" /> must be a <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" />.</exception>
    public void MoveToRecycleBin(IRecyclableDataItem dataItem, CultureInfo language = null)
    {
      PageNode pageNode = dataItem != null ? this.GetItemAsPageNode(dataItem) : throw new ArgumentNullException(nameof (dataItem));
      this.PermissionsAuthorizer.EnsureMoveToRecycleBinPermissions((object) pageNode);
      if (language != null)
        throw new NotSupportedException();
      this.MarkAsDeleted(pageNode);
    }

    /// <summary>Permanently deletes the specified item.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="language">The specific language translation that will be permanently deleted.</param>
    /// <remarks>Same as invoking IManager.DeleteItem method.</remarks>
    public void PermanentlyDeleteFromRecycleBin(IRecyclableDataItem dataItem, CultureInfo language = null)
    {
      if (!dataItem.IsDeleted)
        return;
      this.RecyclingPageManager.DeleteItem((object) dataItem, language);
    }

    /// <summary>
    /// Restores the specified <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" />.
    /// </summary>
    /// <param name="dataItem">The data item to restore.</param>
    /// <param name="language">The specific language translation that will be restored from the Recycle Bin.</param>
    public void RestoreFromRecycleBin(IRecyclableDataItem dataItem, CultureInfo language = null)
    {
      PageNode pageNode = dataItem != null ? this.GetItemAsPageNode(dataItem) : throw new ArgumentNullException(nameof (dataItem));
      this.PermissionsAuthorizer.EnsureRestoreFromRecycleBinPermissions((object) pageNode);
      if (language != null)
        throw new NotSupportedException();
      this.RestoreEntireItem(pageNode);
    }

    /// <summary>
    /// Validates whether the specified <paramref name="dataItem" /> can be restored.
    /// </summary>
    /// <param name="dataItem">The data item which restoration will be validated.</param>
    /// <param name="language">The specific language translation that will be validated for restore from the Recycle Bin.</param>
    /// <returns>Return a list of <see cref="T:Telerik.Sitefinity.RecycleBin.Conflicts.IRestoreConflict" /> containing invalid restore reasons.</returns>
    public IList<IRestoreConflict> ValidateRestore(
      IRecyclableDataItem dataItem,
      CultureInfo language = null)
    {
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      List<IRestoreConflict> restoreConflictList = new List<IRestoreConflict>();
      PageNode itemAsPageNode = this.GetItemAsPageNode(dataItem);
      IRestoreConflict urlConflict = this.UrlValidator.GetUrlConflict((IManager) this.RecyclingPageManager, itemAsPageNode);
      if (urlConflict != null)
        restoreConflictList.Add(urlConflict);
      IRestoreConflict restoreConflict = this.ValidateMissingParentPage(itemAsPageNode);
      if (restoreConflict != null)
        restoreConflictList.Add(restoreConflict);
      return (IList<IRestoreConflict>) restoreConflictList;
    }

    /// <summary>
    /// Marks the specified <paramref name="pageNode" /> as deleted.
    /// </summary>
    /// <param name="pageNode">The page node to mark as deleted.</param>
    private void MarkAsDeleted(PageNode pageNode)
    {
      this.RecyclingPageManager.Provider.DeletePermissionsInheritanceAssociation((ISecuredObject) pageNode.Parent, (ISecuredObject) pageNode);
      pageNode.PreviousParentId = new Guid?(pageNode.ParentId);
      pageNode.Parent = pageNode.RootNode;
      pageNode.IsDeleted = true;
      List<string> stringList = new List<string>();
      if (pageNode.LocalizationStrategy == LocalizationStrategy.Split)
      {
        foreach (PageData pageData in (IEnumerable<PageData>) pageNode.PageDataList)
        {
          this.MarkPageDataAsDeleted(pageData);
          stringList.Add(pageData.Culture);
          this.EventRegistry.RegisterMoveToRecycleBinOperation((IRecyclableDataItem) pageData, new string[1]
          {
            pageData.Culture
          });
        }
      }
      else
      {
        PageData pageData = pageNode.GetPageData();
        if (pageData != null)
        {
          this.MarkPageDataAsDeleted(pageData);
          string[] array = ((IEnumerable<string>) pageNode.AvailableLanguages).Where<string>((Func<string, bool>) (l => l != CultureInfo.InvariantCulture.Name)).ToArray<string>();
          stringList.AddRange((IEnumerable<string>) array);
          this.EventRegistry.RegisterMoveToRecycleBinOperation((IRecyclableDataItem) pageData, array);
        }
      }
      this.EventRegistry.RegisterMoveToRecycleBinOperation((IRecyclableDataItem) pageNode, stringList.ToArray());
    }

    /// <summary>
    /// Restores the entire page node (no translations involved).
    /// </summary>
    /// <param name="pageNode">The page node to restore.</param>
    private void RestoreEntireItem(PageNode pageNode)
    {
      this.UrlValidator.AssertNoUrlConflicts((IManager) this.RecyclingPageManager, pageNode);
      pageNode.IsDeleted = false;
      this.SetPreviousParent(pageNode);
      List<string> stringList = new List<string>();
      if (pageNode.LocalizationStrategy == LocalizationStrategy.Split)
      {
        foreach (PageData pageData in (IEnumerable<PageData>) pageNode.PageDataList)
        {
          this.RestorePageData(pageData);
          stringList.Add(pageData.Culture);
          this.EventRegistry.RegisterRestoreFromRecycleBinOperation((IRecyclableDataItem) pageData, new string[1]
          {
            pageData.Culture
          });
        }
      }
      else
      {
        PageData pageData = pageNode.GetPageData();
        if (pageData != null)
        {
          this.RestorePageData(pageData);
          string[] array = ((IEnumerable<string>) pageNode.AvailableLanguages).Where<string>((Func<string, bool>) (l => l != CultureInfo.InvariantCulture.Name)).ToArray<string>();
          stringList.AddRange((IEnumerable<string>) array);
          this.EventRegistry.RegisterRestoreFromRecycleBinOperation((IRecyclableDataItem) pageData, array);
        }
      }
      this.EventRegistry.RegisterRestoreFromRecycleBinOperation((IRecyclableDataItem) pageNode, stringList.ToArray());
    }

    /// <summary>
    /// Casts the <paramref name="dataItem" /> as <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" />.
    /// </summary>
    /// <param name="dataItem">The IRecyclableDataItem item.</param>
    /// <returns>Return PageNode item.</returns>
    /// <exception cref="T:System.Exception">Throw exception if dataItem is not <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" />.</exception>
    private PageNode GetItemAsPageNode(IRecyclableDataItem dataItem) => dataItem is PageNode pageNode ? pageNode : throw new Exception(string.Format("The specified dataItem of type '{0}' and Id '{1}' must be a PageNode.", (object) dataItem.GetType().FullName, (object) dataItem.Id));

    private void MarkPageDataAsDeleted(PageData pageData)
    {
      pageData.IsDeleted = true;
      pageData.PublishedTranslations.Clear();
      this.RecyclingPageManager.PagesLifecycle.DiscardAllTemps(pageData);
    }

    private void RestorePageData(PageData pageData)
    {
      pageData.IsDeleted = false;
      foreach (string str in pageData.LanguageData.Where<LanguageData>((Func<LanguageData, bool>) (ld => ld.ContentState == LifecycleState.Published && ld.Language != null)).Select<LanguageData, string>((Func<LanguageData, string>) (pd => pd.Language)).ToList<string>())
        pageData.PublishedTranslations.Add(str);
    }

    private IRestoreConflict ValidateMissingParentPage(PageNode node)
    {
      if (node.PreviousParentId.HasValue)
      {
        PageNode pageNode = this.RecyclingPageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == node.PreviousParentId.Value));
        if (pageNode == null || pageNode.IsDeleted)
          return (IRestoreConflict) new RestoreConflict()
          {
            IsRecoverable = true,
            Reason = Enum.GetName(typeof (RestoreConflictReasons), (object) RestoreConflictReasons.MissingParentPage)
          };
      }
      return (IRestoreConflict) null;
    }

    private void SetPreviousParent(PageNode node)
    {
      if (!node.PreviousParentId.HasValue)
        return;
      PageNode parent = this.RecyclingPageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == node.PreviousParentId.Value && !p.IsDeleted)) ?? node.RootNode;
      node.Parent = parent;
      this.RecyclingPageManager.Provider.CreatePermissionInheritanceAssociation((ISecuredObject) parent, (ISecuredObject) node);
      if (node.InheritsPermissions)
        this.RecyclingPageManager.Provider.RestorePermissionInheritance((ISecuredObject) node);
      node.PreviousParentId = new Guid?();
    }
  }
}
