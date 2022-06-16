// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.LanguageListControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>Represents a list of available languages.</summary>
  [PersistChildren(false)]
  [ParseChildren(true)]
  public class LanguageListControl : SimpleScriptView
  {
    private Dictionary<string, Control> languageElements = new Dictionary<string, Control>();
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Localization.LanguageListControl.ascx");
    private List<CultureInfo> al;

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LanguageListControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override string TagName => "div";

    /// <summary>Gets or sets source for available languages.</summary>
    /// <value>The language source.</value>
    [DefaultValue(LanguageSource.Backend)]
    public LanguageSource LanguageSource { get; set; }

    /// <summary>
    /// Gets or sets the list of all listed languages. This is only used if LanguageSource is set to Custom.
    /// </summary>
    /// <value>All available languages.</value>
    public IList<string> AvailableLanguages { get; set; }

    /// <summary>
    /// Gets or sets the list of languages that are currently in use.
    /// </summary>
    /// <value>The languages that are currently in use.</value>
    public List<CultureInfo> LanguagesInUse
    {
      get => this.al;
      set => this.al = value;
    }

    /// <summary>
    /// Gets or sets a list of languages that will be excluded from representation. This is only used when
    /// the LanguageSource is not Custom. Using this property you can exclude specific languages.
    /// </summary>
    /// <value>The excluded languages.</value>
    public List<CultureInfo> ExcludedLanguages { get; set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }

    [DefaultValue(null)]
    [Browsable(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (RepeaterItem))]
    public ITemplate ItemTemplate { get; set; }

    [DefaultValue(null)]
    [Browsable(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (RepeaterItem))]
    public ITemplate HeaderTemplate { get; set; }

    [DefaultValue(null)]
    [Browsable(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (RepeaterItem))]
    public ITemplate FooterTemplate { get; set; }

    /// <summary>Gets the control wrapper.</summary>
    /// <value>The control wrapper.</value>
    public HtmlGenericControl ControlWrapper => this.Container.GetControl<HtmlGenericControl>("wrapper", false);

    /// <summary>Gets the languages panel.</summary>
    /// <value>The languages panel.</value>
    public HtmlGenericControl LanguagesPanel => this.Container.GetControl<HtmlGenericControl>("languagesPanel", true);

    public Repeater LanguagesRepeater => this.Container.GetControl<Repeater>("languagesRepeater", true);

    public Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The dialog container.</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      this.TitleLabel.Text = this.Title == null ? "" : this.Title;
      if (this.HeaderTemplate != null)
        this.LanguagesRepeater.HeaderTemplate = this.HeaderTemplate;
      if (this.ItemTemplate != null)
        this.LanguagesRepeater.ItemTemplate = this.ItemTemplate;
      if (this.FooterTemplate != null)
        this.LanguagesRepeater.FooterTemplate = this.FooterTemplate;
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
        AppSettings currentSettings = AppSettings.CurrentSettings;
        IEnumerable<CultureInfo> cultureInfos = (IEnumerable<CultureInfo>) null;
        if (this.LanguageSource == LanguageSource.Backend)
        {
          cultureInfos = (IEnumerable<CultureInfo>) currentSettings.DefinedBackendLanguages;
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
      this.languageElements.Clear();
      this.LanguagesRepeater.ItemCreated += new RepeaterItemEventHandler(this.LanguagesRepeater_ItemCreated);
      this.LanguagesRepeater.DataSource = (object) cultureInfoList;
      this.LanguagesRepeater.DataBind();
    }

    private void LanguagesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      CultureInfo dataItem = (CultureInfo) e.Item.DataItem;
      HtmlGenericControl control1 = (HtmlGenericControl) e.Item.FindControl("languageItem");
      control1.Attributes.Add("title", dataItem.NativeName);
      string str = "sfLangWrp sfLang_" + dataItem.Name;
      HtmlControl control2;
      if (this.IsLanguageUsed(dataItem))
      {
        control2 = (HtmlControl) e.Item.FindControl("addButton");
      }
      else
      {
        control2 = (HtmlControl) e.Item.FindControl("editButton");
        str += " sfNotTranslated";
      }
      control2.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
      control1.Attributes.Add("class", str);
      ((HtmlContainerControl) e.Item.FindControl("languageNameAdd")).InnerHtml = dataItem.Name;
      ((HtmlContainerControl) e.Item.FindControl("languageNameEdit")).InnerHtml = dataItem.Name;
      this.languageElements[dataItem.Name] = (Control) control1;
    }

    public bool IsLanguageUsed(CultureInfo ci) => this.LanguagesInUse != null && this.LanguagesInUse.Contains(ci);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().FullName, this.ClientID);
      behaviorDescriptor.AddElementProperty("titleLabelElement", this.TitleLabel.ClientID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (KeyValuePair<string, Control> languageElement in this.languageElements)
        dictionary.Add(languageElement.Key, languageElement.Value.ClientID);
      string str1 = scriptSerializer.Serialize((object) dictionary);
      behaviorDescriptor.AddProperty("languageElementIds", (object) str1);
      List<string> stringList = new List<string>();
      if (this.LanguagesInUse != null)
      {
        foreach (CultureInfo cultureInfo in this.LanguagesInUse)
          stringList.Add(cultureInfo.Name);
      }
      string str2 = scriptSerializer.Serialize((object) stringList.ToArray());
      behaviorDescriptor.AddProperty("languagesInUse", (object) str2);
      behaviorDescriptor.AddProperty("_createCommandName", (object) "create");
      behaviorDescriptor.AddProperty("_editCommandName", (object) "edit");
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = this.GetType().Assembly.FullName;
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Localization.Web.UI.Scripts.LanguageListControl.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
