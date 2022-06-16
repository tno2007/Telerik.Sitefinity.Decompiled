// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.WcfPropertyManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using Progress.Sitefinity.Renderer.Entities.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>
  /// Methods of this class provide a streamlined way for working with <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" />
  /// objects.
  /// </summary>
  public class WcfPropertyManager
  {
    private object objectInstance;
    private char levelDelimiter;
    private char indexerStart;
    private char indexerEnd;
    internal const char defaultLevelDelimiter = '/';
    internal const char defaultIndexerStart = '[';
    internal const char defaultIndexerEnd = ']';

    internal bool ResolveDynamicLinks { get; set; } = true;

    /// <summary>
    /// Determines the character that implies that the property after it is
    /// the child property of the property before the delimiter.
    /// </summary>
    public virtual char LevelDelimiter
    {
      get => this.levelDelimiter == char.MinValue ? '/' : this.levelDelimiter;
      set => this.levelDelimiter = value;
    }

    /// <summary>
    /// Determines the character (start of the index) which implies that
    /// property is indexable and that the value before the IndexerStart
    /// character and IndexerEnd character is in fact the index of the property.
    /// </summary>
    public virtual char IndexerStart
    {
      get => this.indexerStart == char.MinValue ? '[' : this.indexerStart;
      set => this.indexerStart = value;
    }

    /// <summary>
    /// Determines the character (end of index) which implies that property
    /// is indexable and that the value before the IndexerStart character
    /// and IndexerEnd character is in fact the index of the property.
    /// </summary>
    public virtual char IndexerEnd
    {
      get => this.indexerEnd == char.MinValue ? ']' : this.indexerEnd;
      set => this.indexerEnd = value;
    }

    /// <summary>
    /// Gets all the first level properties for the specified object.
    /// </summary>
    /// <param name="instance">Instance for which the properites ought to be retrieved.</param>
    /// <param name="controlData">Instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object which holds the persisted properties for the control.</param>
    /// <param name="culture">Culture used to populate the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MultilingualPropertyAttribute">multilingual</see> properties</param>
    /// <returns>
    /// Collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" /> objects.
    /// </returns>
    public virtual IList<WcfControlProperty> GetProperties(
      object instance,
      ControlData controlData,
      CultureInfo culture = null)
    {
      this.objectInstance = instance;
      return this.GetProperties(instance, controlData, 0, (string) null, culture);
    }

    /// <summary>
    /// Reads all the properties of the instance and returns a collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" />
    /// objects that can be serialized and sent to the client.
    /// </summary>
    /// <param name="instance">Instance for which the properties ought to be returned.</param>
    /// <param name="controlData">Instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object which holds the persisted properties for the control.</param>
    /// <param name="depth">Integer value which indicates how many levels in the property hierarchy
    /// should be read. (Use -1 to retrieve the whole object tree).</param>
    /// <param name="culture">Culture used to populate the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MultilingualPropertyAttribute">multilingual</see> properties</param>
    /// <returns>
    /// Collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" /> objects.
    /// </returns>
    public virtual IList<WcfControlProperty> GetProperties(
      object instance,
      ControlData controlData,
      int depth,
      CultureInfo culture = null)
    {
      this.objectInstance = instance;
      return this.GetProperties(instance, controlData, depth, (string) null, culture);
    }

    /// <summary>
    /// Reads all the properties of the instance, starting from the specified parentPropertyPath and reading
    /// the hierarchy to the specified depth.
    /// </summary>
    /// <param name="instance">Instance for which the properties ought to be returned.</param>
    /// <param name="controlData">The instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object which holds the persisted properties for this control.</param>
    /// <param name="depth">Integer value which indicates how many levels in the property hierarchy should
    /// be read. (Use -1 to retrieve the whole object tree).</param>
    /// <param name="parentPropertyPath">The property path of the parent, indicating starting with which property the child properties ought
    /// to be read. Pass null if you want to start from the instance itself.</param>
    /// <param name="culture">Culture used to populate the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MultilingualPropertyAttribute">multilingual</see> properties</param>
    /// <returns></returns>
    public virtual IList<WcfControlProperty> GetProperties(
      object instance,
      ControlData controlData,
      int depth,
      string parentPropertyPath,
      CultureInfo culture = null)
    {
      this.objectInstance = instance != null ? instance : throw new ArgumentNullException(nameof (instance));
      List<WcfControlProperty> list = new List<WcfControlProperty>();
      if (string.IsNullOrEmpty(parentPropertyPath))
      {
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(instance))
          this.AddPropertiesRecursively(instance, (IList<WcfControlProperty>) list, new List<PropertyDescriptor>()
          {
            property
          }, controlData, depth, culture);
      }
      else
      {
        foreach (PropertyDescriptor childProperty in this.GetPropertyDescriptor(instance.GetType(), parentPropertyPath).GetChildProperties())
          this.AddPropertiesRecursively(instance, (IList<WcfControlProperty>) list, new List<PropertyDescriptor>()
          {
            childProperty
          }, controlData, depth);
      }
      return (IList<WcfControlProperty>) list;
    }

    public virtual IList<WcfControlProperty> GetProperties(
      object instance,
      ControlData controlData,
      string parentPropertyPath,
      Guid pageId,
      CultureInfo culture = null)
    {
      if (controlData.IsOverridedControl)
      {
        ControlData overridingControl = this.GetOverridingControl(controlData.Id, pageId);
        if (overridingControl != null)
          return this.GetProperties((object) PageManager.GetManager().LoadControl((ObjectData) overridingControl, culture), overridingControl, -1, parentPropertyPath, culture);
      }
      return this.GetProperties(instance, controlData, -1, parentPropertyPath, culture);
    }

    /// <summary>
    /// Reads the collection of passed <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" /> objects and sets the values of designated
    /// properties on the actual instance passed as the second argument.
    /// </summary>
    /// <param name="wcfProperties">
    /// An instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" />.
    /// </param>
    /// <param name="instance">
    /// An instance on which the value property ought to be set.
    /// </param>
    public virtual void SetProperties(
      IEnumerable<WcfControlProperty> wcfProperties,
      object instance)
    {
      this.SetProperties(wcfProperties, instance, (ControlData) null);
    }

    internal void SetProperties(
      IEnumerable<WcfControlProperty> wcfProperties,
      object instance,
      ControlData controlData)
    {
      if (wcfProperties == null)
        throw new ArgumentNullException(nameof (wcfProperties));
      this.objectInstance = instance;
      foreach (WcfControlProperty wcfProperty in wcfProperties)
        this.SetProperty(wcfProperty, instance, controlData);
    }

    internal void SetProperty(
      WcfControlProperty wcfProperty,
      object rootInstance,
      ControlData controlData)
    {
      if (wcfProperty == null)
        throw new ArgumentNullException(nameof (wcfProperty));
      this.objectInstance = rootInstance != null ? rootInstance : throw new ArgumentNullException("instance");
      List<PropertyDescriptor> descriptorHierarchy = this.GetPropertyDescriptorHierarchy(TypeDescriptor.GetProperties(rootInstance), wcfProperty.PropertyPath);
      PropertyDescriptor propDesc = (PropertyDescriptor) null;
      object component = rootInstance;
      foreach (PropertyDescriptor propertyDescriptor in descriptorHierarchy)
      {
        propDesc = propertyDescriptor;
        if (propertyDescriptor.Converter.GetPropertiesSupported())
          component = propertyDescriptor.GetValue(component);
      }
      object html = this.ConvertValue(propDesc, wcfProperty.PropertyValue);
      if (html != null && propDesc.Attributes.OfType<DynamicLinksContainerAttribute>().FirstOrDefault<DynamicLinksContainerAttribute>() != null)
        html = (object) LinkParser.UnresolveLinks((string) html);
      if (!WcfPropertyManager.IsEditableProperty(propDesc, controlData))
        return;
      WcfPropertyType wcfPropertyType = this.GetWcfPropertyType(propDesc.PropertyType);
      switch (wcfPropertyType)
      {
        case WcfPropertyType.Standard:
          DefaultValueAttribute defaultValueAttribute = propDesc.Attributes.OfType<DefaultValueAttribute>().FirstOrDefault<DefaultValueAttribute>();
          if (propDesc.PropertyType != typeof (bool) && defaultValueAttribute == null && propDesc.CanResetValue(component))
            propDesc.ResetValue(component);
          propDesc.SetValue(component, html);
          break;
        case WcfPropertyType.GenericCollection:
          string name1 = wcfPropertyType == WcfPropertyType.GenericCollection ? "ClearGenericCollection" : "ClearGenericDictionary";
          string name2 = wcfPropertyType == WcfPropertyType.GenericCollection ? "PopulateGenericCollection" : "PopulateGenericDictionary";
          object obj1 = propDesc.GetValue(component);
          typeof (WcfPropertyManager).GetMethod(name1).MakeGenericMethod(propDesc.PropertyType.GetGenericArguments()).Invoke((object) this, new object[1]
          {
            obj1
          });
          if (html == null)
            break;
          typeof (WcfPropertyManager).GetMethod(name2).MakeGenericMethod(propDesc.PropertyType.GetGenericArguments()).Invoke((object) this, new object[2]
          {
            obj1,
            html
          });
          break;
        case WcfPropertyType.IList:
          if (typeof (Array).IsAssignableFrom(propDesc.PropertyType))
          {
            propDesc.SetValue(component, html);
            break;
          }
          IList list = (IList) propDesc.GetValue(component);
          list.Clear();
          foreach (object obj2 in (IEnumerable) html)
            list.Add(obj2);
          propDesc.SetValue(component, (object) list);
          break;
        case WcfPropertyType.Dictionary:
          IDictionary dictionary = (IDictionary) propDesc.GetValue(component);
          dictionary.Clear();
          IDictionaryEnumerator enumerator = (IDictionaryEnumerator) ((IEnumerable) html).GetEnumerator();
          while (enumerator.MoveNext())
            dictionary.Add(enumerator.Key, enumerator.Value);
          break;
      }
    }

    /// <summary>
    /// Reads the passed <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" /> and sets the value on of the designated property
    /// on the actual instance passed as the second argument.
    /// </summary>
    /// <param name="wcfProperty">
    /// An instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" />.
    /// </param>
    /// <param name="instance">
    /// An instance on which the value property ought to be set.
    /// </param>
    public virtual void SetProperty(WcfControlProperty wcfProperty, object rootInstance) => this.SetProperty(wcfProperty, rootInstance, (ControlData) null);

    internal static bool IsEditableProperty(PropertyDescriptor propDesc, ControlData controlData)
    {
      if (controlData is IFormControl formControl)
      {
        if (formControl.Published)
        {
          try
          {
            if (typeof (IMetaField).IsAssignableFrom(propDesc.ComponentType))
            {
              if (!(propDesc.Name == LinqHelper.MemberName<IMetaField>((Expression<Func<IMetaField, object>>) (x => x.FieldName))))
              {
                if (!(propDesc.Name == LinqHelper.MemberName<IMetaField>((Expression<Func<IMetaField, object>>) (x => x.ColumnName))))
                  goto label_7;
              }
              return false;
            }
          }
          catch (NotImplementedException ex)
          {
          }
        }
      }
label_7:
      return true;
    }

    /// <summary>
    /// Some properties has specific functions and cannot be edited in page templates so we prefer not to show them in the UI
    /// and skip adding them to the list.
    /// </summary>
    /// <param name="propDesc">
    /// An instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the property.
    /// </param>
    /// <param name="controlData">
    /// An instance of the <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> which requests the property
    /// and we have to check if it is available for editing on that control.
    /// </param>
    internal static bool SkipProperty(PropertyDescriptor propDesc, ControlData controlData)
    {
      if (controlData is TemplateDraftControl)
      {
        PropertyPersistenceAttribute persistenceAttribute = propDesc.Attributes.OfType<PropertyPersistenceAttribute>().FirstOrDefault<PropertyPersistenceAttribute>();
        if (persistenceAttribute != null)
          return !string.IsNullOrEmpty(persistenceAttribute.PagePropertyName);
      }
      return false;
    }

    internal static void SetValue(object component, PropertyDescriptor desc, string value)
    {
      if (desc == null)
        return;
      TypeConverter converter = desc.Converter;
      if (!converter.CanConvertFrom(typeof (string)))
        return;
      desc.SetValue(component, converter.ConvertFromString(value));
    }

    /// <summary>
    /// Populates the child properties for the given property descriptor up to specified
    /// depth (all of them if depth is -1).
    /// </summary>
    /// <param name="instance">An instance of the object from which the value should be read.</param>
    /// <param name="list">The list to which the properties should be added.</param>
    /// <param name="propertyDescriptorHierarchy">Collection of PropertyDescriptors where each item is a parent of the next item in the collection.</param>
    /// <param name="controlData">The instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object which holds all the persisted properties for the control.</param>
    /// <param name="depth">The depth up to which child properties should be recursively populated.</param>
    /// <param name="culture">Culture used to populate the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MultilingualPropertyAttribute">multilingual</see> properties</param>
    protected internal virtual void AddPropertiesRecursively(
      object instance,
      IList<WcfControlProperty> list,
      List<PropertyDescriptor> propertyDescriptorHierarchy,
      ControlData controlData,
      int depth,
      CultureInfo culture = null)
    {
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      PropertyDescriptor propertyDescriptor1 = propertyDescriptorHierarchy != null ? propertyDescriptorHierarchy.Last<PropertyDescriptor>() : throw new ArgumentNullException("propertyDescriptor");
      if (WcfPropertyManager.SkipProperty(propertyDescriptor1, controlData))
        return;
      if (propertyDescriptor1.Name == "ControllerName")
        propertyDescriptor1.SetValue(instance, (object) controlData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ControllerName" && p.Value != null)).First<ControlProperty>().Value);
      if (!propertyDescriptor1.IsBrowsable)
        return;
      try
      {
        list.Add(this.CreateWcfControlProperty(propertyDescriptorHierarchy, instance, controlData, culture));
      }
      catch (Exception ex)
      {
      }
      if (depth == 0 || !propertyDescriptor1.GetConverter().GetPropertiesSupported())
        return;
      object instance1 = propertyDescriptor1.GetValue(instance);
      PropertyDescriptorCollection childProperties = this.GetChildProperties(propertyDescriptor1, instance1);
      int depth1 = depth < 0 ? depth : depth - 1;
      foreach (PropertyDescriptor propertyDescriptor2 in childProperties)
        this.AddPropertiesRecursively(instance1, list, new List<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) propertyDescriptorHierarchy)
        {
          propertyDescriptor2
        }, controlData, depth1, culture);
    }

    /// <summary>
    /// Gets the property descriptor (using TypeDescriptors) for the given property path on the
    /// specified object type.
    /// </summary>
    /// <param name="type">
    /// Type of the object on which the property descriptor should be found.
    /// </param>
    /// <param name="propertyPath">Path of the property.</param>
    /// <returns>
    /// An instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the specified property on the given type.
    /// </returns>
    protected internal virtual PropertyDescriptor GetPropertyDescriptor(
      Type type,
      string propertyPath)
    {
      IList<WcfPropertyManager.PropertyChunk> chunks = !string.IsNullOrEmpty(propertyPath) ? this.GetPropertyChunks(propertyPath) : throw new ArgumentNullException(nameof (propertyPath));
      return this.GetPropertyDescriptor(TypeDescriptor.GetProperties(type), chunks);
    }

    protected internal virtual List<PropertyDescriptor> GetPropertyDescriptorHierarchy(
      PropertyDescriptorCollection properties,
      string propertyPath)
    {
      IList<WcfPropertyManager.PropertyChunk> propertyChunks = this.GetPropertyChunks(propertyPath);
      List<PropertyDescriptor> descriptorHierarchy = new List<PropertyDescriptor>();
      while (propertyChunks.Count > 0)
      {
        descriptorHierarchy.Add(this.GetPropertyDescriptor(properties, propertyChunks));
        propertyChunks.Remove(propertyChunks.Last<WcfPropertyManager.PropertyChunk>());
      }
      descriptorHierarchy.Reverse();
      return descriptorHierarchy;
    }

    /// <summary>
    /// Gets the property descriptor (using TypeDescriptors) based on the property chunks and specified type.
    /// </summary>
    /// <param name="type">
    /// Type of the object on which the property descriptor should be found.
    /// </param>
    /// <param name="chunks">
    /// Collection of property chunks used to navigate the object graph.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the specified property on the given type.
    /// </returns>
    protected internal virtual PropertyDescriptor GetPropertyDescriptor(
      PropertyDescriptorCollection properties,
      IList<WcfPropertyManager.PropertyChunk> chunks)
    {
      if (chunks == null)
        throw new ArgumentNullException(nameof (chunks));
      if (chunks.Count == 0)
        throw new ArgumentException("In order to find a property descriptor at least one property chunk must be present in the collection of property chunks.");
      PropertyDescriptor propertyDescriptor1 = (PropertyDescriptor) null;
      object component = this.objectInstance;
      PropertyDescriptorCollection descriptorCollection = (PropertyDescriptorCollection) null;
      foreach (WcfPropertyManager.PropertyChunk chunk in (IEnumerable<WcfPropertyManager.PropertyChunk>) chunks)
      {
        bool flag = false;
        if (propertyDescriptor1 == null)
        {
          foreach (PropertyDescriptor property in properties)
          {
            if (property.Name == chunk.PropertyName)
            {
              propertyDescriptor1 = property;
              flag = true;
              object obj = propertyDescriptor1.GetValue(component);
              if (propertyDescriptor1.Converter.GetPropertiesSupported())
                descriptorCollection = this.GetChildProperties(propertyDescriptor1, obj);
              component = obj;
              break;
            }
          }
        }
        else
        {
          foreach (PropertyDescriptor propertyDescriptor2 in descriptorCollection)
          {
            if (propertyDescriptor2.Name == chunk.PropertyName)
            {
              propertyDescriptor1 = propertyDescriptor2;
              flag = true;
              object obj = propertyDescriptor1.GetValue(component);
              descriptorCollection = this.GetChildProperties(propertyDescriptor1, obj);
              component = obj;
              break;
            }
          }
        }
        if (!flag)
          throw new InvalidOperationException(string.Format("Property '{0}' cannot be found.", (object) chunk.PropertyName));
      }
      return propertyDescriptor1;
    }

    internal ControlProperty GetControlPropertyOrDefault(
      IEnumerable<ControlProperty> properties,
      string controlPropertyPath)
    {
      IList<WcfPropertyManager.PropertyChunk> propertyChunks = this.GetPropertyChunks(controlPropertyPath);
      if (propertyChunks == null)
        throw new ArgumentNullException("chunks");
      if (propertyChunks.Count == 0)
        throw new ArgumentException("In order to find a property descriptor at least one property chunk must be present in the collection of property chunks.");
      ControlProperty propertyOrDefault = (ControlProperty) null;
      foreach (WcfPropertyManager.PropertyChunk propertyChunk in (IEnumerable<WcfPropertyManager.PropertyChunk>) propertyChunks)
      {
        bool flag = false;
        if (propertyOrDefault == null)
        {
          foreach (ControlProperty property in properties)
          {
            if (property.Name == propertyChunk.PropertyName)
            {
              propertyOrDefault = property;
              flag = true;
              break;
            }
          }
        }
        else
        {
          foreach (ControlProperty childProperty in (IEnumerable<ControlProperty>) propertyOrDefault.ChildProperties)
          {
            if (childProperty.Name == propertyChunk.PropertyName)
            {
              propertyOrDefault = childProperty;
              flag = true;
              break;
            }
          }
        }
        if (!flag)
          return (ControlProperty) null;
      }
      return propertyOrDefault;
    }

    /// <summary>
    /// Examines the type of property and returns the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.WcfPropertyType" /> used
    /// to determine the proper implementation of getter/setter functionality for the
    /// property.
    /// </summary>
    /// <param name="propertyType">Actual type of the property.</param>
    /// <returns>One of the values of WcfProperty type.</returns>
    protected internal virtual WcfPropertyType GetWcfPropertyType(Type propertyType)
    {
      if (propertyType == (Type) null)
        throw new ArgumentNullException(nameof (propertyType));
      if (typeof (IList).IsAssignableFrom(propertyType))
        return WcfPropertyType.IList;
      if (typeof (IDictionary).IsAssignableFrom(propertyType))
        return WcfPropertyType.Dictionary;
      return ((IEnumerable<Type>) propertyType.GetInterfaces()).Any<Type>((Func<Type, bool>) (x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (ICollection<>))) || ((IEnumerable<Type>) propertyType.GetInterfaces()).Any<Type>((Func<Type, bool>) (x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (IDictionary<,>))) ? WcfPropertyType.GenericCollection : WcfPropertyType.Standard;
    }

    /// <summary>Clears the collection of items</summary>
    /// <typeparam name="T">Type of items that constitute the generic collection.</typeparam>
    /// <param name="collection">The instance of the collection that ought to be cleared.</param>
    public virtual void ClearGenericCollection<T>(object collection)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      if (!(collection is ICollection<T> objs))
        throw new ArgumentException("The collection argument passed to this method must be assignable to the interface ICollection<T>.");
      objs.Clear();
    }

    /// <summary>Populates the given collection with the new values.</summary>
    /// <typeparam name="T">Type of items that constitue the generic collection.</typeparam>
    /// <param name="collection">The instance of the collection that ought to be populated.</param>
    /// <param name="newValues">
    /// IEnumerable representing the new values that ought to be populated in the collection object.
    /// </param>
    public virtual void PopulateGenericCollection<T>(object collection, IEnumerable newValues)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      if (newValues == null)
        throw new ArgumentNullException(nameof (newValues));
      if (!(collection is ICollection<T> objs))
        throw new ArgumentException("The collection argument passed to this method must be assignable to the interface ICollection<T>.");
      foreach (T newValue in newValues)
        objs.Add(newValue);
    }

    /// <summary>
    /// Parses the property path into the ordered collection of property chunks.
    /// </summary>
    /// <param name="propertyPath">Property path that should be parsed.</param>
    /// <returns>
    /// Ordered collection of property chunk objects used to find a given property
    /// on the type hierarchy.
    /// </returns>
    protected internal virtual IList<WcfPropertyManager.PropertyChunk> GetPropertyChunks(
      string propertyPath)
    {
      if (string.IsNullOrEmpty(propertyPath))
        throw new ArgumentNullException(nameof (propertyPath));
      List<WcfPropertyManager.PropertyChunk> propertyChunks = new List<WcfPropertyManager.PropertyChunk>();
      if (propertyPath.IndexOf(this.LevelDelimiter) < 0)
      {
        propertyChunks.Add(new WcfPropertyManager.PropertyChunk()
        {
          PropertyName = propertyPath
        });
      }
      else
      {
        string str1 = propertyPath;
        char[] chArray = new char[1]{ this.LevelDelimiter };
        foreach (string str2 in str1.Split(chArray))
        {
          if (!string.IsNullOrEmpty(str2))
            propertyChunks.Add(new WcfPropertyManager.PropertyChunk()
            {
              PropertyName = str2
            });
        }
      }
      return (IList<WcfPropertyManager.PropertyChunk>) propertyChunks;
    }

    /// <summary>Builds the property path for the given property.</summary>
    /// <param name="instanceType">
    /// Instance that represents the root of the path.
    /// </param>
    /// <param name="propertyDescriptorHierarchy">
    /// Collection of PropertyDescriptors where each item is a parent of the next item in the collection.
    /// </param>
    /// <returns>
    /// String which represents the property path for the given property.
    /// </returns>
    protected internal virtual string BuildPropertyPath(
      Type instanceType,
      ICollection<PropertyDescriptor> propertyDescriptorHierarchy)
    {
      if (propertyDescriptorHierarchy == null)
        throw new ArgumentNullException();
      string str = string.Empty;
      foreach (PropertyDescriptor propertyDescriptor in (IEnumerable<PropertyDescriptor>) propertyDescriptorHierarchy)
        str = str + (object) this.LevelDelimiter + propertyDescriptor.Name;
      return str;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" /> from the property descriptor
    /// and assigns the value from the instance.
    /// </summary>
    /// <param name="propertyDescriptorHierachy">The property descriptor hierachy.</param>
    /// <param name="instance">An instance from which the property value will be obtained.</param>
    /// <param name="controlData">The control data which represents the persisted properties of the control.</param>
    /// <param name="culture">Culture used to populate the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MultilingualPropertyAttribute">multilingual</see> properties</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" /> representing the given property of the given instance.
    /// </returns>
    protected internal virtual WcfControlProperty CreateWcfControlProperty(
      List<PropertyDescriptor> propertyDescriptorHierachy,
      object instance,
      ControlData controlData,
      CultureInfo culture = null)
    {
      if (propertyDescriptorHierachy == null)
        throw new ArgumentNullException("propertyDescriptorHierarchy");
      ControlProperty persistedProperty = this.GetPersistedProperty(propertyDescriptorHierachy, (ObjectData) controlData, culture);
      PropertyDescriptor propertyDescriptor = propertyDescriptorHierachy.Last<PropertyDescriptor>();
      WcfControlProperty property1 = new WcfControlProperty();
      property1.CategoryName = propertyDescriptor.Category;
      string str = propertyDescriptor.Category.Replace(" ", "");
      property1.CategoryNameSafe = str;
      property1.ItemTypeName = propertyDescriptor.PropertyType.FullName;
      property1.ClientPropertyTypeName = this.PrepareTypeName(propertyDescriptor.PropertyType);
      property1.OriginalPropertyName = propertyDescriptor.Name;
      property1.PropertyName = propertyDescriptor.DisplayName;
      property1.PropertyPath = this.BuildPropertyPath((Type) null, (ICollection<PropertyDescriptor>) propertyDescriptorHierachy);
      string html = (string) null;
      if (persistedProperty != null && persistedProperty.Value != null && propertyDescriptor.Converter.CanConvertFrom(typeof (string)))
        html = persistedProperty.Value;
      else if (propertyDescriptor.Converter.GetPropertiesSupported())
      {
        if (property1.PropertyName == "Settings")
        {
          property1.NeedsEditor = false;
          property1.IsProxy = true;
        }
        else
          property1.NeedsEditor = true;
      }
      else if (propertyDescriptor.Converter.CanConvertTo(typeof (string)))
      {
        object obj1 = (object) null;
        DefaultValueAttribute defaultValueAttribute = propertyDescriptor.Attributes.OfType<DefaultValueAttribute>().FirstOrDefault<DefaultValueAttribute>();
        if (instance != null)
          obj1 = propertyDescriptor.GetValue(instance);
        object obj2 = (object) null;
        if (propertyDescriptor.PropertyType.IsValueType)
          obj2 = Activator.CreateInstance(propertyDescriptor.PropertyType);
        if ((obj1 == null || obj1.Equals(obj2)) && defaultValueAttribute != null)
          obj1 = WidgetRendererResolver.GetDefaultValueResolver().GetDefaultValue(propertyDescriptor, false);
        PropertyPersistenceAttribute attribute = (PropertyPersistenceAttribute) propertyDescriptor.Attributes[typeof (PropertyPersistenceAttribute)];
        if (attribute != null && attribute.PersistInPage && controlData is PageDraftControl pageDraftControl)
        {
          PropertyDescriptor property2 = TypeDescriptor.GetProperties((object) pageDraftControl.Page)[attribute.PagePropertyName];
          if (property2 != null)
            obj1 = property2.GetValue((object) pageDraftControl.Page);
        }
        if (obj1 == null)
        {
          html = string.Empty;
        }
        else
        {
          culture = culture ?? SystemManager.CurrentContext.Culture;
          html = propertyDescriptor.Converter.ConvertTo((ITypeDescriptorContext) null, culture, obj1, typeof (string)) as string;
          if (propertyDescriptor.PropertyType == typeof (MixedContentContext))
          {
            try
            {
              html = JsonConvert.SerializeObject(obj1);
            }
            catch (Exception ex)
            {
            }
          }
        }
        property1.OriginalPropertyValue = obj1;
      }
      if (propertyDescriptor.Attributes.OfType<DynamicLinksContainerAttribute>().FirstOrDefault<DynamicLinksContainerAttribute>() != null)
      {
        property1.SupportsDynamicLinks = true;
        if (html != null && html.Length > 0 && this.ResolveDynamicLinks)
          html = LinkParser.ResolveLinks(html, new GetItemUrl(DynamicLinksParser.GetContentUrl), (ResolveUrl) null, true);
      }
      if (html != null)
        property1.PropertyValue = html;
      property1.PropertyId = persistedProperty != null ? persistedProperty.Id : Guid.Empty;
      property1.InCategoryOrdinal = this.DetermineInCategoryOrdinal(property1);
      property1.TypeEditor = this.GetTypeEditor(propertyDescriptor);
      property1.IsEditable = WcfPropertyManager.IsEditableProperty(propertyDescriptor, controlData);
      return property1;
    }

    private string PrepareTypeName(Type propertyType)
    {
      string str1 = propertyType.FullName;
      if (propertyType.IsNullable())
      {
        string str2 = str1.Substring(str1.IndexOf("[[") + 2);
        str1 = str2.Substring(0, str2.IndexOf(",")).Trim();
      }
      return str1;
    }

    /// <summary>Gets the persisted value for the given property.</summary>
    /// <param name="propertyDescriptorHierarchy">
    /// The property descriptor hierarchy.
    /// </param>
    /// <param name="controlData">
    /// The control data which represents the persisted properties of the control.
    /// </param>
    /// <returns>The value of the property that was persisted; if any.</returns>
    protected internal virtual ControlProperty GetPersistedProperty(
      List<PropertyDescriptor> propertyDescriptorHierarchy,
      ObjectData controlData,
      CultureInfo culture = null)
    {
      if (propertyDescriptorHierarchy == null)
        throw new ArgumentNullException(nameof (propertyDescriptorHierarchy));
      if (controlData == null)
        return (ControlProperty) null;
      ControlProperty persistedProperty = (ControlProperty) null;
      foreach (PropertyDescriptor propertyDescriptor1 in propertyDescriptorHierarchy)
      {
        PropertyDescriptor propertyDescriptor = propertyDescriptor1;
        persistedProperty = persistedProperty != null ? persistedProperty.ChildProperties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == propertyDescriptor.Name)).SingleOrDefault<ControlProperty>() : controlData.GetProperties(culture).Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == propertyDescriptor.Name)).SingleOrDefault<ControlProperty>();
        if (persistedProperty != null && persistedProperty.ListItems != null)
        {
          foreach (ObjectData listItem in (IEnumerable<ObjectData>) persistedProperty.ListItems)
          {
            persistedProperty = this.GetPersistedProperty(new List<PropertyDescriptor>()
            {
              propertyDescriptorHierarchy.Last<PropertyDescriptor>()
            }, listItem, culture);
            if (persistedProperty != null)
              return persistedProperty;
          }
          if (persistedProperty != null)
            return persistedProperty;
        }
      }
      return persistedProperty;
    }

    /// <summary>
    /// Converts the string value of the property to the value of the actual property.
    /// </summary>
    /// <param name="propDesc">The prop descriptor that describes the property.</param>
    /// <param name="serializedValue">The serialized value.</param>
    /// <returns>An object representing the value converted to the type of the property.</returns>
    protected internal virtual object ConvertValue(
      PropertyDescriptor propDesc,
      string serializedValue)
    {
      if (propDesc == null)
        throw new ArgumentNullException(nameof (propDesc));
      if (string.IsNullOrEmpty(serializedValue))
        return (object) null;
      TypeConverter converter = propDesc.Converter;
      if (converter.CanConvertFrom(typeof (string)))
        return converter.ConvertFromString(serializedValue);
      if (serializedValue != null)
      {
        if (propDesc.PropertyType == typeof (MixedContentContext))
        {
          try
          {
            return JsonConvert.DeserializeObject(serializedValue, propDesc.PropertyType);
          }
          catch (Exception ex)
          {
          }
        }
      }
      return (object) null;
    }

    /// <summary>
    /// Determines a special, UX designated order of properties inside of a category.
    /// </summary>
    /// <param name="propertyType">Type of the property.</param>
    /// <returns>
    /// Ordinal of the property; the lower the number the higher the property is placed inside of a category.
    /// </returns>
    protected internal virtual int DetermineInCategoryOrdinal(WcfControlProperty property)
    {
      property.InCategoryOrdinal = !(property.ItemTypeName == typeof (bool).FullName) ? (!(property.ItemTypeName == typeof (DateTime).FullName) ? (property.ItemTypeName == typeof (int).FullName || property.ItemTypeName == typeof (double).FullName || property.ItemTypeName == typeof (float).FullName || property.ItemTypeName == typeof (long).FullName || property.ItemTypeName == typeof (short).FullName || property.ItemTypeName == typeof (uint).FullName || property.ItemTypeName == typeof (ulong).FullName || property.ItemTypeName == typeof (ushort).FullName ? 2 : (!property.NeedsEditor ? 3 : 4)) : 1) : 0;
      return 5;
    }

    /// <summary>
    /// Gets the type editor to be used for editing the property.
    /// </summary>
    /// <param name="desc">Property descriptor for the given property.</param>
    /// <returns>
    /// Full name of the type that should be used to edit the property; empty string if no type editor has been defined.
    /// </returns>
    protected internal virtual string GetTypeEditor(PropertyDescriptor desc) => desc.Attributes.OfType<TypeEditorAttribute>().FirstOrDefault<TypeEditorAttribute>()?.EditorType;

    private PropertyDescriptorCollection GetChildProperties(
      PropertyDescriptor propertyDescriptor,
      object value)
    {
      TypeConverter converter = propertyDescriptor.Converter;
      return !typeof (IComplexConverter).IsAssignableFrom(converter.GetType()) ? (Attribute.GetCustomAttribute((MemberInfo) propertyDescriptor.PropertyType, typeof (ReflectInheritedPropertiesAttribute)) != null || value != null && Attribute.GetCustomAttribute((MemberInfo) value.GetType(), typeof (ReflectInheritedPropertiesAttribute)) != null || propertyDescriptor.Attributes.OfType<ReflectInheritedPropertiesAttribute>().Any<ReflectInheritedPropertiesAttribute>() ? propertyDescriptor.GetChildProperties(value) : propertyDescriptor.GetChildProperties()) : converter.GetProperties(value);
    }

    internal ControlData GetOverridingControl(Guid controlDataId, Guid pageId)
    {
      PageManager manager = PageManager.GetManager();
      PageData pageData = manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (page => page.Id == pageId)).FirstOrDefault<PageData>();
      if (pageData != null)
        return this.GetOverriddenControlForPage(controlDataId, pageData);
      PageDraft pageDraft = manager.GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (page => page.Id == pageId)).FirstOrDefault<PageDraft>();
      if (pageDraft != null)
        return this.GetOverriddenControlForPageDraft(controlDataId, pageDraft);
      PageTemplate parentTemplate = manager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (template => template.Id == pageId)).FirstOrDefault<PageTemplate>();
      if (parentTemplate != null)
        return this.GetOverriddenControlForTemplate(controlDataId, parentTemplate);
      TemplateDraft templateDraft = manager.GetDrafts<TemplateDraft>().Where<TemplateDraft>((Expression<Func<TemplateDraft, bool>>) (template => template.Id == pageId)).FirstOrDefault<TemplateDraft>();
      return templateDraft != null ? this.GetOverriddenControlForTemplateDraft(controlDataId, templateDraft) : (ControlData) null;
    }

    private ControlData GetOverriddenControlForPage(
      Guid controlDataId,
      PageData pageData)
    {
      ControlData overriddenControlForPage = (ControlData) PageManager.GetManager().GetControls<PageControl>().Where<PageControl>((Expression<Func<PageControl, bool>>) (control => control.Page != default (object) && control.Page.Id == pageData.Id && control.BaseControlId == controlDataId)).FirstOrDefault<PageControl>();
      if (overriddenControlForPage == null && pageData.Template != null && pageData.Template.Id != Guid.Empty)
        overriddenControlForPage = this.GetOverridingControl(controlDataId, pageData.Template.Id);
      return overriddenControlForPage;
    }

    private ControlData GetOverriddenControlForPageDraft(
      Guid controlDataId,
      PageDraft pageDraft)
    {
      ControlData controlForPageDraft = (ControlData) PageManager.GetManager().GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (control => control.Page != default (object) && control.Page.Id == pageDraft.Id && control.BaseControlId == controlDataId)).FirstOrDefault<PageDraftControl>();
      if (controlForPageDraft == null)
      {
        Guid templateId = pageDraft.TemplateId;
        if (pageDraft.TemplateId != Guid.Empty)
          controlForPageDraft = this.GetOverridingControl(controlDataId, pageDraft.TemplateId);
      }
      return controlForPageDraft;
    }

    private ControlData GetOverriddenControlForTemplateDraft(
      Guid controlDataId,
      TemplateDraft templateDraft)
    {
      ControlData forTemplateDraft = (ControlData) PageManager.GetManager().GetControls<TemplateDraftControl>().Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (control => control.Page != default (object) && control.Page.Id == templateDraft.Id && control.BaseControlId == controlDataId)).FirstOrDefault<TemplateDraftControl>();
      if (forTemplateDraft == null && templateDraft.ParentTemplate != null)
        forTemplateDraft = this.GetOverridingControl(controlDataId, templateDraft.ParentId);
      return forTemplateDraft;
    }

    private ControlData GetOverriddenControlForTemplate(
      Guid controlDataId,
      PageTemplate parentTemplate)
    {
      ControlData controlForTemplate = (ControlData) PageManager.GetManager().GetControls<TemplateControl>().Where<TemplateControl>((Expression<Func<TemplateControl, bool>>) (control => control.Page != default (object) && control.Page.Id == parentTemplate.Id && control.BaseControlId == controlDataId)).FirstOrDefault<TemplateControl>();
      if (controlForTemplate == null && parentTemplate.ParentTemplate != null)
        controlForTemplate = this.GetOverridingControl(controlDataId, parentTemplate.ParentTemplate.Id);
      return controlForTemplate;
    }

    /// <summary>
    /// Property chunk is a type that represents a single property inside of a path
    /// </summary>
    protected internal struct PropertyChunk
    {
      /// <summary>Name of the property.</summary>
      public string PropertyName;
    }
  }
}
