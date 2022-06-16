// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomyForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  /// <summary>The dialog for creating a new taxonomy/classification</summary>
  public class TaxonomyForm : AjaxDialogBase
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.TaxonomyForm.ascx");
    private const string taxonomiesServiceUrl = "~/Sitefinity/Services/Taxonomies/Taxonomy.svc";
    private bool? supportsMultiligual;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TaxonomyForm.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this control supports multiligual.
    /// </summary>
    /// <value><c>true</c> if supports multiligual; otherwise, <c>false</c>.</value>
    public bool SupportsMultilingual
    {
      get
      {
        if (!this.supportsMultiligual.HasValue)
          this.supportsMultiligual = new bool?(AppSettings.CurrentSettings.Multilingual);
        return this.supportsMultiligual.Value;
      }
      set => this.supportsMultiligual = new bool?(value);
    }

    /// <summary>
    /// Gets the reference to the hidden field that holds the url of the taxonomy WCF service.
    /// </summary>
    protected virtual HiddenField TaxonomiesServiceUrl => this.Container.GetControl<HiddenField>("taxonomiesServiceUrl", true);

    /// <summary>
    /// Gets the reference to the generic html control that represents the choice of
    /// flat taxonomy.
    /// </summary>
    protected virtual HtmlInputRadioButton FlatRadio => this.Container.GetControl<HtmlInputRadioButton>("flatRadio", true);

    /// <summary>
    /// Gets the reference to the generic html control that represents the choice of
    /// hierarchical taxonomy.
    /// </summary>
    protected virtual HtmlInputRadioButton HierarchicalRadio => this.Container.GetControl<HtmlInputRadioButton>("hierarchyRadio", true);

    /// <summary>
    /// Gets the hidden field containing the value that indicates the language mode.
    /// </summary>
    protected virtual HiddenField SupportsMultilingualHiddenField => this.Container.GetControl<HiddenField>("supportsMultilingual", true);

    /// <summary>
    /// Gets the hidden field containing the default language.
    /// </summary>
    /// <value>The default language.</value>
    protected virtual HiddenField DefaultLanguage => this.Container.GetControl<HiddenField>("defaultLanguage", true);

    /// <summary>Gets the language list field.</summary>
    /// <value>The language list field.</value>
    protected internal LanguageListField LanguageListField => this.Container.GetControl<LanguageListField>("languageListField", false);

    /// <summary>Gets the language selector.</summary>
    /// <value>The language selector.</value>
    protected virtual LanguageChoiceField LanguageSelector => this.Container.GetControl<LanguageChoiceField>("languageChoiceField", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.TaxonomiesServiceUrl.Value = this.ResolveClientUrl("~/Sitefinity/Services/Taxonomies/Taxonomy.svc");
      this.FlatRadio.Attributes["Value"] = typeof (FlatTaxonomy).Name;
      this.HierarchicalRadio.Attributes["Value"] = typeof (HierarchicalTaxonomy).Name;
      this.SupportsMultilingualHiddenField.Value = this.SupportsMultilingual.ToString().ToLower();
      this.LanguageListField.Visible = this.SupportsMultilingual;
      if (!this.SupportsMultilingual)
        return;
      this.DefaultLanguage.Value = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference()
      {
        Assembly = typeof (TaxonomyForm).Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      }
    }.ToArray();
  }
}
