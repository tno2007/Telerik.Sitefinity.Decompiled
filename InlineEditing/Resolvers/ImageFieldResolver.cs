// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Resolvers.ImageFieldResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services.InlineEditing;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.InlineEditing.Resolvers
{
  internal class ImageFieldResolver : InlineEditingResolverBase, IInlineEditingResolver
  {
    private string fieldType = "ImageField";

    public object GetFieldValue(Guid id, string itemType, string fieldName, string provider)
    {
      ILifecycleDataItem component = (ManagerBase.GetMappedManager(itemType, provider) as ILifecycleManager).GetItem(TypeResolutionService.ResolveType(itemType), id) as ILifecycleDataItem;
      ContentLink contentLink = (TypeDescriptor.GetProperties((object) component)[fieldName].GetValue((object) component) as ContentLink[])[0];
      Image image = LibrariesManager.GetManager(contentLink.ChildItemProviderName).GetImage(contentLink.ChildItemId);
      return (object) new ImageFieldModel()
      {
        Image = new ImageModel()
        {
          Id = image.Id,
          ProviderName = contentLink.ChildItemProviderName,
          Title = (string) image.Title,
          DateCreated = image.DateCreated,
          Extension = image.Extension,
          TotalSize = (image.TotalSize / 1024L),
          ThumbnailName = image.Thumbnail.Name,
          ThumbnailUrl = image.ThumbnailUrl,
          AlternativeText = (string) image.AlternativeText,
          Width = image.Width,
          Height = image.Height
        },
        DetailsViewInfo = new ImageDetailViewModel()
        {
          ItemId = image.Id,
          ParentId = image.Album.Id,
          CommandName = "editMediaContentProperties",
          ProviderName = contentLink.ChildItemProviderName,
          ItemType = typeof (Image).FullName,
          ParentType = typeof (Album).FullName,
          DetailsViewUrl = ImageFieldResolver.GetDetailsViewUrl()
        }
      };
    }

    public string GetFieldType() => this.fieldType;

    private static string GetDetailsViewUrl() => VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ContentViewEditDialog?" + "ControlDefinitionName=" + "ImagesBackend" + "&ViewName=" + "ImagesBackendEdit" + "&IsInlineEditingMode=true");
  }
}
