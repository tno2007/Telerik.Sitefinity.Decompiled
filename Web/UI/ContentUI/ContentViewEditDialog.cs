// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ContentViewEditDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// Dialog for editing all data items that inherit <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /> abstract type.
  /// </summary>
  public class ContentViewEditDialog : AjaxDialogBase
  {
    public static readonly string contentViewEditDialogTemplate = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentUI.ContentViewDialog.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ContentViewEditDialog.contentViewEditDialogTemplate : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentView" /> control in this dialog.
    /// </summary>
    protected internal virtual ContentView ContentViewControl => this.Container.GetControl<ContentView>("contentView", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ContentViewControl.ControlDefinitionName = SystemManager.CurrentHttpContext.Request.QueryString["ControlDefinitionName"];
      this.ContentViewControl.DetailViewName = SystemManager.CurrentHttpContext.Request.QueryString["ViewName"];
      string str = SystemManager.CurrentHttpContext.Request.QueryString["IsInlineEditingMode"];
      if (str == null)
        return;
      SystemManager.SetInlineEditingMode(bool.Parse(str));
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
