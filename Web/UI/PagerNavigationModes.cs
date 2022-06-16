// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PagerNavigationModes
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Defines the navigation modes of the pager control</summary>
  public enum PagerNavigationModes
  {
    /// <summary>Pager mode for navigating with post back</summary>
    Postback,
    /// <summary>Pager mode for navigating with links</summary>
    Links,
    /// <summary>Pager mode for throwing events on the client</summary>
    ClientSide,
    /// <summary>Determine the proper navigation mode automatically</summary>
    Auto,
    /// <summary>Using UrlEvaluator</summary>
    Evaluator,
  }
}
