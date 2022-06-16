// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsDetailView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Backend
{
  internal class CommentsDetailView : DetailViewControl
  {
    private PromptDialog deleteConfirmationDialog;
    internal const string detailFormViewScript = "Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Scripts.CommentsDetailView.js";
    private static string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Comments.CommentsDetailView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsDetailView.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the repeater which binds the sections of the form
    /// </summary>
    protected override Repeater Sections => this.Container.GetControl<Repeater>("sections", true);

    /// <summary>
    /// Gets the reference to the wrapper that holds top toolbar
    /// </summary>
    protected override Control TopToolbarWrapper => this.Container.GetControl<Control>("topToolbarWrapper", true);

    /// <summary>
    /// Gets the reference to the wrapper that holds bottom toolbar
    /// </summary>
    protected override Control BottomToolbarWrapper => this.Container.GetControl<Control>("bottomToolbarWrapper", true);

    /// <summary>Gets the author sidebar.</summary>
    protected AuthorSidebarControl AuthorSidebar => this.Container.GetControl<AuthorSidebarControl>("authorSidebar", false);

    /// <summary>Gets a reference to the message control.</summary>
    protected override Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the loading view.</summary>
    protected override HtmlGenericControl LoadingView => this.Container.GetControl<HtmlGenericControl>("loadingView", true);

    /// <summary>Gets the back button.</summary>
    protected override HtmlAnchor BackButton => this.Container.GetControl<HtmlAnchor>("backButton", true);

    private void CreateConfirmationDialog()
    {
      this.deleteConfirmationDialog = new PromptDialog();
      this.deleteConfirmationDialog.Width = 350;
      this.deleteConfirmationDialog.Height = 300;
      this.deleteConfirmationDialog.Mode = PromptMode.Confirm;
      this.deleteConfirmationDialog.AllowCloseButton = true;
      this.deleteConfirmationDialog.ShowOnLoad = false;
      this.deleteConfirmationDialog.InputRows = 5;
      this.deleteConfirmationDialog.Message = Res.Get<Labels>().QuestionBeforeDeletingItem;
      Collection<CommandToolboxItem> commands1 = this.deleteConfirmationDialog.Commands;
      CommandToolboxItem commandToolboxItem1 = new CommandToolboxItem();
      commandToolboxItem1.CommandName = "delete";
      commandToolboxItem1.CommandType = CommandType.NormalButton;
      commandToolboxItem1.WrapperTagName = "LI";
      commandToolboxItem1.Text = Res.Get<Labels>().YesDelete;
      commandToolboxItem1.CssClass = "sfDelete";
      commands1.Add(commandToolboxItem1);
      Collection<CommandToolboxItem> commands2 = this.deleteConfirmationDialog.Commands;
      CommandToolboxItem commandToolboxItem2 = new CommandToolboxItem();
      commandToolboxItem2.CommandName = "cancel";
      commandToolboxItem2.Text = Res.Get<Labels>().Cancel;
      commandToolboxItem2.CommandType = CommandType.CancelButton;
      commandToolboxItem2.WrapperTagName = "LI";
      commands2.Add(commandToolboxItem2);
      this.Container.Controls.Add((Control) this.deleteConfirmationDialog);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.CreateConfirmationDialog();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) scriptDescriptors.Last<ScriptDescriptor>();
      string absolute = VirtualPathUtility.ToAbsolute("~/RestApi/comments-api");
      controlDescriptor.AddProperty("serviceUrl", (object) absolute);
      if (this.AuthorSidebar != null)
        controlDescriptor.AddComponentProperty("authorSidebar", this.AuthorSidebar.ClientID);
      controlDescriptor.AddComponentProperty("deleteConfirmationDialog", this.deleteConfirmationDialog.ClientID);
      return scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> list = base.GetScriptReferences().ToList<ScriptReference>();
      string fullName = typeof (CommentsDetailView).Assembly.FullName;
      list.Add(new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Scripts.CommentsDetailView.js", fullName));
      list.Add(new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsRestClient.js", fullName));
      return (IEnumerable<ScriptReference>) list;
    }
  }
}
