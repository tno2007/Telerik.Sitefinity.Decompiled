// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.AjaxDialogBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Base abstract class to be implemented by all dialogs that need the support for client side behavior.
  /// </summary>
  public abstract class AjaxDialogBase : DialogBase, IScriptControl
  {
    private const string ajaxDialogBaseScript = "Telerik.Sitefinity.Web.Scripts.AjaxDialogBase.js";
    private bool hostedInRadWindow = true;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public virtual string ClientComponentType => typeof (AjaxDialogBase).FullName;

    /// <summary>
    /// Spevifies whether to register global variable on the client side "dialogBase"
    /// </summary>
    public virtual bool HostedInRadWindow
    {
      get => this.hostedInRadWindow;
      set => this.hostedInRadWindow = value;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      PageManager.ConfigureScriptManager(this.Page, this.GetRequiredCoreScripts()).RegisterScriptControl<AjaxDialogBase>(this);
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the
    /// specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents
    /// the output stream to render HTML content on the client.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ClientComponentType, this.ClientID);
      controlDescriptor.AddProperty("_hostedInRadWindow", (object) this.HostedInRadWindow);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public virtual IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[2]
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.AjaxDialogBase.js", typeof (AjaxDialogBase).Assembly.FullName),
      new ScriptReference("Telerik.Web.UI.Common.Core.js", "Telerik.Web.UI")
    };

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected virtual ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.JQueryValidate | ScriptRef.TelerikSitefinity | ScriptRef.QueryString;
  }
}
