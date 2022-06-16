// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignMessageStep
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps
{
  /// <summary>
  /// Step of the campaign wizard that lets user compose the message of the campaign
  /// </summary>
  public class CampaignMessageStep : SitefinityWizardStepControl
  {
    internal new const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.Scripts.CampaignMessageStep.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.Steps.CampaignMessageStep.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CampaignMessageStep.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the reference to the plain text panel control.</summary>
    protected virtual HtmlGenericControl PlainTextPanel => this.Container.GetControl<HtmlGenericControl>("plainTextPanel", true);

    /// <summary>Gets the reference to the html text panel control.</summary>
    protected virtual HtmlGenericControl HtmlTextPanel => this.Container.GetControl<HtmlGenericControl>("htmlTextPanel", true);

    /// <summary>
    /// Gets the reference to the internal page panel control.
    /// </summary>
    protected virtual HtmlGenericControl InternalPagePanel => this.Container.GetControl<HtmlGenericControl>("internalPagePanel", true);

    /// <summary>
    /// Gets the reference to the external page panel control.
    /// </summary>
    protected virtual HtmlGenericControl ExternalPagePanel => this.Container.GetControl<HtmlGenericControl>("externalPagePanel", true);

    /// <summary>Gets the reference to plain text control.</summary>
    protected virtual TextBox PlainTextControl => this.Container.GetControl<TextBox>("plainTextControl", true);

    /// <summary>Gets the reference to html text control.</summary>
    protected virtual HtmlField HtmlTextControl => this.Container.GetControl<HtmlField>("htmlTextControl", true);

    /// <summary>Gets the reference to the existing pages selector.</summary>
    protected virtual PagesSelector ExistingPagesSelector => this.Container.GetControl<PagesSelector>("externalPagesSelector", true);

    /// <summary>
    /// Gets the reference to the link button that fires up the zone editor for internal page editor.
    /// </summary>
    protected virtual LinkButton EditInternalPageLink => this.Container.GetControl<LinkButton>("editInternalPageLink", true);

    /// <summary>Gets the reference to the merge tag selector control.</summary>
    protected virtual MergeTagSelector MergeTagSelector => this.Container.GetControl<MergeTagSelector>("mergeTagSelector", true);

    /// <summary>
    /// Gets the reference to the merge tag selector control for html messages.
    /// </summary>
    protected virtual MergeTagSelector HtmlMergeTagSelector => this.Container.GetControl<MergeTagSelector>("htmlMergeTagSelector", true);

    /// <summary>
    /// Gets the reference to the plain text generation choice field in Html mode.
    /// </summary>
    protected virtual ChoiceField PlainTextGenerationHtml => this.Container.GetControl<ChoiceField>("plainTextGenerationHtml", true);

    /// <summary>
    /// Gets the reference to the plain text version panel control in Html mode.
    /// </summary>
    protected virtual HtmlGenericControl PlainTextVersionHtmlPanel => this.Container.GetControl<HtmlGenericControl>("plainTextVersionHtmlPanel", true);

    /// <summary>
    /// Gets the reference to the plain text version text box in Html mode.
    /// </summary>
    protected virtual TextBox PlainTextVersionHtml => this.Container.GetControl<TextBox>("plainTextVersionHtml", true);

    /// <summary>
    /// Gets the reference to the plain text generation choice field in Page mode.
    /// </summary>
    protected virtual ChoiceField PlainTextGenerationPage => this.Container.GetControl<ChoiceField>("plainTextGenerationPage", true);

    /// <summary>
    /// Gets the reference to the plain text version panel control in Page mode.
    /// </summary>
    protected virtual HtmlGenericControl PlainTextVersionPagePanel => this.Container.GetControl<HtmlGenericControl>("plainTextVersionPagePanel", true);

    /// <summary>
    /// Gets the reference to the plain text version text box in Page mode.
    /// </summary>
    protected virtual TextBox PlainTextVersionPage => this.Container.GetControl<TextBox>("plainTextVersionPage", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
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
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = scriptDescriptors.Count<ScriptDescriptor>() != 0 ? (ScriptControlDescriptor) scriptDescriptors.Last<ScriptDescriptor>() : new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("_editorUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/SFNwslttrs/"));
      controlDescriptor.AddElementProperty("plainTextPanel", this.PlainTextPanel.ClientID);
      controlDescriptor.AddElementProperty("htmlTextPanel", this.HtmlTextPanel.ClientID);
      controlDescriptor.AddElementProperty("internalPagePanel", this.InternalPagePanel.ClientID);
      controlDescriptor.AddElementProperty("externalPagePanel", this.ExternalPagePanel.ClientID);
      controlDescriptor.AddElementProperty("plainTextControl", this.PlainTextControl.ClientID);
      controlDescriptor.AddComponentProperty("htmlTextControl", this.HtmlTextControl.ClientID);
      controlDescriptor.AddComponentProperty("existingPagesSelector", this.ExistingPagesSelector.ClientID);
      controlDescriptor.AddElementProperty("editInternalPageLink", this.EditInternalPageLink.ClientID);
      controlDescriptor.AddComponentProperty("mergeTagSelector", this.MergeTagSelector.ClientID);
      controlDescriptor.AddComponentProperty("htmlMergeTagSelector", this.HtmlMergeTagSelector.ClientID);
      controlDescriptor.AddComponentProperty("plainTextGenerationHtml", this.PlainTextGenerationHtml.ClientID);
      controlDescriptor.AddElementProperty("plainTextVersionHtmlPanel", this.PlainTextVersionHtmlPanel.ClientID);
      controlDescriptor.AddElementProperty("plainTextVersionHtml", this.PlainTextVersionHtml.ClientID);
      controlDescriptor.AddComponentProperty("plainTextGenerationPage", this.PlainTextGenerationPage.ClientID);
      controlDescriptor.AddElementProperty("plainTextVersionPagePanel", this.PlainTextVersionPagePanel.ClientID);
      controlDescriptor.AddElementProperty("plainTextVersionPage", this.PlainTextVersionPage.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.Scripts.CampaignMessageStep.js", typeof (CampaignMessageStep).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
