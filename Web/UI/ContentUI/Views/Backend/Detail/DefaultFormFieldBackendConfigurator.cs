// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DefaultFormFieldBackendConfigurator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail
{
  /// <summary>
  /// Prepares Form controls for display in the back-end. By default there is no preparation needed.
  /// </summary>
  internal class DefaultFormFieldBackendConfigurator : IFormFieldBackendConfigurator
  {
    /// <summary>Prepares a Form control for display in the back-end.</summary>
    /// <param name="formControl">The form control.</param>
    /// <param name="formId">Id of the form that hosts the field.</param>
    /// <returns>The configured form control.</returns>
    public Control ConfigureFormControl(Control formControl, Guid formId)
    {
      if (formControl != null)
      {
        string fullName = formControl.GetType().FullName;
        if (ObjectFactory.IsTypeRegistered<IFormFieldBackendConfigurator>(fullName))
        {
          IFormFieldBackendConfigurator backendConfigurator = ObjectFactory.Resolve<IFormFieldBackendConfigurator>(fullName);
          if (backendConfigurator.GetType() != this.GetType())
            return backendConfigurator.ConfigureFormControl(formControl, formId);
        }
      }
      return formControl;
    }
  }
}
