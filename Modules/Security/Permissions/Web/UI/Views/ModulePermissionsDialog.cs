// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.ModulePermissionsDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Security.Permissions;

namespace Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views
{
  public class ModulePermissionsDialog : AjaxDialogBase
  {
    public static readonly string UiPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Permissions.ModulePermissionsDialog.ascx");

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (SystemManager.CurrentHttpContext == null)
        return;
      NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
      string str1 = HttpUtility.HtmlDecode(queryString["title"]);
      this.hItemDataType.Value = queryString["typeName"];
      this.hGlobalTitle.Value = str1;
      this.PermissionsList.Title = str1;
      if (!string.IsNullOrEmpty(queryString["permissionSetName"]))
        this.hGlobalPermissionSetName.Value = queryString["permissionSetName"];
      if (!string.IsNullOrEmpty(queryString["relatedSecuredObjects"]))
      {
        this.RelatedSecuredObjects.Value = queryString["relatedSecuredObjects"];
        this.RelatedPermissionsRepeater.DataSource = (object) queryString["relatedSecuredObjects"].Split(',');
        this.RelatedPermissionsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ReleatedPermissionsDataBound);
        this.RelatedPermissionsRepeater.DataBind();
      }
      if (!string.IsNullOrEmpty(queryString["relatedSecuredObjectTypeName"]))
        this.RelatedSecuredObjectsTypeName.Value = queryString["relatedSecuredObjectTypeName"];
      if (!string.IsNullOrEmpty(queryString["moduleName"]))
      {
        this.PermissionsList.ModuleName = queryString["moduleName"];
        this.hGlobalModule.Value = queryString["moduleName"];
      }
      string str2 = queryString["selectedProviderName"];
      if (!string.IsNullOrEmpty(str2))
        this.PermissionsList.SelectedProviderName = str2;
      string str3 = queryString["dataProviderName"];
      if (!string.IsNullOrEmpty(str3))
        this.PermissionsList.DataProviderName = str3;
      if (!string.IsNullOrEmpty(queryString["securedObjectID"]))
      {
        this.PermissionsList.SecuredObjectID = new Guid(queryString["securedObjectID"]);
        this.hGlobalSecuredObjectId.Value = queryString["securedObjectID"];
      }
      if (!string.IsNullOrEmpty(queryString["managerClassName"]))
      {
        this.PermissionsList.ManagerClassName = queryString["managerClassName"];
        this.hGlobalManagerClassName.Value = queryString["managerClassName"];
      }
      if (!string.IsNullOrEmpty(queryString["securedObjectTypeName"]))
      {
        this.PermissionsList.SecuredObjectTypeName = queryString["securedObjectTypeName"];
        this.hGlobalSecuredObjectTypeName.Value = queryString["securedObjectTypeName"];
      }
      else
        this.PermissionsList.SecuredObjectTypeName = WcfHelper.DecodeWcfString(queryString["typeName"]);
      if (!string.IsNullOrEmpty(queryString["showPermissionSetNameTitle"]))
        this.PermissionsList.ShowPermissionSetNameTitle = bool.Parse(queryString["showPermissionSetNameTitle"]);
      if (!string.IsNullOrEmpty(queryString["backLabelText"]))
      {
        string str4 = queryString["backLabelText"];
        this.BackLinkLabel.Text = str4;
        this.CancelLabel.Text = str4;
      }
      if (!string.IsNullOrEmpty(queryString["backLabelLink"]))
      {
        string redirectUri = queryString["backLabelLink"];
        string buttonLink = "#";
        if (ObjectFactory.Resolve<IRedirectUriValidator>().IsValid(redirectUri))
          buttonLink = redirectUri;
        this.ConfigureButtonLink((HtmlAnchor) this.BackLinkLabel.Parent, buttonLink);
        this.ConfigureButtonLink((HtmlAnchor) this.CancelLabel.Parent, buttonLink);
      }
      if (!string.IsNullOrEmpty(queryString["RequireSystemProviders"]))
        this.PermissionsList.RequireSystemProviders = bool.Parse(queryString["RequireSystemProviders"]);
      this.PermissionsList.PermissionsSetName = queryString["permissionSetName"];
      this.PermissionsList.BindOnLoad = true;
    }

    protected void ReleatedPermissionsDataBound(object Sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      ItemActionPermissionsList control = (ItemActionPermissionsList) e.Item.FindControl("relatedPermissionsList");
      string[] strArray = ((string) e.Item.DataItem).Split(':');
      string g = strArray[0];
      string str = strArray[1];
      control.PermissionsSetName = str;
      control.SecuredObjectID = new Guid(g);
      control.BindOnLoad = true;
      if (this.RelatedPermissionsList.Value == string.Empty)
      {
        this.RelatedPermissionsList.Value = control.ClientID;
      }
      else
      {
        HiddenField relatedPermissionsList = this.RelatedPermissionsList;
        relatedPermissionsList.Value = relatedPermissionsList.Value + "," + control.ClientID;
      }
    }

    internal HiddenField RelatedPermissionsList => this.Container.GetControl<HiddenField>("hRelatesPermissionsLists", true);

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ModulePermissionsDialog.UiPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected virtual ItemActionPermissionsList PermissionsList => this.Container.GetControl<ItemActionPermissionsList>("permissionsList", true);

    protected virtual Repeater RelatedPermissionsRepeater => this.Container.GetControl<Repeater>("relatedPermissionsRepeater", true);

    protected virtual HiddenField RelatedSecuredObjects => this.Container.GetControl<HiddenField>("hRelatedSecuredObjects", true);

    protected virtual HiddenField RelatedSecuredObjectsTypeName => this.Container.GetControl<HiddenField>("hRelatedSecuredObjectsType", true);

    protected virtual HiddenField hItemDataType => this.Container.GetControl<HiddenField>(nameof (hItemDataType), true);

    protected virtual HiddenField hGlobalTitle => this.Container.GetControl<HiddenField>(nameof (hGlobalTitle), true);

    protected virtual HiddenField hGlobalModule => this.Container.GetControl<HiddenField>(nameof (hGlobalModule), true);

    protected virtual HiddenField hGlobalManagerClassName => this.Container.GetControl<HiddenField>(nameof (hGlobalManagerClassName), true);

    protected virtual HiddenField hGlobalSecuredObjectId => this.Container.GetControl<HiddenField>(nameof (hGlobalSecuredObjectId), true);

    protected virtual HiddenField hGlobalPermissionSetName => this.Container.GetControl<HiddenField>(nameof (hGlobalPermissionSetName), true);

    protected virtual HiddenField hGlobalSecuredObjectTypeName => this.Container.GetControl<HiddenField>(nameof (hGlobalSecuredObjectTypeName), true);

    protected virtual Literal BackLinkLabel => this.Container.GetControl<Literal>("lblBackLinkText", true);

    protected virtual Literal CancelLabel => this.Container.GetControl<Literal>("lblCancelText", true);

    private void ConfigureButtonLink(HtmlAnchor button, string buttonLink)
    {
      button.HRef = buttonLink;
      button.Attributes.Remove("onclick");
    }
  }
}
