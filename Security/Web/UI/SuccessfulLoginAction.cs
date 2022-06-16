// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.SuccessfulLoginAction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// Indicates what should the login form do when a user finally logs in sucessfully
  /// </summary>
  public enum SuccessfulLoginAction
  {
    /// <summary>
    /// Redirect to a page indicated by the RedirectUrl querystring variable or Sitefinity's backend root page
    /// </summary>
    Redirect,
    /// <summary>Close (with JavaScript) the RadWindow popup.</summary>
    CloseWindow,
  }
}
