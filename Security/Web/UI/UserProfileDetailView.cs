// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserProfileDetailView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.UserProfiles.Configuration;
using Telerik.Sitefinity.Modules.UserProfiles.Web.UI;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Security.Web.UI.Contracts;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// Control used to display a single UserProfile in detail mode.
  /// </summary>
  public abstract class UserProfileDetailView : ViewBase
  {
    private IEnumerable<Control> fieldControlsToBind;
    internal string notLoggedTemplateKey;
    private string[] readOnlyFields = new string[0];
    internal bool hasReadOnlyField;

    /// <summary>Gets the profile view definition.</summary>
    public virtual IUserProfileViewDetailDefinition ProfileViewDefinition => this.Definition as IUserProfileViewDetailDefinition;

    /// <summary>
    /// Gets or sets the template key to use when loading the template for not authenticated users.
    /// </summary>
    public string NotLoggedTemplateKey
    {
      get => this.notLoggedTemplateKey.IsNullOrEmpty() && this.ProfileViewDefinition != null ? this.ProfileViewDefinition.NotLoggedTemplateKey : this.notLoggedTemplateKey;
      set => this.notLoggedTemplateKey = value;
    }

    /// <summary>Gets the field controls to bind.</summary>
    /// <value>The field controls to bind.</value>
    public virtual IEnumerable<Control> FieldControlsToBind
    {
      get
      {
        if (this.fieldControlsToBind == null)
        {
          if (this.ProfileControl != null)
          {
            if (this.ViewKind.HasValue)
              this.ProfileControl.ViewKind = this.ViewKind.Value;
            Dictionary<string, Control>.ValueCollection values = this.FieldControls.Values;
            this.fieldControlsToBind = this.ProfileControl.FieldControls.Cast<Control>();
          }
          else
            this.fieldControlsToBind = (IEnumerable<Control>) this.FieldControls.Values;
        }
        return this.fieldControlsToBind;
      }
    }

    /// <summary>Gets the view key.</summary>
    /// <value>The view key.</value>
    public virtual ProfileTypeViewKind? ViewKind => new ProfileTypeViewKind?();

    /// <summary>
    /// Gets the reference to the container of all field controls.
    /// </summary>
    protected PlaceHolder ItemContainer => this.Container.GetControl<PlaceHolder>("itemContainer", true);

    /// <summary>
    /// Gets a dictionary of field controls and their ids as keys.
    /// </summary>
    protected Dictionary<string, Control> FieldControls => this.Container.GetControls<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl>();

    /// <summary>
    /// Gets the reference to the control that displays profile fields according to a frontend detail view definition.
    /// </summary>
    protected UserProfilesControl ProfileControl => this.Container.GetControl<UserProfilesControl>("profileDataControl", false);

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
      SystemManager.SetNoCache(this.Page.Response.Cache);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      if (FormManager.GetCurrent(this.Page) == null)
        this.Controls.Add((Control) new FormManager());
      base.OnInit(e);
    }

    /// <summary>Gets or sets the template key.</summary>
    /// <value>The template key.</value>
    public override string TemplateKey
    {
      get => this.IsUserAuthenticated() ? base.TemplateKey : this.NotLoggedTemplateKey;
      set => base.TemplateKey = value;
    }

    /// <summary>Gets a url of a page by the id of its node.</summary>
    /// <param name="nodeId">The node id.</param>
    /// <returns>The url of the node or <code>null</code> if none is found</returns>
    protected virtual string GetPageUrl(Guid nodeId)
    {
      SiteMapNode siteMapNodeFromKey = SiteMapBase.GetCurrentProvider().FindSiteMapNodeFromKey(nodeId.ToString());
      return siteMapNodeFromKey != null ? RouteHelper.ResolveUrl(siteMapNodeFromKey.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) : string.Empty;
    }

    /// <summary>
    /// Binds the field controls with the values of the user profile that the host resolved.
    /// </summary>
    protected virtual void BindUserProfile()
    {
      UserProfile userProfileToBind = this.GetUserProfileToBind();
      if (userProfileToBind == null)
        return;
      this.readOnlyFields = UserManager.GetReadOnlyFields(userProfileToBind.GetType().Name, userProfileToBind.User.ExternalProviderName).ToArray<string>();
      foreach (Control control in this.GetFieldControlsToBind())
        this.BindControl(control, userProfileToBind);
    }

    /// <summary>Gets the user profile that will be bound.</summary>
    /// <returns>The user profile to bind.</returns>
    protected virtual UserProfile GetUserProfileToBind() => this.Host != null ? (UserProfile) this.Host.DetailItem : (UserProfile) null;

    /// <summary>Binds a single control.</summary>
    /// <param name="control">The control.</param>
    /// <param name="detailItem">The detail item.</param>
    protected virtual void BindControl(Control control, UserProfile detailItem)
    {
      Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl fieldControl = control as Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl;
      if (fieldControl is TaxonField taxonField)
        fieldControl.DataFieldName = taxonField.TaxonomyMetafieldName;
      if (fieldControl.DataFieldName.IsNullOrEmpty())
        return;
      User user = detailItem.User;
      string externalProviderName = user.ExternalProviderName;
      bool flag = false;
      object obj = (object) null;
      if (detailItem.DoesFieldExist(fieldControl.DataFieldName))
      {
        obj = detailItem.GetValue(fieldControl.DataFieldName);
        flag = externalProviderName != null && ((IEnumerable<string>) this.readOnlyFields).Any<string>((Func<string, bool>) (a => a.Equals(fieldControl.DataFieldName, StringComparison.OrdinalIgnoreCase)));
      }
      else if (fieldControl.DataFieldName == "sf_Email")
      {
        obj = (object) user.Email;
        fieldControl.CssClass += " sfProfileEmailMarker";
        if (externalProviderName != null)
          flag = UserManager.IsEmailMapped(externalProviderName);
        if (flag)
        {
          BaseValidator control1 = this.Container.GetControl<BaseValidator>("emailRegExp", false);
          if (control1 != null)
            control1.Visible = false;
        }
      }
      if (flag)
      {
        this.hasReadOnlyField = true;
        fieldControl.DisplayMode = FieldDisplayMode.Read;
        BaseValidator control2 = this.Container.GetControl<BaseValidator>(control.ID + "Validator", false);
        if (control2 != null)
          control2.Visible = false;
      }
      if (obj is ChoiceOption choiceOption)
        obj = (object) choiceOption.PersistedValue;
      if (obj is string html)
        obj = (object) ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(html);
      fieldControl.Value = obj;
      if (!(fieldControl is IRequiresDataItem requiresDataItem))
        return;
      requiresDataItem.DataItem = (IDataItem) detailItem;
    }

    /// <summary>Gets the field controls that will be bound.</summary>
    /// <returns>The field controls to bind</returns>
    protected virtual IEnumerable<Control> GetFieldControlsToBind()
    {
      IEnumerable<Control> fieldControlsToBind;
      if (this.ProfileControl != null)
      {
        if (this.ViewKind.HasValue)
          this.ProfileControl.ViewKind = this.ViewKind.Value;
        Dictionary<string, Control>.ValueCollection values = this.FieldControls.Values;
        fieldControlsToBind = this.ProfileControl.FieldControls.Cast<Control>();
      }
      else
        fieldControlsToBind = (IEnumerable<Control>) this.FieldControls.Values;
      return fieldControlsToBind;
    }

    /// <summary>Check if the current user is authenticated;</summary>
    /// <returns>true if the user is authenticated; otherwise false</returns>
    protected virtual bool IsUserAuthenticated()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return currentIdentity != null && currentIdentity.IsAuthenticated;
    }
  }
}
