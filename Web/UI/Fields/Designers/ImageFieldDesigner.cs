// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Designers.ImageFieldDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields.Designers.Views;

namespace Telerik.Sitefinity.Web.UI.Fields.Designers
{
  public class ImageFieldDesigner : ContentViewDesignerBase
  {
    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ContentViewDesignerBase).FullName;

    /// <summary>Adds the designer views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      ImageFieldLabelView imageFieldLabelView = new ImageFieldLabelView();
      views.Add(imageFieldLabelView.ViewName, (ControlDesignerView) imageFieldLabelView);
      ImageFieldLimitationsView fieldLimitationsView = new ImageFieldLimitationsView();
      views.Add(fieldLimitationsView.ViewName, (ControlDesignerView) fieldLimitationsView);
      ImageFieldAppearanceView fieldAppearanceView = new ImageFieldAppearanceView();
      views.Add(fieldAppearanceView.ViewName, (ControlDesignerView) fieldAppearanceView);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SetPropertyEditorAdvancedMode();
      base.InitializeControls(container);
    }

    /// <summary>Sets property editor in advanced mode.</summary>
    protected internal virtual void SetPropertyEditorAdvancedMode() => this.PropertyEditor.HideAdvancedMode = false;
  }
}
