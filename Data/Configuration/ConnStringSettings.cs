// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.ConnStringSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Configuration
{
  public class ConnStringSettings : ConfigElement, IConnectionStringSettings
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Configuration.ConnStringSettings" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ConnStringSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Configuration.ConnStringSettings" /> class with name and connectionString
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="name"></param>
    /// <param name="connectionString"></param>
    public ConnStringSettings(ConfigElement parent, string name, string connectionString)
      : this(parent, name, connectionString, "System.Data.SqlClient")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Configuration.ConnStringSettings" /> class with name, connectionString and provider name
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="name"></param>
    /// <param name="connectionString"></param>
    /// <param name="providerName"></param>
    public ConnStringSettings(
      ConfigElement parent,
      string name,
      string connectionString,
      string providerName)
      : base(parent)
    {
      this.Name = name;
      this.ConnectionString = connectionString;
      this.ProviderName = providerName;
    }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Data.Configuration.ConnStringSettings"></see> name.
    /// </summary>
    /// <value></value>
    /// <returns>The string value assigned to the <see cref="P:Telerik.Sitefinity.Data.Configuration.ConnStringSettings.Name"></see> property.</returns>
    [ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the connection string.</summary>
    /// <returns>The string value assigned to the <see cref="P:Telerik.Sitefinity.Data.Configuration.ConnStringSettings.ConnectionString"></see> property.</returns>
    [ConfigurationProperty("connectionString")]
    public string ConnectionString
    {
      get => (string) this["connectionString"];
      set => this["connectionString"] = (object) value;
    }

    /// <summary>Gets or sets the provider name property.</summary>
    /// <returns>Gets or sets the <see cref="P:Telerik.Sitefinity.Data.Configuration.ConnStringSettings.ProviderName"></see> property.</returns>
    [ConfigurationProperty("providerName", DefaultValue = "System.Data.SqlClient")]
    public string ProviderName
    {
      get => (string) this["providerName"];
      set => this["providerName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the type of the database
    /// <list type="Telerik.Sitefinity.Data.Configuration.DatabaseType"></list>
    /// </summary>
    /// <value>The type of the database.</value>
    [ConfigurationProperty("dbType", DefaultValue = DatabaseType.MsSql)]
    public DatabaseType DatabaseType
    {
      get => (DatabaseType) this["dbType"];
      set => this["dbType"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of user-defined parameters for the connection.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }
  }
}
