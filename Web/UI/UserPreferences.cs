// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.UserPreferences
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Web.UI
{
  public class UserPreferences : WebControl, IScriptControl
  {
    private ScriptManager sm;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      if (this.Page.Items.Contains((object) typeof (UserPreferences).FullName))
        return (IEnumerable<ScriptDescriptor>) null;
      this.Page.Items[(object) typeof (UserPreferences).FullName] = (object) this;
      ScriptComponentDescriptor componentDescriptor = new ScriptComponentDescriptor("Telerik.Sitefinity.Web.UI.UserPreferences");
      TimeZoneInfo userTimeZone = UserManager.GetManager().GetUserTimeZone();
      TimeSpan baseUtcOffset = userTimeZone.BaseUtcOffset;
      if (userTimeZone.IsDaylightSavingTime(DateTime.Now))
      {
        foreach (TimeSpan timeSpan in ((IEnumerable<TimeZoneInfo.AdjustmentRule>) userTimeZone.GetAdjustmentRules()).Select<TimeZoneInfo.AdjustmentRule, TimeSpan>((Func<TimeZoneInfo.AdjustmentRule, TimeSpan>) (r => r.DaylightDelta)))
          baseUtcOffset += timeSpan;
      }
      componentDescriptor.AddProperty("_userBrowserSettingsForCalculatingDates", (object) UserManager.GetManager().GetUserBrowserSettingsForCalculatingDates());
      componentDescriptor.AddProperty("_timeOffset", (object) baseUtcOffset.TotalMilliseconds.ToString());
      componentDescriptor.AddProperty("timeZoneDisplayName", (object) userTimeZone.DisplayName);
      componentDescriptor.AddProperty("timeZoneId", (object) userTimeZone.Id);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) componentDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public IEnumerable<ScriptReference> GetScriptReferences()
    {
      string str = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new ScriptReference[1]
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.UserPreferences.js"
        }
      };
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      this.sm = ScriptManager.GetCurrent(this.Page);
      if (!this.DesignMode)
      {
        if (this.sm == null)
          throw new HttpException("A ScriptManager control must exist on the current page.");
        this.sm.RegisterScriptControl<UserPreferences>(this);
      }
      base.OnPreRender(e);
    }

    /// <summary>Renders the control to the specified HTML writer.</summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode)
        this.sm.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }
  }
}
