// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.BackendMultisiteSessionControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  internal class BackendMultisiteSessionControl : Control, IScriptControl
  {
    internal const string ScriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.BackendMultisiteSessionControl.js";

    public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptComponentDescriptor componentDescriptor = new ScriptComponentDescriptor("Telerik.Sitefinity.Multisite.Web.UI.BackendMultisiteSessionControl");
      componentDescriptor.AddProperty("selectedSite", (object) SystemManager.CurrentContext.CurrentSite.Id.ToString());
      componentDescriptor.AddProperty("siteIdParamKey", (object) "sf_site");
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) componentDescriptor
      };
    }

    public IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>()
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Multisite.Web.UI.Scripts.BackendMultisiteSessionControl.js", typeof (BackendMultisiteSessionControl).Assembly.FullName)
    };

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      (ScriptManager.GetCurrent(this.Page) ?? throw new HttpException(Res.Get<ErrorMessages>().ScriptManagerIsNull)).RegisterScriptControl<BackendMultisiteSessionControl>(this);
      base.OnPreRender(e);
    }

    /// <summary>Renders the control to the specified HTML writer.</summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }
  }
}
