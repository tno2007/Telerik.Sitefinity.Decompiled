﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.WcfThumbnail
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// Defines the properties for the thumbnail object that need to tranfered from WCF service to database.
  /// </summary>
  [DataContract]
  public class WcfThumbnail
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.Services.WcfThumbnail" /> class.
    /// </summary>
    public WcfThumbnail()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.Services.WcfThumbnail" /> class.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="data">The data.</param>
    public WcfThumbnail(Guid id, string data)
    {
      this.Id = id;
      this.Data = data;
    }

    /// <summary>Gets or sets the id.</summary>
    /// <value>The id.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the data.</summary>
    /// <value>The data.</value>
    [DataMember]
    public string Data { get; set; }
  }
}
