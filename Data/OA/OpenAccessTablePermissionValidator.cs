// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.OpenAccessTablePermissionValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.OpenAccess;

namespace Telerik.Sitefinity.Data.OA
{
  internal class OpenAccessTablePermissionValidator : OpenAccessObjectPermissionValidator
  {
    private const string DATABASE_CREATE_TABLE_PERMISSION_TOKEN = "CREATE TABLE";
    private const string TABLE_CONTROL_PERMISSION_TOKEN = "CONTROL";
    private const string TABLE_ALTER_PERMISSION_TOKEN = "ALTER";

    public OpenAccessTablePermissionValidator(string schemaName, OpenAccessContext context)
      : base(schemaName, context)
    {
    }

    protected override string DatabaseCreateObjectPermissionToken => "CREATE TABLE";

    protected override string ObjectControlPermissionsToken => "CONTROL";

    protected override string ObjectAlterPermissionToken => "ALTER";

    public bool CanCreateTableInCurrentSchema() => this.CanCreateObjectInCurrentSchema();

    public bool CanAlterTable(string tableName) => this.CanAlterObject(tableName);

    public bool CanDropTableInCurrentSchema(string tableName) => this.CanDropObjectInCurrentSchema(tableName);
  }
}
