// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.WcfBlobStorageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// WCF representation of the <see cref="!:BlobStorageProvider" /> type.
  /// </summary>
  [DataContract]
  public class WcfBlobStorageProvider
  {
    /// <summary>Gets or sets the Id of the blob storage provider.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the friendly name used to refer to the provider during configuration.
    /// </summary>
    /// <returns>
    /// The friendly name used to refer to the provider during configuration.
    /// </returns>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the name of the application to store and retrieve role information for.
    /// </summary>
    /// <returns>
    /// The name of the application to store and retrieve role information for.
    /// </returns>
    [DataMember]
    public string ApplicationName { get; set; }

    /// <summary>Gets the title.</summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title { get; set; }

    /// <summary>
    /// Gets a brief, friendly description suitable for display in administrative tools or other user interfaces (UIs).
    /// </summary>
    /// <returns>
    /// A brief, friendly description suitable for display in administrative tools or other UIs.
    /// </returns>
    [DataMember]
    public string Description { get; set; }

    /// <summary>Gets or sets the total items count.</summary>
    /// <value>The total items count.</value>
    [DataMember]
    public int TotalItemsCount { get; set; }
  }
}
