// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// A  field control abstraction that represents a composite collection of child field controls.
  /// The composite control implements <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" /> interface
  /// in order to add common properties for all field controls.
  /// </summary>
  public abstract class CompositeFieldControl : SimpleScriptView, IField
  {
    private IList<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl> fieldControls;
    internal const string CompositeFieldControlScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.CompositeFieldControl.js";
    internal const string IFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IField.js";
    private string javaScriptComponentName = typeof (CompositeFieldControl).FullName;

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl" /> objects.
    /// </summary>
    /// <value>A collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl" /> objects.</value>
    public virtual IList<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl> FieldControls
    {
      get
      {
        this.EnsureChildControlsWrapper();
        if (this.fieldControls == null)
        {
          this.fieldControls = (IList<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl>) new List<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl>();
          foreach (Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl containerControl in this.GetContainerControls())
            this.fieldControls.Add(containerControl);
        }
        return this.fieldControls;
      }
    }

    /// <summary>Gets or sets the template of the field control.</summary>
    /// <value>The template.</value>
    public virtual ConditionalTemplateContainer ConditionalTemplates { get; set; }

    /// <summary>Gets or sets the display mode of the control.</summary>
    /// <value>The display mode.</value>
    public virtual FieldDisplayMode DisplayMode { get; set; }

    public virtual string JavaScriptComponentName => this.javaScriptComponentName;

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    public HtmlTextWriterTag WrapperTag { get; set; }

    /// <summary>Gets or sets the name of the field.</summary>
    /// <value>The name of the field.</value>
    public virtual string FieldName { get; set; }

    /// <summary>Gets or sets the description of the field control.</summary>
    /// <value>The description of the field control.</value>
    public virtual string Description { get; set; }

    /// <summary>
    /// Gets or sets the example text associated with the field control.
    /// </summary>
    /// <value>The example text associated with the field control.</value>
    public virtual string Example { get; set; }

    /// <summary>Gets or sets the title of the field control.</summary>
    /// <value>The title of the field control.</value>
    public virtual string Title { get; set; }

    /// <summary>
    /// Gets or sets the name of the resource class used to localize the labels of the field.
    /// </summary>
    /// <value>The name of the resource class.</value>
    /// <remarks>
    /// If this property is left empty, string values such as Title, Description and Example will
    /// be used directly; otherwise the values of these properties will be used as keys for the resources
    /// and localized resources will be loaded instead.
    /// </remarks>
    public string ResourceClassId { get; set; }

    /// <summary>
    /// Gets the reference to the control that represents the title of the composite field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal abstract WebControl TitleControl { get; }

    /// <summary>
    /// Gets the reference to the control that represents the description of the composite field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal abstract WebControl DescriptionControl { get; }

    /// <summary>
    /// Gets the reference to the control that represents the example of the composite field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal abstract WebControl ExampleControl { get; }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public virtual void Configure(IFieldDefinition definition)
    {
      if (!(definition is ICompositeFieldDefinition compositeFieldDefinition))
        return;
      this.DisplayMode = compositeFieldDefinition.DisplayMode;
      this.Description = Res.ResolveLocalizedValue(compositeFieldDefinition.ResourceClassId, compositeFieldDefinition.Description);
      this.Example = Res.ResolveLocalizedValue(compositeFieldDefinition.ResourceClassId, compositeFieldDefinition.Example);
      this.Title = Res.ResolveLocalizedValue(compositeFieldDefinition.ResourceClassId, compositeFieldDefinition.Title);
      ControlUtilities.AddCssClass((WebControl) this, compositeFieldDefinition.CssClass);
      this.ResourceClassId = compositeFieldDefinition.ResourceClassId;
      this.WrapperTag = compositeFieldDefinition.WrapperTag;
      this.FieldName = compositeFieldDefinition.FieldName;
    }

    /// <summary>Creates the container.</summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    protected internal override GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = base.CreateContainer(template);
      ConditionalTemplateContainer control = container.GetControl<ConditionalTemplateContainer>();
      if (control == null)
        return container;
      control.Evaluate((object) this);
      return container;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => this.WrapperTag;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.JavaScriptComponentName, this.ClientID);
      controlDescriptor.AddProperty("title", (object) this.Title);
      controlDescriptor.AddProperty("description", (object) this.Description);
      controlDescriptor.AddProperty("example", (object) this.Example);
      if (this.DescriptionControl != null)
        controlDescriptor.AddElementProperty("descriptionElement", this.DescriptionControl.ClientID);
      if (this.ExampleControl != null)
        controlDescriptor.AddElementProperty("exampleElement", this.ExampleControl.ClientID);
      if (this.TitleControl != null)
        controlDescriptor.AddElementProperty("titleElement", this.TitleControl.ClientID);
      controlDescriptor.AddProperty("displayMode", (object) this.DisplayMode);
      controlDescriptor.AddProperty("fieldIds", (object) this.GetFieldIds());
      controlDescriptor.AddProperty("fieldName", (object) this.FieldName);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (CompositeFieldControl).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new ScriptReference[2]
      {
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IField.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.Fields.Scripts.CompositeFieldControl.js"
        }
      };
    }

    internal virtual IList<string> GetFieldIds() => (IList<string>) this.GetContainerControls().Select<Control, string>((Func<Control, string>) (f => f.ClientID)).ToList<string>();

    internal virtual IEnumerable<Control> GetContainerControls() => (IEnumerable<Control>) this.Container.GetControls<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl>().Values;

    /// <summary>Ensures the child controls.</summary>
    protected internal virtual void EnsureChildControlsWrapper() => this.EnsureChildControls();
  }
}
