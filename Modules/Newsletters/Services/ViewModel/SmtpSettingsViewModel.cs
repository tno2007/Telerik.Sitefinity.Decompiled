// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SmtpSettingsViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  [DataContract]
  [Obsolete]
  public class SmtpSettingsViewModel
  {
    [DataMember]
    public string Host { get; set; }

    [DataMember]
    public int Port { get; set; }

    [DataMember]
    public bool UseAuthentication { get; set; }

    [DataMember]
    public string SmtpUsername { get; set; }

    [DataMember]
    public string SmtpPassword { get; set; }

    [DataMember]
    public bool UseSmtpSSL { get; set; }

    [DataMember]
    public string TestEmailAddress { get; set; }

    [DataMember]
    public string NotificationsSmtpProfile { get; set; }
  }
}
