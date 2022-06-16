// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Contracts.IUserProfileViewMasterDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Security.Web.UI.Contracts
{
  /// <summary>Defines the members of the user profiles master view.</summary>
  public interface IUserProfileViewMasterDefinition : 
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>Gets or sets the name of the users provider to use.</summary>
    /// <value>The name of the users provider to use.</value>
    string Provider { get; set; }

    /// <summary>Gets or sets the full name of the profile type.</summary>
    /// <value>The full name of the profile type.</value>
    string ProfileTypeFullName { get; set; }

    /// <summary>Gets or sets which set of users to show.</summary>
    /// <value>The users display mode.</value>
    Telerik.Sitefinity.Security.Web.UI.UsersDisplayMode? UsersDisplayMode { get; set; }

    /// <summary>Gets or sets the id of the user to show.</summary>
    /// <value>The user id.</value>
    Guid? UserId { get; set; }
  }
}
