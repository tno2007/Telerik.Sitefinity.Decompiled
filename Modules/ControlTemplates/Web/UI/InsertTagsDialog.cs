// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.InsertTagsDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.ControlTemplates.Web.UI
{
  /// <summary>Dialog for inserting tag in Template Editor.</summary>
  public class InsertTagsDialog : AjaxDialogBase
  {
    internal const string insertTagsDialogScript = "Telerik.Sitefinity.Modules.ControlTemplates.Web.Scripts.InsertTagsDialog.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ControlTemplates.InsertTagsDialog.ascx");
    private const string textFieldTemplate = "<%#: Eval(\"{0}\")%>";

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (InsertTagsDialog).FullName;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? InsertTagsDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the command bar.</summary>
    /// <value>The command bar.</value>
    protected virtual CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true);

    /// <summary>Gets the field for inserting a tag.</summary>
    /// <value>The field for inserting a tag.</value>
    protected virtual TextField TagField => this.Container.GetControl<TextField>("tagField", true);

    /// <summary>Gets the dialog title label.</summary>
    /// <value>The dialog title label.</value>
    protected virtual Label DialogTitleLabel => this.Container.GetControl<Label>("dialogTitleLabel", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("dialogTitleLabel", this.DialogTitleLabel.ClientID);
      controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      controlDescriptor.AddComponentProperty("tagField", this.TagField.ClientID);
      controlDescriptor.AddProperty("_insertTagsCommandName", (object) "insertTags");
      controlDescriptor.AddProperty("_cancelCommandName", (object) "cancel");
      controlDescriptor.AddProperty("_textFieldTemplate", (object) "<%#: Eval(\"{0}\")%>");
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.ControlTemplates.Web.Scripts.InsertTagsDialog.js", typeof (InsertTagsDialog).Assembly.FullName)
    };
  }
}
