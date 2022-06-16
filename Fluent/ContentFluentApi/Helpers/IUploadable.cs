// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.IUploadable`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Defines an interface for facades that support uploading of their binary content
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this interface</typeparam>
  public interface IUploadable<TCurrentFacade> where TCurrentFacade : BaseFacade
  {
    /// <summary>
    /// Upload the binary contents of <paramref name="dataToUpload" /> into the item which is currently loaded in the facade
    /// </summary>
    /// <param name="dataToUpload">Binary data which is to be uploaded in the content item</param>
    /// <param name="extension">MIME extension</param>
    /// <returns>Currently chained facade</returns>
    TCurrentFacade UploadContent(Stream dataToUpload, string extension);
  }
}
