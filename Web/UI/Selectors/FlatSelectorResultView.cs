// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.FlatSelectorResultView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// A <see cref="T:Telerik.Sitefinity.Web.UI.FlatSelectorResultView" /> control.
  /// </summary>
  public class FlatSelectorResultView : SelectorResultView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.FlatSelectorResultView.ascx");
    private const string controlScript = "Telerik.Sitefinity.Web.Scripts.FlatSelectorResultView.js";
    private string itemTemplate = "{{Title}}";
    private bool allowSearching;
    private bool allowMultipleSelection = true;
    private string openSelectorButtonText = Res.Get<Labels>().Change;
    private string providerName;

    public string ServiceUrl { get; set; }

    public string ItemTemplate
    {
      get => this.itemTemplate;
      set => this.itemTemplate = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
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
      get => !string.IsNullOrEmpty(FlatSelectorResultView.layoutTemplatePath) ? FlatSelectorResultView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    public string TitleText { get; set; }

    public string ItemType { get; set; }

    public bool AllowSearching
    {
      get => this.allowSearching;
      set => this.allowSearching = value;
    }

    public bool AllowMultipleSelection
    {
      get => this.allowMultipleSelection;
      set => this.allowMultipleSelection = value;
    }

    public string OpenSelectorButtonText
    {
      get => this.openSelectorButtonText;
      set => this.openSelectorButtonText = value;
    }

    /// <summary>
    /// Gets or sets the name of the provider that selector ought to use
    /// </summary>
    public string ProviderName
    {
      get => this.providerName;
      set
      {
        this.providerName = value;
        this.Selector.ProviderName = value;
      }
    }

    protected virtual FlatSelector Selector => this.Container.GetControl<FlatSelector>("selector", true, TraverseMethod.DepthFirst);

    /// <summary>Gets the choose button control.</summary>
    protected virtual Label ChooseButtonTextControl => this.Container.GetControl<Label>("chooseButtonTextControl", true, TraverseMethod.DepthFirst);

    /// <summary>Gets the title label.</summary>
    public virtual SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("lblSelectorTitle", true, TraverseMethod.DepthFirst);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("selector", this.Selector.ClientID);
      controlDescriptor.AddProperty("_chooseButtonTextControlId", (object) this.ChooseButtonTextControl.ClientID);
      controlDescriptor.AddProperty("_selectLabel", string.IsNullOrEmpty(this.OpenSelectorButtonText) ? (object) Res.Get<Labels>().Select : (object) this.OpenSelectorButtonText);
      controlDescriptor.AddProperty("_changeLabel", (object) Res.Get<Labels>().Change);
      controlDescriptor.AddProperty("_allowMultipleSelection", (object) this.AllowMultipleSelection);
      controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = this.GetType().Assembly.FullName;
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.Scripts.FlatSelectorResultView.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      string absolute = VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash(this.ServiceUrl));
      this.ClientBinder.ServiceUrl = absolute;
      this.ChooseButtonTextControl.Text = this.OpenSelectorButtonText;
      if (!string.IsNullOrEmpty(this.TitleText))
        this.TitleLabel.Text = this.TitleText;
      DataMemberInfo dataMemberInfo = new DataMemberInfo();
      dataMemberInfo.ColumnTemplate = this.ItemTemplate;
      dataMemberInfo.IsExtendedSearchField = true;
      dataMemberInfo.Name = "Title";
      dataMemberInfo.HeaderText = "Title";
      this.Selector.ItemType = this.ItemType;
      this.Selector.DataKeyNames = "Id";
      this.Selector.DataMembers.Add(dataMemberInfo);
      this.Selector.ShowSelectedFilter = false;
      this.Selector.AllowMultipleSelection = true;
      this.Selector.BindOnLoad = this.BindOnLoad;
      this.Selector.ServiceUrl = absolute;
      this.Selector.SearchBoxTitleText = "Narrow by typing";
      this.Selector.AllowSearching = this.AllowSearching;
      this.Selector.AllowMultipleSelection = this.AllowMultipleSelection;
      this.Selector.ProviderName = this.ProviderName;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
