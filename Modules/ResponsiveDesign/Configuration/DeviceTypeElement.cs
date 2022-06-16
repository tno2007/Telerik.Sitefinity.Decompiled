// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.DeviceTypeElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration
{
  /// <summary>
  /// The configuration element that represents a single device type.
  /// </summary>
  public class DeviceTypeElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public DeviceTypeElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.DeviceTypeElement" /> class.
    /// </summary>
    internal DeviceTypeElement()
      : base(false)
    {
    }

    /// <summary>Gets or sets the name of the device type.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "DeviceTypeNameConfig")]
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the title of the device type.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "DeviceTypeTitleConfig")]
    [ConfigurationProperty("title")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the class used to localize the labels of this class.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "ResourceClassNameConfig")]
    [ConfigurationProperty("resourceClassName")]
    public string ResourceClassName
    {
      get => (string) this["resourceClassName"];
      set => this["resourceClassName"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of default media query rules for the given device type.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "DefaultRulesConfig")]
    [ConfigurationProperty("defaultRules")]
    public virtual ConfigElementDictionary<string, MediaQueryRuleElement> DefaultRules => (ConfigElementDictionary<string, MediaQueryRuleElement>) this["defaultRules"];

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Properties
    {
      public const string Name = "name";
      public const string Title = "title";
      public const string ResourceClassName = "resourceClassName";
      public const string DefaultRules = "defaultRules";
    }
  }
}
