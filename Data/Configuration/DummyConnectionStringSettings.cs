// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.DummyConnectionStringSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Configuration
{
  internal class DummyConnectionStringSettings : IConnectionStringSettings
  {
    private string name;
    private string providerName;
    private string connectionString;
    private DatabaseType databaseType;
    private NameValueCollection parameters;

    internal static DummyConnectionStringSettings FromString(
      string settingsString)
    {
      string[] strArray = settingsString.Split('\n');
      DummyConnectionStringSettings connectionStringSettings = new DummyConnectionStringSettings(strArray[0]);
      connectionStringSettings.ConnectionString = strArray[1];
      connectionStringSettings.DatabaseType = (DatabaseType) Enum.Parse(typeof (DatabaseType), strArray[2]);
      if (strArray.Length > 3)
        connectionStringSettings.ProviderName = strArray[3];
      return connectionStringSettings;
    }

    internal static string ToString(IConnectionStringSettings settings) => string.Format("{0}\n{1}\n{2}\n{3}", (object) settings.Name, (object) settings.ConnectionString, (object) settings.DatabaseType, (object) settings.ProviderName);

    public DummyConnectionStringSettings(string name)
      : this(name, string.Empty, "System.Data.SqlClient")
    {
    }

    public DummyConnectionStringSettings(string name, string connectionString)
      : this(name, connectionString, "System.Data.SqlClient")
    {
    }

    public DummyConnectionStringSettings(string name, string connectionString, string providerName)
    {
      this.name = name;
      this.connectionString = connectionString;
      this.providerName = providerName;
    }

    public string ConnectionString
    {
      get => this.connectionString;
      set => this.connectionString = value;
    }

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    public string ProviderName
    {
      get => this.providerName;
      set => this.providerName = value;
    }

    public DatabaseType DatabaseType
    {
      get => this.databaseType;
      set => this.databaseType = value;
    }

    public NameValueCollection Parameters
    {
      get
      {
        if (this.parameters == null)
          this.parameters = new NameValueCollection();
        return this.parameters;
      }
    }
  }
}
