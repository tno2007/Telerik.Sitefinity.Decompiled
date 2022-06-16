// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.Data.OpenAccessContentLocationsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.ContentLocations.Data
{
  /// <summary>
  /// Implements the site Locations management data layer with OpenAccess
  /// </summary>
  public class OpenAccessContentLocationsProvider : 
    ContentLocationsDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IOpenAccessUpgradableProvider
  {
    /// <inheritdoc />
    internal override ContentLocationDataItem CreateLocation() => this.CreateLocation(this.GetNewGuid());

    /// <inheritdoc />
    internal override ContentLocationDataItem CreateLocation(Guid locationId)
    {
      ContentLocationDataItem entity = new ContentLocationDataItem(this.ApplicationName, locationId);
      entity.Provider = (object) this;
      if (locationId != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override void Delete(ContentLocationDataItem location)
    {
      foreach (ContentLocationFilterDataItem contentFilter in (IEnumerable<ContentLocationFilterDataItem>) this.GetContentFilters(location.Id))
        this.Delete(contentFilter);
      this.GetContext()?.Remove((object) location);
    }

    /// <inheritdoc />
    internal override ContentLocationDataItem GetLocation(Guid locationId)
    {
      ContentLocationDataItem itemById = this.GetContext().GetItemById<ContentLocationDataItem>(locationId.ToString());
      itemById.Provider = (object) this;
      return itemById;
    }

    /// <inheritdoc />
    internal override IQueryable<ContentLocationDataItem> GetLocations()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<ContentLocationDataItem>((DataProviderBase) this).Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    internal override ContentLocationFilterDataItem CreateContentFilter() => this.CreateContentFilter(this.GetNewGuid());

    internal override ContentLocationFilterDataItem CreateContentFilter(
      Guid contentFilterId)
    {
      ContentLocationFilterDataItem entity = new ContentLocationFilterDataItem()
      {
        Id = contentFilterId
      };
      if (contentFilterId != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    internal override void Delete(ContentLocationFilterDataItem contentFilter) => this.GetContext().Remove((object) contentFilter);

    internal override ContentLocationFilterDataItem GetContentFilter(
      Guid contentFilterId)
    {
      return this.GetContext().GetItemById<ContentLocationFilterDataItem>(contentFilterId.ToString());
    }

    internal override IQueryable<ContentLocationFilterDataItem> GetContentFilters() => SitefinityQuery.Get<ContentLocationFilterDataItem>((DataProviderBase) this);

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new ContentLocationsMetadataSource(context);

    int IOpenAccessUpgradableProvider.CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    void IOpenAccessUpgradableProvider.OnUpgrading(
      UpgradingContext context,
      int upgradingFromSchemaVersionNumber)
    {
      if (upgradingFromSchemaVersionNumber <= 0 || upgradingFromSchemaVersionNumber >= SitefinityVersion.Sitefinity12_2.Build || context.DatabaseContext.DatabaseType != DatabaseType.MsSql && context.DatabaseContext.DatabaseType != DatabaseType.SqlAzure)
        return;
      string upgradeScript = "update\r\nsf_cnt_location_filters\r\nset\r\n[value] = CONCAT(SUBSTRING([value], 7, 36), ',', SUBSTRING([value], 67, 36)),\r\n[type] = 'Telerik.Sitefinity.ContentLocations.ContentLocationSingleItemFilter'\r\nfrom\r\nsf_cnt_location_filters f\r\ninner join\r\nsf_cnt_locations l on f.cnt_location_id = l.id\r\nwhere\r\nl.item_type in ('Telerik.Sitefinity.Libraries.Model.Document', 'Telerik.Sitefinity.Libraries.Model.Video', 'Telerik.Sitefinity.Libraries.Model.Image')\r\nand f.type = 'Telerik.Sitefinity.ContentLocations.BasicContentLocationFilter'\r\nand f.[value] like '(Id = % OR OriginalContentId = %)'";
      OpenAccessConnection.Upgrade(context, "Updade content location single item filters.", upgradeScript);
    }

    void IOpenAccessUpgradableProvider.OnUpgraded(
      UpgradingContext context,
      int upgradedFromSchemaVersionNumber)
    {
      if (upgradedFromSchemaVersionNumber <= 0)
        return;
      if (upgradedFromSchemaVersionNumber < SitefinityVersion.Sitefinity12_2.Build + 20 + 2)
      {
        try
        {
          Guid[] array = context.GetAll<ContentLocationFilterDataItem>().Where<ContentLocationFilterDataItem>((Expression<Func<ContentLocationFilterDataItem, bool>>) (f => f.ContentLocation.Priority != -1 && f.Type == typeof (ContentLocationSingleItemFilter).FullName)).Select<ContentLocationFilterDataItem, Guid>((Expression<Func<ContentLocationFilterDataItem, Guid>>) (f => f.ContentLocation.Id)).ToArray<Guid>();
          int count = 100;
          if (array.Length == 0)
            return;
          int num = (int) Math.Ceiling((double) array.Length / (double) count);
          for (int index = 0; index < num; ++index)
          {
            IEnumerable<Guid> batch = ((IEnumerable<Guid>) array).Skip<Guid>(index * count).Take<Guid>(count);
            context.GetAll<ContentLocationDataItem>().Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => batch.Contains<Guid>(l.Id) && l.Priority != -1)).UpdateAll<ContentLocationDataItem>((Expression<Func<Telerik.OpenAccess.ExtensionMethods.UpdateDescription<ContentLocationDataItem>, Telerik.OpenAccess.ExtensionMethods.UpdateDescription<ContentLocationDataItem>>>) (u => u.Set<int>((Expression<Func<ContentLocationDataItem, int>>) (l => l.Priority), (Expression<Func<ContentLocationDataItem, int>>) (l => -1))));
          }
        }
        catch (Exception ex)
        {
          context.ClearChanges();
          Log.Write((object) string.Format((IFormatProvider) CultureInfo.InvariantCulture, "FAILED: Update single item content locations. Actual error: {0}", (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            return;
          throw;
        }
      }
      else
      {
        if (upgradedFromSchemaVersionNumber >= SitefinityVersion.Sitefinity12_2.Build + 20 + 12)
          return;
        try
        {
          Guid[] array = context.GetAll<ContentLocationFilterDataItem>().Where<ContentLocationFilterDataItem>((Expression<Func<ContentLocationFilterDataItem, bool>>) (f => f.ContentLocation.Priority == -1 && f.Type != typeof (ContentLocationSingleItemFilter).FullName)).Select<ContentLocationFilterDataItem, Guid>((Expression<Func<ContentLocationFilterDataItem, Guid>>) (f => f.ContentLocation.Id)).ToArray<Guid>();
          int count = 100;
          if (array.Length == 0)
            return;
          int num = (int) Math.Ceiling((double) array.Length / (double) count);
          for (int index = 0; index < num; ++index)
          {
            IEnumerable<Guid> batch = ((IEnumerable<Guid>) array).Skip<Guid>(index * count).Take<Guid>(count);
            context.GetAll<ContentLocationDataItem>().Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => batch.Contains<Guid>(l.Id) && l.Priority == -1)).UpdateAll<ContentLocationDataItem>((Expression<Func<Telerik.OpenAccess.ExtensionMethods.UpdateDescription<ContentLocationDataItem>, Telerik.OpenAccess.ExtensionMethods.UpdateDescription<ContentLocationDataItem>>>) (u => u.Set<int>((Expression<Func<ContentLocationDataItem, int>>) (l => l.Priority), (Expression<Func<ContentLocationDataItem, int>>) (l => -2))));
          }
        }
        catch (Exception ex)
        {
          context.ClearChanges();
          Log.Write((object) string.Format((IFormatProvider) CultureInfo.InvariantCulture, "FAILED: Update single item content locations. Actual error: {0}", (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            return;
          throw;
        }
      }
    }
  }
}
