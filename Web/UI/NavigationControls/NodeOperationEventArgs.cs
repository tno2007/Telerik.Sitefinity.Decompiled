﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.NodeOperationEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Web;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  public class NodeOperationEventArgs : CancelEventArgs
  {
    private SiteMapNode node;

    public SiteMapNode Node => this.node;

    public bool DoOperation { get; set; }

    public NodeOperationEventArgs(SiteMapNode node) => this.node = node;
  }
}
