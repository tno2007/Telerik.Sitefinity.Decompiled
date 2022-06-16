// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.BackendSearchBox
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI
{
  public class BackendSearchBox : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Components.BackendSearchBox.ascx");
    private const string scriptName = "Telerik.Sitefinity.Web.Scripts.BackendSearchBox.js";
    private int minCharacters = -1;
    private int searchDelay = -1;
    private const int defaultMinCharacters = 3;
    private const int defaultSearchDelay = 500;

    /// <summary>
    /// Get/set the javascript callback that will perform a manual search if <see cref="!:EnableManualSearch" /> is set to <c>true</c>
    /// </summary>
    public string OnClientSearch { get; set; }

    /// <summary>
    /// Gets or sets value that determines should search box use a search button
    /// </summary>
    public bool UseSearchButton { get; set; }

    /// <summary>
    /// retpresents the text of the label infront of the search box
    /// </summary>
    public string BinderBoxLabelText { get; set; }

    /// <summary>Text which is shown in the search box by default</summary>
    public string InnerSearchBoxText { get; set; }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? BackendSearchBox.layoutTemplatePath : base.LayoutTemplatePath;
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
    /// Gets the reference of the text box control used for searching
    /// </summary>
    protected virtual TextBox SearchTextBox => this.Container.GetControl<TextBox>("searchBox", true);

    /// <summary>Gets the reference of the search button</summary>
    protected virtual LinkButton SearchButton => this.Container.GetControl<LinkButton>("searchButton", true);

    /// <summary>
    /// Gets the reference to the label control which represents the label / title of the
    /// search box control.
    /// </summary>
    protected virtual Label SearchBoxLabel => this.Container.GetControl<Label>("lblSearchBox", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      this.SearchButton.Visible = this.UseSearchButton;
      if (this.InnerSearchBoxText != null)
        this.SearchTextBox.Text = this.InnerSearchBoxText;
      if (this.BinderBoxLabelText == null)
        return;
      this.SearchBoxLabel.Text = this.BinderBoxLabelText;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      BackendSearchBox backendSearchBox = this;
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(backendSearchBox.GetType().ToString(), backendSearchBox.ClientID);
      behaviorDescriptor.AddElementProperty("textBox", backendSearchBox.SearchTextBox.ClientID);
      behaviorDescriptor.AddProperty("_minCharacters", (object) (backendSearchBox.MinCharacters == -1 ? 3 : backendSearchBox.MinCharacters));
      behaviorDescriptor.AddProperty("_searchDelay", (object) (backendSearchBox.SearchDelay == -1 ? 500 : backendSearchBox.SearchDelay));
      behaviorDescriptor.AddProperty("_searchBoxText", (object) backendSearchBox.InnerSearchBoxText);
      if (backendSearchBox.UseSearchButton)
        behaviorDescriptor.AddProperty("_searchButtonId", (object) backendSearchBox.SearchButton.ClientID);
      if (!backendSearchBox.OnClientSearch.IsNullOrWhitespace())
        behaviorDescriptor.AddEvent("search", backendSearchBox.OnClientSearch);
      yield return (ScriptDescriptor) behaviorDescriptor;
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
          Name = "Telerik.Sitefinity.Web.Scripts.BackendSearchBox.js"
        }
      };
    }
  }
}
