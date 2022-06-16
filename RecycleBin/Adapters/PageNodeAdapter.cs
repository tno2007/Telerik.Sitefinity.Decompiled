// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Adapters.PageNodeAdapter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.RecycleBin.Adapters
{
  /// <summary>
  /// Populates the properties of a specified <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> from the properties of a <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> item.
  /// </summary>
  public class PageNodeAdapter : IRecycleBinItemAdapter
  {
    /// <summary>
    /// Populates the properties of the specified <paramref name="recycleBinItem" /> from the specified <paramref name="dataItem" />,
    /// casting it to <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" />.
    /// </summary>
    /// <param name="recycleBinItem">The recycle bin item which properties will be populated.</param>
    /// <param name="dataItem">The data item to get values from.</param>
    public virtual void FillProperties(IRecycleBinDataItem recycleBinItem, IDataItem dataItem)
    {
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      if (recycleBinItem == null)
        throw new ArgumentNullException(nameof (recycleBinItem));
      recycleBinItem.DeletedItemTitle = dataItem is PageNode pageNode ? (string) pageNode.Title : throw new Exception(string.Format("PageAdapter received data item with id: {0} and type:{1} that is not PageNode.", (object) dataItem.Id, (object) dataItem.GetType().FullName));
      recycleBinItem.DeletedItemParentType = pageNode.GetType().FullName;
      recycleBinItem.DeletedItemParentTitlesPath = this.GetFullTitlePath(pageNode);
      ISite pageSite = this.GetPageSite(pageNode);
      if (pageSite == null)
        return;
      recycleBinItem.DeletedItemSiteId = pageSite.Id;
    }

    private ISite GetPageSite(PageNode pageNode)
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext == null)
        return SystemManager.CurrentContext.CurrentSite;
      ISite currentSite = multisiteContext.CurrentSite;
      ISite pageSite = (ISite) null;
      if (pageNode.RootNodeId == currentSite.SiteMapRootNodeId)
        pageSite = currentSite;
      else if (pageNode.RootNodeId != SiteInitializer.BackendRootNodeId)
        pageSite = multisiteContext.GetSiteBySiteMapRoot(pageNode.RootNodeId);
      return pageSite;
    }

    private string GetFullTitlePath(PageNode pageNode)
    {
      string fullTitlePath = string.Empty;
      try
      {
        if (pageNode.PreviousParentId.HasValue)
        {
          Guid parentId = pageNode.ParentId;
          pageNode.ParentId = pageNode.PreviousParentId.Value;
          pageNode.PreviousParentId = new Guid?(parentId);
        }
        CultureInfo culture = SystemManager.CurrentContext.AppSettings.Multilingual ? SystemManager.CurrentContext.Culture : CultureInfo.InvariantCulture;
        string str = pageNode.BuildFullTitlesPath(culture, " > ");
        if (str.Contains(">"))
          fullTitlePath = str;
      }
      finally
      {
        if (pageNode.PreviousParentId.HasValue)
        {
          Guid guid = pageNode.PreviousParentId.Value;
          pageNode.PreviousParentId = new Guid?(pageNode.ParentId);
          pageNode.ParentId = guid;
        }
      }
      return fullTitlePath;
    }
  }
}
