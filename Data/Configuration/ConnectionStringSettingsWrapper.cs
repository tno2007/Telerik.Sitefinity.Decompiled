// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.ConnectionStringSettingsWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Configuration
{
  internal class ConnectionStringSettingsWrapper : IConnectionStringSettings
  {
    private string name;
    private IConnectionStringSettings connectionStringSettings;

    public ConnectionStringSettingsWrapper(
      string name,
      IConnectionStringSettings connectionStringSettings)
    {
      this.name = name;
      this.connectionStringSettings = connectionStringSettings;
    }

    public string ConnectionString
    {
      get => this.connectionStringSettings.ConnectionString;
      set
      {
      }
    }

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    public string ProviderName
    {
      get => this.connectionStringSettings.ProviderName;
      set
      {
      }
    }

    public DatabaseType DatabaseType
    {
      get => this.connectionStringSettings.DatabaseType;
      set
      {
      }
    }

    public NameValueCollection Parameters => this.connectionStringSettings.Parameters;
  }
}
