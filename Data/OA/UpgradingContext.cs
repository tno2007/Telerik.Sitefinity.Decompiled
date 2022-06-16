// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.UpgradingContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.OA
{
  /// <summary>Provides database context</summary>
  public class UpgradingContext : OpenAccessContext
  {
    private readonly OpenAccessConnection connection;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.UpgradingContext" /> class.
    /// </summary>
    /// <param name="fromContext">From context.</param>
    internal UpgradingContext(UpgradingContext fromContext)
      : base((OpenAccessContextBase) fromContext)
    {
      this.connection = fromContext.Connection;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.UpgradingContext" /> class.
    /// </summary>
    /// <param name="fromContext">From context.</param>
    internal UpgradingContext(SitefinityOAContext fromContext)
      : base((OpenAccessContextBase) fromContext)
    {
      this.connection = fromContext.OpenAccessConnection;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.SitefinityOAContext" /> class.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="backendConfiguration">The backend configuration.</param>
    /// <param name="metadataContainer">The metadata container.</param>
    public UpgradingContext(
      string connectionString,
      OpenAccessConnection connection,
      MetadataContainer metadataContainer)
      : base(connectionString, connection.Backend, metadataContainer)
    {
      this.connection = connection;
    }

    /// <summary>Executes the SQL script into the database.</summary>
    /// <remarks>
    /// When using this method, ensure that you don't have opened scopes.
    /// </remarks>
    /// <param name="sql">The SQL.</param>
    public void ExecuteSQL(string sql) => this.GetSchemaHandler().ForceExecuteDDLScript(sql);

    /// <summary>Gets the connection.</summary>
    /// <value>The connection.</value>
    public OpenAccessConnection Connection => this.connection;

    public IDatabaseMappingContext DatabaseContext => this.connection.GetFluentMappingContext();
  }
}
