// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ContentLocationInfoField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Control that provides functionality for displaying information for the status of the content location
  /// </summary>
  [RequiresDataItem]
  public class ContentLocationInfoField : FieldControl, ICommandField
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ContentLocationInfoField.ascx");
    internal const string fieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentLocationInfoField.js";
    internal const string requiresDataItemContextInterfaceScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js";
    internal const string commandFieldInterfaceScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ICommandField.js";
    internal const string ILocalizableFieldControlScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js";
    internal const string iRequiresProviderScript = "Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js";

    /// <summary>
    /// Initializes new instances of <c>ContentLocationInfoField</c>
    /// </summary>
    public ContentLocationInfoField() => this.LayoutTemplatePath = ContentLocationInfoField.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the type (full name) of the script descriptor to be used.
    /// </summary>
    protected override string ScriptDescriptorType => typeof (ContentLocationInfoField).FullName;

    /// <summary>
    /// Gets the reference to the client binder component that is used to bind the item locations.
    /// </summary>
    protected virtual GenericCollectionBinder LocationsBinder => this.Container.GetControl<GenericCollectionBinder>("locationsBinder", true);

    /// <summary>
    /// Gets the link that when clicked expands content item locations.
    /// </summary>
    protected HtmlAnchor LocationsListExpander => this.Container.GetControl<HtmlAnchor>("locationsListExpander", true);

    /// <summary>
    /// Gets the link that when clicked expands content item locations.
    /// </summary>
    protected HtmlControl LocationsListWrapper => this.Container.GetControl<HtmlControl>("locationsListWrapper", true);

    /// <summary>Gets the wrapper for the field control.</summary>
    protected HtmlControl FieldWrapper => this.Container.GetControl<HtmlControl>("fieldWrapper", true);

    /// <summary>
    /// Gets the link that when clicked expands content item locations.
    /// </summary>
    protected Control RefreshButton => this.Container.GetControl<Control>("refreshBtn", false);

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      this.ConfigureControl((IContentLocationInfoFieldDefinition) definition);
    }

    /// <summary>Configures the fields with the definition.</summary>
    /// <param name="statusFieldDefinition">The status field definition.</param>
    internal virtual void ConfigureControl(
      IContentLocationInfoFieldDefinition statusFieldDefinition)
    {
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
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
      controlDescriptor.AddProperty("originalServiceUrl", (object) this.Page.ResolveUrl(VirtualPathUtility.RemoveTrailingSlash(this.LocationsBinder.ServiceUrl)));
      controlDescriptor.AddComponentProperty("locationsBinder", this.LocationsBinder.ClientID);
      controlDescriptor.AddElementProperty("locationsListExpander", this.LocationsListExpander.ClientID);
      controlDescriptor.AddElementProperty("locationsListWrapper", this.LocationsListWrapper.ClientID);
      controlDescriptor.AddElementProperty("fieldWrapper", this.FieldWrapper.ClientID);
      if (this.RefreshButton != null)
        controlDescriptor.AddElementProperty("refreshButton", this.RefreshButton.ClientID);
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
      string fullName = typeof (ContentLocationInfoField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ICommandField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentLocationInfoField.js", fullName)
      };
    }

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);
  }
}
