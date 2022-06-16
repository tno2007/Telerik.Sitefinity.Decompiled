// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.ServiceModel.Activation;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Fluent.Modules.Toolboxes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Fluent.Modules
{
  /// <summary>Fluent API for initializing a module.</summary>
  public class ModuleInitializeFacade
  {
    private string moduleName;
    private PageManager pageManager;

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.
    /// </summary>
    public ModuleInitializeFacade(string moduleName) => this.moduleName = moduleName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="pageManager">The page manager.</param>
    internal ModuleInitializeFacade(string moduleName, PageManager pageManager)
    {
      this.moduleName = moduleName;
      this.pageManager = pageManager;
    }

    protected PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager();
        return this.pageManager;
      }
    }

    /// <summary>
    /// Initializes the localization class required by the module. The class will be registered with the Unity container.
    /// </summary>
    /// <typeparam name="TResource">Type of the localization class to be initialized.</typeparam>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade Localization<TResource>() where TResource : Resource, new()
    {
      Res.RegisterResource<TResource>();
      return this;
    }

    /// <summary>
    /// Initializes the localization class required by the module. The class will be registered with the Unity container.
    /// </summary>
    /// <param name="localizationClassResource">The type of the localization class to be initialized.</param>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade Localization(Type localizationClassResource)
    {
      Res.RegisterResource(localizationClassResource);
      return this;
    }

    /// <summary>
    /// Initializes the configuration class required by the module. The class will be registered with the Unity container.
    /// </summary>
    /// <typeparam name="TConfig">The type of the configuration class to be initialized.</typeparam>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade Configuration<TConfig>() where TConfig : ConfigSection, new()
    {
      Config.RegisterSection<TConfig>();
      return this;
    }

    /// <summary>
    /// Initializes the configuration class required by the module. The class will be registered with the Unity container.
    /// </summary>
    /// <param name="configurationClassType">The type of the configuration class to be initialized.</param>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade Configuration(Type configurationClassType)
    {
      Config.RegisterSection(configurationClassType);
      return this;
    }

    /// <summary>
    /// Registers a web service with the specified URL. The class will be registered with the Unity container.
    /// </summary>
    /// <typeparam name="TService">The type of the service class.</typeparam>
    /// <param name="appRelativeUrl">The application relative URL at which the service will be accessible.</param>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade WebService<TService>(string appRelativeUrl)
    {
      SystemManager.RegisterWebService(typeof (TService), appRelativeUrl);
      return this;
    }

    /// <summary>Registers a ServiceStack plugin.</summary>
    /// <param name="plugin">The ServiceStack plugin.</param>
    /// <param name="isHighPriority">If the priority level is hight we insert the plugin on the first place</param>
    /// <returns></returns>
    public ModuleInitializeFacade ServiceStackPlugin(
      IPlugin plugin,
      bool isHighPriority = false)
    {
      SystemManager.RegisterServiceStackPlugin(plugin, isHighPriority);
      return this;
    }

    /// <summary>
    /// Registers a web service with the specified URL. The class will be registered with the Unity container.
    /// </summary>
    /// <param name="serviceType">The type of the service class.</param>
    /// <param name="appRelativeUrl">The application relative URL at which the service will be accessible.</param>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade WebService(
      Type serviceType,
      string appRelativeUrl)
    {
      SystemManager.RegisterWebService(serviceType, appRelativeUrl);
      return this;
    }

    /// <summary>
    /// Registers a web service with the specified URL. The class will be registered with the Unity container.
    /// </summary>
    /// <param name="serviceType">The type of the service class.</param>
    /// <param name="appRelativeUrl">The application relative URL at which the service will be accessible.</param>
    /// <param name="hostFactory">The host factory which will be used.</param>
    /// <param name="requireBasicAuthentication">Whether or not this service supports Basic authentication</param>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade WebService(
      Type serviceType,
      string appRelativeUrl,
      ServiceHostFactory hostFactory,
      bool requireBasicAuthentication = false)
    {
      SystemManager.RegisterWebService(serviceType, appRelativeUrl, hostFactory, this.moduleName, requireBasicAuthentication);
      return this;
    }

    /// <summary>Adds the given route in the routing table.</summary>
    /// <param name="route">The route that will be added.</param>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    [Obsolete("Use the Route method, which accepts: name, route, requireBasicAuthentication")]
    public ModuleInitializeFacade AddRoute(System.Web.Routing.Route route)
    {
      SystemManager.RegisterRoute(route);
      return this;
    }

    /// <summary>Register the given route in the routing table.</summary>
    /// <param name="name"></param>
    /// <param name="route">The route that will be added.</param>
    /// <param name="requireBasicAuthentication"></param>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade Route(
      string name,
      RouteBase route,
      bool requireBasicAuthentication = false)
    {
      SystemManager.RegisterRoute(name, route, this.moduleName, requireBasicAuthentication);
      return this;
    }

    /// <summary>
    /// Initializes the dialog required by the module. Dialogs cannot be used prior to being initialized.
    /// </summary>
    /// <typeparam name="TDialog">Type of the dialog to be initialized.</typeparam>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade Dialog<TDialog>() where TDialog : DialogBase, new()
    {
      Dialogs.RegisterDialog<TDialog>();
      return this;
    }

    /// <summary>
    /// Initializes the dialog required by the module. Dialogs cannot be used prior to being initialized.
    /// </summary>
    /// <param name="dialogType">Type of the dialog to be initialized.</param>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade Dialog(Type dialogType)
    {
      Dialogs.RegisterDialog(dialogType);
      return this;
    }

    /// <summary>
    /// Initializes the Sitemap filter, class used to implement custom logic for preventing access to certain sitemap
    /// nodes.
    /// </summary>
    /// <typeparam name="TFilter">Type of the sitemap filter.</typeparam>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade SitemapFilter<TFilter>() where TFilter : ISitemapNodeFilter
    {
      ObjectFactory.RegisterSitemapNodeFilter<TFilter>(this.moduleName);
      return this;
    }

    /// <summary>
    /// Initializes the Sitemap filter, class used to implement custom logic for preventing access to certain sitemap
    /// nodes.
    /// </summary>
    /// <param name="sitemapNodeFilter">Type of the sitemap filter.</param>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade SitemapFilter(Type sitemapNodeFilter)
    {
      ObjectFactory.RegisterSitemapNodeFilter(sitemapNodeFilter, this.moduleName);
      return this;
    }

    /// <summary>
    /// Initializes the support for editable templates for the specified control.
    /// </summary>
    /// <typeparam name="TControl">Type of the control that supports editable templates.</typeparam>
    /// <typeparam name="TDataItem">Type of the data item that is being displayed by the control.</typeparam>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade TemplatableControl<TControl, TDataItem>()
    {
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl<TControl, TDataItem>();
      return this;
    }

    /// <summary>
    /// Registers basics settings view. This represents user friendly module administration UI that is accessible from Sitefinity basic settings screen
    /// </summary>
    /// <typeparam name="TControl">The type of the view control (view implementation).</typeparam>
    /// <param name="settingsName">Name of the settings. This is also used as the last part of the Url that opens the settings</param>
    /// <param name="settingsTitle">The settings title.</param>
    /// <param name="settingsResource">The settings title resource class. If null the title is not localized and is taken as it is</param>
    /// <returns></returns>
    public ModuleInitializeFacade BasicSettings<TControl>(
      string settingsName,
      string settingsTitle,
      string settingsResource)
    {
      SystemManager.RegisterBasicSettings(settingsName, typeof (TControl), settingsTitle, settingsResource);
      return this;
    }

    /// <summary>Basics the settings.</summary>
    /// <typeparam name="TControl">The type of the control.</typeparam>
    /// <param name="settingsName">Name of the settings.</param>
    /// <param name="settingsTitle">The settings title.</param>
    /// <param name="settingsResource">The settings resource.</param>
    /// <param name="dataContractType">Type of the data contract.</param>
    /// <param name="allowSettingPerSite">if set to <c>true</c> [allow setting per site].</param>
    /// <returns></returns>
    public ModuleInitializeFacade BasicSettings<TControl>(
      string settingsName,
      string settingsTitle,
      string settingsResource,
      Type dataContractType,
      bool allowSettingPerSite = false)
    {
      SystemManager.RegisterBasicSettings(settingsName, typeof (TControl), settingsTitle, settingsResource, dataContractType, allowSettingPerSite);
      return this;
    }

    /// <summary>Register a UserProfile type.</summary>
    /// <typeparam name="TUserProfile">The type of the user profile.</typeparam>
    /// <returns></returns>
    internal ModuleInitializeFacade UserProfileType<TUserProfile>() where TUserProfile : UserProfile
    {
      SystemManager.RegisterUserProfileType<TUserProfile>();
      return this;
    }

    /// <summary>
    /// Initializes the support for editable templates for the specified control.
    /// </summary>
    /// <param name="controlType">Type of the control that supports editable templates.</param>
    /// <param name="dataItemType">Type of the data item that is being displayed by the control.</param>
    /// <returns>The current instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    public ModuleInitializeFacade TemplatableControl(
      Type controlType,
      Type dataItemType)
    {
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(controlType, dataItemType);
      return this;
    }

    /// <summary>Provides fluent API for initializing toolbox items.</summary>
    /// <param name="targetToolbox">
    /// One of the values that determines for which toolbox fluent API will be loaded.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxInitializeFacade" />.</returns>
    [Obsolete("Use use ModuleInstallFacade.Toolbox")]
    public ToolboxInitializeFacade Toolbox(CommonToolbox targetToolbox) => new ToolboxInitializeFacade(targetToolbox, this.moduleName, this);

    /// <summary>Provides fluent API for initializing toolbox items.</summary>
    /// <param name="targetToolboxName">
    /// Name of the toolbox for which the fluent API will be loaded.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxInitializeFacade" />.</returns>
    [Obsolete("Use use ModuleInstallFacade.Toolbox")]
    public ToolboxInitializeFacade Toolbox(string targetToolboxName) => !string.IsNullOrEmpty(targetToolboxName) ? new ToolboxInitializeFacade(targetToolboxName, this.moduleName, this) : throw new ArgumentNullException(nameof (targetToolboxName));

    [Obsolete("Use use ModuleInstallFacade.Toolbox")]
    public ToolboxInitializeFacade Toolbox(Telerik.Sitefinity.Modules.Pages.Configuration.Toolbox toolbox) => toolbox != null ? new ToolboxInitializeFacade(toolbox, this.moduleName, this) : throw new ArgumentNullException(nameof (toolbox));

    internal ModuleInitializeFacade RegisterDataSource(IDataSource dataSource)
    {
      if (dataSource == null)
        throw new ArgumentNullException(nameof (dataSource));
      SystemManager.DataSourceRegistry.RegisterDataSource(dataSource);
      return this;
    }

    internal ModuleNodeFacade<ModuleInitializeFacade> InitializeModulePage(
      Guid moduleNodeId,
      string groupPageName)
    {
      return new ModuleNodeFacade<ModuleInitializeFacade>(this.pageManager, this.moduleName, moduleNodeId, groupPageName, this);
    }

    /// <summary>
    /// Commits all the items that have been placed into the transaction while working with the fluent API.
    /// </summary>
    /// <returns>The instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" />.</returns>
    internal ModuleInitializeFacade SaveChanges()
    {
      this.PageManager.SaveChanges();
      return this;
    }
  }
}
