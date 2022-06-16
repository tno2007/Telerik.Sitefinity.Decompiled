// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.Messages.WorkflowOperationMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.InlineEditing;

namespace Telerik.Sitefinity.Services.InlineEditing.Messages
{
  /// <summary>
  /// Represents the ServiceStack message for the workflow operation
  /// </summary>
  public class WorkflowOperationMessage
  {
    private List<ItemContextModel> items;

    public string PageId { get; set; }

    public InlineEditingOperation WorkflowOperation { get; set; }

    public string CustomWorkflowOperation { get; set; }

    public List<ItemContextModel> Items
    {
      get
      {
        if (this.items == null)
          this.items = new List<ItemContextModel>();
        return this.items;
      }
      set => this.items = value;
    }
  }
}
