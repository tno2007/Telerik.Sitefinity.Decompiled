// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserProfileMasterView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI.Contracts;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// Represents master view that displays a collection news items as list.
  /// </summary>
  [ControlTemplateInfo("UserProfilesResources", "UserList", "Users")]
  public class UserProfileMasterView : ViewBase
  {
    private UserProfileManager manager;
    internal const string templateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileMasterView.ascx";
    internal const string templateNameNamesOnly = "Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileMasterViewNamesOnly.ascx";
    public static readonly string templatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileMasterView.ascx");
    public static readonly string templatePathNamesOnly = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileMasterViewNamesOnly.ascx");

    /// <inheritdoc />
    protected override string DefaultLayoutTemplatePath => UserProfileMasterView.templatePath;

    /// <summary>
    /// Gets the a configured instance of the UserProfileManager.
    /// </summary>
    /// <value>The manager.</value>
    protected virtual UserProfileManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = UserProfileManager.GetManager(this.Host.ControlDefinition.ProviderName);
        return this.manager;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to render links to detail view in the master view.
    /// </summary>
    public bool RenderLinksInMasterView { get; set; }

    /// <summary>Gets the repeater for news list.</summary>
    /// <value>The repeater.</value>
    protected internal virtual RadListView UserProfilesList => this.Container.GetControl<RadListView>(nameof (UserProfilesList), true);

    /// <summary>Gets the pager.</summary>
    /// <value>The pager.</value>
    protected virtual Pager Pager => this.Container.GetControl<Pager>("pager", true);

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      this.AddAttributesToRender(writer);
      writer.RenderBeginTag(HtmlTextWriterTag.Div);
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      base.RenderEndTag(writer);
    }

    /// <summary>
    /// Displays an error message when an exception is thrown during the rendering of the control.
    /// </summary>
    /// <param name="ex">The thrown exception.</param>
    protected override void ProcessInitializationException(Exception ex)
    {
      if (!this.IsBackend())
        return;
      this.Controls.Add((Control) new Label()
      {
        Text = Res.Get<UserProfilesResources>().TheSelectedTemplateCannotBeUsed
      });
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      if (!(definition is IUserProfileViewMasterDefinition masterDefinition))
        return;
      int? totalCount = new int?(0);
      IQueryable itemsList = this.GetItemsList(masterDefinition, ref totalCount);
      this.InitializeListView(masterDefinition, itemsList, ref totalCount);
    }

    /// <summary>
    /// Initializes the list view control with the items specified in the query.
    /// </summary>
    /// <param name="query">The query.</param>
    protected virtual void InitializeListView(
      IUserProfileViewMasterDefinition masterDefinition,
      IQueryable userProfiles,
      ref int? totalCount)
    {
      int? nullable = totalCount;
      int num = 0;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      {
        this.UserProfilesList.Visible = false;
      }
      else
      {
        this.ConfigurePager(totalCount.Value, (IContentViewMasterDefinition) masterDefinition);
        this.UserProfilesList.DataSource = (object) userProfiles;
        this.UserProfilesList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(this.UserProfilesList_ItemDataBound);
      }
    }

    /// <summary>Gets the items list.</summary>
    /// <returns></returns>
    protected virtual IQueryable GetItemsList(
      IUserProfileViewMasterDefinition masterDefinition,
      ref int? totalCount)
    {
      if (masterDefinition.RenderLinksInMasterView.HasValue)
        this.RenderLinksInMasterView = masterDefinition.RenderLinksInMasterView.Value;
      bool? allowPaging;
      if (masterDefinition.AllowPaging.HasValue)
      {
        RadListView userProfilesList = this.UserProfilesList;
        allowPaging = masterDefinition.AllowPaging;
        int num = allowPaging.Value ? 1 : 0;
        userProfilesList.AllowPaging = num != 0;
      }
      int? itemsPerPage;
      if (masterDefinition.ItemsPerPage.HasValue && masterDefinition.ItemsPerPage.Value != 0)
      {
        this.UserProfilesList.AllowPaging = true;
        RadListView userProfilesList = this.UserProfilesList;
        itemsPerPage = masterDefinition.ItemsPerPage;
        int num = itemsPerPage.Value;
        userProfilesList.PageSize = num;
      }
      Type profileType = TypeResolutionService.ResolveType(masterDefinition.ProfileTypeFullName);
      IQueryable itemsList = (IQueryable) null;
      int? nullable = new int?(0);
      allowPaging = masterDefinition.AllowPaging;
      if (allowPaging.HasValue)
      {
        allowPaging = masterDefinition.AllowPaging;
        if (allowPaging.Value)
          nullable = new int?(this.GetItemsToSkipCount(masterDefinition.ItemsPerPage, this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix));
      }
      if (masterDefinition.UsersDisplayMode.HasValue)
      {
        switch (masterDefinition.UsersDisplayMode.Value)
        {
          case UsersDisplayMode.All:
            IQueryable query1 = this.GetQuery(profileType);
            string filterExpression = masterDefinition.FilterExpression;
            string sortExpression1 = masterDefinition.SortExpression;
            int? skip1 = new int?(nullable.Value);
            itemsPerPage = masterDefinition.ItemsPerPage;
            int? take1 = new int?(itemsPerPage.Value);
            ref int? local1 = ref totalCount;
            itemsList = DataProviderBase.SetExpressions(query1, filterExpression, sortExpression1, skip1, take1, ref local1).AsQueryable();
            break;
          case UsersDisplayMode.Specific:
            if (!masterDefinition.UserId.HasValue)
              throw new ArgumentNullException("UserId");
            UserProfile userProfile1 = this.Manager.GetUserProfile(UserManager.GetManager(masterDefinition.Provider).GetUser(masterDefinition.UserId.Value), profileType);
            itemsList = DataProviderBase.SetExpressions(this.GetQuery(profileType), "Id == \"" + (object) userProfile1.Id + "\"", (string) null, new int?(0), new int?(0), ref totalCount).AsQueryable();
            break;
          case UsersDisplayMode.FromRoles:
            UserProfilesView host = this.Host as UserProfilesView;
            Collection<ItemInfoDefinition> collection = (Collection<ItemInfoDefinition>) null;
            if (host != null)
              collection = host.Roles;
            if (collection != null)
            {
              List<User> userList = new List<User>();
              foreach (ItemInfoDefinition itemInfoDefinition in collection)
              {
                IList<User> usersInRole = RoleManager.GetManager(itemInfoDefinition.ProviderName).GetUsersInRole(itemInfoDefinition.ItemId);
                userList.AddRange((IEnumerable<User>) usersInRole);
              }
              string[] array = UserManager.GetManager().GetContextProviders().Select<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Name)).ToArray<string>();
              List<Guid> guidList = new List<Guid>();
              foreach (User user in userList)
              {
                if (((IEnumerable<string>) array).Contains<string>(user.GetProviderName()))
                {
                  UserProfile userProfile2 = this.Manager.GetUserProfile(user, masterDefinition.ProfileTypeFullName);
                  if (userProfile2 != null && !guidList.Contains(userProfile2.Id))
                    guidList.Add(userProfile2.Id);
                }
              }
              StringBuilder stringBuilder = new StringBuilder();
              for (int index = 0; index < guidList.Count; ++index)
              {
                stringBuilder.AppendFormat("Id == \"{0}\"", (object) guidList[index]);
                if (index + 1 < guidList.Count)
                  stringBuilder.Append(" || ");
              }
              if (stringBuilder.Length == 0)
                stringBuilder.AppendFormat("Id == \"{0}\"", (object) Guid.Empty);
              itemsList = DataProviderBase.SetExpressions(this.GetQuery(profileType), stringBuilder.ToString(), masterDefinition.SortExpression, new int?(nullable.Value), new int?(masterDefinition.ItemsPerPage.Value), ref totalCount).AsQueryable();
              break;
            }
            IQueryable query2 = this.GetQuery(profileType);
            string sortExpression2 = masterDefinition.SortExpression;
            int? skip2 = new int?(nullable.Value);
            itemsPerPage = masterDefinition.ItemsPerPage;
            int? take2 = new int?(itemsPerPage.Value);
            ref int? local2 = ref totalCount;
            itemsList = DataProviderBase.SetExpressions(query2, (string) null, sortExpression2, skip2, take2, ref local2).AsQueryable();
            break;
        }
      }
      return itemsList;
    }

    /// <summary>Configures the pager.</summary>
    /// <param name="vrtualItemCount">The vrtual item count.</param>
    /// <param name="masterDefinition">The master definition.</param>
    protected virtual void ConfigurePager(
      int vrtualItemCount,
      IContentViewMasterDefinition masterDefinition)
    {
      if (masterDefinition.AllowPaging.HasValue && masterDefinition.AllowPaging.Value && masterDefinition.ItemsPerPage.GetValueOrDefault() > 0)
      {
        this.Pager.VirtualItemCount = vrtualItemCount;
        this.Pager.PageSize = masterDefinition.ItemsPerPage.Value;
        this.Pager.QueryParamKey = this.Host.UrlKeyPrefix;
      }
      else
        this.Pager.Visible = false;
    }

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.Empty;

    private IQueryable GetQuery(Type profileType)
    {
      IQueryable source = this.Manager.Provider.GetItems(profileType, (string) null, (string) null, 0, 0).AsQueryable();
      string[] userProviders = UserManager.GetManager().GetContextProviders().Select<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Name)).ToArray<string>();
      return (IQueryable) Queryable.Cast<UserProfile>(source).Where<UserProfile>((Expression<Func<UserProfile, bool>>) (p => p.UserLinks.Any<UserProfileLink>((Func<UserProfileLink, bool>) (l => userProviders.Contains<string>(l.MembershipManagerInfo.ProviderName)))));
    }

    /// <summary>
    /// Handles the ItemDataBound event for each item in UserProfilesList.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UserProfilesList_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      if (this.RenderLinksInMasterView)
        return;
      e.Item.FindControl("DetailsViewHyperLink1").Visible = false;
      e.Item.FindControl("DetailsViewText1").Visible = true;
      e.Item.FindControl("DetailsViewText2").Visible = true;
    }
  }
}
