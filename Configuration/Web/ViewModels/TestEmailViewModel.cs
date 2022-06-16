// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.ViewModels.TestEmailViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Configuration.Web.ViewModels
{
  /// <summary>Represents a view model for the test email.</summary>
  [DataContract]
  public class TestEmailViewModel
  {
    /// <summary>Gets or sets the subject of the test email.</summary>
    [DataMember]
    public string Subject { get; set; }

    /// <summary>Gets or sets the body html of the test email.</summary>
    [DataMember]
    public string BodyHtml { get; set; }

    /// <summary>
    /// Gets or sets the sender email address of the test email.
    /// </summary>
    [DataMember]
    public string SenderEmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the sender email address of the test email.
    /// </summary>
    [DataMember]
    public string SenderName { get; set; }

    /// <summary>
    /// Gets or sets the receiver email address of the test email.
    /// </summary>
    [DataMember]
    public string ReceiverEmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the module name for the context from where to send the test email.
    /// </summary>
    [DataMember]
    public string ModuleName { get; set; }

    /// <summary>Gets or sets the system email placeholder fields.</summary>
    [DataMember]
    public IEnumerable<SystemEmailsPlaceholderViewModel> PlaceholderFields { get; set; }

    /// <summary>Gets or sets the dynamic placeholder fields.</summary>
    [DataMember]
    public IEnumerable<SystemEmailsPlaceholderViewModel> DynamicPlaceholderFields { get; set; }
  }
}
