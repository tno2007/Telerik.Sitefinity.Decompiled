// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ScriptsRegisteringEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Event raised while the control loads its scripts.</summary>
  internal class ScriptsRegisteringEvent : IScriptsRegisteringEvent, IEvent
  {
    /// <summary>
    /// Gets or sets the sender. Represents the IScriptControl.
    /// </summary>
    /// <value>The sender.</value>
    public IScriptControl Sender { get; set; }

    /// <summary>Gets or sets the scripts collection.</summary>
    /// <value>The scripts.</value>
    public IList<ScriptReference> Scripts { get; set; }

    /// <summary>A value specifying the origin of the event.</summary>
    public string Origin { get; set; }
  }
}
