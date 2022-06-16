// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.AzureOptionsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>
  /// Represents a configuration element that will allow the use of the same identifier generation for Azure as it is for SQL Server.
  /// </summary>
  public class AzureOptionsElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Configuration.AzureOptionsElement" /> class with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public AzureOptionsElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the limitations for the table, column name identifier generation are the same for azure as they are for SQL Server.
    /// </summary>
    [ConfigurationProperty("useMsSqlIdentifierLimitations", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UseMsSqlIdentifierLimitationsDescription", Title = "UseMsSqlIdentifierLimitationsTitle")]
    public bool UseMsSqlIdentifierLimitations
    {
      get => (bool) this["useMsSqlIdentifierLimitations"];
      set => this["useMsSqlIdentifierLimitations"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct Props
    {
      public const string UseMsSqlIdentifierLimitations = "useMsSqlIdentifierLimitations";
    }
  }
}
