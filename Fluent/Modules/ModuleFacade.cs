// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.ModuleFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Fluent.Modules
{
  /// <summary>
  /// This is an aggregation facade for all the facades related to installing, uninstalling, upgrading and initializing modules
  /// </summary>
  public class ModuleFacade
  {
    private string moduleName;
    private PageManager pageManager;
    private AppSettings settings;

    /// <summary>Creates a new instance of the module facade.</summary>
    public ModuleFacade()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleFacade" /> class.
    /// </summary>
    /// <param name="settings">The settings.</param>
    public ModuleFacade(AppSettings settings) => this.settings = settings;

    /// <summary>Creates a new instance of the module facade.</summary>
    /// <param name="moduleName">The name of the module for which the fluent API will be used.</param>
    public ModuleFacade(string moduleName) => this.moduleName = !string.IsNullOrEmpty(moduleName) ? moduleName : throw new ArgumentNullException(nameof (moduleName), "moduleName argument cannot be null or an empty string.");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleFacade" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="settings">The settings.</param>
    public ModuleFacade(string moduleName, AppSettings settings)
      : this(moduleName)
    {
      this.settings = settings;
    }

    /// <summary>Creates a new instance of the module facade.</summary>
    /// <param name="moduleName">The name of the module for which the fluent API will be used.</param>
    /// <param name="pageManager">The instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleFacade.PageManager" /> to be used with the fluent API call.</param>
    public ModuleFacade(string moduleName, PageManager pageManager, AppSettings settings = null)
    {
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName), "moduleName argument cannot be null or an empty string.");
      if (pageManager == null)
        throw new ArgumentNullException(nameof (pageManager), "pageManager argument cannot be null.");
      this.moduleName = moduleName;
      this.pageManager = pageManager;
      this.settings = settings;
    }

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleFacade.PageManager" /> to be used by module facade and it's child facades.
    /// </summary>
    [Obsolete("Page manager is needed only by ModuleInstallFacade returned by the install method")]
    protected PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
        {
          this.pageManager = this.settings == null ? PageManager.GetManager() : PageManager.GetManager(this.settings.PagesProviderName, this.settings.TransactionName);
          this.pageManager.Provider.SuppressSecurityChecks = true;
        }
        return this.pageManager;
      }
    }

    /// <summary>
    /// Fluent API that provides functionality for installing a module.
    /// </summary>
    /// <returns>A new instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.</returns>
    public ModuleInstallFacade Install() => new ModuleInstallFacade(this.moduleName, this.settings, this.pageManager);

    /// <summary>
    /// Fluent API that provides functionality for installing a module.
    /// </summary>
    /// <returns>
    /// A new instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.
    /// </returns>
    public ModuleInstallFacade InstallWithContext(
      IModuleInstallContext installContext)
    {
      return new ModuleInstallFacade(this.moduleName, this.settings, installContext);
    }

    /// <summary>
    /// Fluent API that provides functionality for uninstalling a module.
    /// </summary>
    /// <returns>
    /// A new instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.
    /// </returns>
    public ModuleUninstallFacade UninstallWithContext(
      IModuleInstallContext installContext)
    {
      return new ModuleUninstallFacade(this.moduleName, this.settings, installContext);
    }

    /// <summary>
    /// Fluent API that provides functionality for initializing a module.
    /// </summary>
    /// <returns>A new instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade Initialize() => new ModuleInitializeFacade(this.moduleName, this.pageManager);

    /// <summary>Provides fluent API for defining ContentView control.</summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <returns></returns>
    public ContentViewControlDefinitionFacade DefineContainer(
      ConfigElement parentElement,
      string definitionName)
    {
      if (parentElement == null)
        throw new ArgumentNullException(nameof (parentElement));
      if (string.IsNullOrEmpty(definitionName))
        throw new ArgumentNullException(nameof (definitionName));
      return new ContentViewControlDefinitionFacade(this.moduleName, parentElement, definitionName, this);
    }

    /// <summary>Provides fluent API for defining ContentView control.</summary>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    public ContentViewControlDefinitionFacade DefineContainer(
      ConfigElement parentElement,
      string definitionName,
      Type contentType)
    {
      if (parentElement == null)
        throw new ArgumentNullException(nameof (parentElement));
      if (string.IsNullOrEmpty(definitionName))
        throw new ArgumentNullException(nameof (definitionName));
      if (contentType == (Type) null)
        throw new ArgumentNullException(nameof (contentType));
      return new ContentViewControlDefinitionFacade(this.moduleName, parentElement, definitionName, contentType, this);
    }
  }
}
