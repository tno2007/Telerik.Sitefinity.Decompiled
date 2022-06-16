// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.OpenAccessBlobStorageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  /// <summary>
  /// Implements a logic for persisting BLOB data as chunks in the database.
  /// </summary>
  public class OpenAccessBlobStorageProvider : 
    ChunksBlobStorageProvider,
    IOpenAccessMetadataProvider,
    IOpenAccessUpgradableProvider
  {
    public const string databaseConnectionKey = "connectionString";
    private int numberOfChunksToCommit = 10;
    private string connectionName;

    /// <summary>Gets the number of chunks to commit.</summary>
    /// <value>The number of chunks to commit.</value>
    public int NumberOfChunksToCommit => this.numberOfChunksToCommit;

    protected override void InitializeStorage(NameValueCollection config)
    {
      base.InitializeStorage(config);
      string s = config["numberOfChunksToCommit"];
      if (!string.IsNullOrEmpty(s))
        this.numberOfChunksToCommit = int.Parse(s);
      string connectionName = config["connectionString"];
      if (string.IsNullOrEmpty(connectionName))
        connectionName = "Sitefinity";
      this.connectionName = OpenAccessConnection.InitializeProvider((IOpenAccessMetadataProvider) this, connectionName).Name;
    }

    protected internal override object GetNewTransaction()
    {
      SitefinityOAContext context = OpenAccessConnection.GetContext((IOpenAccessMetadataProvider) this, this.connectionName);
      context.ProviderName = this.ModuleName;
      return (object) context;
    }

    protected internal override IChunk CreateChunk(
      IChunksBlobContent content,
      object transaction)
    {
      return (IChunk) new Chunk(content.FileId, content.NumberOfChunks);
    }

    protected internal override IEnumerable<IChunk> GetChunks(
      IChunksBlobContent content,
      int ordinal,
      int count,
      object transaction)
    {
      Guid fileId = content.FileId;
      return ((IEnumerable<IChunk>) (transaction as SitefinityOAContext).GetAll<Chunk>().Where<Chunk>((Expression<Func<Chunk, bool>>) (c => c.FileId == fileId && c.Ordinal >= ordinal)).OrderBy<Chunk, int>((Expression<Func<Chunk, int>>) (c => c.Ordinal)).Take<Chunk>(count)).AsEnumerable<IChunk>();
    }

    protected internal override void SaveChunk(
      IChunk chunk,
      IChunksBlobContent content,
      object transaction)
    {
      SitefinityOAContext sitefinityOaContext = transaction as SitefinityOAContext;
      sitefinityOaContext.Add((object) (chunk as Chunk));
      if (content.NumberOfChunks % this.numberOfChunksToCommit != 0)
        return;
      sitefinityOaContext.Commit();
    }

    protected internal override void ClearChunks(IBlobContentLocation location, object transaction)
    {
      Guid fileId = location.FileId;
      if (!(fileId != Guid.Empty))
        return;
      SitefinityOAContext sitefinityOaContext = transaction as SitefinityOAContext;
      IQueryable<Chunk> source = sitefinityOaContext.GetAll<Chunk>().Where<Chunk>((Expression<Func<Chunk, bool>>) (c => c.FileId == fileId));
      int ofChunksToCommit = this.NumberOfChunksToCommit;
      int num1 = source.Count<Chunk>();
      int num2 = 0;
      if (num1 > 0)
        num2 = (int) Math.Ceiling((double) num1 / (double) ofChunksToCommit);
      for (int index = 0; index < num2; ++index)
      {
        foreach (Chunk entity in (IEnumerable<Chunk>) source.Take<Chunk>(ofChunksToCommit))
          sitefinityOaContext.Remove((object) entity);
        sitefinityOaContext.Commit();
      }
    }

    protected internal override int GetChunksCount(
      IBlobContentLocation location,
      object transaction)
    {
      Guid fileId = location.FileId;
      if (!(fileId != Guid.Empty))
        return 0;
      return (transaction as SitefinityOAContext).GetAll<Chunk>().Where<Chunk>((Expression<Func<Chunk, bool>>) (c => c.FileId == fileId)).Count<Chunk>();
    }

    /// <inheritdoc />
    public override bool HasSameLocation(BlobStorageProvider other) => other is OpenAccessBlobStorageProvider blobStorageProvider ? this.connectionName.Equals(blobStorageProvider.connectionName, StringComparison.OrdinalIgnoreCase) : base.HasSameLocation(other);

    protected internal override void Finalize(IChunksBlobContent content, object transaction) => (transaction as SitefinityOAContext).Commit();

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new BlobStorageMetadataSource(context);

    /// <inheritdoc />
    public virtual int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    /// <inheritdoc />
    public virtual void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
      if (upgradingFromSchemaVersionNumber <= 0 || upgradingFromSchemaVersionNumber >= 1600)
        return;
      OpenAccessConnection.MsSqlUpgrade(context.Connection, "OpenAccessBlobStorageProvider: Upgrade to 4.2 - check (and remove) duplicated chunks", (Action<IDbCommand>) (cmd =>
      {
        try
        {
          cmd.ExecuteNonQuery("\r\ndelete sf_chunks\r\nfrom sf_chunks\r\ninner join\r\n\t(\r\n\tselect file_id, ordinal, count(*) as cnt, min(sf_chunks_id) as chunkid\r\n\tfrom sf_chunks \r\n\tgroup by file_id, ordinal\r\n\thaving count(*) > 1\r\n\t) as t1\r\non\r\n\tsf_chunks.file_id = t1.file_id and sf_chunks.ordinal = t1.ordinal\r\nwhere \r\n\tsf_chunks.sf_chunks_id <> t1.chunkid");
        }
        catch
        {
        }
      }));
      if (context.DatabaseContext.DatabaseType != DatabaseType.SqlAzure)
        return;
      context.ExecuteSQL("EXEC sp_rename 'pk_sf_chunks', 'pk_sf_chunks_old41', 'OBJECT'");
      context.ExecuteSQL("EXEC sp_rename 'sf_chunks', 'sf_chunks_old41'");
    }

    /// <inheritdoc />
    public virtual void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
      if (upgradedFromSchemaVersionNumber <= 0 || upgradedFromSchemaVersionNumber >= 1600)
        return;
      string empty = string.Empty;
      string str = "OpenAccessBlobStorageProvider: Upgrade to 4.2 clean obsolete columns";
      try
      {
        if (context.DatabaseContext.DatabaseType == DatabaseType.SqlAzure)
        {
          context.ExecuteSQL("insert into [sf_chunks]\r\n\t([file_id], [ordinal], [sze], [dta], [voa_version])\r\nselect \r\n\t[file_id], [ordinal], [sze], [dta], [voa_version]\r\nfrom \r\n\t[sf_chunks_old41]");
          context.ExecuteSQL("drop table [sf_chunks_old41]");
        }
        else
        {
          string sql = context.DatabaseContext.DatabaseType != DatabaseType.Oracle ? "ALTER TABLE sf_chunks DROP COLUMN sf_chunks_id" : "ALTER TABLE \"sf_chunks\" DROP COLUMN \"sf_chunks_id\"";
          context.ExecuteSQL(sql);
        }
        Log.Write((object) string.Format("PASSED: {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED: {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
    }
  }
}
