// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Upgrades.To5100.ContentLocationsUpgrader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Upgrades.To5100
{
  /// <summary>
  /// Fixes split page node references in Content Locations after merge (7.0).
  /// </summary>
  internal class ContentLocationsUpgrader
  {
    public ContentLocationsUpgrader() => this.TransactionName = this.GetType().Name + DateTime.UtcNow.ToString();

    protected string TransactionName { get; set; }

    public void UpgradeMultilingualSplitPagesContentLocations(Version version)
    {
      string str = "{0} : Upgrade to {1}".Arrange((object) MethodBase.GetCurrentMethod().Name, (object) version.Build.ToString());
      List<ContentLocationsUpgrader.LocationInfo> locationInfoList = new List<ContentLocationsUpgrader.LocationInfo>();
      try
      {
        PageManager manager1 = PageManager.GetManager((string) null, this.TransactionName);
        ContentLocationsManager.GetManager((string) null, this.TransactionName);
        foreach (ContentLocationsDataProvider staticProviders in (Collection<ContentLocationsDataProvider>) ManagerBase<ContentLocationsDataProvider>.StaticProvidersCollection)
        {
          ContentLocationsManager manager2 = ContentLocationsManager.GetManager(staticProviders.Name, this.TransactionName);
          bool flag = false;
          List<ContentLocationDataItem> list = manager2.GetLocations().ToList<ContentLocationDataItem>();
          int num = 0;
          foreach (ContentLocationDataItem locationDataItem in list)
          {
            try
            {
              PageNode pageNode = manager1.GetPageNode(locationDataItem.PageId);
              if (pageNode.Id != locationDataItem.PageId)
              {
                flag = true;
                locationDataItem.PageId = pageNode.Id;
              }
            }
            catch (ItemNotFoundException ex)
            {
              locationInfoList.Add(new ContentLocationsUpgrader.LocationInfo(staticProviders.Name, locationDataItem.PageId, locationDataItem.Id));
              continue;
            }
            if (++num % 10 == 0)
              TransactionManager.FlushTransaction(this.TransactionName);
          }
          if (flag)
            TransactionManager.CommitTransaction(this.TransactionName);
        }
        Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        TransactionManager.RollbackTransaction(this.TransactionName);
        Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
      finally
      {
        TransactionManager.DisposeTransaction(this.TransactionName);
        if (locationInfoList.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendLine("List of content locations with invalid page ids:");
          foreach (ContentLocationsUpgrader.LocationInfo locationInfo in locationInfoList)
            stringBuilder.AppendFormat("Invalid page with id {0} specified in ContentLocation. LocationId: {1}, LocationProvider: {2}", (object) locationInfo.NodeId, (object) locationInfo.LocationId, (object) locationInfo.Provider).AppendLine();
          Log.Write((object) stringBuilder.ToString(), ConfigurationPolicy.UpgradeTrace);
        }
      }
    }

    private class LocationInfo
    {
      public LocationInfo(string provider, Guid nodeId, Guid locationId)
      {
        this.Provider = provider;
        this.NodeId = nodeId;
        this.LocationId = locationId;
      }

      internal string Provider { get; set; }

      internal Guid NodeId { get; set; }

      internal Guid LocationId { get; set; }
    }
  }
}
