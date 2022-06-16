// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataEventAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Provides information for data provider method events such as Executing and Executed.
  /// By default data provider methods fire events,
  /// if you want certain methods not to fire events or provide different event arguments use this attribute.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class DataEventAttribute : Attribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.DataEventAttribute" /> class.
    /// </summary>
    /// <param name="fireEvents">
    /// if set to <c>true</c> the marked method will fire data events (Executing and Executed) otherwise events will not be fired.
    /// </param>
    public DataEventAttribute(bool fireEvents) => this.FireEvents = fireEvents;

    /// <summary>
    /// Gets or sets a value indicating whether the marked method should fire data events (Executing and Executed).
    /// </summary>
    /// <value><c>true</c> if [fire events]; otherwise, <c>false</c>.</value>
    public bool FireEvents { get; private set; }

    /// <summary>
    /// Gets or sets the name of the command to pass to the event arguments.
    /// If this CommandName is null the name of the method is used.
    /// </summary>
    /// <value>The name of the command.</value>
    public string CommandName { get; set; }
  }
}
