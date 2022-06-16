// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.NavigationTransformationsField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls
{
  /// <summary>
  /// Field control for managing the transformations of the navigation controls.
  /// </summary>
  public class NavigationTransformationsField : FieldControl
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.NavigationTransformationsField.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ResponsiveDesign.NavigationTransformationsField.ascx");

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = NavigationTransformationsField.layoutTemplatePath;
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
    /// Gets a reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets a reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets a reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets a reference to the label which displays the title of the field.
    /// </summary>
    protected virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets a reference to the label which displays the description of the field.
    /// </summary>
    protected virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets a reference to the label which displays the example of the field.
    /// </summary>
    protected virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the navigation transformations list.</summary>
    protected virtual Control NavigationTransformationsList => this.Container.GetControl<Control>("navigationTransformationsList", true);

    /// <summary>Gets the add transformation button.</summary>
    protected virtual LinkButton AddTransformationButton => this.Container.GetControl<LinkButton>("addTransformationButton", true);

    /// <summary>Gets the client label manager control.</summary>
    protected virtual ClientLabelManager ClientLabelManagerControl => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.KendoAll;

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.TitleLabel.Text = this.Title;
      this.DescriptionLabel.Text = this.Description;
      this.ExampleLabel.Text = this.Example;
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
      controlDescriptor.AddElementProperty("navigationTransformationsListElement", this.NavigationTransformationsList.ClientID);
      controlDescriptor.AddElementProperty("addTransformationButton", this.AddTransformationButton.ClientID);
      controlDescriptor.AddProperty("transformations", (object) this.GetTransfromationsFromConfig());
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManagerControl.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.WatermarkField.js", typeof (NavigationTransformationsField).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.NavigationTransformationsField.js", typeof (NavigationTransformationsField).Assembly.FullName)
    };

    private Dictionary<string, string> GetTransfromationsFromConfig()
    {
      Dictionary<string, string> transfromationsFromConfig = new Dictionary<string, string>();
      foreach (NavigationTransformationElement transformationElement in (IEnumerable<NavigationTransformationElement>) Config.Get<ResponsiveDesignConfig>().NavigationTransformations.Values)
      {
        if (transformationElement.IsActive)
        {
          string title = transformationElement.Title;
          string str;
          if (transformationElement.ResourceClassId.IsNullOrEmpty())
          {
            str = title;
          }
          else
          {
            string resourceClassId = transformationElement.ResourceClassId;
            try
            {
              str = Res.Get(resourceClassId, title);
            }
            catch (KeyNotFoundException ex)
            {
              str = title;
            }
          }
          transfromationsFromConfig[transformationElement.Name] = str;
        }
      }
      return transfromationsFromConfig;
    }
  }
}
