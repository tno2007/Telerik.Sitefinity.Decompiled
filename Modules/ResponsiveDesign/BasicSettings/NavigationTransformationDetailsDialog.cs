// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavigationTransformationDetailsDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings
{
  public class NavigationTransformationDetailsDialog : KendoWindow
  {
    private static RegexStrategy regexStrategy = (RegexStrategy) null;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.NavigationTransformationDetailsDialog.ascx");
    private const string viewScript = "Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.Scripts.NavigationTransformationDetailsDialog.js";

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? NavigationTransformationDetailsDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the client label manager control.</summary>
    protected virtual ClientLabelManager ClientLabelManagerControl => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets a reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the buttons panel.</summary>
    protected virtual HtmlGenericControl ButtonsPanel => this.Container.GetControl<HtmlGenericControl>("buttonsPanel", true);

    /// <summary>Gets the loading view.</summary>
    protected virtual HtmlGenericControl LoadingView => this.Container.GetControl<HtmlGenericControl>("loadingView", true);

    /// <summary>Gets the dialog title label.</summary>
    protected virtual Label DialogTitleLabel => this.Container.GetControl<Label>("dialogTitleLabel", true);

    /// <summary>Gets the title text field.</summary>
    protected virtual TextField TitleTextField => this.Container.GetControl<TextField>("titleTextField", true);

    /// <summary>Gets the transformation CSS text field.</summary>
    protected virtual TextField TransformationCssTextField => this.Container.GetControl<TextField>("transformationCssTextField", true);

    /// <summary>Gets the name in code text field.</summary>
    protected virtual MirrorTextField NameInCodeTextField => this.Container.GetControl<MirrorTextField>("nameInCodeTextField", true);

    /// <summary>Gets the active transformation choice field.</summary>
    protected virtual CheckBox ActiveTransformationChoiceField => this.Container.GetControl<CheckBox>("activeTransformation", true);

    /// <summary>Gets a reference to the save button.</summary>
    protected virtual HtmlAnchor SaveButton => this.Container.GetControl<HtmlAnchor>("saveButton", true);

    /// <summary>Gets a reference to the cancel link.</summary>
    protected virtual HtmlAnchor CancelLink => this.Container.GetControl<HtmlAnchor>("cancelLink", true);

    /// <summary>Gets the proceed edit confirmation dialog.</summary>
    protected virtual PromptDialog ProceedEditConfirmationDialog => this.Container.GetControl<PromptDialog>("proceedEditConfirmationDialog", true);

    /// <summary>Gets the deactivate confirmation dialog.</summary>
    protected virtual PromptDialog ConfirmationDialog => this.Container.GetControl<PromptDialog>("confirmationDialog", true);

    /// <summary>
    /// Gets a reference to the outer div containing the window content.
    /// </summary>
    protected override HtmlContainerControl OuterDiv => this.Container.GetControl<HtmlContainerControl>("wrapper", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.NameInCodeTextField.RegularExpressionFilter = NavigationTransformationDetailsDialog.RgxStrategy.DefaultExpressionFilter;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().First<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("saveButton", this.SaveButton.ClientID);
      controlDescriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      controlDescriptor.AddElementProperty("dialogTitleLabel", this.DialogTitleLabel.ClientID);
      controlDescriptor.AddComponentProperty("titleTextField", this.TitleTextField.ClientID);
      controlDescriptor.AddComponentProperty("transformationCssTextField", this.TransformationCssTextField.ClientID);
      controlDescriptor.AddComponentProperty("nameInCodeTextField", this.NameInCodeTextField.ClientID);
      controlDescriptor.AddElementProperty("activeTransformationChoiceField", this.ActiveTransformationChoiceField.ClientID);
      string str = this.Page.ResolveUrl("~/Sitefinity/Services/ResponsiveDesign/Settings.svc/nav_transformations/");
      controlDescriptor.AddProperty("webServiceUrl", (object) str);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManagerControl.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
      controlDescriptor.AddComponentProperty("confirmationDialog", this.ConfirmationDialog.ClientID);
      controlDescriptor.AddComponentProperty("proceedEditConfirmationDialog", this.ProceedEditConfirmationDialog.ClientID);
      controlDescriptor.AddElementProperty("buttonsPanel", this.ButtonsPanel.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.Scripts.NavigationTransformationDetailsDialog.js", typeof (NavigationTransformationDetailsDialog).Assembly.GetName().ToString()),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.codemirror.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.css.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name)
    };

    private static RegexStrategy RgxStrategy
    {
      get
      {
        if (NavigationTransformationDetailsDialog.regexStrategy == null)
          NavigationTransformationDetailsDialog.regexStrategy = ObjectFactory.Resolve<RegexStrategy>();
        return NavigationTransformationDetailsDialog.regexStrategy;
      }
    }
  }
}
