// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.FileSizeStringifier
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Telerik.Sitefinity.Utilities
{
  internal class FileSizeStringifier : IFileSizeStringifier
  {
    private readonly CultureInfo culture;
    private readonly Dictionary<int, string> getPrefix = new Dictionary<int, string>()
    {
      {
        0,
        "B"
      },
      {
        1,
        "KB"
      },
      {
        2,
        "MB"
      },
      {
        3,
        "GB"
      },
      {
        4,
        "TB"
      }
    };

    public FileSizeStringifier() => this.culture = CultureInfo.CurrentCulture;

    public FileSizeStringifier(CultureInfo culture) => this.culture = culture;

    public string GetStringFromFileSize(long fileSizeInBytes)
    {
      int num = fileSizeInBytes > 0L ? (int) Math.Floor(Math.Min(Math.Log((double) fileSizeInBytes, 1024.0), 4.0)) : 0;
      return Math.Round((double) fileSizeInBytes / Math.Pow(1024.0, (double) num), 2).ToString((IFormatProvider) this.culture) + " " + this.getPrefix[num];
    }
  }
}
