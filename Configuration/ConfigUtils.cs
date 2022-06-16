// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigUtils
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Provides utility functions for the configuration classes.
  /// </summary>
  internal static class ConfigUtils
  {
    internal static TBaseType CreateInstance<TBaseType>(Type actualType = null, params object[] args) => (TBaseType) ConfigUtils.CreateInstance(typeof (TBaseType), actualType, args);

    internal static object CreateInstance(Type baseType, Type actualType = null, params object[] args)
    {
      Type type = baseType;
      if (actualType != (Type) null)
        type = type.IsAssignableFrom(actualType) ? actualType : throw new ArgumentException("The specified type must inherit from \"{0}\".".Arrange((object) type.AssemblyQualifiedName));
      return type.IsAbstract ? (object) null : Activator.CreateInstance(type, args);
    }

    internal static string GetPrintableName(object obj)
    {
      PropertyDescriptor defaultProperty = TypeDescriptor.GetDefaultProperty(obj);
      if (defaultProperty != null)
        return defaultProperty.GetValue(obj).ToString();
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj))
      {
        ConfigurationPropertyAttribute attribute = (ConfigurationPropertyAttribute) property.Attributes[typeof (ConfigurationPropertyAttribute)];
        if (attribute != null && attribute.IsKey)
          return property.GetValue(obj).ToString();
      }
      return ConfigUtils.GetPrintableName(obj.GetType().Name);
    }

    internal static string GetPrintableName(string name) => ConfigUtils.TrimEnd(ConfigUtils.TrimEnd(name, "Element"), "Config");

    private static string TrimEnd(string str, string value)
    {
      if (str.EndsWith(value, StringComparison.OrdinalIgnoreCase))
        str = str.Substring(0, str.Length - value.Length);
      return str;
    }

    internal static bool ValidateProperty(PropertyDescriptor prop, Type type)
    {
      if (!prop.IsBrowsable)
        return false;
      return typeof (ConfigElement).IsAssignableFrom(prop.ComponentType) ? !typeof (ConfigElement).Equals(prop.ComponentType) : prop.ComponentType.Equals(type);
    }
  }
}
