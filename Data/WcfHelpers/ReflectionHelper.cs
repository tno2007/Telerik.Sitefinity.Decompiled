// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.ReflectionHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>Helper class for reflection operations.</summary>
  public static class ReflectionHelper
  {
    private static readonly HashSet<Type> SimpleBuiltInTypes;
    private static readonly HashSet<Type> ExtendedSimpleTypes;
    private static readonly HashSet<Type> SimpleTypes;
    private static readonly BindingFlags PublicFieldBindingFlags;
    private static object syncRoot = new object();

    /// <summary>
    /// Initializes static members of the <see cref="T:Telerik.Sitefinity.Data.WcfHelpers.ReflectionHelper" /> class
    /// </summary>
    static ReflectionHelper()
    {
      ReflectionHelper.SimpleBuiltInTypes = new HashSet<Type>()
      {
        typeof (bool),
        typeof (byte),
        typeof (sbyte),
        typeof (char),
        typeof (Decimal),
        typeof (double),
        typeof (float),
        typeof (int),
        typeof (uint),
        typeof (long),
        typeof (ulong),
        typeof (short),
        typeof (ushort)
      };
      ReflectionHelper.ExtendedSimpleTypes = new HashSet<Type>()
      {
        typeof (string),
        typeof (DateTime),
        typeof (TimeSpan),
        typeof (DateTimeOffset),
        typeof (Guid),
        typeof (Uri),
        typeof (Enum),
        typeof (byte[])
      };
      HashSet<Type> other = new HashSet<Type>();
      Type type1 = typeof (Nullable<>);
      foreach (Type simpleBuiltInType in ReflectionHelper.SimpleBuiltInTypes)
      {
        if (simpleBuiltInType.IsValueType)
        {
          Type type2 = type1.MakeGenericType(simpleBuiltInType);
          other.Add(type2);
        }
      }
      foreach (Type extendedSimpleType in ReflectionHelper.ExtendedSimpleTypes)
      {
        if (extendedSimpleType.IsValueType)
        {
          Type type3 = type1.MakeGenericType(extendedSimpleType);
          other.Add(type3);
        }
      }
      ReflectionHelper.SimpleTypes = new HashSet<Type>();
      ReflectionHelper.SimpleTypes.UnionWith((IEnumerable<Type>) ReflectionHelper.SimpleBuiltInTypes);
      ReflectionHelper.SimpleTypes.UnionWith((IEnumerable<Type>) ReflectionHelper.ExtendedSimpleTypes);
      ReflectionHelper.SimpleTypes.UnionWith((IEnumerable<Type>) other);
      ReflectionHelper.PublicFieldBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
    }

    /// <summary>Performs a deep copy of an object graph</summary>
    /// <typeparam name="T">Type of the root object in the object graph</typeparam>
    /// <param name="source">The source object graph</param>
    /// <param name="target">The target.</param>
    /// <returns>The deep copied object.</returns>
    public static T DeepCopy<T>(object source, object target)
    {
      ReflectionHelper.CopyProperties(source, ref target, (CreateInstanceDelegate) null, (CanCopyPropertyDelegate) (p => true), string.Empty);
      ReflectionHelper.CopyPublicFields(source, ref target, (CreateInstanceDelegate) null, (CanCopyPropertyDelegate) (p => true), string.Empty);
      return (T) target;
    }

    /// <summary>Performs a deep copy of an object graph.</summary>
    /// <param name="source">Object graph</param>
    /// <param name="type">Type of the root object in the object graph</param>
    /// <param name="createDelegate">Optional delegate to create instances</param>
    /// <param name="canCopyPropertyDelegate">Optional delegate that determines when a property can be copied</param>
    /// <param name="propertyPath">Property path in the original object</param>
    /// <returns>Deep copy of <paramref name="source" /></returns>
    public static object DeepCopy(
      object source,
      Type type,
      CreateInstanceDelegate createDelegate,
      CanCopyPropertyDelegate canCopyPropertyDelegate,
      string propertyPath)
    {
      if (ReflectionHelper.IsSimpleType(type) || ReflectionHelper.IsValueType(type))
        return source;
      if (source == null)
        return (object) null;
      if (createDelegate == null)
        createDelegate = new CreateInstanceDelegate(ReflectionHelper.CreateInstance);
      if (canCopyPropertyDelegate == null)
        canCopyPropertyDelegate = new CanCopyPropertyDelegate(ReflectionHelper.CanCopyProperty);
      object destination = createDelegate(type, propertyPath, source);
      if (destination != null)
      {
        ReflectionHelper.CopyProperties(source, ref destination, createDelegate, canCopyPropertyDelegate, propertyPath);
        if (destination is DynamicContent)
          ((DynamicContent) destination).UnresolveLinks();
        ReflectionHelper.CopyPublicFields(source, ref destination, createDelegate, canCopyPropertyDelegate, propertyPath);
      }
      return destination;
    }

    /// <summary>
    /// Shallow copies all fields, public and private, of the source object to the target object.
    /// Source and target objects must be the same type. Shallow copy means that if the field is
    /// reference type, only the reference is copied.
    /// </summary>
    /// <param name="target">The target object.</param>
    /// <param name="source">The source object.</param>
    public static void ShallowCopy(object target, object source) => ReflectionHelper.ShallowCopy(target, source, false);

    /// <summary>
    /// Shallow copies all fields or public properties of the source object to the target object.
    /// Source and target objects must be the same type. Shallow copy means that if the field or property is
    /// reference type, only the reference is copied.
    /// </summary>
    /// <param name="target">The target object.</param>
    /// <param name="source">The source object.</param>
    /// <param name="publicProperties">Specifies whether only public properties will be copied or private and public fields.</param>
    public static void ShallowCopy(object target, object source, bool publicProperties)
    {
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      Type type1 = source != null ? source.GetType() : throw new ArgumentNullException(nameof (source));
      if (!type1.IsAssignableFrom(target.GetType()))
        throw new InvalidOperationException("Messages.TargetNotAssignable");
      if (publicProperties)
      {
        foreach (PropertyInfo property in type1.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
          if (property.CanWrite)
          {
            string name = property.Name;
            object obj = property.GetValue(source, (object[]) null);
            type1.GetProperty(name, BindingFlags.Instance | BindingFlags.Public).SetValue(target, obj, (object[]) null);
          }
        }
      }
      else
      {
        for (Type type2 = type1; type2 != typeof (object); type2 = type2.BaseType)
        {
          foreach (FieldInfo field in type2.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
          {
            string name = field.Name;
            object obj = field.GetValue(source);
            type2.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).SetValue(target, obj);
          }
        }
      }
    }

    /// <summary>
    /// Converts string representation of a value to its actual type based on property descriptor.
    /// </summary>
    /// <param name="descriptor">The property descriptor used to convert the value.</param>
    /// <param name="value">String representation of a value.</param>
    /// <returns>Converted value.</returns>
    public static object GetPropertyValueFromString(PropertyDescriptor descriptor, string value)
    {
      bool flag = false;
      try
      {
        TypeConverter converter = descriptor.Converter;
        flag = true;
      }
      catch (MethodAccessException ex)
      {
      }
      lock (ReflectionHelper.syncRoot)
      {
        if (string.IsNullOrEmpty(value))
        {
          DefaultValueAttribute attribute = (DefaultValueAttribute) descriptor.Attributes[typeof (DefaultValueAttribute)];
          if (attribute != null)
            return attribute.Value;
          if (flag && descriptor.Converter is GuidConverter)
            return (object) Guid.Empty;
          if (flag && descriptor.Converter is BaseNumberConverter)
            value = "0";
        }
        if (flag)
          return descriptor.Converter.ConvertFromInvariantString(value);
        try
        {
          object obj = (object) value;
          if (descriptor.PropertyType.IsEnum)
            obj = Enum.Parse(typeof (HorizontalAlign), value);
          object propertyValueFromString = Convert.ChangeType(obj, descriptor.PropertyType);
          if (propertyValueFromString != null)
            return propertyValueFromString;
        }
        catch (InvalidCastException ex)
        {
        }
        return (object) null;
      }
    }

    /// <summary>
    /// Shallow copies public properties of the source object to a target object.
    /// Shallow copy means that if the property is reference type, only the reference is copied.
    /// </summary>
    /// <typeparam name="T">The type of the object to copy.</typeparam>
    /// <param name="source">The source object.</param>
    /// <param name="target">The target object. Pass null to create a new instance.</param>
    /// <param name="ignores">Optional filters used to ignore properties while copying.</param>
    /// <returns>A shallow copy of the source object.</returns>
    internal static T ShallowCopyProperties<T>(
      T source,
      T target,
      params Func<PropertyDescriptor, bool>[] ignores)
      where T : class
    {
      Type type = source.GetType();
      if ((object) target == null)
        target = (T) ReflectionHelper.CreateInstance(type, string.Empty, (object) source);
      IEnumerable<PropertyDescriptor> source1 = TypeDescriptor.GetProperties(type).OfType<PropertyDescriptor>();
      if (ignores != null)
        source1 = source1.Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (p => !((IEnumerable<Func<PropertyDescriptor, bool>>) ignores).Any<Func<PropertyDescriptor, bool>>((Func<Func<PropertyDescriptor, bool>, bool>) (i => i(p)))));
      foreach (PropertyDescriptor propertyDescriptor in source1)
      {
        try
        {
          object obj = propertyDescriptor.GetValue((object) source);
          if (!propertyDescriptor.IsReadOnly)
            propertyDescriptor.SetValue((object) target, obj);
        }
        catch (Exception ex)
        {
        }
      }
      return target;
    }

    /// <summary>
    /// Shallow copies public and private fields of the source object to a target object.
    /// Shallow copy means that if the field is reference type, only the reference is copied.
    /// </summary>
    /// <typeparam name="T">The type of the object to copy.</typeparam>
    /// <param name="source">The source object.</param>
    /// <param name="target">The target object. Pass null to create a new instance.</param>
    /// <param name="ignores">Optional filters used to ignore fields while copying.</param>
    /// <returns>A shallow copy of the source object.</returns>
    internal static T ShallowCopyFields<T>(
      T source,
      T target,
      params Func<FieldInfo, bool>[] ignores)
      where T : class
    {
      Type type1 = source.GetType();
      if ((object) target == null)
        target = (T) ReflectionHelper.CreateInstance(type1, string.Empty, (object) source);
      for (Type type2 = type1; type2 != typeof (object); type2 = type2.BaseType)
      {
        IEnumerable<FieldInfo> source1 = (IEnumerable<FieldInfo>) type2.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (ignores != null)
          source1 = source1.Where<FieldInfo>((Func<FieldInfo, bool>) (f => !((IEnumerable<Func<FieldInfo, bool>>) ignores).Any<Func<FieldInfo, bool>>((Func<Func<FieldInfo, bool>, bool>) (i => i(f)))));
        foreach (FieldInfo fieldInfo in source1)
        {
          try
          {
            fieldInfo.SetValue((object) target, fieldInfo.GetValue((object) source));
          }
          catch (Exception ex)
          {
          }
        }
      }
      return target;
    }

    /// <summary>
    /// The method copies the properties of the specified property descriptors. In case that the target property is not null then only the properties
    /// with name different from "Id" are copped across.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="sourceDescriptor">The property descriptor of source object.</param>
    /// <param name="destinationDescriptor">The property descriptor of destination object.</param>
    internal static void CopyArtificialAssociation(
      object source,
      object destination,
      PropertyDescriptor sourceDescriptor,
      PropertyDescriptor destinationDescriptor)
    {
      object component1 = destinationDescriptor.GetValue(destination);
      object component2 = sourceDescriptor.GetValue(source);
      if (component1 == null)
      {
        component1 = destinationDescriptor.PropertyType.GetConstructor(Type.EmptyTypes).Invoke((object[]) null);
        destinationDescriptor.SetValue(destination, component1);
      }
      PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties(sourceDescriptor.PropertyType);
      PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties(destinationDescriptor.PropertyType);
      foreach (PropertyDescriptor propertyDescriptor in properties1)
        properties2[propertyDescriptor.Name].SetValue(component1, propertyDescriptor.GetValue(component2));
    }

    /// <summary>Construct property path</summary>
    /// <param name="current">Current property path</param>
    /// <param name="propName">Property name</param>
    /// <returns>Concatenated property path</returns>
    private static string GetPropertyPath(string current, string propName)
    {
      if (!string.IsNullOrEmpty(current))
        current += ".";
      current += propName;
      return current;
    }

    /// <summary>Copies the properties.</summary>
    /// <param name="source">Object to get values from.</param>
    /// <param name="destination">Object to copy values to.</param>
    /// <param name="createDelegate">The create delegate.</param>
    /// <param name="canCopyPropertyDelegate">Optional delegate that determines when a property can be copied.</param>
    /// <param name="propertyPath">Property path in the original object</param>
    private static void CopyProperties(
      object source,
      ref object destination,
      CreateInstanceDelegate createDelegate,
      CanCopyPropertyDelegate canCopyPropertyDelegate,
      string propertyPath)
    {
      PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties(source);
      PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties(destination);
      foreach (PropertyDescriptor propertyDescriptor1 in properties1)
      {
        PropertyDescriptor propertyDescriptor2 = properties2[propertyDescriptor1.Name];
        if (!(source is Array) && propertyDescriptor2 != null)
        {
          object obj1 = propertyDescriptor1.GetValue(source);
          if (propertyDescriptor2 is ContentLinksPropertyDescriptor && obj1 != null)
          {
            ContentLink contentLink = (ContentLink) obj1;
            if (destination is IDataItem dataItem)
            {
              contentLink.ParentItemId = dataItem.Id;
              contentLink.ParentItemType = dataItem.GetType().FullName;
              contentLink.ParentItemProviderName = ((IDataProviderBase) dataItem.Provider).Name;
              propertyDescriptor2.SetValue(destination, (object) contentLink);
              continue;
            }
          }
          Type type1 = TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product", false);
          if (destination is Telerik.Sitefinity.GenericContent.Model.Content && propertyDescriptor2.Name == "Content" || type1 != (Type) null && type1.IsAssignableFrom(destination.GetType()) && propertyDescriptor2.Name == "Description" || propertyDescriptor2.IsLongText())
          {
            if ((object) (obj1 as Lstring) != null)
              ((Lstring) obj1).PersistedValue = LinkParser.UnresolveLinks(((Lstring) obj1).PersistedValue);
            else if (obj1 is string)
              obj1 = (object) LinkParser.UnresolveLinks((string) obj1);
          }
          if (!propertyDescriptor2.IsReadOnly && canCopyPropertyDelegate(propertyDescriptor2))
          {
            if (propertyDescriptor2.PropertyType.IsArray)
              propertyDescriptor2.SetValue(destination, obj1);
            else if (Attribute.GetCustomAttribute((MemberInfo) propertyDescriptor1.PropertyType, typeof (ArtificialAssociationAttribute)) != null)
            {
              ReflectionHelper.CopyArtificialAssociation(source, destination, propertyDescriptor1, propertyDescriptor2);
            }
            else
            {
              object objB = ReflectionHelper.DeepCopy(obj1, propertyDescriptor1.PropertyType, createDelegate, canCopyPropertyDelegate, ReflectionHelper.GetPropertyPath(propertyPath, propertyDescriptor1.Name));
              object objA = propertyDescriptor2.GetValue(destination);
              if ((object) (obj1 as Lstring) != null)
              {
                bool flag;
                if (!ReflectionHelper.IsEcommerceType(destination))
                {
                  string persistedValue = ((Lstring) obj1).PersistedValue;
                  ((Lstring) objB).Value = persistedValue;
                  flag = (Lstring) objA != (Lstring) objB;
                }
                else
                {
                  flag = ((Lstring) obj1).Value != ((Lstring) obj1).PersistedValue;
                  string persistedValue = ((Lstring) obj1).PersistedValue;
                  ((Lstring) objB).Value = persistedValue;
                }
                if (flag)
                  propertyDescriptor2.SetValue(destination, objB);
              }
              else if (!object.Equals(objA, objB))
                propertyDescriptor2.SetValue(destination, objB);
            }
          }
          else
          {
            object obj2 = propertyDescriptor2.GetValue(destination);
            IList list1 = obj1 as IList;
            if (obj2 is IList list2 && !list2.IsReadOnly && !list2.IsFixedSize)
            {
              if (ReflectionHelper.IsTaxonomyProperty(propertyDescriptor2))
                ReflectionHelper.CopyTaxonomyProperty(destination, createDelegate, canCopyPropertyDelegate, propertyPath, propertyDescriptor1, propertyDescriptor2, list1, list2);
              else if (list1 != null && list1.Count > 0)
              {
                Type type2 = list1[0].GetType();
                bool flag = true;
                if (ReflectionHelper.IsValueType(type2) || ReflectionHelper.IsSimpleType(type2))
                  flag = ReflectionHelper.CheckArraysHaveDifferentValues(list1, list2);
                if (flag)
                {
                  list2.Clear();
                  foreach (object source1 in (IEnumerable) list1)
                  {
                    object obj3 = ReflectionHelper.DeepCopy(source1, type2, createDelegate, canCopyPropertyDelegate, ReflectionHelper.GetPropertyPath(propertyPath, propertyDescriptor1.Name));
                    list2.Add(obj3);
                  }
                }
              }
              else if (list2.Count != 0)
                list2.Clear();
            }
            else if (obj1 is IDictionary)
            {
              if (((ICollection) obj1).Count > 0)
              {
                ((IDictionary) obj2).Clear();
                IDictionaryEnumerator enumerator = ((IDictionary) obj1).GetEnumerator();
                enumerator.MoveNext();
                DictionaryEntry current = (DictionaryEntry) enumerator.Current;
                Type type3 = current.Key.GetType();
                Type type4 = current.Value.GetType();
                foreach (DictionaryEntry dictionaryEntry in (IDictionary) obj1)
                {
                  object key = ReflectionHelper.DeepCopy(dictionaryEntry.Key, type3, createDelegate, canCopyPropertyDelegate, ReflectionHelper.GetPropertyPath(propertyPath, propertyDescriptor1.Name));
                  object obj4 = ReflectionHelper.DeepCopy(dictionaryEntry.Value, type4, createDelegate, canCopyPropertyDelegate, ReflectionHelper.GetPropertyPath(propertyPath, propertyDescriptor1.Name));
                  ((IDictionary) obj2).Add(key, obj4);
                }
              }
            }
            else if (typeof (IDictionary<,>).IsAssignableFrom(propertyDescriptor1.PropertyType))
            {
              obj2.GetType().GetMethod("Clear").Invoke(obj2, (object[]) null);
              Type propertyType = propertyDescriptor1.PropertyType;
              if (((IEnumerable) obj1).GetEnumerator().MoveNext())
              {
                Type[] genericArguments = propertyType.GetGenericArguments();
                Type type5 = genericArguments[0];
                Type type6 = genericArguments[1];
                MethodInfo method = propertyType.GetMethod("Add", new Type[2]
                {
                  type5,
                  type6
                });
                foreach (object obj5 in (IEnumerable) obj1)
                {
                  object source2 = type5.GetProperty("Key").GetValue(obj5, (object[]) null);
                  object source3 = type6.GetProperty("Value").GetValue(obj5, (object[]) null);
                  Type type7 = type5;
                  CreateInstanceDelegate createDelegate1 = createDelegate;
                  CanCopyPropertyDelegate canCopyPropertyDelegate1 = canCopyPropertyDelegate;
                  string propertyPath1 = ReflectionHelper.GetPropertyPath(propertyPath, propertyDescriptor1.Name);
                  object obj6 = ReflectionHelper.DeepCopy(source2, type7, createDelegate1, canCopyPropertyDelegate1, propertyPath1);
                  object obj7 = ReflectionHelper.DeepCopy(source3, type6, createDelegate, canCopyPropertyDelegate, ReflectionHelper.GetPropertyPath(propertyPath, propertyDescriptor1.Name));
                  method.Invoke(source3, new object[2]
                  {
                    obj6,
                    obj7
                  });
                }
              }
            }
          }
        }
      }
    }

    private static bool IsEcommerceType(object item)
    {
      if (item == null)
        return false;
      string lower = item.GetType().FullName.ToLower();
      return lower.StartsWith("telerik.sitefinity.ecommerce.") || lower.StartsWith("telerik.sitefinity.dynamictypes.model.sf_ec_");
    }

    private static bool CheckArraysHaveDifferentValues(IList source, IList destination)
    {
      if (source.Count != destination.Count)
        return true;
      foreach (object obj in (IEnumerable) source)
      {
        if (!destination.Contains(obj))
          return true;
      }
      return false;
    }

    private static void CopyTaxonomyProperty(
      object destination,
      CreateInstanceDelegate createDelegate,
      CanCopyPropertyDelegate canCopyPropertyDelegate,
      string propertyPath,
      PropertyDescriptor srcDesc,
      PropertyDescriptor destDesc,
      IList sourceListValue,
      IList destListValue)
    {
      if (sourceListValue != null && sourceListValue.Count > 0)
      {
        Type type = sourceListValue[0].GetType();
        List<Guid> guidList = new List<Guid>();
        bool flag = false;
        foreach (object source in (IEnumerable) sourceListValue)
        {
          Guid guid = (Guid) ReflectionHelper.DeepCopy(source, type, createDelegate, canCopyPropertyDelegate, ReflectionHelper.GetPropertyPath(propertyPath, srcDesc.Name));
          guidList.Add(guid);
          if (!destListValue.Contains((object) guid))
            flag = true;
        }
        if (!flag && sourceListValue.Count != destListValue.Count)
          flag = true;
        if (!flag)
          return;
        OrganizerBase organizer = ((IOrganizable) destination).Organizer;
        if (destination is IContentLifecycle)
          organizer.StatisticType = ((Telerik.Sitefinity.GenericContent.Model.Content) destination).Status;
        organizer.Clear(destDesc.Name);
        organizer.AddTaxa(destDesc.Name, guidList.ToArray());
      }
      else
      {
        if (destListValue.Count <= 0)
          return;
        ((IOrganizable) destination).Organizer.Clear(destDesc.Name);
      }
    }

    /// <summary>Copies the public fields.</summary>
    /// <param name="source">Object to get values from.</param>
    /// <param name="destination">Object to copy values to.</param>
    /// <param name="createDelegate">Optional delegate to create instances</param>
    /// <param name="canCopyPropertyDelegate">Optional delegate that determines when a property can be copied</param>
    /// <param name="propertyPath">Property path in the original object</param>
    private static void CopyPublicFields(
      object source,
      ref object destination,
      CreateInstanceDelegate createDelegate,
      CanCopyPropertyDelegate canCopyPropertyDelegate,
      string propertyPath)
    {
      Type type1 = source.GetType();
      Type type2 = destination.GetType();
      foreach (FieldInfo field1 in type1.GetFields(ReflectionHelper.PublicFieldBindingFlags))
      {
        FieldInfo field2 = type2.GetField(field1.Name, ReflectionHelper.PublicFieldBindingFlags);
        if (field2 != (FieldInfo) null)
        {
          object obj = ReflectionHelper.DeepCopy(field1.GetValue(source), type1, createDelegate, canCopyPropertyDelegate, propertyPath);
          field2.SetValue(destination, obj);
        }
      }
    }

    /// <summary>Default way to create an instance of an object</summary>
    /// <param name="requestedType">Type to create an instance of</param>
    /// <param name="propertyPath">Property path, unused</param>
    /// <param name="source">Object to get values from.</param>
    /// <returns>Object instance</returns>
    private static object CreateInstance(Type requestedType, string propertyPath, object source)
    {
      if (requestedType.IsAbstract)
        return (object) null;
      if (source is Array sourceArray)
      {
        Array instance = Array.CreateInstance(sourceArray.GetType().GetElementType(), sourceArray.Length);
        Array.Copy(sourceArray, instance, sourceArray.Length);
        return (object) instance;
      }
      ConstructorInfo constructor = requestedType.GetConstructor(Type.EmptyTypes);
      return constructor != (ConstructorInfo) null ? constructor.Invoke(new object[0]) : FormatterServices.GetUninitializedObject(requestedType);
    }

    /// <summary>The default copy condition</summary>
    /// <param name="desc">Property descriptor</param>
    /// <returns>True if <paramref name="desc" /> is not null =&gt; always</returns>
    private static bool CanCopyProperty(PropertyDescriptor desc) => desc != null;

    /// <summary>
    /// Determines whether <paramref name="typeToCheck" /> is a simple type
    /// </summary>
    /// <param name="typeToCheck">The type to check.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="typeToCheck" /> is a simple type; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsSimpleType(Type typeToCheck) => ReflectionHelper.SimpleTypes.Contains(typeToCheck) || typeToCheck.IsEnum;

    /// <summary>
    /// Determines whether <paramref name="typeToCheck" /> is a value type
    /// </summary>
    /// <param name="typeToCheck">The type to check.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="typeToCheck" /> is a value type; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsValueType(Type typeToCheck) => typeToCheck.IsValueType;

    private static bool IsTaxonomyProperty(PropertyDescriptor propertyDescriptor) => propertyDescriptor is TaxonomyPropertyDescriptor;

    /// <summary>
    /// Gets an array of types representing the types of the objects passed in the parameter? The types are returned in the same order as the parameters.
    /// </summary>
    /// <param name="parameters">The objects which types will be returned.</param>
    /// <returns>An array of types representing the types of the objects.</returns>
    private static Type[] GetTypesOfParameters(params object[] parameters)
    {
      if (parameters == null)
        return (Type[]) null;
      Type[] typesOfParameters = new Type[parameters.Length];
      for (int index = 0; index < parameters.Length; ++index)
        typesOfParameters[index] = parameters[index].GetType();
      return typesOfParameters;
    }
  }
}
