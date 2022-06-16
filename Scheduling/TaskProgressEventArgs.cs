// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.TaskProgressEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Scheduling
{
  public class TaskProgressEventArgs : EventArgs
  {
    /// <summary>Gets or sets the message.</summary>
    /// <value>The message.</value>
    public string StatusMessage { get; set; }

    /// <summary>Gets or sets the progress in percentage.</summary>
    /// <value>The progress.</value>
    public int Progress { get; set; }

    public bool Stopped { get; set; }
  }
}
