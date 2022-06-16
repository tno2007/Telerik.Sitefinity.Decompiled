// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.WrapperObject
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Translators;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// Dynamic runtime behavior object used for transforming source item to destination item based on mapping settings.
  /// </summary>
  [TypeDescriptionProvider(typeof (WrapperObjectTypeDescriptorProvider))]
  public class WrapperObject : DynamicObject, IPublishingObject
  {
    private Dictionary<string, object> properties = new Dictionary<string, object>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.WrapperObject" /> class.
    /// </summary>
    /// <param name="theInstance">The instance.</param>
    public WrapperObject(object theInstance) => this.WrappedObject = theInstance;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.WrapperObject" /> class.
    /// </summary>
    /// <param name="setings">The settings.</param>
    /// <param name="theInstance">The instance.</param>
    public WrapperObject(PipeSettings settings, object theInstance)
      : this(theInstance)
    {
      this.MappingSettings = settings.Mappings;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.WrapperObject" /> class.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <param name="theInstance">The instance.</param>
    /// <param name="cultureName">Name of the culture.</param>
    public WrapperObject(PipeSettings settings, object theInstance, string cultureName)
      : this(settings, theInstance)
    {
      this.Language = cultureName;
    }

    /// <summary>Gets or sets the mapping settings.</summary>
    /// <value>The mapping settings.</value>
    public virtual MappingSettings MappingSettings { get; set; }

    /// <summary>
    /// Gets or sets the wrapped object(reference to source object)
    /// </summary>
    /// <value>The wrapped object.</value>
    public virtual object WrappedObject { get; set; }

    /// <summary>Gets or sets the language.</summary>
    /// <value>The language.</value>
    public virtual string Language { get; set; }

    /// <summary>Gets the additional properties of the wrapper object.</summary>
    /// <value>The additional properties.</value>
    public Dictionary<string, object> AdditionalProperties => this.properties;

    /// <inheritdoc />
    public override bool TryGetMember(GetMemberBinder binder, out object result) => this.TryGetProperty(binder.Name, out result);

    /// <inheritdoc />
    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
      this.SetOrAddProperty(binder.Name, value);
      return true;
    }

    /// <inheritdoc />
    public override IEnumerable<string> GetDynamicMemberNames() => TypeDescriptor.GetProperties((object) this).OfType<PropertyDescriptor>().Select<PropertyDescriptor, string>((Func<PropertyDescriptor, string>) (p => p.Name));

    /// <summary>
    /// Returns true if the WrapperObject contains property with this name and false otherwise.
    /// </summary>
    /// <param name="name">The name of the property to check for</param>
    public bool HasProperty(string name) => this.properties.ContainsKey(name) || TypeDescriptor.GetProperties(this.WrappedObject).Find(name, true) != null;

    /// <summary>Gets all properties of the WrapperObject.</summary>
    public Dictionary<string, object> GetAllProperties()
    {
      Dictionary<string, object> allProperties = new Dictionary<string, object>((IDictionary<string, object>) this.properties);
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this.WrappedObject))
        allProperties.Add(property.Name, property.GetValue((object) this));
      return allProperties;
    }

    /// <summary>Gets the value of a property.</summary>
    /// <param name="name">The property name.</param>
    /// <exception cref="T:System.MemberAccessException">The property does not exist.</exception>
    public object GetProperty(string name)
    {
      object property;
      if (!this.TryGetProperty(name, out property))
        throw new MemberAccessException(string.Format("This object does not have a property named '{0}'.", (object) name));
      return property;
    }

    internal virtual object GetProperty(string name, CultureInfo culture)
    {
      WrapperObject.PropertyGetter getter = new WrapperObject.PropertyGetter(this)
      {
        Culture = culture
      };
      object property;
      if (!this.TryGetProperty(name, getter, out property))
        throw new MemberAccessException(string.Format("This object does not have a property named '{0}'.", (object) name));
      return property;
    }

    internal Lstring GetLStringProperty(string name)
    {
      WrapperObject.PropertyGetter getter = new WrapperObject.PropertyGetter(this)
      {
        ReturnLString = true
      };
      object lstringProperty;
      if (!this.TryGetProperty(name, getter, out lstringProperty))
        throw new MemberAccessException(string.Format("This object does not have a property named '{0}'.", (object) name));
      return lstringProperty as Lstring;
    }

    /// <summary>Gets the value of a property.</summary>
    /// <typeparam name="T">The expected value type.</typeparam>
    /// <param name="name">The property name.</param>
    /// <exception cref="T:System.MemberAccessException">The property does not exist.</exception>
    /// <exception cref="T:System.InvalidCastException">The property value cannot be cast to <typeparamref name="T" />.</exception>
    public T GetProperty<T>(string name)
    {
      object property;
      if (!this.TryGetProperty(name, out property))
        throw new MemberAccessException(string.Format("This object does not have a property named '{0}'.", (object) name));
      return (T) property;
    }

    public object GetPropertyOrNull(string name)
    {
      object obj;
      return this.TryGetProperty(name, out obj) ? obj : (object) null;
    }

    /// <summary>
    /// Tries to get the value of a property and if the property does not exists, returns the default value of <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The expected value type.</typeparam>
    /// <param name="name">The property name.</param>
    /// <exception cref="T:System.InvalidCastException">The property value cannot be cast to <typeparamref name="T" />.</exception>
    public T GetPropertyOrDefault<T>(string name)
    {
      object obj;
      return this.TryGetProperty(name, out obj) ? (T) obj : default (T);
    }

    /// <summary>
    /// Tries to get the value of a property and if the property does not exists, returns <paramref name="defaultValue" />.
    /// </summary>
    /// <typeparam name="T">The expected value type.</typeparam>
    /// <param name="name">The property name.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <exception cref="T:System.InvalidCastException">The property value cannot be cast to <typeparamref name="T" />.</exception>
    public T GetPropertyOrDefault<T>(string name, T defaultValue)
    {
      object obj;
      return this.TryGetProperty(name, out obj) ? (T) obj : defaultValue;
    }

    /// <summary>Attempts to get a strongly-typed value of a property.</summary>
    /// <typeparam name="T">The expected value type.</typeparam>
    /// <param name="name">The property name.</param>
    /// <param name="value">The obtained property value.</param>
    /// <exception cref="T:System.InvalidCastException">The property value cannot be cast to <typeparamref name="T" />.</exception>
    /// <returns><c>true</c> if the property exists; <c>false</c> - otherwise.</returns>
    public bool TryGetProperty<T>(string name, out T value)
    {
      object obj;
      if (!this.TryGetProperty(name, out obj))
      {
        value = default (T);
        return false;
      }
      value = (T) obj;
      return true;
    }

    /// <summary>Attempts to get the value of a property.</summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The obtained property value.</param>
    /// <returns><c>true</c> if the property exists; <c>false</c> - otherwise.</returns>
    public bool TryGetProperty(string name, out object value) => this.TryGetProperty(name, (WrapperObject.PropertyGetter) null, out value);

    internal bool TryGetProperty(
      string name,
      WrapperObject.PropertyGetter getter,
      out object value)
    {
      if (this.AdditionalProperties.TryGetValue(name, out value))
        return true;
      getter = getter ?? new WrapperObject.PropertyGetter(this);
      return this.MappingSettings != null && this.TryMap(name, new Func<string, object>(getter.GetOrNull), out value) || getter.TryGet(name, out value);
    }

    private bool TryMap(string propertyName, Func<string, object> getProp, out object value)
    {
      foreach (Mapping mapping in (IEnumerable<Mapping>) this.MappingSettings.Mappings)
      {
        if (mapping.DestinationPropertyName == propertyName)
        {
          int length = mapping.SourcePropertyNames.Length;
          if (length > 0)
          {
            object[] sourceValues = new object[length];
            for (int index = 0; index < length; ++index)
            {
              string sourcePropertyName = mapping.SourcePropertyNames[index];
              sourceValues[index] = getProp(sourcePropertyName);
            }
            value = WrapperObject.ExecuteTranslatorChain(mapping, sourceValues);
            return true;
          }
        }
      }
      value = (object) null;
      return false;
    }

    private static object ExecuteTranslatorChain(Mapping mapping, object[] sourceValues)
    {
      object obj = (object) null;
      if (mapping.Translations.Count > 0)
      {
        object[] valuesToTranslate = sourceValues;
        foreach (PipeMappingTranslation translation in (IEnumerable<PipeMappingTranslation>) mapping.Translations)
        {
          obj = PipeTranslatorFactory.ResolveTranslator(translation.TranslatorName).Translate(valuesToTranslate, (IDictionary<string, string>) translation.GetSettings());
          valuesToTranslate = new object[1]{ obj };
        }
      }
      else
        obj = (sourceValues.Length == 1 ? (ITranslator) new TransparentTranslator() : (ITranslator) new ConcatenationTranslator()).Translate(sourceValues, (IDictionary<string, string>) null);
      return obj;
    }

    /// <summary>
    /// Adds a property to the WrapperObject. If the property already exists, changes its value to the new one.
    /// </summary>
    /// <param name="name">The name of the property to add</param>
    /// <param name="value">The value for the property</param>
    public void AddProperty(string name, object value)
    {
      if (this.properties.ContainsKey(name))
        throw new ArgumentException(string.Format("This object already contains property with name '{0}'.", (object) name));
      this.properties.Add(name, value);
    }

    /// <summary>Sets a property to the WrapperObject.</summary>
    /// <param name="name">The name of the property to set</param>
    /// <param name="value">The value for the property</param>
    public void SetProperty(string name, object value)
    {
      if (this.properties.ContainsKey(name))
        this.properties[name] = value;
      else
        (TypeDescriptor.GetProperties(this.WrappedObject).Find(name, true) ?? throw new ArgumentException(string.Format("This object does not contain property with name '{0}'.", (object) name))).SetValue(this.WrappedObject, value);
    }

    /// <summary>
    /// Sets or adds a property with the given name depending on whether the instance already has such property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    public void SetOrAddProperty(string name, object value)
    {
      if (this.HasProperty(name))
        this.SetProperty(name, value);
      else
        this.AddProperty(name, value);
    }

    protected internal class PropertyGetter
    {
      private WrapperObject obj;

      public CultureInfo Culture { get; set; }

      public bool ReturnLString { get; set; }

      public PropertyGetter(WrapperObject obj) => this.obj = obj;

      public object GetOrNull(string name)
      {
        object obj;
        return this.TryGet(name, out obj) ? obj : (object) null;
      }

      public bool TryGet(string name, out object value) => this.TryGet(this.obj, name, out value);

      public bool TryGet(WrapperObject obj, string name, out object value)
      {
        PropertyDescriptor propertyDescriptor1 = TypeDescriptor.GetProperties(obj.WrappedObject).Find(name, true);
        if (propertyDescriptor1 != null)
        {
          if (propertyDescriptor1 is LstringPropertyDescriptor propertyDescriptor2 && !this.ReturnLString && (this.Culture != null || !obj.Language.IsNullOrEmpty()))
          {
            this.Culture = this.Culture ?? CultureInfo.GetCultureInfo(obj.Language);
            value = (object) propertyDescriptor2.GetValue(obj.WrappedObject, this.Culture, false);
          }
          else
            value = propertyDescriptor1.GetValue(obj.WrappedObject);
          return true;
        }
        if (obj.WrappedObject is WrapperObject wrappedObject)
          return this.TryGet(wrappedObject, name, out value);
        value = (object) null;
        return false;
      }
    }
  }
}
