// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Conflicts.PageRecycleBinUrlValidator`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.RecycleBin.Conflicts
{
  internal class PageRecycleBinUrlValidator<T> : IRecycleBinUrlValidator<T> where T : PageNode
  {
    /// <summary>
    /// Determines whether the specified <paramref name="dataItem" />'s URL is unique.
    /// </summary>
    /// <param name="manager">The manager for specified dataItem.</param>
    /// <param name="dataItem">The data item which URL will be checked.</param>
    /// <returns>Returns a URL conflict if any.</returns>
    public IRestoreConflict GetUrlConflict(IManager manager, T dataItem) => this.ValidateExistingUrl((PageManager) manager, (PageNode) dataItem);

    /// <summary>
    /// Asserts that the specified <paramref name="dataItem" />'s URL is unique.
    /// If the a URL conflict is encountered an Exception containing the
    /// conflict information will be thrown.
    /// </summary>
    /// <param name="manager">The manager for current <see cref="!:dataItem" />.</param>
    /// <param name="dataItem">The data item which URL will be checked.</param>
    public void AssertNoUrlConflicts(IManager manager, T dataItem) => this.ValidateDuplicateUrl((PageManager) manager, (PageNode) dataItem);

    private IRestoreConflict ValidateExistingUrl(PageManager manager, PageNode node)
    {
      try
      {
        this.ValidateDuplicateUrl(manager, node);
      }
      catch (DuplicatePageUrlException ex)
      {
        return (IRestoreConflict) new RestoreConflict()
        {
          IsRecoverable = false,
          Reason = Enum.GetName(typeof (RestoreConflictReasons), (object) RestoreConflictReasons.ExistingPageUrl),
          ReasonArgs = ex.Data
        };
      }
      return (IRestoreConflict) null;
    }

    private void ValidateDuplicateUrl(PageManager manager, PageNode node)
    {
      bool flag = false;
      if (node.PreviousParentId.HasValue)
      {
        PageNode pageNode = manager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == node.PreviousParentId.Value));
        flag = pageNode != null && !pageNode.IsDeleted;
      }
      try
      {
        if (flag)
        {
          Guid parentId = node.ParentId;
          node.ParentId = node.PreviousParentId.Value;
          node.PreviousParentId = new Guid?(parentId);
        }
        PageManager.ValidateDuplicateUrl(manager.Provider, node);
      }
      finally
      {
        if (flag)
        {
          Guid guid = node.PreviousParentId.Value;
          node.PreviousParentId = new Guid?(node.ParentId);
          node.ParentId = guid;
        }
      }
    }
  }
}
