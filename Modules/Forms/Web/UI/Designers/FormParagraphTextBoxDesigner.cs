// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormParagraphTextBoxDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Designers
{
  public class FormParagraphTextBoxDesigner : ContentViewDesignerBase
  {
    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ContentViewDesignerBase).FullName;

    /// <summary>Adds the designer views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      FormParagraphTextBoxLabelView textBoxLabelView = new FormParagraphTextBoxLabelView();
      views.Add(textBoxLabelView.ViewName, (ControlDesignerView) textBoxLabelView);
      FormParagraphTextBoxLimitationsView boxLimitationsView = new FormParagraphTextBoxLimitationsView();
      views.Add(boxLimitationsView.ViewName, (ControlDesignerView) boxLimitationsView);
      FormParagraphTextBoxAppearanceView boxAppearanceView = new FormParagraphTextBoxAppearanceView();
      views.Add(boxAppearanceView.ViewName, (ControlDesignerView) boxAppearanceView);
    }
  }
}
