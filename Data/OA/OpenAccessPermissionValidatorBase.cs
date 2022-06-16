// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.OpenAccessPermissionValidatorBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Data.Common;
using Telerik.OpenAccess;

namespace Telerik.Sitefinity.Data.OA
{
  internal abstract class OpenAccessPermissionValidatorBase
  {
    private OpenAccessContext context;

    public OpenAccessPermissionValidatorBase(string schemaName, OpenAccessContext context)
    {
      this.context = context;
      this.SchemaName = schemaName;
    }

    protected string SchemaName { get; set; }

    public abstract void ChangeSchema(string schemaName);

    protected IList<OpenAccessPermissionQueryResult> ExecutePermissionQuery(
      string securable,
      string secuarableClass)
    {
      System.Data.SqlClient.SqlParameter sqlParameter1 = new System.Data.SqlClient.SqlParameter(nameof (securable), (object) securable);
      if (securable == null)
        sqlParameter1.Value = (object) DBNull.Value;
      System.Data.SqlClient.SqlParameter sqlParameter2 = new System.Data.SqlClient.SqlParameter(nameof (secuarableClass), (object) secuarableClass);
      return this.context.ExecuteQuery<OpenAccessPermissionQueryResult>(string.Format("SELECT * FROM fn_my_permissions (@{0}, @{1});", (object) nameof (securable), (object) nameof (secuarableClass)), (DbParameter) sqlParameter1, (DbParameter) sqlParameter2);
    }
  }
}
