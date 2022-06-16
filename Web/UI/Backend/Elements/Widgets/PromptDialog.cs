// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PromptDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>This controls represents a bar with various command</summary>
  [ParseChildren(true)]
  public class PromptDialog : SimpleScriptView
  {
    /// <summary>The layout template path</summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Ignored so that the file can be included in StyleCop")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "Ignored so that the file can be included in StyleCop")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Ignored so that the file can be included in StyleCop")]
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.PromptDialog.ascx");
    private Collection<CommandToolboxItem> commands;
    private string wrapperTag;
    private string itemTag;
    private bool displayed = true;
    private PromptMode promptMode;
    private bool allowCloseButton = true;
    private bool showOnLoad;

    /// <summary>Creates prompt dialog from definition.</summary>
    /// <param name="def">The definition.</param>
    /// <returns>The created prompt dialog.</returns>
    public static PromptDialog FromDefinition(IPromptDialogDefinition def)
    {
      PromptDialog promptDialog = new PromptDialog();
      promptDialog.AllowCloseButton = def.AllowCloseButton;
      promptDialog.DefaultInputText = DefinitionsHelper.GetLabel(def.ResourceClassId, def.DefaultInputText);
      promptDialog.Displayed = def.Displayed;
      promptDialog.Width = def.Width;
      promptDialog.Height = def.Height;
      promptDialog.InputRows = def.InputRows;
      promptDialog.ItemTag = def.ItemTag;
      promptDialog.Message = DefinitionsHelper.GetLabel(def.ResourceClassId, def.Message);
      promptDialog.Mode = def.Mode;
      promptDialog.OnClientCommand = def.OnClientCommand;
      promptDialog.ShowOnLoad = def.ShowOnLoad;
      promptDialog.TextFieldExample = DefinitionsHelper.GetLabel(def.ResourceClassId, def.TextFieldExample);
      promptDialog.TextFieldTitle = DefinitionsHelper.GetLabel(def.ResourceClassId, def.TextFieldTitle);
      promptDialog.Title = DefinitionsHelper.GetLabel(def.ResourceClassId, def.Title);
      promptDialog.ValidatorDefinition = def.ValidatorDefinition;
      promptDialog.WrapperCssClass = def.WrapperCssClass;
      promptDialog.WrapperTag = def.WrapperTag;
      promptDialog.DialogName = def.DialogName;
      promptDialog.OpenOnCommand = def.OpenOnCommandName;
      promptDialog.ShowCheckRelatingData = false;
      foreach (ICommandToolboxItemDefinition command in def.Commands)
      {
        CommandToolboxItem commandToolboxItem = CommandToolboxItem.FromDefinition(command);
        promptDialog.Commands.Add(commandToolboxItem);
      }
      return promptDialog;
    }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? PromptDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the comma delimited list of toolbox items</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual Collection<CommandToolboxItem> Commands
    {
      get
      {
        if (this.commands == null)
          this.commands = new Collection<CommandToolboxItem>();
        return this.commands;
      }
    }

    /// <summary>Gets or sets the name of the dialog.</summary>
    /// <value>The name of the dialog.</value>
    public string DialogName { get; set; }

    /// <summary>
    /// Gets or sets the command on which the dialog is automatically displayed(when registered in MasterGridView).
    /// </summary>
    /// <value>The command on which the dialog is automatically displayed(when registered in MasterGridView).</value>
    public string OpenOnCommand { get; set; }

    /// <summary>
    /// Gets or sets the name of the client side function to be fired when client raises a command
    /// </summary>
    public string OnClientCommand { get; set; }

    /// <summary>Gets or sets the wrapper tag.</summary>
    /// <value>The wrapper tag.</value>
    public string WrapperTag
    {
      get
      {
        if (string.IsNullOrEmpty(this.wrapperTag))
          this.wrapperTag = "DIV";
        return this.wrapperTag;
      }
      set => this.wrapperTag = value;
    }

    /// <summary>Gets or sets the wrapper CSS class.</summary>
    /// <value>The wrapper CSS class.</value>
    public virtual string WrapperCssClass { get; set; }

    /// <summary>Gets or sets the default input text.</summary>
    /// <value>The default input text.</value>
    public virtual string DefaultInputText { get; set; }

    /// <summary>Gets or sets the text field example.</summary>
    /// <value>The text field example.</value>
    public virtual string TextFieldExample { get; set; }

    /// <summary>Gets or sets the message.</summary>
    /// <value>The message.</value>
    public virtual string Message { get; set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public virtual string Title { get; set; }

    /// <summary>Gets or sets the text field title.</summary>
    /// <value>The text field title.</value>
    public virtual string TextFieldTitle { get; set; }

    /// <summary>Gets or sets the inner validator definition.</summary>
    /// <value>The inner validator definition.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public Telerik.Sitefinity.Web.UI.Validation.Definitions.ValidatorDefinition InnerValidatorDefinition
    {
      get
      {
        if (this.ValidatorDefinition == null)
          this.ValidatorDefinition = (IValidatorDefinition) new Telerik.Sitefinity.Web.UI.Validation.Definitions.ValidatorDefinition();
        return this.ValidatorDefinition as Telerik.Sitefinity.Web.UI.Validation.Definitions.ValidatorDefinition;
      }
      set => this.ValidatorDefinition = (IValidatorDefinition) value;
    }

    /// <summary>Gets or sets the validator definition.</summary>
    /// <value>The validator definition.</value>
    public IValidatorDefinition ValidatorDefinition { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show on load.
    /// </summary>
    /// <value><c>true</c> if showed on load; otherwise, <c>false</c>.</value>
    public virtual bool ShowOnLoad
    {
      get => this.showOnLoad;
      set => this.showOnLoad = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the close button will be displayed.
    /// </summary>
    /// <value><c>true</c> if the close button will be rendered; otherwise, <c>false</c>.</value>
    [DefaultValue(true)]
    public virtual bool AllowCloseButton
    {
      get => this.allowCloseButton;
      set => this.allowCloseButton = value;
    }

    /// <summary>Gets or sets the input rows.</summary>
    /// <value>The input rows.</value>
    public int InputRows { get; set; }

    /// <summary>Gets or sets the width of the Web server control.</summary>
    /// <value></value>
    /// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the width of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />.</returns>
    /// <exception cref="T:System.ArgumentException">The width of the Web server control was set to a negative value. </exception>
    public int Width { get; set; }

    /// <summary>Gets or sets the height of the Web server control.</summary>
    /// <value></value>
    /// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the height of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />.</returns>
    /// <exception cref="T:System.ArgumentException">The height was set to a negative value.</exception>
    public int Height { get; set; }

    /// <summary>Gets or sets the mode.</summary>
    /// <value>The mode.</value>
    public PromptMode Mode
    {
      get => this.promptMode;
      set => this.promptMode = value;
    }

    /// <summary>Gets or sets the item tag.</summary>
    /// <value>The item tag.</value>
    public string ItemTag
    {
      get
      {
        if (string.IsNullOrEmpty(this.itemTag))
          this.itemTag = "BUTTON";
        return this.itemTag;
      }
      set => this.itemTag = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.UI.PromptDialog" /> is displayed.
    /// </summary>
    /// <value><c>true</c> if displayed; otherwise, <c>false</c>.</value>
    public bool Displayed
    {
      get => this.displayed;
      set => this.displayed = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show check relating data.
    /// </summary>
    /// <value>The show check relating data.</value>
    public bool ShowCheckRelatingData { get; set; }

    /// <summary>Gets or sets the custom template.</summary>
    /// <value>The custom template.</value>
    [TemplateContainer(typeof (PromptDialogContainer))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public ITemplate CustomTemplate { get; set; }

    /// <summary>Gets the wrapper panel.</summary>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual Panel WrapperPanel => this.Container.GetControl<Panel>("promptDialogWrapper", true);

    /// <summary>Gets the input text field.</summary>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual TextField InputTextField => this.Container.GetControl<TextField>("inputTextField", true);

    /// <summary>Gets the prompt close button.</summary>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual HtmlGenericControl PromptCloseButton => this.Container.GetControl<HtmlGenericControl>("promptCloseButton", true);

    /// <summary>Gets the prompt message.</summary>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual HtmlGenericControl PromptMessage => this.Container.GetControl<HtmlGenericControl>("promptMessage", true);

    /// <summary>Gets the prompt title.</summary>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual HtmlGenericControl PromptTitle => this.Container.GetControl<HtmlGenericControl>("promptTitle", true);

    /// <summary>Gets the prompt inner content wrapper.</summary>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual HtmlGenericControl PromptInnerContentWrapper => this.Container.GetControl<HtmlGenericControl>("promptInnerContentWrapper", true);

    /// <summary>Gets the custom content wrapper.</summary>
    public virtual HtmlGenericControl CustomContentWrapper => this.Container.GetControl<HtmlGenericControl>("customContentWrapper", true);

    /// <summary>Gets the prompt buttons panel.</summary>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual Panel PromptButtonsPannel => this.Container.GetControl<Panel>("buttonsDiv", true);

    /// <summary>Gets the check relating data checkbox div.</summary>
    protected virtual HtmlGenericControl CheckRelatingDataCheckBoxDiv => this.Container.GetControl<HtmlGenericControl>("checkRelatingDataCheckBoxDiv", true);

    /// <summary>Gets the check relating data checkbox.</summary>
    protected virtual CheckBox CheckRelatingDataCheckBox => this.Container.GetControl<CheckBox>("checkRelatingDataCheckBox", true);

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      writer.AddAttribute("id", this.ClientID);
      writer.AddAttribute("class", this.WrapperCssClass);
      if (!this.Displayed)
        writer.AddAttribute("style", "display:none;");
      writer.RenderBeginTag(this.WrapperTag);
    }

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.
    /// </returns>
    /// <example>
    /// The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (PromptDialog).FullName, this.ClientID);
      controlDescriptor.AddProperty("_commands", (object) scriptSerializer.Serialize((object) this.Commands));
      controlDescriptor.AddProperty("_defaultInputText", (object) this.DefaultInputText);
      controlDescriptor.AddProperty("_textFieldExample", (object) this.TextFieldExample);
      controlDescriptor.AddProperty("_textFieldTitle", (object) this.TextFieldTitle);
      controlDescriptor.AddProperty("_message", (object) this.Message);
      controlDescriptor.AddProperty("_title", (object) this.Title);
      controlDescriptor.AddProperty("_showOnLoad", (object) this.ShowOnLoad);
      controlDescriptor.AddProperty("_allowCloseButton", (object) this.AllowCloseButton);
      controlDescriptor.AddProperty("_promptMode", (object) this.Mode);
      controlDescriptor.AddElementProperty("wrapperElement", this.WrapperPanel.ClientID);
      controlDescriptor.AddComponentProperty("inputTextField", this.InputTextField.ClientID);
      controlDescriptor.AddElementProperty("closeButtonElement", this.PromptCloseButton.ClientID);
      controlDescriptor.AddElementProperty("promptMessageElement", this.PromptMessage.ClientID);
      controlDescriptor.AddElementProperty("promptTitleElement", this.PromptTitle.ClientID);
      controlDescriptor.AddElementProperty("promptInnerContentElement", this.PromptInnerContentWrapper.ClientID);
      controlDescriptor.AddElementProperty(this.CheckRelatingDataCheckBox.ID, this.CheckRelatingDataCheckBox.ClientID);
      controlDescriptor.AddProperty("_height", (object) this.Height);
      controlDescriptor.AddProperty("_width", (object) this.Width);
      if (!string.IsNullOrEmpty(this.OnClientCommand))
        controlDescriptor.AddEvent("command", this.OnClientCommand);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.PromptDialog.js", typeof (PromptDialog).Assembly.FullName)
    };

    /// <summary>Initializes the command buttons.</summary>
    public virtual void InitializeCommandButtons()
    {
      foreach (CommandToolboxItem command in this.Commands)
      {
        if (command != null && string.IsNullOrEmpty(command.ContainerId))
        {
          command.ContainerId = this.ID;
          command.WrapperTagName = this.ItemTag;
          int num = command.CausesValidation ? 1 : 0;
          this.PromptButtonsPannel.Controls.Add(command.GenerateCommandItem());
        }
      }
      if (this.Commands.Count != 0)
        return;
      this.PromptButtonsPannel.Visible = false;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.InitializeCommandButtons();
      TextField inputTextField = this.InputTextField;
      if (this.Mode == PromptMode.Input)
        inputTextField.DisplayMode = FieldDisplayMode.Write;
      if (this.Mode == PromptMode.Confirm)
        inputTextField.DisplayMode = FieldDisplayMode.Read;
      if (this.Mode != PromptMode.Custom)
      {
        this.CustomContentWrapper.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
      }
      else
      {
        this.PromptInnerContentWrapper.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
        if (this.Mode == PromptMode.Custom && this.CustomTemplate != null)
          this.CustomTemplate.InstantiateIn((Control) this.CustomContentWrapper);
      }
      inputTextField.Example = this.TextFieldExample;
      inputTextField.Title = this.TextFieldTitle;
      inputTextField.Value = (object) this.DefaultInputText;
      inputTextField.Rows = this.InputRows;
      this.CheckRelatingDataCheckBoxDiv.Visible = this.ShowCheckRelatingData;
      inputTextField.ValidatorDefinition = this.ValidatorDefinition != null ? (Telerik.Sitefinity.Web.UI.Validation.Definitions.ValidatorDefinition) this.ValidatorDefinition.GetDefinition() : (Telerik.Sitefinity.Web.UI.Validation.Definitions.ValidatorDefinition) null;
      this.PromptTitle.InnerHtml = this.Title;
    }
  }
}
