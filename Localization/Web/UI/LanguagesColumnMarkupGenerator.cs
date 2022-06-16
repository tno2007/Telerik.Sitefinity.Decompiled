// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.LanguagesColumnMarkupGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.Localization.Contracts;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>
  /// An HTML markup generator that produces markup for a languages column.
  /// </summary>
  public class LanguagesColumnMarkupGenerator : Control, IDynamicMarkupGenerator
  {
    private IAppSettings appSettings;
    private LanguageSource languageSource = LanguageSource.Frontend;
    private int itemsInGroupCount = 6;
    private string containerTag = "div";
    private string groupTag = "div";
    private string itemTag = "div";
    private const string BasicGroupClass = "sfBasicLangs";
    private const string MoreGroupClass = "sfMoreLangs sfDisplayNoneImportant";
    private const string BaseContainerClass = "sfTranslationCommands";
    private const string LanguageMarkupFormat = "<{0} class=\"sfLang{1} sfNotTranslated sfLangWrp {2}\" title=\"{3}\">\r\n    <span class=\"sfCulture\">{1}</span>\r\n    <a class=\"sfLangAdd\" href=\"javascript:void(0);\">{4}</a>\r\n    <a class=\"sfLangEdit sfDisplayNone\" href=\"javascript:void(0);\">{5}</a>\r\n</{0}>";
    private const string ToggleTranslationsMarkupFormat = "<a class=\"sf_binderCommand_{0} sfToggleLangListBtn\" href=\"javascript:void(0);\">{1}</a>\r\n<a class=\"sf_binderCommand_{2} sfDisplayNone sfToggleLangListBtn\" href=\"javascript:void(0);\">{3}</a>";
    private const string AppendingFormat = "{0} {1}";

    /// <summary>Gets the default application settings information.</summary>
    protected IAppSettings AppSettings
    {
      get
      {
        if (this.appSettings == null)
          this.appSettings = (IAppSettings) Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings;
        return this.appSettings;
      }
    }

    /// <summary>Gets or sets the number of items in a group.</summary>
    public int ItemsInGroupCount
    {
      get => this.itemsInGroupCount;
      set => this.itemsInGroupCount = value;
    }

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
    public IList<string> AvailableCultures { get; set; }

    /// <summary>Gets or sets the tag of the container element.</summary>
    public string ContainerTag
    {
      get => this.containerTag;
      set => this.containerTag = value;
    }

    /// <summary>Gets or sets the tag of the group element.</summary>
    public string GroupTag
    {
      get => this.groupTag;
      set => this.groupTag = value;
    }

    /// <summary>Gets or sets the tag of the item element.</summary>
    public string ItemTag
    {
      get => this.itemTag;
      set => this.itemTag = value;
    }

    /// <summary>Gets or sets the css class of the container element.</summary>
    public string ContainerClass { get; set; }

    /// <summary>Gets or sets the css class of the group element.</summary>
    public string GroupClass { get; set; }

    /// <summary>Gets or sets the css class of the item element.</summary>
    public string ItemClass { get; set; }

    /// <summary>Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" />
    /// object, which writes the content to be rendered on the client.</summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object
    /// that receives the server control content. </param>
    protected override void Render(HtmlTextWriter writer) => writer.Write(this.GenerateMarkup());

    /// <summary>Generates HTML markup for a dynamic column.</summary>
    /// <returns>The generated HTML markup.</returns>
    public virtual string GetMarkup() => this.GenerateMarkup();

    /// <summary>
    /// Gets a value indicating whether the dynamic column should be rendered.
    /// </summary>
    /// <value><c>true</c> if the column should be rendered; otherwise, <c>false</c>.</value>
    public new virtual bool Visible => this.LanguageSource == LanguageSource.Backend ? ((IEnumerable<CultureInfo>) SystemManager.CurrentContext.AppSettings.DefinedBackendLanguages).Count<CultureInfo>() > 1 : SystemManager.CurrentContext.AppSettings.Multilingual;

    /// <summary>
    /// Initialize properties of the markup generator implementing <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicMarkupGenerator" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public virtual void Configure(IDynamicMarkupGeneratorDefinition definition)
    {
      if (!(definition is ILanguagesColumnMarkupGeneratorDefinition generatorDefinition))
        return;
      this.LanguageSource = generatorDefinition.LanguageSource;
      this.AvailableCultures = generatorDefinition.AvailableLanguages;
      this.ItemsInGroupCount = generatorDefinition.ItemsInGroupCount;
      this.ContainerTag = generatorDefinition.ContainerTag;
      this.GroupTag = generatorDefinition.GroupTag;
      this.ItemTag = generatorDefinition.ItemTag;
      this.ContainerClass = generatorDefinition.ContainerClass;
      this.GroupClass = generatorDefinition.GroupClass;
      this.ItemClass = generatorDefinition.ItemClass;
    }

    private string GenerateMarkup()
    {
      if (!this.Visible)
        return string.Empty;
      List<CultureInfo> languages = this.GetLanguages();
      StringBuilder stringBuilder = new StringBuilder();
      int num1 = this.ItemsInGroupCount;
      if (num1 <= 0)
        num1 = int.MaxValue;
      int count = languages.Count;
      int num2 = (int) Math.Ceiling((double) count / (double) num1);
      int index1 = 0;
      string str1 = "sfBasicLangs";
      string str2 = "sfMoreLangs sfDisplayNoneImportant";
      if (!string.IsNullOrEmpty(this.GroupClass))
      {
        str1 = string.Format("{0} {1}", (object) str1, (object) this.GroupClass);
        str2 = string.Format("{0} {1}", (object) str2, (object) this.GroupClass);
      }
      string str3 = str1;
      string str4 = "sfTranslationCommands";
      if (!string.IsNullOrEmpty(this.ContainerClass))
        str4 = string.Format("{0} {1}", (object) str4, (object) this.ContainerClass);
      stringBuilder.AppendFormat("<{0} class=\"{1}\">", (object) this.ContainerTag, (object) str4);
      int num3;
      for (num3 = 0; num3 < num2; ++num3)
      {
        stringBuilder.AppendFormat("<{0} class=\"{1}\">", (object) this.GroupTag, (object) str3);
        str3 = str2;
        for (int index2 = 0; index2 < num1 && index1 < count; ++index1)
        {
          CultureInfo cultureInfo = languages[index1];
          stringBuilder.AppendFormat("<{0} class=\"sfLang{1} sfNotTranslated sfLangWrp {2}\" title=\"{3}\">\r\n    <span class=\"sfCulture\">{1}</span>\r\n    <a class=\"sfLangAdd\" href=\"javascript:void(0);\">{4}</a>\r\n    <a class=\"sfLangEdit sfDisplayNone\" href=\"javascript:void(0);\">{5}</a>\r\n</{0}>", (object) this.ItemTag, (object) cultureInfo.Name, (object) this.ItemClass, (object) cultureInfo.NativeName, (object) Res.Get<Labels>().Add, (object) Res.Get<Labels>().Edit);
          ++index2;
        }
        stringBuilder.AppendFormat("</{0}>", (object) this.GroupTag);
      }
      stringBuilder.AppendFormat("</{0}>", (object) this.ContainerTag);
      if (num3 > 1)
        stringBuilder.AppendFormat("<a class=\"sf_binderCommand_{0} sfToggleLangListBtn\" href=\"javascript:void(0);\">{1}</a>\r\n<a class=\"sf_binderCommand_{2} sfDisplayNone sfToggleLangListBtn\" href=\"javascript:void(0);\">{3}</a>", (object) "showMoreTranslations", (object) Res.Get<Telerik.Sitefinity.Localization.LocalizationResources>().MoreTranslations, (object) "hideMoreTranslations", (object) Res.Get<Telerik.Sitefinity.Localization.LocalizationResources>().BasicTranslationsOnly);
      return stringBuilder.ToString();
    }

    /// <summary>Gets the languages to be displayed in the markup.</summary>
    protected internal virtual List<CultureInfo> GetLanguages()
    {
      List<CultureInfo> languages = new List<CultureInfo>();
      if (this.LanguageSource == LanguageSource.Custom)
      {
        foreach (string availableCulture in (IEnumerable<string>) this.AvailableCultures)
        {
          CultureInfo cultureInfo = new CultureInfo(availableCulture);
          languages.Add(cultureInfo);
        }
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
  }
}
