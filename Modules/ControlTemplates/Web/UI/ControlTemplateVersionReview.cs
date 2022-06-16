// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.ControlTemplateVersionReview
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning.Web.UI;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.FieldControls;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.ControlTemplates.Web.UI
{
  /// <summary>Dialog for selecting users or roles.</summary>
  public class ControlTemplateVersionReview : AjaxDialogBase
  {
    private IDictionary<string, string> typeMappings;
    private IDictionary<string, string> areaNameMappings;
    internal const string controlTemplateEditorScript = "Telerik.Sitefinity.Modules.ControlTemplates.Web.Scripts.ControlTemplateVersionReview.js";
    private const string jQueryCaretScript = "Telerik.Sitefinity.Resources.Scripts.jquery.a-tools-1.5.2.min.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ControlTemplates.ControlTemplateVersionReview.ascx");

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (ControlTemplateVersionReview).FullName;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ControlTemplateVersionReview.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public virtual string ProviderName { get; set; }

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    protected virtual string ViewName
    {
      get
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        return currentHttpContext != null ? currentHttpContext.Request.QueryString[nameof (ViewName)] : string.Empty;
      }
    }

    /// <summary>
    ///  Gets a string data used to filter given presentation data (template).
    /// </summary>
    /// <value>The name of the view.</value>
    protected virtual string TemplateCondition
    {
      get
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        return currentHttpContext != null ? currentHttpContext.Request.QueryString["Condition"] : string.Empty;
      }
    }

    /// <summary>
    /// Gets the literal of the button for turning back and closing the dialog.
    /// </summary>
    protected virtual ITextControl BackLiteral => this.Container.GetControl<ITextControl>("backLiteral", true);

    /// <summary>Gets the bottom command bar.</summary>
    /// <value>The bottom command bar.</value>
    protected virtual CommandBar BottomCommandBar => this.Container.GetControl<CommandBar>("bottomCommandBar", true);

    /// <summary>Gets the fields client binder.</summary>
    public virtual FieldControlsBinder FieldsBinder => this.Container.GetControl<FieldControlsBinder>("fieldsBinder", true);

    /// <summary>Gets the field for inserting data.</summary>
    /// <value>The field for inserting data.</value>
    public virtual TextField TemplateDataField => this.Container.GetControl<TextField>("templateDataField", true);

    /// <summary>Gets the label displaying module title.</summary>
    protected virtual Label ModuleTitle => this.Container.GetControl<Label>("moduleTitle", false);

    /// <summary>
    /// Gets the message control displaying success and error messages
    /// </summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>
    /// Get the client label manager which provides localized resources on the client
    /// </summary>
    protected virtual ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("labelManager", true);

    /// <summary>Get the item type full name selector.</summary>
    protected virtual VersionNoteControl VersionNoteControl => this.Container.GetControl<VersionNoteControl>("versionNote", true);

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
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      if (this.ModuleTitle != null)
        controlDescriptor.AddElementProperty("moduleTitle", this.ModuleTitle.ClientID);
      controlDescriptor.AddComponentProperty("bottomCommandBar", this.BottomCommandBar.ClientID);
      controlDescriptor.AddComponentProperty("binder", this.FieldsBinder.ClientID);
      controlDescriptor.AddComponentProperty("templateDataField", this.TemplateDataField.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.LabelManager.ClientID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      controlDescriptor.AddProperty("_typeMappings", (object) scriptSerializer.Serialize((object) this.typeMappings));
      controlDescriptor.AddProperty("_areaNameMappings", (object) scriptSerializer.Serialize((object) this.areaNameMappings));
      controlDescriptor.AddProperty("_templateCondition", (object) this.TemplateCondition);
      controlDescriptor.AddProperty("_createCommandName", (object) "create");
      controlDescriptor.AddProperty("_saveCommandName", (object) "save");
      controlDescriptor.AddProperty("_deleteCommandName", (object) "delete");
      controlDescriptor.AddProperty("_cancelCommandName", (object) "cancel");
      controlDescriptor.AddProperty("_previewCommandName", (object) "preview");
      controlDescriptor.AddProperty("_restoreCommandName", (object) "restoreTemplate");
      controlDescriptor.AddProperty("_historyServiceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Versioning/HistoryService.svc/"));
      PresentationData blankDataItem = this.CreateBlankDataItem();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(blankDataItem.GetType(), (IEnumerable<Type>) new Type[1]
        {
          blankDataItem.GetType()
        }).WriteObject((Stream) memoryStream, (object) blankDataItem);
        controlDescriptor.AddProperty("_blankDataItem", (object) Encoding.Default.GetString(memoryStream.ToArray()));
      }
      controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.ControlTemplates.Web.Scripts.ControlTemplateVersionReview.js", typeof (ControlTemplateVersionReview).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery.a-tools-1.5.2.min.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.codemirror.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.htmlmixed.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.xml.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.css.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.javascript.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name)
    };

    private void HideCommandToolboxItem(CommandBar commandBar, string commandName)
    {
      ICommandButton commandButton = commandBar.Commands.OfType<ICommandButton>().Where<ICommandButton>((Func<ICommandButton, bool>) (b => b.CommandName == commandName)).FirstOrDefault<ICommandButton>();
      if (commandButton == null)
        return;
      ((ToolboxItemBase) commandButton).Visible = false;
    }

    private PresentationData CreateBlankDataItem() => (PresentationData) PageManager.GetManager(this.ProviderName).CreatePresentationItem<ControlPresentation>(Guid.Empty);
  }
}
