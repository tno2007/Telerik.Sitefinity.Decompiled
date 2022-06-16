// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ThemesEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI
{
  public class ThemesEditor : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.ThemesEditorToolbox.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ThemesEditor" /> class.
    /// </summary>
    public ThemesEditor() => this.LayoutTemplatePath = ThemesEditor.layoutTemplatePath;

    public Guid PageId { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>This is the language in which the control works.</summary>
    public CultureInfo Language { get; set; }

    protected virtual DropDownList ThemeSelector => this.Container.GetControl<DropDownList>("themeSelector", true);

    protected virtual HiddenField DraftIdHidden => this.Container.GetControl<HiddenField>("draftIdHidden", true);

    protected virtual HiddenField IsTemplateHidden => this.Container.GetControl<HiddenField>("isTemplateHidden", true);

    protected virtual HiddenField ServiceBaseUrlHidden => this.Container.GetControl<HiddenField>("serviceBaseUrlHidden", true);

    protected virtual HiddenField LanguageHidden => this.Container.GetControl<HiddenField>("languageHidden", true);

    protected virtual Panel ThemesPanel => this.Container.GetControl<Panel>("themesPanel", true);

    protected virtual Panel NoThemesPanel => this.Container.GetControl<Panel>("noThemesPanel", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      ConfigElementDictionary<string, ThemeElement> elementDictionary;
      if (SystemManager.CurrentHttpContext.Items[(object) "IsTemplate"] != null && (bool) SystemManager.CurrentHttpContext.Items[(object) "IsTemplate"])
      {
        elementDictionary = ThemeController.IsBackendPage() ? Config.Get<AppearanceConfig>().BackendThemes : Config.Get<AppearanceConfig>().FrontendThemes;
        this.IsTemplateHidden.Value = "true";
      }
      else
      {
        elementDictionary = !ThemeController.IsBackendPage() ? Config.Get<AppearanceConfig>().FrontendThemes : Config.Get<AppearanceConfig>().BackendThemes;
        this.IsTemplateHidden.Value = "false";
      }
      this.ThemeSelector.Items.Clear();
      this.ThemeSelector.Items.Add(new ListItem()
      {
        Text = Res.Get<PageResources>().NoTheme,
        Value = "notheme"
      });
      foreach (string key in (IEnumerable<string>) elementDictionary.Keys)
      {
        ListItem listItem = new ListItem();
        listItem.Text = key;
        if (this.Page.Items[(object) "theme"] != null && this.Page.Items[(object) "theme"].ToString() == key)
          listItem.Selected = true;
        this.ThemeSelector.Items.Add(listItem);
      }
      if (this.ThemeSelector.SelectedIndex < 0)
      {
        for (int index = 0; index < elementDictionary.Keys.Count; ++index)
        {
          string text = this.ThemeSelector.Items[index].Text;
          if (ThemeController.IsBackendPage())
          {
            if (text == Config.Get<AppearanceConfig>().BackendTheme)
            {
              this.ThemeSelector.SelectedIndex = index;
              break;
            }
          }
          else if (text == Config.Get<AppearanceConfig>().DefaultFrontendTheme)
          {
            this.ThemeSelector.SelectedIndex = index;
            break;
          }
        }
      }
      this.ServiceBaseUrlHidden.Value = this.Page.ResolveUrl("~/Sitefinity/Services/Pages/ZoneEditorService.svc/Page/Theme/");
      this.DraftIdHidden.Value = this.PageId.ToString();
      this.LanguageHidden.Value = this.Language != null ? this.Language.Name : "";
      if (elementDictionary.Count > 0)
      {
        this.ThemesPanel.Style.Add("display", "");
        this.NoThemesPanel.Style.Add("display", "none");
      }
      else
      {
        this.ThemesPanel.Style.Add("display", "none");
        this.NoThemesPanel.Style.Add("display", "");
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[0];

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[0];

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
