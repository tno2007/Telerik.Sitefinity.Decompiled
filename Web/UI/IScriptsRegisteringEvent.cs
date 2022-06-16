// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IScriptsRegisteringEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Event raised while the control loads its scripts.</summary>
  public interface IScriptsRegisteringEvent : IEvent
  {
    /// <summary>Gets or sets the sender.</summary>
    /// <value>The sender.</value>
    IScriptControl Sender { get; set; }

    /// <summary>Gets or sets the scripts collection.</summary>
    /// <value>The scripts.</value>
    IList<ScriptReference> Scripts { get; set; }
  }
}
