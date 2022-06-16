// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.UI.FieldsGrid
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ItemLists;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.ModuleEditor.Web.UI
{
  /// <summary>
  /// Displays a grid of fields (default or custom) of a given type
  /// </summary>
  public class FieldsGrid : SimpleScriptView
  {
    /// <summary>The layout template path</summary>
    public static readonly string LayoutTemplatePathValue = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleEditor.FieldsGrid.ascx");
    /// <summary>The layout template path</summary>
    public static readonly string layoutTemplatePathValue = FieldsGrid.LayoutTemplatePathValue;
    internal const string FieldsGridScript = "Telerik.Sitefinity.ModuleEditor.Web.Scripts.FieldsGrid.js";
    private const string CustomFieldEditDialogUrl = "~/Sitefinity/Dialog/CustomFieldPropertyEditor/";
    private const string CustomFieldCreateDialogUrl = "~/Sitefinity/Dialog/FieldWizardDialog";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Web.UI.FieldsGrid" /> class.
    /// </summary>
    public FieldsGrid() => this.LayoutTemplatePath = FieldsGrid.LayoutTemplatePathValue;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the base URL for the service that works with fields
    /// </summary>
    /// <value>The service base URL.</value>
    public string ServiceBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the type of the field that the grid is bound to. Different columns are displayed according to this.
    /// </summary>
    public FieldType FieldType { get; set; }

    /// <summary>
    /// Gets or sets whether addition of content link data type fields is allowed.
    /// </summary>
    /// <value>Whether addition of content link data type fields is allowed.</value>
    public bool AllowContentLinks { get; set; }

    /// <summary>Gets the items grid listing default fields</summary>
    protected virtual ItemsGrid ItemsGridDefault => this.Container.GetControl<ItemsGrid>("fieldsGridDefault", this.FieldType == FieldType.Default);

    /// <summary>Gets the items grid listing custom fields</summary>
    protected virtual ItemsGrid ItemsGridCustom => this.Container.GetControl<ItemsGrid>("fieldsGridCustom", this.FieldType == FieldType.Custom);

    /// <summary>Gets the RadWindowManager</summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>Gets the link for adding custom fields.</summary>
    protected virtual HyperLink AddCustomFieldLink => this.Container.GetControl<HyperLink>("addCustomField", this.FieldType == FieldType.Custom);

    /// <summary>
    /// Gets the label displaying a message when there are no custom fields.
    /// </summary>
    protected Label NoCustomFieldsLabel => this.Container.GetControl<Label>(nameof (NoCustomFieldsLabel), this.FieldType == FieldType.Custom);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
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

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("radWindowManager", this.RadWindowManager.ClientID);
      if (this.FieldType == FieldType.Custom)
      {
        controlDescriptor.AddElementProperty("addCustomFieldLink", this.AddCustomFieldLink.ClientID);
        controlDescriptor.AddElementProperty("noCustomFieldsLabel", this.NoCustomFieldsLabel.ClientID);
        controlDescriptor.AddComponentProperty("itemsGrid", this.ItemsGridCustom.ClientID);
      }
      else if (this.FieldType == FieldType.Default)
        controlDescriptor.AddComponentProperty("itemsGrid", this.ItemsGridDefault.ClientID);
      controlDescriptor.AddProperty("_fieldEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/CustomFieldPropertyEditor/"));
      controlDescriptor.AddProperty("_createFieldDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/FieldWizardDialog"));
      controlDescriptor.AddProperty("allowContentLinks", (object) this.AllowContentLinks);
      controlDescriptor.AddProperty("_siteBaseUrl", (object) (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') + "/"));
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
      ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.ModuleEditor.Web.Scripts.FieldsGrid.js", this.GetType().Assembly.GetName().ToString()));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
