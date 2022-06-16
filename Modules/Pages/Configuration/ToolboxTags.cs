// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ToolboxTags
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>Some common toolbox tag constants.</summary>
  public static class ToolboxTags
  {
    /// <summary>Widgets visible only in the backend page editor.</summary>
    public const string Backend = "backend";

    internal static ISet<string> Parse(string tags)
    {
      IEqualityComparer<string> cultureIgnoreCase = (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase;
      if (string.IsNullOrEmpty(tags))
        return (ISet<string>) new HashSet<string>(cultureIgnoreCase);
      return (ISet<string>) new HashSet<string>(((IEnumerable<string>) tags.Split(',')).Select<string, string>((Func<string, string>) (t => t.Trim())), cultureIgnoreCase);
    }

    internal static string ToString(ISet<string> tags) => string.Join(", ", (IEnumerable<string>) tags);
  }
}
