// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ClientBinders
{
  /// <summary>
  ///  The mediator maintains list of pairs and transfer their client ids to the client.
  ///  At the client actual connection between sender and receiver events is performed.
  /// </summary>
  [RequireScriptManager]
  [ToolboxBitmap(typeof (ResourceLinks), "Telerik.Sitefinity.Resources.Sitefinity.bmp")]
  [ParseChildren(typeof (List<SenderReceiverPair>), ChildrenAsProperties = true, DefaultProperty = "AsyncPairs")]
  public class AsyncCommandMediator : ScriptControl
  {
    private List<SenderReceiverPair> asyncPairs;
    internal const string script = "Telerik.Sitefinity.Web.Scripts.AsyncCommandMediator.js";

    /// <summary>
    ///  Sets the sender - receiver pairs that will communicate through the mediator
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual List<SenderReceiverPair> AsyncPairs
    {
      get
      {
        if (this.asyncPairs == null)
          this.asyncPairs = new List<SenderReceiverPair>();
        return this.asyncPairs;
      }
    }

    /// <summary>
    /// Gets the current AsyncCommandMediator from the given page
    /// </summary>
    public static AsyncCommandMediator GetCurrent(Page page) => page != null ? page.Items[(object) typeof (AsyncCommandMediator)] as AsyncCommandMediator : throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);

    /// <summary>
    /// Checks if there is only one instance per page and initializes the scripts
    /// </summary>
    internal virtual void Initialize()
    {
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      if (this.DesignMode)
        return;
      if (AsyncCommandMediator.GetCurrent(this.Page) != null)
        throw new InvalidOperationException("Only one instance of AsyncCommandMediator is allowed per page. Please, use AsyncCommandMediator.GetCurrent to access the mediator.");
      this.Page.Items[(object) typeof (AsyncCommandMediator)] = (object) this;
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.MicrosoftAjax | ScriptRef.JQuery | ScriptRef.TelerikSitefinity);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      this.Initialize();
      base.OnInit(e);
    }

    protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptComponentDescriptor componentDescriptor = new ScriptComponentDescriptor(typeof (AsyncCommandMediator).FullName);
      componentDescriptor.AddProperty("asyncCommandPairs", (object) this.AsyncPairs);
      return (IEnumerable<ScriptDescriptor>) new ScriptComponentDescriptor[1]
      {
        componentDescriptor
      };
    }

    protected override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.AsyncCommandMediator.js", this.GetType().Assembly.GetName().ToString())
    };
  }
}
