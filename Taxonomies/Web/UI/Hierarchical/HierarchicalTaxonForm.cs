// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical
{
  /// <summary>
  /// Dialog for creating new or editing existing hierarchical taxon
  /// </summary>
  public class HierarchicalTaxonForm : AjaxDialogBase
  {
    private static RegexStrategy regexStrategy = (RegexStrategy) null;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.Hierarchical.HierarchicalTaxonForm.ascx");
    private const string webServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc";
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? HierarchicalTaxonForm.layoutTemplatePath : base.LayoutTemplatePath;
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
    /// Gets the reference to the hidden field which holds the url of the web service
    /// </summary>
    protected virtual HiddenField WebServiceUrl => this.Container.GetControl<HiddenField>("webServiceUrl", true);

    /// <summary>
    /// Gets the hidden field containing the default language.
    /// </summary>
    /// <value>The default language.</value>
    protected virtual HiddenField DefaultLanguage => this.Container.GetControl<HiddenField>("defaultLanguage", true);

    /// <summary>
    /// Gets the hidden field containing the value that indicates the language mode.
    /// </summary>
    protected virtual HiddenField SupportsMultilingualHiddenField => this.Container.GetControl<HiddenField>("supportsMultilingual", true);

    /// <summary>Gets the language list field.</summary>
    /// <value>The language list field.</value>
    protected internal LanguageListField LanguageListField => this.Container.GetControl<LanguageListField>("languageListField", false);

    /// <summary>Gets the language selector.</summary>
    /// <value>The language selector.</value>
    protected virtual LanguageChoiceField LanguageSelector => this.Container.GetControl<LanguageChoiceField>("languageChoiceField", true);

    /// <summary>Gets the mirror text field for the taxon name.</summary>
    /// <value>The mirror text field.</value>
    protected virtual MirrorTextField TaxonName => this.Container.GetControl<MirrorTextField>("taxonName", true);

    /// <summary>Gets the mirror text field for the taxon URL name.</summary>
    /// <value>The mirror text field.</value>
    protected virtual MirrorTextField TaxonUrlName => this.Container.GetControl<MirrorTextField>("taxonUrlName", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.WebServiceUrl.Value = this.ResolveUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc");
      this.SupportsMultilingualHiddenField.Value = this.SupportsMultilingual.ToString().ToLower();
      this.LanguageListField.Visible = this.SupportsMultilingual;
      if (this.SupportsMultilingual)
        this.DefaultLanguage.Value = AppSettings.CurrentSettings.DefaultFrontendLanguage.Name;
      this.TaxonName.RegularExpressionFilter = HierarchicalTaxonForm.RgxStrategy.DefaultExpressionFilter;
      if (this.TaxonName.ValidatorDefinition != null)
        this.TaxonName.ValidatorDefinition.RegularExpression = HierarchicalTaxonForm.RgxStrategy.DefaultValidationExpression;
      this.TaxonUrlName.RegularExpressionFilter = HierarchicalTaxonForm.RgxStrategy.DefaultExpressionFilter;
      if (this.TaxonUrlName.ValidatorDefinition == null)
        return;
      this.TaxonUrlName.ValidatorDefinition.RegularExpression = HierarchicalTaxonForm.RgxStrategy.DefaultValidationExpression;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => base.GetScriptDescriptors();

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
        Assembly = typeof (HierarchicalTaxonForm).Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      }
    }.ToArray();

    private static RegexStrategy RgxStrategy
    {
      get
      {
        if (HierarchicalTaxonForm.regexStrategy == null)
          HierarchicalTaxonForm.regexStrategy = ObjectFactory.Resolve<RegexStrategy>();
        return HierarchicalTaxonForm.regexStrategy;
      }
    }
  }
}
