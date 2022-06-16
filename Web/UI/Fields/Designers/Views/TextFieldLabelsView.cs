// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Designers.Views.TextFieldLabelsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views;

namespace Telerik.Sitefinity.Web.UI.Fields.Designers.Views
{
  /// <summary>
  /// The "Label and Tests" view of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Designers.TextFieldDesigner" />.
  /// </summary>
  public class TextFieldLabelsView : FormTextBoxLabelView
  {
    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (FormTextBoxLabelView).FullName;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.ErrorMessageTextField.Style.Add("dispay", "none");
      this.MetaFieldNameTextBox.Visible = false;
    }
  }
}
