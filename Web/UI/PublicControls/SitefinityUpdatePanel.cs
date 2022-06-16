// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.SitefinityUpdatePanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  public class SitefinityUpdatePanel : UpdatePanel
  {
    protected override void OnInit(EventArgs e) => this.Page.InitComplete += new EventHandler(this.Page_InitComplete);

    protected override void OnUnload(EventArgs e)
    {
      if (this.IsBackend())
        return;
      base.OnUnload(e);
    }

    private void Page_InitComplete(object sender, EventArgs e) => base.OnInit(e);
  }
}
