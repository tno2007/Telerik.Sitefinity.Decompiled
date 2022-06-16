// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LifecycleFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Lifecycle
{
  public static class LifecycleFactory
  {
    public const string NonContentLifecycleResolverName = "NonContent";

    /// <summary>Creates a lifecycle decorator</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="manager">The manager.</param>
    /// <param name="copyDelegate">The copy delegate.</param>
    public static ILifecycleDecorator CreateLifecycle<T>(
      ILifecycleManager manager,
      LifecycleItemCopyDelegate copyDelegate)
      where T : ILifecycleDataItemGeneric
    {
      return LifecycleFactory.CreateLifecycle(manager, copyDelegate, new Type[1]
      {
        typeof (T)
      });
    }

    /// <summary>Creates a lifecycle decorator</summary>
    /// <param name="manager">The manager.</param>
    /// <param name="copyDelegate">The copy delegate.</param>
    /// <param name="supportedTypes">The supported types.</param>
    public static ILifecycleDecorator CreateLifecycle(
      ILifecycleManager manager,
      LifecycleItemCopyDelegate copyDelegate,
      Type[] supportedTypes)
    {
      string fullName = manager.GetType().FullName;
      int num = ObjectFactory.IsTypeRegistered<ILifecycleDecorator>(fullName) ? 1 : 0;
      ParameterOverrides parameterOverrides1 = new ParameterOverrides();
      parameterOverrides1.Add(nameof (manager), (object) manager);
      parameterOverrides1.Add(nameof (copyDelegate), (object) copyDelegate);
      parameterOverrides1.Add("itemTypes", (object) supportedTypes);
      ParameterOverrides parameterOverrides2 = parameterOverrides1;
      return num != 0 ? ObjectFactory.Container.Resolve<ILifecycleDecorator>(fullName, (ResolverOverride) parameterOverrides2) : (!((IEnumerable<Type>) supportedTypes).Contains<Type>(typeof (Content)) ? ObjectFactory.Container.Resolve<ILifecycleDecorator>("NonContent", (ResolverOverride) parameterOverrides2) : ObjectFactory.Container.Resolve<ILifecycleDecorator>((ResolverOverride) parameterOverrides2));
    }

    /// <summary>Creates a lifecycle decorator</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="manager">The manager.</param>
    /// <param name="copyDelegate">The copy delegate.</param>
    /// <returns></returns>
    public static ILifecycleDecorator CreateLifecycle<T>(
      ILifecycleManager manager,
      Action<Content, Content> copyDelegate)
      where T : ILifecycleDataItemGeneric
    {
      return LifecycleFactory.CreateLifecycle(manager, copyDelegate, new Type[1]
      {
        typeof (T)
      });
    }

    /// <summary>Creates a lifecycle decorator</summary>
    /// <param name="manager">The manager.</param>
    /// <param name="copyDelegate">The copy delegate.</param>
    /// <param name="supportedTypes">The supported types.</param>
    /// <returns></returns>
    public static ILifecycleDecorator CreateLifecycle(
      ILifecycleManager manager,
      Action<Content, Content> copyDelegate,
      Type[] supportedTypes)
    {
      string fullName = manager.GetType().FullName;
      int num = ObjectFactory.IsTypeRegistered<ILifecycleDecorator>(fullName) ? 1 : 0;
      ParameterOverrides parameterOverrides1 = new ParameterOverrides();
      parameterOverrides1.Add(nameof (manager), (object) manager);
      parameterOverrides1.Add(nameof (copyDelegate), (object) copyDelegate);
      parameterOverrides1.Add("itemTypes", (object) supportedTypes);
      ParameterOverrides parameterOverrides2 = parameterOverrides1;
      return num != 0 ? ObjectFactory.Container.Resolve<ILifecycleDecorator>(fullName, (ResolverOverride) parameterOverrides2) : ObjectFactory.Container.Resolve<ILifecycleDecorator>((ResolverOverride) parameterOverrides2);
    }
  }
}
