// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.IMultiDisplayModesSupport
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  /// <summary>
  /// Contains properties that are used for defining read only and hidden modes for form field controls.
  /// </summary>
  public interface IMultiDisplayModesSupport
  {
    /// <summary>
    /// Gets or sets the read only modes. These values are used by the Forms control to determine whether the form field control is in read only mode.
    /// </summary>
    /// <value>The read only modes.</value>
    string[] ReadOnlyModes { get; set; }

    /// <summary>
    /// Gets or sets the hidden modes. These values are used by the Forms control to determine whether the form field control is in hidden mode.
    /// </summary>
    /// <value>The hidden modes.</value>
    string[] HiddenModes { get; set; }
  }
}
