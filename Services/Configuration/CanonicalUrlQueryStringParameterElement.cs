// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Configuration.CanonicalUrlQueryStringParameterElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services.Configuration
{
  /// <summary>
  /// Global settings for the allowed canonical url query string parameter.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Title = "CanonicalUrlQueryStringParameterElementTitle")]
  public class CanonicalUrlQueryStringParameterElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public CanonicalUrlQueryStringParameterElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the query string parameter name that can be preserved in the canonical url.
    /// </summary>
    [ConfigurationProperty("parameterName", IsKey = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CanonicalUrlQyeryStringParameterNameDescription", Title = "CanonicalUrlQyeryStringParameterNameTitle")]
    public string ParameterName
    {
      get => (string) this["parameterName"];
      set => this["parameterName"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropNames
    {
      public const string ParameterName = "parameterName";
    }
  }
}
