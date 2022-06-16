// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.IService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Defines the contract for Sitefinity system services.</summary>
  public interface IService
  {
    /// <summary>Gets the programmatic name of the service.</summary>
    /// <value>The name.</value>
    string Name { get; }

    /// <summary>Gets the title displayed for this service.</summary>
    /// <value>The title.</value>
    string Title { get; }

    /// <summary>Gets the description of the service.</summary>
    /// <value>The description.</value>
    string Description { get; }

    /// <summary>Gets the current status of the service.</summary>
    /// <value>The status.</value>
    ServiceStatus Status { get; }

    /// <summary>Gets the startup type of the service.</summary>
    /// <value>The startup type.</value>
    StartupType Startup { get; set; }

    /// <summary>
    /// Specifies the global resource class identifier to use for retrieving Title and Description values.
    /// If ClassId is specified the values of Title and Description properties are assumed to be resource keys.
    /// </summary>
    string ClassId { get; }

    /// <summary>Loads and initializes this service.</summary>
    void Start();

    /// <summary>
    /// Stops the service and releases any occupied resources.
    /// </summary>
    void Stop();

    /// <summary>
    /// Stops the service without releasing resources or clearing caches.
    /// </summary>
    void Pause();

    /// <summary>Resumes the service if it has been paused.</summary>
    void Resume();

    /// <summary>
    /// Releases all occupied resources, clears all caches and reinitializes the service.
    /// </summary>
    void Restart();

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    void Initialize(ModuleSettings settings);

    /// <summary>
    /// Gets the types of the service used to register and resolve the service from the Service Bus.
    /// </summary>
    /// <value>The type of the service.</value>
    Type[] Interfaces { get; }
  }
}
