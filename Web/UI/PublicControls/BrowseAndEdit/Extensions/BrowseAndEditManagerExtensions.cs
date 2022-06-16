// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManagerExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  public static class BrowseAndEditManagerExtensions
  {
    /// <summary>Adds new browse-and-edit toolbar to the page</summary>
    /// <param name="item">The current item</param>
    /// <param name="host">The container for the specific views</param>
    /// <param name="toolbarItemId">The ID of the toolbar control</param>
    /// <param name="serviceUrl">The REST service URL which will be used for BrowseAndEdit functionality</param>
    public static void AddBrowseAndEditToolBar(
      this BrowseAndEditManager manager,
      Control item,
      ContentView host,
      string toolbarItemId)
    {
      Content contentItem = (Content) null;
      if (item is RadListViewDataItem)
        contentItem = ((RadListViewDataItem) item).DataItem as Content;
      if (!(item.FindControl(toolbarItemId) is ContentBrowseAndEditToolbar control))
        return;
      manager.AddConfiguredContentBrowseAndEditToolBar(contentItem, host, control);
    }

    /// <summary>Adds new browse-and-edit toolbar to the page</summary>
    /// <param name="item">The current item</param>
    /// <param name="host">The container for the specific views</param>
    /// <param name="toolbarItemId">The ID of the toolbar control</param>
    /// <param name="serviceUrl">The REST service URL which will be used for BrowseAndEdit functionality</param>
    /// <param name="parentId">The parent ID for new items</param>
    public static void AddBrowseAndEditToolBar(
      this BrowseAndEditManager manager,
      Control item,
      ContentView host,
      string toolbarItemId,
      Content parentItem)
    {
      Content contentItem = (Content) null;
      if (item is RadListViewDataItem)
        contentItem = ((RadListViewDataItem) item).DataItem as Content;
      if (!(item.FindControl(toolbarItemId) is ContentBrowseAndEditToolbar control))
        return;
      manager.AddConfiguredContentBrowseAndEditToolBar(contentItem, host, control, parentItem);
    }

    /// <summary>Adds new browse-and-edit toolbar to the page</summary>
    /// <param name="contentItem">The content item for the toolbar</param>
    /// <param name="host">The container for the specific views</param>
    /// <param name="toolbar">The toolbar control</param>
    /// <param name="serviceUrl">The REST service URL which will be used for BrowseAndEdit functionality</param>
    public static void AddConfiguredContentBrowseAndEditToolBar(
      this BrowseAndEditManager manager,
      Content contentItem,
      ContentView host,
      ContentBrowseAndEditToolbar toolbar,
      Content parentItem = null)
    {
      if (toolbar == null)
        throw new ArgumentNullException(nameof (toolbar), "The given toolbar control is null.");
      toolbar.Configure(host, contentItem, (IDataItem) parentItem);
    }
  }
}
