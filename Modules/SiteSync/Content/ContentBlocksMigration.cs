// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ContentBlocksMigration
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.SiteSync
{
  /// <summary>
  /// Contains methods for fixing links to content in the target site.
  /// </summary>
  internal class ContentBlocksMigration
  {
    public const string IsMvcContentBlock = "IsMvcContentBlock";
    public const string MigratedValueKey = "migratedContent";
    public const string HtmlControlPropertyName = "Html";
    public const string MVCHtmlControlPropertyName = "Content";
    public const string ContentBlockFullTypeName = "Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock";

    /// <summary>
    /// Replaces all site map root node ids and media providers in the content block's links with their mapped alternatives in the target site.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="obj">The obj.</param>
    /// <param name="context">The context.</param>
    public static void PrepareContentBlockForMigration(
      ContentItem item,
      WrapperObject obj,
      ISiteSyncExportContext context)
    {
      string propertyOrDefault = obj.GetPropertyOrDefault<string>("LangId");
      Lstring lstring = item.GetString("Content");
      ContentBlocksMigration.PrepareContentBlockForMigration(propertyOrDefault == null ? lstring.Value : lstring[propertyOrDefault], obj, context);
    }

    /// <summary>
    /// Replaces all site map root node ids and media providers in the content block's links with their mapped alternatives in the target site.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="obj">The obj.</param>
    /// <param name="context">The context.</param>
    public static void PrepareContentBlockForMigration(
      string content,
      WrapperObject obj,
      ISiteSyncExportContext context)
    {
      string str1 = LinkParser.ResolveLinks(content, (GetItemUrl) ((key, id, resolveAsAbsoluteUrl, status) =>
      {
        string[] strArray = key.Split(new string[3]
        {
          "|",
          "%7C",
          "%7C".ToLower()
        }, StringSplitOptions.None);
        if (Guid.TryParse(strArray[0], out Guid _))
        {
          strArray[0] = context.TargetMicrosite.SiteMapRootNodeId.ToString();
        }
        else
        {
          string str2 = strArray[0];
          string typeName = str2 == "images" ? typeof (Image).FullName : (str2 == "videos" ? typeof (Video).FullName : (str2 == "documents" ? typeof (Document).FullName : strArray[0]));
          string empty = string.Empty;
          if (strArray.Length > 1)
            empty = strArray[1];
          object mapping = context.GetMapping(typeName, (object) empty, "Provider");
          if (mapping != null)
            strArray[1] = mapping.ToString();
        }
        return "[" + string.Join("|", strArray) + "]" + (object) id;
      }), (ResolveUrl) null, false);
      obj.AddProperty("migratedContent", (object) str1);
    }
  }
}
