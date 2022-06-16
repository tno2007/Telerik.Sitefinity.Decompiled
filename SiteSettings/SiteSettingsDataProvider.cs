// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.SiteSettingsDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.SiteSettings.Model;

namespace Telerik.Sitefinity.SiteSettings
{
  /// <summary>
  /// Represents a base class for Site Settings data providers.
  /// </summary>
  public abstract class SiteSettingsDataProvider : DataProviderBase
  {
    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.SiteSettings.Model.SiteSetting" /> instance.
    /// </summary>
    internal abstract SiteSetting CreateSetting();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.SiteSettings.Model.SiteSetting" /> with the given id.
    /// </summary>
    /// <param name="settingId">The setting id.</param>
    /// <returns></returns>
    internal abstract SiteSetting CreateSetting(Guid settingId);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.SiteSettings.Model.SiteSetting" /> for removal.
    /// </summary>
    /// <param name="setting">The setting to delete.</param>
    internal abstract void Delete(SiteSetting setting);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.SiteSettings.Model.SiteSetting" /> object with the given id.
    /// </summary>
    /// <param name="settingId">The setting id.</param>
    internal abstract SiteSetting GetSetting(Guid settingId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.SiteSettings.Model.SiteSetting" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.SiteSettings.Model.SiteSetting" /> objects.
    /// </returns>
    internal abstract IQueryable<SiteSetting> GetSettings();

    /// <summary>Get a list of types served by this manager</summary>
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (SiteSetting)
    };

    /// <summary>Gets a unique key for each data provider base.</summary>
    public override string RootKey => "AppSettingsDataProvider";
  }
}
