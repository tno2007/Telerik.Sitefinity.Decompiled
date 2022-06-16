// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ManagerSettingsRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.OA;

namespace Telerik.Sitefinity.Data
{
  internal class ManagerSettingsRegion : IDisposable
  {
    private readonly IManager manager;
    private bool? suppressSecurityChecksPrevValue;
    private bool? suppressEventsPrevValue;
    private bool? suppressNotificationsPrevValue;
    private bool? filterQueriesByViewPermissionsPrevValue;
    private SitefinityOAContext context;
    private bool? supressAutoCalculationPrevValue;

    public ManagerSettingsRegion(IManager manager) => this.manager = manager != null ? manager : throw new ArgumentNullException(nameof (manager));

    public ManagerSettingsRegion SuppressSecurityChecks(
      bool suppressSecurityChecks = true)
    {
      if (this.manager.Provider.SuppressSecurityChecks == suppressSecurityChecks)
        return this;
      this.suppressSecurityChecksPrevValue = new bool?(this.manager.Provider.SuppressSecurityChecks);
      this.manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      return this;
    }

    public ManagerSettingsRegion SuppressEvents(bool suppressEvents = true)
    {
      if (this.manager.Provider.SuppressEvents == suppressEvents)
        return this;
      this.suppressEventsPrevValue = new bool?(this.manager.Provider.SuppressEvents);
      this.manager.Provider.SuppressEvents = suppressEvents;
      return this;
    }

    public ManagerSettingsRegion SuppressNotifications(
      bool suppressNotifications = true)
    {
      if (this.manager.Provider.SuppressNotifications == suppressNotifications)
        return this;
      this.suppressNotificationsPrevValue = new bool?(this.manager.Provider.SuppressNotifications);
      this.manager.Provider.SuppressNotifications = suppressNotifications;
      return this;
    }

    public ManagerSettingsRegion SuppressFilterQueriesByViewPermissions(
      bool suppressFilterQueries = true)
    {
      bool flag = !suppressFilterQueries;
      if (this.manager.Provider.FilterQueriesByViewPermissions == flag)
        return this;
      this.filterQueriesByViewPermissionsPrevValue = new bool?(this.manager.Provider.FilterQueriesByViewPermissions);
      this.manager.Provider.FilterQueriesByViewPermissions = flag;
      return this;
    }

    public ManagerSettingsRegion SuppressOpenAccessFieldsAutoCalculation(
      bool suppressAutoCalculation = true)
    {
      DataProviderBase provider1 = this.manager.Provider;
      if (provider1 != null && provider1 is IOpenAccessDataProvider provider2)
      {
        this.context = provider2.GetContext();
        if (this.context != null)
        {
          this.supressAutoCalculationPrevValue = new bool?(this.context.ContextOptions.EnableDataSynchronization);
          this.context.ContextOptions.EnableDataSynchronization = suppressAutoCalculation;
        }
      }
      return this;
    }

    void IDisposable.Dispose()
    {
      if (this.manager != null && this.manager.Provider != null)
      {
        if (this.suppressSecurityChecksPrevValue.HasValue)
          this.manager.Provider.SuppressSecurityChecks = this.suppressSecurityChecksPrevValue.GetValueOrDefault();
        if (this.suppressEventsPrevValue.HasValue)
          this.manager.Provider.SuppressEvents = this.suppressEventsPrevValue.GetValueOrDefault();
        if (this.suppressNotificationsPrevValue.HasValue)
          this.manager.Provider.SuppressNotifications = this.suppressNotificationsPrevValue.GetValueOrDefault();
        if (this.filterQueriesByViewPermissionsPrevValue.HasValue)
          this.manager.Provider.FilterQueriesByViewPermissions = this.filterQueriesByViewPermissionsPrevValue.GetValueOrDefault();
      }
      if (this.context == null)
        return;
      if (this.supressAutoCalculationPrevValue.HasValue)
        this.context.ContextOptions.EnableDataSynchronization = this.supressAutoCalculationPrevValue.GetValueOrDefault();
      this.context = (SitefinityOAContext) null;
    }
  }
}
