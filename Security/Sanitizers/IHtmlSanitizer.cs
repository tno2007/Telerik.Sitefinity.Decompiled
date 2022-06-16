// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Sanitizers.IHtmlSanitizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security.Sanitizers
{
  /// <summary>
  /// Defines interface for html sanitizers used in Sitefinity.
  /// </summary>
  public interface IHtmlSanitizer
  {
    /// <summary>
    /// Sanitizes the given html string so that it is safe to display as unencoded html.
    /// </summary>
    /// <param name="html">Html object to be sanitized. ToString method is used to get the html string.</param>
    /// <returns>The sanitized html string.</returns>
    string Sanitize(string html);

    /// <summary>
    /// Sanitizes the given url string. Will encode the parameters if needed and remove the url if the protocol is not allowed or if it is malformed.
    /// </summary>
    /// <param name="url">The url object to be sanitized. ToString method is used to get the url string.</param>
    /// <returns>The sanitized html string.</returns>
    string SanitizeUrl(string url);
  }
}
