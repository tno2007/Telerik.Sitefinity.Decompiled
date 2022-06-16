// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.Enums.MediaType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.PublicControls.Enums
{
  /// <summary>
  /// Media Types allow you to specify how documents will be presented in different media.
  /// </summary>
  [Flags]
  public enum MediaType
  {
    /// <summary>Suitable for all devices.</summary>
    all = 1,
    /// <summary>Intended for braille tactile feedback devices.</summary>
    braille = 2,
    /// <summary>Intended for paged braille printers.</summary>
    embossed = 4,
    /// <summary>
    /// Intended for handheld devices (typically small screen, limited bandwidth).
    /// </summary>
    handheld = 8,
    /// <summary>
    /// Intended for paged material and for documents viewed on screen in print preview mode. Please consult the section on paged media for information about formatting issues that are specific to paged media.
    /// </summary>
    print = 16, // 0x00000010
    /// <summary>
    /// Intended for projected presentations, for example projectors. Please consult the section on paged media for information about formatting issues that are specific to paged media.
    /// </summary>
    projection = 32, // 0x00000020
    /// <summary>Intended primarily for color computer screens.</summary>
    screen = 64, // 0x00000040
    /// <summary>Intended for speech synthesizers.</summary>
    speech = 128, // 0x00000080
    /// <summary>
    /// Intended for media using a fixed-pitch character grid (such as teletypes, terminals, or portable devices with limited display capabilities).
    /// </summary>
    tty = 256, // 0x00000100
    /// <summary>
    /// Intended for television-type devices (low resolution, color, limited-scrollability screens, sound available).
    /// </summary>
    tv = 512, // 0x00000200
  }
}
