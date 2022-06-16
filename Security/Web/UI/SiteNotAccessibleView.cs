﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.SiteNotAccessibleView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Site not accessible view</summary>
  public class SiteNotAccessibleView : ViewBase
  {
    private static readonly string LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.SiteNotAccessibleView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.SiteNotAccessibleView" /> class.
    /// </summary>
    public SiteNotAccessibleView() => this.LayoutTemplatePath = SiteNotAccessibleView.LayoutTemplatePath;

    /// <summary>Gets a reference to the message label.</summary>
    protected Label Message => this.Container.GetControl<Label>("message", false);

    /// <summary>Gets a reference to the log out button.</summary>
    protected HyperLink LogoutLink => this.Container.GetControl<HyperLink>("logoutLink", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      if (this.Message != null)
      {
        string name = SystemManager.CurrentContext.MultisiteContext.CurrentSite.Name;
        this.Message.Text = Res.Get<Labels>().SiteNotAccessible.Arrange((object) name);
      }
      if (this.LogoutLink == null)
        return;
      this.LogoutLink.NavigateUrl = ClaimsManager.GetLogoutUrl();
    }

    /// <summary>Gets the tag key</summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
