// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.SectionControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Extenders;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Extenders.Definitions;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail
{
  /// <summary>Represents the sections in edit/insert forms.</summary>
  public class SectionControl : SimpleScriptView, IExpandableControl
  {
    private string title;
    private string cssClass;
    private HtmlTextWriterTag wrapperTag;
    private List<Control> fieldControls;
    private List<Control> requireDataItemControls;
    private List<Control> bulkEditFieldControls;
    private List<Control> compositeFieldControls;
    private List<Control> commandFieldControls;
    private Collection<IFieldDefinition> fields;
    private bool? expanded = new bool?(false);
    private FieldDisplayMode? _displayMode;
    private ExpandableControlDefinition expandableControlDefinition;
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentUI.SectionControl.ascx");
    private const string sectionScript = "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.SectionControl.js";

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => this.WrapperTag;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SectionControl.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the section definition.</summary>
    public IContentViewSectionDefinition SectionDefinition { get; set; }

    /// <summary>Gets a collection of field controls.</summary>
    public List<Control> FieldControls
    {
      get
      {
        if (this.fieldControls == null)
          this.fieldControls = new List<Control>();
        return this.fieldControls;
      }
    }

    /// <summary>
    /// All the field controls that require access to the dataItem
    /// when binding on the client side
    /// </summary>
    public List<Control> RequireDataItemControls
    {
      get
      {
        if (this.requireDataItemControls == null)
          this.requireDataItemControls = new List<Control>();
        return this.requireDataItemControls;
      }
    }

    /// <summary>Gets a collection of all bulk edit field controls.</summary>
    public List<Control> BulkEditFieldControls
    {
      get
      {
        if (this.bulkEditFieldControls == null)
          this.bulkEditFieldControls = new List<Control>();
        return this.bulkEditFieldControls;
      }
    }

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ICommandField" /> controls for this section.
    /// </summary>
    public List<Control> CommandFieldControls
    {
      get
      {
        if (this.commandFieldControls == null)
          this.commandFieldControls = new List<Control>();
        return this.commandFieldControls;
      }
    }

    /// <summary>Gets a collection of all composite field controls.</summary>
    public List<Control> CompositeFieldControls
    {
      get
      {
        if (this.compositeFieldControls == null)
          this.compositeFieldControls = new List<Control>();
        return this.compositeFieldControls;
      }
    }

    /// <summary>Gets the fields that belong to this section.</summary>
    /// <value>The fields.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public ICollection<IFieldDefinition> Fields
    {
      get
      {
        if (this.fields == null)
          this.fields = this.SectionDefinition == null ? new Collection<IFieldDefinition>() : new Collection<IFieldDefinition>((IList<IFieldDefinition>) this.SectionDefinition.Fields.ToList<IFieldDefinition>());
        return (ICollection<IFieldDefinition>) this.fields;
      }
    }

    /// <summary>Gets or sets the expandable control definition.</summary>
    /// <value>The expandable control definition.</value>
    public ExpandableControlDefinition ExpandableControlDefinition
    {
      get
      {
        if (this.expandableControlDefinition == null)
          this.expandableControlDefinition = new ExpandableControlDefinition()
          {
            Expanded = this.Expanded,
            ExpandText = this.ExpandText
          };
        return this.expandableControlDefinition;
      }
      set => this.expandableControlDefinition = value;
    }

    /// <summary>Gets or sets the title of the section.</summary>
    /// <value>The title.</value>
    public string Title
    {
      get
      {
        if (string.IsNullOrEmpty(this.title) && this.SectionDefinition != null)
          this.title = Res.ResolveLocalizedValue(this.SectionDefinition.ResourceClassId, this.SectionDefinition.Title);
        return this.title;
      }
      set => this.title = value;
    }

    /// <summary>
    /// Gets or sets the Cascading Style Sheet (CSS) class rendered by the Web server control on the client.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.
    /// </returns>
    public override string CssClass
    {
      get
      {
        if (string.IsNullOrEmpty(this.cssClass) && this.SectionDefinition != null)
          this.cssClass = this.SectionDefinition.CssClass;
        return this.cssClass;
      }
      set => this.cssClass = value;
    }

    /// <summary>
    /// Gets or sets the CSS class that is being added to each of the field controls in the section.
    /// </summary>
    /// <value>A CSS class.</value>
    public string FieldControlsCssClass { get; set; }

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    public HtmlTextWriterTag WrapperTag
    {
      get
      {
        if (this.wrapperTag == HtmlTextWriterTag.Unknown && this.SectionDefinition != null)
          this.wrapperTag = this.SectionDefinition.WrapperTag;
        return this.wrapperTag;
      }
      set => this.wrapperTag = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.SectionControl.DisplayMode" />.
    /// </summary>
    /// <value>The display mode.</value>
    public FieldDisplayMode? DisplayMode
    {
      get
      {
        if (!this._displayMode.HasValue && this.SectionDefinition != null && this.SectionDefinition.DisplayMode.HasValue)
          this._displayMode = this.SectionDefinition.DisplayMode;
        return this._displayMode;
      }
      set => this._displayMode = value;
    }

    /// <summary>
    /// Gets or sets whether to override the contained field controls' ids.
    /// </summary>
    public bool OverrideFieldControlsDisplayMode { get; set; }

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
    /// Gets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    /// <value></value>
    public WebControl ExpandControl => (WebControl) this.SectionTitleLabel;

    /// <summary>
    /// Gets the reference to the control that is hidden when Expanded is false and displayed
    /// upon clicking of the ExpandControl.
    /// </summary>
    /// <value></value>
    public WebControl ExpandTarget => this.Container.GetControl<WebControl>("expandableTarget", true);

    /// <summary>
    /// Gets the reference to the control that wraps other controls of the SectionControl.
    /// </summary>
    protected internal virtual WebControl Wrapper => this.Container.GetControl<WebControl>("wrapper", true);

    /// <summary>
    /// Gets the reference to the control that wraps the section title.
    /// </summary>
    protected internal virtual HtmlControl SectionTitle => this.Container.GetControl<HtmlControl>("sectionTitle", true);

    /// <summary>
    /// Gets the reference to the literal that displays the title of the section.
    /// </summary>
    protected internal virtual LinkButton SectionTitleLabel => this.Container.GetControl<LinkButton>("sectionTitleLabel", false);

    /// <summary>Gets the reference to the fields repeater.</summary>
    protected internal virtual Repeater FieldsRepeater => this.Container.GetControl<Repeater>("fields", true);

    /// <summary>Recreates this instance.</summary>
    public void Rebuild() => this.ChildControlsCreated = false;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.fieldControls = (List<Control>) null;
      this.Controls.Clear();
      this.FieldsRepeater.DataSource = (object) this.Fields;
      this.FieldsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.FieldsRepeater_ItemDataBound);
      if (!(this.Parent is IDataItemContainer))
        this.FieldsRepeater.DataBind();
      if (this.SectionDefinition != null)
        this.ExpandableControlDefinition = new ExpandableControlDefinition()
        {
          ControlDefinitionName = this.SectionDefinition.ControlDefinitionName,
          ViewName = this.SectionDefinition.ViewName,
          SectionName = this.SectionDefinition.Name
        };
      else
        this.ExpandableControlDefinition = new ExpandableControlDefinition()
        {
          Expanded = this.Expanded,
          ExpandText = this.ExpandText
        };
      this.ConfigureExpandableControl((IExpandableControlDefinition) this.ExpandableControlDefinition);
      if (!this.ExpandableControlDefinition.Expanded.HasValue || this.ExpandableControlDefinition.Expanded.Value)
      {
        this.Wrapper.CssClass = string.Format("{0} {1}", (object) this.Wrapper.CssClass, (object) "sfExpandedForm");
        this.ExpandTarget.CssClass = string.Format("{0} {1}", (object) this.ExpandTarget.CssClass, (object) "sfExpandedTarget");
      }
      if (string.IsNullOrEmpty(this.Title))
      {
        this.SectionTitle.Visible = false;
      }
      else
      {
        this.SectionTitle.Visible = true;
        if (string.IsNullOrEmpty(this.Title))
          this.SectionTitleLabel.Visible = false;
        else
          this.SectionTitleLabel.Text = this.Title;
      }
      this.Wrapper.CssClass = string.Format("{0} {1}", (object) this.Wrapper.CssClass, (object) this.CssClass);
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptorList = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (SectionControl).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("wrapperElement", this.Wrapper.ClientID);
      List<string> list = this.FieldControls.Select<Control, string>((Func<Control, string>) (n => n.ClientID)).ToList<string>();
      if (list.Count > 0)
        controlDescriptor.AddProperty("fieldControlIds", (object) list);
      controlDescriptor.AddProperty("name", (object) this.ExpandableControlDefinition.SectionName);
      scriptDescriptorList.Add((ScriptDescriptor) controlDescriptor);
      scriptDescriptorList.Add((ScriptDescriptor) this.GetExpandableExtenderDescriptor(this.ClientID));
      return (IEnumerable<ScriptDescriptor>) scriptDescriptorList.ToArray();
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.SectionControl.js", typeof (SectionControl).Assembly.FullName),
      this.GetExpandableExtenderScript()
    };

    /// <summary>
    /// Handles the ItemDataBound event of the FieldsRepeater control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    private void FieldsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.DataItem is IFieldDefinition dataItem))
        return;
      if (this.DisplayMode.HasValue && dataItem is FieldControlDefinition)
      {
        FieldControlDefinition controlDefinition1 = dataItem as FieldControlDefinition;
        FieldDisplayMode? displayMode = controlDefinition1.DisplayMode;
        if (!displayMode.HasValue || this.OverrideFieldControlsDisplayMode)
        {
          FieldControlDefinition controlDefinition2 = controlDefinition1;
          displayMode = this.DisplayMode;
          FieldDisplayMode? nullable = new FieldDisplayMode?(displayMode.Value);
          controlDefinition2.DisplayMode = nullable;
        }
      }
      IField configuredFieldControl = this.GetConfiguredFieldControl(dataItem);
      Control child = configuredFieldControl as Control;
      if (child is CompositeFieldControl)
      {
        this.FieldControls.AddRange(((CompositeFieldControl) child).FieldControls.Cast<Control>());
        this.CompositeFieldControls.Add(child);
      }
      else if (child is ExpandableField)
      {
        this.FieldControls.AddRange((IEnumerable<Control>) ((ExpandableField) child).ExpandableFields);
        this.FieldControls.Add(child);
      }
      else if (child.Visible && (!(dataItem is ITextFieldDefinition) || !((ITextFieldDefinition) dataItem).ServerSideOnly))
        this.FieldControls.Add(child);
      if (dataItem.FieldType != (Type) null && dataItem.FieldType.GetCustomAttributes(typeof (RequiresDataItemAttribute), true).Length != 0)
        this.RequireDataItemControls.Add(child);
      if (child is IBulkEditFieldControl)
        this.BulkEditFieldControls.Add(child);
      if (child is ICommandField)
        this.CommandFieldControls.Add(child);
      if (!string.IsNullOrEmpty(this.FieldControlsCssClass))
        configuredFieldControl.CssClass = string.Format("{0} {1}", (object) configuredFieldControl.CssClass, (object) this.FieldControlsCssClass);
      e.Item.FindControl("fieldControlWrapper").Controls.Add(child);
    }

    /// <summary>Gets the field control by the provided definition.</summary>
    /// <param name="definition">The definition.</param>
    /// <returns></returns>
    protected internal IField GetConfiguredFieldControl(IFieldDefinition definition) => ObjectFactory.Resolve<IFieldFactory>().GetFieldControl(definition);
  }
}
