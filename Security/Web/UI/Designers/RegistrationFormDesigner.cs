// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Designers.RegistrationFormDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Security.Web.UI.Designers
{
  public class RegistrationFormDesigner : ContentViewDesignerBase
  {
    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ContentViewDesignerBase).FullName;

    /// <summary>Adds the designer views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      RegistrationFormGeneralView registrationFormGeneralView = new RegistrationFormGeneralView();
      views.Add(registrationFormGeneralView.ViewName, (ControlDesignerView) registrationFormGeneralView);
      RegistrationFormAccountActivationView accountActivationView = new RegistrationFormAccountActivationView();
      views.Add(accountActivationView.ViewName, (ControlDesignerView) accountActivationView);
    }
  }
}
