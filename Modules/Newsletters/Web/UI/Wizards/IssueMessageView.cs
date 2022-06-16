// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssueMessageView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards
{
  public class IssueMessageView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.IssueMessageView.js";
    private const string newslettersHandlerUrl = "~/Sitefinity/SFNwslttrs/";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.IssueMessageView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? IssueMessageView.layoutTemplatePath : base.LayoutTemplatePath;
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
    protected virtual HtmlContainerControl Wrapper => this.Container.GetControl<HtmlContainerControl>(nameof (IssueMessageView), true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets a reference to the message view buttons div.</summary>
    protected virtual HtmlContainerControl MessageViewButtons => this.Container.GetControl<HtmlContainerControl>("messageViewButtons", true);

    /// <summary>Gets a reference the schedule delivery window.</summary>
    protected virtual ScheduleDeliveryWindow ScheduleDeliveryWindow => this.Container.GetControl<ScheduleDeliveryWindow>("scheduleDeliveryWindow", true);

    /// <summary>Gets a reference to the campaign preview window.</summary>
    protected virtual CampaignPreviewWindow CampaignPreviewWindow => this.Container.GetControl<CampaignPreviewWindow>("campaignPreviewWindow", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.PromptDialog" /> control for sending the test emails.
    /// </summary>
    protected virtual PromptDialog SendTestPrompt => this.Container.GetControl<PromptDialog>("sendTestPrompt", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.PromptDialog" /> control for sending issues.
    /// </summary>
    protected virtual PromptDialog SendIssuePrompt => this.Container.GetControl<PromptDialog>("sendIssuePrompt", true);

    /// <summary>Gets a reference to the HTML text panel.</summary>
    protected virtual HtmlContainerControl HtmlTextPanel => this.Container.GetControl<HtmlContainerControl>("htmlTextPanel", true);

    /// <summary>Gets a reference to the plain text panel.</summary>
    protected virtual HtmlContainerControl PlainTextPanel => this.Container.GetControl<HtmlContainerControl>("plainTextPanel", true);

    /// <summary>Gets a reference to the HTML text control.</summary>
    protected virtual HtmlField HtmlTextControl => this.Container.GetControl<HtmlField>("htmlTextControl", true);

    /// <summary>
    /// Gets the reference to the merge tag selector control for html messages.
    /// </summary>
    protected virtual MergeTagSelector HtmlMergeTagSelector => this.Container.GetControl<MergeTagSelector>("htmlMergeTagSelector", true);

    /// <summary>Gets a reference to the plain text control.</summary>
    protected virtual TextBox PlainTextControl => this.Container.GetControl<TextBox>("plainTextControl", true);

    /// <summary>Gets the reference to the merge tag selector control.</summary>
    protected virtual MergeTagSelector MergeTagSelector => this.Container.GetControl<MergeTagSelector>("mergeTagSelector", true);

    /// <summary>
    /// Gets a reference to the automatic plain text generation choice.
    /// </summary>
    protected virtual RadioButton AutomaticPlainTextRadio => this.Container.GetControl<RadioButton>("automaticPlainTextRadio", true);

    /// <summary>
    /// Gets a reference to the manual plain text generation choice.
    /// </summary>
    protected virtual RadioButton ManualPlainTextRadio => this.Container.GetControl<RadioButton>("manualPlainTextRadio", true);

    /// <summary>
    /// Gets a reference to the plain text version HTML panel.
    /// </summary>
    protected virtual HtmlContainerControl PlainTextVersionHtmlPanel => this.Container.GetControl<HtmlContainerControl>("plainTextVersionHtmlPanel", true);

    /// <summary>Gets a reference to the plain text version HTML.</summary>
    protected virtual TextBox PlainTextVersionHtml => this.Container.GetControl<TextBox>("plainTextVersionHtml", true);

    /// <summary>
    /// Gets the reference to the save as draft campaign button.
    /// </summary>
    protected virtual HtmlAnchor SaveDraftButton => this.Container.GetControl<HtmlAnchor>("saveDraftButton", true);

    /// <summary>Gets the reference to the send button.</summary>
    protected virtual HtmlAnchor SendButton => this.Container.GetControl<HtmlAnchor>("sendButton", true);

    /// <summary>Gets the reference to the actions menu.</summary>
    protected virtual RadMenu ActionsMenu => this.Container.GetControl<RadMenu>("actionsMenu", true);

    /// <summary>Gets the reference to the preview button.</summary>
    protected virtual HtmlAnchor PreviewButton => this.Container.GetControl<HtmlAnchor>("previewButton", true);

    /// <summary>Gets a reference to the message cancel link.</summary>
    protected virtual HtmlAnchor MessageCancelLink => this.Container.GetControl<HtmlAnchor>("messageCancelLink", true);

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
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("wrapper", this.Wrapper.ClientID);
      controlDescriptor.AddProperty("newslettersHandlerUrl", (object) this.Page.ResolveUrl("~/Sitefinity/SFNwslttrs/"));
      controlDescriptor.AddComponentProperty("scheduleDeliveryWindow", this.ScheduleDeliveryWindow.ClientID);
      controlDescriptor.AddComponentProperty("campaignPreviewWindow", this.CampaignPreviewWindow.ClientID);
      controlDescriptor.AddComponentProperty("sendTestPrompt", this.SendTestPrompt.ClientID);
      controlDescriptor.AddComponentProperty("sendIssuePrompt", this.SendIssuePrompt.ClientID);
      controlDescriptor.AddElementProperty("htmlTextPanel", this.HtmlTextPanel.ClientID);
      controlDescriptor.AddElementProperty("plainTextPanel", this.PlainTextPanel.ClientID);
      controlDescriptor.AddComponentProperty("htmlTextControl", this.HtmlTextControl.ClientID);
      controlDescriptor.AddElementProperty("plainTextControl", this.PlainTextControl.ClientID);
      controlDescriptor.AddComponentProperty("mergeTagSelector", this.MergeTagSelector.ClientID);
      controlDescriptor.AddComponentProperty("htmlMergeTagSelector", this.HtmlMergeTagSelector.ClientID);
      controlDescriptor.AddElementProperty("plainTextVersionHtmlPanel", this.PlainTextVersionHtmlPanel.ClientID);
      controlDescriptor.AddElementProperty("plainTextVersionHtml", this.PlainTextVersionHtml.ClientID);
      controlDescriptor.AddElementProperty("automaticPlainTextRadio", this.AutomaticPlainTextRadio.ClientID);
      controlDescriptor.AddElementProperty("manualPlainTextRadio", this.ManualPlainTextRadio.ClientID);
      controlDescriptor.AddElementProperty("messageViewButtons", this.MessageViewButtons.ClientID);
      controlDescriptor.AddElementProperty("sendButton", this.SendButton.ClientID);
      controlDescriptor.AddElementProperty("saveDraftButton", this.SaveDraftButton.ClientID);
      controlDescriptor.AddComponentProperty("actionsMenu", this.ActionsMenu.ClientID);
      controlDescriptor.AddElementProperty("previewButton", this.PreviewButton.ClientID);
      controlDescriptor.AddElementProperty("messageCancelLink", this.MessageCancelLink.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.IssueMessageView.js", typeof (IssueMessageView).Assembly.FullName)
    };
  }
}
