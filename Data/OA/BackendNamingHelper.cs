// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.BackendNamingHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.OA
{
  internal class BackendNamingHelper
  {
    private readonly BackendNamingSettings settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.BackendNamingHelper" /> class.
    /// </summary>
    /// <param name="dbType">Type of the db.</param>
    public BackendNamingHelper(DatabaseType dbType) => this.settings = BackendSpecifics.GetNamingSettingsForBackend(dbType);

    public static string Shrink(string name, int maxlen)
    {
      int length = name.Length;
      if (length <= maxlen)
        return name;
      int num = length - maxlen;
      StringBuilder stringBuilder = new StringBuilder(maxlen);
      stringBuilder.Append(name[0]);
      int startIndex = 1;
      while (num > 0 && startIndex < length)
      {
        char ch = name[startIndex++];
        switch (ch)
        {
          case 'a':
          case 'e':
          case 'i':
          case 'o':
          case 'u':
            --num;
            continue;
          default:
            stringBuilder.Append(ch);
            continue;
        }
      }
      if (num == 0)
        stringBuilder.Append(name.Substring(startIndex));
      if (stringBuilder.Length > maxlen)
        stringBuilder.Length = maxlen;
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets short, valid and unique table name from suggested name and list of already used names.
    /// </summary>
    /// <param name="originalName">The original name.</param>
    /// <param name="reservedNames">The reserved names.</param>
    /// <returns></returns>
    public string GetShortAndUniqueTableName(string originalName, IList<string> reservedNames) => this.GetUniqueName(this.ShrinkName(originalName), (IEnumerable<string>) reservedNames, this.settings.MaxTableNameLength);

    /// <summary>Shrinks the name.</summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public string ShrinkName(string name) => this.ShrinkName(name, (Func<BackendNamingSettings, int>) (x => x.MaxTableNameLength));

    /// <summary>Shrinks the name.</summary>
    /// <param name="name">The name.</param>
    /// <param name="maxLenghtFunc">The max lenght func.</param>
    /// <returns></returns>
    public string ShrinkName(string name, Func<BackendNamingSettings, int> maxLenghtFunc)
    {
      int maxlen = maxLenghtFunc(this.settings);
      string key = this.ShrinkName(name, maxlen);
      return this.settings.ReservedWords != null && this.settings.ReservedWords.ContainsKey(key) ? this.settings.ReservedWords[key] : key;
    }

    /// <summary>Gets unique name.</summary>
    /// <param name="name">The name.</param>
    /// <param name="reservedNames">The reserved names.</param>
    /// <param name="maxLength">The max allowed length.</param>
    /// <returns></returns>
    public string GetUniqueName(string name, IEnumerable<string> reservedNames, int maxLength)
    {
      Func<IEnumerable<string>, string, bool> func = (Func<IEnumerable<string>, string, bool>) ((source, target) => source.Any<string>((Func<string, bool>) (x => string.Equals(x, target))));
      if (!func(reservedNames, name))
        return name;
      int num = 1;
      StringBuilder stringBuilder = new StringBuilder();
      if (name.Length == maxLength)
        stringBuilder.Append(name.Substring(0, name.Length - 1));
      else
        stringBuilder.Append(name);
      stringBuilder.Append(num);
      while (func(reservedNames, stringBuilder.ToString()))
      {
        ++num;
        int length = num.ToString().Length;
        int startIndex = stringBuilder.Length - length;
        stringBuilder = stringBuilder.Remove(startIndex, length);
        stringBuilder.Append(num);
      }
      return stringBuilder.ToString();
    }

    /// <summary>Shrinks the name.</summary>
    /// <param name="name">The name.</param>
    /// <param name="maxlen">The max allowed length.</param>
    /// <returns></returns>
    public string ShrinkName(string name, int maxlen)
    {
      if (!this.settings.NameCanStartWithUnderscore)
      {
        int length = name.Length;
        int num = 0;
        while (num < length && name[num] == '_')
          ++num;
        name = name.Substring(num, length - num);
      }
      return BackendNamingHelper.Shrink(name, maxlen);
    }
  }
}
