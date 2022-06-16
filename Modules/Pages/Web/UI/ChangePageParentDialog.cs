// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageParentDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>The dialog that provides user interface for changing</summary>
  public class ChangePageParentDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.ChangePageParentDialog.ascx");
    private const string dialogScript = "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.ChangePageParentDialog.js";
    private const string webServiceBaseUrl = "~/Sitefinity/Services/Pages/PagesService.svc";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ChangePageParentDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (ChangePageParentDialog).FullName;

    /// <summary>
    /// Gets the reference to the page selector control used to select the new parent page.
    /// </summary>
    protected virtual PageSelector PageSelector => this.Container.GetControl<PageSelector>("pageSelector", true);

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
      controlDescriptor.AddComponentProperty("pageSelector", this.PageSelector.ClientID);
      controlDescriptor.AddProperty("_webServiceBaseUrl", (object) VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Pages/PagesService.svc")));
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
      new ScriptReference("Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.ChangePageParentDialog.js", typeof (ChangePageParentDialog).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", typeof (ChangePageOwnerDialog).Assembly.FullName)
    };
  }
}
