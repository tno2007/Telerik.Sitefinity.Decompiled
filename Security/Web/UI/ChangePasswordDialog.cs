// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.ChangePasswordDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Security.Web.UI
{
  public class ChangePasswordDialog : AjaxDialogBase
  {
    private const string usersServiceUrl = "~/Sitefinity/Services/Security/MembershipSettings.svc";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.ChangePassword.ascx");

    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ChangePasswordDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override void InitializeControls(GenericContainer container)
    {
      this.ServiceUrl.Value = this.ResolveClientUrl("~/Sitefinity/Services/Security/MembershipSettings.svc");
      this.CreatePasswordHints();
    }

    private void CreatePasswordHints()
    {
      using (UserManager userManager = new UserManager())
      {
        if (userManager.MinRequiredPasswordLength > 0)
        {
          HtmlGenericControl child = new HtmlGenericControl("p");
          child.Attributes.Add("class", "sfExample");
          child.InnerHtml = SecurityUtility.GetPasswordLengthHint(userManager.MinRequiredPasswordLength);
          this.PasswordHints.Controls.Add((Control) child);
        }
        if (userManager.MinRequiredNonAlphanumericCharacters <= 0)
          return;
        HtmlGenericControl child1 = new HtmlGenericControl("p");
        child1.Attributes.Add("class", "sfExample");
        child1.InnerHtml = SecurityUtility.GetPasswordAlphaNumCharactersHint(userManager.MinRequiredNonAlphanumericCharacters);
        this.PasswordHints.Controls.Add((Control) child1);
      }
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    public HiddenField ServiceUrl => this.Container.GetControl<HiddenField>("usersServiceUrl", true);

    public HtmlGenericControl PasswordHints => this.Container.GetControl<HtmlGenericControl>("passwordHints", true);
  }
}
