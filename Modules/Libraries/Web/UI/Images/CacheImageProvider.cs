// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.CacheImageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.TemporaryStorage;
using Telerik.Sitefinity.Security;
using Telerik.Web.UI.ImageEditor;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Images
{
  internal class CacheImageProvider : ICacheImageProvider
  {
    private const string StorageKeyword = "ImageEditor";
    private SitefinityTemporaryStorage tempStorage = ObjectFactory.Resolve<ITemporaryStorage>() as SitefinityTemporaryStorage;

    public ImageStorage Storage { get; set; }

    public string ImageStorageKey { get; set; }

    public void ClearImages() => this.tempStorage.RemoveAllStartingWith(string.Format("{0}_{1}", (object) "ImageEditor", (object) SecurityManager.CurrentUserId));

    public EditableImage Retrieve(string key) => new EditableImage(this.StringToImage(this.tempStorage.Get(key)));

    public string Store(EditableImage image)
    {
      string key = string.Format("{0}_{1}_{2}", (object) "ImageEditor", (object) SecurityManager.CurrentUserId, (object) Guid.NewGuid().ToString());
      this.tempStorage.AddOrUpdate(key, this.ImageToString(image), DateTime.UtcNow.AddHours(1.0));
      return key;
    }

    public void ClearImages(string imageKey) => throw new NotImplementedException();

    public System.Drawing.Image LoadImage(
      string imageUrl,
      string physicalPath,
      HttpContext context)
    {
      throw new NotImplementedException();
    }

    public string SaveImage(
      EditableImage editableImage,
      string physicalPath,
      string imageUrl,
      bool overwrite)
    {
      throw new NotImplementedException();
    }

    private string ImageToString(EditableImage image)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        image.Image.Save((Stream) memoryStream, image.RawFormat);
        return Convert.ToBase64String(memoryStream.ToArray());
      }
    }

    private System.Drawing.Image StringToImage(string imageString) => System.Drawing.Image.FromStream((Stream) new MemoryStream(Convert.FromBase64String(imageString)));
  }
}
