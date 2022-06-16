// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SitefinityScriptManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  internal class SitefinityScriptManager : Control
  {
    public List<ScriptDefinition> ScriptDefinitions = new List<ScriptDefinition>();

    public static SitefinityScriptManager GetCurrent(Page page) => page != null ? page.Items[(object) typeof (SitefinityScriptManager)] as SitefinityScriptManager : throw new ArgumentNullException(nameof (page));

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (this.DesignMode)
        return;
      if (SitefinityScriptManager.GetCurrent(this.Page) != null)
        throw new InvalidOperationException("There must be only one instance of SitefinityScriptManager per page.");
      this.Page.Items[(object) typeof (SitefinityScriptManager)] = (object) this;
    }
  }
}
