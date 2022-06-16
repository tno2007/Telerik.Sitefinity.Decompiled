// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.TextMode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Specifies how the texts in a control is rendered.</summary>
  public enum TextMode
  {
    /// <summary>The texts are not modified.</summary>
    PassThrough,
    /// <summary>The texts are HTML-encoded.</summary>
    Encode,
    /// <summary>
    /// The texts are sanitized, so they are safe to be displayed as unencoded HTML.
    /// </summary>
    Sanitize,
    /// <summary>
    /// The texts are sanitized, so they are safe to be used as urls.
    /// </summary>
    SanitizeUrl,
  }
}
