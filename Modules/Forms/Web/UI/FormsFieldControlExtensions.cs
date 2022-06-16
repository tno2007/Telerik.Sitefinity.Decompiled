// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.FormsFieldControlExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI
{
  /// <summary>Defines extension methods for various field controls.</summary>
  internal static class FormsFieldControlExtensions
  {
    /// <summary>Gets the field read only by mode.</summary>
    /// <param name="formFieldControl">The form field control.</param>
    /// <param name="mode">The mode.</param>
    /// <returns>Returns value indicating whether the field is in read only mode according to the provided mode</returns>
    public static bool GetFieldReadOnlyByMode(
      this IMultiDisplayModesSupport formFieldControl,
      string mode)
    {
      return !string.IsNullOrWhiteSpace(mode) && formFieldControl != null && formFieldControl.ReadOnlyModes != null && ((IEnumerable<string>) formFieldControl.ReadOnlyModes).Contains<string>(mode);
    }

    /// <summary>Gets the field visible by mode.</summary>
    /// <param name="formFieldControl">The form field control.</param>
    /// <param name="mode">The mode.</param>
    /// <returns>Returns value indicating whether the field is in read only mode according to the provided mode</returns>
    public static bool GetFieldVisibleByMode(
      this IMultiDisplayModesSupport formFieldControl,
      string mode)
    {
      if (string.IsNullOrWhiteSpace(mode))
        return true;
      if (formFieldControl == null)
        return false;
      return formFieldControl.HiddenModes == null || !((IEnumerable<string>) formFieldControl.HiddenModes).Contains<string>(mode);
    }
  }
}
