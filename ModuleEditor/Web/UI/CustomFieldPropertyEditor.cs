// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.UI.CustomFieldPropertyEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ModuleEditor.Configuration;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Designers;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.ModuleEditor.Web.UI
{
  /// <summary>Property editor for the custom field controls.</summary>
  public class CustomFieldPropertyEditor : PropertyEditor
  {
    private bool? showIsLocalizableOption;
    private string fieldTypeName;
    internal const string customFieldPropertyEditorScript = "Telerik.Sitefinity.ModuleEditor.Web.Scripts.CustomFieldPropertyEditor.js";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleEditor.CustomFieldPropertyEditor.ascx");
    private Type componentType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Web.UI.CustomFieldPropertyEditor" /> class.
    /// </summary>
    public CustomFieldPropertyEditor()
    {
      this.PropertyValuesCulture = SystemManager.CurrentContext.Culture.Name;
      this.LayoutTemplatePath = CustomFieldPropertyEditor.layoutTemplatePath;
    }

    /// <summary>
    /// Gets or sets the field type name configuration key for the field being edited.
    /// </summary>
    protected string FieldTypeName
    {
      get
      {
        if (this.fieldTypeName == null)
          this.fieldTypeName = this.Page.Request.QueryString["fieldTypeName"];
        return this.fieldTypeName;
      }
      set => this.fieldTypeName = value;
    }

    /// <summary>
    /// Gets or sets the definition for the field being edited.
    /// </summary>
    protected WcfFieldDefinition Definition { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets a value indicating whether the is localizable option should be visible.
    /// </summary>
    /// <value>
    /// <c>true</c> if is localizable should be visible; otherwise, <c>false</c>.
    /// </value>
    internal bool ShowIsLocalizableOption
    {
      get
      {
        if (!this.showIsLocalizableOption.HasValue)
        {
          string str1 = this.Page.Request.QueryString["isCustom"];
          bool flag;
          if (!str1.IsNullOrEmpty())
          {
            flag = true.ToString().Equals(str1, StringComparison.InvariantCultureIgnoreCase);
          }
          else
          {
            string str2 = this.Page.Request.QueryString["command"];
            flag = "editCustomField".Equals(str2, StringComparison.InvariantCultureIgnoreCase) || "createCustomField".Equals(str2, StringComparison.InvariantCultureIgnoreCase);
          }
          int num1 = flag ? 1 : 0;
          UserFriendlyDataType friendlyDataType = UserFriendlyDataType.ShortText;
          int num2;
          if (!friendlyDataType.ToString().Equals(this.FieldTypeName, StringComparison.InvariantCultureIgnoreCase))
          {
            friendlyDataType = UserFriendlyDataType.LongText;
            if (!friendlyDataType.ToString().Equals(this.FieldTypeName, StringComparison.InvariantCultureIgnoreCase))
            {
              friendlyDataType = UserFriendlyDataType.Number;
              if (!friendlyDataType.ToString().Equals(this.FieldTypeName, StringComparison.InvariantCultureIgnoreCase))
              {
                friendlyDataType = UserFriendlyDataType.DateAndTime;
                num2 = friendlyDataType.ToString().Equals(this.FieldTypeName, StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;
                goto label_9;
              }
            }
          }
          num2 = 1;
label_9:
          this.showIsLocalizableOption = (num1 & num2) == 0 ? new bool?(false) : new bool?(this.ComponentType.ImplementsInterface(typeof (ILocalizable)));
        }
        return this.showIsLocalizableOption.Value;
      }
      set => this.showIsLocalizableOption = new bool?(value);
    }

    /// <summary>
    /// Gets the type of the component from the querystring parameter.
    /// </summary>
    /// <value>The type of the component.</value>
    internal Type ComponentType
    {
      get
      {
        if (this.componentType == (Type) null)
          this.componentType = TypeResolutionService.ResolveType(this.Page.Request.QueryString["componentType"]);
        return this.componentType;
      }
    }

    /// <summary>Gets the reference to the back link.</summary>
    public LinkButton BackLink => this.Container.GetControl<LinkButton>("backLink", true);

    /// <summary>This control is not required in this context.</summary>
    protected override LinkButton AdvancedModeButton => (LinkButton) null;

    /// <summary>This control is not required in this context.</summary>
    protected override LinkButton SimpleModeButton => (LinkButton) null;

    /// <summary>This control is not required in this context.</summary>
    protected override HtmlAnchor OkButton => (HtmlAnchor) null;

    /// <summary>
    /// Represents the manager that controls the localization strings.
    /// </summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      this.HideAdvancedMode = true;
      this.DisableAdvancedMode();
      this.PropertyGrid.PropertyEditor = (PropertyEditor) this;
      FieldTypeElement fieldTypeElement;
      if (this.FieldTypeName.IsNullOrEmpty() || !Config.Get<CustomFieldsConfig>().FieldTypes.TryGetValue(this.FieldTypeName, out fieldTypeElement))
        return;
      FieldControlElement fieldControlElement = fieldTypeElement.Controls[0];
      string fieldName = this.Page.Request.QueryString["fieldName"];
      Type fieldType = TypeResolutionService.ResolveType(fieldControlElement.FieldTypeOrPath);
      string command = this.Page.Request.QueryString["command"];
      bool localizableByDefault = this.ShowIsLocalizableOption && (fieldControlElement.Parent.Parent.TagName == "ShortText" || fieldControlElement.Parent.Parent.TagName == "LongText");
      this.Definition = WcfDefinitionBuilder.GetWcfDefinition(fieldType, this.ComponentType, fieldName, command, localizableByDefault);
      if (this.FieldTypeName == UserFriendlyDataType.Classification.ToString())
      {
        string input = !this.Definition.TaxonomyId.IsNullOrEmpty() ? this.Definition.TaxonomyId : this.Page.Request.QueryString["taxonomyId"];
        if (input != null)
        {
          fieldControlElement = TaxonomyManager.GetManager().GetTaxonomy(Guid.Parse(input)) is FlatTaxonomy ? fieldTypeElement.Controls[0] : fieldTypeElement.Controls[1];
          this.Definition = WcfDefinitionBuilder.GetWcfDefinition(TypeResolutionService.ResolveType(fieldControlElement.FieldTypeOrPath), this.ComponentType, fieldName, command, localizableByDefault);
        }
      }
      this.ControlDesigner = (ControlDesignerBase) this.Page.LoadControl(string.IsNullOrEmpty(fieldControlElement.DesignerType) ? typeof (TextFieldDesigner) : TypeResolutionService.ResolveType(fieldControlElement.DesignerType), (object[]) null);
      this.Control = (object) fieldControlElement.GetFieldControl();
      string str = this.Page.Request.QueryString["renderChoiceAs"];
      if (!(str == "RadioButtons"))
      {
        if (!(str == "DropDownList"))
        {
          if (str == "Checkboxes")
          {
            ((ChoiceField) this.Control).RenderChoicesAs = RenderChoicesAs.CheckBoxes;
            this.Definition.RenderChoiceAs = RenderChoicesAs.CheckBoxes;
          }
        }
        else
        {
          ((ChoiceField) this.Control).RenderChoicesAs = RenderChoicesAs.DropDown;
          this.Definition.RenderChoiceAs = RenderChoicesAs.DropDown;
        }
      }
      else
      {
        ((ChoiceField) this.Control).RenderChoicesAs = RenderChoicesAs.RadioButtons;
        this.Definition.RenderChoiceAs = RenderChoicesAs.RadioButtons;
      }
      this.ControlDesigner.ControlId = this.ControlId;
      this.ControlDesigner.PropertyEditor = (PropertyEditor) this;
      this.SimpleModeView.Controls.Add((Control) this.ControlDesigner);
      this.implementsDesigner = true;
      if (!string.IsNullOrEmpty(this.Definition.ResourceClassId))
      {
        string title = this.Definition.Title;
        string example = this.Definition.Example;
        try
        {
          if (!title.IsNullOrEmpty())
            this.Definition.Title = Res.Get(this.Definition.ResourceClassId, title);
          if (!example.IsNullOrEmpty())
            this.Definition.Example = Res.Get(this.Definition.ResourceClassId, example);
        }
        catch
        {
        }
      }
      if (string.IsNullOrEmpty(this.Definition.Title))
        this.Definition.Title = fieldName;
      bool result = false;
      bool flag1 = bool.TryParse(this.Page.Request.QueryString["hidden"], out result);
      if (result)
        this.Definition.Hidden = flag1;
      result = false;
      bool flag2 = bool.TryParse(this.Page.Request.QueryString["multiple"], out result);
      if (result)
        this.Definition.AllowMultipleSelection = flag2;
      if (!(command == "editCustomField") && !(command == "editDefaultField"))
        return;
      this.BackLink.Attributes["style"] = "display: none;";
    }

    /// <summary>Gets the property bag.</summary>
    /// <returns></returns>
    protected override IList<WcfControlProperty> GetPropertyBag() => (IList<WcfControlProperty>) new List<WcfControlProperty>();

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("backLink", this.BackLink.ClientID);
      controlDescriptor.AddProperty("_defaultValueValidationMessage", (object) Res.Get<ModuleEditorResources>().DefaultValueExceedsMaxRange);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(typeof (WcfFieldDefinition)).WriteObject((Stream) memoryStream, (object) this.Definition);
        controlDescriptor.AddProperty("_definition", (object) Encoding.UTF8.GetString(memoryStream.ToArray()));
      }
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.ModuleEditor.Web.Scripts.CustomFieldPropertyEditor.js", typeof (CustomFieldPropertyEditor).Assembly.FullName)
    };
  }
}
