// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.IFormFieldBackendConfigurator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail
{
  /// <summary>
  /// Classes that implement this interface should prepare form control for display in the back-end.
  /// </summary>
  public interface IFormFieldBackendConfigurator
  {
    /// <summary>Prepares a Form control for display in the back-end.</summary>
    /// <param name="formControl">The form control.</param>
    /// <param name="formId">Id of the form that hosts the field.</param>
    /// <returns>The configured form control.</returns>
    Control ConfigureFormControl(Control formControl, Guid formId);
  }
}
