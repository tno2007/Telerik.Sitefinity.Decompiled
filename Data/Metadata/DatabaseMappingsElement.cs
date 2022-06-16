// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.DatabaseMappingsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Data.Metadata
{
  /// <summary>
  /// Describes a configuration element for storing predefined metatefield mappings.
  /// Generally used to load defaults when creating forms or dynamic types
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "DatabaseMappingsElementDescription", Title = "DatabaseMappingsElementTitle")]
  [DataContract]
  public class DatabaseMappingsElement : ConfigElement, IDatabaseMapping
  {
    public DatabaseMappingsElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal DatabaseMappingsElement()
      : base(false)
    {
    }

    /// <summary>Gets or sets the programmatic name of the mapping</summary>
    /// <remarks>
    /// This is the key for the collection of mappings, not the name of the field
    /// </remarks>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemName", Title = "ItemNameCaption")]
    [DataMember]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the CLR type of the meta field</summary>
    [ConfigurationProperty("clrType", DefaultValue = "", IsKey = false, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ClrTypeDescription", Title = "ClrTypeCaption")]
    [DataMember]
    public string ClrType
    {
      get => (string) this["clrType"];
      set => this["clrType"] = (object) value;
    }

    /// <summary>Gets or sets the lenght of the database field</summary>
    [ConfigurationProperty("dbLength", DefaultValue = "", IsKey = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DbLengthDescription", Title = "DbLengthCaption")]
    [DataMember]
    public string DbLength
    {
      get => (string) this["dbLength"];
      set => this["dbLength"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("dbPrecision", DefaultValue = "", IsKey = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DbPrecisionDescription", Title = "DbPrecisionCaption")]
    [DataMember]
    public string DbPrecision
    {
      get => (string) this["dbPrecision"];
      set => this["dbPrecision"] = (object) value;
    }

    /// <summary>Gets or sets the scale for the specified data type</summary>
    [ConfigurationProperty("dbScale", DefaultValue = "", IsKey = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DbScaleDescription", Title = "DbScaleCaption")]
    [DataMember]
    public string DbScale
    {
      get => (string) this["dbScale"];
      set => this["dbScale"] = (object) value;
    }

    /// <summary>Gets or sets SQL server-specific data type of a field</summary>
    [ConfigurationProperty("dbSqlType", DefaultValue = "", IsKey = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DbSqlTypeDescription", Title = "DbSqlTypeCaption")]
    [DataMember]
    public string DbSqlType
    {
      get => (string) this["dbSqlType"];
      set => this["dbSqlType"] = (object) value;
    }

    /// <summary>Specifies the data type of a field</summary>
    [ConfigurationProperty("dbType", DefaultValue = "", IsKey = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DbTypeDescription", Title = "DbTypeCaption")]
    [DataMember]
    public string DbType
    {
      get => (string) this["dbType"];
      set => this["dbType"] = (object) value;
    }

    /// <summary>Determines whether the column allows null values</summary>
    [ConfigurationProperty("nullable", DefaultValue = false, IsKey = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NullableDescription", Title = "NullableCaption")]
    [DataMember]
    public bool Nullable
    {
      get => Convert.ToBoolean(this["nullable"]);
      set => this["nullable"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("indexed", DefaultValue = false, IsKey = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IndexedDescription", Title = "IndexedCaption")]
    [DataMember]
    public bool Indexed
    {
      get => Convert.ToBoolean(this["indexed"]);
      set => this["indexed"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("columnName", DefaultValue = "", IsKey = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ColumnNameDescription", Title = "ColumnNameCaption")]
    [DataMember]
    public string ColumnName
    {
      get => (string) this["columnName"];
      set => this["columnName"] = (object) value;
    }

    [ConfigurationProperty("additionalDbTypeChoices", DefaultValue = "", IsKey = false, IsRequired = false)]
    [DataMember]
    public string AdditionalDbTypeChoices
    {
      get => (string) this["additionalDbTypeChoices"];
      set => this["additionalDbTypeChoices"] = (object) value;
    }
  }
}
