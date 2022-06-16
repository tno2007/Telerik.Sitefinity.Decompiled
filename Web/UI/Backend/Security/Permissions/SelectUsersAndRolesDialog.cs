// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SelectUsersAndRolesDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.Backend.Security.Permissions
{
  /// <summary>Dialog for selecting users or roles.</summary>
  public class SelectUsersAndRolesDialog : AjaxDialogBase
  {
    private const string dialogScript = "Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.Scripts.SelectUsersAndRolesDialog.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Permissions.SelectUsersAndRolesDialog.ascx");

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (SelectUsersAndRolesDialog).FullName;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SelectUsersAndRolesDialog.layoutTemplatePath : base.LayoutTemplatePath;
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

    /// <summary>
    /// Gets the reference to the selector for users and roles.
    /// </summary>
    protected virtual SimpleUsersAndRolesSelector UsersAndRolesSelector => this.Container.GetControl<SimpleUsersAndRolesSelector>("usersAndRolesSelector", true);

    /// <summary>
    /// Gets the button for saving the selected principals, and closing the dialog.
    /// </summary>
    protected virtual LinkButton DoneSelectingLink => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    /// <summary>Gets the reference to the back button.</summary>
    protected virtual LinkButton BackLink => this.Container.GetControl<LinkButton>("lnkBack", true);

    /// <summary>
    /// Gets the button for cancelling and closing the dialog.
    /// </summary>
    protected virtual LinkButton CancelSelectingLink => this.Container.GetControl<LinkButton>("lnkCancelSelecting", true);

    /// <summary>Gets the label for header of the dialog.</summary>
    protected virtual Label HeaderLabel => this.Container.GetControl<Label>("headerLabel", true);

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
      controlDescriptor.AddComponentProperty("usersAndRolesSelector", this.UsersAndRolesSelector.ClientID);
      controlDescriptor.AddElementProperty("doneSelectingLink", this.DoneSelectingLink.ClientID);
      controlDescriptor.AddElementProperty("cancelSelectingLink", this.CancelSelectingLink.ClientID);
      controlDescriptor.AddElementProperty("backLink", this.BackLink.ClientID);
      controlDescriptor.AddElementProperty("headerLabel", this.HeaderLabel.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.Scripts.SelectUsersAndRolesDialog.js", typeof (SelectUsersAndRolesDialog).Assembly.FullName)
    };
  }
}
