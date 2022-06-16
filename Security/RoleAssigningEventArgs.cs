// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.RoleAssigningEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security
{
  public class RoleAssigningEventArgs : EventArgs
  {
    private bool cnacel;
    private string commandName;
    private object commandArgs;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ExecutingEventArgs" /> class.
    /// </summary>
    public RoleAssigningEventArgs()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ExecutingEventArgs" /> class.
    /// </summary>
    /// <param name="commandName">Name of the command.</param>
    public RoleAssigningEventArgs(string commandName) => this.commandName = commandName;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ExecutingEventArgs" /> class.
    /// </summary>
    /// <param name="commandName">Name of the command.</param>
    /// <param name="commandArgs">The command arguments.</param>
    public RoleAssigningEventArgs(string commandName, object commandArgs)
    {
      this.commandName = commandName;
      this.commandArgs = commandArgs;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the current execution should be canceled.
    /// </summary>
    /// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
    public bool Cancel
    {
      get => this.cnacel;
      set => this.cnacel = value;
    }

    /// <summary>Gets the name of the command.</summary>
    /// <value>The name of the command.</value>
    public string CommandName => this.commandName;

    /// <summary>Gets the command arguments.</summary>
    /// <value>The command arguments.</value>
    public object CommandArguments => this.commandArgs;
  }
}
