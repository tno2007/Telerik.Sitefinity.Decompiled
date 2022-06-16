// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// A control that is used in conjunction with <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector" /> control to build a part of a filter expression.
  /// </summary>
  [ParseChildren(true)]
  public class FilterSelectorItem : SimpleScriptView
  {
    private GenericContainer selectorPreviewContainer;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.FilterSelectorItem.ascx");
    private const string controlScript = "Telerik.Sitefinity.Web.Scripts.FilterSelectorItem.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FilterSelectorItem.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the text displayed in the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector" /> control for the item represented by this instance.
    /// </summary>
    /// <value>The displayed text.</value>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the logical operator which is used to group the <see cref="T:Telerik.Sitefinity.Web.Model.QueryItem" /> objects.
    /// </summary>
    /// <value>The logical operator for items.</value>
    public string ItemLogicalOperator { get; set; }

    /// <summary>
    /// Gets or sets the logical operator which is used to group the groups of <see cref="T:Telerik.Sitefinity.Web.Model.QueryItem" /> objects.
    /// </summary>
    /// <value>The logical operator for groups.</value>
    public string GroupLogicalOperator { get; set; }

    /// <summary>
    /// Gets or sets the name of the group of <see cref="T:Telerik.Sitefinity.Web.Model.QueryItem" /> objects represented by this instance.
    /// </summary>
    /// <value>The name of the group.</value>
    public string QueryDataName { get; set; }

    /// <summary>
    /// Gets or sets the name of the data member used in the <see cref="T:Telerik.Sitefinity.Web.Model.QueryItem" /> objects.
    /// The name is used later in the linq expressions.
    /// </summary>
    /// <value>The name of the member.</value>
    public string QueryFieldName { get; set; }

    /// <summary>
    /// Gets or sets the type of the data member used in the <see cref="T:Telerik.Sitefinity.Web.Model.QueryItem" /> objects.
    /// </summary>
    /// <value>The type of the field.</value>
    public string QueryFieldType { get; set; }

    /// <summary>
    /// Gets or sets the operator used in the Condition object of the <see cref="T:Telerik.Sitefinity.Web.Model.QueryItem" /> objects.
    /// </summary>
    /// <value>The operator of the condition.</value>
    public string ConditionOperator { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem" /> instance is selected.
    /// </summary>
    /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
    public bool Selected { get; set; }

    /// <summary>
    /// Gets or sets a delegate used to translate a single <see cref="T:Telerik.Sitefinity.Web.Model.QueryItem" /> object to a value used by the selector result view.
    /// If specified the delegate is called for every <see cref="T:Telerik.Sitefinity.Web.Model.QueryItem" /> object unless the <see cref="P:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem.CollectionTranslatorDelegate" /> property is not null or empty.
    /// The delegate is a javascript function name or a javascript expression that returns function.
    /// </summary>
    /// <value>The item translator delegate.</value>
    public string ItemTranslatorDelegate { get; set; }

    /// <summary>
    /// Gets or sets a delegate used to translate all <see cref="T:Telerik.Sitefinity.Web.Model.QueryItem" /> objects to an array of values used by the selector result view.
    /// If a delegate is set then the <see cref="P:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem.ItemTranslatorDelegate" /> property is ignored unless called explicitly.
    /// The delegate is a javascript function name or a javascript expression that returns function.
    /// </summary>
    /// <value>The collection translator delegate.</value>
    public string CollectionTranslatorDelegate { get; set; }

    /// <summary>
    /// Gets or sets a delegate used to construct a <see cref="T:Telerik.Sitefinity.Web.Model.QueryItem" /> object from a value returned by the selector result view.
    /// If specified the delegate is called for every value returned by the selector result view unless the <see cref="P:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem.CollectionBuilderDelegate" /> property is not null or empty.
    /// The delegate is a javascript function name or a javascript expression that returns function.
    /// </summary>
    /// <value>The item builder delegate.</value>
    public string ItemBuilderDelegate { get; set; }

    /// <summary>
    /// Gets or sets a delegate used to construct a set of <see cref="T:Telerik.Sitefinity.Web.Model.QueryItem" /> objects from the values returned by the selector result view.
    /// If a delegate is set then the <see cref="P:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem.ItemBuilderDelegate" /> property is ignored unless called explicitly.
    /// The delegate is a javascript function name or a javascript expression that returns function.
    /// </summary>
    /// <value>The collection builder delegate.</value>
    public string CollectionBuilderDelegate { get; set; }

    /// <summary>
    /// Gets or sets a template that is used to define a <see cref="T:Telerik.Sitefinity.Web.UI.SelectorResultView" /> instance.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public ITemplate SelectorResultView { get; set; }

    /// <summary>
    /// Gets the container of the controls defined by the <see cref="P:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem.SelectorResultView" /> template.
    /// </summary>
    protected virtual GenericContainer SelectorPreviewContainer
    {
      get
      {
        if (this.selectorPreviewContainer == null && this.SelectorResultView != null)
          this.selectorPreviewContainer = this.CreateContainer(this.SelectorResultView);
        return this.selectorPreviewContainer;
      }
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds
    /// to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Query items that are fixed for the item and will be returned alwyas
    /// Good for using with radio buttons that set specific set of filters
    /// </summary>
    public List<QueryItem> StaticQueryItems { get; set; }

    /// <summary>
    /// Gets the selector result view defined by the <see cref="P:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem.SelectorResultView" /> template.
    /// </summary>
    public virtual Telerik.Sitefinity.Web.UI.SelectorResultView ActualSelectorResultView => this.SelectorPreviewContainer != null ? this.SelectorPreviewContainer.GetControl<Telerik.Sitefinity.Web.UI.SelectorResultView>() : (Telerik.Sitefinity.Web.UI.SelectorResultView) null;

    /// <summary>
    /// Gets the container that holds the <see cref="T:Telerik.Sitefinity.Web.UI.SelectorResultView" /> instance.
    /// </summary>
    protected virtual Control SelectorResultViewContainer => this.Container.GetControl<Control>("selectorResultViewContainer", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (FilterSelectorItem).FullName, this.ClientID);
      controlDescriptor.AddProperty("itemLogicalOperator", (object) this.ItemLogicalOperator);
      controlDescriptor.AddProperty("groupLogicalOperator", (object) this.GroupLogicalOperator);
      controlDescriptor.AddProperty("queryDataName", (object) this.QueryDataName);
      controlDescriptor.AddProperty("queryFieldName", (object) this.QueryFieldName);
      controlDescriptor.AddProperty("queryFieldType", (object) this.QueryFieldType);
      controlDescriptor.AddProperty("conditionOperator", (object) this.ConditionOperator);
      controlDescriptor.AddProperty("selected", (object) this.Selected);
      if (!string.IsNullOrEmpty(this.ItemTranslatorDelegate))
        controlDescriptor.AddScriptProperty("itemTranslatorDelegate", this.ItemTranslatorDelegate);
      if (!string.IsNullOrEmpty(this.CollectionTranslatorDelegate))
        controlDescriptor.AddScriptProperty("collectionTranslatorDelegate", this.CollectionTranslatorDelegate);
      if (!string.IsNullOrEmpty(this.ItemBuilderDelegate))
        controlDescriptor.AddScriptProperty("itemBuilderDelegate", this.ItemBuilderDelegate);
      if (!string.IsNullOrEmpty(this.CollectionBuilderDelegate))
        controlDescriptor.AddScriptProperty("collectionBuilderDelegate", this.CollectionBuilderDelegate);
      controlDescriptor.AddProperty("container", (object) this.SelectorResultViewContainer.ClientID);
      if (this.StaticQueryItems != null && this.StaticQueryItems.Count > 0)
        controlDescriptor.AddProperty("staticQueryItems", (object) this.StaticQueryItems);
      if (this.ActualSelectorResultView != null)
        controlDescriptor.AddComponentProperty("selectorResultView", this.ActualSelectorResultView.ClientID);
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
      return (IEnumerable<ScriptReference>) new ScriptReference[1]
      {
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.Scripts.FilterSelectorItem.js"
        }
      };
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.SelectorPreviewContainer == null)
        return;
      this.SelectorResultViewContainer.Controls.Add((Control) this.SelectorPreviewContainer);
    }
  }
}
