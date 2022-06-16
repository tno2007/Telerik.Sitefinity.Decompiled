// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ConfigPropertyResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Reflection;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Helper class for resolving properties that have default value specified via configuration and can have different value specified at UI.
  /// The approach to set the property value when property is resolved is if property do not have value return default value.
  /// Default values are determined with following priority:
  ///     1) parent content type
  ///     2) module level
  ///     3) configuration
  /// </summary>
  public class ConfigPropertyResolver : PropertyResolverBase
  {
    private string configPath;
    private SettingsResolverDelegate settingsResolverDelegate;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ConfigPropertyResolver" /> class.
    /// </summary>
    /// <param name="virtualPath">The virtual path.</param>
    public ConfigPropertyResolver(string configPath) => this.configPath = configPath != null ? configPath : throw new ArgumentNullException(nameof (configPath));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ConfigPropertyResolver" /> class.
    /// </summary>
    /// <param name="virtualPath">The virtual path.</param>
    public ConfigPropertyResolver(ConfigElement config) => this.configPath = config != null ? config.GetPath() : throw new ArgumentNullException(nameof (config));

    /// <summary>Gets the config.</summary>
    /// <value>The config.</value>
    protected ConfigElement GetConfig() => Config.GetByPath<ConfigElement>(this.configPath);

    protected internal void RegisterSettingsResolverMethod(SettingsResolverDelegate method)
    {
      if (this.settingsResolverDelegate == null)
        this.settingsResolverDelegate = method;
      else
        this.settingsResolverDelegate += method;
    }

    /// <summary>
    /// Resolves the property that do not have values specified to default values.
    /// </summary>
    /// <typeparam name="T">The type of the return value of property to be resolved</typeparam>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    public override T ResolveProperty<T>(string propertyName)
    {
      if (propertyName.IsNullOrEmpty())
        throw new ArgumentNullException(nameof (propertyName));
      if (this.settingsResolverDelegate != null)
      {
        foreach (Delegate invocation in this.settingsResolverDelegate.GetInvocationList())
        {
          object obj1 = invocation.DynamicInvoke();
          if (obj1 != null)
          {
            propertyName = char.ToUpperInvariant(propertyName[0]).ToString() + propertyName.Substring(1);
            PropertyInfo property = obj1.GetType().GetProperty(propertyName);
            if (property != (PropertyInfo) null)
            {
              T obj2 = (T) property.GetValue(obj1, (object[]) null);
              if ((object) obj2 != null)
                return obj2;
            }
          }
        }
      }
      ConfigElement config = this.GetConfig();
      object obj = (object) null;
      if (char.IsLower(propertyName[0]))
      {
        obj = config[propertyName];
      }
      else
      {
        PropertyDescriptor property = TypeDescriptor.GetProperties((object) config)[propertyName];
        if (property != null)
          obj = property.GetValue((object) config);
      }
      return (T) obj;
    }
  }
}
