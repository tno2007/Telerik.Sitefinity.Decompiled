// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.FormFieldValidationEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI
{
  /// <summary>
  /// Event arguments for validation of a field control
  /// If the event is Validated the result from the validation is taken, otherwise the standard processing is applied
  /// If the event is canceled the whole validation process is canceled - no more controls will be validated
  /// </summary>
  public class FormFieldValidationEventArgs : CancelEventArgs
  {
    public FormFieldValidationEventArgs(IFormFieldControl formFieldControl)
    {
      this.FormFieldControl = formFieldControl;
      this.IsValid = true;
      this.Validated = false;
    }

    /// <summary>Represents the field that is being validated</summary>
    public IFormFieldControl FormFieldControl { get; set; }

    /// <summary>
    /// Gets/sets the result from the validation of the control
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Represents whether the field was validated by the event
    /// If true the IsValid state is taken as a result of the validation
    /// </summary>
    /// <value>The validated.</value>
    public bool Validated { get; set; }
  }
}
