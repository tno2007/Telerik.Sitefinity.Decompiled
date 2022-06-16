// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.ModuleInstallContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Fluent.Modules
{
  internal class ModuleInstallContext : IModuleInstallContext
  {
    private readonly SiteInitializer siteInitializer;

    public ModuleInstallContext(SiteInitializer siteInitializer) => this.siteInitializer = siteInitializer != null ? siteInitializer : throw new ArgumentNullException(nameof (siteInitializer), "siteInitializer argument cannot be null");

    /// <summary>Gets the manager from the current install context</summary>
    /// <typeparam name="TManager">The type of the manager.</typeparam>
    /// <returns></returns>
    public TManager GetManager<TManager>() where TManager : IManager => this.siteInitializer.GetManagerInTransaction<TManager>();

    /// <summary>
    /// Gets the manager from the current install context for the specified provider.
    /// </summary>
    /// <typeparam name="TManager">The type of the manager.</typeparam>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public TManager GetManager<TManager>(string providerName) where TManager : IManager => this.siteInitializer.GetManagerInTransaction<TManager>();

    /// <summary>Gets the config.</summary>
    /// <typeparam name="TSection">The type of the section.</typeparam>
    /// <returns></returns>
    public TSection GetConfig<TSection>() where TSection : ConfigSection, new() => this.siteInitializer.Context.GetConfig<TSection>();
  }
}
