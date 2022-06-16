// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.IRedirectUriValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Provides abilities to check if a given uri is trusted for redirect.
  /// </summary>
  public interface IRedirectUriValidator
  {
    /// <summary>
    /// Returns <c>true</c> if the given requestUri is trusted for redirection.
    /// </summary>
    /// <param name="redirectUri">The request URI.</param>
    /// <returns>
    ///   <c>true</c> if the specified request URI is trusted; otherwise, <c>false</c>.
    /// </returns>
    bool IsValid(string redirectUri);
  }
}
