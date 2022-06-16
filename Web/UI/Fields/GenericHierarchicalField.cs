// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.GenericHierarchicalField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Web.UI.Extenders;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  [RequiresDataItem]
  public class GenericHierarchicalField : FieldControl, IExpandableControl
  {
    private bool? expanded = new bool?(true);
    private string selectedNodeDataFieldName = "Path";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.GenericHierarchicalField.ascx");
    private const string script = "Telerik.Sitefinity.Web.UI.Fields.Scripts.GenericHierarchicalField.js";
    private const string reqDataContextScriptFileName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.GenericHierarchicalField" /> class.
    /// </summary>
    public GenericHierarchicalField() => this.LayoutTemplatePath = GenericHierarchicalField.layoutTemplatePath;

    /// <summary>
    /// Gets or sets the value of the property.
    /// The value should be either a Guid or a IList of Guids that represent the taxon pageId's
    /// </summary>
    /// <value>The value.</value>
    public new object Value { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets a value indicating whether to allow root selection.
    /// </summary>
    /// <value><c>true</c> if to allow root selection; otherwise, <c>false</c>.</value>
    public bool AllowRootSelection { get; set; }

    /// <summary>
    /// Gets or sets the ID of the taxon from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    public Guid RootNodeID { get; set; }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    public virtual string Provider { get; set; }

    /// <summary>
    /// Gets or sets the web service URL which will be used to bind the selector.
    /// </summary>
    /// <value>The web service URL.</value>
    public virtual string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the text that will be displayed on the control that can expand the hidden part.
    /// </summary>
    /// <value></value>
    public string ExpandText { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the control is expanded by default. If control is to
    /// be expanded by default true; otherwise false.
    /// </summary>
    /// <value></value>
    public bool? Expanded
    {
      get => this.expanded;
      set => this.expanded = value;
    }

    /// <summary>
    /// Gets or sets the selected node data field. Used to select the property of the data item that will be displayed when a dataitem is selected.
    /// Default values is Path.
    /// </summary>
    public string SelectedNodeDataFieldName
    {
      get => this.selectedNodeDataFieldName;
      set => this.selectedNodeDataFieldName = value;
    }

    /// <summary>
    /// Gets or sets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    /// <value></value>
    public WebControl ExpandControl => (WebControl) this.ExpandButton;

    /// <summary>
    /// Gets or sets the reference to the control that is hidden when Expanded is false and displayed
    /// upon clicking of the ExpandControl.
    /// </summary>
    /// <value></value>
    public WebControl ExpandTarget => this.Container.GetControl<WebControl>("expandTarget", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the label that displays the description of the hierarchical taxon field.
    /// </summary>
    protected internal virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label that displayes the title of the hierarchical taxon field.
    /// </summary>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label that displays the example of the heirarchical taxon field.
    /// </summary>
    protected internal virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the expand button.</summary>
    /// <value>The expand button.</value>
    protected internal virtual LinkButton ExpandButton => this.Container.GetControl<LinkButton>("expandButton", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the link button which is used to change the selected taxa.
    /// </summary>
    protected internal virtual LinkButton ChangeSelectedNodeButton => this.Container.GetControl<LinkButton>("changeSelectedNodeButton", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the web control which is displayed when user has selected a taxa.
    /// </summary>
    protected internal virtual Panel SelectedNodePanel => this.Container.GetControl<Panel>("selectedNodePanel", true);

    /// <summary>
    /// Gets the reference to the web control which is displayed when user has selected a taxa.
    /// </summary>
    protected internal virtual Label SelectedNodeLabel => this.Container.GetControl<Label>("selectedNodeLabel", true);

    /// <summary>
    /// Gets the reference to hierarchical selector used to select hierarchical taxa.
    /// </summary>
    protected internal virtual GenericHierarchicalSelector NodeSelector => this.Container.GetControl<GenericHierarchicalSelector>("nodeSelector", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.TitleLabel.SetTextOrHide(this.Title, this.ResourceClassId);
      this.DescriptionLabel.SetTextOrHide(this.Description, this.ResourceClassId);
      this.ExampleLabel.SetTextOrHide(this.Example, this.ResourceClassId);
      if (this.NodeSelector != null)
      {
        this.NodeSelector.ConfigureSelector(this.WebServiceUrl, this.RootNodeID, this.Provider);
        this.NodeSelector.TabIndex = this.TabIndex;
        this.NodeSelector.RootNodeID = this.RootNodeID;
        this.NodeSelector.AllowRootSelection = true;
      }
      this.SelectedNodePanel.Style.Add("display", "none");
      this.ConfigureExpandableControl(this.ResourceClassId);
      if (!this.Expanded.GetValueOrDefault())
        this.ExpandControl.TabIndex = this.TabIndex;
      this.TabIndex = (short) 0;
    }

    /// <summary>
    /// Checks if the container contains any conditional templates.
    /// </summary>
    /// <param name="container">The container.</param>
    protected internal override void CheckConditionalTemplates(GenericContainer container)
    {
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IGenericHierarchicalFieldDefinition hierarchicalFieldDefinition))
        return;
      this.ResourceClassId = hierarchicalFieldDefinition.ResourceClassId;
      this.WebServiceUrl = hierarchicalFieldDefinition.WebServiceBaseUrl;
      this.RootNodeID = hierarchicalFieldDefinition.RootId;
      this.SelectedNodeDataFieldName = hierarchicalFieldDefinition.SelectedNodeDataFieldName;
      if (hierarchicalFieldDefinition.ExpandableDefinition == null)
        return;
      this.ConfigureExpandableControl(hierarchicalFieldDefinition.ExpandableDefinition);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptorList = new List<ScriptDescriptor>();
      ScriptControlDescriptor descriptorPrivate = this.GetLastScriptDescriptor_Private();
      if (this.ChangeSelectedNodeButton != null)
        descriptorPrivate.AddElementProperty("changeSelectedNodeButton", this.ChangeSelectedNodeButton.ClientID);
      if (this.SelectedNodePanel != null)
        descriptorPrivate.AddElementProperty("selectedNodePanel", this.SelectedNodePanel.ClientID);
      if (this.SelectedNodeLabel != null)
        descriptorPrivate.AddElementProperty("selectedNodeLabel", this.SelectedNodeLabel.ClientID);
      if (this.NodeSelector != null)
        descriptorPrivate.AddComponentProperty("nodeSelector", this.NodeSelector.ClientID);
      descriptorPrivate.AddProperty("_rootNodeId", (object) this.RootNodeID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      PageFacade pageFacade = App.WorkWith().Page(this.RootNodeID);
      WcfPageNode wcfPageNode = new WcfPageNode(pageFacade.Get(), pageFacade.PageManager);
      descriptorPrivate.AddProperty("_rootNode", (object) scriptSerializer.Serialize((object) wcfPageNode));
      descriptorPrivate.AddProperty("selectedNodeDataFieldName", (object) this.SelectedNodeDataFieldName);
      scriptDescriptorList.Add((ScriptDescriptor) this.GetExpandableExtenderDescriptor(this.ClientID));
      scriptDescriptorList.Add((ScriptDescriptor) descriptorPrivate);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptorList.ToArray();
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
      scriptReferences.Add(this.GetExpandableExtenderScript());
      string fullName = typeof (GenericHierarchicalField).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.GenericHierarchicalField.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    protected internal virtual ScriptControlDescriptor GetLastScriptDescriptor_Private() => (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
  }
}
