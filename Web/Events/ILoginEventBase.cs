// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.ILoginEventBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Events
{
  /// <summary>
  /// An interface containing information about the user being logged in or logged out
  /// </summary>
  [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Justification = "Changing it will cause a breaking change since it is a public interface", MessageId = "Login")]
  public interface ILoginEventBase : IEvent
  {
    /// <summary>Gets the user id.</summary>
    /// <value>The user id.</value>
    string UserId { get; }

    /// <summary>Gets the user name of the log in attempt.</summary>
    /// <value>The user name.</value>
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", Justification = "Changing it will cause a breaking change since it is a public interface", MessageId = "Username")]
    string Username { get; }

    /// <summary>Gets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    string ProviderName { get; }

    /// <summary>Gets the IP host address of the client.</summary>
    /// <value>The IP address.</value>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Changing it will cause a breaking change since it is a public interface", MessageId = "Ip")]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Changing it will cause a breaking change since it is a public interface", MessageId = "Ip")]
    string IpAddress { get; }

    /// <summary>
    /// Gets a value indicating whether the user is a back end one.
    /// </summary>
    /// <value>True if the user is back end user.</value>
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", Justification = "Changing it will cause a breaking change since it is a public interface", MessageId = "Backend")]
    bool IsBackendUser { get; }

    /// <summary>Gets the email of the user.</summary>
    /// <value>The email.</value>
    string Email { get; }
  }
}
