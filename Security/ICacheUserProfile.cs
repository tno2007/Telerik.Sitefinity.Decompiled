// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.ICacheUserProfile
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.DataResolving;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Contains minimum user profile information that will be cached.
  /// </summary>
  public interface ICacheUserProfile : ISimpleLocatable
  {
    /// <summary>Gets the user id.</summary>
    /// <value>The user id.</value>
    Guid UserId { get; }

    /// <summary>Gets the username.</summary>
    /// <value>The username.</value>
    string UserName { get; }

    /// <summary>Gets the email.</summary>
    /// <value>The email.</value>
    string Email { get; }

    /// <summary>Gets the profile nickname.</summary>
    /// <value>The nickname.</value>
    string Nickname { get; }

    /// <summary>Gets the first name of the user.</summary>
    string FirstName { get; }

    /// <summary>Gets the last name of the user.</summary>
    string LastName { get; }

    /// <summary>Gets the about field of the user.</summary>
    string About { get; set; }

    /// <summary>Gets the registration date.</summary>
    /// <value>The registration date.</value>
    DateTime RegistrationDate { get; }

    /// <summary>Gets the preferred language.</summary>
    [Obsolete("Will be removed when user settings a re implemented")]
    string PreferredLanguage { get; }

    /// <summary>Gets a user preference</summary>
    /// <typeparam name="TType">The type of the requested preference.</typeparam>
    /// <param name="key">The key of the preference.</param>
    /// <param name="defaultValueIfEmpty">Teh default value to return in case the preference is not persisted.</param>
    /// <returns>The preference value</returns>
    TType GetPreference<TType>(string key, TType defaultValueIfEmpty);

    /// <summary>Gets the avatar url.</summary>
    string AvatarUrl { get; }

    /// <summary>
    /// Tries to get the item for a given key. Returns true if the item has been retrieved successfully.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The unique key of the item.</param>
    /// <param name="value">The item that will be get.</param>
    /// <returns>Returns true if the item has been retrieved successfully. Otherwise false.</returns>
    bool TryGetValue<T>(string key, out T value);

    /// <summary>Stores the given value with the specified key.</summary>
    /// <param name="key">The key by which the item will be retrieved.</param>
    /// <param name="value">The item that will be stored.</param>
    void SetValue(string key, object value);
  }
}
