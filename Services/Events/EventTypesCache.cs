// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Events.EventTypesCache
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Services.Events
{
  internal class EventTypesCache
  {
    private static readonly ConcurrentDictionary<Type, IEnumerable<Type>> Types = new ConcurrentDictionary<Type, IEnumerable<Type>>();
    private static readonly ConcurrentDictionary<Type, IEnumerable<Type>> EventTypes = new ConcurrentDictionary<Type, IEnumerable<Type>>();

    public static IEnumerable<Type> GetAllAssignableEventTypes(Type type)
    {
      IEnumerable<Type> assignableEventTypes;
      if (!EventTypesCache.EventTypes.TryGetValue(type, out assignableEventTypes))
      {
        assignableEventTypes = type.GetAllAssignableTypes().Where<Type>((Func<Type, bool>) (t => typeof (IEvent).IsAssignableFrom(t)));
        EventTypesCache.EventTypes.TryAdd(type, assignableEventTypes);
      }
      return assignableEventTypes;
    }

    public static IEnumerable<Type> GetAllAssignableTypes(Type type)
    {
      IEnumerable<Type> allAssignableTypes;
      if (!EventTypesCache.Types.TryGetValue(type, out allAssignableTypes))
      {
        allAssignableTypes = type.GetAllAssignableTypes();
        EventTypesCache.Types.TryAdd(type, allAssignableTypes);
      }
      return allAssignableTypes;
    }
  }
}
