// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserOperationInvokedEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// Represents an argument passed to event handlers operating on a user instance.
  /// </summary>
  public class UserOperationInvokedEventArgs : EventArgs
  {
    private User user;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:UserOperationEventArgs" /> class.
    /// </summary>
    public UserOperationInvokedEventArgs(User user) => this.user = user;

    /// <summary>Gets the user instance.</summary>
    /// <value>The user.</value>
    public User User => this.user;
  }
}
