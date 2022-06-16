// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.DefaultFieldsEditorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.ModuleEditor.Configuration;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.ModuleEditor.Web.UI;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.ModuleEditor.Web
{
  /// <summary>
  /// The dialog that is used to edit the data structure and the backend user interface of the modules.
  /// </summary>
  public class DefaultFieldsEditorDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleEditor.DefaultFieldsEditorDialog.ascx");
    internal const string dialogScript = "Telerik.Sitefinity.ModuleEditor.Web.Scripts.DefaultFieldsEditorDialog.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Web.DefaultFieldsEditorDialog" /> class.
    /// </summary>
    public DefaultFieldsEditorDialog() => this.LayoutTemplatePath = DefaultFieldsEditorDialog.layoutTemplatePath;

    /// <summary>
    /// Gets or sets the field type name configuration key for the field being edited.
    /// </summary>
    protected string FieldTypeName { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (DefaultFieldsEditorDialog).FullName;

    /// <summary>Gets the command bar.</summary>
    /// <value>The command bar.</value>
    public CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true);

    /// <summary>Gets the views selector.</summary>
    /// <value>The views selector.</value>
    protected ViewsSelector ViewsSelector => this.Container.GetControl<ViewsSelector>("viewsSelector", true);

    /// <summary>
    /// Gets or sets the definition for the field being edited.
    /// </summary>
    protected WcfFieldDefinition Definition { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      NameValueCollection queryString = this.Page.Request.QueryString;
      this.FieldTypeName = queryString["fieldTypeName"];
      string name = queryString["componentType"];
      string fieldName = queryString["fieldName"];
      string command = this.Page.Request.QueryString["command"];
      FieldTypeElement fieldTypeElement;
      this.Definition = WcfDefinitionBuilder.GetBaseWcfDefinition(this.FieldTypeName.IsNullOrEmpty() || !Config.Get<CustomFieldsConfig>().FieldTypes.TryGetValue(this.FieldTypeName, out fieldTypeElement) ? typeof (TextField) : TypeResolutionService.ResolveType(fieldTypeElement.Controls[0].FieldTypeOrPath), TypeResolutionService.ResolveType(name), fieldName, command);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      controlDescriptor.AddComponentProperty("viewsSelector", this.ViewsSelector.ClientID);
      controlDescriptor.AddProperty("_defaultValueValidationMessage", (object) Res.Get<ModuleEditorResources>().DefaultValueExceedsMaxRange);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(typeof (WcfFieldDefinition)).WriteObject((Stream) memoryStream, (object) this.Definition);
        controlDescriptor.AddProperty("_definition", (object) Encoding.Default.GetString(memoryStream.ToArray()));
      }
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.ModuleEditor.Web.Scripts.DefaultFieldsEditorDialog.js", this.GetType().Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
