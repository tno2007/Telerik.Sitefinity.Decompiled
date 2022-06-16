// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.TemplateForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms
{
  /// <summary>Base class inherited by all template forms.</summary>
  public class TemplateForm : AjaxDialogBase
  {
    private NewslettersManager manager;
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.TemplateForm.js";
    internal const string clientManagerReference = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";
    private const string webServiceUrl = "~/Sitefinity/Services/Newsletters/MessageTemplate.svc";
    private const string createTemplateDisplayMode = "createTemplate";
    private const string addContentDisplayMode = "addContent";
    private const string editTemplateDisplayMode = "editTemplate";
    private const string editContentDisplayMode = "editContent";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Forms.TemplateForm.ascx");

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (TemplateForm).FullName;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TemplateForm.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" />.
    /// </summary>
    protected NewslettersManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = NewslettersManager.GetManager();
        return this.manager;
      }
    }

    /// <summary>
    /// Gets the reference to the control that displays the title of the template form.
    /// </summary>
    protected virtual Label FormTitleControl => this.Container.GetControl<Label>("formTitleControl", true);

    /// <summary>
    /// Gets the reference to the text field representing the name of the template.
    /// </summary>
    protected virtual TextField TemplateNameField => this.Container.GetControl<TextField>("templateName", true);

    /// <summary>Gets the reference to the save changes button.</summary>
    protected virtual LinkButton SaveChangesButton => this.Container.GetControl<LinkButton>("saveChangesButton", true);

    /// <summary>Gets the reference to the save changes label.</summary>
    protected virtual Label SaveChangesLabel => this.Container.GetControl<Label>("saveChangesLabel", true);

    /// <summary>Gets the reference to the html template radio button.</summary>
    protected virtual RadioButton HtmlTemplateRadio => this.Container.GetControl<RadioButton>("htmlTemplateRadio", true);

    /// <summary>Gets the reference to the html text panel control.</summary>
    protected virtual HtmlGenericControl HtmlTextPanel => this.Container.GetControl<HtmlGenericControl>("htmlTextPanel", true);

    /// <summary>Gets the reference to the html text control.</summary>
    protected virtual HtmlField HtmlTextControl => this.Container.GetControl<HtmlField>("htmlTextControl", true);

    /// <summary>
    /// Gets the reference to the plain text template radio button.
    /// </summary>
    protected virtual RadioButton PlainTextTemplateRadio => this.Container.GetControl<RadioButton>("plainTextTemplateRadio", true);

    /// <summary>Gets the reference to the plain text panel control.</summary>
    protected virtual HtmlGenericControl PlainTextPanel => this.Container.GetControl<HtmlGenericControl>("plainTextPanel", true);

    /// <summary>Gets the reference to the plain text control.</summary>
    protected virtual TextBox PlainTextControl => this.Container.GetControl<TextBox>("plainTextControl", true);

    /// <summary>Gets the reference to the standard template radio.</summary>
    protected virtual RadioButton StandardTemplateRadio => this.Container.GetControl<RadioButton>("standardTemplateRadio", true);

    /// <summary>Gets the reference to the merge tag selector</summary>
    protected virtual MergeTagSelector MergeTagSelector => this.Container.GetControl<MergeTagSelector>("mergeTagSelector", true);

    /// <summary>
    /// Gets the reference to the merge tag selector of the html templates.
    /// </summary>
    protected virtual MergeTagSelector HtmlMergeTagSelector => this.Container.GetControl<MergeTagSelector>("htmlMergeTagSelector", true);

    /// <summary>
    /// Gets the reference to the template name panel control.
    /// </summary>
    protected virtual HtmlGenericControl TemplateNamePanel => this.Container.GetControl<HtmlGenericControl>("templateNamePanel", true);

    /// <summary>
    /// Gets the reference to the template type panel control.
    /// </summary>
    protected virtual HtmlGenericControl TemplateTypePanel => this.Container.GetControl<HtmlGenericControl>("templateTypePanel", true);

    /// <summary>
    /// Gets the reference to the template content panel control.
    /// </summary>
    protected virtual HtmlGenericControl TemplateContentPanel => this.Container.GetControl<HtmlGenericControl>("templateContentPanel", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.BindMergeTags(this.MergeTagSelector);
      this.BindHtmlMergeTags(this.HtmlMergeTagSelector);
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void BindMergeTags(MergeTagSelector selector)
    {
      foreach (MergeTag mergeTag in (IEnumerable<MergeTag>) this.Manager.GetMergeTags())
      {
        if (!(mergeTag.PropertyName == "UnsubscribeLink"))
          selector.MergeTags.Add(mergeTag);
      }
    }

    private void BindHtmlMergeTags(MergeTagSelector selector)
    {
      IList<MergeTag> mergeTags = this.Manager.GetMergeTags();
      selector.MergeTags.AddRange((IEnumerable<MergeTag>) mergeTags);
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
      controlDescriptor.AddProperty("webServiceUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/MessageTemplate.svc"));
      controlDescriptor.AddProperty("createTemplateTitle", (object) Res.Get<NewslettersResources>().CreateATemplate);
      controlDescriptor.AddProperty("editTemplateTitle", (object) Res.Get<NewslettersResources>().EditTemplate);
      controlDescriptor.AddProperty("_createAndAddContentText", (object) Res.Get<PageResources>().CreateAndAddContent);
      controlDescriptor.AddProperty("_saveChangesText", (object) Res.Get<Labels>().SaveChanges);
      controlDescriptor.AddProperty("_saveText", (object) Res.Get<Labels>().Save);
      controlDescriptor.AddProperty("_editorUrl", (object) this.Page.ResolveUrl("~/Sitefinity/SFNwslttrs/"));
      controlDescriptor.AddProperty("_invariantCulture", (object) "INVARIANT");
      controlDescriptor.AddProperty("_createTemplateDisplayMode", (object) "createTemplate");
      controlDescriptor.AddProperty("_addContentDisplayMode", (object) "addContent");
      controlDescriptor.AddProperty("_editTemplateDisplayMode", (object) "editTemplate");
      controlDescriptor.AddProperty("_editContentDisplayMode", (object) "editContent");
      controlDescriptor.AddElementProperty("htmlTemplateRadio", this.HtmlTemplateRadio.ClientID);
      controlDescriptor.AddElementProperty("htmlTextPanel", this.HtmlTextPanel.ClientID);
      controlDescriptor.AddElementProperty("plainTextTemplateRadio", this.PlainTextTemplateRadio.ClientID);
      controlDescriptor.AddElementProperty("plainTextPanel", this.PlainTextPanel.ClientID);
      controlDescriptor.AddElementProperty("plainTextControl", this.PlainTextControl.ClientID);
      controlDescriptor.AddElementProperty("standardTemplateRadio", this.StandardTemplateRadio.ClientID);
      controlDescriptor.AddElementProperty("saveChangesButton", this.SaveChangesButton.ClientID);
      controlDescriptor.AddElementProperty("saveChangesLabel", this.SaveChangesLabel.ClientID);
      controlDescriptor.AddElementProperty("formTitleControl", this.FormTitleControl.ClientID);
      controlDescriptor.AddElementProperty("templateNamePanel", this.TemplateNamePanel.ClientID);
      controlDescriptor.AddElementProperty("templateTypePanel", this.TemplateTypePanel.ClientID);
      controlDescriptor.AddElementProperty("templateContentPanel", this.TemplateContentPanel.ClientID);
      controlDescriptor.AddComponentProperty("templateNameField", this.TemplateNameField.ClientID);
      controlDescriptor.AddComponentProperty("htmlTextControl", this.HtmlTextControl.ClientID);
      controlDescriptor.AddComponentProperty("mergeTagSelector", this.MergeTagSelector.ClientID);
      controlDescriptor.AddComponentProperty("htmlMergeTagSelector", this.HtmlMergeTagSelector.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", typeof (TemplateForm).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.TemplateForm.js", typeof (TemplateForm).Assembly.FullName)
    };
  }
}
