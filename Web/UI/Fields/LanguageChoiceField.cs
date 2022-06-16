// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  public class LanguageChoiceField : ChoiceField
  {
    private bool? isToRender;
    private bool changeCultureOnSave = true;
    private LanguageSource languageSource = LanguageSource.Frontend;
    private const string LanguagesSelectionScriptName = "Telerik.Sitefinity.Localization.Web.UI.Scripts.LanguagesSelection.js";
    private const string SelfExecutableScriptName = "Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js";
    private const string LanguageChoiceFieldScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.LanguageChoiceField.js";
    private CultureInfo[] languages;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceField" /> class.
    /// </summary>
    public LanguageChoiceField() => this.HideIfSingleLanguage = false;

    /// <summary>Gets or sets the language source.</summary>
    /// <value>The language source.</value>
    [DefaultValue(LanguageSource.Frontend)]
    public LanguageSource LanguageSource
    {
      get => this.languageSource;
      set => this.languageSource = value;
    }

    /// <summary>Gets or sets the languages to show.</summary>
    /// <value>The languages to show.</value>
    [DefaultValue(LanguagesSelection.Unavailable)]
    public LanguagesSelection LanguagesToShow { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to change the culture of the caller on save.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to change the culture of the caller on save; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(true)]
    public bool ChangeCultureOnSave
    {
      get => this.changeCultureOnSave;
      set => this.changeCultureOnSave = value;
    }

    /// <summary>
    /// Gets or sets the list of all listed languages. This is only used if LanguageSource is set to Custom.
    /// </summary>
    /// <value>All available languages.</value>
    public IList<string> AvailableLanguages { get; set; }

    /// <summary>
    /// Gets or sets a list of languages that will be excluded from representation. This is only used when
    /// the LanguageSource is not Custom. Using this property you can exclude specific languages.
    /// </summary>
    /// <value>The excluded languages.</value>
    public List<CultureInfo> ExcludedLanguages { get; set; }

    /// <summary>
    /// Gets or sets whether to hide the field if only a single language is found.
    /// </summary>
    /// <value>True if the field must be hidden when only a single language is found.</value>
    public bool HideIfSingleLanguage { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.Visible)
        return;
      CultureInfo[] languages = this.GetLanguages();
      if (this.HideIfSingleLanguage && ((IEnumerable<CultureInfo>) languages).Count<CultureInfo>() <= 1)
        this.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
      else
        this.BindLanguages();
      base.InitializeControls(container);
    }

    /// <summary>
    /// Gets or sets a value that indicates whether a server control is rendered as UI on the page.
    /// </summary>
    /// <value></value>
    /// <returns>true if the control is visible on the page; otherwise false.</returns>
    public override bool Visible
    {
      get
      {
        if (!this.isToRender.HasValue)
          this.isToRender = new bool?(SystemManager.CurrentContext.AppSettings.Multilingual);
        return this.isToRender.Value;
      }
      set => this.isToRender = new bool?(value);
    }

    public override object Value
    {
      get => base.Value;
      set
      {
        base.Value = value;
        this.ValueChanged();
      }
    }

    protected virtual void ValueChanged()
    {
      if (this.DisplayMode != FieldDisplayMode.Read)
        return;
      string name = this.Value as string;
      if (string.IsNullOrEmpty(name))
        this.ReadModeLabel.Text = Res.Get<Labels>().NotSelected;
      else
        this.ReadModeLabel.Text = new CultureInfo(name).NativeName;
    }

    protected virtual string GetHtmlValueForCulture(CultureInfo culture) => culture.Name;

    protected virtual string GetHtmlLabelForCulture(CultureInfo culture) => culture.NativeName;

    protected virtual void BindLanguages()
    {
      foreach (CultureInfo language in this.GetLanguages())
        this.Choices.Add(new ChoiceItem()
        {
          Text = this.GetHtmlLabelForCulture(language),
          Value = this.GetHtmlValueForCulture(language)
        });
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBase(definition);
      if (!(definition is ILanguageChoiceFieldDefinition choiceFieldDefinition))
        return;
      if (choiceFieldDefinition.LanguageSource.HasValue)
        this.LanguageSource = choiceFieldDefinition.LanguageSource.Value;
      if (choiceFieldDefinition.HideIfSingleLanguage.HasValue)
        this.HideIfSingleLanguage = choiceFieldDefinition.HideIfSingleLanguage.Value;
      if (!choiceFieldDefinition.LanguagesToShow.HasValue)
        return;
      this.LanguagesToShow = choiceFieldDefinition.LanguagesToShow.Value;
    }

    internal new virtual void ConfigureBase(IFieldDefinition definition) => base.Configure(definition);

    protected virtual CultureInfo[] GetLanguages()
    {
      if (this.languages == null)
      {
        List<CultureInfo> cultureInfoList = new List<CultureInfo>();
        if (this.LanguageSource == LanguageSource.Custom)
        {
          foreach (string availableLanguage in (IEnumerable<string>) this.AvailableLanguages)
          {
            CultureInfo cultureInfo = new CultureInfo(availableLanguage);
            cultureInfoList.Add(cultureInfo);
          }
        }
        else
        {
          IEnumerable<CultureInfo> cultureInfos = (IEnumerable<CultureInfo>) null;
          if (this.LanguageSource == LanguageSource.Backend)
          {
            cultureInfos = (IEnumerable<CultureInfo>) AppSettings.CurrentSettings.DefinedBackendLanguages;
            if (this.ExcludedLanguages != null)
              cultureInfos = cultureInfos.Where<CultureInfo>((Func<CultureInfo, bool>) (ci => !this.ExcludedLanguages.Contains(ci)));
          }
          else if (this.LanguageSource == LanguageSource.Frontend)
          {
            cultureInfos = (IEnumerable<CultureInfo>) SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages;
            if (this.ExcludedLanguages != null)
              cultureInfos = cultureInfos.Where<CultureInfo>((Func<CultureInfo, bool>) (ci => !this.ExcludedLanguages.Contains(ci)));
          }
          cultureInfoList.AddRange(cultureInfos);
        }
        this.languages = cultureInfoList.ToArray();
      }
      return this.languages;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.Type = this.ScriptDescriptorTypeName;
      controlDescriptor.AddProperty("languagesToShow", (object) this.LanguagesToShow);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (LanguageChoiceField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Localization.Web.UI.Scripts.LanguagesSelection.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.LanguageChoiceField.js", fullName)
      };
    }
  }
}
