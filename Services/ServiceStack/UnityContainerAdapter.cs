// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceStack.UnityContainerAdapter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Configuration;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Services.ServiceStack
{
  /// <summary>IContainerAdapter for the unity framework.</summary>
  public class UnityContainerAdapter : IContainerAdapter, IResolver
  {
    public T Resolve<T>() => ObjectFactory.Resolve<T>();

    public T TryResolve<T>() => ObjectFactory.IsTypeRegistered<T>() ? ObjectFactory.Resolve<T>() : default (T);
  }
}
