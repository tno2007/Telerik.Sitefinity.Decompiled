// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.PublishingSystem.DynamicContentPipeDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.DynamicModules.PublishingSystem
{
  /// <summary>
  /// Designer for the <see cref="T:Telerik.Sitefinity.DynamicModules.PublishingSystem.DynamicContentInboundPipe" />.
  /// </summary>
  public class DynamicContentPipeDesignerView : ControlDesignerBase, IPipeDesigner
  {
    internal const string scriptReference = "Telerik.Sitefinity.DynamicModules.PublishingSystem.Scripts.DynamicContentPipeDesignerView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleBuilder.DynamicContentPipeDesignerView.ascx");
    private bool isDynamicModuleTypeMultilingual;
    private ModuleBuilderManager moduleBuilderManager;
    private DynamicModule dynamicModule;
    private DynamicModuleType dynamicModuleType;
    private string providerName;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = DynamicContentPipeDesignerView.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the pipe that the designer is supposed to design.
    /// </summary>
    public IPipe Pipe { get; set; }

    /// <summary>
    /// Gets the reference to the html control that represents the button for opening the mapping settings.
    /// </summary>
    protected virtual Control OpenMappingSettingsButton => this.Container.GetControl<Control>("openMappingSettingsButton", true);

    /// <summary>Gets the back links page picker control.</summary>
    /// <value>The back links page picker.</value>
    protected PageField BackLinksPagePicker => this.Container.GetControl<PageField>("backLinksPagePicker", true);

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.DynamicModules.PublishingSystem.DynamicContentPipeDesignerView.ModuleBuilderManager" />.
    /// </summary>
    protected virtual ModuleBuilderManager ModuleBuilderManager
    {
      get
      {
        if (this.moduleBuilderManager == null)
          this.moduleBuilderManager = ModuleBuilderManager.GetManager();
        return this.moduleBuilderManager;
      }
    }

    /// <summary>Gets a reference to the filter selector control.</summary>
    protected virtual ContentFilterSelector ContentFilterSelector => this.Container.GetControl<ContentFilterSelector>("contentFilterSelector", true);

    /// <summary>Gets the language selector.</summary>
    /// <value>The language selector.</value>
    protected virtual LanguageChoiceField LanguageSelector => this.Container.GetControl<LanguageChoiceField>("languageChoiceField", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.providerName = PublishingManager.GetProviderNameFromQueryString();
      string typeName = (string) null;
      string typeNamespace = (string) null;
      ModuleNamesGenerator.ResolveDynamicTypeNameFromPipeName(this.Pipe.Name, out typeNamespace, out typeName);
      this.dynamicModuleType = this.ModuleBuilderManager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.TypeName == typeName && t.TypeNamespace == typeNamespace)).Single<DynamicModuleType>();
      this.dynamicModule = this.ModuleBuilderManager.GetDynamicModule(this.dynamicModuleType.ParentModuleId);
      this.ModuleBuilderManager.LoadDynamicModuleGraph(this.dynamicModule);
      this.isDynamicModuleTypeMultilingual = ModuleInstallerHelper.ContainsLocalizableFields((IEnumerable<IDynamicModuleField>) this.dynamicModuleType.Fields) && SystemManager.CurrentContext.AppSettings.Multilingual;
      this.LanguageSelector.Visible = this.isDynamicModuleTypeMultilingual;
      this.BackLinksPagePicker.WebServiceUrl = "~/Sitefinity/Services/Pages/PagesService.svc/";
      this.BackLinksPagePicker.RootNodeID = SiteInitializer.CurrentFrontendRootNodeId;
      this.BackLinksPagePicker.DisplayMode = FieldDisplayMode.Write;
      this.ContentFilterSelector.ItemTypeName = this.dynamicModuleType.GetFullTypeName();
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("_pipeName", (object) this.Pipe.Name);
      controlDescriptor.AddProperty("_contentName", (object) this.dynamicModule.Title);
      controlDescriptor.AddProperty("_allItemsLabel", (object) Res.Get<Labels>().AllItems);
      controlDescriptor.AddProperty("_settingsData", this.CreateContentPipeSettingsCollection());
      controlDescriptor.AddComponentProperty("filterSelector", this.ContentFilterSelector.FilterSelector.ClientID);
      if (this.isDynamicModuleTypeMultilingual)
        controlDescriptor.AddElementProperty("selectLanguage", this.LanguageSelector.ClientID);
      else
        controlDescriptor.AddProperty("_defaultFrontendLanguage", (object) AppSettings.CurrentSettings.DefaultFrontendLanguage.Name);
      controlDescriptor.AddElementProperty("openMappingSettingsButton", this.OpenMappingSettingsButton.ClientID);
      controlDescriptor.AddComponentProperty("backLinksPagePicker", this.BackLinksPagePicker.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
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
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.DynamicModules.PublishingSystem.Scripts.DynamicContentPipeDesignerView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    private object CreateContentPipeSettingsCollection()
    {
      PipeSettings defaultSettings = (PublishingSystemFactory.GetPipe(this.Pipe.Name) ?? throw new InvalidOperationException()).GetDefaultSettings();
      Dictionary<string, object> settingsCollection = new Dictionary<string, object>();
      settingsCollection.Add("settings", (object) defaultSettings);
      WcfPipeSettings wcfPipeSettings = new WcfPipeSettings();
      wcfPipeSettings.InitializeFromModel(defaultSettings, this.providerName);
      settingsCollection.Add("pipe", (object) wcfPipeSettings);
      return (object) settingsCollection;
    }
  }
}
