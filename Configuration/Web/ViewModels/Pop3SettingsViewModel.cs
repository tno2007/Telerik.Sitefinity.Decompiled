// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.ViewModels.Pop3SettingsViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Configuration.Web.ViewModels
{
  /// <summary>Represents a view model for the pop3 settings.</summary>
  [DataContract]
  public class Pop3SettingsViewModel
  {
    /// <summary>Gets or sets the host of the server.</summary>
    [DataMember]
    public string Host { get; set; }

    /// <summary>Gets or sets the port of the server.</summary>
    [DataMember]
    public int Port { get; set; }

    /// <summary>Gets or sets the username for the server.</summary>
    [DataMember]
    public string Username { get; set; }

    /// <summary>Gets or sets the password for the server.</summary>
    [DataMember]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if SSL should be used.
    /// </summary>
    [DataMember]
    public bool UseSSL { get; set; }
  }
}
