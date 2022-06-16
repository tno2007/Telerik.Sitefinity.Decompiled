// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Components.BinderSearch
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.Components
{
  public class BinderSearch : SimpleScriptView
  {
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Components.BinderSearch.ascx");
    private bool enableManualSearch;
    private bool enableAllLanguagesSearch;
    private LanguageSource languageSource = LanguageSource.Frontend;
    private string binderClientId;
    private bool enableProgressPanel = true;

    /// <summary>
    /// Enable or disable manual search. If Manual search is enabled, the client-side "search" event with the proper
    /// search filter will be raised to let you perform the search yourself.
    /// </summary>
    public bool EnableManualSearch
    {
      get => this.enableManualSearch;
      set => this.enableManualSearch = value;
    }

    /// <summary>
    /// Gets or sets the client pageId of the binder that search box will filter
    /// </summary>
    [Obsolete("Use Binder property to pass the binder control.")]
    public string BinderClientId
    {
      get
      {
        if (!string.IsNullOrEmpty(this.binderClientId))
          return this.binderClientId;
        return this.Binder != null ? this.Binder.ClientID : (string) null;
      }
      set => this.binderClientId = value;
    }

    /// <summary>
    /// Gets or sets the client binder that search box will use
    /// </summary>
    public Control Binder { get; set; }

    /// <summary>
    /// Comma separated list of fields (properties) on which the search ought to be performed
    /// </summary>
    public string SearchFields { get; set; }

    /// <summary>
    /// Comma separated list of fields (properties) of type <see cref="!:Lstring" /> on which the search ought to be performed
    /// </summary>
    public string ExtendedSearchFields { get; set; }

    /// <summary>
    /// Gets or sets the type of search that should be performed
    /// </summary>
    public SearchTypes SearchType { get; set; }

    /// <summary>Gets or sets source for available languages.</summary>
    /// <value>The language source.</value>
    public LanguageSource LanguageSource
    {
      get => this.languageSource;
      set => this.languageSource = value;
    }

    /// <summary>
    /// Gets or sets the value determining can user select the fields that ought to be searched
    /// </summary>
    public bool AllowFieldSelection { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether results should be highlighted
    /// </summary>
    public bool DisableHighlighting { get; set; }

    /// <summary>Gets or sets a value of optional additional filtering</summary>
    public string AdditionalFilterExpression { get; set; }

    /// <summary>
    /// Enable or disable search on all languages if mode is multilingual.
    /// </summary>
    public bool EnableAllLanguagesSearch
    {
      get => this.enableAllLanguagesSearch;
      set => this.enableAllLanguagesSearch = value;
    }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? BinderSearch.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets whether the instance is multilingual. If yes the binder will apply multilingual search filters.
    /// </summary>
    internal bool? Multilingual { get; set; }

    /// <summary>
    /// Gets or sets a value indication whether a progress panel should be enabled.
    /// </summary>
    public bool EnableProgressPanel
    {
      get => this.enableProgressPanel;
      set => this.enableProgressPanel = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
    }

    /// <summary>
    /// Gets the reference of the panel holding the search progress animation or message
    /// </summary>
    protected virtual HtmlGenericControl SearchProgressPanel => this.Container.GetControl<HtmlGenericControl>("searchProgressPanel", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddProperty("_binderId", (object) this.BinderClientId);
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      bool flag = this.Multilingual.HasValue ? this.Multilingual.Value : appSettings.Multilingual;
      behaviorDescriptor.AddProperty("_multilingual", (object) flag);
      if (flag)
      {
        string str = (string) null;
        CultureInfo[] source = (CultureInfo[]) null;
        if (this.LanguageSource == LanguageSource.Frontend)
        {
          str = appSettings.DefaultFrontendLanguage.Name;
          source = appSettings.DefinedFrontendLanguages;
        }
        else if (this.LanguageSource == LanguageSource.Backend)
        {
          str = appSettings.DefaultBackendLanguage.Name;
          source = appSettings.DefinedBackendLanguages;
        }
        behaviorDescriptor.AddProperty("_defaultLanguage", (object) str);
        behaviorDescriptor.AddProperty("_definedLanguages", (object) ((IEnumerable<CultureInfo>) source).Select<CultureInfo, string>((Func<CultureInfo, string>) (ci => ci.Name)));
        behaviorDescriptor.AddProperty("_enableAllLanguagesSearch", (object) this.enableAllLanguagesSearch);
      }
      behaviorDescriptor.AddProperty("_searchFieldsString", (object) this.SearchFields);
      behaviorDescriptor.AddProperty("_extendedSearchFieldsString", (object) this.ExtendedSearchFields);
      behaviorDescriptor.AddProperty("_searchType", (object) Enum.GetName(typeof (SearchTypes), (object) this.SearchType));
      behaviorDescriptor.AddProperty("_disableHighlighthing", (object) this.DisableHighlighting);
      behaviorDescriptor.AddProperty("_additionalFilterExpression", (object) this.AdditionalFilterExpression);
      behaviorDescriptor.AddProperty("_enableManualSearch", (object) this.EnableManualSearch);
      behaviorDescriptor.AddProperty("_enableProgressPanel", (object) this.EnableProgressPanel);
      behaviorDescriptor.AddElementProperty("searchProgressPanel", this.SearchProgressPanel.ClientID);
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
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      BinderSearch binderSearch = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = new ScriptReference()
      {
        Assembly = binderSearch.GetType().Assembly.GetName().ToString(),
        Name = "Telerik.Sitefinity.Web.UI.Components.Scripts.BinderSearch.js"
      };
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }
}
