// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// A control that is used to provide a user with the ability to select(build) a filter expression.
  /// </summary>
  [ParseChildren(true)]
  public class FilterSelector : SimpleScriptView
  {
    private List<FilterSelectorItem> items;
    private List<Control> itemButtons;
    private string itemsContainerTag = "ul";
    private string itemTag = "li";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.FilterSelector.ascx");
    private const string controlScript = "Telerik.Sitefinity.Web.Scripts.FilterSelector.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FilterSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether multiple groups of <see cref="!:QueryItem" /> objects can be used.
    /// If <c>true</c> the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem" /> objects are represented as checkboxes otherwise as radio buttons.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if multiple groups of <see cref="!:QueryItem" /> objects can be used; otherwise, <c>false</c>.
    /// </value>
    public bool AllowMultipleSelection { get; set; }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem" /> objects used by this instance.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<FilterSelectorItem> Items
    {
      get
      {
        if (this.items == null)
          this.items = new List<FilterSelectorItem>();
        return this.items;
      }
    }

    /// <summary>
    /// Gets or sets the name of the radio buttons group when <see cref="P:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector.AllowMultipleSelection" /> is <c>false</c>.
    /// </summary>
    /// <value>The name of the radio group.</value>
    public string RadioGroupName { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds
    /// to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets the name of the CSS class for disabled text.
    /// </summary>
    public string DisabledTextCssClass { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector" /> is disabled.
    /// </summary>
    /// <value><c>true</c> if disabled; otherwise, <c>false</c>.</value>
    public bool Disabled { get; set; }

    /// <summary>Gets or sets the tag name of the items container.</summary>
    public string ItemsContainerTag
    {
      get => this.itemsContainerTag;
      set => this.itemsContainerTag = value;
    }

    /// <summary>Gets or sets the CSS class of the items container.</summary>
    public string ItemsContainerCssClass { get; set; }

    /// <summary>Gets or sets the tag name used for an item.</summary>
    public string ItemTag
    {
      get => this.itemTag;
      set => this.itemTag = value;
    }

    /// <summary>Gets or sets the CSS class used for an item.</summary>
    /// <value>The item CSS class.</value>
    public string ItemCssClass { get; set; }

    /// <summary>Gets the control used for an items container.</summary>
    protected virtual Control ItemsContainer => this.Container.GetControl<Control>("itemsContainer", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (FilterSelector).FullName, this.ClientID);
      List<string> itemsIds = new List<string>(this.Items.Count);
      this.Items.ForEach((Action<FilterSelectorItem>) (item => itemsIds.Add(item.ClientID)));
      controlDescriptor.AddProperty("items", (object) itemsIds);
      List<string> itemsButtonIds = new List<string>(this.itemButtons.Count);
      this.itemButtons.ForEach((Action<Control>) (cnt => itemsButtonIds.Add(cnt.ClientID)));
      controlDescriptor.AddProperty("itemButtons", (object) itemsButtonIds);
      controlDescriptor.AddProperty("allowMultipleSelection", (object) this.AllowMultipleSelection);
      controlDescriptor.AddProperty("disabledTextClass", (object) this.DisabledTextCssClass);
      controlDescriptor.AddProperty("disabled", (object) this.Disabled);
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
      string fullName = this.GetType().Assembly.FullName;
      return (IEnumerable<ScriptReference>) new ScriptReference[3]
      {
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.Scripts.FilterSelector.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.QueryBuilder.Scripts.QueryData.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.QueryBuilder.Scripts.QueryDataItem.js"
        }
      };
    }

    /// <summary>Finds an item by the its QueryDataName property.</summary>
    /// <param name="queryDataName">The value of QueryDataName property.</param>
    /// <returns>The first <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem" /> instance with that property if exists; otherwise <c>null</c>.</returns>
    public FilterSelectorItem FindItemByQueryDataName(string queryDataName) => this.Items.Where<FilterSelectorItem>((Func<FilterSelectorItem, bool>) (item => item.QueryDataName == queryDataName)).FirstOrDefault<FilterSelectorItem>();

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container</param>
    protected override void InitializeControls(GenericContainer container) => this.CreateItemControls();

    /// <summary>
    /// Creates item controls from the <see cref="P:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector.Items" /> collection.
    /// The items are checkboxes or radio buttons depending on the <see cref="P:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector.AllowMultipleSelection" /> value.
    /// </summary>
    protected virtual void CreateItemControls()
    {
      string radioGroupName = this.RadioGroupName;
      this.itemButtons = new List<Control>();
      HtmlGenericControl child1 = new HtmlGenericControl(this.itemsContainerTag.ToLowerInvariant());
      child1.Attributes["class"] = this.ItemsContainerCssClass;
      foreach (FilterSelectorItem child2 in this.Items)
      {
        HtmlGenericControl child3 = new HtmlGenericControl(this.itemTag.ToLowerInvariant());
        child3.Attributes["class"] = this.ItemCssClass;
        Control child4;
        if (this.AllowMultipleSelection)
        {
          child4 = (Control) new CheckBox()
          {
            Text = HttpUtility.HtmlEncode(child2.Text),
            Checked = child2.Selected
          };
        }
        else
        {
          RadioButton radioButton = new RadioButton();
          radioButton.Text = child2.Text;
          radioButton.GroupName = radioGroupName;
          radioButton.Checked = child2.Selected;
          child4 = (Control) radioButton;
        }
        this.itemButtons.Add(child4);
        child3.Controls.Add(child4);
        child3.Controls.Add((Control) child2);
        child1.Controls.Add((Control) child3);
      }
      this.ItemsContainer.Controls.Add((Control) child1);
    }
  }
}
