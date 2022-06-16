// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Designers.RssAtomPipeDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Publishing.Web.UI.Designers
{
  public class RssAtomPipeDesignerView : ControlDesignerBase
  {
    /// <summary>The name of the layout template.</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.RssAtomPipeDesignerView.ascx");
    /// <summary>The minimum value for the MaxItems setting</summary>
    protected const int minMaxItemsCount = 1;
    /// <summary>The minimum value for the ContentSize setting</summary>
    protected const int minContentSize = 1;
    private string providerName;

    public TextField MaxItemsCount => this.Container.GetControl<TextField>("maxItemsCount", true);

    public TextField ContentSize => this.Container.GetControl<TextField>("contentSize", true);

    public ITextControl UINameLabel => this.Container.GetControl<ITextControl>("uiNameLabel", true);

    public FieldControl UrlName => this.Container.GetControl<FieldControl>("urlName", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the language selector.</summary>
    /// <value>The language selector.</value>
    protected virtual LanguageChoiceField LanguageSelector => this.Container.GetControl<LanguageChoiceField>("languageChoiceField", true);

    protected virtual Control OpenMappingSettingsDialog => this.Container.GetControl<Control>("openMappingSettings", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      foreach (FieldControl fieldControl in this.Container.GetControls<FieldControl>().Values)
      {
        if (!string.IsNullOrEmpty(fieldControl.DataFieldName))
          dictionary1.Add(fieldControl.DataFieldName, fieldControl.ClientID);
      }
      controlDescriptor.AddProperty("dataFieldNameControlIdMap", (object) dictionary1);
      controlDescriptor.AddElementProperty("uiNameLabel", ((Control) this.UINameLabel).ClientID);
      controlDescriptor.AddProperty("_shortDescriptionBase", (object) Res.Get<PublishingMessages>().PipeSettingsShortDescriptionBase);
      controlDescriptor.AddProperty("_urlNameNotSet", (object) Res.Get<PublishingMessages>().PipeSettingsUrlNameNotSet);
      controlDescriptor.AddProperty("_feedsBaseUrl", (object) PublishingManager.GetFeedsBaseURl());
      controlDescriptor.AddElementProperty("openMappingSettingsButton", this.OpenMappingSettingsDialog.ClientID);
      controlDescriptor.AddElementProperty("selectLanguage", this.LanguageSelector.ClientID);
      Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
      PipeSettings defaultSettings = PublishingSystemFactory.GetPipe("RSSOutboundPipe").GetDefaultSettings();
      dictionary2.Add("settings", (object) defaultSettings);
      dictionary2.Add("pipe", (object) new WcfPipeSettings("RSSOutboundPipe", this.providerName));
      controlDescriptor.AddProperty("_settingsData", (object) dictionary2);
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
      ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.RssAtomPipeDesignerView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? RssAtomPipeDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    protected override void InitializeControls(GenericContainer container)
    {
      this.providerName = PublishingManager.GetProviderNameFromQueryString();
      this.UrlName.Description = string.Format(Res.Get<PublishingMessages>().FeedsBaseUrlPattern, (object) PublishingManager.GetFeedsBaseURl());
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.MaxItemsCount.ValidatorDefinition.MinValue = (object) 1;
      this.ContentSize.ValidatorDefinition.MinValue = (object) 1;
    }
  }
}
