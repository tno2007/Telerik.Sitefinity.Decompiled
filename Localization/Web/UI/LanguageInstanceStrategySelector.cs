// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.LanguageInstanceStrategySelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>
  /// Represents a selection of localization strategy for new language version.
  /// </summary>
  public class LanguageInstanceStrategySelector : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Localization.LanguageInstanceStrategySelector.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LanguageInstanceStrategySelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    public DraftProxyBase Proxy { get; set; }

    /// <summary>Gets the control wrapper.</summary>
    /// <value>The control wrapper.</value>
    public HtmlGenericControl ControlWrapper => this.Container.GetControl<HtmlGenericControl>("languageInstanceStrategyWrapper", false);

    public HtmlControl CopyFromAnotherLanguageButton => this.Container.GetControl<HtmlControl>("copyAnotherButton", true);

    public HtmlAnchor StartFromScratchButton => this.Container.GetControl<HtmlAnchor>("startFromScratchButton", true);

    public PromptDialog ChooseSourceDialog => this.Container.GetControl<PromptDialog>("promptDialog", true);

    public ChoiceField SourceLanguageCombo => this.Container.GetControl<ChoiceField>("sourceLanguageCombo", true, TraverseMethod.DepthFirst);

    public ChoiceField SyncedCheckbox => this.Container.GetControl<ChoiceField>("syncedCheckbox", true, TraverseMethod.DepthFirst);

    public HtmlGenericControl LoadingImage => this.Container.GetControl<HtmlGenericControl>("loadingView", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The dialog container.</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      List<CultureInfo> usedLanguages = new List<CultureInfo>((IEnumerable<CultureInfo>) this.Proxy.UsedLanguages);
      ControlCollection controls = this.ChooseSourceDialog.Controls;
      ChoiceField sourceLanguageCombo = this.SourceLanguageCombo;
      PageDraftProxy proxy = this.Proxy as PageDraftProxy;
      if (proxy.Page.NavigationNode != null && proxy.Page.NavigationNode.IsSplitPage)
      {
        this.RenderNoSyncMode(sourceLanguageCombo, usedLanguages, proxy);
      }
      else
      {
        ChoiceItem choiceItem = new ChoiceItem();
        CultureInfo cultureInfo = proxy.Page.Culture == null ? CultureInfo.InvariantCulture : new CultureInfo(proxy.Page.Culture);
        choiceItem.Text = cultureInfo.NativeName;
        choiceItem.Value = cultureInfo.Name;
        choiceItem.Enabled = false;
        sourceLanguageCombo.Choices.Add(choiceItem);
        sourceLanguageCombo.Enabled = false;
      }
    }

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
      behaviorDescriptor.AddElementProperty("loading", this.LoadingImage.ClientID);
      behaviorDescriptor.AddElementProperty("wrapper", this.ControlWrapper.ClientID);
      behaviorDescriptor.AddElementProperty("copyFromOtherElement", this.CopyFromAnotherLanguageButton.ClientID);
      behaviorDescriptor.AddElementProperty("startFromScratchElement", this.StartFromScratchButton.ClientID);
      behaviorDescriptor.AddComponentProperty("chooseSourceDialog", this.ChooseSourceDialog.ClientID);
      behaviorDescriptor.AddComponentProperty("sourceLanguageCombo", this.SourceLanguageCombo.ClientID);
      behaviorDescriptor.AddComponentProperty("syncedCheckbox", this.SyncedCheckbox.ClientID);
      behaviorDescriptor.AddProperty("pageNodeId", (object) this.Proxy.PageNodeId);
      behaviorDescriptor.AddProperty("isInSplitMode", (object) ((PageDraftProxy) this.Proxy).Page.NavigationNode.IsSplitPage);
      behaviorDescriptor.AddProperty("currentLanguage", (object) this.Proxy.CurrentObjectCulture.Name);
      behaviorDescriptor.AddProperty("serviceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Pages/ZoneEditorService.svc/"));
      behaviorDescriptor.AddProperty("baseEditUrl", (object) DraftProxyBase.GetPageEditUrl(this.Proxy.PageUrl));
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
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      });
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Localization.Web.UI.Scripts.LanguageInstanceStrategySelector.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    private void RenderNoSyncMode(
      ChoiceField combo,
      List<CultureInfo> usedLanguages,
      PageDraftProxy proxy)
    {
      combo.Choices.Add(new ChoiceItem()
      {
        Text = Res.Get<Labels>().NoneStartFromScratch,
        Value = "none"
      });
      foreach (CultureInfo usedLanguage in usedLanguages)
      {
        if (!usedLanguage.Equals((object) proxy.CurrentObjectCulture))
          combo.Choices.Add(new ChoiceItem()
          {
            Text = usedLanguage.NativeName,
            Value = usedLanguage.Name
          });
      }
      this.SyncedCheckbox.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
    }
  }
}
