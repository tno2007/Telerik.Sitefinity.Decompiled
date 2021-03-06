// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.ImageService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries.Images;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// This web service is used to work with <see cref="T:Telerik.Sitefinity.Libraries.Model.Image" /> objects.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  public class ImageService : MediaContentService<Image, Album, AlbumItemViewModel, LibrariesManager>
  {
  }
}
