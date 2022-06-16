// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestSendView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards
{
  /// <summary>The view for sending an AB test.</summary>
  public class AbTestSendView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.AbTestSendView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.AbTestSendView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestSendView" /> class.
    /// </summary>
    public AbTestSendView() => this.LayoutTemplatePath = AbTestSendView.layoutTemplatePath;

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

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <summary>Gets the reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the reference to the send view wrapper.</summary>
    protected virtual HtmlContainerControl SendViewWrapper => this.Container.GetControl<HtmlContainerControl>("sendViewWrapper", true);

    /// <summary>Gets the reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the reference to the preview window.</summary>
    protected virtual CampaignPreviewWindow PreviewWindow => this.Container.GetControl<CampaignPreviewWindow>("previewWindow", true);

    /// <summary>Gets the reference to the issue A title.</summary>
    protected virtual Label IssueATitle => this.Container.GetControl<Label>("issueATitle", true);

    /// <summary>Gets the reference to the view link for issue A.</summary>
    protected virtual HtmlAnchor ViewIssueALink => this.Container.GetControl<HtmlAnchor>("viewIssueALink", true);

    /// <summary>Gets the reference to the issue A message subject.</summary>
    protected virtual Label IssueAMessageSubject => this.Container.GetControl<Label>("issueAMessageSubject", true);

    /// <summary>Gets the reference to the issue A from name.</summary>
    protected virtual Label IssueAFromName => this.Container.GetControl<Label>("issueAFromName", true);

    /// <summary>Gets the reference to the issue A reply email.</summary>
    protected virtual Label IssueAReplyEmail => this.Container.GetControl<Label>("issueAReplyEmail", true);

    /// <summary>Gets the reference to the issue A mailing list.</summary>
    protected virtual Label IssueAMailingList => this.Container.GetControl<Label>("issueAMailingList", true);

    /// <summary>Gets the reference to the button for editing issue A.</summary>
    protected virtual HtmlAnchor EditIssueAButton => this.Container.GetControl<HtmlAnchor>("editIssueAButton", true);

    /// <summary>Gets the reference to the issue B title.</summary>
    protected virtual Label IssueBTitle => this.Container.GetControl<Label>("issueBTitle", true);

    /// <summary>Gets the reference to the view link for issue B.</summary>
    protected virtual HtmlAnchor ViewIssueBLink => this.Container.GetControl<HtmlAnchor>("viewIssueBLink", true);

    /// <summary>Gets the reference to the issue B message subject.</summary>
    protected virtual Label IssueBMessageSubject => this.Container.GetControl<Label>("issueBMessageSubject", true);

    /// <summary>Gets the reference to the issue B from name.</summary>
    protected virtual Label IssueBFromName => this.Container.GetControl<Label>("issueBFromName", true);

    /// <summary>Gets the reference to the issue B reply email.</summary>
    protected virtual Label IssueBReplyEmail => this.Container.GetControl<Label>("issueBReplyEmail", true);

    /// <summary>Gets the reference to the issue B mailing list.</summary>
    protected virtual Label IssueBMailingList => this.Container.GetControl<Label>("issueBMailingList", true);

    /// <summary>Gets the reference to the button for editing issue B.</summary>
    protected virtual HtmlAnchor EditIssueBButton => this.Container.GetControl<HtmlAnchor>("editIssueBButton", true);

    /// <summary>Gets the name text field.</summary>
    protected virtual TextField NameTextField => this.Container.GetControl<TextField>("nameTextField", true);

    /// <summary>Gets the testing note text field.</summary>
    protected virtual TextField TestingNoteTextField => this.Container.GetControl<TextField>("testingNoteTextField", true);

    /// <summary>
    /// Gets the reference to the testing sample description label.
    /// </summary>
    protected virtual Label TestingSampleDescriptionLabel => this.Container.GetControl<Label>("testingSampleDescriptionLabel", true);

    /// <summary>Gets the reference to the testing sample slider.</summary>
    protected virtual RadSlider TestingSampleSlider => this.Container.GetControl<RadSlider>("testingSampleSlider", true);

    /// <summary>
    /// Gets the reference to the label showing the testing sample percentage.
    /// </summary>
    protected virtual Label TestingSamplePercentageLabel => this.Container.GetControl<Label>("testingSamplePercentageLabel", true);

    /// <summary>
    /// Gets the reference to the winning factor choice field.
    /// </summary>
    protected virtual ChoiceField WinningFactorChoiceField => this.Container.GetControl<ChoiceField>("winningFactorChoiceField", true);

    /// <summary>
    /// Gets the reference to the sending decision choice field.
    /// </summary>
    protected virtual ChoiceField SendingDecisionChoiceField => this.Container.GetControl<ChoiceField>("sendingDecisionChoiceField", true);

    /// <summary>Gets the reference to the scheduler wrapper.</summary>
    protected virtual HtmlContainerControl SchedulerWrapper => this.Container.GetControl<HtmlContainerControl>("schedulerWrapper", true);

    /// <summary>Gets the reference to the schedule ab test picker.</summary>
    protected virtual RadDateTimePicker ScheduleABTestPicker => this.Container.GetControl<RadDateTimePicker>("scheduleABTestPicker", true);

    /// <summary>Gets the reference to the testing period end picker.</summary>
    protected virtual RadDateTimePicker TestingPeriodEndPicker => this.Container.GetControl<RadDateTimePicker>("testingPeriodEndPicker", true);

    /// <summary>Gets the buttons panel.</summary>
    protected virtual HtmlContainerControl ButtonsPanel => this.Container.GetControl<HtmlContainerControl>("buttonsPanel", true);

    /// <summary>Gets the reference to the send ab test button.</summary>
    protected virtual HtmlAnchor SendABTestButton => this.Container.GetControl<HtmlAnchor>("sendABTestButton", true);

    /// <summary>Gets the reference to the schedule ab test button.</summary>
    protected virtual HtmlAnchor ScheduleABTestButton => this.Container.GetControl<HtmlAnchor>("scheduleABTestButton", true);

    /// <summary>Gets the reference to the save as draft button.</summary>
    protected virtual HtmlAnchor SaveDraftButton => this.Container.GetControl<HtmlAnchor>("saveDraftButton", true);

    /// <summary>Gets the reference to the cancel link.</summary>
    protected virtual HtmlAnchor CancelLink => this.Container.GetControl<HtmlAnchor>("cancelLink", true);

    /// <summary>Gets a reference to the loading view.</summary>
    protected virtual HtmlContainerControl LoadingView => this.Container.GetControl<HtmlContainerControl>("loadingView", true);

    /// <summary>Gets the send prompt dialog.</summary>
    protected virtual PromptDialog SendPrompt => this.Container.GetControl<PromptDialog>("sendPrompt", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string str = SystemManager.CurrentHttpContext.Request.Params["providerName"];
      if (str != null)
        this.ProviderName = str;
      this.ScheduleABTestPicker.SelectedDate = new DateTime?(DateTime.UtcNow.AddDays(7.0));
      this.TestingPeriodEndPicker.SelectedDate = new DateTime?(DateTime.UtcNow.AddDays(7.0));
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
      controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddComponentProperty("previewWindow", this.PreviewWindow.ClientID);
      controlDescriptor.AddElementProperty("sendViewWrapper", this.SendViewWrapper.ClientID);
      controlDescriptor.AddComponentProperty("sendPrompt", this.SendPrompt.ClientID);
      controlDescriptor.AddElementProperty("issueATitle", this.IssueATitle.ClientID);
      controlDescriptor.AddElementProperty("viewIssueALink", this.ViewIssueALink.ClientID);
      controlDescriptor.AddElementProperty("issueAMessageSubject", this.IssueAMessageSubject.ClientID);
      controlDescriptor.AddElementProperty("issueAFromName", this.IssueAFromName.ClientID);
      controlDescriptor.AddElementProperty("issueAReplyEmail", this.IssueAReplyEmail.ClientID);
      controlDescriptor.AddElementProperty("issueAMailingList", this.IssueAMailingList.ClientID);
      controlDescriptor.AddElementProperty("editIssueAButton", this.EditIssueAButton.ClientID);
      controlDescriptor.AddElementProperty("issueBTitle", this.IssueBTitle.ClientID);
      controlDescriptor.AddElementProperty("viewIssueBLink", this.ViewIssueBLink.ClientID);
      controlDescriptor.AddElementProperty("issueBMessageSubject", this.IssueBMessageSubject.ClientID);
      controlDescriptor.AddElementProperty("issueBFromName", this.IssueBFromName.ClientID);
      controlDescriptor.AddElementProperty("issueBReplyEmail", this.IssueBReplyEmail.ClientID);
      controlDescriptor.AddElementProperty("issueBMailingList", this.IssueBMailingList.ClientID);
      controlDescriptor.AddElementProperty("editIssueBButton", this.EditIssueBButton.ClientID);
      controlDescriptor.AddComponentProperty("nameTextField", this.NameTextField.ClientID);
      controlDescriptor.AddComponentProperty("testingNoteTextField", this.TestingNoteTextField.ClientID);
      controlDescriptor.AddElementProperty("testingSampleDescriptionLabel", this.TestingSampleDescriptionLabel.ClientID);
      controlDescriptor.AddComponentProperty("testingSampleSlider", this.TestingSampleSlider.ClientID);
      controlDescriptor.AddElementProperty("testingSamplePercentageLabel", this.TestingSamplePercentageLabel.ClientID);
      controlDescriptor.AddComponentProperty("winningFactorChoiceField", this.WinningFactorChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("sendingDecisionChoiceField", this.SendingDecisionChoiceField.ClientID);
      controlDescriptor.AddElementProperty("schedulerWrapper", this.SchedulerWrapper.ClientID);
      controlDescriptor.AddComponentProperty("scheduleABTestPicker", this.ScheduleABTestPicker.ClientID);
      controlDescriptor.AddComponentProperty("testingPeriodEndPicker", this.TestingPeriodEndPicker.ClientID);
      controlDescriptor.AddElementProperty("buttonsPanel", this.ButtonsPanel.ClientID);
      controlDescriptor.AddElementProperty("sendABTestButton", this.SendABTestButton.ClientID);
      controlDescriptor.AddElementProperty("scheduleABTestButton", this.ScheduleABTestButton.ClientID);
      controlDescriptor.AddElementProperty("saveDraftButton", this.SaveDraftButton.ClientID);
      controlDescriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
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
      string fullName = typeof (AbTestSendView).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.NewslettersClientManager.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.AbTestSendView.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
