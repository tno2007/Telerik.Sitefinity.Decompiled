// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.LoginNameControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  /// <summary>
  /// Mimicks <see cref="T:System.Web.UI.WebControls.LoginName" />
  /// </summary>
  public class LoginNameControl : WebControl
  {
    private string formatString;
    private const string ClientIDParam = "ClientID";
    private const string TagNameParam = "TagName";
    private const string FormatStringParam = "FormatString";

    /// <summary>Initiate the Sitefinity login name control</summary>
    public LoginNameControl() => this.formatString = "{FirstName} {LastName}";

    /// <summary>
    /// Free-form formatting string, containing (in any order or not at all) the placeholders
    /// {FirstName}, {LastName} or {UserName}
    /// </summary>
    public string FormatString
    {
      get => this.formatString;
      set => this.formatString = value;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e) => this.Visible = this.GetIndexRenderMode() == IndexRenderModes.Normal;

    /// <summary>Renders the specified writer.</summary>
    /// <param name="writer">The writer.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      this.DoPostCacheSubstitution();
    }

    /// <summary>
    /// Format the currently logged in user login name like the provided formatString
    /// </summary>
    /// <param name="loginNameFormatString">the desired string format of the login name</param>
    internal static string FormatLoginName(string loginNameFormatString)
    {
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      User user = (User) null;
      if (currentUserId != Guid.Empty)
        user = SecurityManager.GetUser(currentUserId);
      if (user == null)
        return (string) null;
      if (loginNameFormatString == null)
        return user.UserName;
      string str = loginNameFormatString;
      return (!(UserProfileManager.GetManager().GetUserProfile(user.Id, typeof (SitefinityProfile).FullName) is SitefinityProfile userProfile) ? str.Replace("{FirstName}", user.FirstName).Replace("{LastName}", user.LastName) : str.Replace("{FirstName}", userProfile.FirstName).Replace("{LastName}", userProfile.LastName)).Replace("{UserName}", user.UserName);
    }

    /// <summary>
    /// When OutputCache is used on the page, we substitute it with the CacheSubstitutionWrapper
    /// </summary>
    private void DoPostCacheSubstitution() => new CacheSubstitutionWrapper(new Dictionary<string, string>()
    {
      {
        "TagName",
        this.TagName
      },
      {
        "ClientID",
        this.ClientID
      },
      {
        "FormatString",
        this.FormatString
      }
    }, new CacheSubstitutionWrapper.RenderMarkupDelegate(LoginNameControl.RenderCacheSubstitutionMarkup)).RegisterPostCacheCallBack(this.Context);

    /// <summary>
    /// This method is used by the CacheSubstitutionWrapper delegate and is used when the page
    /// uses OutputCache to render the correct markup
    /// </summary>
    internal static string RenderCacheSubstitutionMarkup(Dictionary<string, string> parameters)
    {
      if (parameters == null || !parameters.ContainsKey("FormatString"))
        return string.Empty;
      string s = LoginNameControl.FormatLoginName(parameters["FormatString"]);
      if (s == null)
        return string.Empty;
      return "<{0} id=\"{1}\">{2}</{0}>".Arrange((object) parameters["TagName"], (object) parameters["ClientID"], (object) HttpUtility.HtmlEncode(s));
    }
  }
}
