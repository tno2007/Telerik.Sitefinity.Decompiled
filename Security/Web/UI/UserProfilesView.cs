// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserProfilesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Web.UI.Designers;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// This control acts as a container for the views that display user profiles.
  /// </summary>
  [RequireScriptManager]
  [ControlDesigner(typeof (UsersProfilesViewDesigner))]
  [PropertyEditorTitle(typeof (UserProfilesResources), "UserList")]
  public class UserProfilesView : ContentView
  {
    private UserProfileViewMode? profileViewMode;
    private bool? displayCurrentUser;
    private Collection<ItemInfoDefinition> roles;

    /// <summary>
    /// Gets or sets an array of the roles as ItemInfos that will be assigned to the newly created user.
    /// </summary>
    [TypeConverter(typeof (CollectionJsonTypeConverter<ItemInfoDefinition>))]
    public virtual Collection<ItemInfoDefinition> Roles
    {
      get
      {
        if (this.roles == null)
          this.roles = new Collection<ItemInfoDefinition>();
        return this.roles;
      }
      set => this.roles = value;
    }

    /// <summary>
    /// Gets or sets the name of the configuration definition for the whole control. From this definition
    /// control can find out all other configurations needed in order to construct views.
    /// </summary>
    /// <value>The name of the control definition.</value>
    public override string ControlDefinitionName
    {
      get => string.IsNullOrEmpty(base.ControlDefinitionName) ? "FrontendUsersList" : base.ControlDefinitionName;
      set => base.ControlDefinitionName = value;
    }

    /// <summary>
    /// Gets or sets the name of the detail view to be loaded when
    /// control is in the ContentViewDisplayMode.Detail
    /// </summary>
    /// <value></value>
    public override string DetailViewName
    {
      get => string.IsNullOrEmpty(base.DetailViewName) ? "UserProfilesFrontendDetailsRead" : base.DetailViewName;
      set => base.DetailViewName = value;
    }

    /// <summary>
    /// Gets or sets the name of the master view to be loaded when
    /// control is in the ContentViewDisplayMode.Master
    /// </summary>
    /// <value></value>
    public override string MasterViewName
    {
      get => string.IsNullOrEmpty(base.MasterViewName) ? "UserProfilesFrontendMaster" : base.MasterViewName;
      set => base.MasterViewName = value;
    }

    /// <summary>
    /// Gets or sets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public override string EmptyLinkText => Res.Get<UserProfilesResources>().EditProfilesWidgetSettings;

    /// <summary>Gets or sets the id of the user to show.</summary>
    public virtual Guid? UserId { get; set; }

    /// <summary>Gets or sets the name of the users provider to use.</summary>
    public virtual string Provider { get; set; }

    /// <summary>Gets or sets the full name of the profile type.</summary>
    public virtual string ProfileTypeFullName { get; set; }

    /// <summary>
    ///  ContentViewDisplayMode property is not working for UserProfilesView.
    /// </summary>
    protected override void ResolvePageTitle()
    {
    }
  }
}
