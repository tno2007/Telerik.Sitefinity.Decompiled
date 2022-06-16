// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SyncMap.Data.OpenAccessSyncMapProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.SyncMap.Model;

namespace Telerik.Sitefinity.SyncMap.Data
{
  /// <summary>
  /// Implements the site settings management data layer with OpenAccess
  /// </summary>
  public class OpenAccessSyncMapProvider : 
    SyncMapDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <inheritdoc />
    internal override SyncApp CreateApp(string moduleName, string appName) => this.CreateApp(this.GetNewGuid(), moduleName, appName);

    /// <inheritdoc />
    internal override SyncApp CreateApp(Guid appId, string moduleName, string appName)
    {
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      if (string.IsNullOrEmpty(appName))
        throw new ArgumentNullException(nameof (appName));
      SyncApp entity = new SyncApp();
      entity.Id = appId;
      entity.ModuleName = moduleName;
      entity.AppName = appName;
      if (appId != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override void Delete(SyncApp app) => this.GetContext().Remove((object) app);

    /// <inheritdoc />
    internal override SyncApp GetApp(Guid appId) => this.GetContext().GetItemById<SyncApp>(appId.ToString());

    /// <inheritdoc />
    internal override SyncApp GetApp(string moduleName, string appName) => this.GetContext().GetAll<SyncApp>().Where<SyncApp>((Expression<Func<SyncApp, bool>>) (app => app.ModuleName == moduleName && app.AppName == appName)).SingleOrDefault<SyncApp>();

    /// <inheritdoc />
    internal override IQueryable<SyncApp> GetApps() => this.GetContext().GetAll<SyncApp>();

    /// <inheritdoc />
    internal override SyncMapping CreateMapping(
      Guid appId,
      string externalKey,
      Guid itemId)
    {
      if (string.IsNullOrEmpty(externalKey))
        throw new ArgumentNullException("moduleName");
      SyncMapping entity = new SyncMapping();
      entity.AppId = appId;
      entity.ExternalKey = externalKey;
      entity.ItemId = itemId;
      if (appId != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override void Delete(SyncMapping mapping) => this.GetContext().Remove((object) mapping);

    /// <inheritdoc />
    internal override SyncMapping GetMapping(
      Guid appId,
      string externalKey,
      Guid itemId)
    {
      return this.GetContext().GetAll<SyncMapping>().Where<SyncMapping>((Expression<Func<SyncMapping, bool>>) (m => m.AppId == appId && m.ExternalKey == externalKey && m.ItemId == itemId)).SingleOrDefault<SyncMapping>();
    }

    /// <inheritdoc />
    internal override IQueryable<SyncMapping> GetMappings(Guid appId) => this.GetContext().GetAll<SyncMapping>().Where<SyncMapping>((Expression<Func<SyncMapping, bool>>) (m => m.AppId == appId));

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new SyncMapMetadataSource(context);
  }
}
