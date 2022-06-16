// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Data.OpenAccessSiteSettingsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.SiteSettings.Model;

namespace Telerik.Sitefinity.SiteSettings.Data
{
  /// <summary>
  /// Implements the site settings management data layer with OpenAccess
  /// </summary>
  public class OpenAccessSiteSettingsProvider : 
    SiteSettingsDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <inheritdoc />
    internal override SiteSetting CreateSetting() => this.CreateSetting(this.GetNewGuid());

    /// <inheritdoc />
    internal override SiteSetting CreateSetting(Guid settingId)
    {
      SiteSetting entity = new SiteSetting(this.ApplicationName, settingId);
      entity.Provider = (object) this;
      if (settingId != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override void Delete(SiteSetting setting) => this.GetContext().Remove((object) setting);

    /// <inheritdoc />
    internal override SiteSetting GetSetting(Guid settingId)
    {
      SiteSetting itemById = this.GetContext().GetItemById<SiteSetting>(settingId.ToString());
      itemById.Provider = (object) this;
      return itemById;
    }

    /// <inheritdoc />
    internal override IQueryable<SiteSetting> GetSettings()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<SiteSetting>((DataProviderBase) this).Where<SiteSetting>((Expression<Func<SiteSetting, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new SiteSettingsMetadataSource(context);
  }
}
