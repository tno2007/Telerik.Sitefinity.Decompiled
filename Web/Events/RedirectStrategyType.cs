// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.RedirectStrategyType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.Events
{
  /// <summary>
  /// Specifies the available options for handling authentication redirects.
  /// </summary>
  public enum RedirectStrategyType : byte
  {
    /// <summary>Default value. No strategy is applied.</summary>
    None,
    /// <summary>
    /// The redirect login URI is specified at the top level - for the given application.
    /// </summary>
    Global,
    /// <summary>
    /// The redirect login URI is specified at the site level.
    /// </summary>
    Site,
    /// <summary>
    /// The redirect login URI is specified at the page level.
    /// </summary>
    Page,
    /// <summary>The redirect login URI is specified at custom level.</summary>
    Custom,
  }
}
