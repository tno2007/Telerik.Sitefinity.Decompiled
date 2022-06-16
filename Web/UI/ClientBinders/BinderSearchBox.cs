// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.BinderSearchBox
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Components;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents a generic search box component that is used for filtering (searching) binders.
  /// </summary>
  public class BinderSearchBox : SimpleScriptView
  {
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ClientBinders.BinderSearchBox.ascx");
    private int minCharacters = -1;
    private int searchDelay = -1;
    private bool enableManualSearch;
    private string binderClientId;
    private Control binder;

    /// <summary>
    /// Enable or disable manual search. If Manual ssearch is enabled, the client-side "search" event with the proper
    /// search filter will be raised to let you perform the search yourself.
    /// </summary>
    public bool EnableManualSearch
    {
      get => this.enableManualSearch;
      set => this.enableManualSearch = value;
    }

    /// <summary>
    /// Get/set the javascript callback that will perform a manual search if <see cref="P:Telerik.Sitefinity.Web.UI.BinderSearchBox.EnableManualSearch" /> is set to <c>true</c>
    /// </summary>
    public string OnClientSearch { get; set; }

    /// <summary>
    /// Gets or sets the client pageId of the binder that search box will filter
    /// </summary>
    [Obsolete("Use Binder property to pass the binder control.")]
    public string BinderClientId { get; set; }

    /// <summary>
    /// Gets or sets the client binder that search box will use
    /// </summary>
    public Control Binder
    {
      get => this.binder;
      set
      {
        this.binder = value;
        if (this.BinderSearch == null)
          return;
        this.BinderSearch.Binder = this.Binder;
      }
    }

    /// <summary>
    /// Comma separated list of fields (properties) on which the search ought to be performed
    /// </summary>
    public string SearchFields { get; set; }

    /// <summary>
    /// Comma separated list of fields (properties) of type <see cref="!:Lstring" /> on which the search ought to be performed
    /// </summary>
    public string ExtendedSearchFields { get; set; }

    /// <summary>
    /// Gets or sets value that determines should search box use a search button
    /// </summary>
    public bool UseSearchButton { get; set; }

    /// <summary>
    /// Gets or sets the type of search that should be performed
    /// </summary>
    public SearchTypes SearchType { get; set; }

    /// <summary>
    /// retpresents the text of the label infront of the search box
    /// </summary>
    public string BinderBoxLabelText { get; set; }

    /// <summary>Text which is shown in the search box by default</summary>
    public string InnerSearchBoxText { get; set; }

    /// <summary>
    /// Gets or sets the value determining can user select the fields that ought to be searched
    /// </summary>
    public bool AllowFieldSelection { get; set; }

    /// <summary>
    /// Gets or sets the number of characters after which search is performed
    /// </summary>
    public int MinCharacters
    {
      get => this.minCharacters;
      set => this.minCharacters = value;
    }

    /// <summary>
    /// Gets or sets the delay in milliseconds after which the search is started
    /// </summary>
    public int SearchDelay
    {
      get => this.searchDelay;
      set => this.searchDelay = value;
    }

    /// <summary>
    /// Gets or sets the value determining whether results should be highlighted
    /// </summary>
    public bool DisableHighlighting { get; set; }

    /// <summary>Gets or sets a value of optional additional filtering</summary>
    public string AdditionalFilterExpression { get; set; }

    /// <summary>
    /// Gets or sets a value determining whether search should be performed on all languages.
    /// </summary>
    public bool EnableAllLanguagesSearch { get; set; }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? BinderSearchBox.layoutTemplateName : base.LayoutTemplatePath;
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

    protected virtual BackendSearchBox SearchBox => this.Container.GetControl<BackendSearchBox>("searchBox", true);

    protected virtual BinderSearch BinderSearch => this.Container.GetControl<BinderSearch>("binderSearch", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddComponentProperty("searchBox", this.SearchBox.ClientID);
      behaviorDescriptor.AddComponentProperty("binderSearch", this.BinderSearch.ClientID);
      behaviorDescriptor.AddProperty("_enableManualSearch", (object) this.EnableManualSearch);
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
      string str = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new ScriptReference[1]
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.BinderSearchBox.js"
        }
      };
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      this.ConfigureSearchBox();
      this.ConfigureFilterBuilder();
    }

    private void ConfigureFilterBuilder()
    {
      this.BinderSearch.Binder = this.Binder;
      this.BinderSearch.BinderClientId = this.BinderClientId;
      this.BinderSearch.SearchFields = this.SearchFields;
      this.BinderSearch.ExtendedSearchFields = this.ExtendedSearchFields;
      this.BinderSearch.SearchType = this.SearchType;
      this.BinderSearch.AllowFieldSelection = this.AllowFieldSelection;
      this.BinderSearch.DisableHighlighting = this.DisableHighlighting;
      this.BinderSearch.AdditionalFilterExpression = this.AdditionalFilterExpression;
      this.BinderSearch.EnableAllLanguagesSearch = this.EnableAllLanguagesSearch;
    }

    private void ConfigureSearchBox()
    {
      this.SearchBox.InnerSearchBoxText = this.InnerSearchBoxText;
      this.SearchBox.BinderBoxLabelText = this.BinderBoxLabelText;
      this.SearchBox.OnClientSearch = this.OnClientSearch;
      this.SearchBox.UseSearchButton = this.UseSearchButton;
      this.SearchBox.BinderBoxLabelText = this.BinderBoxLabelText;
      this.SearchBox.InnerSearchBoxText = this.InnerSearchBoxText;
      this.SearchBox.MinCharacters = this.MinCharacters;
      this.SearchBox.SearchDelay = this.SearchDelay;
    }
  }
}
