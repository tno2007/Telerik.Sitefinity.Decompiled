// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Designers.UsersProfilesViewDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.ControlDesign.ContentView;

namespace Telerik.Sitefinity.Security.Web.UI.Designers
{
  /// <summary>Users profiles list view designer control</summary>
  public class UsersProfilesViewDesigner : ContentViewDesignerBase
  {
    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ContentViewDesignerBase).FullName;

    /// <summary>Adds the designer views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      UsersListUserView usersListUserView = new UsersListUserView();
      CustomListSettingsDesignerView settingsDesignerView1 = new CustomListSettingsDesignerView();
      settingsDesignerView1.SortItemsText = Res.Get<UserProfilesResources>().SortUsersBy;
      settingsDesignerView1.DesignedMasterViewType = typeof (UserProfileMasterView).FullName;
      SingleItemSettingsDesignerView settingsDesignerView2 = new SingleItemSettingsDesignerView();
      settingsDesignerView2.DesignedDetailViewType = typeof (UserProfileDetailReadView).FullName;
      settingsDesignerView2.TopHeaderLabelText = Res.Get<UserProfilesResources>().OpenSingleUserProfileInDotDotDot;
      settingsDesignerView2.RenderDontDisplayAnySingleItemChoice = true;
      views.Add(usersListUserView.ViewName, (ControlDesignerView) usersListUserView);
      views.Add(settingsDesignerView1.ViewName, (ControlDesignerView) settingsDesignerView1);
      views.Add(settingsDesignerView2.ViewName, (ControlDesignerView) settingsDesignerView2);
    }
  }
}
