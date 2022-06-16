// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Environment.EnvironmentVariablesProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Configuration.Environment
{
  internal class EnvironmentVariablesProvider
  {
    private ConcurrentProperty<Variables> variablesCache;
    private const string AppSettingPrefix = "sf-env:";
    private const string ConnectionStringParams = "ConnectionStringParams:";
    private const string ConnectionStringName = "ConnectionStringName";

    public EnvironmentVariablesProvider() => this.variablesCache = new ConcurrentProperty<Variables>(new Func<Variables>(this.GetVariables));

    public virtual Variables Variables => this.variablesCache.Value;

    public virtual void UpdateVariables(Variables changes)
    {
      System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~/");
      bool flag = false;
      if (changes.ConnectionStrings.Any<KeyValuePair<string, string>>())
      {
        ConnectionStringsSection connectionStrings = configuration.ConnectionStrings;
        foreach (KeyValuePair<string, string> connectionString1 in (IEnumerable<KeyValuePair<string, string>>) changes.ConnectionStrings)
        {
          ConnectionStringSettings connectionString2 = connectionStrings.ConnectionStrings[connectionString1.Key];
          if (connectionString2 == null)
          {
            ConnectionStringSettings settings = new ConnectionStringSettings(connectionString1.Key, connectionString1.Value);
            connectionStrings.ConnectionStrings.Add(settings);
          }
          else
            connectionString2.ConnectionString = connectionString1.Value;
        }
        flag = true;
      }
      if (changes.Settings.Any<KeyValuePair<string, string>>())
      {
        AppSettingsSection appSettings = configuration.AppSettings;
        foreach (KeyValuePair<string, string> setting1 in (IEnumerable<KeyValuePair<string, string>>) changes.Settings)
        {
          string key = "sf-env:" + setting1.Key;
          KeyValueConfigurationElement setting2 = appSettings.Settings[key];
          if (setting2 == null)
          {
            KeyValueConfigurationElement keyValue = new KeyValueConfigurationElement(key, setting1.Value);
            appSettings.Settings.Add(keyValue);
          }
          else
            setting2.Value = setting1.Value;
        }
        flag = true;
      }
      if (!flag)
        return;
      configuration.Save(ConfigurationSaveMode.Modified);
      if (SystemManager.CurrentHttpContext != null)
      {
        if (!(SystemManager.HttpContextItems[(object) "sf_env_var"] is IList<Variables> variablesList))
        {
          variablesList = (IList<Variables>) new List<Variables>();
          SystemManager.HttpContextItems[(object) "sf_env_var"] = (object) variablesList;
        }
        variablesList.Add(changes);
      }
      this.variablesCache.Reset();
    }

    protected virtual Variables GetVariables()
    {
      Variables variables = new Variables();
      Dictionary<string, string> dictionary = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (string allKey in ConfigurationManager.AppSettings.AllKeys)
      {
        if (allKey.StartsWith("sf-env:"))
        {
          string key = allKey.Substring("sf-env:".Length);
          if (key.StartsWith("ConnectionStringParams:"))
            dictionary[key.Substring("ConnectionStringParams:".Length)] = ConfigurationManager.AppSettings[allKey];
          else
            variables.AddSetting(key, ConfigurationManager.AppSettings[allKey]);
        }
      }
      foreach (ConnectionStringSettings connectionString1 in (ConfigurationElementCollection) ConfigurationManager.ConnectionStrings)
      {
        string name = connectionString1.Name;
        string connectionString2 = connectionString1.ConnectionString;
        if (dictionary.ContainsKey(name))
          connectionString2 += dictionary[name];
        variables.AddConnectionString(name, connectionString2);
      }
      if (variables.Settings.ContainsKey("ConnectionStringName"))
      {
        string setting = variables.Settings["ConnectionStringName"];
        if (variables.ConnectionStrings.ContainsKey(setting))
          variables.AddConnectionString("Sitefinity", variables.ConnectionStrings[setting]);
      }
      if (SystemManager.CurrentHttpContext != null && SystemManager.HttpContextItems[(object) "sf_env_var"] is IList<Variables> httpContextItem)
        variables.Merge((IEnumerable<Variables>) httpContextItem);
      return variables;
    }
  }
}
