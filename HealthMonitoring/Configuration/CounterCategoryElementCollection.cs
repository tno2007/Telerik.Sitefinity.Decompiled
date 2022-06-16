// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.HealthMonitoring.Configuration.CounterCategoryElementCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;

namespace Telerik.Sitefinity.HealthMonitoring.Configuration
{
  /// <summary>
  /// 
  /// </summary>
  public class CounterCategoryElementCollection : ConfigurationElementCollection
  {
    /// <summary>
    /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
    /// </summary>
    /// <returns>
    /// A new <see cref="T:System.Configuration.ConfigurationElement" />.
    /// </returns>
    protected override ConfigurationElement CreateNewElement() => (ConfigurationElement) new CounterCategoryElement();

    /// <summary>
    /// Gets the element key for a specified configuration element when overridden in a derived class.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
    /// <returns>
    /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
    /// </returns>
    protected override object GetElementKey(ConfigurationElement element) => (object) ((CounterCategoryElement) element).CategoryName;
  }
}
