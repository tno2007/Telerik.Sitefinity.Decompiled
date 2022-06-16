// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterPipeDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

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
  public class TwitterPipeDesignerView : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.TwitterPipeDesignerView.ascx");
    private string providerName;

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected virtual Control OpenMappingSettingsDialog => this.Container.GetControl<Control>("openMappingSettings", true);

    protected virtual GenericCollectionBinder AppsBinder => this.Container.GetControl<GenericCollectionBinder>(nameof (AppsBinder), true);

    protected virtual Control AppsSelector => this.Container.GetControl<Control>("AppsSelect", true);

    protected virtual LanguageChoiceField LanguageChoiceField => this.Container.GetControl<LanguageChoiceField>("languageChoiceField", true);

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
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.TwitterPipeDesignerView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("_shortDescriptionBase", (object) Res.Get<PublishingMessages>().TwitterPipeSettingsShortDescription);
      controlDescriptor.AddProperty("_urlNameNotSet", (object) Res.Get<PublishingMessages>().TwitterPipeSettingsUrlNameNotSet);
      controlDescriptor.AddProperty("_appsSelectorId", (object) this.AppsSelector.ClientID);
      controlDescriptor.AddElementProperty("openMappingSettingsButton", this.OpenMappingSettingsDialog.ClientID);
      controlDescriptor.AddElementProperty("languageChoiceField", this.LanguageChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("appsBinder", this.AppsBinder.ClientID);
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      PipeSettings defaultSettings = PublishingSystemFactory.GetPipe("Twitter").GetDefaultSettings();
      dictionary.Add("settings", (object) defaultSettings);
      dictionary.Add("pipe", (object) new WcfPipeSettings("Twitter", this.providerName));
      controlDescriptor.AddProperty("_settingsData", (object) dictionary);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TwitterPipeDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override void InitializeControls(GenericContainer container) => this.providerName = this.Context.Request.QueryString["provider"];
  }
}
