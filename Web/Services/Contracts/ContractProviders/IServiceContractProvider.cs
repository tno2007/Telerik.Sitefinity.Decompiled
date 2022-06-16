// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.TypeSettingsProviderRepo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class TypeSettingsProviderRepo
  {
    private static ConcurrentProperty<IDictionary<string, ITypeSettings>> contracts = new ConcurrentProperty<IDictionary<string, ITypeSettings>>(new Func<IDictionary<string, ITypeSettings>>(TypeSettingsProviderRepo.GetContractsInternal));

    static TypeSettingsProviderRepo()
    {
      SystemManager.ModelReset += new EventHandler<EventArgs>(TypeSettingsProviderRepo.InvalidateHandler);
      Bootstrapper.Bootstrapped += new EventHandler<EventArgs>(TypeSettingsProviderRepo.InvalidateHandler);
      ModuleInstaller.ModuleInstalled += new EventHandler<DynamicModuleEventArgs>(TypeSettingsProviderRepo.InvalidateHandler);
      ModuleInstaller.ModuleUninstalled += new EventHandler<DynamicModuleEventArgs>(TypeSettingsProviderRepo.InvalidateHandler);
      ModuleInstaller.ModuleDeleted += new EventHandler<DynamicModuleEventArgs>(TypeSettingsProviderRepo.InvalidateHandler);
      ModuleInstaller.ContentTypeUpdated += new EventHandler<DynamicModuleTypeEventArgs>(TypeSettingsProviderRepo.InvalidateHandler);
    }

    public static IDictionary<string, ITypeSettings> GetTypeSettings() => TypeSettingsProviderRepo.contracts.Value;

    private static void InvalidateHandler(object sender, EventArgs args) => TypeSettingsProviderRepo.Reset();

    private static void Reset()
    {
      TypeSettingsProviderRepo.contracts.Reset();
      if (TypeSettingsProviderRepo.OnModelChange == null)
        return;
      TypeSettingsProviderRepo.OnModelChange((object) null, (EventArgs) null);
    }

    private static IDictionary<string, ITypeSettings> GetContractsInternal()
    {
      List<ITypeSettingsProvider> source1 = new List<ITypeSettingsProvider>(SystemManager.ApplicationModules.Values.OfType<ITypeSettingsProvider>());
      source1.Add((ITypeSettingsProvider) new TaxonomyServiceContractProvider());
      source1.Add((ITypeSettingsProvider) new FolderContractProvider());
      source1.Add((ITypeSettingsProvider) new PageNodeServiceContractProvider());
      if (PageTemplateServiceContractProvider.GetAreTempaltesAvailable())
        source1.Add((ITypeSettingsProvider) new PageTemplateServiceContractProvider());
      ITypeSettingsProvider settingsProvider1 = source1.FirstOrDefault<ITypeSettingsProvider>((Func<ITypeSettingsProvider, bool>) (x => x is ModuleBuilderModule));
      if (settingsProvider1 != null)
      {
        source1.Remove(settingsProvider1);
        source1.Add(settingsProvider1);
      }
      IDictionary<string, ITypeSettings> contractsInternal = (IDictionary<string, ITypeSettings>) new Dictionary<string, ITypeSettings>();
      foreach (ITypeSettingsProvider settingsProvider2 in source1)
      {
        IDictionary<string, ITypeSettings> typeSettings = settingsProvider2.GetTypeSettings();
        if (typeSettings != null)
          contractsInternal = (IDictionary<string, ITypeSettings>) contractsInternal.Concat<KeyValuePair<string, ITypeSettings>>((IEnumerable<KeyValuePair<string, ITypeSettings>>) typeSettings).ToDictionary<KeyValuePair<string, ITypeSettings>, string, ITypeSettings>((Func<KeyValuePair<string, ITypeSettings>, string>) (kvp => kvp.Key), (Func<KeyValuePair<string, ITypeSettings>, ITypeSettings>) (kvp => kvp.Value));
      }
      IEnumerable<IGrouping<string, KeyValuePair<string, ITypeSettings>>> source2 = contractsInternal.GroupBy<KeyValuePair<string, ITypeSettings>, string>((Func<KeyValuePair<string, ITypeSettings>, string>) (x => x.Value.UrlName)).Where<IGrouping<string, KeyValuePair<string, ITypeSettings>>>((Func<IGrouping<string, KeyValuePair<string, ITypeSettings>>, bool>) (x => x.Skip<KeyValuePair<string, ITypeSettings>>(1).Any<KeyValuePair<string, ITypeSettings>>()));
      if (source2.Any<IGrouping<string, KeyValuePair<string, ITypeSettings>>>())
      {
        foreach (IGrouping<string, KeyValuePair<string, ITypeSettings>> grouping in source2)
        {
          IGrouping<string, KeyValuePair<string, ITypeSettings>> entry = grouping;
          int num = 0;
          foreach (KeyValuePair<string, ITypeSettings> keyValuePair in contractsInternal.Where<KeyValuePair<string, ITypeSettings>>((Func<KeyValuePair<string, ITypeSettings>, bool>) (x => x.Value.UrlName == entry.Key)).Skip<KeyValuePair<string, ITypeSettings>>(1).ToList<KeyValuePair<string, ITypeSettings>>())
            (keyValuePair.Value as TypeSettingsProxy).UrlName = keyValuePair.Value.UrlName + (object) ++num;
        }
      }
      return contractsInternal;
    }

    internal static event EventHandler<EventArgs> OnModelChange;
  }
}
