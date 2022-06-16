// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsCountControlBinder
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

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend
{
  /// <summary>
  /// This cotnrol is used for setting the values of all CommentsCounterControls on the page
  /// </summary>
  internal class CommentsCountControlBinder : WebControl, IScriptControl
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
      if (this.Page.Items.Contains((object) typeof (CommentsCountControlBinder).FullName))
        return (IEnumerable<ScriptDescriptor>) null;
      this.Page.Items[(object) typeof (CommentsCountControlBinder).FullName] = (object) this;
      ScriptComponentDescriptor componentDescriptor = new ScriptComponentDescriptor("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsCountControlBinder");
      string str = this.Page.ResolveUrl("~/RestApi/comments-api");
      componentDescriptor.AddProperty("serviceUrl", (object) str);
      componentDescriptor.AddProperty("ninetyNinePlusText", (object) Res.Get<CommentsResources>().NinetyNinePlus);
      componentDescriptor.AddProperty("commentsText", (object) Res.Get<CommentsResources>().CommentsPluralTypeName);
      componentDescriptor.AddProperty("commentText", (object) Res.Get<CommentsResources>().CommentsSingleTypeName);
      componentDescriptor.AddProperty("leaveCommentText", (object) Res.Get<CommentsResources>().LeaveComment);
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
          Name = "Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsCountControlBinder.js"
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
        PageManager.ConfigureScriptManager(this.Page, ScriptRef.JQuery | ScriptRef.TelerikSitefinity);
        this.sm.RegisterScriptControl<CommentsCountControlBinder>(this);
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
