// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.BackendContentView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// Temporary wrapper around the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentView" /> class used for the alpha release due to the
  /// problems with definition persistence.
  /// </summary>
  [Obsolete("Remove this class once the ControlBuilders can correctly instantiate standard ContentView control.")]
  public class BackendContentView : ContentView
  {
    /// <summary>Gets or sets the control definition object.</summary>
    /// <value>The definition.</value>
    [Browsable(false)]
    public override ContentViewControlDefinition ControlDefinition => base.ControlDefinition;
  }
}
