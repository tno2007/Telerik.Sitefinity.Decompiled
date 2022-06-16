// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Environment.EnvironmentVariable
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Configuration.Environment
{
  internal class EnvironmentVariable
  {
    private string rawKey;
    private string section;
    private string path;
    private string propertyName;
    private bool isRoot;

    public EnvironmentVariable(
      string rawKey,
      string section,
      string path,
      string propertyName,
      bool isRoot)
    {
      this.rawKey = rawKey;
      this.section = section;
      this.path = path;
      this.propertyName = propertyName;
      this.isRoot = isRoot;
    }

    public static bool TryParse(string key, out EnvironmentVariable variable)
    {
      variable = (EnvironmentVariable) null;
      string[] strArray1 = key.Split(':');
      if (strArray1.Length != 2)
        return false;
      string str = strArray1[0];
      string propertyName = strArray1[1];
      string[] strArray2 = str.Split('/');
      if (strArray2.Length < 1)
        return false;
      string section = strArray2[0];
      bool isRoot = true;
      string path = string.Empty;
      if (strArray2.Length > 1)
      {
        isRoot = false;
        path = str.Sub(section.Length + 1, str.Length - 1);
      }
      variable = new EnvironmentVariable(key, section, path, propertyName, isRoot);
      return true;
    }

    public string RawKey => this.rawKey;

    public string Section => this.section;

    public string Path => this.path;

    public string PropertyName => this.propertyName;

    public bool IsRoot => this.isRoot;
  }
}
