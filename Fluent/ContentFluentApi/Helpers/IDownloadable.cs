// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.IDownloadable`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Defines an interface for facades that support uploading of their binary content via streaming
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this interface</typeparam>
  public interface IDownloadable<TCurrentFacade> where TCurrentFacade : BaseFacade
  {
    /// <summary>
    /// Provides a streaming download of the content's binary data
    /// </summary>
    /// <param name="storage">Stream that can be used to retrieve the binary content of the content item</param>
    /// <returns>Currently chained facade</returns>
    TCurrentFacade DownloadContent(out Stream storage);
  }
}
