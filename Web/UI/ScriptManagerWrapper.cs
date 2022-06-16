// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ScriptManagerWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI.PublicControls.InlineEditing;

namespace Telerik.Sitefinity.Web.UI
{
  public class ScriptManagerWrapper : Control
  {
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if ((ControlExtensions.IsBackend() || !this.Published ? 0 : (ControlExtensions.InlineEditingIsEnabled() ? 1 : 0)) == 0)
        return;
      if (ScriptManager.GetCurrent(this.Page) == null)
        RouteHandler.EnsureScriptManager(this.Page);
      this.Page.InitComplete += new EventHandler(this.Page_InitComplete);
    }

    private void Page_InitComplete(object sender, EventArgs e)
    {
      if (this.Page.Form == null || SiteMapBase.GetActualCurrentNode().Framework == PageTemplateFramework.Mvc)
        return;
      this.Page.Form.Controls.Add((Control) new InlineEditingManager(this.PageId, this.PageVersion, this.PageStatus));
    }

    public Guid PageId { get; set; }

    public int PageVersion { get; set; }

    public ContentLifecycleStatus PageStatus { get; set; }

    public bool Published { get; set; }

    [TypeConverter(typeof (StringArrayConverter))]
    public string[] ControlIDs { get; set; }
  }
}
