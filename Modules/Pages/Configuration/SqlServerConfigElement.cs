// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.SqlServerConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>Represents SqlServer configuration element.</summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
  public class SqlServerConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.SqlServerConfigElement" /> class.
    /// </summary>
    /// <param name="parent">Parent configuration element.</param>
    public SqlServerConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.SqlServerConfigElement" /> class.
    /// </summary>
    internal SqlServerConfigElement()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.SqlServerConfigElement" /> class.
    /// </summary>
    /// <param name="check">if set to <c>true</c> [check].</param>
    internal SqlServerConfigElement(bool check)
      : base(check)
    {
    }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("ConnectionString")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = null, Title = "ConnectionStringTitle")]
    public string ConnectionString
    {
      get => (string) this[nameof (ConnectionString)];
      set => this[nameof (ConnectionString)] = (object) value;
    }
  }
}
