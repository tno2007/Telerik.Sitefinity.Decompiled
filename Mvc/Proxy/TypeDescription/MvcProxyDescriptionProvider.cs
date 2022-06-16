// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Mvc.Proxy.MvcProxyDescriptionProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Mvc.Proxy
{
  public class MvcProxyDescriptionProvider : TypeDescriptionProvider
  {
    public override ICustomTypeDescriptor GetTypeDescriptor(
      Type objectType,
      object instance)
    {
      Type type1 = typeof (MvcProxyBase);
      if (type1.IsAssignableFrom(objectType))
      {
        ParameterOverride parameterOverride = new ParameterOverride(nameof (objectType), (object) new InjectionParameter<Type>(objectType));
        return ObjectFactory.Container.Resolve<ICustomTypeDescriptor>(type1.FullName, (ResolverOverride) parameterOverride);
      }
      Type type2 = typeof (IControllerSettings);
      if (!type2.IsAssignableFrom(objectType))
        throw new NotSupportedException();
      ParameterOverrides parameterOverrides1 = new ParameterOverrides();
      parameterOverrides1.Add(nameof (objectType), (object) new InjectionParameter<Type>(objectType));
      parameterOverrides1.Add(nameof (instance), instance);
      ParameterOverrides parameterOverrides2 = parameterOverrides1;
      return ObjectFactory.Container.Resolve<ICustomTypeDescriptor>(type2.FullName, (ResolverOverride) parameterOverrides2);
    }
  }
}
