// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.MetaFieldProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>
  /// Helper proxy class used only for persistings the dsigner settings
  /// </summary>
  public class MetaFieldProxy : MetaField
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.MetaFieldProxy" /> class.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="dbType">The database type of the field</param>
    /// <param name="clrType">The CLR type of the property</param>
    public MetaFieldProxy(string fieldName, string dbType, string clrType)
    {
      this.FieldName = fieldName;
      this.ClrType = clrType;
      this.DBType = dbType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.MetaFieldProxy" /> class.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="dbType">The database type of the field</param>
    /// <param name="clrType">The CLR type of the property</param>
    /// <param name="applicationName">Name of the application.</param>
    /// <param name="id">The id of the field</param>
    public MetaFieldProxy(
      string fieldName,
      string dbType,
      string clrType,
      string applicationName,
      Guid id)
      : base(applicationName, id)
    {
      this.FieldName = fieldName;
      this.ClrType = clrType;
      this.DBType = dbType;
    }

    public MetaFieldProxy()
    {
    }
  }
}
