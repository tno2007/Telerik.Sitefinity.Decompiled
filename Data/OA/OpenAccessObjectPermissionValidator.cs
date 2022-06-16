// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.OpenAccessObjectPermissionValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace Telerik.Sitefinity.Data.OA
{
  internal abstract class OpenAccessObjectPermissionValidator : OpenAccessPermissionValidatorBase
  {
    private const string SCHEMA_ALTER_PERMISSION_TOKEN = "ALTER";
    private bool? canAlterSchema;
    private bool? canCreateObjectInDatabase;
    private IList<OpenAccessPermissionQueryResult> schemaPermissions;
    private IList<OpenAccessPermissionQueryResult> databasePermissions;

    public OpenAccessObjectPermissionValidator(string schemaName, OpenAccessContext context)
      : base(schemaName, context)
    {
      this.canCreateObjectInDatabase = new bool?();
      this.canAlterSchema = new bool?();
    }

    protected string CanAlterSchemaPermissionToken => "ALTER";

    protected abstract string DatabaseCreateObjectPermissionToken { get; }

    protected abstract string ObjectControlPermissionsToken { get; }

    protected abstract string ObjectAlterPermissionToken { get; }

    protected bool CanAlterSchema
    {
      get
      {
        if (!this.canAlterSchema.HasValue)
          this.canAlterSchema = new bool?(this.SchemaPermissions.Any<OpenAccessPermissionQueryResult>((Func<OpenAccessPermissionQueryResult, bool>) (x => x.permission_name == this.CanAlterSchemaPermissionToken)));
        return this.canAlterSchema.Value;
      }
    }

    protected bool CanCreateObjectInDatabase
    {
      get
      {
        if (!this.canCreateObjectInDatabase.HasValue)
          this.canCreateObjectInDatabase = new bool?(this.DatabasePermissions.Any<OpenAccessPermissionQueryResult>((Func<OpenAccessPermissionQueryResult, bool>) (x => x.permission_name == this.DatabaseCreateObjectPermissionToken)));
        return this.canCreateObjectInDatabase.Value;
      }
    }

    protected IList<OpenAccessPermissionQueryResult> DatabasePermissions
    {
      get
      {
        if (this.databasePermissions == null)
          this.LoadDatabasePermissions();
        return this.databasePermissions;
      }
    }

    protected IList<OpenAccessPermissionQueryResult> SchemaPermissions
    {
      get
      {
        if (this.schemaPermissions == null)
          this.LoadSchemaPermissions();
        return this.schemaPermissions;
      }
    }

    public override void ChangeSchema(string schemaName)
    {
      this.SchemaName = schemaName;
      this.EvictCache();
    }

    public void EvictCache()
    {
      this.schemaPermissions = (IList<OpenAccessPermissionQueryResult>) null;
      this.databasePermissions = (IList<OpenAccessPermissionQueryResult>) null;
      this.canAlterSchema = new bool?();
      this.canCreateObjectInDatabase = new bool?();
    }

    protected virtual bool CanCreateObjectInCurrentSchema() => this.CanCreateObjectInDatabase && this.CanAlterSchema;

    protected virtual bool CanAlterObject(string targetName) => this.GetPerObjectPermissions(targetName).Any<OpenAccessPermissionQueryResult>((Func<OpenAccessPermissionQueryResult, bool>) (x => x.permission_name == this.ObjectAlterPermissionToken));

    protected virtual bool CanDropObjectInCurrentSchema(string tableName) => this.GetPerObjectPermissions(tableName).Any<OpenAccessPermissionQueryResult>((Func<OpenAccessPermissionQueryResult, bool>) (x => x.permission_name == this.ObjectControlPermissionsToken));

    protected virtual void LoadSchemaPermissions() => this.schemaPermissions = this.ExecutePermissionQuery(this.SchemaName, "SCHEMA");

    private void LoadDatabasePermissions() => this.databasePermissions = this.ExecutePermissionQuery((string) null, "DATABASE");

    private IList<OpenAccessPermissionQueryResult> GetPerObjectPermissions(
      string objectName)
    {
      return this.ExecutePermissionQuery(this.GetObjectFullName(objectName), "OBJECT");
    }

    private string GetObjectFullName(string objectName) => string.Format("[{0}].[{1}]", (object) this.SchemaName, (object) objectName);
  }
}
