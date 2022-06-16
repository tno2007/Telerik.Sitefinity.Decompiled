// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Versioning.BlobContentCleanerTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Libraries.Versioning
{
  internal class BlobContentCleanerTask : ScheduledTask
  {
    private string customData;
    internal const string FilePath = "filePath";
    internal const string BlobStorageProvider = "blobStorageProvider";
    internal const string LibraryProviderName = "libraryProviderName";
    internal const string NumberOfChunks = "numberOfChunks";
    internal const string TotalSize = "totalSize";
    internal const string Width = "width";
    internal const string Height = "height";
    internal const string Extension = "extension";
    internal const string MimeType = "mimeType";
    internal const string DefaultUrl = "defaultUrl";

    public BlobContentCleanerTask()
    {
      this.Id = Guid.NewGuid();
      this.ExecuteTime = DateTime.UtcNow;
    }

    public override string TaskName => this.GetType().FullName;

    public override void ExecuteTask()
    {
      string key = this.Key;
      IDictionary<string, object> data = BlobContentCleanerTask.GetData(this.GetCustomData());
      data["filePath"].ToString();
      string str = data["blobStorageProvider"].ToString();
      if (LibrariesManager.GetManager(data["libraryProviderName"].ToString()).Provider.IsFileUsedByOtherMediaContent(new Guid(key), str, Guid.Empty))
        return;
      IBlobContent content = (IBlobContent) new BlobContentProxy()
      {
        FileId = new Guid(key),
        FilePath = key
      };
      BlobStorageManager manager = BlobStorageManager.GetManager(str);
      manager.Delete(manager.ResolveBlobContentLocation(content));
    }

    public override void SetCustomData(string customData) => this.customData = customData;

    public override string GetCustomData() => this.customData;

    internal static string ConvertData(IDictionary<string, object> taskData) => new CollectionJsonTypeConverter<Dictionary<string, object>>().ConvertToString((object) taskData);

    internal static IDictionary<string, object> GetData(string taskData) => (IDictionary<string, object>) new CollectionJsonTypeConverter<Dictionary<string, object>>().ConvertFromString(taskData);
  }
}
