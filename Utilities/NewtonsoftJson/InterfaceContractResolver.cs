// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.Newtonsoft.Json.InterfaceContractResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Utilities.Newtonsoft.Json
{
  internal class InterfaceContractResolver : DefaultContractResolver
  {
    protected override IList<JsonProperty> CreateProperties(
      Type type,
      MemberSerialization memberSerialization)
    {
      IList<JsonProperty> properties;
      if (type.IsPublic)
      {
        properties = base.CreateProperties(type, memberSerialization);
      }
      else
      {
        IEnumerable<Type> types1 = EventTypesCache.GetAllAssignableTypes(type).Where<Type>((Func<Type, bool>) (x => x.IsPublic));
        IEnumerable<Type> types2 = types1.Except<Type>(types1.SelectMany<Type, Type>((Func<Type, IEnumerable<Type>>) (t => EventTypesCache.GetAllAssignableTypes(t).Where<Type>((Func<Type, bool>) (baseType => baseType.IsPublic && baseType.FullName != t.FullName)))));
        IEnumerable<JsonProperty> jsonProperties = (IEnumerable<JsonProperty>) new List<JsonProperty>();
        foreach (Type type1 in types2)
          jsonProperties = jsonProperties.Union<JsonProperty>((IEnumerable<JsonProperty>) base.CreateProperties(type1, memberSerialization));
        properties = (IList<JsonProperty>) jsonProperties.Distinct<JsonProperty>((IEqualityComparer<JsonProperty>) new InterfaceContractResolver.JsonPropertyEqualityComparer()).ToList<JsonProperty>();
      }
      return properties;
    }

    internal class JsonPropertyEqualityComparer : IEqualityComparer<JsonProperty>
    {
      public bool Equals(JsonProperty firstProperty, JsonProperty secondProperty) => firstProperty.PropertyName.Equals(secondProperty.PropertyName);

      public int GetHashCode(JsonProperty property) => property.PropertyName.GetHashCode();
    }
  }
}
