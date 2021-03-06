// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Designers.MultipleChoiceFieldDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields.Designers.Views;

namespace Telerik.Sitefinity.Web.UI.Fields.Designers
{
  /// <summary>
  /// Content View designer for multiple ChoiceField control.
  /// </summary>
  public class MultipleChoiceFieldDesigner : ContentViewDesignerBase
  {
    private bool editMode;

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ContentViewDesignerBase).FullName;

    /// <summary>Adds the designer views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      ChoiceFieldLabelsView choiceFieldLabelsView = new ChoiceFieldLabelsView(this.editMode);
      views.Add(choiceFieldLabelsView.ViewName, (ControlDesignerView) choiceFieldLabelsView);
      ChoiceFieldAppearanceView fieldAppearanceView = new ChoiceFieldAppearanceView();
      views.Add(fieldAppearanceView.ViewName, (ControlDesignerView) fieldAppearanceView);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SetupCustomMode();
      this.SetPropertyEditorAdvancedMode();
      base.InitializeControls(container);
    }

    protected override void Customize(string mode)
    {
      base.Customize(mode);
      if (!(mode == "edit"))
        return;
      this.editMode = true;
    }

    /// <summary>Sets the property editor advanced mode.</summary>
    protected internal virtual void SetPropertyEditorAdvancedMode() => this.PropertyEditor.HideAdvancedMode = true;
  }
}
