// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Composition.HtmlStripper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Text.RegularExpressions;

namespace Telerik.Sitefinity.Modules.Newsletters.Composition
{
  /// <summary>Methods to remove HTML from strings.</summary>
  public static class HtmlStripper
  {
    /// <summary>Compiled regular expression for performance.</summary>
    private static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

    /// <summary>Remove HTML from string with Regex.</summary>
    public static string StripTagsRegex(string source) => Regex.Replace(source, "<.*?>", string.Empty);

    /// <summary>Remove HTML from string with compiled Regex.</summary>
    public static string StripTagsRegexCompiled(string source) => HtmlStripper._htmlRegex.Replace(source, string.Empty);

    /// <summary>Remove HTML tags from string using char array.</summary>
    public static string StripTagsCharArray(string source)
    {
      char[] chArray = new char[source.Length];
      int length = 0;
      bool flag = false;
      for (int index = 0; index < source.Length; ++index)
      {
        char ch = source[index];
        switch (ch)
        {
          case '<':
            flag = true;
            break;
          case '>':
            flag = false;
            break;
          default:
            if (!flag)
            {
              chArray[length] = ch;
              ++length;
              break;
            }
            break;
        }
      }
      return new string(chArray, 0, length);
    }
  }
}
