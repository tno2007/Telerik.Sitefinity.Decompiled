// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ObjectFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Runtime.CompilerServices;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Configuration.Fluent;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Security;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.Sitefinity.Abstractions.TemporaryStorage;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Analytics.Server.Infrastructure.Web.Services;
using Telerik.Sitefinity.BackgroundTasks;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Web.UI.Basic;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.ContentLocations.Configuration;
using Telerik.Sitefinity.ContentLocations.Web;
using Telerik.Sitefinity.ContentUsages;
using Telerik.Sitefinity.ContentUsages.Sources;
using Telerik.Sitefinity.Dashboard;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Data.Summary;
using Telerik.Sitefinity.Data.Utilities.Exporters;
using Telerik.Sitefinity.DesignerToolbox;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Events;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Filters;
using Telerik.Sitefinity.Fluent.AnyContent;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation;
using Telerik.Sitefinity.Fluent.Permissions;
using Telerik.Sitefinity.GeoLocations;
using Telerik.Sitefinity.GeoLocations.Configuration;
using Telerik.Sitefinity.InlineEditing;
using Telerik.Sitefinity.InlineEditing.Resolvers;
using Telerik.Sitefinity.InlineEditing.Setters;
using Telerik.Sitefinity.InlineEditing.Strategies;
using Telerik.Sitefinity.Installer;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.LoadBalancing.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Localization.Web;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Locations.Configuration;
using Telerik.Sitefinity.Logging;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ModuleEditor;
using Telerik.Sitefinity.ModuleEditor.Configuration;
using Telerik.Sitefinity.ModuleEditor.Web;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.ModuleEditor.Web.UI;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Modules.ControlTemplates.Web.UI;
using Telerik.Sitefinity.Modules.Files;
using Telerik.Sitefinity.Modules.Files.Web.UI;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI.Dialogs;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.UserFiles;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Libraries.Videos.Thumbnails;
using Telerik.Sitefinity.Modules.Libraries.Web;
using Telerik.Sitefinity.Modules.Libraries.Web.UI;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Images;
using Telerik.Sitefinity.Modules.Newsletters.Communication;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Modules.RelatedData.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Web;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls;
using Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Modules.UserProfiles.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Packaging.Configuration;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.ProtectionShield;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Twitter;
using Telerik.Sitefinity.Publishing.Web;
using Telerik.Sitefinity.Publishing.Web.UI;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.RecycleBin.Conflicts;
using Telerik.Sitefinity.RecycleBin.ItemFactories;
using Telerik.Sitefinity.RecycleBin.Security;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Renderer;
using Telerik.Sitefinity.Renderer.Editor;
using Telerik.Sitefinity.Renderer.Generators;
using Telerik.Sitefinity.Renderer.Web.Services;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Security.Web.UI;
using Telerik.Sitefinity.Security.Web.UI.Principals;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Configuration;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.RelatedData.ResponseBuilders;
using Telerik.Sitefinity.SiteSettings.Basic;
using Telerik.Sitefinity.SiteSettings.Configuration;
using Telerik.Sitefinity.Statistics;
using Telerik.Sitefinity.SyncMap.Configuration;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Configuration;
using Telerik.Sitefinity.Taxonomies.Web.Services.Contracts;
using Telerik.Sitefinity.Taxonomies.Web.UI;
using Telerik.Sitefinity.Taxonomies.Web.UI.Flat;
using Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical;
using Telerik.Sitefinity.TrackingConsent.UI;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.ColumnProviders;
using Telerik.Sitefinity.Versioning.Configuration;
using Telerik.Sitefinity.Versioning.Web.Services;
using Telerik.Sitefinity.Versioning.Web.UI.Basic;
using Telerik.Sitefinity.Versioning.Web.UI.Views;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Compilation;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.Configuration.ContentView.Plugins;
using Telerik.Sitefinity.Web.Configuration.FieldControls;
using Telerik.Sitefinity.Web.Mail;
using Telerik.Sitefinity.Web.OutputCache;
using Telerik.Sitefinity.Web.OutputCache.Configuration;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Countries;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.System;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.TimeZone;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Sitefinity.Web.UI.Backend.Elements;
using Telerik.Sitefinity.Web.UI.Backend.Security.Permissions;
using Telerik.Sitefinity.Web.UI.Components;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.PublicControls;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;
using Telerik.Sitefinity.Web.UrlShorteners;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Activities;
using Telerik.Sitefinity.Workflow.Configuration;
using Telerik.Sitefinity.Workflow.ContentTypeResolvers;
using Telerik.Sitefinity.Workflow.UI;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Represents a static factory class for creating application objects.
  /// </summary>
  public static class ObjectFactory
  {
    /// <summary>
    /// Fired before initializing Sitefinity application.
    /// Note this event could be fired multiple times by different
    /// process and services upon starting their initialization.
    /// </summary>
    public static EventHandler<ExecutingEventArgs> Initializing;
    /// <summary>
    /// Fired after initialization of Sitefinity application.
    /// Note this event could be fired multiple times by different
    /// process and services upon finishing their initialization.
    /// </summary>
    public static EventHandler<ExecutedEventArgs> Initialized;
    private static IUnityContainer container;
    private static readonly object ContainerLockObject = new object();
    private static QueryableContainerExtension queryableContainerExtension;
    /// <summary>The logging files directory</summary>
    [Obsolete("Use the Log.MapLogFilePath method instead.")]
    public const string LoggingFilesDirectory = "~/App_Data/Sitefinity/Logs/";

    /// <summary>
    /// Gets a static <see cref="T:Telerik.Microsoft.Practices.Unity.IUnityContainer" />.
    /// </summary>
    public static IUnityContainer Container
    {
      get
      {
        IUnityContainer container = ObjectFactory.container;
        if (container == null)
        {
          lock (ObjectFactory.ContainerLockObject)
          {
            container = ObjectFactory.container;
            if (container == null)
              ObjectFactory.RegisterIoCTypes();
            container = ObjectFactory.container;
          }
        }
        return container;
      }
    }

    internal static bool Registering { [MethodImpl(MethodImplOptions.Synchronized)] get; private set; }

    private static QueryableContainerExtension QueryableExtension
    {
      get
      {
        lock (ObjectFactory.ContainerLockObject)
          return ObjectFactory.queryableContainerExtension;
      }
    }

    /// <summary>
    /// Executes the specified <paramref name="delegateToExecute" /> with a replaced ObjectFactory.Container instance
    /// which is passed as the <paramref name="container" />
    /// </summary>
    /// <param name="container">The container to use.</param>
    /// <param name="delegateToExecute">The delegate to execute.</param>
    /// <param name="disposeContainer">Specified whether to dispose the specified <paramref name="container" />
    /// after executing the delegate </param>
    public static void RunWithContainer(
      IUnityContainer container,
      Action delegateToExecute,
      bool disposeContainer = true)
    {
      QueryableContainerExtension containerExtension = new QueryableContainerExtension();
      container?.AddExtension((UnityContainerExtension) containerExtension);
      ObjectFactory.RunWithContainer(container, containerExtension, delegateToExecute, disposeContainer);
    }

    /// <summary>
    /// Executes the specified <paramref name="delegateToExecute" /> with a replaced ObjectFactory.Container and
    /// ObjectFactory.QueryableExtension instances.
    /// </summary>
    /// <param name="container">The container to use.</param>
    /// <param name="queryableExtensions">The queryable object which extends the Unity container.</param>
    /// <param name="delegateToExecute">The delegate to execute.</param>
    /// <param name="disposeContainer">Specified whether to dispose the specified <paramref name="container" />
    /// after executing the delegate</param>
    /// <remarks>
    /// The <paramref name="queryableExtensions" /> object is not added to the extensions objects of the passed
    /// <paramref name="container" />. You can do this by calling container.AddExtension(queryableExtensions);
    /// </remarks>
    public static void RunWithContainer(
      IUnityContainer container,
      QueryableContainerExtension queryableExtensions,
      Action delegateToExecute,
      bool disposeContainer = true)
    {
      IUnityContainer container1 = ObjectFactory.container;
      QueryableContainerExtension containerExtension = ObjectFactory.queryableContainerExtension;
      try
      {
        ObjectFactory.container = container;
        ObjectFactory.queryableContainerExtension = queryableExtensions;
        delegateToExecute();
      }
      finally
      {
        ObjectFactory.container = container1;
        ObjectFactory.queryableContainerExtension = containerExtension;
        if (disposeContainer)
          container.Dispose();
      }
    }

    /// <summary>
    /// Get an instance of the requested type with the given name from the container.
    /// </summary>
    /// <typeparam name="T"><see cref="T:System.Type" /> of object to get from the container.</typeparam>
    /// <returns>The retrieved object.</returns>
    public static T Resolve<T>() => ObjectFactory.Container.Resolve<T>();

    /// <summary>
    /// Get an instance of the requested type with the given name from the container.
    /// </summary>
    /// <typeparam name="T"><see cref="T:System.Type" /> of object to get from the container.</typeparam>
    /// <param name="name">Name of the object to retrieve.</param>
    /// <returns>The retrieved object.</returns>
    public static T Resolve<T>(string name) => ObjectFactory.Container.Resolve<T>(name);

    /// <summary>
    /// Get an instance of the default requested type from the container.
    /// </summary>
    /// <param name="type"><see cref="T:System.Type" /> of object to get from the container.</param>
    /// <returns>The retrieved object.</returns>
    public static object Resolve(Type type) => ObjectFactory.Container.Resolve(type, (string) null);

    /// <summary>
    /// Get an instance of the requested type with the given name from the container.
    /// </summary>
    /// <param name="type"><see cref="T:System.Type" /> of object to get from the container.</param>
    /// <param name="name">Name of the object to retrieve.</param>
    /// <returns>The retrieved object.</returns>
    public static object Resolve(Type type, string name) => ObjectFactory.Container.Resolve(type, name);

    /// <summary>
    /// Test if a type is registered as singleton with container.
    /// </summary>
    /// <typeparam name="T"><see cref="T:System.Type" /> that will be requested.</typeparam>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public static bool IsTypeRegistered<T>() => ObjectFactory.QueryableExtension.IsTypeRegistered<T>();

    /// <summary>
    /// Test if a type is registered as singleton with container.
    /// </summary>
    /// <typeparam name="T"><see cref="T:System.Type" /> that will be requested.</typeparam>
    /// <param name="namedInstance">The name of the instance.</param>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public static bool IsTypeRegistered<T>(string namedInstance) => ObjectFactory.QueryableExtension.IsTypeRegistered<T>(namedInstance);

    /// <summary>
    /// Test if a type is registered as singleton with container.
    /// </summary>
    /// <typeparam name="TFrom"><see cref="T:System.Type" /> that will be requested.</typeparam>
    /// <typeparam name="TTo"><see cref="T:System.Type" /> that will actually be returned.</typeparam>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public static bool IsTypeRegistered<TFrom, TTo>() => ObjectFactory.QueryableExtension.IsTypeRegistered<TFrom, TTo>();

    /// <summary>Test if a type is registered with container.</summary>
    /// <typeparam name="TFrom"><see cref="T:System.Type" /> that will be requested.</typeparam>
    /// <typeparam name="TTo"><see cref="T:System.Type" /> that will actually be returned.</typeparam>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public static bool IsTypeRegisteredAsSingleton<TFrom, TTo>() => ObjectFactory.QueryableExtension.IsTypeRegisteredAsSingleton<TFrom, TTo>();

    /// <summary>Test if a type is registered with container.</summary>
    /// <param name="type"><see cref="T:System.Type" /> that will be requested.</param>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public static bool IsTypeRegistered(Type type) => ObjectFactory.QueryableExtension.IsTypeRegistered(type);

    /// <summary>Test if a type is registered with container.</summary>
    /// <param name="from"><see cref="T:System.Type" /> that will be requested.</param>
    /// <param name="to"><see cref="T:System.Type" /> that will actually be returned.</param>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public static bool IsTypeRegistered(Type from, Type to) => ObjectFactory.QueryableExtension.IsTypeRegistered(from, to);

    /// <summary>Test if a type is registered with container.</summary>
    /// <param name="from"><see cref="T:System.Type" /> that will be requested.</param>
    /// <param name="to"><see cref="T:System.Type" /> that will actually be returned.</param>
    /// <param name="isSingleton">If true the type is tested if it is registered as singleton.</param>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public static bool IsTypeRegistered(Type from, Type to, bool isSingleton) => ObjectFactory.QueryableExtension.IsTypeRegistered(from, to, (string) null, isSingleton);

    /// <summary>Test if a type is registered with container.</summary>
    /// <param name="from"><see cref="T:System.Type" /> that will be requested.</param>
    /// <param name="to"><see cref="T:System.Type" /> that will actually be returned.</param>
    /// <param name="namedInstance">The name of the instance.</param>
    /// <param name="isSingleton">If true the type is tested if it is registered as singleton.</param>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public static bool IsTypeRegistered(
      Type from,
      Type to,
      string namedInstance,
      bool isSingleton)
    {
      return ObjectFactory.QueryableExtension.IsTypeRegistered(from, to, namedInstance, isSingleton);
    }

    /// <summary>
    /// Gets an array of <see cref="T:Telerik.Microsoft.Practices.Unity.RegisterEventArgs" /> for registered types
    /// that inherit form the provided type.
    /// </summary>
    /// <param name="baseType">The base type to search for.</param>
    /// <returns><see cref="T:System.Collections.Generic.IEnumerable`1" /></returns>
    public static IEnumerable<RegisterEventArgs> GetArgsForType(
      Type baseType)
    {
      return ObjectFactory.QueryableExtension.GetArgsForType(baseType);
    }

    /// <summary>
    /// Gets the arguments for the first registered type that matches the parameters.
    /// </summary>
    /// <param name="name">The name by which the type was registered.</param>
    /// <param name="baseType">A base type that the named type must inherit.</param>
    /// <returns><see cref="T:Telerik.Microsoft.Practices.Unity.RegisterEventArgs" /></returns>
    public static RegisterEventArgs GetArgsByName(string name, Type baseType) => ObjectFactory.QueryableExtension.GetArgsByName(name, baseType);

    /// <summary>Gets the type for the first match of the parameters.</summary>
    /// <param name="name">The name by which the type was registered.</param>
    /// <param name="baseType">A base type that the named type must inherit.</param>
    /// <returns>The type if found else null.</returns>
    public static Type GetNamedType(string name, Type baseType) => ObjectFactory.QueryableExtension.GetNamedType(name, baseType);

    /// <summary>
    /// Raised when the inversion of control types registrations is starting.
    /// This can happen multiple times since the container in which those registrations
    /// are performed is recreated on each soft restart.
    /// </summary>
    public static event EventHandler<EventArgs> RegisteringIoCTypes;

    /// <summary>
    /// Raised when the inversion of control types registrations have finished.
    /// This can happen multiple times since the container in which those registrations
    /// are performed is recreated on each soft restart.
    /// </summary>
    public static event EventHandler<EventArgs> RegisteredIoCTypes;

    /// <summary>Register a sitemap node filter</summary>
    /// <typeparam name="T">Type of the sitemap node filter. Must implement <c>ISitemapNodeFilter</c></typeparam>
    /// <param name="name">Name of the filter. Must be unique.</param>
    /// <remarks>
    /// If <paramref name="name" /> is null or empty, the filter will be registered with the full name of its CLR type.
    /// The filter should have an empty constructor.
    /// </remarks>
    /// <seealso cref="T:Telerik.Sitefinity.Web.ISitemapNodeFilter" />
    public static void RegisterSitemapNodeFilter<T>(string name) where T : ISitemapNodeFilter => ObjectFactory.RegisterSitemapNodeFilter(typeof (T), name);

    /// <summary>Registers a sitemap node filter.</summary>
    /// <param name="filterType">The type of the sitemap node filter. Must implement <see cref="T:Telerik.Sitefinity.Web.ISitemapNodeFilter" />.</param>
    /// <param name="name">Name of the filter. Must ne unique.</param>
    /// <remarks>
    /// If <paramref name="name" /> is null or empty, the filter will be registered with the full name of its CLR type.
    /// The filter should have an empty constructor.
    /// </remarks>
    /// <seealso cref="T:Telerik.Sitefinity.Web.ISitemapNodeFilter" />
    public static void RegisterSitemapNodeFilter(Type filterType, string name)
    {
      if (filterType == (Type) null)
        throw new ArgumentNullException(nameof (filterType));
      if (string.IsNullOrEmpty(name))
        name = filterType.FullName;
      ObjectFactory.Container.RegisterType(typeof (ISitemapNodeFilter), filterType, name, (LifetimeManager) new ContainerControlledLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
    }

    /// <summary>Registers a dynamic WCF rest full service.</summary>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="appRelativeUrl">The app relative URL.</param>
    [Obsolete("This method is moved to the SystemManager class")]
    public static void RegisterWebService(Type serviceType, string appRelativeUrl) => SystemManager.RegisterWebService(serviceType, appRelativeUrl);

    /// <summary>
    /// Initializes and registers types with IoC framework.
    /// IMPORTANT!!! The method creates new container and therefore any
    /// subsequent call to the method will replace the old container.
    /// </summary>
    internal static void RegisterIoCTypes()
    {
      ObjectFactory.Registering = true;
      if (ObjectFactory.container != null)
        ObjectFactory.container.Dispose();
      ObjectFactory.container = (IUnityContainer) new UnityContainer();
      ObjectFactory.queryableContainerExtension = new QueryableContainerExtension();
      ObjectFactory.container.AddExtension((UnityContainerExtension) ObjectFactory.QueryableExtension);
      ObjectFactory.container.AddNewExtension<Interception>();
      ObjectFactory.OnRegisteringIoCTypes((object) null, EventArgs.Empty);
      ObjectFactory.container.RegisterType(typeof (IManagerFactory<>), typeof (ManagerFactory<>));
      ObjectFactory.RegisterDatabaseMappingContexts();
      ObjectFactory.RegisterConfigurationClasses();
      ObjectFactory.RegisterStringResources();
      ObjectFactory.RegisterRegexStrategy();
      ConfigurationSourceBuilder configBuilder = new ConfigurationSourceBuilder();
      ObjectFactory.ConfigureLogging(configBuilder);
      ObjectFactory.ConfigureExceptionHandling(configBuilder);
      ObjectFactory.ConfigureCaching(configBuilder);
      DictionaryConfigurationSource source = new DictionaryConfigurationSource();
      configBuilder.UpdateConfigurationWithReplace((IConfigurationSource) source);
      ObjectFactory.container.AddExtension((UnityContainerExtension) new EnterpriseLibraryCoreExtension((IConfigurationSource) source));
      EnterpriseLibraryContainer.Current = (IServiceLocator) new UnityServiceLocator(ObjectFactory.container);
      ObjectFactory.ConfigureInterception();
      ObjectFactory.RegisterConfigRestrictionStrategies();
      ObjectFactory.RegisterDialogs();
      ObjectFactory.RegisterCommonTypes();
      ObjectFactory.RegisterVirtualFileResolvers();
      ObjectFactory.RegisterBasicSettings();
      ObjectFactory.RegisterInlineEditing();
      ObjectFactory.RegisterRelatedData();
      ObjectFactory.RegisterRecycleBin();
      ObjectFactory.RegisterCompilation();
      ObjectFactory.RegisterFilterStrategies();
      ObjectFactory.RegisterOperationProviders();
      ObjectFactory.RegisterRedirectValidation();
      ObjectFactory.RegisterRenderer();
      ObjectFactory.Registering = false;
      PropertyEventContainer.ClearHandlers();
      PropertyEventContainer.RegisterPropertyChanging(typeof (IDataItem), typeof (CheckModifyPermissionsCallHandler));
      PropertyEventContainer.RegisterPropertyChanging(typeof (PageNode), typeof (CheckLinkedNodeCallHandler));
      PropertyEventContainer.RegisterPropertyChanging(typeof (Site), typeof (CheckMultisitePermissionsCallHandler));
      ObjectFactory.OnRegisteredIoCTypes((object) null, EventArgs.Empty);
    }

    internal static void Clear()
    {
      lock (ObjectFactory.ContainerLockObject)
      {
        if (ObjectFactory.container != null)
        {
          ObjectFactory.container.Dispose();
          ObjectFactory.container = (IUnityContainer) null;
        }
        ObjectFactory.queryableContainerExtension = (QueryableContainerExtension) null;
      }
    }

    private static void OnRegisteringIoCTypes(object sender, EventArgs args)
    {
      if (ObjectFactory.RegisteringIoCTypes == null)
        return;
      ObjectFactory.RegisteringIoCTypes(sender, args);
    }

    private static void OnInitializing(object sender, ExecutingEventArgs args)
    {
      if (ObjectFactory.Initializing == null)
        return;
      ObjectFactory.Initializing(sender, args);
    }

    private static void OnInitialized(object sender, ExecutedEventArgs args)
    {
      if (ObjectFactory.Initialized == null)
        return;
      ObjectFactory.Initialized(sender, args);
    }

    private static void OnRegisteredIoCTypes(object sender, EventArgs args)
    {
      if (ObjectFactory.RegisteredIoCTypes == null)
        return;
      ObjectFactory.RegisteredIoCTypes(sender, args);
    }

    private static void RegisterVirtualFileResolvers()
    {
      ObjectFactory.container.RegisterType<IVirtualFileResolver, SitefinityPageResolver>("PageResolver", (LifetimeManager) new ContainerControlledLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
      ObjectFactory.container.RegisterType<IVirtualFileResolver, EmbeddedResourceResolver>("EmbeddedResourceResolver", (LifetimeManager) new ContainerControlledLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
      ObjectFactory.container.RegisterType<IVirtualFileResolver, FileSystemResolver>("FileSystemResolver", (LifetimeManager) new ContainerControlledLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
      ObjectFactory.container.RegisterType<IVirtualFileResolver, ControlPresentationResolver>("ControlPresentationResolver", (LifetimeManager) new ContainerControlledLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
    }

    private static void RegisterFilterStrategies()
    {
      ObjectFactory.Container.RegisterType(typeof (IFilterStrategy), typeof (ContentClassificationFilterStrategy), typeof (ContentClassificationFilterStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType(typeof (IFilterStrategy), typeof (TaxonomyFilterStrategy), typeof (TaxonomyFilterStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType(typeof (IFilterStrategy), typeof (OwnerFilterStrategy), typeof (OwnerFilterStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType(typeof (IFilterStrategy), typeof (WorkflowFilterStrategy), typeof (WorkflowFilterStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType(typeof (IFilterStrategy), typeof (StatusFilterStrategy), typeof (StatusFilterStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType(typeof (IFilterStrategy), typeof (StatusProviderFilterStrategy), typeof (StatusProviderFilterStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType(typeof (IFilterStrategy), typeof (LastModifiedFilterStrategy), typeof (LastModifiedFilterStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType(typeof (IFilterStrategy), typeof (ContentItemFilterStrategy), typeof (ContentItemFilterStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType(typeof (IFilterStrategy), typeof (TemplatesInSitesFilterStrategy), typeof (TemplatesInSitesFilterStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
    }

    private static void RegisterOperationProviders()
    {
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (SystemOperatonsProvider), typeof (SystemOperatonsProvider).Name);
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (TimeZoneOperationsProvider), typeof (TimeZoneOperationsProvider).Name);
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (CountriesOperationProvider), typeof (CountriesOperationProvider).Name);
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (FieldSettingsOperationProvider), typeof (FieldSettingsOperationProvider).Name);
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (VersionOperationProvider), typeof (VersionOperationProvider).Name);
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (AnalyticsOperationProvider), typeof (AnalyticsOperationProvider).Name);
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (DiagnosticsOperationProvider), typeof (DiagnosticsOperationProvider).Name);
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (WebFormsAndMvcRendererOperationProvider), typeof (WebFormsAndMvcRendererOperationProvider).Name);
      ObjectFactory.Container.RegisterType(typeof (IPropertiesAdaptor), typeof (ComponentAdaptorBase), typeof (ComponentAdaptorBase).Name);
      ObjectFactory.Container.RegisterType(typeof (IPropertiesAdaptor), typeof (LayoutControlAdaptor), typeof (LayoutControlAdaptor).Name);
      ObjectFactory.Container.RegisterType<IAdaptorFactory, AdaptorFactory>();
    }

    private static void RegisterRedirectValidation() => ObjectFactory.Container.RegisterInstance<IRedirectUriValidator>((IRedirectUriValidator) new SitefinityRedirectUriValidator(), (LifetimeManager) new ContainerControlledLifetimeManager());

    private static void RegisterDatabaseMappingContexts()
    {
      OpenAccessConnection.RegisterDatabaseMappingContext<MsSqlDatabaseMappingContext>(DatabaseType.MsSql);
      OpenAccessConnection.RegisterDatabaseMappingContext<SqlAzureDatabaseMappingContext>(DatabaseType.SqlAzure);
      OpenAccessConnection.RegisterDatabaseMappingContext<OracleDatabaseMappingContext>(DatabaseType.Oracle);
      OpenAccessConnection.RegisterDatabaseMappingContext<MySqlDatabaseMappingContext>(DatabaseType.MySQL);
      OpenAccessConnection.RegisterDatabaseMappingContext<PostgreSqlDatabaseMappingContext>(DatabaseType.PostgreSql);
    }

    private static void RegisterRegexStrategy()
    {
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (RegisterRegexStrategy), (object) null);
      ObjectFactory.OnInitializing((object) null, args);
      if (!args.Cancel)
        ObjectFactory.container.RegisterType<RegexStrategy, RegexStrategy>((LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.OnInitialized((object) null, new ExecutedEventArgs(nameof (RegisterRegexStrategy), (object) null));
    }

    private static void ConfigureLogging(ConfigurationSourceBuilder configBuilder)
    {
      ObjectFactory.container.RegisterInstance<ISitefinityLogCategoryConfigurator>(Log.GetDefaultCategoryConfigurator());
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (ConfigureLogging), (object) configBuilder);
      ObjectFactory.OnInitializing((object) null, args);
      if (args.Cancel)
        return;
      Log.Configure((IConfigurationSourceBuilder) configBuilder);
      ObjectFactory.OnInitialized((object) null, new ExecutedEventArgs(nameof (ConfigureLogging), (object) configBuilder));
    }

    private static void ConfigureExceptionHandling(ConfigurationSourceBuilder configBuilder)
    {
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (ConfigureExceptionHandling), (object) configBuilder);
      ObjectFactory.OnInitializing((object) null, args);
      if (!args.Cancel)
        Exceptions.Configure(configBuilder);
      ObjectFactory.OnInitialized((object) null, new ExecutedEventArgs(nameof (ConfigureExceptionHandling), (object) configBuilder));
    }

    private static void ConfigureCaching(ConfigurationSourceBuilder configBuilder)
    {
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (ConfigureCaching), (object) configBuilder);
      ObjectFactory.OnInitializing((object) null, args);
      if (!args.Cancel)
      {
        IUnityContainer container = ObjectFactory.container;
        CacheManagerInstance cacheManagerInstance = CacheManagerInstance.Configuration;
        string name1 = cacheManagerInstance.ToString();
        InjectionMember[] injectionMemberArray = Array.Empty<InjectionMember>();
        container.RegisterType<ICacheManager, NoCacheManager>(name1, injectionMemberArray);
        ConfigElementDictionary<string, CacheManagerElement> cacheManagers = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().CacheManagers;
        ICachingConfiguration context = configBuilder.ConfigureCaching();
        foreach (CacheManagerElement cacheManagerElement in (IEnumerable<CacheManagerElement>) cacheManagers.Values)
        {
          ICachingConfigurationCacheManagerOptions withOptions = context.ForCacheManagerNamed(cacheManagerElement.Name).WithOptions;
          withOptions.PollWhetherItemsAreExpiredIntervalSeconds(cacheManagerElement.PollWhetherItemsAreExpiredIntervalSeconds).StartScavengingAfterItemCount(cacheManagerElement.StartScavengingAfterItemCount).WhenScavengingRemoveItemCount(cacheManagerElement.WhenScavengingRemoveItemCount);
          string name2 = cacheManagerElement.Name;
          cacheManagerInstance = CacheManagerInstance.Global;
          string str = cacheManagerInstance.ToString();
          if (name2 == str)
            withOptions.UseAsDefaultCache();
          switch (cacheManagerElement.CacheStore)
          {
            case CacheStore.InMemory:
              withOptions.StoreInMemory();
              continue;
            case CacheStore.SharedBackingStore:
              withOptions.StoreInSharedBackingStore(cacheManagerElement.BackingStoreName);
              continue;
            case CacheStore.IsolatedStorage:
              withOptions.StoreInIsolatedStorage(cacheManagerElement.BackingStoreName);
              continue;
            case CacheStore.CustomStore:
              withOptions.StoreInCustomStore(cacheManagerElement.BackingStoreName, cacheManagerElement.CustomCacheStoreType);
              continue;
            default:
              continue;
          }
        }
      }
      ObjectFactory.OnInitialized((object) null, new ExecutedEventArgs(nameof (ConfigureCaching), (object) configBuilder));
    }

    private static void ConfigureInterception()
    {
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (ConfigureInterception), (object) new string[15]
      {
        "Save*",
        "Load*",
        "Set*",
        "Get*",
        "Commit*",
        "Delete*",
        "Abort*",
        "Add*",
        "Create*",
        "New*",
        "Init*",
        "Remove*",
        "Move*",
        "Change*",
        "Flush*"
      });
      ObjectFactory.OnInitializing((object) null, args);
      if (!args.Cancel)
      {
        AnyMatchingRuleSet anyMatchingRuleSet = new AnyMatchingRuleSet();
        anyMatchingRuleSet.Add((IMatchingRule) new MemberNameMatchingRule((IEnumerable<string>) (string[]) args.CommandArguments));
        anyMatchingRuleSet.Add((IMatchingRule) new TagAttributeMatchingRule("DataExecution"));
        AnyMatchingRuleSet instance = anyMatchingRuleSet;
        ObjectFactory.container.Configure<Interception>().AddPolicy("DataExecution").AddMatchingRule((IMatchingRule) new IgnoreSpecialMethodsMatchingRule()).AddMatchingRule((IMatchingRule) instance).AddCallHandler((ICallHandler) new SitefinityAuthorizationCallHandler()).AddCallHandler((ICallHandler) new DataEventsCallHandler());
      }
      ObjectFactory.OnInitialized((object) null, new ExecutedEventArgs(nameof (ConfigureInterception), (object) null));
    }

    private static void RegisterConfigRestrictionStrategies()
    {
      ObjectFactory.container.RegisterType<IPageNodeRestrictionStrategy, PageNodeRestrictionStrategy>(typeof (PageNodeRestrictionStrategy).FullName, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.container.RegisterType<ICommandWidgetRestrictionStrategy, CommandWidgetReadOnlyConfigRestrictionStrategy>(typeof (CommandWidgetReadOnlyConfigRestrictionStrategy).FullName, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.container.RegisterType<IColumnRestrictionStrategy, ColumnRestrictionStrategy>(typeof (ColumnRestrictionStrategy).FullName, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.container.RegisterType<ICommandWidgetRestrictionStrategy, WorkflowCommandWidgetRestrictionStrategy>(typeof (WorkflowCommandWidgetRestrictionStrategy).FullName, (LifetimeManager) new ContainerControlledLifetimeManager());
    }

    private static void RegisterConfigurationClasses()
    {
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (RegisterConfigurationClasses), (object) null);
      ObjectFactory.OnInitializing((object) null, args);
      if (!args.Cancel)
      {
        Telerik.Sitefinity.Configuration.Config.RegisterSection<VirtualPathSettingsConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<ControlsConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<AppearanceConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<ResourcesConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<CulturesConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<SecurityConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<UserProfilesConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<ProjectConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<LoginConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<MetadataConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<ContentConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<SystemConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<DataConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<PagesConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<ToolboxesConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<FieldTemplatesConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<FieldControlsConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<ContentViewConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<CommentsConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<ContentPluginsConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<ItemToManagerMappingConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<VersionConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<PublishingConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<WorkflowConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<TaxonomyConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<CustomFieldsConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<ContentLinksConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<ContentLocationsConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<UserActivityConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<LibrariesConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<SiteSettingsConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<SyncMapConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<StartupConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<GeoLocationsConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<LocationsConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<SchedulingConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<InlineEditingConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<CommentsModuleConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<RelatedDataConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<PackagingConfig>();
        Telerik.Sitefinity.Configuration.Config.RegisterSection<OutputCacheConfig>();
      }
      ObjectFactory.OnInitialized((object) null, new ExecutedEventArgs(nameof (RegisterConfigurationClasses), (object) null));
    }

    private static void RegisterStringResources()
    {
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (RegisterStringResources), (object) null);
      ObjectFactory.OnInitializing((object) null, args);
      if (!args.Cancel)
      {
        Res.RegisterResource<ErrorMessages>();
        Res.RegisterResource<Labels>();
        Res.RegisterResource<Help>();
        Res.RegisterResource<PageResources>();
        Res.RegisterResource<PageTemplateResources>();
        Res.RegisterResource<UserProfilesResources>();
        Res.RegisterResource<ConfigDescriptions>();
        Res.RegisterResource<LocalizationConfigDescriptions>();
        Res.RegisterResource<TemplateDescriptions>();
        Res.RegisterResource<SecurityResources>();
        Res.RegisterResource<ContentResources>();
        Res.RegisterResource<ValidationMessages>();
        Res.RegisterResource<FilesResources>();
        Res.RegisterResource<RadAsyncUpload>();
        Res.RegisterResource<RadEditorTools>();
        Res.RegisterResource<RadEditorModules>();
        Res.RegisterResource<RadEditorMain>();
        Res.RegisterResource<RadEditorDialogs>();
        Res.RegisterResource<RadImageEditorMain>();
        Res.RegisterResource<RadImageEditorDialogs>();
        Res.RegisterResource<RadListBoxResources>();
        Res.RegisterResource<RadProgressArea>();
        Res.RegisterResource<RadSchedulerMain>();
        Res.RegisterResource<RadSpellDialog>();
        Res.RegisterResource<RadSpreadsheet>();
        Res.RegisterResource<RadUploadResources>();
        Res.RegisterResource<RadComboBoxResources>();
        Res.RegisterResource<RadColorPicker>();
        Res.RegisterResource<RadSocialShare>();
        Res.RegisterResource<RadWindow>();
        Res.RegisterResource<RadCalendarMain>();
        Res.RegisterResource<RadDatePickerMain>();
        Res.RegisterResource<RadDateTimePickerMain>();
        Res.RegisterResource<RadDropDownTree>();
        Res.RegisterResource<RadImageGalleryMain>();
        Res.RegisterResource<RadMonthYearPickerMain>();
        Res.RegisterResource<RadDataPagerMain>();
        Res.RegisterResource<RadFilterMain>();
        Res.RegisterResource<RadPivotGridMain>();
        Res.RegisterResource<RadTreeListMain>();
        Res.RegisterResource<RadGridMain>();
        Res.RegisterResource<RadSchedulerRecurrenceEditor>();
        Res.RegisterResource<RadMediaPlayerMain>();
        Res.RegisterResource<ContentItemSectionNames>();
        Res.RegisterResource<ControlResources>();
        Res.RegisterResource<ContentViewPluginMessages>();
        Res.RegisterResource<SummaryResources>();
        Res.RegisterResource<FormsResources>();
        Res.RegisterResource<VersionResources>();
        Res.RegisterResource<DataResources>();
        Res.RegisterResource<LicensingMessages>();
        Res.RegisterResource<LibrariesResources>();
        Res.RegisterResource<ImagesResources>();
        Res.RegisterResource<VideosResources>();
        Res.RegisterResource<DocumentsResources>();
        Res.RegisterResource<PublicControlsResources>();
        Res.RegisterResource<PublishingMessages>();
        Res.RegisterResource<ContentLifecycleMessages>();
        Res.RegisterResource<WorkflowResources>();
        Res.RegisterResource<ApprovalWorkflowResources>();
        Res.RegisterResource<SchedulingResources>();
        Res.RegisterResource<AddressFieldResources>();
        Res.RegisterResource<Telerik.Sitefinity.Localization.LocalizationResources>();
        Res.RegisterResource<TaxonomyResources>();
        Res.RegisterResource<ModuleEditorResources>();
        Res.RegisterResource<DynamicModuleResources>();
        Res.RegisterResource<UserFilesResources>();
        Res.RegisterResource<ContentLocationsResources>();
        Res.RegisterResource<OperationReasonResources>();
        Res.RegisterResource<ProtectionShieldResources>();
        Res.RegisterResource<ReplicationSyncConfigDescriptions>();
      }
      ObjectFactory.OnInitialized((object) null, new ExecutedEventArgs(nameof (RegisterStringResources), (object) null));
    }

    private static void RegisterDialogs()
    {
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (RegisterDialogs), (object) null);
      ObjectFactory.OnInitializing((object) null, args);
      if (!args.Cancel)
      {
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<NewMonolingualResourceDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<NewMultilingualResourceDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<EditMonolingualResourceDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<EditMultilingualResourceDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<MonolingualBatchEditorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<MultilingualBatchEditorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ImportLanguagePackDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<UserNewDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<UserEditDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ChangePasswordDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<AssignRolesDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<Telerik.Sitefinity.Web.UI.PropertyEditor>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<TemplatePropertiesDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<TemplatePagesDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ActionUsersSelection>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<UserPermissionsEditor>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<MasterPageSelector>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<EditCommentsDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ModulePermissionsDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<TwitterDetailDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<TwitterUrlShortConfigDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<PrincipalPermissionsDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ContentViewEditDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ContentViewEditDialogStandalone>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ContentViewInsertDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<UploadDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<EmbedDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<LibrarySelectorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<CustomSortingDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ChangePageOwnerDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ChangePageParentDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<VersionHistoryDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<PageTakeOwnershipDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<LockingDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ReorderDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<PageViewVersionDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<PageVersionHistoryDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<SettingsDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<MediaPlayerDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<TemplateSelectorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<LinkManagerDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<EditorContentManagerDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<MediaContentManagerDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<FormEntryEditDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ThumbnailMediaPlayerDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<WorkflowForm>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<SelectUsersAndRolesDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<WorkflowScopeSelectorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ControlTemplateEditor>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<InsertTagsDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<LanguageSelectorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<MetaTypeStructureEditorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<TaxonomyForm>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<FlatTaxonForm>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<FlatTaxaBulkEditForm>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<HierarchicalTaxonForm>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<NewHierarchicalTaxonSimpleDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ChangeParentDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<WorkflowSendForApprovalDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<WorkflowRejectDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<WorkflowScheduleDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ModuleEditorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<FieldWizardDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<CustomFieldPropertyEditor>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<DefaultFieldsEditorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ContentPagesDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<MediaQueryPagesAndTemplatesDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<EmailTemplateEditor>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<BlobStorageProviderSettingsDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ImageEditorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<LibraryRelocateDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<TextEditorToolSetDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<TextEditorUploadToolSetDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ViewContainerDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ControlTemplateVersionReview>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ShareLinkDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<SingleMediaContentItemDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<DisplayOverridenWidgets>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<WorkflowLanguageSelectorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<WorkflowContentScopeDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ScopeDefinitionDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<SetNotificationDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<WorkflowTypeSelectorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<ScopePageSelectorDialog>();
        Telerik.Sitefinity.Web.UI.Dialogs.RegisterDialog<CssSelectorDialog>();
      }
      ObjectFactory.OnInitialized((object) null, new ExecutedEventArgs(nameof (RegisterDialogs), (object) null));
    }

    private static void RegisterCommonTypes()
    {
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (RegisterCommonTypes), (object) null);
      ObjectFactory.OnInitializing((object) null, args);
      if (!args.Cancel)
      {
        ObjectFactory.container.RegisterType<IEmailSender, StandardDotNetEmailSender>("Standard", (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IUrlShortener, BitLyUrlShortener>("BitLy", (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IAuthorizationProvider, AuthorizationPermissionProvider>();
        ObjectFactory.container.RegisterType<IFieldFactory, FieldFactory>();
        ObjectFactory.container.RegisterType<LibraryRouteHandler>((LifetimeManager) new ContainerControlledLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<PageHelperImplementation, PageHelperImplementation>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<UrlLocalizationService, UrlLocalizationService>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IBackgroundTasksService, BackgroundTasksService>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<OutputCacheWorker.OutputCacheWorkerBackgroundService, OutputCacheWorker.OutputCacheWorkerBackgroundService>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IDataSourceRegistry, DataSourceRegistry>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IStatusProviderRegistry, StatusProviderRegistry>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IProviderNameResolver, DataProviderNameResolver>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IDataProviderDecorator, OpenAccessDecorator>(typeof (OpenAccessDecorator).FullName);
        ObjectFactory.container.RegisterType<IDataProviderDecorator, EmptyDecorator>(typeof (EmptyDecorator).FullName);
        ObjectFactory.container.RegisterType<IContentProviderDecorator, OpenAccessContentDecorator>(typeof (OpenAccessContentDecorator).FullName);
        ObjectFactory.container.RegisterType<IUrlProviderDecorator, OpenAccessUrlProviderDecorator>(typeof (OpenAccessUrlProviderDecorator).FullName);
        ObjectFactory.container.RegisterType<IUrlProviderDecorator, OpenAccessDynamicModuleUrlProviderDecorator>(typeof (OpenAccessDynamicModuleUrlProviderDecorator).FullName);
        ObjectFactory.container.RegisterType<IPermissionsFacade, PermissionsFacade>();
        ObjectFactory.container.RegisterType<IPermissionsFacade, EmptyPermissionsFacade>("Empty");
        ObjectFactory.container.RegisterType<IPermissionsForPrincipalFacade, PermissionsForPrincipalFacade>();
        ObjectFactory.container.RegisterType<IPermissionsForPrincipalFacade, EmptyPermissionsForPrincipalFacade>("Empty");
        ObjectFactory.container.RegisterType<IActionsFacade, GrantActionsFacade>("Grant");
        ObjectFactory.container.RegisterType<IActionsFacade, DenyActionsFacade>("Deny");
        ObjectFactory.container.RegisterType<IActionsFacade, EmptyActionFacade>("Empty");
        ObjectFactory.container.RegisterType<IAnyContentManager, AnyContentManager>((InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IAnyDraftFacade, AnyDraftFacade>((InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IAnyPublicFacade, AnyPublicFacade>((InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IAnyTempFacade, AnyTempFacade>((InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IAnyDraftPropertyEditorFacade, AnyDraftPropertyEditorFacade>((InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IAnyPublicPropertyEditorFacade, AnyPublicPropertyEditorFacade>((InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IAnyTempPropertyEditorFacade, AnyTempPropertyEditorFacade>((InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IDataItemExporter, CsvExporter>((InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IDataItemExporter, ExcelExporter>((InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IBrowseAndEditStrategy, PropertyEditorBrowseAndEditStrategy>();
        ObjectFactory.container.RegisterType<IUserDisplayNameBuilder, SitefinityUserDisplayNameBuilder>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<ICustomFieldBuilder, ContentLinksFieldBuilder>("Image", (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.RegisterSitemapNodeFilter<AdminSitemapNodeFilter>("Admin");
        ObjectFactory.RegisterSitemapNodeFilter<PageTemplatesSitemapNodeFilter>("PageTemplates");
        ObjectFactory.container.RegisterType<ILifecycleDecorator, LifecycleDecorator>((InjectionMember) new InjectionConstructor(new object[3]
        {
          (object) new InjectionParameter<ILifecycleManager>((ILifecycleManager) null),
          (object) new InjectionParameter<Action<Telerik.Sitefinity.GenericContent.Model.Content, Telerik.Sitefinity.GenericContent.Model.Content>>((Action<Telerik.Sitefinity.GenericContent.Model.Content, Telerik.Sitefinity.GenericContent.Model.Content>) null),
          (object) new InjectionParameter<Type[]>((Type[]) null)
        }));
        ObjectFactory.container.RegisterType<ILifecycleDecorator, LifecycleDecorator>("NonContent", (InjectionMember) new InjectionConstructor(new object[3]
        {
          (object) new InjectionParameter<ILifecycleManager>((ILifecycleManager) null),
          (object) new InjectionParameter<LifecycleItemCopyDelegate>((LifecycleItemCopyDelegate) null),
          (object) new InjectionParameter<Type[]>((Type[]) null)
        }));
        ObjectFactory.container.RegisterType<IFeedFormatter, FeedFormatter>();
        ObjectFactory.container.RegisterType<IPublishingXslTranslator, PublishingXslTranslator>((InjectionMember) new InjectionConstructor(new object[1]
        {
          (object) new ResolvedParameter<IFeedFormatter>()
        }));
        ObjectFactory.container.RegisterType<IStatisticsWebCounterService, StatisticsWebCounterService>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IEventService, EventService>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<INotificationSubscriptionSynchronizer, NotificationSubscriptionSynchronizer>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<ContentLocationRouteHandler>((LifetimeManager) new ContainerControlledLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        ObjectFactory.container.RegisterType<IDynamicContentBeforeCommitEventFactory, DynamicContentBeforeCommitEventFactory>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IModuleBuilderBeforeCommitEventFactory, ModuleBuilderBeforeCommitEventFactory>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IModuleBuilderAfterCommitEventFactory, ModuleBuilderAfterCommitEventFactory>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<ContentLocationService, ContentLocationService>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IGeoLocationService, GeoLocationService>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IImageProcessor, ImageProcessor>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IVideoThumbnailGenerator, VideoThumbnailGenerator>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IControlUtilities, DefaultControlUtilities>();
        ObjectFactory.container.RegisterType<IToolboxFactory, DefaultToolboxFactory>();
        ObjectFactory.container.RegisterType<IToolboxFilter, DefaultToolboxFilter>(typeof (DefaultToolboxFilter).FullName);
        ObjectFactory.container.RegisterType<IMessageReceiver, Receiver>();
        ObjectFactory.container.RegisterType<FormsSubmitHttpHandler>();
        ObjectFactory.container.RegisterType<IControlBehaviorResolver, DefaultBehaviorResolver>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IModuleBuilderProxy), typeof (ModuleBuilderProxy), (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType<IWorkflowDefinitionResolver, WorkflowDefinitionResolver>();
        ObjectFactory.container.RegisterType<IWorkflowItemScopeResolver, PageScopeResolver>(typeof (PageNode).FullName);
        ObjectFactory.container.RegisterType<IWorkflowNotifier, WorkflowNotifier>();
        ContainerControlledLifetimeManager controlledLifetimeManager = new ContainerControlledLifetimeManager();
        try
        {
          ObjectFactory.Container.RegisterType<IStructureTransfer, PagesStructureTransfer>(new PagesStructureTransfer().Area, (LifetimeManager) controlledLifetimeManager);
        }
        catch
        {
          controlledLifetimeManager.Dispose();
          throw;
        }
        ObjectFactory.Container.RegisterType<IStructureTransfer, UserProfilesStructureTransfer>(new UserProfilesStructureTransfer().Area, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.Container.RegisterType<IContentTransfer, PagesContentTransfer>(new PagesContentTransfer().Area, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.Container.RegisterType<IContentTransfer, PageTemplatesContentTransfer>(new PageTemplatesContentTransfer().Area, (LifetimeManager) new ContainerControlledLifetimeManager());
        TypeResolutionService.RegisterType(typeof (AuthType));
        ObjectFactory.container.RegisterType<IHtmlSanitizer, HtmlSanitizer>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.Container.RegisterType<IEtagProvider, EtagProvider>();
        ObjectFactory.Container.RegisterType<IResponseHandler, ResponseHandler>();
        ObjectFactory.Container.RegisterType<ITimeZoneInfoProvider, TimeZoneInfoProvider>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.Container.RegisterType<ITemporaryStorage, SitefinityTemporaryStorage>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.Container.RegisterType<ISystemStatusRetriever, LicensingSystemStatusRetriever>("LicensingSystemStatusRetriever", (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.Container.RegisterType(typeof (IPropertiesAdaptor), typeof (ContentBlockControllerAdaptor), typeof (ContentBlockControllerAdaptor).Name);
        ObjectFactory.Container.RegisterType(typeof (IVersionHistoryColumnProvider), typeof (MainVersionHistoryColumnProvider), typeof (MainVersionHistoryColumnProvider).Name);
        ObjectFactory.Container.RegisterType(typeof (IVersionHistoryColumnProvider), typeof (PageVersionHistoryColumnProvider), typeof (PageVersionHistoryColumnProvider).Name);
        ObjectFactory.container.RegisterType<IContentUsageService, ContentUsageService>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IContentUsageSource), typeof (ContentLinksUsageSource), typeof (ContentLinksUsageSource).Name);
        ObjectFactory.container.RegisterType(typeof (IContentUsageSource), typeof (ContentLocationUsageSource), typeof (ContentLocationUsageSource).Name);
        ObjectFactory.container.RegisterType(typeof (IContentUsageSource), typeof (BackendSearchUsageSource), typeof (BackendSearchUsageSource).Name);
      }
      ObjectFactory.OnInitialized((object) null, new ExecutedEventArgs(nameof (RegisterCommonTypes), (object) null));
    }

    private static void RegisterInlineEditing()
    {
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (RegisterInlineEditing), (object) null);
      ObjectFactory.OnInitializing((object) null, args);
      if (!args.Cancel)
      {
        ObjectFactory.container.RegisterType<IInlineEditingStrategyFactory, InlineEditingStrategyFactory>((LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IInlineEditingStrategy), typeof (PageControlStrategy), typeof (PageControlStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IInlineEditingStrategy), typeof (WorkflowItemStrategy), typeof (WorkflowItemStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IInlineEditingStrategy), typeof (GenericItemStrategy), typeof (GenericItemStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IInlineEditingResolver), typeof (ImageFieldResolver), typeof (ImageFieldResolver).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IInlineEditingResolver), typeof (YesNoFieldResolver), typeof (YesNoFieldResolver).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IInlineEditingResolver), typeof (ImageControlResolver), typeof (ImageControlResolver).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IInlineEditingResolver), typeof (FlatTaxonFieldResolver), typeof (FlatTaxonFieldResolver).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IInlineEditingResolver), typeof (HierarchicalTaxonFieldResolver), typeof (HierarchicalTaxonFieldResolver).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IInlineEditingResolver), typeof (ChoiceFieldResolver), typeof (ChoiceFieldResolver).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IInlineEditingResolver), typeof (DateTimeFieldResolver), typeof (DateTimeFieldResolver).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IPropertySetter), typeof (PropertySetterBase), typeof (PropertySetterBase).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IPropertySetter), typeof (ChoiceOptionSetter), typeof (ChoiceOptionSetter).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IPropertySetter), typeof (ContentLinkSetter), typeof (ContentLinkSetter).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IPropertySetter), typeof (LStringSetter), typeof (LStringSetter).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IPropertySetter), typeof (TrackedListSetter), typeof (TrackedListSetter).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IPropertySetter), typeof (DateTimeSetter), typeof (DateTimeSetter).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IPropertySetter), typeof (NullableDateTimeSetter), typeof (NullableDateTimeSetter).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IPropertySetter), typeof (StringArraySetter), typeof (StringArraySetter).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
      }
      ObjectFactory.OnInitialized((object) null, new ExecutedEventArgs(nameof (RegisterInlineEditing), (object) null));
    }

    private static void RegisterRelatedData()
    {
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (RegisterRelatedData), (object) null);
      ObjectFactory.OnInitializing((object) null, args);
      if (!args.Cancel)
      {
        ObjectFactory.container.RegisterType(typeof (IResponseBuilder), typeof (ImageResponseBuilder), typeof (ImageResponseBuilder).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IResponseBuilder), typeof (DocumentResponseBuilder), typeof (DocumentResponseBuilder).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IResponseBuilder), typeof (VideoResponseBuilder), typeof (VideoResponseBuilder).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IResponseBuilder), typeof (DynamicContentResponseBuilder), typeof (DynamicContentResponseBuilder).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IResponseBuilder), typeof (PagesResponseBuilder), typeof (PagesResponseBuilder).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IResponseBuilder), typeof (StaticContentResponseBuilder), typeof (StaticContentResponseBuilder).Name, (LifetimeManager) new ContainerControlledLifetimeManager());
        ObjectFactory.container.RegisterType(typeof (IRelatedDataResolver), typeof (RelatedDataResolver), (LifetimeManager) new ContainerControlledLifetimeManager());
      }
      ObjectFactory.OnInitialized((object) null, new ExecutedEventArgs(nameof (RegisterRelatedData), (object) null));
    }

    private static void RegisterRenderer()
    {
      ObjectFactory.container.RegisterType(typeof (IOperationProvider), typeof (LayoutOperationProvider), typeof (LayoutOperationProvider).Name);
      ObjectFactory.container.RegisterType(typeof (IOperationProvider), typeof (EditorOperationProvider), typeof (EditorOperationProvider).Name);
      ObjectFactory.container.RegisterType(typeof (IPageModelGenerator), typeof (WebFormsAndMvcGenerator), typeof (WebFormsAndMvcGenerator).Name);
      ObjectFactory.container.RegisterType(typeof (IPageModelGenerator), typeof (AgnosticGenerator), typeof (AgnosticGenerator).Name);
      ObjectFactory.container.RegisterType<CompositeGenerator>();
      ObjectFactory.container.RegisterType<IEditorAdaptor, PageEditorAdaptor>();
      ObjectFactory.container.RegisterType(typeof (IRendererIntegration), typeof (HostHeaderRendererIntegration), typeof (HostHeaderRendererIntegration).Name);
    }

    private static void RegisterRecycleBin()
    {
      ObjectFactory.container.RegisterType<IRecycleBinActionsAuthorizer, RecycleBinActionsAuthorizer>();
      ObjectFactory.container.RegisterType<IRecycleBinStrategy, RecycleBinLifecycleStrategy>();
      ObjectFactory.container.RegisterType<IRecyclableLifecycleDataItemModifier, RecyclableLifecycleDataItemModifier>((LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.container.RegisterType<IRecycleBinStrategy, RecycleBinPagesStrategy>(typeof (PageManager).FullName);
      ObjectFactory.container.RegisterType<IRecycleBinItemFactory, PageNodeRecycleBinFactory>(typeof (PageManager).FullName);
      ObjectFactory.container.RegisterType<IRecycleBinEventRegistry, RecycleBinEventRegistry>((LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.container.RegisterType<IRecycleBinStateResolver, RecycleBinStateResolver>((LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.container.RegisterType<IRecycleBinUrlValidator<PageNode>, PageRecycleBinUrlValidator<PageNode>>((LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.container.RegisterType<IRecycleBinUrlValidator<ILifecycleDataItem>, LifecycleRecycleBinUrlValidator<ILifecycleDataItem>>((LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.container.RegisterType<IRecycleBinUrlValidator<DynamicContent>, DynamicContentRecycleBinUrlValidator<DynamicContent>>((LifetimeManager) new ContainerControlledLifetimeManager());
    }

    private static void RegisterCompilation()
    {
      ObjectFactory.container.RegisterType<IPageKeyStrategy, BackendPageKeyStrategy>("Backend");
      ObjectFactory.container.RegisterType<IPageKeyStrategy, FrontendPageKeyStrategy>("Frontend");
      ObjectFactory.container.RegisterType<ITemplateKeyStrategy, DefaultTemplateKeyStrategy>("Default");
    }

    /// <summary>
    /// Registers the core basic settings - like time zone, language , text editor etc.
    /// Application modules should register their basic settings on Module Initialize
    /// </summary>
    private static void RegisterBasicSettings()
    {
      SystemManager.RegisterBasicSettings<GenericBasicSettingsView<TimeZoneBasicSettingsView, TimeZoneSettingsContract>>("General", "TimeZone", "PageResources", true);
      SystemManager.RegisterBasicSettings<LocalizationBasicSettingsView>("Languages", "Languages", "PageResources");
      SystemManager.RegisterBasicSettings<TextEditorBasicSettingsView>("TextEditor", "TextEditor", "PageResources");
      SystemManager.RegisterBasicSettings<TwitterBasicSettingsView>("Twitter", "Twitter", "PageResources");
      SystemManager.RegisterBasicSettings<GenericBasicSettingsView<UserAuthenticationBasicSettingsView, UserAuthenticationSettingsContract>>("UserAuthentication", "UserAuthentication", "PageResources");
      SystemManager.RegisterBasicSettings<GenericBasicSettingsView<GoogleMapsBasicSettingsView, GoogleMapsSettingsContract>>("GoogleMaps", "GoogleMaps", "PageResources");
      SystemManager.RegisterBasicSettings<CacheProfilesBasicSettingsView>("CacheProfiles", "CacheProfiles", "ConfigDescriptions");
      SystemManager.RegisterBasicSettings<GenericBasicSettingsView<RevisionHistoryBasicSettingsView, RevisionHistorySettingsContract>>("RevisionHistory", "RevisionHistory", "PageResources");
      SystemManager.RegisterBasicSettings<TrackingConsentBasicSettingsView>("Tracking", "TrackingConsent", "PageResources");
      SystemManager.RegisterBasicSettings<GenericBasicSettingsView<EmailSettingsBasicSettingsView, EmailSettingsContract>>("EmailSettings", "EmailSettings", "ConfigDescriptions");
      SystemManager.RegisterBasicSettings<SystemEmailsBasicSettingsView>("SystemEmailTemplates", "SystemEmailTemplates", "ConfigDescriptions", true);
    }
  }
}
