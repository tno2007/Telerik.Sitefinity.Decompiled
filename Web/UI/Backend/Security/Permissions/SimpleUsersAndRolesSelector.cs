// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SimpleUsersAndRolesSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Security.Web.Services;
using Telerik.Sitefinity.Utilities.Json;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;

namespace Telerik.Sitefinity.Web.UI.Backend.Security.Permissions
{
  /// <summary>Control for selecting users and roles</summary>
  public class SimpleUsersAndRolesSelector : SimpleScriptView
  {
    private const string selectorScript = "Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.Scripts.SimpleUsersAndRolesSelector.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Permissions.SimpleUsersAndRolesSelector.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SimpleUsersAndRolesSelector.layoutTemplatePath : base.LayoutTemplatePath;
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

    /// <summary>Get/set whether to hide the anonymous role</summary>
    public bool HideAnonymousRole { get; set; }

    /// <summary>Gets the reference to the principal multi selector.</summary>
    protected virtual MultiSelector PrincipalSelector => this.Container.GetControl<MultiSelector>("principalSelector", true);

    /// <summary>Gets the reference to the roles selector.</summary>
    protected virtual ItemSelector RolesSelector => this.PrincipalSelector.ItemSelectors["rolesSelector"];

    /// <summary>Gets the refernce to the users selector.</summary>
    protected virtual ItemSelector UsersSelector => this.PrincipalSelector.ItemSelectors["usersSelector"];

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.PrincipalSelector.HideAnonymousRole = this.HideAnonymousRole;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (SimpleUsersAndRolesSelector).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("rolesSelector", this.RolesSelector.ClientID);
      controlDescriptor.AddComponentProperty("usersSelector", this.UsersSelector.ClientID);
      controlDescriptor.AddComponentProperty("principalSelector", this.PrincipalSelector.ClientID);
      controlDescriptor.AddProperty("_wcfPrincipalType", (object) JsonUtility.EnumToJson(typeof (WcfPrincipalType)));
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
      new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.Scripts.SimpleUsersAndRolesSelector.js", typeof (SimpleUsersAndRolesSelector).Assembly.FullName)
    };
  }
}
