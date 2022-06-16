// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.DynamicContentSelectorsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  ///  Represents the Content Selectors tab for Dynamic Modules
  /// </summary>
  public class DynamicContentSelectorsDesignerView : ContentSelectorsDesignerView
  {
    internal const string dynamicContentSelectorsDesignerViewScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.DynamicContentSelectorsDesignerView.js";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.DynamicContentSelectorsDesignerView.ascx");
    private string contentSelectorCultureFilter;

    public DynamicContentSelectorsDesignerView() => this.LayoutTemplatePath = DynamicContentSelectorsDesignerView.layoutTemplatePath;

    /// <summary>Gets or sets the content selector culture filter.</summary>
    /// <value>The content selector culture filter.</value>
    public string ContentSelectorCultureFilter
    {
      get => this.contentSelectorCultureFilter;
      set => this.contentSelectorCultureFilter = value;
    }

    /// <summary>Gets or sets the dynamic module type.</summary>
    /// <value>The  dynamic module type.</value>
    public DynamicModuleType ModuleType { get; set; }

    protected override string ScriptDescriptorType => typeof (DynamicContentSelectorsDesignerView).FullName;

    protected override void InitializeContentManager()
    {
      string defaultProviderName = DynamicModuleManager.GetDefaultProviderName(this.ModuleType.ModuleName);
      this.contentManager = (IManager) this.ResolveManagerWithProvider(this.CurrentContentView == null || string.IsNullOrEmpty(this.CurrentContentView.ControlDefinition.ProviderName) ? defaultProviderName : this.CurrentContentView.ControlDefinition.ProviderName);
      if (this.contentManager != null)
        return;
      this.contentManager = (IManager) this.ResolveManagerWithProvider(defaultProviderName);
      this.isControlDefinitionProviderCorrect = false;
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      string str1 = this.GetUICulture() ?? CultureInfo.InvariantCulture.Name;
      this.ContentSelector.UICulture = str1;
      this.ContentSelectorCultureFilter = string.Empty;
      string shortTextFieldName = this.ModuleType.MainShortTextFieldName;
      if (DynamicTypesHelper.IsFieldLocalizable(this.ModuleType.GetFullTypeName(), this.ModuleType.MainShortTextFieldName))
      {
        shortTextFieldName += ".PersistedValue";
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
          this.ContentSelectorCultureFilter = string.Format("Culture == {0}", (object) str1);
      }
      this.ContentSelector.ListModeClientTemplate = "<strong class='sfItemTitle'>" + ("{{" + shortTextFieldName + "}}") + "</strong><span class='sfDate'>{{PublicationDate ? PublicationDate.sitefinityLocaleFormat('dd MMM yyyy') : &quot;&quot;}} by {{Author}}</span>";
      string str2 = this.ContentManager.Provider.Name;
      if (!this.IsControlDefinitionProviderCorrect)
        str2 = this.CurrentContentView.ControlDefinition.ProviderName;
      this.ContentSelector.ProviderName = str2;
      this.ContentSelector.ItemSelectorDataMember = new DataMemberInfo()
      {
        Name = this.ModuleType.MainShortTextFieldName,
        IsExtendedSearchField = true,
        HeaderText = this.ModuleType.MainShortTextFieldName,
        ColumnTemplate = "{{" + this.ModuleType.MainShortTextFieldName + "}}"
      };
      this.ContentFilterSelector.ItemTypeName = this.ModuleType.TypeNamespace + "." + this.ModuleType.TypeName;
      if (this.ProvidersSelector == null)
        return;
      this.ProvidersSelector.Manager = this.ContentManager;
      this.ProvidersSelector.DynamicModuleName = this.ModuleType.ModuleName;
      this.ProvidersSelector.SelectedProviderName = this.CurrentContentView.ControlDefinition.ProviderName;
      this.ProvidersSelector.Providers = (IList<DataProviderBase>) null;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_mainFieldName", (object) this.ModuleType.MainShortTextFieldName);
      controlDescriptor.AddProperty("_buttonChangeText", (object) Res.Get<Labels>().Change);
      controlDescriptor.AddProperty("_buttonSelectText", (object) (this.SingleSelectorButtonText ?? Res.Get<Labels>().Select));
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        controlDescriptor.AddProperty("_contentSelectorCultureFilter", (object) this.ContentSelectorCultureFilter);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.DynamicContentSelectorsDesignerView.js", this.GetType().Assembly.GetName().ToString())
    };

    private DynamicModuleManager ResolveManagerWithProvider(string providerName)
    {
      try
      {
        return DynamicModuleManager.GetManager(providerName);
      }
      catch (ConfigurationErrorsException ex)
      {
        return (DynamicModuleManager) null;
      }
    }
  }
}
