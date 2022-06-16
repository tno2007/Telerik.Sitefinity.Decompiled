// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.UploadDownloadHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  internal static class UploadDownloadHelper
  {
    internal static void UploadContent(
      LibrariesManager manager,
      MediaContent content,
      Stream dataToUpload,
      string extension)
    {
      FacadeHelper.AssertArgumentNotNull<Stream>(dataToUpload, nameof (dataToUpload));
      FacadeHelper.AssertArgumentNotNull<MediaContent>(content, nameof (content));
      FacadeHelper.AssertIsValidMimeExtension(extension, "Media content data should have a valid extension");
      FacadeHelper.AssertArgumentNotNull<LibrariesManager>(manager, "Media temp facade manager is not LibrariesManager");
      manager.Upload(content, dataToUpload, extension);
    }

    internal static Stream DownlaodContent(LibrariesManager manager, MediaContent content)
    {
      FacadeHelper.AssertArgumentNotNull<LibrariesManager>(manager, "Media temp facade manager is not LibrariesManager");
      FacadeHelper.AssertArgumentNotNull<MediaContent>(content, nameof (content));
      return manager.Download(content);
    }
  }
}
