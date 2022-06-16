// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.LoginFailedPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// The control panel used by the view for log-in failed message.
  /// </summary>
  public class LoginFailedPanel : ControlPanel<LoginFailedPanel>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.LoginFailedPanel" /> class.
    /// </summary>
    public LoginFailedPanel()
      : base(false)
    {
      this.Title = Res.Get<Labels>().Sitefinity;
    }

    /// <summary>Creates the views.</summary>
    protected override void CreateViews() => this.AddView<LoginFailedView>();
  }
}
