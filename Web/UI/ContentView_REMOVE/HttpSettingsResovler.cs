// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.HttpSettingsResovler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;

namespace Telerik.Sitefinity.Web.UI
{
  public class HttpSettingsResovler : ISettingsResolver
  {
    private NameValueCollection settings;
    private Dictionary<string, string> keyMappings;

    public Dictionary<string, string> KeyMappings
    {
      set
      {
        if (this.keyMappings == null)
          this.keyMappings = new Dictionary<string, string>();
        this.keyMappings = value;
      }
      get => this.keyMappings;
    }

    public HttpSettingsResovler(NameValueCollection settingsCollection) => this.settings = settingsCollection;

    public HttpSettingsResovler(
      NameValueCollection settingsCollection,
      Dictionary<string, string> keyMaps)
    {
      this.settings = settingsCollection;
      this.keyMappings = keyMaps;
    }

    public void AddKeyMapping(string fromKey, string toKey) => this.KeyMappings.Add(fromKey, toKey);

    public object ResolveSetting(string name)
    {
      string name1 = name;
      if (this.keyMappings != null && this.keyMappings.ContainsKey(name))
        name1 = this.keyMappings[name];
      return (object) this.settings[name1];
    }
  }
}
