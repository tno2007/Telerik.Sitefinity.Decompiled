// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMessageView
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
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards
{
  /// <summary>A view that presents the content of a A/B issue.</summary>
  public class AbTestMessageView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.AbTestMessageView.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.AbTestMessageView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? AbTestMessageView.layoutTemplatePath : base.LayoutTemplatePath;
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

    /// <summary>Gets a reference to the message view wrapper.</summary>
    protected virtual HtmlContainerControl MessageWrapper => this.Container.GetControl<HtmlContainerControl>("messageWrapper", true);

    /// <summary>Gets a reference to the loading view.</summary>
    protected virtual HtmlContainerControl LoadingView => this.Container.GetControl<HtmlContainerControl>("loadingView", true);

    /// <summary>Gets the dialog title.</summary>
    protected virtual SitefinityLabel DialogTitle => this.Container.GetControl<SitefinityLabel>("dialogTitle", true);

    /// <summary>Gets the buttons panel.</summary>
    protected virtual HtmlContainerControl ButtonsPanel => this.Container.GetControl<HtmlContainerControl>("buttonsPanel", true);

    /// <summary>Gets the cancel link.</summary>
    protected virtual LinkButton CancelLink => this.Container.GetControl<LinkButton>("cancelLink", true);

    /// <summary>Gets the HTML text panel.</summary>
    protected virtual HtmlContainerControl HtmlTextPanel => this.Container.GetControl<HtmlContainerControl>("htmlTextPanel", true);

    /// <summary>Gets the plain text panel.</summary>
    protected virtual HtmlContainerControl PlainTextPanel => this.Container.GetControl<HtmlContainerControl>("plainTextPanel", true);

    /// <summary>Gets the HTML text control.</summary>
    protected virtual HtmlField HtmlTextControl => this.Container.GetControl<HtmlField>("htmlTextControl", true);

    /// <summary>Gets the merge tag selector for the HTML content.</summary>
    protected virtual MergeTagSelector HtmlMergeTagSelector => this.Container.GetControl<MergeTagSelector>("htmlMergeTagSelector", true);

    /// <summary>Gets the automatic plain text radio.</summary>
    protected virtual RadioButton AutomaticPlainTextRadio => this.Container.GetControl<RadioButton>("automaticPlainTextRadio", true);

    /// <summary>Gets the manual plain text radio.</summary>
    protected virtual RadioButton ManualPlainTextRadio => this.Container.GetControl<RadioButton>("manualPlainTextRadio", true);

    /// <summary>
    /// Gets the plain text version panel of the HTML content.
    /// </summary>
    protected virtual HtmlContainerControl PlainTextVersionHtmlPanel => this.Container.GetControl<HtmlContainerControl>("plainTextVersionHtmlPanel", true);

    /// <summary>Gets the plain text control.</summary>
    protected virtual TextBox PlainTextControl => this.Container.GetControl<TextBox>("plainTextControl", true);

    /// <summary>
    /// Gets the merge tag selector for the plain text content.
    /// </summary>
    protected virtual MergeTagSelector MergeTagSelector => this.Container.GetControl<MergeTagSelector>("mergeTagSelector", true);

    /// <summary>
    /// Gets the plain text version textbox for the HTML content.
    /// </summary>
    protected virtual TextBox PlainTextVersionHtml => this.Container.GetControl<TextBox>("plainTextVersionHtml", true);

    /// <summary>Gets the preview window.</summary>
    protected virtual CampaignPreviewWindow PreviewWindow => this.Container.GetControl<CampaignPreviewWindow>("previewWindow", true);

    /// <summary>Gets the send test prompt.</summary>
    protected virtual PromptDialog SendTestPrompt => this.Container.GetControl<PromptDialog>("sendTestPrompt", true);

    /// <summary>Gets the send button.</summary>
    protected virtual HtmlAnchor SendButton => this.Container.GetControl<HtmlAnchor>("sendButton", true);

    /// <summary>Gets the save draft button.</summary>
    protected virtual HtmlAnchor SaveDraftButton => this.Container.GetControl<HtmlAnchor>("saveDraftButton", true);

    /// <summary>Gets the action menu.</summary>
    protected virtual RadMenu ActionsMenu => this.Container.GetControl<RadMenu>("actionsMenu", true);

    /// <summary>Gets the preview button.</summary>
    protected virtual HtmlAnchor PreviewButton => this.Container.GetControl<HtmlAnchor>("previewButton", true);

    /// <summary>Gets the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

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
      controlDescriptor.AddElementProperty("messageWrapper", this.MessageWrapper.ClientID);
      controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
      controlDescriptor.AddElementProperty("dialogTitle", this.DialogTitle.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("buttonsPanel", this.ButtonsPanel.ClientID);
      controlDescriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      controlDescriptor.AddComponentProperty("actionsMenu", this.ActionsMenu.ClientID);
      controlDescriptor.AddElementProperty("sendButton", this.SendButton.ClientID);
      controlDescriptor.AddElementProperty("saveDraftButton", this.SaveDraftButton.ClientID);
      controlDescriptor.AddElementProperty("previewButton", this.PreviewButton.ClientID);
      controlDescriptor.AddComponentProperty("previewWindow", this.PreviewWindow.ClientID);
      controlDescriptor.AddComponentProperty("sendTestPrompt", this.SendTestPrompt.ClientID);
      controlDescriptor.AddElementProperty("htmlTextPanel", this.HtmlTextPanel.ClientID);
      controlDescriptor.AddComponentProperty("htmlTextControl", this.HtmlTextControl.ClientID);
      controlDescriptor.AddComponentProperty("htmlMergeTagSelector", this.HtmlMergeTagSelector.ClientID);
      controlDescriptor.AddElementProperty("automaticPlainTextRadio", this.AutomaticPlainTextRadio.ClientID);
      controlDescriptor.AddElementProperty("manualPlainTextRadio", this.ManualPlainTextRadio.ClientID);
      controlDescriptor.AddElementProperty("plainTextVersionHtmlPanel", this.PlainTextVersionHtmlPanel.ClientID);
      controlDescriptor.AddElementProperty("plainTextVersionHtml", this.PlainTextVersionHtml.ClientID);
      controlDescriptor.AddElementProperty("plainTextPanel", this.PlainTextPanel.ClientID);
      controlDescriptor.AddElementProperty("plainTextControl", this.PlainTextControl.ClientID);
      controlDescriptor.AddComponentProperty("mergeTagSelector", this.MergeTagSelector.ClientID);
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
      string fullName = typeof (AbTestMessageView).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.NewslettersClientManager.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.AbTestMessageView.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
