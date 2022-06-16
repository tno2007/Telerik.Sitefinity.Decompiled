// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI
{
  /// <summary>Control used to render the form fields of a form.</summary>
  public class FormEntryEditDialog : AjaxDialogBase
  {
    public static readonly string FormEntryEditDialogTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Forms.FormEntryEditDialog.ascx");

    /// <summary>Gets the form entry control.</summary>
    /// <value>The form entry control.</value>
    public IFormEntryEditControl FormEntryControl => this.Container.GetControl<IFormEntryEditControl>("formEntryControl", true);

    /// <summary>
    /// Gets the link navigating back to all items (closing the dialog).
    /// </summary>
    /// <value>The back to all items link.</value>
    public HyperLink BackToAllItemsLink => this.Container.GetControl<HyperLink>("backToAllItemsLink", true);

    /// <summary>Gets the title label control.</summary>
    /// <value>The title label.</value>
    public Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string str = SystemManager.CurrentHttpContext.Request.QueryString["formName"];
      this.FormEntryControl.FormName = !string.IsNullOrEmpty(str) ? str : throw new ArgumentException("No formId specified in the url.", "formName");
      this.SetUpTitle();
    }

    private void SetUpTitle()
    {
      this.BackToAllItemsLink.Text = Res.Get<FormsResources>().BackToFormEntries;
      if (SystemManager.CurrentHttpContext.Request.QueryString["viewName"] == "create")
        this.TitleLabel.Text = string.Format("{0} {1}", (object) Res.Get<FormsResources>().Create, (object) Res.Get<FormsResources>().Response);
      else
        this.TitleLabel.Text = string.Format("{0} {1}", (object) Res.Get<FormsResources>().EditNoTags, (object) Res.Get<FormsResources>().Response);
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FormEntryEditDialog.FormEntryEditDialogTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
