// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.ViewModeControlSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.Configuration
{
  /// <summary>Defines settings for view host control.</summary>
  public class ViewModeControlSettings : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ViewModeControlSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>The type of the host control.</summary>
    [TypeConverter(typeof (StringTypeConverter))]
    [ConfigurationProperty("hostType", IsKey = true, IsRequired = true)]
    public Type HostType
    {
      get => (Type) this["hostType"];
      set => this["hostType"] = (object) value;
    }

    /// <summary>Defines the path to external template.</summary>
    [ConfigurationProperty("layoutTemplatePath")]
    public string LayoutTemplatePath
    {
      get => (string) this["layoutTemplatePath"];
      set => this["layoutTemplatePath"] = (object) value;
    }

    /// <summary>Defines any additional templates.</summary>
    [ConfigurationProperty("additionalTemplates")]
    public ConfigElementDictionary<string, AdditionalTemplateElement> AdditionalTemplates => (ConfigElementDictionary<string, AdditionalTemplateElement>) this["additionalTemplates"];

    /// <summary>Defines a list of views for the host control.</summary>
    [KeepRemoveClearItems]
    [ConfigurationProperty("views")]
    public ConfigElementList<ViewElement> Views => (ConfigElementList<ViewElement>) this["views"];
  }
}
