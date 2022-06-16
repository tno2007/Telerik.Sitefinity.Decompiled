// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.MediaContentView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Enums;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// This control acts as a container for the views that display media items.
  /// </summary>
  public class MediaContentView : ContentView
  {
    /// <summary>Gets or sets the title of the field control.</summary>
    /// <value>The title of the field control.</value>
    public virtual string Title { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether detail item will be taken from the data source.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if detail item is taken from the data source; otherwise, <c>false</c>.
    /// </value>
    internal bool GetDetailItemFromDataSource { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether default view should be used.
    /// </summary>
    /// <value>
    ///     <c>true</c> if default view should be used; otherwise, <c>false</c>.
    /// </value>
    internal bool DefaultViewFallBack { get; set; }

    protected internal override IDataItem GetItemFromUrl(
      IManager manager,
      string itemUrl,
      out string redirectUrl)
    {
      if (!this.GetDetailItemFromDataSource)
        return base.GetItemFromUrl(manager, itemUrl, out redirectUrl);
      redirectUrl = string.Empty;
      IDataItem itemFromUrl = (IDataItem) null;
      if (this.DataSource != null)
        itemFromUrl = (IDataItem) this.DataSource.OfType<MediaContent>().FirstOrDefault<MediaContent>((Func<MediaContent, bool>) (mc => mc.Urls.Any<MediaUrlData>((Func<MediaUrlData, bool>) (u => u.Url == itemUrl)) && mc.Status == ContentLifecycleStatus.Live && mc.Visible));
      return itemFromUrl;
    }

    protected internal override string ValidateViewIsFromControlDefinition(string viewName)
    {
      if (this.DefaultViewFallBack)
        return this.ControlDefinition.Views[0].ViewName;
      return this.ControlDefinition.ContainsView(viewName) ? viewName : throw new ArgumentOutOfRangeException("The view specified in the url cannot be found in the Views collection of the current control.");
    }

    public override IEnumerable<IContentLocationInfo> GetLocations()
    {
      ContentViewControlDefinition controlDefinition = this.ControlDefinition;
      if (controlDefinition == null)
        return (IEnumerable<IContentLocationInfo>) null;
      ContentLocationInfo location = new ContentLocationInfo();
      Type contentType = controlDefinition.ContentType;
      location.ContentType = contentType;
      string str = this.ControlDefinition.ProviderName;
      IManager contentManager = (IManager) null;
      Type managerType;
      if (ManagerBase.TryGetMappedManagerType(contentType, out managerType))
        contentManager = ManagerBase.GetManager(managerType);
      if (str.IsNullOrEmpty() && contentManager != null)
        str = contentManager.Provider.Name;
      location.ProviderName = str;
      switch (this.ContentViewDisplayMode)
      {
        case ContentViewDisplayMode.Automatic:
          this.AddMasterViewFilters(location, this.MasterViewDefinition, contentType, contentManager, new ContentLocationFilterExpressionSettings()
          {
            SkipParentFilter = true
          });
          this.AddMasterViewFilters(location);
          break;
        case ContentViewDisplayMode.Detail:
          this.AddDetailViewFilter(location, this.DetailViewDefinition, contentType, contentManager);
          break;
        default:
          return (IEnumerable<IContentLocationInfo>) null;
      }
      return (IEnumerable<IContentLocationInfo>) new ContentLocationInfo[1]
      {
        location
      };
    }

    private void AddMasterViewFilters(ContentLocationInfo location)
    {
      if (this.MasterViewDefinition == null || !(this.MasterViewDefinition.ItemsParentId != Guid.Empty))
        return;
      location.Filters.Add((IContentLocationFilter) new MediaContentParentLocationFilter()
      {
        Value = this.MasterViewDefinition.ItemsParentId.ToString()
      });
    }
  }
}
