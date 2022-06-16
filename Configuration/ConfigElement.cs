// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Represents a configuration element within a configuration file.
  /// </summary>
  [DataContract]
  public class ConfigElement
  {
    protected internal string tagName;
    private string path;
    private string intrKey;
    private ConfigElement parent;
    private ConfigSection section;
    private ConfigProvider provider;
    private string collectionItemName;
    private object valuesLock = new object();
    private ConfigPropertyCollection properties;
    private Dictionary<ConfigProperty, object> values = new Dictionary<ConfigProperty, object>();
    private static object propertyBagsLock = new object();
    private static Dictionary<Type, ConfigPropertyCollection> propertyBags = new Dictionary<Type, ConfigPropertyCollection>();
    private string origin;
    private ConfigSource source;
    protected internal PropertyResolverBase propertyResolver;
    protected internal Telerik.Sitefinity.Configuration.GetDefaultValue getDefaultValueHandler;
    private const string secretValueResolverPrefix = "[sf-scrt-rslvr=";
    private const string secretValueResolverSufix = "]";
    public const char PathSeparator = '/';
    public const string ParametersPropertyName = "parameters";

    /// <summary>
    /// Initializes new instance of ConfigElement with the parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ConfigElement(ConfigElement parent)
    {
      if (parent == null)
      {
        Type type = this.GetType();
        if (!typeof (ConfigSection).IsAssignableFrom(type) && !typeof (ConfigElementCollection).IsAssignableFrom(type))
          throw new ArgumentNullException(nameof (parent), "Null parent is allowed only for inheritors of ConfigSection and ConfigElementCollection.");
      }
      this.parent = parent;
    }

    internal ConfigElement()
      : this(true)
    {
    }

    internal ConfigElement(bool check)
    {
      if (!check)
        return;
      Type type = this.GetType();
      if (!typeof (ConfigSection).IsAssignableFrom(type) && !typeof (ConfigElementCollection).IsAssignableFrom(type))
        throw new InvalidOperationException("This constructor can be used only by ConfigSection inheritors.");
    }

    /// <summary>
    /// Gets or sets a value indicating whether the current element has any site specific values for persistence when saving the ConfigSection.
    /// </summary>
    public bool PersistsSiteSpecificValues { get; set; }

    /// <summary>Gets the section of which this element belongs to.</summary>
    public ConfigSection Section
    {
      get
      {
        if (this.section == null)
        {
          if (this.parent == null)
          {
            this.section = this as ConfigSection;
          }
          else
          {
            for (ConfigElement parent = this.parent; parent != null; parent = parent.parent)
            {
              if (parent.parent == null)
                this.section = parent as ConfigSection;
            }
          }
        }
        return this.section;
      }
    }

    /// <summary>Gets the parent element of this element.</summary>
    public ConfigElement Parent
    {
      get => this.parent;
      internal set => this.parent = value;
    }

    /// <summary>
    /// Gets or sets the name of the module the config element depends on.
    /// </summary>
    /// <value>The name of the module.</value>
    internal string LinkModuleName { get; set; }

    /// <summary>Gets or sets the source.</summary>
    /// <value>The source.</value>
    internal ConfigSource Source
    {
      get => this.source == ConfigSource.NotSet && this.Parent != null ? this.Parent.Source : this.source;
      set => this.source = value;
    }

    /// <summary>Get the data provider for this element.</summary>
    protected internal ConfigProvider Provider
    {
      get => this.provider == null && this.Section != null ? this.Section.provider : this.provider;
      internal set => this.provider = value;
    }

    /// <summary>
    /// Check this property to see, if the section containing the current element is loading default values
    /// in the <see cref="M:Telerik.Sitefinity.Configuration.ConfigElement.OnPropertiesInitialized" /> method.
    /// </summary>
    protected internal bool IsLoadingDefaults => this.Section != null && this.Section.isLoadingDefaults;

    /// <summary>XML element tag name.</summary>
    public virtual string TagName
    {
      get => this.tagName;
      set => this.tagName = value;
    }

    /// <summary>
    /// Gets or sets a property, attribute, or child element of this configuration element.
    /// </summary>
    /// <returns>The specified property, attribute, or child element</returns>
    /// <param name="propertyName">
    /// The name of the <see cref="T:System.Configuration.ConfigurationProperty" /> to access.
    /// </param>
    /// <remarks>Member access changed to facilitate unit testing</remarks>
    public virtual object this[string propertyName]
    {
      get
      {
        ConfigProperty prop;
        return !this.Properties.TryGetValue(propertyName, out prop) && !this.ThrowInvalidPropetyName(propertyName) ? (object) null : this[prop];
      }
      set
      {
        ConfigProperty prop;
        if (this.Properties.TryGetValue(propertyName, out prop))
          this[prop] = value;
        else
          this.ThrowInvalidPropetyName(propertyName);
      }
    }

    /// <summary>
    /// Gets or sets a property or attribute of this configuration element.
    /// </summary>
    /// <returns>The specified property, attribute, or child element.</returns>
    /// <param name="prop">The property to access.</param>
    protected internal object this[ConfigProperty prop]
    {
      get
      {
        object rawValue = this.GetRawValue(prop);
        return rawValue is LazyValue lazyValue ? lazyValue.Value : rawValue;
      }
      set => this.SetRawValue(prop, value);
    }

    protected internal void SetRawValue(ConfigProperty prop, object value)
    {
      switch (value)
      {
        case ConfigElement _:
          ConfigElement configElement = (ConfigElement) value;
          if (configElement.Parent != this)
            throw new ArgumentException(Res.Get<ErrorMessages>().InvalidConfigElementParent.Arrange((object) prop.Name));
          configElement.tagName = prop.Name;
          break;
        case NameValueCollection _:
          if (!(value is SecretNameValueCollection))
          {
            value = (object) new NameValueCollection((NameValueCollection) value);
            break;
          }
          break;
        case string stringValue:
          if (prop.Type != typeof (string) && prop.Type != typeof (Type) && prop.Converter != null && prop.Converter.CanConvertFrom(typeof (string)))
          {
            value = ConfigElement.GetValueFromString(stringValue, prop);
            break;
          }
          break;
      }
      if (prop.Validator != null)
        prop.Validator.Validate(value);
      PersistedValueWrapper persistedValueWrapper = (PersistedValueWrapper) null;
      object obj;
      if (this.values.TryGetValue(prop, out obj))
        persistedValueWrapper = obj as PersistedValueWrapper;
      if (persistedValueWrapper != null && !(value is LazyValue) && persistedValueWrapper.Value is LazyValue lazyValue && ConfigElement.GetStringValue(value, prop) == lazyValue.Key)
        return;
      if (prop.Type == typeof (string) && !(value is SecretValue))
      {
        string resolver1;
        string actualStringValue = ConfigElement.GetActualStringValue(value as string, out resolver1);
        ISecretDataResolver resolver2 = (ISecretDataResolver) null;
        if ((resolver1 == null || !Config.TryGetSecretResolver(resolver1, out resolver2)) && prop.IsSecret)
          resolver2 = Config.GetDefaultSecretResolver();
        if (resolver2 != null && resolver2.Mode == SecretDataMode.Encrypt)
        {
          if (!actualStringValue.IsNullOrEmpty())
          {
            try
            {
              value = (object) new SecretValue(resolver2.GenerateKey(actualStringValue), prop, resolver2.Name);
              goto label_23;
            }
            catch
            {
              InvalidOperationException exceptionToHandle = new InvalidOperationException("Unable to generate key for the secret property '{0}' at this time.".Arrange((object) this.GetPropertyPath(prop.Name)));
              if (Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
                throw exceptionToHandle;
              value = (object) actualStringValue;
              goto label_23;
            }
          }
        }
        value = (object) actualStringValue;
      }
label_23:
      if (persistedValueWrapper != null)
        persistedValueWrapper.Value = value;
      else
        this.values[prop] = value;
    }

    internal void SetPersistedRawValue(
      string configPropertyName,
      object value,
      ConfigSource source)
    {
      this.SetPersistedRawValue(this.Properties[configPropertyName], value, source);
    }

    internal void SetPersistedRawValue(
      ConfigProperty configProperty,
      object value,
      ConfigSource source)
    {
      this.values[configProperty] = (object) new PersistedValueWrapper(value, source);
    }

    [Obsolete("Use SetRawValue method instead")]
    protected internal void SetValueInternal(
      ConfigProperty prop,
      object value,
      bool loadingPersisted,
      ConfigSource source = ConfigSource.NotSet)
    {
      switch (value)
      {
        case ConfigElement _:
          ConfigElement configElement = (ConfigElement) value;
          if (configElement.Parent != this)
            throw new ArgumentException(Res.Get<ErrorMessages>().InvalidConfigElementParent.Arrange((object) prop.Name));
          configElement.tagName = prop.Name;
          break;
        case NameValueCollection _:
          value = (object) new NameValueCollection((NameValueCollection) value);
          break;
        case string text:
          if (prop.Type != typeof (string) && prop.Type != typeof (Type) && prop.Converter != null && prop.Converter.CanConvertFrom(typeof (string)))
          {
            value = prop.Converter.ConvertFromString(text);
            break;
          }
          break;
      }
      if (prop.Validator != null)
        prop.Validator.Validate(value);
      if (source == ConfigSource.NotSet)
      {
        object obj;
        if (this.values.TryGetValue(prop, out obj) && obj is PersistedValueWrapper)
        {
          ((PersistedValueWrapper) obj).Value = value;
          value = obj;
        }
      }
      else
        value = (object) new PersistedValueWrapper(value, source);
      this.values[prop] = value;
    }

    internal object ResolveDefaultValue(ConfigProperty prop)
    {
      if (this.propertyResolver != null)
      {
        object obj = this.propertyResolver.ResolveProperty<object>(prop.Name);
        if (obj != null)
          return obj;
      }
      if (this.getDefaultValueHandler != null)
      {
        object obj = this.getDefaultValueHandler(prop.Name);
        if (obj != null)
          return obj;
      }
      object defaultValue = prop.DefaultValue;
      return defaultValue == null && prop.Type.IsValueType ? Activator.CreateInstance(prop.Type) : defaultValue;
    }

    /// <summary>
    /// Determines whether the collection contains an item with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>
    /// 	<c>true</c> if contains an item with the specified key; otherwise, <c>false</c>.
    /// </returns>
    protected internal virtual ConfigElement GetElementByKey(string key) => this[key] as ConfigElement;

    /// <summary>
    /// Gets the raw value object of a configuration property. It could be a the actual value object, or a LazyValue wrapper.
    /// </summary>
    /// <param name="prop">The configuration property which value is wanted.</param>
    /// <returns>The value of the property.</returns>
    protected internal object GetRawValue(ConfigProperty prop) => this.GetRawValue(prop, out PersistedValueWrapper _);

    /// <summary>
    /// Gets the raw value object of a configuration property. It could be a the actual value object, or a LazyValue wrapper.
    /// </summary>
    /// <param name="prop">The configuration property which value is wanted.</param>
    /// <returns>The value of the property.</returns>
    [Obsolete("Use GetRawValue value instead.")]
    protected internal object GetDefaultValue(ConfigProperty prop) => this.GetRawValue(prop, out PersistedValueWrapper _);

    internal object GetRawValue(
      ConfigProperty prop,
      out PersistedValueWrapper valueWrapper,
      bool skipDefaultValue = false)
    {
      object rawValue;
      if (!this.values.TryGetValue(prop, out rawValue))
      {
        lock (this.valuesLock)
        {
          if (!this.values.TryGetValue(prop, out rawValue))
          {
            if (!this.TryResolveComplexValue(prop, out rawValue))
            {
              valueWrapper = (PersistedValueWrapper) null;
              if (!skipDefaultValue)
                rawValue = this.ResolveDefaultValue(prop);
              return rawValue;
            }
            this.values.Add(prop, rawValue);
          }
        }
      }
      valueWrapper = rawValue as PersistedValueWrapper;
      if (valueWrapper != null)
        rawValue = valueWrapper.Value;
      if (rawValue != null)
      {
        if (rawValue.GetType().Equals(typeof (ConfigElement.DefaultValueWrapper)))
          rawValue = this.ResolveDefaultValue(prop);
        else if (rawValue is ConfigElementCollection)
          ((ConfigElementCollection) rawValue).EnsureDelayedInitialization();
      }
      return rawValue;
    }

    private bool TryResolveComplexValue(ConfigProperty prop, out object value)
    {
      value = (object) null;
      if (typeof (ConfigElement).IsAssignableFrom(prop.Type))
      {
        value = (object) (ConfigElement) Activator.CreateInstance(prop.Type, (object) this);
        ((ConfigElement) value).tagName = prop.Name;
      }
      else if (typeof (NameValueCollection).IsAssignableFrom(prop.Type))
        value = !prop.IsSecret ? (object) new SecretNameValueCollection() : (object) new NameValueCollection();
      else if (prop.Type.ImplementsGenericInterface(typeof (IDictionary<,>)))
      {
        Type interfaceImplementation = prop.Type.GetGenericInterfaceImplementation(typeof (IDictionary<,>));
        if (interfaceImplementation != (Type) null)
          value = Activator.CreateInstance(typeof (Dictionary<,>).MakeGenericType(interfaceImplementation.GetGenericArguments()));
      }
      else if (prop.Type.ImplementsGenericInterface(typeof (ICollection<>)))
      {
        Type interfaceImplementation = prop.Type.GetGenericInterfaceImplementation(typeof (ICollection<>));
        if (interfaceImplementation != (Type) null)
          value = Activator.CreateInstance(typeof (List<>).MakeGenericType(interfaceImplementation.GetGenericArguments()));
      }
      return value != null;
    }

    /// <summary>Gets the collection of properties.</summary>
    /// <value>The properties.</value>
    public virtual ConfigPropertyCollection Properties
    {
      get
      {
        this.EnsurePropertiesInitialized();
        return this.properties;
      }
    }

    /// <summary>
    /// Specifies the name of the tag that is used when this element is an item in a collection.
    /// By default the tag name is "add".
    /// </summary>
    public virtual string CollectionItemName
    {
      get
      {
        if (string.IsNullOrEmpty(this.collectionItemName))
          this.collectionItemName = this.Parent is ConfigElementCollection parent ? parent.AddElementName : throw new InvalidOperationException("The parent of this ConfigElement must be of a type ConfigElementCollection.");
        return this.collectionItemName;
      }
      set => this.collectionItemName = value;
    }

    /// <summary>Initializes the properties.</summary>
    protected virtual void InitializeProperties() => this.OnPropertiesInitialized();

    /// <summary>Ensures the properties initialized.</summary>
    protected internal virtual void EnsurePropertiesInitialized()
    {
      if (this.properties != null)
        return;
      lock (this.valuesLock)
      {
        if (this.properties != null)
          return;
        ConfigElement.GetPropertiesFromType(this.GetType(), out this.properties);
        ConfigElement.ApplyInstanceAttributes((object) this);
        this.InitializeProperties();
      }
    }

    /// <summary>
    /// Validates the properties of the current instance and throws an exception with the appropriate message in it if the validation fails.
    /// </summary>
    protected internal virtual void Validate() => this.ValidateRequiredProperties();

    private void ValidateRequiredProperties()
    {
      List<string> stringList = new List<string>(this.properties.Count);
      foreach (ConfigProperty property in (Collection<ConfigProperty>) this.properties)
      {
        if ((property.IsKey || property.IsRequired) && property.DefaultValue == null)
        {
          if (this.GetRawValue(property) is LazyValue rawValue)
          {
            if (string.IsNullOrEmpty(rawValue.Key))
              stringList.Add(property.Name);
          }
          else if (property.Type == typeof (string) && string.IsNullOrEmpty((string) this[property]))
            stringList.Add(property.Name);
          else if (this[property] == null)
            stringList.Add(property.Name);
        }
      }
      if (stringList.Count > 0)
        throw new ArgumentException(string.Format("The following required properties are not set: {0}", (object) string.Join(", ", stringList.ToArray())));
    }

    private void ApplyValidators()
    {
      foreach (ConfigProperty property in (Collection<ConfigProperty>) this.properties)
      {
        if (property.Validator != null)
          property.Validator.Validate(this[property]);
      }
    }

    /// <summary>Gets or sets the origin of the element.</summary>
    /// <value>The origin.</value>
    internal string Origin
    {
      get => this.origin;
      set => this.origin = value;
    }

    /// <summary>
    /// Overriding this method allows to plug custom logic when reading parameters from the 'parameters' NameValueCollection
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public virtual string GetParameter(string key)
    {
      ConfigProperty prop;
      return this.Properties.TryGetValue("parameters", out prop) && prop != null && typeof (NameValueCollection).IsAssignableFrom(prop.Type) && this[prop] is NameValueCollection nameValueCollection ? nameValueCollection[key] : (string) null;
    }

    internal virtual string GetUnresolvedParameter(string key, out string resolver)
    {
      resolver = (string) null;
      ConfigProperty prop;
      if (this.Properties.TryGetValue("parameters", out prop) && prop != null && typeof (NameValueCollection).IsAssignableFrom(prop.Type))
      {
        NameValueCollection nameValueCollection1 = this[prop] as NameValueCollection;
        SecretNameValueCollection nameValueCollection2 = nameValueCollection1 as SecretNameValueCollection;
        if (nameValueCollection1 != null)
          return nameValueCollection2 != null && nameValueCollection2.IsSecret(key, out resolver) ? nameValueCollection2.GetValues(key)[0] : nameValueCollection1[key];
      }
      return (string) null;
    }

    internal virtual string GetEnvironmentParameterOriginalValue(string key, out string resolver)
    {
      resolver = (string) null;
      ConfigProperty prop;
      if (this.Properties.TryGetValue("parameters", out prop) && prop != null && typeof (NameValueCollection).IsAssignableFrom(prop.Type))
      {
        NameValueCollection nameValueCollection1 = this[prop] as NameValueCollection;
        SecretNameValueCollection nameValueCollection2 = nameValueCollection1 as SecretNameValueCollection;
        if (nameValueCollection1 != null && nameValueCollection2 != null && nameValueCollection2.IsSecret(key, out resolver))
          return nameValueCollection2.GetValues(key)[0];
      }
      return (string) null;
    }

    /// <summary>
    /// Overriding this method allows to plug custom logic when writing parameters in the 'parameters' NameValueCollection
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="T:System.InvalidOperationException"></exception>
    public virtual void SetParameter(string key, string value) => this.SetParameter(key, value, (string) null);

    protected internal virtual void SetParameter(string key, string value, string secretResolver)
    {
      if (!(secretResolver != "EnvVariables"))
        return;
      NameValueCollection parametersCollection = this.GetParametersCollection(secretResolver);
      if (parametersCollection is SecretNameValueCollection && !string.IsNullOrEmpty(secretResolver))
        ((SecretNameValueCollection) parametersCollection).SetSecretValue(key, value, secretResolver);
      else
        parametersCollection[key] = value;
    }

    protected internal virtual void SetEnvironmentParameter(
      string key,
      string value,
      string secretResolver,
      string originalValue)
    {
      NameValueCollection parametersCollection = this.GetParametersCollection(secretResolver);
      if (parametersCollection is SecretNameValueCollection && !string.IsNullOrEmpty(secretResolver))
        ((SecretNameValueCollection) parametersCollection).SetEnvironmentValue(key, value, secretResolver, originalValue);
      else
        parametersCollection[key] = value;
    }

    internal NameValueCollection GetParametersCollection(string secretResolver)
    {
      ConfigProperty prop;
      if (!this.Properties.TryGetValue("parameters", out prop) || prop == null || !typeof (NameValueCollection).IsAssignableFrom(prop.Type))
        throw new InvalidOperationException(string.Format("'{0}' does not have '{1}' property of type '{2}'", (object) this.GetType().FullName, (object) "parameters", (object) typeof (NameValueCollection).FullName));
      if (!(this[prop] is NameValueCollection col))
        col = !string.IsNullOrEmpty(secretResolver) ? (NameValueCollection) new SecretNameValueCollection() : new NameValueCollection();
      if (string.IsNullOrEmpty(secretResolver))
        return col;
      if (!(col is SecretNameValueCollection parametersCollection))
      {
        parametersCollection = new SecretNameValueCollection(col);
        this[prop] = (object) parametersCollection;
      }
      return (NameValueCollection) parametersCollection;
    }

    /// <summary>Clones this element with the specified parent.</summary>
    /// <param name="withParent">The parent.</param>
    /// <returns></returns>
    protected internal virtual ConfigElement Clone(
      ConfigElement withParent = null,
      bool deepCopy = true)
    {
      ConfigElement instance;
      if (withParent == null)
      {
        instance = (ConfigElement) Activator.CreateInstance(this.GetType(), (object) this.Parent);
        instance.parent = (ConfigElement) null;
      }
      else
        instance = (ConfigElement) Activator.CreateInstance(this.GetType(), (object) withParent);
      instance.tagName = this.tagName;
      foreach (KeyValuePair<ConfigProperty, object> keyValuePair in this.values)
      {
        if (keyValuePair.Value != null)
        {
          if (typeof (ConfigElement).IsAssignableFrom(keyValuePair.Value.GetType()) & deepCopy)
          {
            instance.values[keyValuePair.Key] = (object) ((ConfigElement) keyValuePair.Value).Clone();
          }
          else
          {
            object obj = ConfigElement.ClonePropertyValue(keyValuePair.Value);
            if (obj != null)
              instance.values[keyValuePair.Key] = obj;
          }
        }
      }
      return instance;
    }

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected virtual void OnPropertiesInitialized()
    {
    }

    /// <summary>
    /// Throws the exception that a specified property name is not valid.
    /// </summary>
    /// <param name="name">The name of the property that is invalid.</param>
    /// <returns>Returns false if exceptions ought to be ignored per exception policy.</returns>
    protected internal bool ThrowInvalidPropetyName(string name)
    {
      Exception exceptionToHandle = (Exception) new ArgumentException(string.Format("Invalid property name \"{0}\" for configuration element \"{1}\".", (object) name, (object) this.GetType().Name));
      if (Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
        throw exceptionToHandle;
      return false;
    }

    protected internal bool TryGenerateFileName(out string fileName)
    {
      if (this.Section != null)
        return this.Section.TryGenerateLazyElementFileName(this, out fileName);
      fileName = (string) null;
      return false;
    }

    /// <summary>Gets the path of the ConfigElement.</summary>
    /// <returns>The ConfigElement path.</returns>
    public virtual string GetPath()
    {
      if (this.path == null)
        this.path = this.Parent != null ? this.Parent.GetPath() + (object) '/' + this.GetKey() : this.GetKey();
      return this.path;
    }

    internal virtual string GetPropertyPath(string propertyName) => this.GetPath() + (object) '/' + propertyName;

    /// <summary>
    /// Gets the key to uniquely identify the configuration property.
    /// </summary>
    /// <returns></returns>
    public string GetKey()
    {
      if (this.tagName == null)
        this.tagName = this.GenerateKeyIfNeeded();
      return this.tagName;
    }

    protected virtual string GenerateKeyIfNeeded() => this.Parent != null && this.Parent is ConfigElementCollection ? ((ConfigElementCollection) this.parent).GenerateChildKey(this) : string.Empty;

    internal static string GeneretateSecretKeyValue(string key, string resolver) => "[sf-scrt-rslvr=" + resolver + "]" + key;

    internal static string GetActualStringValue(string value, out string resolver)
    {
      resolver = (string) null;
      if (value != null && value.StartsWith("[sf-scrt-rslvr="))
      {
        int num = value.IndexOf("]", "[sf-scrt-rslvr=".Length);
        if (num > "[sf-scrt-rslvr=".Length)
        {
          if (value.Length > num + 1)
          {
            resolver = value.Substring("[sf-scrt-rslvr=".Length, num - "[sf-scrt-rslvr=".Length);
            value = value.Substring(num + 1);
          }
          else
            value = string.Empty;
        }
      }
      return value;
    }

    public static string GetStringValue(object value, ConfigProperty property = null)
    {
      if (value == null)
        return (string) null;
      if (value is LazyValue)
        return ((LazyValue) value).Key;
      Type type = value.GetType();
      if (type == typeof (string))
        return (string) value;
      if (type == typeof (DateTime))
        return ((DateTime) value).ToString("u");
      if (type == typeof (double))
        return ((double) value).ToString("R", (IFormatProvider) CultureInfo.InvariantCulture);
      if (type == typeof (float))
        return ((float) value).ToString("R", (IFormatProvider) CultureInfo.InvariantCulture);
      if (type == typeof (Decimal))
        return Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
      if (type == typeof (Type))
        return TypeResolutionService.UnresolveType((Type) value);
      TypeConverter typeConverter = (TypeConverter) null;
      if (property != null)
        typeConverter = property.Converter;
      if (typeConverter == null)
        typeConverter = TypeDescriptor.GetConverter(type);
      return typeConverter != null && typeConverter.CanConvertTo(typeof (string)) ? typeConverter.ConvertToString(value) : throw new ArgumentException("Unsupported configuration value type.");
    }

    public static object GetValueFromString(string stringValue, ConfigProperty property) => ConfigElement.GetValueFromString(stringValue, property.Type, property);

    public static T GetValueFromString<T>(string stringValue, ConfigProperty property = null) => (T) ConfigElement.GetValueFromString(stringValue, typeof (T), property);

    public static object GetValueFromString(string stringValue, Type type, ConfigProperty property = null)
    {
      object valueFromString;
      if (type == typeof (string))
        valueFromString = (object) stringValue;
      else if (type == typeof (bool))
        valueFromString = (object) bool.Parse(stringValue);
      else if (type == typeof (DateTime))
        valueFromString = (object) DateTime.Parse(stringValue, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
      else if (type == typeof (float) || type == typeof (double) || type == typeof (Decimal))
        valueFromString = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(stringValue);
      else if (type == typeof (Type))
      {
        valueFromString = (object) TypeResolutionService.ResolveType(stringValue, false) ?? (object) stringValue;
      }
      else
      {
        TypeConverter typeConverter = (TypeConverter) null;
        if (property != null)
          typeConverter = property.Converter;
        if (typeConverter == null)
          typeConverter = TypeDescriptor.GetConverter(type);
        valueFromString = typeConverter != null && typeConverter.CanConvertFrom(typeof (string)) ? typeConverter.ConvertFromString(stringValue) : throw new NotSupportedException(property == null ? string.Format("No appropriate conversion for configuration value of type {0}.", (object) type) : string.Format("No appropriate conversion for {0} configuration property {1} with actual type {2}.", (object) property.Type, (object) property.Name, (object) type));
      }
      return valueFromString;
    }

    internal static ConfigProperty[] GetKeyPropertiesFromType(Type type)
    {
      ConfigPropertyCollection result;
      ConfigElement.GetPropertiesFromType(type, out result);
      return result?.KeyProperties;
    }

    internal static object ClonePropertyValue(object value)
    {
      Type type = value.GetType();
      if (typeof (string).IsAssignableFrom(type) || type.IsValueType)
        return value;
      if (typeof (ICloneable).IsAssignableFrom(type))
        return ((ICloneable) value).Clone();
      return typeof (NameValueCollection).IsAssignableFrom(type) ? (object) new NameValueCollection((NameValueCollection) value) : (object) null;
    }

    private static void GetPropertiesFromType(Type type, out ConfigPropertyCollection result)
    {
      if (ConfigElement.propertyBags.TryGetValue(type, out result))
        return;
      lock (ConfigElement.propertyBagsLock)
      {
        if (ConfigElement.propertyBags.TryGetValue(type, out result))
          return;
        result = ConfigElement.CreatePropertyBagFromType(type);
        ConfigElement.propertyBags.Add(type, result);
      }
    }

    private static ConfigPropertyCollection CreatePropertyBagFromType(
      Type type)
    {
      ConfigPropertyCollection propertyBagFromType = new ConfigPropertyCollection();
      foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
      {
        ConfigurationProperty propertyFromAttributes = ConfigElement.CreateConfigurationPropertyFromAttributes(property);
        if (propertyFromAttributes != null && !propertyBagFromType.Contains(propertyFromAttributes.Name))
        {
          ConfigProperty configProperty = new ConfigProperty(propertyFromAttributes);
          if (Attribute.GetCustomAttribute((MemberInfo) property, typeof (SkipOnExportAttribute)) is SkipOnExportAttribute)
            configProperty.SkipOnExport = true;
          if (Attribute.GetCustomAttribute((MemberInfo) property, typeof (SecretDataAttribute)) is SecretDataAttribute)
            configProperty.IsSecret = true;
          propertyBagFromType.Add(configProperty);
        }
      }
      return propertyBagFromType;
    }

    private static void ApplyInstanceAttributes(object instance)
    {
      foreach (PropertyInfo property in instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
      {
        if (Attribute.GetCustomAttribute((MemberInfo) property, typeof (ConfigurationPropertyAttribute)) is ConfigurationPropertyAttribute)
        {
          Type propertyType = property.PropertyType;
          if (typeof (ConfigElementCollection).IsAssignableFrom(propertyType))
          {
            if (!(property.GetValue(instance, (object[]) null) is ConfigElementCollection elementCollection))
              throw new ConfigurationErrorsException("Config element null instance");
            if (!(Attribute.GetCustomAttribute((MemberInfo) property, typeof (ConfigurationCollectionAttribute)) is ConfigurationCollectionAttribute customAttribute1))
              customAttribute1 = Attribute.GetCustomAttribute((MemberInfo) propertyType, typeof (ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute;
            if (customAttribute1 != null)
            {
              if (customAttribute1.AddItemName.IndexOf(',') == -1)
                elementCollection.AddElementName = customAttribute1.AddItemName;
              elementCollection.RemoveElementName = customAttribute1.RemoveItemName;
              elementCollection.ClearElementName = customAttribute1.ClearItemsName;
            }
            if (Attribute.GetCustomAttribute((MemberInfo) property, typeof (KeepRemoveClearItemsAttribute)) is KeepRemoveClearItemsAttribute customAttribute2)
              elementCollection.KeepRemoveItems = customAttribute2.Keep;
          }
          else if (typeof (ConfigElement).IsAssignableFrom(propertyType))
            ConfigElement.ApplyInstanceAttributes(property.GetValue(instance, (object[]) null) ?? throw new ConfigurationErrorsException("Config element null instance."));
        }
      }
    }

    private static ConfigurationProperty CreateConfigurationPropertyFromAttributes(
      PropertyInfo info)
    {
      ConfigurationProperty propertyFromAttributes = (ConfigurationProperty) null;
      if (Attribute.GetCustomAttribute((MemberInfo) info, typeof (ConfigurationPropertyAttribute)) is ConfigurationPropertyAttribute)
      {
        string description = (string) null;
        object defaultValue = (object) null;
        ConfigurationPropertyAttribute propertyAttribute = (ConfigurationPropertyAttribute) null;
        TypeConverter typeConverter = (TypeConverter) null;
        ConfigurationValidatorBase validator = (ConfigurationValidatorBase) null;
        foreach (Attribute customAttribute in Attribute.GetCustomAttributes((MemberInfo) info))
        {
          switch (customAttribute)
          {
            case TypeConverterAttribute _:
              typeConverter = (TypeConverter) Activator.CreateInstance(Type.GetType(((TypeConverterAttribute) customAttribute).ConverterTypeName), true);
              break;
            case ConfigurationPropertyAttribute _:
              propertyAttribute = (ConfigurationPropertyAttribute) customAttribute;
              break;
            case ConfigurationValidatorAttribute _:
              validator = validator == null ? ((ConfigurationValidatorAttribute) customAttribute).ValidatorInstance : throw new ConfigurationErrorsException("Validator multiple validator attributes.");
              break;
            case DefaultValueAttribute _:
              defaultValue = ((DefaultValueAttribute) customAttribute).Value;
              break;
          }
        }
        if (defaultValue == null)
          defaultValue = propertyAttribute.DefaultValue;
        propertyFromAttributes = new ConfigurationProperty(propertyAttribute.Name, info.PropertyType, defaultValue, typeConverter, validator, propertyAttribute.Options, description);
      }
      if (propertyFromAttributes != null && typeof (ConfigElement).IsAssignableFrom(propertyFromAttributes.Type))
      {
        ConfigPropertyCollection result = (ConfigPropertyCollection) null;
        ConfigElement.GetPropertiesFromType(propertyFromAttributes.Type, out result);
      }
      return propertyFromAttributes;
    }

    protected static string GetKey(ConfigElement element) => element.GetPath();

    private string InternalKey
    {
      get
      {
        if (this.intrKey == null)
          this.intrKey = this.GenerateInternalKey();
        return this.intrKey;
      }
    }

    protected virtual string GenerateInternalKey() => this.GetHashCode().ToString();

    private class DefaultValueWrapper
    {
    }
  }
}
