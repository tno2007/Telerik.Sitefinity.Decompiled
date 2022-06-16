// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.IContextWithAdditionalUrls
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>Context with additional urls</summary>
  internal interface IContextWithAdditionalUrls
  {
    /// <summary>Gets or sets the URL names of the item.</summary>
    [DataMember]
    string[] AdditionalUrlNames { get; set; }

    /// <summary>Gets or sets the flag, if multiple urls are allowed.</summary>
    [DataMember]
    bool AllowMultipleUrls { get; set; }

    /// <summary>
    /// Gets or sets the flag, if all additional url-s will redirect to the default one.
    /// </summary>
    [DataMember]
    bool AdditionalUrlsRedirectToDefault { get; set; }

    /// <summary>Gets or sets the default URL.</summary>
    /// <value>The default URL.</value>
    [DataMember]
    string DefaultUrl { get; set; }
  }
}
