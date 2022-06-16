// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.HealthCheckFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Health.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Health
{
  internal class HealthCheckFactory
  {
    private static IEnumerable<IHealthCheck> healthChecks;

    public static void Initialize() => HealthCheckFactory.healthChecks = HealthCheckFactory.InitHealthChecksFromConfiguration();

    public static IEnumerable<IHealthCheck> FilterByGroups(
      IEnumerable<string> groups)
    {
      return groups != null && groups.Any<string>() ? HealthCheckFactory.healthChecks.Where<IHealthCheck>((Func<IHealthCheck, bool>) (check => check.Groups != null && groups.Any<string>((Func<string, bool>) (groupCategory => check.Groups.Any<string>((Func<string, bool>) (checkCategory => checkCategory.Equals(groupCategory, StringComparison.OrdinalIgnoreCase))))))) : HealthCheckFactory.healthChecks;
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Need to catching all exceptions in this case.")]
    private static IEnumerable<IHealthCheck> InitHealthChecksFromConfiguration()
    {
      List<IHealthCheck> healthCheckList = new List<IHealthCheck>();
      SystemConfig systemConfig = Config.Get<SystemConfig>();
      if (!systemConfig.HealthCheckConfig.Enabled)
        return Enumerable.Empty<IHealthCheck>();
      List<ICheckConfigElement> list = systemConfig.HealthCheckConfig.HealthChecks.Values.Cast<ICheckConfigElement>().ToList<ICheckConfigElement>();
      HealthCheckFactory.FillConfigElementsByApplicationModuleElements(list);
      foreach (ICheckConfigElement checkElement in list)
      {
        try
        {
          if (checkElement.Enabled)
          {
            HealthCheckBase instance = HealthCheckFactory.CreateInstance(checkElement);
            healthCheckList.Add((IHealthCheck) instance);
          }
        }
        catch (Exception ex)
        {
          Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions);
        }
      }
      return (IEnumerable<IHealthCheck>) healthCheckList;
    }

    private static HealthCheckBase CreateInstance(ICheckConfigElement checkElement)
    {
      HealthCheckBase instanceFromType = HealthCheckFactory.GetInstanceFromType(checkElement.Type);
      instanceFromType.InitializeInternal(checkElement.Name, checkElement.Critical, checkElement.Groups, checkElement.TimeoutSeconds, checkElement.Parameters);
      return instanceFromType;
    }

    private static void FillConfigElementsByApplicationModuleElements(
      List<ICheckConfigElement> configElements)
    {
      foreach (ModuleBase module in SystemManager.ApplicationModules.Values.OfType<ModuleBase>())
      {
        foreach (ICheckConfigElement moduleConfigElement in HealthCheckFactory.GetModuleConfigElements(module))
        {
          ICheckConfigElement check = moduleConfigElement;
          if (!configElements.Contains(check) && configElements.FirstOrDefault<ICheckConfigElement>((Func<ICheckConfigElement, bool>) (p => p.Name.Equals(check.Name))) == null)
            configElements.Add(check);
        }
      }
    }

    private static List<ICheckConfigElement> GetModuleConfigElements(
      ModuleBase module)
    {
      List<ICheckConfigElement> source = new List<ICheckConfigElement>();
      foreach (IHealthCheckConfig healthCheckConfig in module.GetConfigsOfType<IHealthCheckConfig>())
      {
        if (healthCheckConfig != null && healthCheckConfig.HealthCheckConfigElements != null)
        {
          foreach (ICheckConfigElement checkConfigElement in healthCheckConfig.HealthCheckConfigElements)
          {
            ICheckConfigElement check = checkConfigElement;
            if (!source.Contains(check) && source.FirstOrDefault<ICheckConfigElement>((Func<ICheckConfigElement, bool>) (p => p.Name.Equals(check.Name))) == null)
              source.Add(check);
          }
        }
      }
      return source;
    }

    private static HealthCheckBase GetInstanceFromType(string type) => (HealthCheckBase) ObjectFactory.Resolve(TypeResolutionService.ResolveType(type, true));

    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Not needed")]
    public static IEnumerable<IHealthCheck> HealthChecks
    {
      get
      {
        if (HealthCheckFactory.healthChecks == null)
          HealthCheckFactory.healthChecks = Enumerable.Empty<IHealthCheck>();
        return HealthCheckFactory.healthChecks;
      }
      private set => HealthCheckFactory.healthChecks = value;
    }
  }
}
