// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InactiveModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Represents information about inactive module.</summary>
  public sealed class InactiveModule : IModule
  {
    private volatile bool started;
    private ModuleSettings settings;
    private IModule activeModule;
    private readonly object syncLock = new object();

    /// <summary>Starts this instance.</summary>
    public void Start() => this.Start((SiteInitializer) null);

    /// <summary>Starts the specified initializer.</summary>
    /// <param name="initializer">The initializer. Can be NULL.</param>
    public void Start(SiteInitializer initializer)
    {
      if (this.started)
        return;
      lock (this.syncLock)
      {
        if (this.started)
          return;
        if (this.settings == null)
          throw new InvalidOperationException("This instance is not initialized.");
        if (this.Startup == StartupType.Disabled)
          throw new InvalidOperationException(Res.Get<ErrorMessages>().DisabledServiceCannotStart.Arrange((object) this.Name));
        this.activeModule = SystemManager.InitializeModule(this.settings, initializer?.Context, (SystemManager.ModuleVersionInfo) null, new bool?(true));
        this.activeModule.Load();
        this.started = true;
      }
    }

    /// <summary>Gets the module globally unique identifier.</summary>
    /// <value>The module ID.</value>
    public Guid ModuleId => this.activeModule != null ? this.activeModule.ModuleId : this.settings.ModuleId;

    /// <summary>Gets the programmatic name of the service.</summary>
    /// <value>The name.</value>
    public string Name => this.activeModule != null ? this.activeModule.Name : this.settings.Name;

    /// <summary>Gets the title displayed for this service.</summary>
    /// <value>The title.</value>
    public string Title
    {
      get
      {
        if (this.activeModule != null)
          return this.activeModule.Title;
        string title = this.settings.Title;
        if (!string.IsNullOrEmpty(this.ClassId))
        {
          this.Start();
          title = Res.Get(this.ClassId, title);
        }
        return title;
      }
    }

    /// <summary>Gets the description of the service.</summary>
    /// <value>The description.</value>
    public string Description
    {
      get
      {
        if (this.activeModule != null)
          return this.activeModule.Description;
        string description = this.settings.Description;
        if (!string.IsNullOrEmpty(this.ClassId))
        {
          this.Start();
          description = Res.Get(this.ClassId, description);
        }
        return description;
      }
    }

    /// <summary>
    /// Specifies the global resource class identifier to use for retrieving Title and Description values.
    /// If ClassId is specified the values of Title and Description properties are assumed to be resource keys.
    /// </summary>
    /// <value></value>
    public string ClassId => this.activeModule != null ? this.activeModule.ClassId : this.settings.ResourceClassId;

    /// <summary>Gets the startup type of the service.</summary>
    /// <value>The startup type.</value>
    public StartupType Startup
    {
      get => this.activeModule != null ? this.activeModule.Startup : this.settings.StartupType;
      set
      {
        this.Start();
        this.activeModule.Startup = value;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is application module.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is application module; otherwise, <c>false</c>.
    /// </value>
    public bool IsApplicationModule => this.settings is AppModuleSettings;

    /// <summary>
    /// Gets the control panel for the specified request context.
    /// </summary>
    public IControlPanel GetControlPanel()
    {
      this.Start((SiteInitializer) null);
      return this.activeModule.GetControlPanel();
    }

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public void Initialize(ModuleSettings settings) => this.settings = settings;

    /// <summary>Installs this module in Sitefinity system.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="upgradeFrom">The version this module is upgrading from</param>
    public void Install(SiteInitializer initializer, Version upgradeFrom) => this.Start(initializer);

    /// <summary>Gets the security roots for this module.</summary>
    /// <returns></returns>
    public IList<SecurityRoot> GetSecurityRoots()
    {
      this.Start();
      return this.activeModule is ISecuredModule ? ((ISecuredModule) this.activeModule).GetSecurityRoots() : (IList<SecurityRoot>) new List<SecurityRoot>();
    }

    /// <summary>
    /// Gets the identity of the home (landing) page for the module.
    /// </summary>
    /// <value>The landing page id.</value>
    public Guid LandingPageId => Guid.Empty;

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public Type[] Managers => new Type[0];

    /// <inheritdoc />
    public void Load()
    {
    }

    /// <inheritdoc />
    public void Unload()
    {
    }

    /// <inheritdoc />
    public void Uninstall(SiteInitializer initializer)
    {
    }
  }
}
