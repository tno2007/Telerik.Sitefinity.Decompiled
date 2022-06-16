// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignTypeStep
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
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps
{
  /// <summary>
  /// Step of the campaign wizard that lets user decide which type of campaign he/she want's to create.
  /// </summary>
  public class CampaignTypeStep : SitefinityWizardStepControl
  {
    private NewslettersManager manager;
    internal new const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.Scripts.CampaignTypeStep.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.Steps.CampaignTypeStep.ascx");

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CampaignTypeStep.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> instance.
    /// </summary>
    protected virtual NewslettersManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = NewslettersManager.GetManager();
        return this.manager;
      }
    }

    /// <summary>
    /// Gets the reference to the radio button for the html campaign.
    /// </summary>
    protected virtual RadioButton HtmlCampaignRadio => this.Container.GetControl<RadioButton>("htmlCampaignRadio", true);

    /// <summary>
    /// Gets the reference to the radio button for the plain text campaign.
    /// </summary>
    protected virtual RadioButton PlainTextCampaignRadio => this.Container.GetControl<RadioButton>("plainTextCampaignRadio", true);

    /// <summary>
    /// Gets the reference to the radio button for the standard campaign.
    /// </summary>
    protected virtual RadioButton StandardCampaignRadio => this.Container.GetControl<RadioButton>("standardCampaignRadio", true);

    /// <summary>
    /// Gets the reference to the radio button for the standard campaign based on the internal page.
    /// </summary>
    protected virtual RadioButton StandardCampaignInternalPageRadio => this.Container.GetControl<RadioButton>("standardCampaignInternalPageRadio", true);

    /// <summary>
    /// Gets the reference to the radio button for the standard campaign based on the external page.
    /// </summary>
    protected virtual RadioButton StandardCampaignExternalPageRadio => this.Container.GetControl<RadioButton>("standardCampaignExternalPageRadio", true);

    /// <summary>
    /// Gets the reference to the control that holds the additional options for the standard
    /// campaign type.
    /// </summary>
    protected virtual HtmlGenericControl StandardCampaignOptions => this.Container.GetControl<HtmlGenericControl>("standardCampaignOptions", true);

    /// <summary>
    /// Gets the reference to the create from template radio button
    /// </summary>
    protected virtual RadioButton CreateFromTemplateRadio => this.Container.GetControl<RadioButton>("createFromTemplateRadio", true);

    /// <summary>
    /// Gets the reference to the create from scratch radio button
    /// </summary>
    protected virtual RadioButton CreateFromScratchRadio => this.Container.GetControl<RadioButton>("createFromScratchRadio", true);

    /// <summary>
    /// Gets the reference to the container holding settings for creating the template from scratch.
    /// </summary>
    protected virtual HtmlGenericControl FromScratchContainer => this.Container.GetControl<HtmlGenericControl>("fromScratchContainer", true);

    /// <summary>
    /// Gets the reference to the choice field with available message templates.
    /// </summary>
    protected virtual ChoiceField TemplatesChoiceField => this.Container.GetControl<ChoiceField>("templatesChoiceField", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    protected override void InitializeControls(GenericContainer container) => this.BindMessageTemplates();

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
      ScriptControlDescriptor controlDescriptor = scriptDescriptors.Count<ScriptDescriptor>() != 0 ? (ScriptControlDescriptor) scriptDescriptors.Last<ScriptDescriptor>() : new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddElementProperty("htmlCampaignRadio", this.HtmlCampaignRadio.ClientID);
      controlDescriptor.AddElementProperty("plainTextCampaignRadio", this.PlainTextCampaignRadio.ClientID);
      controlDescriptor.AddElementProperty("standardCampaignRadio", this.StandardCampaignRadio.ClientID);
      controlDescriptor.AddElementProperty("standardCampaignOptions", this.StandardCampaignOptions.ClientID);
      controlDescriptor.AddElementProperty("standardCampaignInternalPageRadio", this.StandardCampaignInternalPageRadio.ClientID);
      controlDescriptor.AddElementProperty("standardCampaignExternalPageRadio", this.StandardCampaignExternalPageRadio.ClientID);
      controlDescriptor.AddElementProperty("createFromTemplateRadio", this.CreateFromTemplateRadio.ClientID);
      controlDescriptor.AddElementProperty("createFromScratchRadio", this.CreateFromScratchRadio.ClientID);
      controlDescriptor.AddElementProperty("fromScratchContainer", this.FromScratchContainer.ClientID);
      controlDescriptor.AddComponentProperty("templatesChoiceField", this.TemplatesChoiceField.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.Scripts.CampaignTypeStep.js", typeof (CampaignTypeStep).Assembly.FullName)
    };

    private void BindMessageTemplates()
    {
      this.TemplatesChoiceField.Choices.Clear();
      IOrderedQueryable<MessageBody> orderedQueryable = this.Manager.GetMessageBodies().Where<MessageBody>((Expression<Func<MessageBody, bool>>) (mb => mb.IsTemplate == true)).OrderBy<MessageBody, string>((Expression<Func<MessageBody, string>>) (mb => mb.Name));
      this.TemplatesChoiceField.Choices.Add(new ChoiceItem()
      {
        Text = Res.Get<Labels>().SelectATemplate,
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
