// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>Control that lists specific set of languages.</summary>
  [ParseChildren(true)]
  public class LanguagesDropDownList : SimpleScriptView
  {
    private const string scriptName = "Telerik.Sitefinity.Localization.Web.UI.Scripts.LanguagesDropDownList.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Localization.LanguagesDropDownList.ascx");
    private LanguageSource languageSource;
    private IAppSettings appSettings;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LanguagesDropDownList.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that
    /// corresponds to this Web server control. This property is used primarily by control
    /// developers.</summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration
    /// values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets source for available languages.</summary>
    /// <value>The language source.</value>
    public LanguageSource LanguageSource
    {
      get => this.languageSource;
      set => this.languageSource = value;
    }

    /// <summary>
    /// Gets or sets the list of all listed cultures. This is only used if LanguageSource is set to Custom.
    /// </summary>
    /// <value>All available languages.</value>
    public IList<CultureInfo> AvailableCultures { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether an option for all languages should be added.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if an option for all languages is to be added; otherwise, <c>false</c>.
    /// </value>
    public bool AddAllLanguagesOption { get; set; }

    /// <summary>Gets the default application settings information.</summary>
    protected virtual IAppSettings AppSettings
    {
      get
      {
        if (this.appSettings == null)
          this.appSettings = (IAppSettings) Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings;
        return this.appSettings;
      }
    }

    /// <summary>
    /// This property is used to determine whether to show the language selector in monolingual mode.
    /// </summary>
    internal bool ShowInMonolingual { set; get; }

    /// <summary>Get a reference to the RadComboBox in the template</summary>
    protected virtual RadComboBox ComboBox => this.Container.GetControl<RadComboBox>("comboBox", false);

    /// <summary>Get a reference to the DropDownList in the template</summary>
    protected virtual DropDownList DropDownList => this.Container.GetControl<DropDownList>("dropDownList", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (SystemManager.CurrentContext.AppSettings.Multilingual || this.ShowInMonolingual)
      {
        this.PopulateDropDown();
        this.PreselectLanguage();
      }
      else
        this.Visible = false;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (LanguagesDropDownList).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("dropDownList", this.DropDownList.ClientID);
      controlDescriptor.AddProperty("selectedIndex", (object) this.DropDownList.SelectedIndex);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Localization.Web.UI.Scripts.LanguagesDropDownList.js", typeof (LanguagesDropDownList).Assembly.GetName().FullName)
    };

    private List<CultureInfo> GetLanguages()
    {
      List<CultureInfo> languages = new List<CultureInfo>();
      if (this.LanguageSource == LanguageSource.Custom)
      {
        foreach (CultureInfo availableCulture in (IEnumerable<CultureInfo>) this.AvailableCultures)
          languages.Add(availableCulture);
      }
      else
      {
        IAppSettings appSettings = this.AppSettings;
        if (this.LanguageSource == LanguageSource.Backend)
          languages.AddRange((IEnumerable<CultureInfo>) appSettings.DefinedBackendLanguages);
        else if (this.LanguageSource == LanguageSource.Frontend)
          languages.AddRange((IEnumerable<CultureInfo>) SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages);
      }
      return languages;
    }

    private string GetDefaultLanguage()
    {
      string defaultLanguage = (string) null;
      if (this.LanguageSource == LanguageSource.Custom)
      {
        if (this.AvailableCultures.Count > 0)
          defaultLanguage = this.AvailableCultures[0].Name;
      }
      else
      {
        IAppSettings appSettings = this.AppSettings;
        if (this.LanguageSource == LanguageSource.Backend)
          defaultLanguage = appSettings.DefaultBackendLanguage.Name;
        else if (this.LanguageSource == LanguageSource.Frontend)
          defaultLanguage = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
      }
      return defaultLanguage;
    }

    private void PreselectLanguage()
    {
      if (this.Page != null)
      {
        string str = this.Page.Request.QueryStringGet("lang");
        if (!string.IsNullOrEmpty(str))
          this.DropDownList.SelectedValue = str;
        else
          this.PopulateLanguageDropdown();
      }
      else
        this.PopulateLanguageDropdown();
    }

    private void PopulateLanguageDropdown()
    {
      string defaultLanguage = this.GetDefaultLanguage();
      if (this.DropDownList.Items.FindByValue(defaultLanguage) == null)
        return;
      this.DropDownList.SelectedValue = defaultLanguage;
    }

    private void PopulateDropDown()
    {
      this.DropDownList.DataSource = (object) this.GetLanguages();
      this.DropDownList.DataTextField = "NativeName";
      this.DropDownList.DataValueField = "Name";
      this.DropDownList.DataBind();
      if (!this.AddAllLanguagesOption)
        return;
      this.DropDownList.Items.Add(new ListItem(Res.Get<Labels>().All, ""));
    }
  }
}
