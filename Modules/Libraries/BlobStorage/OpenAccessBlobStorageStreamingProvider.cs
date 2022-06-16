// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.OpenAccessBlobStorageStreamingProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  internal class OpenAccessBlobStorageStreamingProvider : 
    BlobStorageProvider,
    IOpenAccessMetadataProvider
  {
    private string connectionName;
    private int streamFlushSize = 524288;

    protected override void InitializeStorage(NameValueCollection config)
    {
      string connectionName = config["connectionString"];
      if (string.IsNullOrEmpty(connectionName))
        connectionName = "Sitefinity";
      this.connectionName = OpenAccessConnection.InitializeProvider((IOpenAccessMetadataProvider) this, connectionName).Name;
      this.streamFlushSize = config["streamFlushSize"].IsNullOrEmpty() ? this.streamFlushSize : int.Parse(config["streamFlushSize"]);
    }

    public override Stream GetUploadStream(IBlobContent content)
    {
      SitefinityOAContext newTransaction = this.GetNewTransaction();
      Blob entity = new Blob();
      entity.FileId = content.FileId;
      newTransaction.Add((object) entity);
      entity.BinaryData.Append = true;
      newTransaction.Commit();
      return (Stream) new SitefinityBinaryStream(newTransaction, entity.BinaryData, this.streamFlushSize);
    }

    public override Stream GetDownloadStream(IBlobContent content) => (Stream) (this.GetNewTransaction().GetAll<Blob>().FirstOrDefault<Blob>((Expression<Func<Blob, bool>>) (b => b.FileId == content.FileId)) ?? throw new ArgumentException("No such file with the specified id exists")).BinaryData;

    public override void Delete(IBlobContentLocation location)
    {
      SitefinityOAContext newTransaction = this.GetNewTransaction();
      Blob entity = newTransaction.GetAll<Blob>().FirstOrDefault<Blob>((Expression<Func<Blob, bool>>) (b => b.FileId == location.FileId));
      if (entity == null)
        return;
      newTransaction.Delete((object) entity);
      newTransaction.Commit();
    }

    public override bool BlobExists(IBlobContentLocation location) => this.GetNewTransaction().GetAll<Blob>().Any<Blob>((Expression<Func<Blob, bool>>) (b => b.FileId == location.FileId));

    /// <inheritdoc />
    public override bool HasSameLocation(BlobStorageProvider other) => other is OpenAccessBlobStorageStreamingProvider streamingProvider ? this.connectionName.Equals(streamingProvider.connectionName, StringComparison.OrdinalIgnoreCase) : base.HasSameLocation(other);

    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new StreamingBlobStorageMetadataSource(context);

    protected internal SitefinityOAContext GetNewTransaction()
    {
      SitefinityOAContext context = OpenAccessConnection.GetContext((IOpenAccessMetadataProvider) this, this.connectionName);
      context.ProviderName = this.ModuleName;
      return context;
    }
  }
}
