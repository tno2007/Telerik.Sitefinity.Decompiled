// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.Newtonsoft.Json.PropertyIgnoringContractResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Sitefinity.Utilities.Newtonsoft.Json
{
  /// <summary>
  /// Json.NET (Newtonsoft.Json) contract resolver, which allows to ignore properties during serialization.
  /// Based on http://stackoverflow.com/a/14510134 and http://stackoverflow.com/a/16647343
  /// </summary>
  /// <example>
  /// <code>
  /// var ignorePropsResolver = new PropertyIgnoringContractResolver()
  ///     .Ignore&lt;Employee&gt;(p =&gt; p.Salary)
  ///     .Ignore&lt;Employee&gt;(p =&gt; p.Confidential);
  /// var settings = new JsonSerializerSettings { ContractResolver = ignorePropsResolver, ... };
  /// string json = JsonConvert.SerializeObject(obj, settings);
  /// </code>
  /// </example>
  internal class PropertyIgnoringContractResolver : DefaultContractResolver
  {
    protected readonly Dictionary<Type, HashSet<string>> ignores = new Dictionary<Type, HashSet<string>>();

    /// <summary>
    /// Explicitly ignore the given property(s) for the given type
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="propertyName">one or more properties to ignore. Leave empty to ignore the type entirely.</param>
    public void Ignore(Type type, params string[] propertyName)
    {
      if (!this.ignores.ContainsKey(type))
        this.ignores[type] = new HashSet<string>();
      foreach (string str in propertyName)
        this.ignores[type].Add(str);
    }

    /// <summary>
    /// Strongly typed and fluent (meaning you can chain them) way to ignore properties.
    /// </summary>
    /// <typeparam name="T">The type which property should be ignored.</typeparam>
    /// <param name="selectors">Property selector expressions, e.g. <code>p =&gt; p.Property</code>.</param>
    public PropertyIgnoringContractResolver Ignore<T>(
      params Expression<Func<T, object>>[] selectors)
    {
      foreach (Expression<Func<T, object>> selector in selectors)
      {
        if (!(selector.Body is MemberExpression memberExpression2) && !(((UnaryExpression) selector.Body).Operand is MemberExpression memberExpression2))
          throw new ArgumentException("Could not get property name from the given expression", "selector");
        this.Ignore(typeof (T), memberExpression2.Member.Name);
      }
      return this;
    }

    /// <summary>Is the given property for the given type ignored?</summary>
    public bool IsIgnored(Type type, string propertyName)
    {
      if (!this.ignores.ContainsKey(type))
        return false;
      return this.ignores[type].Count == 0 || this.ignores[type].Contains(propertyName);
    }

    /// <summary>The decision logic goes here.</summary>
    protected override JsonProperty CreateProperty(
      MemberInfo member,
      MemberSerialization memberSerialization)
    {
      JsonProperty property = base.CreateProperty(member, memberSerialization);
      if (this.IsIgnored(property.DeclaringType, property.PropertyName) || this.IsIgnored(property.DeclaringType.BaseType, property.PropertyName))
        property.ShouldSerialize = (Predicate<object>) (instance => false);
      return property;
    }
  }
}
