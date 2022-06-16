// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldLabelsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views;

namespace Telerik.Sitefinity.Web.UI.Fields.Designers.Views
{
  /// <summary>
  /// The "Label and Tests" view of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Designers.MultipleChoiceFieldDesigner" />.
  /// </summary>
  public class ChoiceFieldLabelsView : FormChoiceFieldLabelView
  {
    private bool editMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldLabelsView" /> class.
    /// </summary>
    /// <param name="editMode">Whether in edit mode.</param>
    public ChoiceFieldLabelsView(bool editMode = false) => this.editMode = editMode;

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (FormChoiceFieldLabelView).FullName;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.ConfigureView();
      this.MetaFieldNameTextBox.Visible = false;
      this.SortChoicesAlphabeticallyChoiceField.Visible = false;
    }
  }
}
