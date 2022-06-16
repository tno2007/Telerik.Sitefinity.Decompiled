// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Configuration.UserProfileViewMasterElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Web.UI.Contracts;
using Telerik.Sitefinity.Security.Web.UI.Definitions;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Security.Web.UI.Configuration
{
  /// <summary>
  /// The configuration element for the master user profile view.
  /// </summary>
  public class UserProfileViewMasterElement : 
    ContentViewMasterElement,
    IUserProfileViewMasterDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private Collection<IItemInfoDefinition> roles;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.Configuration.UserProfileDetailElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public UserProfileViewMasterElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new UserProfileViewMasterDefinition((ConfigElement) this);

    /// <summary>Gets or sets the full name of the profile type.</summary>
    /// <value>The full name of the profile type.</value>
    [ConfigurationProperty("profileTypeFullName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProfileTypeFullNameDescription", Title = "ProfileTypeFullNameCaption")]
    public string ProfileTypeFullName
    {
      get => (string) this["profileTypeFullName"];
      set => this["profileTypeFullName"] = (object) value;
    }

    /// <summary>Get the data provider for this element.</summary>
    [ConfigurationProperty("provider")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProviderDescription", Title = "ProviderCaption")]
    public string Provider
    {
      get => (string) this["provider"];
      set => this["provider"] = (object) value;
    }

    /// <summary>Gets or sets the id of the user to show.</summary>
    /// <value>The user id.</value>
    [ConfigurationProperty("userId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UserIdDescription", Title = "UserIdCaption")]
    public Guid? UserId
    {
      get => (Guid?) this["userId"];
      set => this["userId"] = (object) value;
    }

    /// <summary>Gets or sets which set of users to show.</summary>
    /// <value>The users display mode.</value>
    [ConfigurationProperty("usersDisplayMode")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UsersDisplayModeDescription", Title = "UsersDisplayModeCaption")]
    public Telerik.Sitefinity.Security.Web.UI.UsersDisplayMode? UsersDisplayMode
    {
      get => (Telerik.Sitefinity.Security.Web.UI.UsersDisplayMode?) this["usersDisplayMode"];
      set => this["usersDisplayMode"] = (object) value;
    }

    /// <summary>
    /// Constants to hold the string keys for configuration properties of UserProfileDetailElement
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct UserProfileViewMasterElementProps
    {
      public const string Provider = "provider";
      public const string UserId = "userId";
      public const string UsersDisplayMode = "usersDisplayMode";
      public const string ProfileTypeFullName = "profileTypeFullName";
    }
  }
}
