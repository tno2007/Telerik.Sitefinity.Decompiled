// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ChangeTracking.EntityTracker
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.ChangeTracking
{
  /// <summary>
  /// Allows to intercept changes to entitiy properties after registering their types for tracking.
  /// The types should implement ITrackableItem interface and all the properties that are to be tracked should be virtual
  /// </summary>
  public class EntityTracker
  {
    /// <summary>Tracks the type.</summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    public static void TrackType<TType>() where TType : class, ITrackableItem => EntityTracker.TrackType(typeof (TType));

    /// <summary>Registers the Type for tracking with the application.</summary>
    /// <param name="persistentType">Type of the persistent.</param>
    public static void TrackType(Type persistentType)
    {
      if (ObjectFactory.IsTypeRegistered(persistentType))
        return;
      ObjectFactory.Container.RegisterType(persistentType, (LifetimeManager) new TransientLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
      Interception interception = ObjectFactory.Container.Configure<Interception>();
      interception.SetInterceptorFor(persistentType, (ITypeInterceptor) new VirtualMethodInterceptor());
      interception.AddPolicy("Track changes").AddMatchingRule((IMatchingRule) EntityTracker.CreateDataMemberMatchingRule(persistentType)).AddCallHandler((ICallHandler) new TrackableItemChangedCallHandler());
    }

    private static MemberNameMatchingRule CreateDataMemberMatchingRule(
      Type modelType)
    {
      if (modelType == (Type) null)
        throw new ArgumentNullException(nameof (modelType));
      List<string> stringList = new List<string>();
      foreach (PropertyInfo property in modelType.GetProperties())
        stringList.Add("set_" + property.Name);
      return new MemberNameMatchingRule((IEnumerable<string>) stringList.ToArray(), true);
    }

    /// <summary>
    /// Resolves the specified persistent type and returns an instance of that type.
    /// </summary>
    /// <typeparam name="TPersistent">
    /// Type of the persistent object for which an instance ought to be resolved.
    /// </typeparam>
    /// <returns>An instance of a persistent object.</returns>
    public static TPersistent Create<TPersistent>() => (TPersistent) EntityTracker.Create(typeof (TPersistent));

    /// <summary>
    /// Resolves the specified persistent type and returns an instance of that type.
    /// </summary>
    /// <param name="persistentType">
    /// Type of the persistent object for which an instance ought to be resolved.
    /// </param>
    /// <returns>An instance of a persistent object.</returns>
    public static ITrackableItem Create(Type persistentType) => (ITrackableItem) ObjectFactory.Resolve(persistentType);
  }
}
