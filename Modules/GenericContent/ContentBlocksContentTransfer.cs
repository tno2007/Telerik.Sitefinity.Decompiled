// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.ContentBlocksContentTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// Implements functionality for converting items from content blocks content in transferable format.
  /// </summary>
  internal class ContentBlocksContentTransfer : StaticContentTransfer
  {
    private IEnumerable<ExportType> supportedTypes;
    private const string AreaName = "Content blocks";

    /// <inheritdoc />
    public override string Area => "Content blocks";

    /// <inheritdoc />
    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        if (this.supportedTypes == null)
        {
          IList<ExportType> exportTypeList = (IList<ExportType>) new List<ExportType>();
          ExportType exportType = new ExportType("Content blocks", typeof (ContentItem).FullName);
          exportTypeList.Add(exportType);
          this.supportedTypes = (IEnumerable<ExportType>) exportTypeList;
        }
        return this.supportedTypes;
      }
    }

    /// <inheritdoc />
    public override bool IsAvailableForCurrentSite() => SystemManager.IsModuleAccessible("GenericContent");

    /// <inheritdoc />
    public override IEnumerable<IQueryable<object>> GetItemsQueries(
      ExportParams parameters)
    {
      yield return (IQueryable<object>) ContentManager.GetManager(parameters.ProviderName).GetItems<ContentItem>().Where<ContentItem>((Expression<Func<ContentItem, bool>>) (i => (int) i.Status == 2 && i.Visible));
    }

    /// <inheritdoc />
    public override void CreateItem(WrapperObject transferableObject, string transactionName)
    {
      this.PrepareItemForImport(transferableObject);
      base.CreateItem(transferableObject, transactionName);
    }

    /// <inheritdoc />
    public override void Delete(string sourceName) => this.DeleteImportedData(sourceName, typeof (ContentManager), (IList<Type>) new List<Type>()
    {
      typeof (ContentItem)
    });

    /// <summary>
    /// Replaces all site map root node ids and media providers in the content's links with their mapped alternatives in the target site.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="siteMapRootNodeId">Target site SiteMapRootNodeId</param>
    /// <returns>The new content</returns>
    internal static string PrepareContentForImport(string content, Guid siteMapRootNodeId) => LinkParser.ResolveLinks(content, (GetItemUrl) ((key, id, resolveAsAbsoluteUrl, status) =>
    {
      string[] strArray = key.Split(new string[3]
      {
        "|",
        "%7C",
        "%7C".ToLower()
      }, StringSplitOptions.None);
      if (Guid.TryParse(strArray[0], out Guid _))
      {
        strArray[0] = siteMapRootNodeId.ToString();
      }
      else
      {
        string str = strArray[0];
        string defaultProvider = SystemManager.GetDefaultProvider(str == "images" ? typeof (Image).FullName : (str == "videos" ? typeof (Video).FullName : (str == "documents" ? typeof (Document).FullName : strArray[0])));
        if (defaultProvider != null)
          strArray[1] = defaultProvider.ToString();
      }
      return "[" + string.Join("|", strArray) + "]" + (object) id;
    }), (ResolveUrl) null, false);

    private void PrepareItemForImport(WrapperObject obj)
    {
      Guid frontendRootNodeId = SiteInitializer.CurrentFrontendRootNodeId;
      CultureInfo key = new CultureInfo(obj.GetProperty<string>("LangId"));
      object propertyOrNull = obj.GetPropertyOrNull("Content");
      switch (propertyOrNull)
      {
        case IDictionary<CultureInfo, string> _:
          IDictionary<CultureInfo, string> dictionary = propertyOrNull as IDictionary<CultureInfo, string>;
          if (dictionary[key] == null)
            break;
          string str1 = ContentBlocksContentTransfer.PrepareContentForImport(dictionary[key], frontendRootNodeId);
          dictionary[key] = str1;
          obj.SetOrAddProperty("Content", (object) dictionary);
          break;
        case string _:
          string str2 = ContentBlocksContentTransfer.PrepareContentForImport(propertyOrNull as string, frontendRootNodeId);
          obj.SetOrAddProperty("Content", (object) str2);
          break;
      }
    }
  }
}
