// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.ViewContainerDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  internal class ViewContainerDialog : AjaxDialogBase
  {
    public static readonly string contentViewEditDialogTemplate = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ViewContainerDialog.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ViewContainerDialog.contentViewEditDialogTemplate : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the <see cref="!:ContentView" /> control in this dialog.
    /// </summary>
    protected internal virtual ViewContainer ViewContainerControl => this.Container.GetControl<ViewContainer>("viewContainer", true);

    protected override void InitializeControls(GenericContainer container) => this.ViewContainerControl.ControlDefinitionName = SystemManager.CurrentHttpContext.Request.QueryString["ControlDefinitionName"];

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
