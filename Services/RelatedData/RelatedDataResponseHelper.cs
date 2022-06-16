// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.RelatedDataResponseHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.ModuleEditor;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Services.RelatedData
{
  /// <summary>
  /// Helper class providing helper methods used in related data service responses
  /// </summary>
  internal class RelatedDataResponseHelper
  {
    private static readonly Dictionary<string, string> displayNames = new Dictionary<string, string>();
    private static readonly Dictionary<string, RelatedDataResponseHelper.StaticTypeDefinition> staticTypeDefinitions = new Dictionary<string, RelatedDataResponseHelper.StaticTypeDefinition>()
    {
      {
        "Telerik.Sitefinity.News.Model.NewsItem",
        new RelatedDataResponseHelper.StaticTypeDefinition()
        {
          ControlDefinitionName = "NewsBackend",
          BackendEditViewName = "NewsBackendEdit",
          BackendInsertViewName = "NewsBackendInsert"
        }
      },
      {
        "Telerik.Sitefinity.Events.Model.Event",
        new RelatedDataResponseHelper.StaticTypeDefinition()
        {
          ControlDefinitionName = "EventsBackend",
          BackendEditViewName = "EventsBackendEdit",
          BackendInsertViewName = "EventsBackendInsert"
        }
      },
      {
        "Telerik.Sitefinity.Blogs.Model.BlogPost",
        new RelatedDataResponseHelper.StaticTypeDefinition()
        {
          ControlDefinitionName = "PostsBackend",
          BackendEditViewName = "BlogsBackendEditPost",
          BackendInsertViewName = "BlogsBackendInsertPost"
        }
      },
      {
        "Telerik.Sitefinity.Blogs.Model.Blog",
        new RelatedDataResponseHelper.StaticTypeDefinition()
        {
          ControlDefinitionName = "BlogsBackend",
          BackendEditViewName = "BlogsBackendEdit",
          BackendInsertViewName = "BlogsBackendInsert"
        }
      },
      {
        "Telerik.Sitefinity.Lists.Model.ListItem",
        new RelatedDataResponseHelper.StaticTypeDefinition()
        {
          ControlDefinitionName = "ItemsBackend",
          BackendEditViewName = "ListsBackendEditItem",
          BackendInsertViewName = "ListsBackendInsertItem"
        }
      },
      {
        "Telerik.Sitefinity.Libraries.Model.Image",
        new RelatedDataResponseHelper.StaticTypeDefinition()
        {
          ControlDefinitionName = "ImagesBackend",
          BackendEditViewName = "ImagesBackendEdit",
          BackendInsertViewName = ""
        }
      },
      {
        "Telerik.Sitefinity.Libraries.Model.Video",
        new RelatedDataResponseHelper.StaticTypeDefinition()
        {
          ControlDefinitionName = "VideosBackend",
          BackendEditViewName = "VideosBackendEdit",
          BackendInsertViewName = ""
        }
      },
      {
        "Telerik.Sitefinity.Libraries.Model.Document",
        new RelatedDataResponseHelper.StaticTypeDefinition()
        {
          ControlDefinitionName = "DocumentsBackend",
          BackendEditViewName = "DocumentsBackendEdit",
          BackendInsertViewName = ""
        }
      },
      {
        "Telerik.Sitefinity.Pages.Model.PageNode",
        new RelatedDataResponseHelper.StaticTypeDefinition()
        {
          ControlDefinitionName = "FrontendPages",
          BackendEditViewName = "FrontendPagesEdit",
          BackendInsertViewName = "FrontendPagesCreate"
        }
      }
    };

    /// <summary>Returns the items view url by the parameters sent</summary>
    /// <param name="id">item`s id</param>
    /// <param name="type">item`s type</param>
    /// <param name="providerName">item`s provider name</param>
    /// <param name="culture">culture</param>
    internal static string GetItemViewUrl(
      string id,
      string type,
      string providerName,
      string culture)
    {
      return RouteHelper.ResolveUrl("~/Sitefinity/ContentLocation/Preview" + new NameValueCollection()
      {
        {
          "item_id",
          id
        },
        {
          "item_type",
          type
        },
        {
          "item_provider",
          providerName
        },
        {
          "item_culture",
          culture
        }
      }.ToQueryString(), UrlResolveOptions.Absolute);
    }

    /// <summary>Gets the details view URL require for item editing.</summary>
    internal static string GetDetailsViewUrl(Type itemType)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      RelatedDataResponseHelper.GetBackendViewUrl(itemType, true, ref empty1, ref empty2);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("~/Sitefinity/Dialog/ContentViewEditDialog?").Append("ControlDefinitionName=").Append(empty1).Append("&ViewName=").Append(empty2).Append("&").Append("IsInlineEditingMode").Append("=true").Append("&backLabelText=").Append(Res.Get<ModuleEditorResources>().BackButtonLabel);
      if (RelatedDataResponseHelper.IsProductType(itemType))
      {
        stringBuilder.Append("&ItemType=");
        stringBuilder.Append(itemType.FullName);
      }
      return VirtualPathUtility.ToAbsolute(stringBuilder.ToString());
    }

    /// <summary>Gets the URL required for creating item.</summary>
    internal static string GetInsertViewUrl(Type itemType)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      RelatedDataResponseHelper.GetBackendViewUrl(itemType, false, ref empty1, ref empty2);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("~/Sitefinity/Dialog/ContentViewInsertDialog?").Append("ControlDefinitionName=").Append(empty1).Append("&ViewName=").Append(empty2).Append("&").Append("IsInlineEditingMode").Append("=true").Append("&backLabelText=").Append(Res.Get<ModuleEditorResources>().BackButtonLabel);
      if (RelatedDataResponseHelper.IsProductType(itemType))
      {
        stringBuilder.Append("&ItemType=");
        stringBuilder.Append(itemType.FullName);
      }
      return VirtualPathUtility.ToAbsolute(stringBuilder.ToString());
    }

    /// <summary>
    /// Gets the backend controlDefinitionName, and viewName of the provided item type
    /// </summary>
    /// <param name="isEdit">Get the definitions names in Edit mode. If false, returns in Insert mode.</param>
    internal static void GetBackendViewUrl(
      Type itemType,
      bool isEdit,
      ref string controlDefinitionName,
      ref string viewName)
    {
      if (typeof (DynamicContent).IsAssignableFrom(itemType))
      {
        controlDefinitionName = ModuleNamesGenerator.GenerateContentViewDefinitionName(itemType.FullName);
        string dynamicTypeDisplayName = RelatedDataResponseHelper.GetDynamicTypeDisplayName(itemType);
        viewName = isEdit ? ModuleNamesGenerator.GenerateBackendEditViewName(dynamicTypeDisplayName) : ModuleNamesGenerator.GenerateBackendInsertViewName(dynamicTypeDisplayName);
      }
      else
      {
        RelatedDataResponseHelper.StaticTypeDefinition valueOrDefault = RelatedDataResponseHelper.staticTypeDefinitions.GetValueOrDefault<RelatedDataResponseHelper.StaticTypeDefinition, string>(itemType.FullName);
        if (valueOrDefault != null)
        {
          controlDefinitionName = valueOrDefault.ControlDefinitionName;
          viewName = isEdit ? valueOrDefault.BackendEditViewName : valueOrDefault.BackendInsertViewName;
        }
        else if (RelatedDataResponseHelper.IsProductType(itemType))
        {
          controlDefinitionName = "ProductsBackendDefinitionName";
          viewName = (isEdit ? "edit_" : "insert_") + itemType.Name;
        }
        else
        {
          DetailFormViewElement detailFormViewElement = CustomFieldsContext.GetViews(itemType.FullName).Where<DetailFormViewElement>((Func<DetailFormViewElement, bool>) (v => !v.IsMasterView && v.DisplayMode == FieldDisplayMode.Write)).FirstOrDefault<DetailFormViewElement>();
          if (detailFormViewElement == null)
            return;
          controlDefinitionName = detailFormViewElement.ControlDefinitionName;
          viewName = detailFormViewElement.ViewName;
        }
      }
    }

    private static bool IsProductType(Type itemType)
    {
      Type type = TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product", false);
      return type != (Type) null && type.IsAssignableFrom(itemType);
    }

    private static string GetDynamicTypeDisplayName(Type type)
    {
      string dynamicTypeDisplayName;
      if (!RelatedDataResponseHelper.displayNames.TryGetValue(type.FullName, out dynamicTypeDisplayName))
      {
        lock (RelatedDataResponseHelper.displayNames)
        {
          if (!RelatedDataResponseHelper.displayNames.TryGetValue(type.FullName, out dynamicTypeDisplayName))
          {
            DynamicModuleType dynamicModuleType = ModuleBuilderManager.GetManager().GetDynamicModuleType(type);
            dynamicTypeDisplayName = dynamicModuleType == null ? string.Empty : dynamicModuleType.DisplayName;
            RelatedDataResponseHelper.displayNames.Add(type.FullName, dynamicTypeDisplayName);
          }
        }
      }
      return dynamicTypeDisplayName;
    }

    private class StaticTypeDefinition
    {
      public string ControlDefinitionName { get; set; }

      public string BackendEditViewName { get; set; }

      public string BackendInsertViewName { get; set; }
    }
  }
}
