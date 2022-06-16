// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.SortingExpressionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>Represents sorting exrpession configuration elememnt.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "SortingExpressionElementDescription", Title = "SortingExpressionElementCaption")]
  public class SortingExpressionElement : SortingExpressionBaseElement
  {
    /// <summary>
    /// Initializes new isntance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public SortingExpressionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether this sorting expression instance is custom defined sorting.
    /// </summary>
    /// <value><c>true</c> if this instance is custom; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("isCustom", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsCustomDescription", Title = "IsCustomTitle")]
    public bool IsCustom
    {
      get => (bool) this["isCustom"];
      set => this["isCustom"] = (object) value;
    }
  }
}
