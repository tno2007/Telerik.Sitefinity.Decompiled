// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.ResourceParser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Text.RegularExpressions;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal static class ResourceParser
  {
    private static Regex regex = new Regex("\\{\\$([a-zA-Z0-9\\s]+?),([a-zA-Z0-9\\s]+?)\\$\\}", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static string ParseResources(string inputValue) => ResourceParser.regex.Replace(inputValue, new MatchEvaluator(ResourceParser.ReplaceResource));

    private static string ReplaceResource(Match m) => Res.Get(m.Groups[1].Value.Trim(), m.Groups[2].Value.Trim());
  }
}
