// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserActivationControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Security.Web.UI
{
  public class UserActivationControl : SimpleView
  {
    private string errorMessage;
    private string successMessage;
    public static readonly string LayoutTemplatePathConst = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserActivationControl.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? UserActivationControl.LayoutTemplatePathConst : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the message show when the activation was successful.
    /// </summary>
    /// <value>The success message.</value>
    public string SuccessMessage
    {
      get => this.successMessage.IsNullOrEmpty() ? Res.Get<ErrorMessages>().SuccessfulActivationMessage : this.successMessage;
      set => this.successMessage = value;
    }

    /// <summary>
    /// Gets or sets the message show when the activation was unsuccessful.
    /// </summary>
    /// <value>The error message.</value>
    public string ErrorMessage
    {
      get => this.errorMessage.IsNullOrEmpty() ? Res.Get<ErrorMessages>().ActivationErrorMessage : this.errorMessage;
      set => this.errorMessage = value;
    }

    /// <summary>Gets the message panel.</summary>
    /// <value>The message panel.</value>
    protected virtual Panel MessagePanel => this.Container.GetControl<Panel>("messagePanel", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container.</param>
    protected override void InitializeControls(GenericContainer container)
    {
      bool success = false;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null)
        return;
      string providerName = currentHttpContext.Request.QueryStringGet("provider");
      string g = currentHttpContext.Request.QueryStringGet("user");
      if (g.IsNullOrEmpty() || !ControlUtilities.IsGuid(g))
        return;
      Guid id = new Guid(g);
      UserManager manager = UserManager.GetManager(providerName);
      bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
      try
      {
        manager.Provider.SuppressSecurityChecks = true;
        manager.GetUser(id).IsApproved = true;
        manager.SaveChanges();
        success = true;
      }
      catch (ItemNotFoundException ex)
      {
      }
      finally
      {
        manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        if (currentHttpContext.Request.QueryString.Keys.Contains(SecurityManager.AuthenticationReturnUrl))
        {
          string url = string.Format("{0}?{1}={2}", (object) this.GetFrontEndLoginPageUrl(), (object) SecurityManager.AuthenticationReturnUrl, (object) HttpUtility.UrlEncode(currentHttpContext.Request.QueryStringGet(SecurityManager.AuthenticationReturnUrl)));
          currentHttpContext.Response.Redirect(url);
        }
        else
          this.ShowMessage(success);
      }
    }

    /// <summary>Gets the tag key.</summary>
    /// <value>The tag key.</value>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected virtual void ShowMessage(bool success)
    {
      this.MessagePanel.Visible = true;
      string str = !success ? this.ErrorMessage : this.SuccessMessage;
      this.MessagePanel.Controls.Add((Control) new Label()
      {
        Text = str
      });
    }

    private string GetFrontEndLoginPageUrl()
    {
      string relativePath = string.Empty;
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      if (currentSite.FrontEndLoginPageId != Guid.Empty)
      {
        PageNode pageNode = PageManager.GetManager().GetPageNode(currentSite.FrontEndLoginPageId);
        if (pageNode != null)
          relativePath = pageNode.GetUrl();
      }
      else if (!string.IsNullOrWhiteSpace(currentSite.FrontEndLoginPageUrl))
        relativePath = currentSite.FrontEndLoginPageUrl;
      return UrlPath.ResolveAbsoluteUrl(relativePath);
    }
  }
}
