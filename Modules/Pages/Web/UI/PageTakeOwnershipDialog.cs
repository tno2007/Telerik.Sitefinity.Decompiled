// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PageTakeOwnershipDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  public class PageTakeOwnershipDialog : AjaxDialogBase
  {
    /// <summary>Path of the layout template for the dialog</summary>
    public static readonly string TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.PageTakeOwnershipDialog.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.PageTakeOwnershipDialog" /> class.
    /// </summary>
    public PageTakeOwnershipDialog() => this.LayoutTemplatePath = PageTakeOwnershipDialog.TemplatePath;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      Guid userId = new Guid(this.Page.Request.QueryString["LockedBy"]);
      string str = this.Page.Request.QueryString["ViewUrl"].ToString();
      container.GetControl<HtmlAnchor>("viewUrl", true).Attributes["href"] = str;
      container.GetControl<HiddenField>("hfEditorsName", true).Value = UserProfilesHelper.GetUserDisplayName(userId);
    }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;
  }
}
