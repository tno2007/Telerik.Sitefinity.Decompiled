// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Environment.EnvironmentVariables
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security.Configuration;

namespace Telerik.Sitefinity.Configuration.Environment
{
  /// <summary>
  /// Helper class for reading environment specific configuration
  /// </summary>
  public class EnvironmentVariables
  {
    private static EnvironmentVariables current;
    private const string EnVarCachKey = "sf_envar_cache";
    private EnvironmentVariablesProvider provider;

    /// <summary>Gets the current instance.</summary>
    public static EnvironmentVariables Current
    {
      get
      {
        if (EnvironmentVariables.current == null)
          EnvironmentVariables.current = new EnvironmentVariables();
        return EnvironmentVariables.current;
      }
    }

    /// <summary>Gets the key that is used to validate encrypted data.</summary>
    /// <returns>The key</returns>
    public string GetDecryptionKey() => this.GetSetting("DecryptionKey") ?? Config.Get<SecurityConfig>(true).DecryptionKey;

    /// <summary>
    /// Gets the key that is used to encrypt and decrypt data.
    /// </summary>
    /// <returns>The key</returns>
    public string GetValidationKey() => this.GetSetting("ValidationKey") ?? Config.Get<SecurityConfig>(true).ValidationKey;

    internal string GetSetting(string key) => this.GetVariable(key, this.Provider.Variables.Settings);

    internal IDictionary<string, string> GetSettings() => this.Provider.Variables.Settings;

    internal EnvironmentVariable GetSettingDetails(string key) => this.GetDetails(key);

    internal IList<EnvironmentVariable> GetSectionSettings(
      string sectionName)
    {
      List<EnvironmentVariable> sectionSettings = new List<EnvironmentVariable>();
      foreach (KeyValuePair<string, string> setting in (IEnumerable<KeyValuePair<string, string>>) this.Provider.Variables.Settings)
      {
        EnvironmentVariable variable;
        if (EnvironmentVariable.TryParse(setting.Key, out variable) && variable.Section == sectionName)
          sectionSettings.Add(variable);
      }
      return (IList<EnvironmentVariable>) sectionSettings;
    }

    internal string GetConnectionString(string key) => this.GetVariable(key, this.Provider.Variables.ConnectionStrings);

    internal UpdateContext Update() => new UpdateContext(this);

    internal virtual EnvironmentVariablesProvider Provider
    {
      get
      {
        if (this.provider == null)
          this.provider = ObjectFactory.Resolve<EnvironmentVariablesProvider>();
        return this.provider;
      }
    }

    private string GetVariable(string key, IDictionary<string, string> dictionary)
    {
      string str;
      return dictionary.TryGetValue(key, out str) ? str : (string) null;
    }

    private EnvironmentVariable GetDetails(string key)
    {
      EnvironmentVariable variable;
      return EnvironmentVariable.TryParse(key, out variable) ? variable : (EnvironmentVariable) null;
    }
  }
}
