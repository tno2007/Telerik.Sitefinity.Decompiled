// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ExecutedEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Provides data for events fired after executing data methods.
  /// </summary>
  public class ExecutedEventArgs : EventArgs
  {
    private object data;
    private string commandName;
    private object commandArgs;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> class.
    /// </summary>
    public ExecutedEventArgs()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> class.
    /// </summary>
    /// <param name="commandName">Name of the command.</param>
    /// <param name="data">The data.</param>
    public ExecutedEventArgs(string commandName, object data)
    {
      this.commandName = commandName;
      this.data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> class.
    /// </summary>
    /// <param name="commandName">Name of the command.</param>
    /// <param name="commandArgs">The command arguments.</param>
    /// <param name="data">The data.</param>
    public ExecutedEventArgs(string commandName, object commandArgs, object data)
    {
      this.commandName = commandName;
      this.commandArgs = commandArgs;
      this.data = data;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the current execution should be canceled.
    /// </summary>
    /// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
    public object Data => this.data;

    /// <summary>Gets the name of the command.</summary>
    /// <value>The name of the command.</value>
    public string CommandName => this.commandName;

    /// <summary>Gets the command arguments.</summary>
    /// <value>The command arguments.</value>
    public object CommandArguments => this.commandArgs;
  }
}
