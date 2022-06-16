// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Resource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization.Data;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Base class for localizable string resources.</summary>
  public abstract class Resource
  {
    internal const string MissingResourcePrefix = "#ResourceNotFound#";
    private string classId;
    private PropertyCollection properties;
    internal ResourceDataProvider dataProvider;
    private static readonly object propertyBagsLock = new object();
    private static readonly Dictionary<string, PropertyCollection> propertyBags = new Dictionary<string, PropertyCollection>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Resource" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    protected Resource()
      : this(Res.DefaultProviderName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Resource" /> class.
    /// </summary>
    /// <param name="dataProviderName">Name of the data provider.</param>
    protected Resource(string dataProviderName)
    {
      if (ManagerBase<ResourceDataProvider>.StaticProvidersCollection == null)
        this.dataProvider = ResourceManager.GetManager(dataProviderName).Provider;
      else
        this.dataProvider = ManagerBase<ResourceDataProvider>.StaticProvidersCollection[dataProviderName];
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Resource" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    protected Resource(ResourceDataProvider dataProvider) => this.dataProvider = dataProvider != null ? dataProvider : throw new ArgumentNullException(nameof (dataProvider));

    /// <summary>
    /// Get the class ID for this resource. By default this is the type name.
    /// </summary>
    public virtual string ClassId
    {
      get
      {
        if (this.classId == null)
          this.classId = this.GetType().Name;
        return this.classId;
      }
      protected set => this.classId = value;
    }

    /// <summary>
    /// Gets the value corresponding to the specified key from this resource for the current UI culture.
    /// </summary>
    /// <param name="key">The resource entry key.</param>
    /// <returns>Resource value for the current UI culture.</returns>
    protected internal string this[string key] => this[key, (CultureInfo) null];

    /// <summary>
    /// Gets the value corresponding to the specified key from this resource for the specified culture.
    /// </summary>
    /// <param name="key">The resource entry key.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized. Note that if the resource is not localized for this culture,
    /// the lookup will fall back using the culture's <see cref="P:System.Globalization.CultureInfo.Parent" /> property,
    /// stopping after checking in the neutral culture.
    /// 
    /// If this value is null, the <see cref="T:System.Globalization.CultureInfo" /> is obtained using the
    /// culture's <see cref="!:Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture" /> property.
    /// </param>
    /// <returns>Resource value for the specified culture.</returns>
    protected internal string this[string key, CultureInfo culture]
    {
      get
      {
        ResourceProperty property = this.Properties[key];
        if (property == null)
          this.ThrowInvalidPropetyName(key);
        return this[property, culture];
      }
    }

    /// <summary>
    /// Gets the value corresponding to the specified property from this resource for the specified culture.
    /// </summary>
    /// <param name="property">
    /// The <see cref="T:Telerik.Sitefinity.Localization.ResourceProperty" /> object providing information about property that is
    /// defined as localizable resource entry.
    /// </param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized. Note that if the resource is not localized for this culture,
    /// the lookup will fall back using the culture's <see cref="P:System.Globalization.CultureInfo.Parent" /> property,
    /// stopping after checking in the neutral culture.
    /// 
    /// If this value is null, the <see cref="T:System.Globalization.CultureInfo" /> is obtained using the
    /// culture's <see cref="!:Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture" /> property.
    /// </param>
    /// <returns>Resource value for the specified culture.</returns>
    protected internal string this[ResourceProperty property, CultureInfo culture] => Resource.GetString(this.dataProvider, this.ClassId, property, culture, true);

    /// <summary>Gets the collection of properties.</summary>
    public virtual PropertyCollection Properties
    {
      get
      {
        if (this.properties == null)
          Resource.GetPropertiesFromType(this.GetType(), out this.properties);
        return this.properties;
      }
    }

    internal static Dictionary<string, PropertyCollection> PropertyBags => Resource.propertyBags;

    /// <summary>
    /// Gets the value corresponding to the specified key from this resource for the current UI culture.
    /// </summary>
    /// <param name="key">The resource entry key.</param>
    /// <returns>Resource value for the specified culture.</returns>
    public virtual string Get(string key) => this.Get(key, (CultureInfo) null, (object[]) null);

    /// <summary>
    /// Gets formatted string value corresponding to the specified key from this resource for the current UI culture.
    /// </summary>
    /// <param name="key">The resource entry key.</param>
    /// <param name="args">An <see cref="T:System.Object" /> to format.</param>
    /// <returns>Formatted resource string for the current UI culture.</returns>
    public virtual string Get(string key, params object[] args) => this.Get(key, (CultureInfo) null, args);

    /// <summary>
    /// Gets formatted string value corresponding to the specified key from this resource for the specified culture.
    /// </summary>
    /// <param name="key">The resource entry key.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized. Note that if the resource is not localized for this culture,
    /// the lookup will fall back using the culture's <see cref="P:System.Globalization.CultureInfo.Parent" /> property,
    /// stopping after checking in the neutral culture.
    /// 
    /// If this value is null, the <see cref="T:System.Globalization.CultureInfo" /> is obtained using the
    /// culture's <see cref="!:Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture" /> property.
    /// </param>
    /// <param name="args">An <see cref="T:System.Object" /> to format.</param>
    /// <returns>Formatted resource string for the specified culture.</returns>
    public virtual string Get(string key, CultureInfo culture, params object[] args)
    {
      if (culture == null)
        culture = Resource.ResolveCulture(culture);
      string format = !this.Properties.Contains(key) ? Resource.GetString(this.dataProvider, this.ClassId, key, culture, true, true) : this[key, culture];
      if (format != null)
      {
        if (args != null)
        {
          try
          {
            format = string.Format(format, args);
          }
          catch (FormatException ex)
          {
            string str = string.Empty;
            foreach (object obj in args)
              str = str + obj.ToString() + ", ";
            FormatException exceptionToHandle = new FormatException(Res.Get<ErrorMessages>("ResourceIncorrectArgs", (object) this.classId, (object) key, (object) format, (object) str.TrimEnd(',', ' ')), (Exception) ex);
            if (Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.Global))
              throw exceptionToHandle;
          }
        }
      }
      return format;
    }

    internal static string GetString(
      ResourceDataProvider provider,
      string classId,
      ResourceProperty property,
      CultureInfo culture,
      bool falback)
    {
      if (string.IsNullOrEmpty(classId))
        throw new ArgumentNullException(nameof (classId));
      if (property == null)
        throw new ArgumentNullException(nameof (property));
      if (property.AlwaysReturnDefaultValue)
        return property.DefaultValue;
      culture = Resource.ResolveCulture(culture);
      string str1 = provider.GetString(culture, classId, property.Key);
      if (!string.IsNullOrEmpty(str1))
        return str1;
      if (falback)
      {
        while (!culture.Equals((object) CultureInfo.InvariantCulture))
        {
          culture = culture.Parent;
          string str2 = provider.GetString(culture, classId, property.Key);
          if (!string.IsNullOrEmpty(str2))
            return str2;
        }
        return property.DefaultValue;
      }
      return culture == CultureInfo.InvariantCulture ? property.DefaultValue : (string) null;
    }

    internal static string GetString(
      ResourceDataProvider provider,
      string classId,
      string key,
      CultureInfo culture,
      bool falback,
      bool throws)
    {
      if (string.IsNullOrEmpty(classId))
        throw new ArgumentNullException(nameof (classId));
      if (string.IsNullOrEmpty(key))
        throw new ArgumentNullException(nameof (key));
      culture = Resource.ResolveCulture(culture);
      string str1 = provider.GetString(culture, classId, key);
      if (str1 != null)
        return str1;
      if (falback)
      {
        while (!culture.Equals((object) CultureInfo.InvariantCulture))
        {
          culture = culture.Parent;
          string str2 = provider.GetString(culture, classId, key);
          if (str2 != null)
            return str2;
        }
      }
      return throws ? string.Format("{0}:{1}:{2}", (object) "#ResourceNotFound#", (object) classId, (object) key) : (string) null;
    }

    internal static CultureInfo ResolveCulture(CultureInfo culture)
    {
      CultureInfo cultureInfo = culture;
      if (culture == null)
      {
        bool endControlRender = SystemManager.IsFrontEndControlRender;
        if (SystemManager.IsBackendRequest(out CultureInfo _) && !endControlRender)
          cultureInfo = SystemManager.CurrentContext.BackendCulture;
        if (cultureInfo == null)
          cultureInfo = SystemManager.CurrentContext.Culture;
      }
      return cultureInfo;
    }

    private static void GetPropertiesFromType(Type type, out PropertyCollection result)
    {
      if (Resource.propertyBags.TryGetValue(type.Name, out result))
        return;
      lock (Resource.propertyBagsLock)
      {
        if (Resource.propertyBags.TryGetValue(type.Name, out result))
          return;
        result = Resource.CreatePropertyBagFromType(type);
        Resource.propertyBags.Add(type.Name, result);
        object[] customAttributes = type.GetCustomAttributes(typeof (ObjectInfoAttribute), false);
        if (customAttributes.Length == 0)
          return;
        string name = ((ObjectInfoAttribute) customAttributes[0]).Name;
        if (string.IsNullOrEmpty(name) || name.Equals(type.Name, StringComparison.OrdinalIgnoreCase))
          return;
        Resource.propertyBags.Add(name, result);
      }
    }

    private static PropertyCollection CreatePropertyBagFromType(Type type)
    {
      PropertyCollection propertyBagFromType = new PropertyCollection();
      foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
      {
        ResourceProperty propertyFromAttributes = Resource.CreateConfigurationPropertyFromAttributes(property);
        if (propertyFromAttributes != null)
        {
          if (propertyBagFromType.Contains(propertyFromAttributes.Key))
            throw new DuplicateNameException(propertyFromAttributes.Key);
          propertyBagFromType.Add(propertyFromAttributes);
        }
      }
      return propertyBagFromType;
    }

    private static ResourceProperty CreateConfigurationPropertyFromAttributes(
      PropertyInfo info)
    {
      return Attribute.GetCustomAttribute((MemberInfo) info, typeof (ResourceEntryAttribute)) is ResourceEntryAttribute customAttribute ? new ResourceProperty(customAttribute) : (ResourceProperty) null;
    }

    private void ThrowInvalidPropetyName(string name) => throw new ArgumentException(Res.Get<ErrorMessages>("Resource_InvalidPropertyName", (object) name, (object) this.GetType().Name));
  }
}
