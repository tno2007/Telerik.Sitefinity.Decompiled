// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.ResponsiveDesignTransformationsBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings
{
  /// <summary>View for comments basic settings.</summary>
  public class ResponsiveDesignTransformationsBasicSettingsView : KendoView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.ResponsiveDesignTransformationsBasicSettingsView.ascx");
    private const string viewScript = "Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.Scripts.ResponsiveDesignTransformationsBasicSettingsView.js";

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ResponsiveDesignTransformationsBasicSettingsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overriden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    protected virtual string ScriptDescriptorTypeName => this.GetType().FullName;

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid.
    /// </summary>
    protected virtual HtmlContainerControl Grid => this.Container.GetControl<HtmlContainerControl>("transformationsGrid", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the show edit widnow button.</summary>
    protected virtual LinkButton ShowEditWidnowButton => this.Container.GetControl<LinkButton>("showEditWidnow", true);

    /// <summary>Gets the navigation transformation dialog.</summary>
    protected virtual NavigationTransformationDetailsDialog NavigationTransformationDialog => this.Container.GetControl<NavigationTransformationDetailsDialog>("navigationTransformationDialog", true);

    /// <summary>Gets the dialog that restrict delete and deactivate.</summary>
    protected virtual PromptDialog ConfirmationDialog => this.Container.GetControl<PromptDialog>("confirmationDialog", true);

    /// <summary>Gets the confirmation dialog for delete a setting.</summary>
    protected virtual PromptDialog DeleteConfirmationDialog => this.Container.GetControl<PromptDialog>("deleteConfirmationDialog", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
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
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddElementProperty("grid", this.Grid.ClientID);
      controlDescriptor.AddElementProperty("showEditWidnowButton", this.ShowEditWidnowButton.ClientID);
      string absolute = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/ResponsiveDesign/Settings.svc");
      controlDescriptor.AddProperty("webServiceUrl", (object) absolute);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddComponentProperty("navTransformationsEditDialog", this.NavigationTransformationDialog.ClientID);
      controlDescriptor.AddComponentProperty("confirmationDialog", this.ConfirmationDialog.ClientID);
      controlDescriptor.AddComponentProperty("deleteConfirmationDialog", this.DeleteConfirmationDialog.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.Scripts.ResponsiveDesignTransformationsBasicSettingsView.js", typeof (ResponsiveDesignTransformationsBasicSettingsView).Assembly.FullName)
    };
  }
}
