// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormControlDisplayModeAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  /// <summary>
  /// Attribute that specifies whether the control should be displayed in the read or write mode of the form
  /// If the attribute is not specified the controls is always shown
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public class FormControlDisplayModeAttribute : Attribute
  {
    private FormControlDisplayMode formControlDisplayMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormControlDisplayModeAttribute" /> class.
    /// </summary>
    /// <param name="formDisplayMode">The form display mode.</param>
    public FormControlDisplayModeAttribute(FormControlDisplayMode formDisplayMode) => this.formControlDisplayMode = formDisplayMode;

    /// <summary>Gets the form control display mode.</summary>
    public FormControlDisplayMode FormControlDisplayMode => this.formControlDisplayMode;
  }
}
