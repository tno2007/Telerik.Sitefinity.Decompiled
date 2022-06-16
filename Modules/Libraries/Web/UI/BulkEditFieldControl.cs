// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.BulkEditFieldControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// A field control for bulk editing properties of the items.
  /// </summary>
  public class BulkEditFieldControl : CompositeFieldControl, IBulkEditFieldControl
  {
    private IBulkEditFieldDefinition bulkEditFieldDefinition;
    private const string bulkEditControlScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.BulkEditFieldControl.js";
    private const string bulkEditInterfaceScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IBulkEditFieldControl.js";
    private const string selfExecutableScript = "Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => this.bulkEditFieldDefinition != null && !this.bulkEditFieldDefinition.TemplateName.IsNullOrEmpty() ? this.bulkEditFieldDefinition.TemplateName : string.Empty;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => this.bulkEditFieldDefinition != null && !this.bulkEditFieldDefinition.TemplatePath.IsNullOrEmpty() ? this.bulkEditFieldDefinition.TemplatePath : string.Empty;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the web service URL which will be used to bind the items grid.
    /// </summary>
    /// <value>The web service URL.</value>
    public virtual string WebServiceUrl { get; set; }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    public virtual Type ContentType { get; set; }

    /// <summary>Gets or sets the type of the parent.</summary>
    /// <value>The type of the parent.</value>
    public virtual Type ParentType { get; set; }

    /// <summary>Gets the name of the java script component.</summary>
    public override string JavaScriptComponentName => typeof (BulkEditFieldControl).FullName;

    /// <summary>
    /// Gets the reference to the control that represents the title of the composite field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.Container.GetControl<Label>("titleLabel", false);

    /// <summary>
    /// Gets the reference to the control that represents the description of the composite field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) this.Container.GetControl<Label>("descriptionLabel", false);

    /// <summary>
    /// Gets the reference to the control that represents the example of the composite field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) this.Container.GetControl<Label>("exampleLabel", false);

    /// <summary>Gets the reference to the items grid.</summary>
    /// <value>The items grid.</value>
    protected internal virtual RadGrid ItemsGrid => this.Container.GetControl<RadGrid>("itemsGrid", true);

    /// <summary>Gets the reference to the items binder.</summary>
    /// <value>The items binder.</value>
    protected internal virtual RadGridBinder ItemsBinder => this.Container.GetControl<RadGridBinder>("itemsBinder", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      this.bulkEditFieldDefinition = definition as IBulkEditFieldDefinition;
      if (this.bulkEditFieldDefinition == null)
        return;
      this.WebServiceUrl = this.bulkEditFieldDefinition.WebServiceUrl;
      this.ContentType = this.bulkEditFieldDefinition.ContentType;
      this.ParentType = this.bulkEditFieldDefinition.ParentType;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      string absolute = VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash(this.WebServiceUrl));
      controlDescriptor.AddProperty("_webServiceUrl", (object) absolute);
      controlDescriptor.AddProperty("_contentType", (object) this.ContentType.FullName);
      controlDescriptor.AddProperty("_parentType", (object) this.ParentType.FullName);
      controlDescriptor.AddComponentProperty("itemsBinder", this.ItemsBinder.ClientID);
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
      string fullName = typeof (BulkEditFieldControl).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(this.GetBaseScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IBulkEditFieldControl.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.BulkEditFieldControl.js", fullName)
      };
    }

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);

    internal virtual IEnumerable<ScriptReference> GetBaseScriptReferences() => base.GetScriptReferences();
  }
}
