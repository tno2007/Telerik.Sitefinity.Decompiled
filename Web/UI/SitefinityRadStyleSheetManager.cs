// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SitefinityRadStyleSheetManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// This class is an inheritor of RadStyleSheetManager which uses SitefinityStyleSheetmanager to get additional style sheet definitions.
  /// </summary>
  public class SitefinityRadStyleSheetManager : RadStyleSheetManager
  {
    protected override void OnInit(EventArgs e)
    {
      this.Page.PreRenderComplete += new EventHandler(this.Page_PreRenderComplete);
      base.OnInit(e);
    }

    private void Page_PreRenderComplete(object sender, EventArgs e) => SitefinityStyleSheetManager.GetCurrent(this.Page)?.ProcessAdditionalStyleSheets((RadStyleSheetManager) this);
  }
}
