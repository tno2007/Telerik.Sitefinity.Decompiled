// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Represents base class for Sitefinity system services.
  /// </summary>
  public abstract class ServiceBase : IService
  {
    private bool initialized;
    private string name;
    private string title;
    private string description;
    private string classId;
    private StartupType startupType;

    /// <summary>Gets the programmatic name of the service.</summary>
    /// <value>The name.</value>
    public virtual string Name
    {
      get
      {
        this.VerifyInitialized();
        return this.name;
      }
    }

    /// <summary>Gets the title displayed for this service.</summary>
    /// <value>The title.</value>
    public virtual string Title
    {
      get
      {
        this.VerifyInitialized();
        return this.title;
      }
    }

    /// <summary>Gets the description of the service.</summary>
    /// <value>The description.</value>
    public virtual string Description
    {
      get
      {
        this.VerifyInitialized();
        return this.description;
      }
    }

    /// <summary>Gets the current status of the service.</summary>
    /// <value>The status.</value>
    public virtual ServiceStatus Status { get; protected set; }

    /// <summary>Gets the startup type of the service.</summary>
    /// <value>The startup type.</value>
    public virtual StartupType Startup
    {
      get
      {
        this.VerifyInitialized();
        return this.startupType;
      }
      set
      {
        if (this.startupType == value)
          return;
        this.startupType = value;
        ConfigManager manager = Config.GetManager();
        SystemConfig section = manager.GetSection<SystemConfig>();
        section.SystemServices[this.name].StartupType = value;
        manager.SaveSection((ConfigSection) section);
      }
    }

    /// <summary>
    /// Specifies the global resource class identifier to use for retrieving Title and Description values.
    /// If ClassId is specified the values of Title and Description properties are assumed to be resource keys.
    /// </summary>
    /// <value></value>
    public virtual string ClassId
    {
      get
      {
        this.VerifyInitialized();
        return this.classId;
      }
    }

    /// <summary>Loads and initializes this service.</summary>
    public virtual void Start() => this.Status = ServiceStatus.Started;

    /// <summary>
    /// Stops the service and releases any occupied resources.
    /// </summary>
    public virtual void Stop() => this.Status = ServiceStatus.Stopped;

    /// <summary>
    /// Stops the service without releasing resources or clearing caches.
    /// </summary>
    public virtual void Pause() => this.Status = ServiceStatus.Paused;

    /// <summary>Resumes the service if it has been paused.</summary>
    public virtual void Resume() => this.Status = ServiceStatus.Started;

    /// <summary>
    /// Releases all occupied resources, clears all caches and reinitializes the service.
    /// </summary>
    public virtual void Restart()
    {
      this.Stop();
      this.Start();
    }

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public virtual void Initialize(ModuleSettings settings)
    {
      this.name = settings != null ? settings.Name : throw new ArgumentNullException(nameof (settings));
      this.title = settings.Title;
      this.description = settings.Description;
      this.classId = settings.ResourceClassId;
      this.startupType = settings.StartupType;
      this.Status = ServiceStatus.Stopped;
      this.initialized = true;
    }

    private void VerifyInitialized()
    {
      if (!this.initialized)
        throw new InvalidOperationException("This instance is not initialized.");
    }

    /// <inheritdoc />
    public abstract Type[] Interfaces { get; }

    /// <summary>Gets the configuration element.</summary>
    /// <value>The settings.</value>
    protected ModuleSettings Settings => Config.Get<SystemConfig>().SystemServices[this.Name];
  }
}
