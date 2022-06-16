// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.RelatedDataWrapper
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

namespace Telerik.Sitefinity.RelatedData
{
  /// <summary>
  /// This class represents a dynamic object wrapper for related items in Sitefinity
  /// </summary>
  public class RelatedDataWrapper : DynamicObject
  {
    private Dictionary<string, object> properties = new Dictionary<string, object>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RelatedData.RelatedDataWrapper" /> class.
    /// </summary>
    /// <param name="theInstance">The instance.</param>
    public RelatedDataWrapper(object theInstance) => this.RelatedDataWrapperObject = theInstance;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RelatedData.RelatedDataWrapper" /> class.
    /// </summary>
    /// <param name="theInstance">The instance.</param>
    /// <param name="id">Object id</param>
    public RelatedDataWrapper(object theInstance, Guid id)
    {
      this.RelatedDataWrapperObject = theInstance;
      this.Id = id;
    }

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is RelatedDataWrapper && this.Id == (obj as RelatedDataWrapper).Id;

    /// <summary>Gets or sets the id of the item</summary>
    public virtual Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the wrapped object(reference to source object)
    /// </summary>
    public virtual object RelatedDataWrapperObject { get; set; }

    /// <summary>Gets or sets the language.</summary>
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
    /// Returns true if the object contains property with this name and false otherwise.
    /// </summary>
    /// <param name="name">The name of the property to check for</param>
    /// <returns>Boolean property indicating whether this property is found</returns>
    public bool HasProperty(string name) => this.properties.ContainsKey(name) || TypeDescriptor.GetProperties(this.RelatedDataWrapperObject).Find(name, true) != null;

    /// <summary>Gets all properties of the object.</summary>
    /// <returns>All properties</returns>
    public Dictionary<string, object> GetAllProperties()
    {
      Dictionary<string, object> allProperties = new Dictionary<string, object>((IDictionary<string, object>) this.properties);
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this.RelatedDataWrapperObject))
        allProperties.Add(property.Name, property.GetValue((object) this));
      return allProperties;
    }

    /// <summary>Gets the value of a property.</summary>
    /// <param name="name">The property name.</param>
    /// <exception cref="T:System.MemberAccessException">The property does not exist.</exception>
    /// <returns>The property</returns>
    public object GetProperty(string name)
    {
      object property;
      if (!this.TryGetProperty(name, out property))
        throw new MemberAccessException(string.Format("This object does not have a property named '{0}'.", (object) name));
      return property;
    }

    /// <summary>Gets the value of a property.</summary>
    /// <typeparam name="T">The expected value type.</typeparam>
    /// <param name="name">The property name.</param>
    /// <exception cref="T:System.MemberAccessException">The property does not exist.</exception>
    /// <exception cref="T:System.InvalidCastException">The property value cannot be cast to <typeparamref name="T" />.</exception>
    /// <returns>The property</returns>
    public T GetProperty<T>(string name)
    {
      object property;
      if (!this.TryGetProperty(name, out property))
        throw new MemberAccessException(string.Format("This object does not have a property named '{0}'.", (object) name));
      return (T) property;
    }

    /// <summary>Gets the value of a property.</summary>
    /// <param name="name">Name of the property</param>
    /// <returns>The object</returns>
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
    /// <returns>The property value or default value</returns>
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
    /// <returns>The property value or default value</returns>
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
    public bool TryGetProperty(string name, out object value) => this.TryGetProperty(name, (RelatedDataWrapper.PropertyGetter) null, out value);

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
        (TypeDescriptor.GetProperties(this.RelatedDataWrapperObject).Find(name, true) ?? throw new ArgumentException(string.Format("This object does not contain property with name '{0}'.", (object) name))).SetValue(this.RelatedDataWrapperObject, value);
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

    internal virtual object GetProperty(string name, CultureInfo culture)
    {
      RelatedDataWrapper.PropertyGetter getter = new RelatedDataWrapper.PropertyGetter(this)
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
      RelatedDataWrapper.PropertyGetter getter = new RelatedDataWrapper.PropertyGetter(this)
      {
        ReturnLString = true
      };
      object lstringProperty;
      if (!this.TryGetProperty(name, getter, out lstringProperty))
        throw new MemberAccessException(string.Format("This object does not have a property named '{0}'.", (object) name));
      return lstringProperty as Lstring;
    }

    internal bool TryGetProperty(
      string name,
      RelatedDataWrapper.PropertyGetter getter,
      out object value)
    {
      if (this.AdditionalProperties.TryGetValue(name, out value))
        return true;
      getter = getter ?? new RelatedDataWrapper.PropertyGetter(this);
      return getter.TryGet(name, out value);
    }

    /// <summary>Gets or sets the property getter</summary>
    protected internal class PropertyGetter
    {
      private RelatedDataWrapper obj;

      /// <summary>Gets or sets the culture</summary>
      public CultureInfo Culture { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether the string is returned
      /// </summary>
      public bool ReturnLString { get; set; }

      /// <summary>
      ///  Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RelatedData.RelatedDataWrapper.PropertyGetter" /> class.
      /// </summary>
      /// <param name="obj">The instance.</param>
      public PropertyGetter(RelatedDataWrapper obj) => this.obj = obj;

      /// <summary>Gets the property or null</summary>
      /// <param name="name">name of the property</param>
      /// <returns>The object if exist or null</returns>
      public object GetOrNull(string name)
      {
        object obj;
        return this.TryGet(name, out obj) ? obj : (object) null;
      }

      /// <summary>Gets the property</summary>
      /// <param name="name">The name of the property</param>
      /// <param name="value">The value</param>
      /// <returns>True if property exist</returns>
      public bool TryGet(string name, out object value) => this.TryGet(this.obj, name, out value);

      /// <summary>Gets the property</summary>
      /// <param name="obj">The object</param>
      /// <param name="name">The name of the property</param>
      /// <param name="value">The value</param>
      /// <returns>True if property exist</returns>
      public bool TryGet(RelatedDataWrapper obj, string name, out object value)
      {
        PropertyDescriptor propertyDescriptor1 = TypeDescriptor.GetProperties(obj.RelatedDataWrapperObject).Find(name, true);
        if (propertyDescriptor1 != null)
        {
          if (propertyDescriptor1 is LstringPropertyDescriptor propertyDescriptor2 && !this.ReturnLString && (this.Culture != null || !obj.Language.IsNullOrEmpty()))
          {
            this.Culture = this.Culture ?? CultureInfo.GetCultureInfo(obj.Language);
            value = (object) propertyDescriptor2.GetValue(obj.RelatedDataWrapperObject, this.Culture, false);
          }
          else
            value = propertyDescriptor1.GetValue(obj.RelatedDataWrapperObject);
          return true;
        }
        if (obj.RelatedDataWrapperObject is RelatedDataWrapper dataWrapperObject)
          return this.TryGet(dataWrapperObject, name, out value);
        value = (object) null;
        return false;
      }
    }
  }
}
