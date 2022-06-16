// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Components.ILockingControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Components
{
  internal interface ILockingControl
  {
    string CloseUrl { get; set; }

    string UnlockUrl { get; set; }

    string ViewUrl { get; set; }

    string UnlockServiceUrl { get; set; }

    bool ShowCloseButton { get; set; }

    bool ShowUnlockButton { get; set; }

    bool ShowViewButton { get; set; }

    string ItemName { get; set; }

    string LockedBy { get; set; }

    string Title { get; set; }
  }
}
