// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.SortingExpressionEnabledElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>
  /// Represents sorting exrpession configuration elemement that will allow enabling for a configuration item element.
  /// </summary>
  public class SortingExpressionEnabledElement : SortingExpressionBaseElement
  {
    /// <summary>
    /// Initializes new isntance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public SortingExpressionEnabledElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the content type of the sort expression</summary>
    /// <value>The license service URL.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnabledDescription", Title = "EnabledTitle")]
    [ConfigurationProperty("enabled", DefaultValue = false, IsKey = false, IsRequired = false)]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }
  }
}
