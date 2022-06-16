// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssuePropertiesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards
{
  public class IssuePropertiesView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.IssuePropertiesView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.IssuePropertiesView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? IssuePropertiesView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a reference to the wrapper that wraps the whole view.
    /// </summary>
    protected virtual HtmlContainerControl Wrapper => this.Container.GetControl<HtmlContainerControl>("issuePropertiesView", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets a reference to the cancel link.</summary>
    protected virtual HtmlAnchor CancelLink => this.Container.GetControl<HtmlAnchor>("cancelLink", true);

    /// <summary>Gets a reference to the selected lists element.</summary>
    protected virtual HtmlContainerControl SelectedListsElement => this.Container.GetControl<HtmlContainerControl>("selectedLists", true);

    /// <summary>Gets a reference to the select list button.</summary>
    protected virtual LinkButton SelectListButton => this.Container.GetControl<LinkButton>("selectListsButton", true);

    /// <summary>Gets a reference to the message subject textfield.</summary>
    protected virtual TextField MessageSubject => this.Container.GetControl<TextField>(nameof (MessageSubject), true);

    /// <summary>Gets a reference to the from name text field.</summary>
    protected virtual TextField FromName => this.Container.GetControl<TextField>(nameof (FromName), true);

    /// <summary>Gets a reference to the "Reply to Email" text field.</summary>
    protected virtual TextField ReplyToEmail => this.Container.GetControl<TextField>(nameof (ReplyToEmail), true);

    /// <summary>Gets a reference to the "Issue name" text field.</summary>
    protected virtual TextField IssueName => this.Container.GetControl<TextField>("Name", true);

    /// <summary>
    /// Gets a reference to the containers that contains the buttons of the view.
    /// </summary>
    protected virtual HtmlContainerControl IssuePropertiesViewButtons => this.Container.GetControl<HtmlContainerControl>("issuePropertiesViewButtons", true);

    /// <summary>
    /// Gets a reference to the "Create and go to add content" button.
    /// </summary>
    protected virtual HtmlAnchor CreateAndGoToAddContent => this.Container.GetControl<HtmlAnchor>("btnCreateAndGoToAddContent", true);

    /// <summary>
    /// Gets a reference to the "Create and go to email campaigns" button.
    /// </summary>
    protected virtual HtmlAnchor CreateAndGoToEmailCampaigns => this.Container.GetControl<HtmlAnchor>("btnCreateAndGoToEmailCampaigns", true);

    /// <summary>
    /// Gets a reference to the mailing list error message container.
    /// </summary>
    protected virtual HtmlContainerControl MailingListErrorMessage => this.Container.GetControl<HtmlContainerControl>("mailingListErrorMessage", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.MessageSubject.ValidatorDefinition.MaxLength = Config.Get<NewslettersConfig>().CampaignSubjectLength;

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
      controlDescriptor.AddElementProperty("wrapper", this.Wrapper.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      controlDescriptor.AddElementProperty("selectListsButton", this.SelectListButton.ClientID);
      controlDescriptor.AddElementProperty("selectedLists", this.SelectedListsElement.ClientID);
      controlDescriptor.AddComponentProperty("messageSubject", this.MessageSubject.ClientID);
      controlDescriptor.AddComponentProperty("fromName", this.FromName.ClientID);
      controlDescriptor.AddComponentProperty("replyToEmail", this.ReplyToEmail.ClientID);
      controlDescriptor.AddComponentProperty("issueName", this.IssueName.ClientID);
      controlDescriptor.AddElementProperty("mailingListErrorMessage", this.MailingListErrorMessage.ClientID);
      controlDescriptor.AddElementProperty("issuePropertiesViewButtons", this.IssuePropertiesViewButtons.ClientID);
      controlDescriptor.AddElementProperty("btnCreateAndGoToAddContent", this.CreateAndGoToAddContent.ClientID);
      controlDescriptor.AddElementProperty("btnCreateAndGoToEmailCampaigns", this.CreateAndGoToEmailCampaigns.ClientID);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.IssuePropertiesView.js", typeof (IssuePropertiesView).Assembly.FullName)
    };
  }
}
