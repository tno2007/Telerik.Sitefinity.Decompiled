// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.HideIfNoTextMode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Gets or sets a value indicating whether the control is hiding from the server or from the client
  /// </summary>
  /// <value><c>"client"</c> if the control is hidden through CSS; if the entire HTML element not to be displayed: <c>"server"</c>.</value>
  public enum HideIfNoTextMode
  {
    /// <summary>Represents the field control hidden from the server</summary>
    Client,
    /// <summary>Represents the field control hidden from the client</summary>
    Server,
  }
}
