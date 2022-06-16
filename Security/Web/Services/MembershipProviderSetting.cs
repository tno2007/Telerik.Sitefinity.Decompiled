// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.MembershipProviderSetting
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>Provides information about membership provider.</summary>
  [DataContract]
  public class MembershipProviderSetting
  {
    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether requires password question and answer.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if requires password question and answer otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool RequiresQuestionAndAnswer { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether password retrieval is enabled.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if password retrieval is enabled otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool EnablePasswordRetrieval { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether password reset is enabled.
    /// </summary>
    /// <value><c>true</c> if password reset is enabled otherwise, <c>false</c>.</value>
    [DataMember]
    public bool EnablePasswordReset { get; set; }
  }
}
