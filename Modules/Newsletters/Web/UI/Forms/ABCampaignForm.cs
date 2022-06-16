// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ABCampaignForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms
{
  /// <summary>The form for defining AB campaign.</summary>
  [Obsolete("Use AbTestDetailView instead.")]
  public class ABCampaignForm : AjaxDialogBase
  {
    private NewslettersManager manager;
    private IList<Campaign> allCampaigns;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Forms.ABCampaignForm.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.ABCampaignForm.js";
    private const string clientManagerScript = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";
    private const string webServiceUrl = "~/Sitefinity/Services/Newsletters/ABCampaign.svc";

    /// <summary>Gets or sets the name of the newsletters provider.</summary>
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ABCampaignForm.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (ABCampaignForm).FullName;

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> to be used by this
    /// control.
    /// </summary>
    protected NewslettersManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = NewslettersManager.GetManager(this.ProviderName);
        return this.manager;
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the list of all campaigns.</summary>
    protected IList<Campaign> AllCampaigns
    {
      get
      {
        if (this.allCampaigns == null)
          this.allCampaigns = this.Manager.GetABTestingEligableCampaigns();
        return this.allCampaigns;
      }
    }

    /// <summary>Gets the reference to the back link.</summary>
    protected virtual LinkButton BackLink => this.Container.GetControl<LinkButton>("backLink", true);

    /// <summary>Gets the reference to the ab test form title.</summary>
    protected virtual Label AbTestFormTitle => this.Container.GetControl<Label>("abTestFormTitle", true);

    /// <summary>Gets the Name text field.</summary>
    protected virtual TextField NameTextField => this.Container.GetControl<TextField>("nameTextField", true);

    /// <summary>Gets the reference to the Campaign A choice field.</summary>
    protected virtual ChoiceField CampaignAChoiceField => this.Container.GetControl<ChoiceField>("campaignAChoiceField", true);

    /// <summary>Gets the reference to the Campaign B choice field.</summary>
    protected virtual ChoiceField CampaignBChoiceField => this.Container.GetControl<ChoiceField>("campaignBChoiceField", true);

    /// <summary>
    /// Gets the reference to the winning factor choice field.
    /// </summary>
    protected virtual ChoiceField WinningFactorChoiceField => this.Container.GetControl<ChoiceField>("winningFactorChoiceField", true);

    /// <summary>Gets the reference to the test sample slider.</summary>
    protected virtual RadSlider TestSampleSlider => this.Container.GetControl<RadSlider>("testSampleSlider", true);

    /// <summary>
    /// Gets the reference to the label showing the testing sample percentage
    /// </summary>
    protected virtual Label TestingSamplePercentageLabel => this.Container.GetControl<Label>("testingSamplePercentageLabel", true);

    /// <summary>Gets the reference to the testing period end picker</summary>
    protected virtual RadDateTimePicker TestingPeriodEndPicker => this.Container.GetControl<RadDateTimePicker>("testingPeriodEndPicker", true);

    /// <summary>Gets the reference to the command bar.</summary>
    protected virtual CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true);

    /// <summary>
    /// Gets the reference to the control that holds the id of the form itself.
    /// </summary>
    protected virtual HiddenField FormIdHidden => this.Container.GetControl<HiddenField>("formIdHidden", true);

    /// <summary>Gets the reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.FormIdHidden.Value = this.ClientID;
      this.BindCampaigns();
      this.TestingPeriodEndPicker.SelectedDate = new DateTime?(DateTime.Now.AddDays(7.0));
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      controlDescriptor.AddProperty("_webServiceUrl", (object) VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Newsletters/ABCampaign.svc")));
      controlDescriptor.AddProperty("_campaignBMatrix", (object) scriptSerializer.Serialize((object) this.GenerateCampaignBMatrix()));
      controlDescriptor.AddElementProperty("backLink", this.BackLink.ClientID);
      controlDescriptor.AddElementProperty("abTestFormTitle", this.AbTestFormTitle.ClientID);
      controlDescriptor.AddComponentProperty("nameTextField", this.NameTextField.ClientID);
      controlDescriptor.AddComponentProperty("campaignAChoiceField", this.CampaignAChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("campaignBChoiceField", this.CampaignBChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("winningFactorChoiceField", this.WinningFactorChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("testSampleSlider", this.TestSampleSlider.ClientID);
      controlDescriptor.AddComponentProperty("testingPeriodEndPicker", this.TestingPeriodEndPicker.ClientID);
      controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      controlDescriptor.AddElementProperty("testingSamplePercentageLabel", this.TestingSamplePercentageLabel.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", typeof (ABCampaignForm).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.ABCampaignForm.js", typeof (ABCampaignForm).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void BindCampaigns()
    {
      foreach (Campaign allCampaign in (IEnumerable<Campaign>) this.AllCampaigns)
        this.CampaignAChoiceField.Choices.Add(new ChoiceItem()
        {
          Text = allCampaign.Name,
          Value = allCampaign.Id.ToString()
        });
    }

    private Dictionary<string, object> GenerateCampaignBMatrix()
    {
      Dictionary<string, object> campaignBmatrix = new Dictionary<string, object>();
      foreach (Campaign allCampaign in (IEnumerable<Campaign>) this.AllCampaigns)
      {
        Campaign campaign = allCampaign;
        if (!campaignBmatrix.ContainsKey(campaign.Id.ToString()))
        {
          IEnumerable<\u003C\u003Ef__AnonymousType56<string, Guid>> source = this.AllCampaigns.Where<Campaign>((Func<Campaign, bool>) (c => c.List.Id == campaign.List.Id)).Select(c => new
          {
            Name = c.Name,
            Id = c.Id
          });
          campaignBmatrix.Add(campaign.Id.ToString(), (object) source.ToList());
        }
      }
      return campaignBmatrix;
    }
  }
}
