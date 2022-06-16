// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Designers.ChoiceFieldDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields.Designers.Views;

namespace Telerik.Sitefinity.Web.UI.Fields.Designers
{
  /// <summary>Content View designer for ChoiceField control.</summary>
  public class ChoiceFieldDesigner : ContentViewDesignerBase
  {
    private bool editMode;

    /// <summary>Sets the property editor advanced mode.</summary>
    protected internal virtual void SetPropertyEditorAdvancedMode() => this.PropertyEditor.HideAdvancedMode = true;

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ContentViewDesignerBase).FullName;

    /// <summary>Adds the designer views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      ChoicesLabelsView choicesLabelsView = new ChoicesLabelsView(this.editMode);
      views.Add(choicesLabelsView.ViewName, (ControlDesignerView) choicesLabelsView);
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.SetupCustomMode();
      this.SetPropertyEditorAdvancedMode();
      base.InitializeControls(container);
    }

    /// <inheritdoc />
    protected override void Customize(string mode)
    {
      base.Customize(mode);
      if (!(mode == "edit"))
        return;
      this.editMode = true;
    }
  }
}
