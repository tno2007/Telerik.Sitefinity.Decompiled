// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.BackendUIHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI
{
  internal static class BackendUIHelper
  {
    public static string GetCssClassByField(DynamicModuleField field, string mainShortTextFieldName)
    {
      switch (field.FieldType)
      {
        case FieldType.Unknown:
          if (field.SpecialType == FieldSpecialType.Actions)
            return "sfActions";
          if (field.SpecialType == FieldSpecialType.Author)
            return "sfAuthor";
          if (field.SpecialType == FieldSpecialType.PublicationDate || field.SpecialType == FieldSpecialType.DateCreated || field.SpecialType == FieldSpecialType.LastModified)
            return "sfPublicationDate";
          if (field.SpecialType == FieldSpecialType.UrlName)
            return "sfUrlName";
          return field.SpecialType == FieldSpecialType.Translations ? "sfTranslations" : "sfUnknown";
        case FieldType.ShortText:
          return field.Name.Equals(mainShortTextFieldName) ? "sfShortText sfTitle" : "sfShortText";
        case FieldType.LongText:
          return "sfLongText";
        case FieldType.MultipleChoice:
          return field.ChoiceRenderType == "Checkbox" ? "sfMultipleChoice" : "sfSingleChoice";
        case FieldType.YesNo:
          return "sfYesNo";
        case FieldType.Currency:
          return "sfCurrency";
        case FieldType.DateTime:
          return "sfDateTime";
        case FieldType.Number:
          return "sfNumber";
        case FieldType.Classification:
          return "sfClassification sfNewUI";
        case FieldType.Media:
        case FieldType.RelatedMedia:
          if (field.MediaType == "image")
            return "sfImage sfNewUI";
          if (field.MediaType == "file")
            return "sfDocument sfNewUI";
          if (field.MediaType == "video")
            return "sfVideo sfNewUI";
          throw new NotSupportedException();
        case FieldType.Guid:
          return "sfGuid";
        case FieldType.GuidArray:
          return "sfGuidArray";
        case FieldType.Choices:
          return field.ChoiceRenderType == "Checkbox" ? "sfMultipleChoice" : "sfSingleChoice";
        case FieldType.Address:
          return "sfAddressField";
        case FieldType.RelatedData:
          if (field.CanSelectMultipleItems)
            return "sfRelatedDataField sfRelatedDataMultiple sfNewUI";
          return field.RelatedDataType == typeof (PageNode).FullName ? "sfRelatedDataField sfRelatedPages sfNewUI" : "sfRelatedDataField sfNewUI";
        default:
          throw new NotSupportedException();
      }
    }

    /// <summary>Gets a link to the page with the specified id.</summary>
    public static string GetPageLink(Guid pageId)
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(pageId, false);
      return siteMapNode == null ? string.Empty : RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
    }
  }
}
