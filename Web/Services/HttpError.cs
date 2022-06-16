// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.HttpError
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Net;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Represents summarized information about an exception that can be returned as web service response.
  /// </summary>
  [DataContract]
  public class HttpError
  {
    /// <summary>Gets or sets the HTTP response status code.</summary>
    /// <value>The code.</value>
    [DataMember]
    public HttpStatusCode Code { get; set; }

    /// <summary>Gets or sets the description of the error.</summary>
    /// <value>The description of the error.</value>
    [DataMember]
    public string Description { get; set; }
  }
}
