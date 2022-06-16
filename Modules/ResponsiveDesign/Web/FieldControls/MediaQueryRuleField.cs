// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

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
using Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls
{
  /// <summary>
  /// Field control for managing the rules of the media query.
  /// </summary>
  public class MediaQueryRuleField : FieldControl
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.MediaQueryRuleField.js";
    private const string kendoScriptRef = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ResponsiveDesign.MediaQueryRuleField.ascx");

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = MediaQueryRuleField.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the label which displays the title of the field.
    /// </summary>
    protected virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label which displays the description of the field.
    /// </summary>
    protected virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label which displays the example of the field.
    /// </summary>
    protected virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the drop down list which displas the possible
    /// types of the devices.
    /// </summary>
    protected virtual DropDownList DeviceTypesDropDown => this.Container.GetControl<DropDownList>("deviceTypesDropDown", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the button for adding the new media query rule.
    /// </summary>
    protected virtual LinkButton AddNewRuleButton => this.Container.GetControl<LinkButton>("addNewRuleButton", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the dialog for definining the media query rule.
    /// </summary>
    protected virtual HtmlGenericControl MediaQueryRuleDialog => this.Container.GetControl<HtmlGenericControl>("mediaQueryRuleDialog", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.TitleLabel.Text = this.Title;
      this.DescriptionLabel.Text = this.Description;
      this.ExampleLabel.Text = this.Example;
      this.BindDeviceTypes();
    }

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
      controlDescriptor.AddElementProperty("addNewRuleButton", this.AddNewRuleButton.ClientID);
      controlDescriptor.AddElementProperty("mediaQueryRuleDialog", this.MediaQueryRuleDialog.ClientID);
      controlDescriptor.AddElementProperty("deviceTypesDropDown", this.DeviceTypesDropDown.ClientID);
      List<MediaQueryRuleElement> queryRuleElementList = new List<MediaQueryRuleElement>();
      MediaQueryRuleElement[] queryRuleElementArray = new MediaQueryRuleElement[0];
      foreach (DeviceTypeElement deviceTypeElement in (IEnumerable<DeviceTypeElement>) Config.Get<ResponsiveDesignConfig>().DeviceTypes.Values)
      {
        foreach (MediaQueryRuleElement queryRuleElement in (IEnumerable<MediaQueryRuleElement>) deviceTypeElement.DefaultRules.Values)
          queryRuleElementList.Add(queryRuleElement);
      }
      MediaQueryRuleElement[] array = queryRuleElementList.ToArray();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(typeof (MediaQueryRuleElement[])).WriteObject((Stream) memoryStream, (object) array);
        controlDescriptor.AddProperty("_defaultRules", (object) Encoding.Default.GetString(memoryStream.ToArray()));
      }
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
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.MediaQueryRuleField.js", typeof (MediaQueryRuleField).Assembly.FullName)
    };

    private void BindDeviceTypes()
    {
      this.DeviceTypesDropDown.Items.Clear();
      foreach (DeviceTypeElement deviceTypeElement in (IEnumerable<DeviceTypeElement>) Config.Get<ResponsiveDesignConfig>().DeviceTypes.Values)
      {
        string str1 = deviceTypeElement.Title;
        if (!string.IsNullOrEmpty(deviceTypeElement.ResourceClassName))
        {
          string str2 = Res.Get(deviceTypeElement.ResourceClassName, deviceTypeElement.Title, SystemManager.CurrentContext.Culture, true, false);
          if (!string.IsNullOrEmpty(str2))
            str1 = str2;
        }
        this.DeviceTypesDropDown.Items.Add(new ListItem()
        {
          Text = str1,
          Value = deviceTypeElement.Name
        });
      }
    }
  }
}
