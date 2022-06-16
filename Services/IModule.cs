// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.IModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Defines the contract for Sitefinity modules.</summary>
  public interface IModule
  {
    /// <summary>Gets the module globally unique identifier.</summary>
    /// <value>The module ID.</value>
    Guid ModuleId { get; }

    /// <summary>Gets the programmatic name of the service.</summary>
    /// <value>The name.</value>
    string Name { get; }

    /// <summary>Gets the title displayed for this service.</summary>
    /// <value>The title.</value>
    string Title { get; }

    /// <summary>Gets the description of the service.</summary>
    /// <value>The description.</value>
    string Description { get; }

    /// <summary>
    /// Specifies the global resource class identifier to use for retrieving Title and Description values.
    /// If ClassId is specified the values of Title and Description properties are assumed to be resource keys.
    /// </summary>
    string ClassId { get; }

    /// <summary>Gets the identity of the home (landing) page.</summary>
    /// <value>The landing page id.</value>
    Guid LandingPageId { get; }

    /// <summary>Gets the startup type of the service.</summary>
    /// <value>The startup type.</value>
    StartupType Startup { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance is application module.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is application module; otherwise, <c>false</c>.
    /// </value>
    bool IsApplicationModule { get; }

    /// <summary>Initializes the module with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    void Initialize(ModuleSettings settings);

    /// <summary>Installs this module in Sitefinity system.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="installContext">The install context.</param>
    /// <param name="upgradeFrom">The version this module is upgrading from. If it is null, the module is installing for the first time.</param>
    void Install(SiteInitializer initializer, Version upgradeFrom);

    /// <summary>
    /// Loads the module dependencies after the module has been initialized and installed.
    /// </summary>
    void Load();

    /// <summary>
    /// This method is invoked during the unload process of an active module from Sitefinity, e.g. when a module is deactivated. For instance this method is also invoked for every active module during a restart of the application.
    /// Typically you will use this method to unsubscribe the module from all events to which it has subscription.
    /// </summary>
    void Unload();

    /// <summary>
    /// Uninstall the module from Sitefinity system. Deletes the module artifacts added with Install method.
    /// </summary>
    void Uninstall(SiteInitializer initializer);

    /// <summary>
    /// Gets the control panel for the specified request context.
    /// </summary>
    /// <returns></returns>
    IControlPanel GetControlPanel();

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    Type[] Managers { get; }
  }
}
