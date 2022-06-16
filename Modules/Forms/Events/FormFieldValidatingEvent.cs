// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.FormFieldValidatingEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// Event that is raised when validating an form field. Note that this event won't be raised when the Form UseAsyncSubmit is True, because then the field validation is performed on the client side.
  /// Event handlers should throw Telerik.Sitefinity.Services.Events.ValidationException if the form field value is not valid.
  /// </summary>
  public class FormFieldValidatingEvent : IFormFieldValidatingEvent, IFormsModuleEvent, IEvent
  {
    /// <summary>
    /// Gets or sets a value specifying the origin of the event.
    /// </summary>
    public string Origin { get; set; }

    /// <summary>Gets or sets the field that is being validated.</summary>
    public IFormFieldControl FormFieldControl { get; set; }
  }
}
