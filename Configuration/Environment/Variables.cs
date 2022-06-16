// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Environment.Variables
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Configuration.Environment
{
  internal class Variables
  {
    private IDictionary<string, string> settings = (IDictionary<string, string>) new Dictionary<string, string>();
    private IDictionary<string, string> connectionStrings = (IDictionary<string, string>) new Dictionary<string, string>();

    public Variables AddSetting(string key, string value)
    {
      this.Settings[key] = value;
      return this;
    }

    public Variables AddConnectionString(string key, string value)
    {
      this.ConnectionStrings[key] = value;
      return this;
    }

    public IDictionary<string, string> Settings => this.settings;

    public IDictionary<string, string> ConnectionStrings => this.connectionStrings;

    internal void Merge(IEnumerable<Variables> changes)
    {
      foreach (Variables change in changes)
      {
        this.Merge(ref this.settings, change.Settings);
        this.Merge(ref this.connectionStrings, change.ConnectionStrings);
      }
    }

    private void Merge(ref IDictionary<string, string> to, IDictionary<string, string> from)
    {
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) from)
        to[keyValuePair.Key] = keyValuePair.Value;
    }
  }
}
