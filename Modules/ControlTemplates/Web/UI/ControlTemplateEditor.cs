// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.ControlTemplateEditor
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
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.FieldControls;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Modules.ControlTemplates.Web.UI
{
  /// <summary>Dialog for selecting users or roles.</summary>
  public class ControlTemplateEditor : AjaxDialogBase
  {
    private static RegexStrategy regexStrategy = (RegexStrategy) null;
    private IDictionary<string, string> typeMappings;
    private IDictionary<string, string> areaNameMappings;
    internal const string controlTemplateEditorScript = "Telerik.Sitefinity.Modules.ControlTemplates.Web.Scripts.ControlTemplateEditor.js";
    private const string jQueryCaretScript = "Telerik.Sitefinity.Resources.Scripts.jquery.a-tools-1.5.2.min.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ControlTemplates.ControlTemplateEditor.ascx");

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (ControlTemplateEditor).FullName;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ControlTemplateEditor.layoutTemplatePath : base.LayoutTemplatePath;
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

    /// <summary>Gets the literal displaying dialog title.</summary>
    protected virtual ITextControl DialogTitleLiteral => this.Container.GetControl<ITextControl>("dialogTitleLiteral", true);

    /// <summary>Gets the top command bar.</summary>
    /// <value>The top command bar.</value>
    protected virtual CommandBar TopCommandBar => this.Container.GetControl<CommandBar>("topCommandBar", true);

    /// <summary>Gets the bottom command bar.</summary>
    /// <value>The bottom command bar.</value>
    protected virtual CommandBar BottomCommandBar => this.Container.GetControl<CommandBar>("bottomCommandBar", true);

    /// <summary>Gets the list with control types.</summary>
    /// <value>The list with control types.</value>
    protected virtual ChoiceField ControlTypesList => this.Container.GetControl<ChoiceField>("controlTypesList", false);

    /// <summary>Gets the fields client binder.</summary>
    public virtual FieldControlsBinder FieldsBinder => this.Container.GetControl<FieldControlsBinder>("fieldsBinder", true);

    /// <summary>Gets the field control client ids.</summary>
    /// <value>The field control client ids.</value>
    public virtual IEnumerable<string> FieldControlClientIds => this.Container.GetControls<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl>().Select<KeyValuePair<string, Control>, string>((Func<KeyValuePair<string, Control>, string>) (c => c.Value.ClientID));

    /// <summary>Gets the common properties items list.</summary>
    /// <value>The common properties items list.</value>
    public virtual ItemsList CommonPropertiesItemsList => this.Container.GetControl<ItemsList>("commonPropertiesItemsList", false);

    /// <summary>Gets the other properties items list.</summary>
    /// <value>The other properties items list.</value>
    public virtual ItemsList OtherPropertiesItemsList => this.Container.GetControl<ItemsList>("otherPropertiesItemsList", false);

    /// <summary>Gets the field for inserting data.</summary>
    /// <value>The field for inserting data.</value>
    public virtual TextField TemplateDataField => this.Container.GetControl<TextField>("templateDataField", true);

    /// <summary>Gets the label displaying module title.</summary>
    protected virtual Label ModuleTitle => this.Container.GetControl<Label>("moduleTitle", false);

    /// <summary>
    /// Gets the button for expanding common properties container.
    /// </summary>
    protected virtual LinkButton ExpandCommonPropertiesButton => this.Container.GetControl<LinkButton>("expandCommonPropertiesButton", false);

    /// <summary>
    /// Gets the button for expanding other properties container.
    /// </summary>
    protected virtual LinkButton ExpandOtherPropertiesButton => this.Container.GetControl<LinkButton>("expandOtherPropertiesButton", false);

    /// <summary>Gets the common properties container.</summary>
    protected virtual Control CommonPropertiesContainer => this.Container.GetControl<Control>("commonPropertiesContainer", false);

    /// <summary>Gets the other properties container.</summary>
    /// <value>The other properties container.</value>
    protected virtual Control OtherPropertiesContainer => this.Container.GetControl<Control>("otherPropertiesContainer", false);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Web.UI.PromptDialog" /> control used to confirm the user's choice for restoring a template to its defaults.
    /// </summary>
    protected virtual PromptDialog RestorePrompt => this.Container.GetControl<PromptDialog>("promptWindow", true);

    /// <summary>
    /// Gets the message control displaying success and error messages
    /// </summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>
    /// Get the client label manager which provides localized resources on the client
    /// </summary>
    protected virtual ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("labelManager", true);

    /// <summary>Get the control type selector.</summary>
    protected virtual ChoiceField ControlTypeSelector => this.Container.GetControl<ChoiceField>("controlTypesList", false);

    /// <summary>Get the item type full name selector.</summary>
    protected virtual ChoiceField ItemTypesNamesSelector => this.Container.GetControl<ChoiceField>("itemTypesNamesSelector", false);

    /// <summary>Gets the mirror text field for the taxon name.</summary>
    /// <value>The mirror text field.</value>
    protected virtual MirrorTextField NameForDevelopersField => this.Container.GetControl<MirrorTextField>("nameForDevelopersField", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string viewName = this.ViewName;
      if (!(viewName == "ControlTemplatesBackendInsert"))
      {
        if (viewName == "ControlTemplatesBackendEdit")
        {
          this.DialogTitleLiteral.Text = Res.Get<ControlTemplatesResources>().EditTemplate;
          this.HideCommandToolboxItem(this.TopCommandBar, "create");
          this.HideCommandToolboxItem(this.BottomCommandBar, "create");
        }
      }
      else
      {
        this.DialogTitleLiteral.Text = Res.Get<ControlTemplatesResources>().CreateTemplate;
        this.HideCommandToolboxItem(this.TopCommandBar, "save");
        this.HideCommandToolboxItem(this.BottomCommandBar, "save");
        this.HideCommandToolboxItem(this.TopCommandBar, "restoreTemplate");
        this.HideCommandToolboxItem(this.BottomCommandBar, "restoreTemplate");
      }
      this.BindControlTypesList();
      if (this.NameForDevelopersField == null)
        return;
      this.NameForDevelopersField.RegularExpressionFilter = ControlTemplateEditor.RgxStrategy.DefaultExpressionFilter;
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
      if (this.ExpandCommonPropertiesButton != null)
        controlDescriptor.AddElementProperty("expandCommonPropertiesButton", this.ExpandCommonPropertiesButton.ClientID);
      if (this.ExpandOtherPropertiesButton != null)
        controlDescriptor.AddElementProperty("expandOtherPropertiesButton", this.ExpandOtherPropertiesButton.ClientID);
      if (this.CommonPropertiesContainer != null)
        controlDescriptor.AddElementProperty("commonPropertiesContainer", this.CommonPropertiesContainer.ClientID);
      if (this.OtherPropertiesContainer != null)
        controlDescriptor.AddElementProperty("otherPropertiesContainer", this.OtherPropertiesContainer.ClientID);
      controlDescriptor.AddComponentProperty("topCommandBar", this.TopCommandBar.ClientID);
      controlDescriptor.AddComponentProperty("bottomCommandBar", this.BottomCommandBar.ClientID);
      controlDescriptor.AddComponentProperty("binder", this.FieldsBinder.ClientID);
      if (this.CommonPropertiesItemsList != null)
        controlDescriptor.AddComponentProperty("commonPropertiesItemsList", this.CommonPropertiesItemsList.ClientID);
      if (this.OtherPropertiesItemsList != null)
        controlDescriptor.AddComponentProperty("otherPropertiesItemsList", this.OtherPropertiesItemsList.ClientID);
      if (this.ControlTypesList != null)
        controlDescriptor.AddComponentProperty("controlTypesList", this.ControlTypesList.ClientID);
      controlDescriptor.AddComponentProperty("templateDataField", this.TemplateDataField.ClientID);
      controlDescriptor.AddComponentProperty("restorePrompt", this.RestorePrompt.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.LabelManager.ClientID);
      if (this.ControlTypeSelector != null)
        controlDescriptor.AddComponentProperty("controlTypeSelector", this.ControlTypeSelector.ClientID);
      if (this.ItemTypesNamesSelector != null)
        controlDescriptor.AddComponentProperty("itemTypesNamesSelector", this.ItemTypesNamesSelector.ClientID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      controlDescriptor.AddProperty("_fieldControlIds", (object) scriptSerializer.Serialize((object) this.FieldControlClientIds));
      controlDescriptor.AddProperty("_typeMappings", (object) scriptSerializer.Serialize((object) this.typeMappings));
      controlDescriptor.AddProperty("_areaNameMappings", (object) scriptSerializer.Serialize((object) this.areaNameMappings));
      controlDescriptor.AddProperty("_templateCondition", (object) this.TemplateCondition);
      controlDescriptor.AddProperty("_createCommandName", (object) "create");
      controlDescriptor.AddProperty("_saveCommandName", (object) "save");
      controlDescriptor.AddProperty("_cancelCommandName", (object) "cancel");
      controlDescriptor.AddProperty("_previewCommandName", (object) "preview");
      controlDescriptor.AddProperty("_restoreCommandName", (object) "restoreTemplate");
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
      new ScriptReference("Telerik.Sitefinity.Modules.ControlTemplates.Web.Scripts.ControlTemplateEditor.js", typeof (ControlTemplateEditor).Assembly.FullName),
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

    private void BindControlTypesList()
    {
      if (this.typeMappings == null)
        this.typeMappings = (IDictionary<string, string>) new Dictionary<string, string>();
      if (this.areaNameMappings == null)
        this.areaNameMappings = (IDictionary<string, string>) new Dictionary<string, string>();
      if (this.ControlTypesList == null)
        return;
      IDictionary<string, IControlTemplateInfo> templatableControls = Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.GetTemplatableControls();
      PageTemplatesAvailability config = Config.Get<PagesConfig>().PageTemplatesFrameworks;
      foreach (KeyValuePair<string, IControlTemplateInfo> keyValuePair in templatableControls.OrderByDescending<KeyValuePair<string, IControlTemplateInfo>, bool>((Func<KeyValuePair<string, IControlTemplateInfo>, bool>) (p => p.Value.FriendlyControlName.ToString().Contains("MVC"))).Where<KeyValuePair<string, IControlTemplateInfo>>((Func<KeyValuePair<string, IControlTemplateInfo>, bool>) (p => config != PageTemplatesAvailability.MvcOnly || p.Value.FriendlyControlName.ToString().Contains("MVC"))).ToList<KeyValuePair<string, IControlTemplateInfo>>())
      {
        string key = keyValuePair.Key;
        IControlTemplateInfo controlTemplateInfo = keyValuePair.Value;
        if (!string.IsNullOrEmpty(keyValuePair.Value.ResourceClassId))
        {
          string areaName = Res.Get(keyValuePair.Value.ResourceClassId, keyValuePair.Value.AreaName);
          key = Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.GetControlKey(keyValuePair.Value.ControlType, areaName);
        }
        this.ControlTypesList.Choices.Add(new ChoiceItem()
        {
          Text = controlTemplateInfo.ResourceClassId.IsNullOrEmpty() ? controlTemplateInfo.FriendlyControlName : Res.Get(controlTemplateInfo.ResourceClassId, controlTemplateInfo.FriendlyControlName),
          Value = key
        });
        if (keyValuePair.Value.DataItemType != (Type) null)
        {
          if (keyValuePair.Value.DataItemType.FullName.StartsWith("Telerik.Sitefinity"))
            this.typeMappings.Add(key, WcfHelper.EncodeWcfString(keyValuePair.Value.DataItemType.FullName));
          else
            this.typeMappings.Add(key, WcfHelper.EncodeWcfString(keyValuePair.Value.DataItemType.AssemblyQualifiedName));
        }
        this.areaNameMappings.Add(key, controlTemplateInfo.ResourceClassId.IsNullOrEmpty() ? controlTemplateInfo.AreaName : Res.Get(controlTemplateInfo.ResourceClassId, controlTemplateInfo.AreaName));
      }
    }

    private PresentationData CreateBlankDataItem() => (PresentationData) PageManager.GetManager(this.ProviderName).CreatePresentationItem<ControlPresentation>(Guid.Empty);

    private static RegexStrategy RgxStrategy
    {
      get
      {
        if (ControlTemplateEditor.regexStrategy == null)
          ControlTemplateEditor.regexStrategy = ObjectFactory.Resolve<RegexStrategy>();
        return ControlTemplateEditor.regexStrategy;
      }
    }
  }
}
