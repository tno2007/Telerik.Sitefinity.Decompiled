// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.DatabaseMappingAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Metadata
{
  /// <summary>
  /// Represents attrbiute for setting default database mappings to the <see cref="!:IFormFieldControl" />
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public class DatabaseMappingAttribute : Attribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.DatabaseMappingAttribute" /> class.
    /// </summary>
    /// <param name="databaseMappingKey">The database mapping key.</param>
    public DatabaseMappingAttribute(string databaseMappingKey) => this.DatabaseMappingKey = databaseMappingKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.DatabaseMappingAttribute" /> class.
    /// </summary>
    /// <param name="dbType">Type of the db.</param>
    public DatabaseMappingAttribute(System.Data.DbType dbType) => this.DbType = new System.Data.DbType?(dbType);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.DatabaseMappingAttribute" /> class.
    /// </summary>
    /// <param name="mappingKey">The mapping key.</param>
    public DatabaseMappingAttribute(UserFriendlyDataType mappingKey) => this.DatabaseMappingKey = mappingKey.ToString();

    /// <summary>
    /// Gets or sets the key which will be used the get the database mapping from the forms configuration
    /// </summary>
    public string DatabaseMappingKey { get; private set; }

    /// <summary>
    /// Gets or sets the DbType which will be used to create the metafield
    /// </summary>
    public System.Data.DbType? DbType { get; private set; }
  }
}
