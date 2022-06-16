// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.Serialization.SiteSyncSerializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Translators;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Cmis.RestAtom;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.SiteSync.Serialization
{
  internal class SiteSyncSerializer : ISiteSyncSerializer
  {
    private const string PrefixSeparator = "!";
    private string registrationPrefix;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.Serialization.SiteSyncSerializer" /> class.
    /// </summary>
    public SiteSyncSerializer()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.Serialization.SiteSyncSerializer" /> class.
    /// </summary>
    /// <param name="registrationPrefix">The registration prefix.</param>
    public SiteSyncSerializer(string registrationPrefix) => this.registrationPrefix = registrationPrefix;

    internal bool PermissionsSyncDisabled { get; set; }

    public static void RegisterConverter(Type dataType, Type converterType, string prefix = null)
    {
      string containerTypeMappingName = SiteSyncSerializer.GetContainerTypeMappingName(dataType, prefix);
      ObjectFactory.Container.RegisterType(typeof (ISiteSyncConverter), converterType, containerTypeMappingName, (LifetimeManager) new ContainerControlledLifetimeManager());
    }

    public static void RegisterConverter<TData, TConverter>(string prefix = null) where TConverter : ISiteSyncConverter => SiteSyncSerializer.RegisterConverter(typeof (TData), typeof (TConverter), prefix);

    public static ISiteSyncConverter ResolveConverter(
      Type type,
      bool lookupBaseTypes = false,
      bool throwExceptions = false,
      string prefix = null)
    {
      ISiteSyncConverter siteSyncConverter = (ISiteSyncConverter) null;
      Type type1 = type;
      do
      {
        string containerTypeMappingName = SiteSyncSerializer.GetContainerTypeMappingName(type1, prefix);
        if (ObjectFactory.IsTypeRegistered<ISiteSyncConverter>(containerTypeMappingName))
        {
          siteSyncConverter = ObjectFactory.Container.Resolve<ISiteSyncConverter>(containerTypeMappingName);
          break;
        }
        if (lookupBaseTypes)
          type1 = type1.BaseType;
      }
      while (lookupBaseTypes && type1 != (Type) null);
      if (throwExceptions && siteSyncConverter == null)
      {
        string message = "No appropriate ISiteSyncConverter found for type " + type.FullName;
        if (lookupBaseTypes)
          message += ", neither for any of its base types";
        throw new InvalidOperationException(message);
      }
      return siteSyncConverter;
    }

    public static ISiteSyncConverter ResolveConverterByAssignableType(
      Type type,
      string registrationPrefix,
      bool throwExceptions = false)
    {
      foreach (RegisterEventArgs registerEventArgs in ObjectFactory.GetArgsForType(typeof (ISiteSyncConverter)))
      {
        string name = string.Empty;
        if (string.IsNullOrEmpty(registrationPrefix))
        {
          if (registerEventArgs.Name.IndexOf("!") == -1)
            name = registerEventArgs.Name;
        }
        else
        {
          string oldValue = registrationPrefix + "!";
          if (registerEventArgs.Name.StartsWith(oldValue))
            name = registerEventArgs.Name.Replace(oldValue, string.Empty);
        }
        if (!string.IsNullOrEmpty(name) && TypeResolutionService.ResolveType(name).IsAssignableFrom(type))
          return ObjectFactory.Container.Resolve<ISiteSyncConverter>(registerEventArgs.Name);
      }
      if (throwExceptions)
        throw new InvalidOperationException("No appropriate ISiteSyncConverter found for type " + type.FullName);
      return (ISiteSyncConverter) null;
    }

    private static string GetContainerTypeMappingName(Type type, string prefix = null) => string.IsNullOrEmpty(prefix) ? type.FullName : prefix + "!" + type.FullName;

    internal bool DisableConverters { get; set; }

    public virtual MappingSettings GetTypeMappings(Type type)
    {
      IList<Mapping> mappingList = this.BuildMappingForType(type);
      MappingSettings typeMappings = new MappingSettings();
      foreach (Mapping mapping in (IEnumerable<Mapping>) mappingList)
        typeMappings.Mappings.Add(mapping);
      return typeMappings;
    }

    public virtual IList<Mapping> BuildMappingForType(Type type)
    {
      List<Mapping> mappingList = new List<Mapping>();
      ISet<string> stringSet = this.ExcludedProperties(type);
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(type))
      {
        if (!stringSet.Contains(property.Name))
        {
          if (type.ImplementsInterface(typeof (IHasParent)) && property.ComponentType != (Type) null && property.ComponentType == typeof (IHasParent))
          {
            mappingList.Add(PublishingSystemFactory.CreateMapping(property.Name, "ItemToGuidTranslator", true, property.Name));
          }
          else
          {
            string translatorForType = this.GetTranslatorForType(property.PropertyType);
            if (!string.IsNullOrEmpty(translatorForType))
              mappingList.Add(PublishingSystemFactory.CreateMapping(property.Name, translatorForType, true, property.Name));
          }
        }
      }
      mappingList.Add(PublishingSystemFactory.CreateMapping("objectId", "TransparentTranslator", true, "Id"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("objectTypeId", "TransparentTranslator", true));
      mappingList.Add(PublishingSystemFactory.CreateMapping("ItemAction", "TransparentTranslator", true));
      mappingList.Add(PublishingSystemFactory.CreateMapping("LangId", "TransparentTranslator", true));
      mappingList.Add(PublishingSystemFactory.CreateMapping("Author", "authorTranslator", true, "Author"));
      return (IList<Mapping>) mappingList;
    }

    protected virtual ISet<string> ExcludedProperties(Type type)
    {
      HashSet<string> stringSet = new HashSet<string>();
      stringSet.Add("ApplicationName");
      stringSet.Add("Author");
      if (type.ImplementsInterface(typeof (ISecuredObject)))
      {
        if (this.PermissionsSyncDisabled)
        {
          stringSet.Add("InheritsPermissions");
          stringSet.Add("CanInheritPermissions");
        }
        stringSet.Add("PermissionsetObjectTitleResKeys");
      }
      foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(type).OfType<PropertyDescriptor>().Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.PropertyType == typeof (Lstring))))
      {
        if (propertyDescriptor.Attributes.OfType<LStringPropertyAttribute>().Any<LStringPropertyAttribute>((Func<LStringPropertyAttribute, bool>) (x => x.Transient)))
          stringSet.Add(propertyDescriptor.Name);
      }
      return (ISet<string>) stringSet;
    }

    protected virtual bool IsSimpleType(Type sourceType) => sourceType.IsPrimitive || typeof (Decimal) == sourceType || typeof (string) == sourceType || typeof (DateTime) == sourceType || typeof (Guid) == sourceType;

    protected virtual string GetTranslatorForSimpleType(Type sourceType)
    {
      if (typeof (bool) == sourceType)
        return "TransparentTranslator";
      if (typeof (Decimal) == sourceType || typeof (double) == sourceType || typeof (float) == sourceType)
        return ToDecimalTranslator.TranslatorName;
      if (typeof (ulong) == sourceType)
        throw new NotSupportedException("tyepof ULONG is not supported ");
      if (typeof (int) == sourceType || typeof (uint) == sourceType || typeof (long) == sourceType || typeof (short) == sourceType || typeof (ushort) == sourceType || typeof (byte) == sourceType || typeof (sbyte) == sourceType)
        return ToLongTranslator.TranslatorName;
      return typeof (string) == sourceType || typeof (Guid) == sourceType || typeof (DateTime) == sourceType ? "TransparentTranslator" : (string) null;
    }

    protected virtual string GetTrasnaltorComplexTypes(Type sourceType)
    {
      if (!this.DisableConverters && SiteSyncSerializer.ResolveConverterByAssignableType(sourceType, this.registrationPrefix) != null)
        return "TransparentTranslator";
      if (typeof (Lstring).IsAssignableFrom(sourceType))
        return "concatenationtranslator";
      if (typeof (LanguageData).IsAssignableFrom(sourceType))
        return "languageDataTranslator";
      if (sourceType.IsGenericType)
      {
        Type[] genericArguments = sourceType.GetGenericArguments();
        if (genericArguments.Length != 0)
        {
          Type type = genericArguments[0];
          if (typeof (LanguageData).IsAssignableFrom(type))
            return "languageDataTranslator";
          if (typeof (Synonym).IsAssignableFrom(type))
            return "synonymTranslatorTranslator";
          return this.IsSimpleType(type) ? "TransparentTranslator" : (string) null;
        }
      }
      if (sourceType.IsArray && this.IsSimpleType(sourceType.GetElementType()))
        return "TransparentTranslator";
      return sourceType.IsEnum ? EnumToIntTranslator.TranslatorName : (string) null;
    }

    protected virtual string GetTranslatorForType(Type type) => this.IsSimpleType(type) ? this.GetTranslatorForSimpleType(type) : this.GetTrasnaltorComplexTypes(type);

    public virtual void SetProperties(object destination, object source, object args = null)
    {
      // ISSUE: reference to a compiler-generated field
      if (SiteSyncSerializer.\u003C\u003Eo__22.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SiteSyncSerializer.\u003C\u003Eo__22.\u003C\u003Ep__0 = CallSite<Action<CallSite, SiteSyncSerializer, object, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, nameof (SetProperties), (IEnumerable<Type>) null, typeof (SiteSyncSerializer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[5]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      SiteSyncSerializer.\u003C\u003Eo__22.\u003C\u003Ep__0.Target((CallSite) SiteSyncSerializer.\u003C\u003Eo__22.\u003C\u003Ep__0, this, destination, source, (object) null, args);
    }

    public virtual void SetProperties(
      object destination,
      object source,
      Func<PropertyDescriptor, PropertyDescriptor, bool> setPredicate,
      object args = null)
    {
      PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties(source);
      PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties(destination);
      foreach (PropertyDescriptor propertyDescriptor1 in properties1.OfType<PropertyDescriptor>())
      {
        PropertyDescriptor propertyDescriptor2 = properties2.Find(propertyDescriptor1.Name, true);
        if (propertyDescriptor2 != null && (setPredicate == null || setPredicate(propertyDescriptor1, propertyDescriptor2)))
        {
          object obj = propertyDescriptor1.GetValue(source);
          // ISSUE: reference to a compiler-generated field
          if (SiteSyncSerializer.\u003C\u003Eo__23.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            SiteSyncSerializer.\u003C\u003Eo__23.\u003C\u003Ep__0 = CallSite<Action<CallSite, SiteSyncSerializer, PropertyDescriptor, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetProperty", (IEnumerable<Type>) null, typeof (SiteSyncSerializer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[5]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          SiteSyncSerializer.\u003C\u003Eo__23.\u003C\u003Ep__0.Target((CallSite) SiteSyncSerializer.\u003C\u003Eo__23.\u003C\u003Ep__0, this, propertyDescriptor2, obj, destination, args);
        }
      }
    }

    protected virtual void SetProperty(
      PropertyDescriptor prop,
      object value,
      object instance,
      object args)
    {
      Type propertyType = prop.PropertyType;
      ISiteSyncConverter siteSyncConverter = (ISiteSyncConverter) null;
      if (!this.DisableConverters)
        siteSyncConverter = SiteSyncSerializer.ResolveConverterByAssignableType(propertyType, this.registrationPrefix);
      if (siteSyncConverter != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__0 = CallSite<Action<CallSite, ISiteSyncConverter, object, PropertyDescriptor, object, Type, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ImportProperty", (IEnumerable<Type>) null, typeof (SiteSyncSerializer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[6]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__0.Target((CallSite) SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__0, siteSyncConverter, instance, prop, value, propertyType, args);
      }
      else if (propertyType.IsEnum)
        this.SetEnum(prop, value, instance);
      else if (propertyType == typeof (Lstring))
      {
        this.SetLstring(prop, value, instance);
      }
      else
      {
        Type interfaceImplementation = propertyType.GetGenericInterfaceImplementation(typeof (IDictionary<,>));
        if (interfaceImplementation != (Type) null)
        {
          Type[] genericArguments = interfaceImplementation.GetGenericArguments();
          this.SetDictionary(prop, value, instance, genericArguments[0], genericArguments[1]);
        }
        else if (typeof (IDictionary).IsAssignableFrom(propertyType))
          this.SetDictionary(prop, value, instance);
        bool flag = true;
        if (propertyType.ImplementsInterface(typeof (IList)) && propertyType != typeof (string))
        {
          IEnumerable items = (IEnumerable) value;
          if (propertyType.IsArray)
            this.SetArray(prop, items, instance);
          else
            this.SetList(prop, items, instance);
          flag = false;
        }
        if (propertyType.ImplementsGenericInterface(typeof (IList<>)))
        {
          Type[] genericArguments = propertyType.GetGenericArguments();
          if (((IEnumerable<Type>) genericArguments).Any<Type>())
          {
            Type c = genericArguments[0];
            if (typeof (Synonym).IsAssignableFrom(c))
            {
              // ISSUE: reference to a compiler-generated field
              if (SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__1 == null)
              {
                // ISSUE: reference to a compiler-generated field
                SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__1 = CallSite<Action<CallSite, SiteSyncSerializer, PropertyDescriptor, object, object, Type, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetSynonym", (IEnumerable<Type>) null, typeof (SiteSyncSerializer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[6]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__1.Target((CallSite) SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__1, this, prop, value, instance, c, args);
            }
            else if (typeof (LanguageData).IsAssignableFrom(c))
            {
              // ISSUE: reference to a compiler-generated field
              if (SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__2 == null)
              {
                // ISSUE: reference to a compiler-generated field
                SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__2 = CallSite<Action<CallSite, SiteSyncSerializer, PropertyDescriptor, object, object, Type, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetLanguageData", (IEnumerable<Type>) null, typeof (SiteSyncSerializer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[6]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__2.Target((CallSite) SiteSyncSerializer.\u003C\u003Eo__24.\u003C\u003Ep__2, this, prop, value, instance, c, args);
            }
            else
            {
              IEnumerable items = (IEnumerable) value;
              this.SetList(prop, items, instance);
            }
          }
          flag = false;
        }
        if (!flag)
          return;
        this.SetValue(prop, value, instance);
      }
    }

    private void ImportProperty(
      PropertyDescriptor prop,
      object value,
      Type type,
      object instance,
      object args)
    {
    }

    private bool ShoudSetProperty(PropertyDescriptor prop)
    {
      if (this.ShouldIgnoreProperty(prop))
        return false;
      return !prop.IsReadOnly || this.ShouldSetReadOnlyProperty(prop);
    }

    private bool ShouldIgnoreProperty(PropertyDescriptor prop) => prop.Name == "Id" || prop.Name == "Avatar" || typeof (DraftData).IsAssignableFrom(prop.ComponentType) && (prop.Name == "Status" || prop.Name == "Visible") || typeof (FormDescription).IsAssignableFrom(prop.ComponentType) && (prop.Name == "SubscriptionListId" || prop.Name == "SubscriptionListIdAfterFormUpdate" || prop.Name == "FormEntriesSeed") || typeof (User).IsAssignableFrom(prop.ComponentType) && (prop.Name == "LastActivityDate" || prop.Name == "LastLockoutDate" || prop.Name == "LastLoginDate") || typeof (PageTemplate).IsAssignableFrom(prop.ComponentType) && (prop.Name == "Theme" || prop.Name == "TemplateId") || prop.Name == "ApplicationName";

    private bool ShouldSetReadOnlyProperty(PropertyDescriptor prop) => typeof (ILifecycleDataItemGeneric).IsAssignableFrom(prop.ComponentType) && prop.Name == "OriginalContentId" || typeof (Content).IsAssignableFrom(prop.ComponentType) && (prop.Name == "OriginalContentId" || prop.Name == "Owner") || typeof (MediaContent).IsAssignableFrom(prop.ComponentType) && (prop.Name == "TotalSize" || prop.Name == "MimeType" || prop.Name == "Extension") || typeof (Image).IsAssignableFrom(prop.ComponentType) && (prop.Name == "Width" || prop.Name == "Height");

    protected virtual void SetArray(
      PropertyDescriptor destinationProperty,
      IEnumerable items,
      object component)
    {
      if (!destinationProperty.PropertyType.IsArray)
        return;
      Array array = (Array) null;
      if (items != null)
      {
        if (items is string)
        {
          array = (Array) new string[1]
          {
            (string) items
          };
        }
        else
        {
          int length = 0;
          foreach (object obj in items)
            ++length;
          Type elementType = destinationProperty.PropertyType.GetElementType();
          array = Array.CreateInstance(elementType, length);
          int index = 0;
          foreach (object obj1 in items)
          {
            if (obj1 is string str)
            {
              object obj2 = this.ConvertFromString(elementType, str);
              array.SetValue(obj2, index);
            }
            else
              array.SetValue(obj1, index);
            ++index;
          }
        }
      }
      destinationProperty.SetValue(component, (object) array);
    }

    protected virtual void SetList(
      PropertyDescriptor destinationProperty,
      IEnumerable items,
      object component)
    {
      if (component.GetType() == typeof (FormDraft) && destinationProperty.Name == "PublishedTranslations")
        return;
      object obj1 = destinationProperty.GetValue(component);
      Type propertyType = destinationProperty.PropertyType;
      if (propertyType.ImplementsGenericInterface(typeof (IList<>)))
      {
        if (items is string)
          items = (IEnumerable) new string[1]
          {
            (string) items
          };
        Type[] genericArguments = propertyType.GetGenericArguments();
        if (genericArguments.Length != 0)
        {
          Type type = genericArguments[0];
          this.GetType().GetMethod("CollectionClear", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(type).Invoke((object) this, new object[1]
          {
            obj1
          });
          if (items == null)
            return;
          MethodInfo methodInfo = this.GetType().GetMethod("ListAdd", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(type);
          TypeConverter converter = TypeDescriptor.GetConverter(type);
          IEnumerator enumerator = items.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              object current = enumerator.Current;
              object obj2 = current == null || !(type == current.GetType()) ? converter.ConvertFrom(current) : current;
              methodInfo.Invoke((object) this, new object[2]
              {
                obj1,
                obj2
              });
            }
            return;
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        }
      }
      if (!(obj1 is IList))
        return;
      ((IList) obj1).Clear();
      if (items == null)
        return;
      if (items.GetType() == typeof (string))
      {
        Type type = obj1.GetType();
        if (type.IsArray)
        {
          if (!(type.GetElementType() == typeof (Guid)))
            return;
          Guid guid = Guid.Parse((string) items);
          ((IList) obj1).Add((object) guid);
        }
        else
        {
          Type[] genericArguments = type.GetGenericArguments();
          if (((IEnumerable<Type>) genericArguments).Count<Type>() != 1 || !(genericArguments[0] == typeof (Guid)))
            return;
          Guid guid = Guid.Parse((string) items);
          ((IList) obj1).Add((object) guid);
        }
      }
      else
      {
        foreach (object obj3 in items)
          ((IList) obj1).Add(obj3);
      }
    }

    protected virtual void SetDictionary(
      PropertyDescriptor prop,
      object sourceValue,
      object instance,
      Type keyType = null,
      Type valueType = null)
    {
      if (sourceValue == null)
        dictionary = (IDictionary<string, string>) null;
      else if (!(sourceValue is IDictionary<string, string> dictionary))
        throw new InvalidCastException("Trying to set non-dictionary value to a dictionary property.");
      object dict = prop.GetValue(instance);
      if (prop.IsReadOnly || dict != null)
      {
        if (dict == null)
          return;
        if (keyType == (Type) null)
        {
          ((IDictionary) dict).Clear();
        }
        else
        {
          Type type = typeof (KeyValuePair<,>).MakeGenericType(keyType, valueType);
          this.GetType().GetMethod("CollectionClear", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(type).Invoke((object) this, new object[1]
          {
            dict
          });
        }
      }
      else if (dictionary != null)
      {
        Type type = prop.PropertyType;
        if (type.IsAbstract)
        {
          if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (IDictionary<,>))
          {
            type = typeof (Dictionary<,>).MakeGenericType(keyType, valueType);
          }
          else
          {
            if (!(type == typeof (IDictionary)))
              throw new NotSupportedException("Creation of abstract dictionary types during deserialization is not supported except for the generic and non-generic IDictionary implementations. Dictionary property type: " + type.FullName);
            type = typeof (Hashtable);
          }
        }
        dict = Activator.CreateInstance(type);
        prop.SetValue(instance, dict);
      }
      if (dictionary == null || dictionary.Count == 0)
        return;
      Action<object, object> action;
      if (keyType == (Type) null)
      {
        IDictionary nonGenericDict = dict as IDictionary;
        action = (Action<object, object>) ((key, value) => nonGenericDict.Add(key, value));
      }
      else
      {
        MethodInfo genericAdd = this.GetType().GetMethod("DictionaryAdd", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(keyType, valueType);
        action = (Action<object, object>) ((key, value) => genericAdd.Invoke((object) this, new object[3]
        {
          dict,
          key,
          value
        }));
      }
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) dictionary)
      {
        object obj1 = keyType == (Type) null ? (object) keyValuePair.Key : this.ConvertFromString(keyType, keyValuePair.Key);
        object obj2 = valueType == (Type) null ? (object) keyValuePair.Value : this.ConvertFromString(valueType, keyValuePair.Value);
        action(obj1, obj2);
      }
    }

    private void CollectionClear<T>(object collection) => ((ICollection<T>) collection).Clear();

    private void ListAdd<T>(object list, T value) => ((ICollection<T>) list).Add(value);

    private void DictionaryAdd<TKey, TValue>(object dict, TKey key, TValue value) => ((IDictionary<TKey, TValue>) dict).Add(key, value);

    /// <summary>
    /// We need always to get the actual value for the specified culture and not to fallback to the invariant.
    /// In order to achieve this we set false value to IsBackend request and return it again to orginal value after we get the string.
    /// </summary>
    /// <param name="lstringProp">The lstring property</param>
    /// <param name="component">The component</param>
    /// <param name="currentLang">The current language</param>
    /// <returns></returns>
    private string GetLstringValueWithoutFallBack(
      LstringPropertyDescriptor lstringProp,
      object component,
      CultureInfo currentLang)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      object obj = currentHttpContext.Items[(object) "IsBackendRequest"];
      try
      {
        currentHttpContext.Items[(object) "IsBackendRequest"] = (object) false;
        return lstringProp.GetString(component, currentLang, false);
      }
      finally
      {
        currentHttpContext.Items[(object) "IsBackendRequest"] = obj;
      }
    }

    protected virtual void SetEnum(
      PropertyDescriptor destinationValue,
      object value,
      object component)
    {
      if (!this.ShoudSetProperty(destinationValue))
        return;
      object obj = Enum.Parse(destinationValue.PropertyType, value.ToString());
      destinationValue.SetValue(component, obj);
    }

    protected virtual void SetLstring(PropertyDescriptor prop, object value, object component)
    {
      if (value == null)
      {
        this.SetValue(prop, value, component);
      }
      else
      {
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        LstringPropertyDescriptor lstringProp = (LstringPropertyDescriptor) prop;
        string a = Reader.GetLstringValue(value, culture);
        string valueWithoutFallBack1 = this.GetLstringValueWithoutFallBack(lstringProp, component, culture);
        string lstringValue = Reader.GetLstringValue(value, CultureInfo.InvariantCulture);
        string valueWithoutFallBack2 = this.GetLstringValueWithoutFallBack(lstringProp, component, CultureInfo.InvariantCulture);
        if (a == null)
          a = value as string;
        if (a != null && !string.Equals(a, valueWithoutFallBack1))
          lstringProp.SetString(component, a, culture);
        if (lstringValue == null || string.Equals(lstringValue, valueWithoutFallBack2) || !AppSettings.CurrentSettings.LegacyMultilingual)
          return;
        lstringProp.SetStringRaw(component, CultureInfo.InvariantCulture, lstringValue);
      }
    }

    protected virtual void SetValue(PropertyDescriptor prop, object value, object component)
    {
      if (!this.ShoudSetProperty(prop))
        return;
      if (value == null || prop.PropertyType == value.GetType() || prop.PropertyType.IsNullable())
      {
        DateTime? nullable1 = value as DateTime?;
        if (nullable1.HasValue)
        {
          DateTime? nullable2 = nullable1;
          DateTime minValue = DateTime.MinValue;
          if ((nullable2.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == minValue ? 1 : 0) : 1) : 0) != 0)
            value = (object) DateTime.UtcNow;
        }
        prop.SetValue(component, value);
      }
      else
      {
        TypeConverter converter = TypeDescriptor.GetConverter(prop.PropertyType);
        object obj = prop.PropertyType == typeof (object) ? value : converter.ConvertTo(value, prop.PropertyType);
        prop.SetValue(component, obj);
      }
    }

    protected virtual object ConvertFromString(Type type, string value)
    {
      TypeConverter converter = TypeDescriptor.GetConverter(type);
      return converter.CanConvertFrom(typeof (string)) ? converter.ConvertFromString(value) : throw new Exception("Cannot convert from string to " + type.FullName);
    }

    protected virtual void SetLanguageData(
      PropertyDescriptor destinationProperty,
      object value,
      object component,
      Type elementType,
      object args)
    {
      List<string> source = new List<string>();
      if (value != null)
      {
        if (typeof (IList<string>).IsAssignableFrom(value.GetType()))
          source.AddRange((IEnumerable<string>) value);
        else if (value is string)
          source.Add((string) value);
      }
      if (!source.Any<string>())
        return;
      // ISSUE: reference to a compiler-generated field
      if (SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__0 = CallSite<Action<CallSite, Type, List<string>, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TranslateXmlToLanguageData", (IEnumerable<Type>) null, typeof (SiteSyncSerializer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__0.Target((CallSite) SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__0, typeof (LanguageDataTranslator), source, args);
      if (!SystemManager.CurrentContext.AppSettings.Multilingual)
        return;
      CultureInfo frontendLanguage = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
      // ISSUE: reference to a compiler-generated field
      if (SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Manager", typeof (SiteSyncSerializer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!(SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__1.Target((CallSite) SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__1, args) is ILifecycleManager manager))
        return;
      // ISSUE: reference to a compiler-generated field
      if (SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Item", typeof (SiteSyncSerializer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!(SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__2.Target((CallSite) SiteSyncSerializer.\u003C\u003Eo__40.\u003C\u003Ep__2, args) is ILifecycleDataItem cnt))
        return;
      ILifecycleDataItem live = manager.Lifecycle.GetLive(cnt);
      if (live == null)
        return;
      bool flag = false;
      LanguageData languageDataRaw = live.GetLanguageDataRaw((CultureInfo) null);
      if (live.Visible && live.LanguageData.Count == 1 && languageDataRaw != null && languageDataRaw.ContentState == LifecycleState.Published && live.PublishedTranslations.Count == 0)
        flag = true;
      if (!flag)
        return;
      ILifecycleDataItem master = manager.Lifecycle.GetMaster(cnt);
      master.AddPublishedTranslation(frontendLanguage.Name);
      manager.Lifecycle.GetOrCreateLanguageData(master, frontendLanguage);
      live.AddPublishedTranslation(frontendLanguage.Name);
      manager.Lifecycle.GetOrCreateLanguageData(live, frontendLanguage);
      master.IncrementLanguageVersion((ILanguageDataManager) manager, frontendLanguage);
    }

    protected virtual void SetSynonym(
      PropertyDescriptor destinationProperty,
      object value,
      object component,
      Type elementType,
      object args)
    {
      object collection = destinationProperty.GetValue(component);
      // ISSUE: reference to a compiler-generated field
      if (SiteSyncSerializer.\u003C\u003Eo__41.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SiteSyncSerializer.\u003C\u003Eo__41.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Manager", typeof (SiteSyncSerializer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      TaxonomyManager taxonomyManager = SiteSyncSerializer.\u003C\u003Eo__41.\u003C\u003Ep__0.Target((CallSite) SiteSyncSerializer.\u003C\u003Eo__41.\u003C\u003Ep__0, args) as TaxonomyManager;
      foreach (Synonym synonym1 in new List<Synonym>((IEnumerable<Synonym>) collection))
      {
        Synonym item = synonym1;
        Synonym synonym2 = taxonomyManager.GetSynonyms().Where<Synonym>((Expression<Func<Synonym, bool>>) (s => s.Id == item.Id)).FirstOrDefault<Synonym>();
        if (synonym2 != null)
          taxonomyManager.Delete(synonym2);
      }
      ((IList) collection).Clear();
      if (value == null)
        return;
      List<string> valuesToTranslate = new List<string>();
      if (typeof (IList<string>).IsAssignableFrom(value.GetType()))
        valuesToTranslate.AddRange((IEnumerable<string>) value);
      else if (value is string)
        valuesToTranslate.Add((string) value);
      if (valuesToTranslate.Count == 0)
        return;
      foreach (Synonym synonym in (IEnumerable<Synonym>) SynonymFromStringTranslator.TranslateXmlToSynonym(valuesToTranslate, elementType))
      {
        synonym.Id = taxonomyManager.Provider.GetNewGuid();
        synonym.Parent = (Taxon) component;
        ((IList) collection).Add((object) synonym);
      }
    }
  }
}
