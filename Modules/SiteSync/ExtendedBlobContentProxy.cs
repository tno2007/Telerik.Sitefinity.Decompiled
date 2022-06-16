// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ExtendedBlobContentProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;

namespace Telerik.Sitefinity.SiteSync
{
  /// <summary>
  /// An extended version of BlobContentProxy used in SiteSync
  /// </summary>
  internal class ExtendedBlobContentProxy : BlobContentProxy
  {
    public ExtendedBlobContentProxy()
    {
    }

    public ExtendedBlobContentProxy(IChunksBlobContent content)
      : base((IBlobContent) content)
    {
      if (!(content is MediaContent))
        return;
      MediaContent mediaContent = content as MediaContent;
      this.BlobStorageProvider = mediaContent.GetStorageProviderName();
      object provider = mediaContent.Provider;
      if (provider == null)
        return;
      this.Provider = provider.ToString();
    }

    public string BlobStorageProvider { get; set; }

    public string Provider { get; set; }
  }
}
