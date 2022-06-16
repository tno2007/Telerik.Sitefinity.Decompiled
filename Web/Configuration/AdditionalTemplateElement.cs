// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.AdditionalTemplateElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.Configuration
{
  /// <summary>Defines settings for additional templates.</summary>
  public sealed class AdditionalTemplateElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of AdditionalTemplateElement with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public AdditionalTemplateElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>The key for the additional template.</summary>
    [ConfigurationProperty("key", IsKey = true, IsRequired = true)]
    public string Key
    {
      get => (string) this["key"];
      set => this["key"] = (object) value;
    }

    /// <summary>Defines the path to external template.</summary>
    [ConfigurationProperty("layoutTemplatePath")]
    public string LayoutTemplatePath
    {
      get => (string) this["layoutTemplatePath"];
      set => this["layoutTemplatePath"] = (object) value;
    }
  }
}
