// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.IOwnershipExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.UserProfiles;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Extension methods for the <see cref="T:Telerik.Sitefinity.Model.IOwnership" /> interface.
  /// </summary>
  public static class IOwnershipExtensions
  {
    /// <summary>Gets the display name of the user based on it's id.</summary>
    /// <param name="dataItem">
    /// The data item for which the user display name should be obtained.
    /// </param>
    /// <returns>The display name of the user.</returns>
    public static string GetUserDisplayName(this IOwnership dataItem) => UserProfilesHelper.GetUserDisplayName(dataItem.Owner);
  }
}
