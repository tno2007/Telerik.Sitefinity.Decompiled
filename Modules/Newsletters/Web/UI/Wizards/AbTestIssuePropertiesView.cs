// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestIssuePropertiesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards
{
  /// <summary>A view that presents</summary>
  public class AbTestIssuePropertiesView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.AbTestIssuePropertiesView.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.AbTestIssuePropertiesView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? AbTestIssuePropertiesView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets a reference to the back link.</summary>
    protected virtual LinkButton BackLink => this.Container.GetControl<LinkButton>("backLink", true);

    /// <summary>Gets a reference to the issue properties wrapper.</summary>
    protected virtual HtmlContainerControl IssuePropertiesWrapper => this.Container.GetControl<HtmlContainerControl>("issuePropertiesWrapper", true);

    /// <summary>Gets a reference to the loading view.</summary>
    protected virtual HtmlContainerControl LoadingView => this.Container.GetControl<HtmlContainerControl>("loadingView", true);

    /// <summary>Gets the dialog title.</summary>
    protected virtual SitefinityLabel DialogTitle => this.Container.GetControl<SitefinityLabel>("dialogTitle", true);

    /// <summary>Gets the cancel link.</summary>
    protected virtual LinkButton CancelLink => this.Container.GetControl<LinkButton>("cancelLink", true);

    /// <summary>Gets the buttons panel.</summary>
    protected virtual HtmlContainerControl ButtonsPanel => this.Container.GetControl<HtmlContainerControl>("buttonsPanel", true);

    /// <summary>Gets the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the create and close label.</summary>
    protected virtual SitefinityLabel CreateAndCloseLabel => this.Container.GetControl<SitefinityLabel>("createAndCloseLabel", true);

    /// <summary>Gets the create and close button.</summary>
    protected virtual HtmlAnchor CreateAndCloseButton => this.Container.GetControl<HtmlAnchor>("createAndCloseButton", true);

    /// <summary>Gets the go to content button.</summary>
    protected virtual HtmlAnchor GoToContentButton => this.Container.GetControl<HtmlAnchor>("goToContentButton", true);

    /// <summary>Gets the name text field.</summary>
    protected virtual TextField NameTextField => this.Container.GetControl<TextField>("nameTextField", true);

    /// <summary>Gets the name text field read.</summary>
    protected virtual TextField NameTextFieldRead => this.Container.GetControl<TextField>("nameTextFieldRead", true);

    /// <summary>Gets the message subject text field.</summary>
    protected virtual TextField MessageSubjectTextField => this.Container.GetControl<TextField>("messageSubjectTextField", true);

    /// <summary>Gets from name text field.</summary>
    protected virtual TextField FromNameTextField => this.Container.GetControl<TextField>("fromNameTextField", true);

    /// <summary>Gets the reply to email text field.</summary>
    protected virtual TextField ReplyToEmailTextField => this.Container.GetControl<TextField>("replyToEmailTextField", true);

    /// <summary>Gets a reference to the selected lists element.</summary>
    protected virtual HtmlContainerControl SelectedListsElement => this.Container.GetControl<HtmlContainerControl>("selectedLists", true);

    /// <summary>Gets a reference to the select list button.</summary>
    protected virtual LinkButton SelectListButton => this.Container.GetControl<LinkButton>("selectListsButton", true);

    /// <summary>
    /// Gets a reference to the mailing list error message container.
    /// </summary>
    protected virtual HtmlContainerControl MailingListErrorMessage => this.Container.GetControl<HtmlContainerControl>("mailingListErrorMessage", true);

    /// <summary>Gets the mailing list selector.</summary>
    protected virtual MailingListSelector MailListSelector => this.Container.GetControl<MailingListSelector>("mailingListSelector", true);

    /// <summary>Gets the save button.</summary>
    protected virtual HtmlAnchor SaveButton => this.Container.GetControl<HtmlAnchor>("saveButton", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string str = SystemManager.CurrentHttpContext.Request.Params["providerName"];
      if (str == null)
        return;
      this.ProviderName = str;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("backLink", this.BackLink.ClientID);
      controlDescriptor.AddElementProperty("issuePropertiesWrapper", this.IssuePropertiesWrapper.ClientID);
      controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
      controlDescriptor.AddElementProperty("dialogTitle", this.DialogTitle.ClientID);
      controlDescriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      controlDescriptor.AddElementProperty("buttonsPanel", this.ButtonsPanel.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("createAndCloseLabel", this.CreateAndCloseLabel.ClientID);
      controlDescriptor.AddElementProperty("createAndCloseButton", this.CreateAndCloseButton.ClientID);
      controlDescriptor.AddElementProperty("goToContentButton", this.GoToContentButton.ClientID);
      controlDescriptor.AddComponentProperty("nameTextField", this.NameTextField.ClientID);
      controlDescriptor.AddComponentProperty("nameTextFieldRead", this.NameTextFieldRead.ClientID);
      controlDescriptor.AddComponentProperty("messageSubjectTextField", this.MessageSubjectTextField.ClientID);
      controlDescriptor.AddComponentProperty("fromNameTextField", this.FromNameTextField.ClientID);
      controlDescriptor.AddComponentProperty("replyToEmailTextField", this.ReplyToEmailTextField.ClientID);
      controlDescriptor.AddElementProperty("selectedLists", this.SelectedListsElement.ClientID);
      controlDescriptor.AddElementProperty("selectListsButton", this.SelectListButton.ClientID);
      controlDescriptor.AddElementProperty("mailingListErrorMessage", this.MailingListErrorMessage.ClientID);
      controlDescriptor.AddComponentProperty("mailingListSelector", this.MailListSelector.ClientID);
      controlDescriptor.AddElementProperty("saveButton", this.SaveButton.ClientID);
      controlDescriptor.AddProperty("_rootUrl", (object) this.Page.ResolveUrl("~/"));
      controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = typeof (AbTestIssuePropertiesView).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.NewslettersClientManager.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.AbTestIssuePropertiesView.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
