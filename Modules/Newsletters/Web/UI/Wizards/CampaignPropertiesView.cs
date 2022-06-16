// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignPropertiesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards
{
  public class CampaignPropertiesView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.CampaignPropertiesView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.CampaignPropertiesView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CampaignPropertiesView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets a reference to the hosting control.</summary>
    public CampaignDetailView Host { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a reference to the wrapper that wraps the whole view.
    /// </summary>
    protected virtual HtmlContainerControl Wrapper => this.Container.GetControl<HtmlContainerControl>("campaignPropertiesView", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets a reference to the "Create and go to add the first issue" button.
    /// </summary>
    protected virtual HtmlAnchor CreateAndGoToAddFirstIssue => this.Container.GetControl<HtmlAnchor>("btnCreateAndGoToAddFirstIssue", true);

    /// <summary>
    /// Gets a reference to the "create and go to email campaigns" button.
    /// </summary>
    protected virtual HtmlAnchor SaveChanges => this.Container.GetControl<HtmlAnchor>("btnSaveChanges", true);

    /// <summary>Gets a reference to the campaign name textfield.</summary>
    protected virtual TextField CampaignName => this.Container.GetControl<TextField>("Name", true);

    /// <summary>Gets a reference to the from name textfield.</summary>
    protected virtual TextField FromName => this.Container.GetControl<TextField>(nameof (FromName), true);

    /// <summary>Gets a reference to the "Reply to Email" textfield.</summary>
    protected virtual TextField ReplyToEmail => this.Container.GetControl<TextField>(nameof (ReplyToEmail), true);

    /// <summary>Gets a reference to the "from scratch" container.</summary>
    protected virtual HtmlContainerControl FromScratchContainer => this.Container.GetControl<HtmlContainerControl>("fromScratchContainer", true);

    /// <summary>Gets a reference to the "use template" radio button.</summary>
    protected virtual RadioButton UseTemplateRadio => this.Container.GetControl<RadioButton>("createFromTemplateRadio", true);

    /// <summary>
    /// Gets a reference to the "start from scratch" radio button.
    /// </summary>
    protected virtual RadioButton StartFromScratchRadio => this.Container.GetControl<RadioButton>("createFromScratchRadio", true);

    /// <summary>Gets a reference to the templates choice field.</summary>
    protected virtual ChoiceField TemplatesChoiceField => this.Container.GetControl<ChoiceField>("templatesChoiceField", true);

    /// <summary>Gets a reference to the select list button.</summary>
    protected virtual LinkButton SelectListButton => this.Container.GetControl<LinkButton>("selectListsButton", true);

    /// <summary>Gets a reference to the selected lists element.</summary>
    protected virtual HtmlContainerControl SelectedListsElement => this.Container.GetControl<HtmlContainerControl>("selectedLists", true);

    /// <summary>Gets a reference to the google tracking checkbox.</summary>
    protected virtual CheckBox UseGoogleTrackingField => this.Container.GetControl<CheckBox>("useGoogleTrackingField", true);

    /// <summary>Gets a reference to the HTML campaign radio.</summary>
    protected virtual RadioButton HtmlCampaignRadio => this.Container.GetControl<RadioButton>("htmlCampaignRadio", true);

    /// <summary>Gets a reference to the plain text campaign radio.</summary>
    protected virtual RadioButton PlainTextCampaignRadio => this.Container.GetControl<RadioButton>("plainTextCampaignRadio", true);

    /// <summary>Gets a reference to the standard campaign radio.</summary>
    protected virtual RadioButton StandardCampaignRadio => this.Container.GetControl<RadioButton>("standardCampaignRadio", true);

    /// <summary>Gets a reference to the properties cancel link.</summary>
    protected virtual HtmlAnchor PropertiesCancelLink => this.Container.GetControl<HtmlAnchor>("propertiesCancelLink", true);

    /// <summary>Gets a reference to the campaign type container.</summary>
    protected virtual HtmlContainerControl CampaignTypeContainer => this.Container.GetControl<HtmlContainerControl>("campaignTypeContainer", true);

    /// <summary>
    /// Gets the reference to the buttons from the "Campaign Properties View".
    /// </summary>
    protected virtual HtmlGenericControl PropertiesViewButtons => this.Container.GetControl<HtmlGenericControl>("propertiesViewButtons", true);

    /// <summary>Gets the reference to the send button.</summary>
    protected virtual HtmlGenericControl MailingListErrorMessage => this.Container.GetControl<HtmlGenericControl>("mailingListErrorMessage", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.BindMessageTemplates();

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
      controlDescriptor.AddComponentProperty("campaignName", this.CampaignName.ClientID);
      controlDescriptor.AddComponentProperty("fromName", this.FromName.ClientID);
      controlDescriptor.AddComponentProperty("replyToEmail", this.ReplyToEmail.ClientID);
      controlDescriptor.AddElementProperty("fromScratchContainer", this.FromScratchContainer.ClientID);
      controlDescriptor.AddElementProperty("createFromTemplateRadio", this.UseTemplateRadio.ClientID);
      controlDescriptor.AddElementProperty("createFromScratchRadio", this.StartFromScratchRadio.ClientID);
      controlDescriptor.AddComponentProperty("templatesChoiceField", this.TemplatesChoiceField.ClientID);
      controlDescriptor.AddElementProperty("selectListsButton", this.SelectListButton.ClientID);
      controlDescriptor.AddElementProperty("selectedLists", this.SelectedListsElement.ClientID);
      controlDescriptor.AddElementProperty("googleTrackingCheckbox", this.UseGoogleTrackingField.ClientID);
      controlDescriptor.AddElementProperty("htmlCampaignRadio", this.HtmlCampaignRadio.ClientID);
      controlDescriptor.AddElementProperty("plainTextCampaignRadio", this.PlainTextCampaignRadio.ClientID);
      controlDescriptor.AddElementProperty("standardCampaignRadio", this.StandardCampaignRadio.ClientID);
      controlDescriptor.AddElementProperty("campaignTypeContainer", this.CampaignTypeContainer.ClientID);
      controlDescriptor.AddElementProperty("mailingListErrorMessage", this.MailingListErrorMessage.ClientID);
      controlDescriptor.AddElementProperty("propertiesViewButtons", this.PropertiesViewButtons.ClientID);
      controlDescriptor.AddElementProperty("propertiesCancelLink", this.PropertiesCancelLink.ClientID);
      controlDescriptor.AddElementProperty("btnCreateAndGoToAddFirstIssue", this.CreateAndGoToAddFirstIssue.ClientID);
      controlDescriptor.AddElementProperty("btnSaveChanges", this.SaveChanges.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.CampaignPropertiesView.js", typeof (CampaignPropertiesView).Assembly.FullName)
    };

    private void BindMessageTemplates()
    {
      this.TemplatesChoiceField.Choices.Clear();
      IOrderedQueryable<MessageBody> orderedQueryable = NewslettersManager.GetManager(this.Host.ProviderName).GetMessageBodies().Where<MessageBody>((Expression<Func<MessageBody, bool>>) (mb => mb.IsTemplate == true)).OrderBy<MessageBody, string>((Expression<Func<MessageBody, string>>) (mb => mb.Name));
      this.TemplatesChoiceField.Choices.Add(new ChoiceItem()
      {
        Text = string.Format("-{0}-", (object) Res.Get<Labels>().SelectATemplate),
        Value = Guid.Empty.ToString()
      });
      foreach (MessageBody messageBody in (IEnumerable<MessageBody>) orderedQueryable)
        this.TemplatesChoiceField.Choices.Add(new ChoiceItem()
        {
          Text = messageBody.Name,
          Value = messageBody.Id.ToString()
        });
    }
  }
}
