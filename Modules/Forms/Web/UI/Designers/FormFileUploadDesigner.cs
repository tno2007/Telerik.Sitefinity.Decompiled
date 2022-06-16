// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormFileUploadDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Designers
{
  internal class FormFileUploadDesigner : ContentViewDesignerBase
  {
    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ContentViewDesignerBase).FullName;

    /// <inheritdoc />
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      FormFileUploadLabelView fileUploadLabelView = new FormFileUploadLabelView();
      views.Add(fileUploadLabelView.ViewName, (ControlDesignerView) fileUploadLabelView);
      FormFileUploadLimitationsView uploadLimitationsView = new FormFileUploadLimitationsView();
      views.Add(uploadLimitationsView.ViewName, (ControlDesignerView) uploadLimitationsView);
      FormFileUploadAppearanceView uploadAppearanceView = new FormFileUploadAppearanceView();
      views.Add(uploadAppearanceView.ViewName, (ControlDesignerView) uploadAppearanceView);
    }
  }
}
