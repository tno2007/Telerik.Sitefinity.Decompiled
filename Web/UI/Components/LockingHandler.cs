// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Components.LockingHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Components
{
  public class LockingHandler : CompositeControl, IScriptControl, ILockingControl
  {
    private Telerik.Web.UI.RadWindow lockingDialog;
    private const string scriptFileName = "Telerik.Sitefinity.Web.UI.Components.Scripts.LockingHandler.js";
    private string dialogUrl;
    private ScriptManager sm;

    /// <summary>Gets the url of the locking dialog.</summary>
    public string DialogUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.dialogUrl))
          this.dialogUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/" + typeof (LockingDialog).Name);
        return this.dialogUrl;
      }
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }

    /// <summary>Gets or sets the name of the item.</summary>
    /// <value>The name of the item.</value>
    public string ItemName { get; set; }

    /// <summary>Gets or sets the view URL.</summary>
    /// <value>The view URL.</value>
    public string ViewUrl { get; set; }

    /// <summary>Gets or sets the close URL.</summary>
    /// <value>The close URL.</value>
    public string CloseUrl { get; set; }

    /// <summary>Gets or sets the unlock URL.</summary>
    /// <value>The unlock URL.</value>
    public string UnlockUrl { get; set; }

    /// <summary>
    /// Gets or sets the GUID of the user that posseses the lock.
    /// </summary>
    /// <value>the GUID of the user that posseses the lock.</value>
    public string LockedBy { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display the Unlock button.
    /// </summary>
    /// <value><c>true</c> if Unlock button must be displayed; otherwise, <c>false</c>.</value>
    public bool ShowUnlockButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display the View button.
    /// </summary>
    /// <value><c>true</c> if View button must be displayed; otherwise, <c>false</c>.</value>
    public bool ShowViewButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display the Close button.
    /// </summary>
    /// <value><c>true</c> if Close button must be displayed; otherwise, <c>false</c>.</value>
    public bool ShowCloseButton { get; set; }

    /// <summary>Gets or sets the unlock service URL.</summary>
    /// <value>The unlock service URL.</value>
    public string UnlockServiceUrl { get; set; }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.MicrosoftAjax | ScriptRef.MicrosoftAjaxWebForms | ScriptRef.JQuery | ScriptRef.JQueryValidate | ScriptRef.TelerikSitefinity | ScriptRef.JQueryCookie);
      base.OnInit(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
      (ScriptManager.GetCurrent(this.Page) ?? throw new HttpException(Res.Get<ErrorMessages>().ScriptManagerIsNull)).RegisterScriptControl<LockingHandler>(this);
      base.OnPreRender(e);
    }

    protected override void Render(HtmlTextWriter writer)
    {
      if (this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    protected override void CreateChildControls()
    {
      Telerik.Web.UI.RadWindow child = new Telerik.Web.UI.RadWindow();
      child.Skin = "Default";
      child.Behaviors = WindowBehaviors.Close;
      child.VisibleTitlebar = true;
      child.VisibleStatusbar = false;
      child.Modal = true;
      child.ShowContentDuringLoad = false;
      child.ReloadOnShow = false;
      child.ID = "lockingScreenWindow";
      this.lockingDialog = child;
      this.Controls.Add((Control) child);
    }

    public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddProperty("dialogUrl", (object) this.DialogUrl);
      behaviorDescriptor.AddProperty("title", (object) this.Title);
      behaviorDescriptor.AddProperty("viewUrl", (object) this.ViewUrl);
      behaviorDescriptor.AddProperty("closeUrl", (object) this.CloseUrl);
      behaviorDescriptor.AddProperty("unlockUrl", (object) this.UnlockUrl);
      behaviorDescriptor.AddProperty("lockedBy", (object) this.LockedBy);
      behaviorDescriptor.AddProperty("showUnlockButton", (object) this.ShowUnlockButton);
      behaviorDescriptor.AddProperty("showViewButton", (object) this.ShowViewButton);
      behaviorDescriptor.AddProperty("showCloseButton", (object) this.ShowCloseButton);
      behaviorDescriptor.AddComponentProperty("lockingDialog", this.lockingDialog.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    public IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Components.Scripts.LockingHandler.js", this.GetType().Assembly.FullName)
    };
  }
}
