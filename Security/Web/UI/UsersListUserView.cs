// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UsersListUserView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Modules.UserProfiles.Web.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Security.Principals;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// Represents view used in the first tab of the Users list control designer.
  /// </summary>
  public class UsersListUserView : ContentViewDesignerView
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.UsersListUserView" /> class.
    /// </summary>
    public UsersListUserView() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.UserProfiles.UsersListUserView.ascx");

    public override string ViewName => "usersListUserViewSettings";

    public override string ViewTitle => "Users";

    /// <summary>
    /// Gets a reference to the OneUserOnlySelectButtonPanel span panel.
    /// </summary>
    public HtmlGenericControl OneUserOnlySelectButtonPanel => this.Container.GetControl<HtmlGenericControl>("oneUserOnlySelectButtonPanel", true);

    /// <summary>Gets a reference to the RolesPanel div panel.</summary>
    public HtmlGenericControl RolesPanel => this.Container.GetControl<HtmlGenericControl>("rolesPanel", true);

    public ChoiceField ProfileTypeSelector => this.Container.GetControl<ChoiceField>("profileTypeSelector", true);

    /// <summary>Gets a reference to the UserSelectorWrapper div tag.</summary>
    public HtmlGenericControl UserSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("userSelectorWrapper", true);

    /// <summary>Gets a reference to the RoleSelectorWrapper div tag.</summary>
    public HtmlGenericControl RoleSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("roleSelectorWrapper", true);

    /// <summary>Gets a reference to the SelectUserButton LinkButton.</summary>
    public LinkButton SelectUserButton => this.Container.GetControl<LinkButton>("selectUserButton", true);

    /// <summary>
    /// Gets a reference to the SelectUserButtonLiteral Literal.
    /// </summary>
    public Literal SelectUserButtonLiteral => this.Container.GetControl<Literal>("selectUserButtonLiteral", true);

    /// <summary>Gets a reference to the SelectRolesButton LinkButton.</summary>
    public LinkButton SelectRolesButton => this.Container.GetControl<LinkButton>("selectRolesButton", true);

    /// <summary>
    /// Gets a reference to the SelectUserButtonLiteral Literal.
    /// </summary>
    public Literal SelectRolesButtonLiteral => this.Container.GetControl<Literal>("selectRolesButtonLiteral", true);

    /// <summary>Gets a reference to the user selector.</summary>
    protected UserSelector UserSelector => this.Container.GetControl<UserSelector>("userSelector", true);

    /// <summary>Gets a reference to the role selector.</summary>
    protected RoleSelector RoleSelector => this.Container.GetControl<RoleSelector>("roleSelector", true);

    /// <summary>
    /// Gets a reference to the client label manager in the control.
    /// </summary>
    /// <value>The client label manager.</value>
    private ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.BindUserProfilesTypes();
      this.OneUserOnlySelectButtonPanel.Style["display"] = "none";
      this.RolesPanel.Style["display"] = "none";
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => this.DesignerTemplateName;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void BindUserProfilesTypes()
    {
      foreach (UserProfileTypeViewModel userProfileType in (IEnumerable<UserProfileTypeViewModel>) UserProfilesHelper.GetUserProfileTypes((string) null))
        this.ProfileTypeSelector.Choices.Add(new ChoiceItem()
        {
          Text = userProfileType.Title,
          Value = userProfileType.DynamicTypeName
        });
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (FieldControl fieldControl in this.Container.GetControls<FieldControl>().Values)
      {
        if (!string.IsNullOrEmpty(fieldControl.DataFieldName))
          dictionary.Add(fieldControl.DataFieldName, fieldControl.ClientID);
      }
      controlDescriptor.AddElementProperty("oneUserOnlySelectButtonPanel", this.OneUserOnlySelectButtonPanel.ClientID);
      controlDescriptor.AddElementProperty("rolesPanel", this.RolesPanel.ClientID);
      controlDescriptor.AddElementProperty("userSelectorWrapper", this.UserSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("roleSelectorWrapper", this.RoleSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("selectUserButton", this.SelectUserButton.ClientID);
      controlDescriptor.AddElementProperty("selectUserButtonLiteral", this.SelectUserButtonLiteral.ClientID);
      controlDescriptor.AddElementProperty("selectRolesButton", this.SelectRolesButton.ClientID);
      controlDescriptor.AddElementProperty("selectRolesButtonLiteral", this.SelectRolesButtonLiteral.ClientID);
      controlDescriptor.AddComponentProperty("profileTypeSelector", this.ProfileTypeSelector.ClientID);
      controlDescriptor.AddComponentProperty("userSelector", this.UserSelector.ClientID);
      controlDescriptor.AddComponentProperty("roleSelector", this.RoleSelector.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Scripts.UsersListUserView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
