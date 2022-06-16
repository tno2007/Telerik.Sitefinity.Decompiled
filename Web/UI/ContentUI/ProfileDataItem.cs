// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ProfileDataItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  public class ProfileDataItem
  {
    private User user;

    public User User
    {
      get => this.user;
      set => this.user = value;
    }

    public UserProfile Profile { get; set; }
  }
}
