// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.ExternalConnStringSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>
  /// 
  /// </summary>
  public class ExternalConnStringSettings : IConnectionStringSettings
  {
    private string connectionString;
    private ConnectionStringSettings settings;
    private DatabaseType databaseType;
    private NameValueCollection parameters;

    public ExternalConnStringSettings(ConnectionStringSettings settings)
    {
      this.settings = settings;
      bool flag = false;
      IDictionary<string, string> parameters = ExternalConnStringSettings.GetParameters(settings.ConnectionString);
      if (parameters.ContainsKey("Backend"))
      {
        this.databaseType = OABackendNameAttribute.GetDatabaseTypeOrDefault(parameters["Backend"]);
        parameters.Remove("Backend");
        flag = true;
      }
      List<string> stringList = new List<string>();
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) parameters)
      {
        if (keyValuePair.Key.StartsWith("SF_"))
        {
          this.Parameters[keyValuePair.Key.Substring(3)] = parameters[keyValuePair.Key];
          stringList.Add(keyValuePair.Key);
        }
      }
      if (stringList.Count > 0)
      {
        foreach (string key in stringList)
          parameters.Remove(key);
        flag = true;
      }
      if (!flag)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) parameters)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(";");
        stringBuilder.Append(keyValuePair.Key);
        stringBuilder.Append("=");
        stringBuilder.Append(keyValuePair.Value);
      }
      this.connectionString = stringBuilder.ToString();
    }

    public ConnectionStringSettings ExternalSettings => this.settings;

    /// <summary>Gets or sets the connection string.</summary>
    /// <value></value>
    public string ConnectionString
    {
      get => this.connectionString != null ? this.connectionString : this.settings.ConnectionString;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets the key name of the connection string element.
    /// </summary>
    /// <value></value>
    public string Name
    {
      get => this.settings.Name;
      set
      {
      }
    }

    /// <summary>Gets or sets the provider name property</summary>
    /// <value></value>
    public string ProviderName
    {
      get => !string.IsNullOrEmpty(this.settings.ProviderName) ? this.settings.ProviderName : "System.Data.SqlClient";
      set
      {
      }
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

    internal static IDictionary<string, string> GetParameters(string connectionString)
    {
      string[] strArray1 = connectionString.Split(new char[1]
      {
        ';'
      }, StringSplitOptions.RemoveEmptyEntries);
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      foreach (string str in strArray1)
      {
        char[] separator = new char[1]{ '=' };
        string[] strArray2 = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        if (strArray2.Length == 2)
          parameters.Add(strArray2[0], strArray2[1]);
      }
      return (IDictionary<string, string>) parameters;
    }
  }
}
