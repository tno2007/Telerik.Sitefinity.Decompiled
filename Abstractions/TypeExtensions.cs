// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.TypeExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Extensions to the <see cref="T:System.Type" /> class.
  /// </summary>
  public static class TypeExtensions
  {
    /// <summary>
    /// Gets all the types that are assignable from <paramref name="type" />,
    /// which includes the type itself, all its base types and all implemented interfaces.
    /// </summary>
    /// <param name="type">The type being inspected.</param>
    /// <returns>An array of all assignable types (and interfaces).</returns>
    public static IEnumerable<Type> GetAllAssignableTypes(this Type type)
    {
      List<Type> typeList = new List<Type>();
      typeList.AddRange((IEnumerable<Type>) type.GetInterfaces());
      do
      {
        typeList.Add(type);
        type = type.BaseType;
      }
      while (type != (Type) null);
      return (IEnumerable<Type>) typeList.ToArray();
    }

    /// <summary>Gets type's generic interface implementation type.</summary>
    /// <param name="type">The type being inspected.</param>
    /// <param name="genericInterfaceTypeDefinition">A generic interface type definition, e.g. IDictionary&lt;,&gt;</param>
    /// <returns>An interface implementation type, if <paramref name="type" /> implements <paramref name="genericInterfaceTypeDefinition" />; <c>null</c>, otherwise.</returns>
    /// <example>
    /// <code>
    ///     var value = new string[] { ... };
    ///     ...
    ///     var implDef = value.GetType().GetGenericInterfaceImplementation(typeof(ICollection&lt;&gt;));
    ///     if (implDef != null)
    ///         return Activator.CreateInstance(typeof(List&lt;&gt;).MakeGenericType(implDef.GetGenericArguments()));
    /// </code>
    /// </example>
    public static Type GetGenericInterfaceImplementation(
      this Type type,
      Type genericInterfaceTypeDefinition)
    {
      if (type.IsInterface && type.IsGenericType && type.GetGenericTypeDefinition() == genericInterfaceTypeDefinition)
        return type;
      foreach (Type interfaceImplementation in type.GetInterfaces())
      {
        if (interfaceImplementation.IsGenericType && interfaceImplementation.GetGenericTypeDefinition() == genericInterfaceTypeDefinition)
          return interfaceImplementation;
      }
      return (Type) null;
    }

    /// <summary>
    /// Determines whether a type implements generic interface.
    /// </summary>
    /// <param name="type">The type being inspected.</param>
    /// <param name="genericInterfaceTypeDefinition">A generic interface type definition, e.g. IDictionary&lt;,&gt;</param>
    /// <returns><c>true</c>, if <paramref name="type" /> implements <paramref name="genericInterfaceTypeDefinition" />; <c>false</c>, otherwise.</returns>
    /// <example>
    /// <code>
    ///     if (value.GetType().ImplementsGenericInterface(typeof(IDictionary&lt;,&gt;)))
    ///         SerializeGenericDictionary(value);
    /// </code>
    /// </example>
    public static bool ImplementsGenericInterface(
      this Type type,
      Type genericInterfaceTypeDefinition)
    {
      return (Type) null != type.GetGenericInterfaceImplementation(genericInterfaceTypeDefinition);
    }

    internal static bool TryGetEnumerableType(this Type type, out Type elementType)
    {
      elementType = (Type) null;
      Type type1 = !type.IsGenericType || !object.Equals((object) typeof (IEnumerable<>), (object) type.GetGenericTypeDefinition()) ? ((IEnumerable<Type>) type.GetInterfaces()).FirstOrDefault<Type>((Func<Type, bool>) (i => i.IsGenericType && object.Equals((object) typeof (IEnumerable<>), (object) i.GetGenericTypeDefinition()))) : type;
      if (type1 != (Type) null)
        elementType = type1.GetGenericArguments()[0];
      return elementType != (Type) null;
    }

    public static bool HasEnumerableType(this Type type, Type toCompare)
    {
      Type elementType = (Type) null;
      return type.TryGetEnumerableType(out elementType) && toCompare.IsAssignableFrom(elementType);
    }

    /// <summary>
    /// Gets all the types in the current application domain, which can be assigned to <paramref name="type" />.
    /// </summary>
    /// <remarks>Potentially slow.</remarks>
    /// <param name="type">The type to be inspected.</param>
    /// <returns>A collection of all the types in the current application domain, which can be assigned to <paramref name="type" />.</returns>
    public static IEnumerable<Type> GetAssignableTypes(this Type type)
    {
      List<Type> assignableTypes = new List<Type>();
      TypeExtensions.FillAssignableTypes(type, type.Assembly, assignableTypes);
      foreach (Assembly dependentAssembly in TypeExtensions.GetDependentAssemblies(type.Assembly))
        TypeExtensions.FillAssignableTypes(type, dependentAssembly, assignableTypes);
      return (IEnumerable<Type>) assignableTypes;
    }

    private static void FillAssignableTypes(
      Type type,
      Assembly assembly,
      List<Type> assignableTypes)
    {
      try
      {
        foreach (Type type1 in assembly.GetTypes())
        {
          if (type.IsAssignableFrom(type1))
            assignableTypes.Add(type1);
        }
      }
      catch
      {
      }
    }

    private static IEnumerable<Assembly> GetDependentAssemblies(
      Assembly analyzedAssembly)
    {
      return ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).Where<Assembly>((Func<Assembly, bool>) (a => TypeExtensions.GetNamesOfAssembliesReferencedBy(a).Contains<string>(analyzedAssembly.FullName)));
    }

    public static IEnumerable<string> GetNamesOfAssembliesReferencedBy(Assembly assembly) => ((IEnumerable<AssemblyName>) assembly.GetReferencedAssemblies()).Select<AssemblyName, string>((Func<AssemblyName, string>) (assemblyName => assemblyName.FullName));

    /// <summary>
    /// Verifies if all the instaces are assignable to the specified type
    /// </summary>
    public static bool InstancesAreAssignableToType(
      this Type assignmentTargetType,
      params object[] instances)
    {
      foreach (object instance in instances)
      {
        if (!assignmentTargetType.IsInstanceOfType(instance))
          return false;
      }
      return true;
    }

    /// <summary>
    /// Returns true if the type is collection of items thata are either value types or strings
    /// 
    /// </summary>
    public static bool IsCollectionOfSimpleTypes(this Type type)
    {
      if (!type.ImplementsInterface(typeof (ICollection<>)))
        return false;
      Type[] genericArguments = type.GetGenericArguments();
      if (genericArguments.Length != 1)
        return false;
      Type type1 = genericArguments[0];
      return type1.IsValueType && type1.IsPrimitive || type1 == typeof (string) || type1 == typeof (Guid) || type1 == typeof (DateTime);
    }

    public static bool IsNullableType(this Type type) => Nullable.GetUnderlyingType(type) != (Type) null;

    [Obsolete("Use the standard Nullable.GetUnderlyingType method, provided by .NET, instead.")]
    public static Type GetNonNullableType(this Type type) => !type.IsNullableType() ? type : type.GetGenericArguments()[0];

    public static string GetResolvableTypeName(this Type type) => type.Assembly.GetName().Name.StartsWith("artificial") ? type.FullName : type.AssemblyQualifiedName;

    internal static string GetMainFieldName(this Type type)
    {
      string mainFieldName = (string) null;
      if (typeof (DynamicContent).IsAssignableFrom(type))
        mainFieldName = ModuleBuilderManager.GetTypeMainProperty(type)?.Name;
      else if (typeof (IContent).IsAssignableFrom(type))
        mainFieldName = TypeDescriptor.GetProperties(type)[LinqHelper.MemberName<IContent>((Expression<Func<IContent, object>>) (x => x.Title))].Name;
      else if (((IEnumerable<Attribute>) Attribute.GetCustomAttributes((MemberInfo) type, typeof (RequiredLocalizablePropertyAttribute))).Count<Attribute>() != 0)
      {
        Attribute[] customAttributes = Attribute.GetCustomAttributes((MemberInfo) type, typeof (RequiredLocalizablePropertyAttribute));
        int index = 0;
        if (index < customAttributes.Length)
          mainFieldName = ((RequiredLocalizablePropertyAttribute) customAttributes[index]).RequiredPropertyName;
      }
      else
      {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
        for (int index = 0; index < properties.Count; ++index)
        {
          if (properties[index].PropertyType == typeof (string))
          {
            mainFieldName = properties[index].Name;
            break;
          }
        }
      }
      return mainFieldName;
    }

    internal static bool Inherits<TParent>(this Type type) => typeof (TParent).IsAssignableFrom(type);
  }
}
