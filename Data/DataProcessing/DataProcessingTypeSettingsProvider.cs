// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataProcessing.DataProcessingTypeSettingsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Web.Services.Contracts;

namespace Telerik.Sitefinity.Data.DataProcessing
{
  internal class DataProcessingTypeSettingsProvider
  {
    public static IDictionary<string, ITypeSettings> GetTypeSettings()
    {
      Dictionary<string, ITypeSettings> dictionary = TypeSettingsProviderRepo.GetTypeSettings().ToDictionary<KeyValuePair<string, ITypeSettings>, string, ITypeSettings>((Func<KeyValuePair<string, ITypeSettings>, string>) (x => x.Key), (Func<KeyValuePair<string, ITypeSettings>, ITypeSettings>) (x => x.Value));
      ITypeSettings siteTypeSettings = DataProcessingTypeSettingsProvider.GetSiteTypeSettings();
      if (!dictionary.ContainsKey(siteTypeSettings.ClrType))
        dictionary.Add(siteTypeSettings.ClrType, siteTypeSettings);
      else
        dictionary[siteTypeSettings.ClrType] = siteTypeSettings;
      return (IDictionary<string, ITypeSettings>) dictionary;
    }

    private static ITypeSettings GetSiteTypeSettings()
    {
      PersistentPropertyMappingProxy[] source1 = new PersistentPropertyMappingProxy[6];
      PersistentPropertyMappingProxy propertyMappingProxy1 = new PersistentPropertyMappingProxy();
      propertyMappingProxy1.Name = LinqHelper.MemberName<Site>((Expression<Func<Site, object>>) (t => t.Name));
      source1[0] = propertyMappingProxy1;
      PersistentPropertyMappingProxy propertyMappingProxy2 = new PersistentPropertyMappingProxy();
      propertyMappingProxy2.Name = LinqHelper.MemberName<Site>((Expression<Func<Site, object>>) (t => t.LiveUrl));
      source1[1] = propertyMappingProxy2;
      PersistentPropertyMappingProxy propertyMappingProxy3 = new PersistentPropertyMappingProxy();
      propertyMappingProxy3.Name = LinqHelper.MemberName<Site>((Expression<Func<Site, object>>) (t => t.FrontEndLoginPageUrl));
      source1[2] = propertyMappingProxy3;
      PersistentPropertyMappingProxy propertyMappingProxy4 = new PersistentPropertyMappingProxy();
      propertyMappingProxy4.Name = LinqHelper.MemberName<Site>((Expression<Func<Site, object>>) (t => t.OfflineSiteMessage));
      source1[3] = propertyMappingProxy4;
      PersistentPropertyMappingProxy propertyMappingProxy5 = new PersistentPropertyMappingProxy();
      propertyMappingProxy5.Name = LinqHelper.MemberName<Site>((Expression<Func<Site, object>>) (t => t.DomainAliases));
      source1[4] = propertyMappingProxy5;
      PersistentPropertyMappingProxy propertyMappingProxy6 = new PersistentPropertyMappingProxy();
      propertyMappingProxy6.Name = LinqHelper.MemberName<Site>((Expression<Func<Site, object>>) (t => t.StagingUrl));
      source1[5] = propertyMappingProxy6;
      IEnumerable<IPropertyMapping> source2 = source1.Cast<IPropertyMapping>();
      return (ITypeSettings) new TypeSettingsProxy()
      {
        ClrType = typeof (Site).FullName,
        Enabled = true,
        Properties = (IList<IPropertyMapping>) source2.ToList<IPropertyMapping>()
      };
    }
  }
}
