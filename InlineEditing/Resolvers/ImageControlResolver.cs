// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Resolvers.ImageControlResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services.InlineEditing;
using Telerik.Sitefinity.Web.UI.PublicControls;

namespace Telerik.Sitefinity.InlineEditing.Resolvers
{
  internal class ImageControlResolver : InlineEditingResolverBase, IInlineEditingResolver
  {
    private string fieldType = "ImageControl";

    public object GetFieldValue(Guid id, string itemType, string fieldName, string provider)
    {
      PageManager manager = PageManager.GetManager();
      Control component = manager.LoadControl((ObjectData) manager.GetControl<ControlData>(id), (CultureInfo) null);
      if (!(component is ImageControl))
        return (object) null;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) component);
      Guid id1 = (Guid) properties["ImageId"].GetValue((object) component);
      Image image = LibrariesManager.GetManager().GetImage(id1);
      string str = properties["ProviderName"].GetValue((object) component) as string;
      return (object) new ImageControlModel()
      {
        ControlData = new ImageControlDataModel()
        {
          ToolTip = (properties["ToolTip"].GetValue((object) component) as string),
          AlternateText = (properties["AlternateText"].GetValue((object) component) as string),
          ImageId = id1,
          ThumbnailName = (properties["ThumbnailName"].GetValue((object) component) as string),
          ProviderName = (properties["ProviderName"].GetValue((object) component) as string),
          Width = int.Parse(properties["Width"].GetValue((object) component).ToString()),
          Height = int.Parse(properties["Height"].GetValue((object) component).ToString())
        },
        Image = new ImageModel()
        {
          Id = id1,
          AlbumId = image.Album.Id,
          Title = (string) image.Title,
          DateCreated = image.DateCreated,
          Extension = image.Extension,
          TotalSize = (image.TotalSize / 1024L),
          ThumbnailUrl = image.ThumbnailUrl,
          AlternativeText = (string) image.AlternativeText,
          Width = image.Width,
          Height = image.Height
        },
        DetailsViewInfo = new ImageDetailViewModel()
        {
          ItemId = id1,
          ParentId = image.Album.Id,
          CommandName = "editMediaContentProperties",
          ProviderName = (str ?? "OpenAccessDataProvider"),
          ItemType = typeof (Image).FullName,
          ParentType = typeof (Album).FullName,
          DetailsViewUrl = ImageControlResolver.GetDetailsViewUrl()
        }
      };
    }

    public string GetFieldType() => this.fieldType;

    private static string GetDetailsViewUrl() => VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ContentViewEditDialog?" + "ControlDefinitionName=" + "ImagesBackend" + "&ViewName=" + "ImagesBackendEdit" + "&IsInlineEditingMode=true");
  }
}
