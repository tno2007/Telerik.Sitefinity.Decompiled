// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Data.OpenAccessContentProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Hosting;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.GenericContent.Data
{
  /// <summary>
  /// Represents Generic Content data provider that uses OpenAccess to store and retrieve content data.
  /// </summary>
  [ContentProviderDecorator(typeof (OpenAccessContentDecorator))]
  public class OpenAccessContentProvider : 
    ContentDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IOpenAccessUpgradableProvider
  {
    /// <summary>Creates new content item.</summary>
    /// <returns>The new content item.</returns>
    public override ContentItem CreateContent() => this.CreateContent(this.GetNewGuid());

    /// <summary>Creates new content item with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new content.</param>
    /// <returns>The new content item.</returns>
    public override ContentItem CreateContent(Guid id)
    {
      ContentItem content = new ContentItem(this.ApplicationName, id);
      content.Owner = SecurityManager.GetCurrentUserId();
      content.Provider = (object) this;
      this.providerDecorator.CreatePermissionInheritanceAssociation(this.GetSecurityRoot() ?? throw new InvalidOperationException(string.Format(Telerik.Sitefinity.Localization.Res.Get<SecurityResources>().NoSecurityRoot, (object) typeof (ContentItem).AssemblyQualifiedName)), (ISecuredObject) content);
      if (id != Guid.Empty)
        this.GetContext().Add((object) content);
      return content;
    }

    /// <summary>Gets a content with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>A content item.</returns>
    public override ContentItem GetContent(Guid id)
    {
      ContentItem content = !(id == Guid.Empty) ? this.GetContext().GetItemById<ContentItem>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      content.Provider = (object) this;
      return content;
    }

    /// <summary>Gets a query for content items.</summary>
    /// <returns>The query for content items.</returns>
    public override IQueryable<ContentItem> GetContent()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<ContentItem>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<ContentItem>((Expression<Func<ContentItem, bool>>) (item => item.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The content item to delete.</param>
    public override void Delete(ContentItem item)
    {
      SitefinityOAContext context = this.GetContext();
      this.ClearLifecycle<ContentItem>(item, this.GetContent());
      ContentItem entity = item;
      context.Remove((object) entity);
      this.DeleteItemComments(item.GetType(), item.Id);
    }

    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new ContentMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <inheritdoc />
    public virtual int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    /// <inheritdoc />
    public virtual void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
    }

    /// <inheritdoc />
    public virtual void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
      if (upgradedFromSchemaVersionNumber <= 0)
        return;
      if (upgradedFromSchemaVersionNumber <= 1300)
        OpenAccessConnection.MsSqlUpgrade(context.Connection, "OpenAccessContentProvider: Upgrade to 1300 from 4.0, 4.0 SP1", (Action<IDbCommand>) (cmd =>
        {
          cmd.ExecuteNonQuery("ALTER TABLE sf_content_items ALTER COLUMN can_inherit_permissions TINYINT NULL");
          cmd.ExecuteNonQuery("ALTER TABLE sf_content_items ALTER COLUMN inherits_permissions TINYINT NULL");
        }));
      if (upgradedFromSchemaVersionNumber <= 1367)
        OpenAccessConnection.MsSqlUpgrade(context.Connection, "OpenAccessContentProvider: Upgrade to 1367", (Action<IDbCommand>) (cmd =>
        {
          cmd.ExecuteNonQuery("\r\n                        IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'ref_sf_cntnt_tms_sf_prmssns_s2') AND parent_object_id = OBJECT_ID(N'sf_cntent_items_sf_permissions'))\r\n                        ALTER TABLE sf_cntent_items_sf_permissions DROP CONSTRAINT ref_sf_cntnt_tms_sf_prmssns_s2\r\n                    ");
          cmd.ExecuteNonQuery("\r\n                        IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'ref_sf_cntnt_tms_sf_prmssns_sf') AND parent_object_id = OBJECT_ID(N'sf_cntent_items_sf_permissions'))\r\n                        ALTER TABLE sf_cntent_items_sf_permissions DROP CONSTRAINT ref_sf_cntnt_tms_sf_prmssns_sf\r\n                    ");
          cmd.ExecuteNonQuery("\r\n                        IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'sf_cntent_items_sf_permissions') AND type in (N'U'))\r\n                        DROP TABLE sf_cntent_items_sf_permissions\r\n                    ");
          cmd.ExecuteNonQuery("\r\n                        IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'ref_sf_cntnt_tms_sf_prmssns_n2') AND parent_object_id = OBJECT_ID(N'sf_cntnt_tms_sf_prmssns_nhrtnc'))\r\n                        ALTER TABLE sf_cntnt_tms_sf_prmssns_nhrtnc DROP CONSTRAINT ref_sf_cntnt_tms_sf_prmssns_n2\r\n                    ");
          cmd.ExecuteNonQuery("\r\n                        IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'ref_sf_cntnt_tms_sf_prmssns_nh') AND parent_object_id = OBJECT_ID(N'sf_cntnt_tms_sf_prmssns_nhrtnc'))\r\n                        ALTER TABLE sf_cntnt_tms_sf_prmssns_nhrtnc DROP CONSTRAINT ref_sf_cntnt_tms_sf_prmssns_nh\r\n                    ");
          cmd.ExecuteNonQuery("\r\n                        IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'sf_cntnt_tms_sf_prmssns_nhrtnc') AND type in (N'U'))\r\n                        DROP TABLE sf_cntnt_tms_sf_prmssns_nhrtnc\r\n                    ");
        }));
      if (upgradedFromSchemaVersionNumber < 1650)
      {
        IQueryable<ContentItem> all = context.GetAll<ContentItem>();
        string str1 = "";
        foreach (ContentItem contentItem1 in (IEnumerable<ContentItem>) all)
        {
          ContentItem content = contentItem1;
          if (!content.Visible && content.Status == ContentLifecycleStatus.Master)
          {
            ContentItem contentItem2 = all.Where<ContentItem>((Expression<Func<ContentItem, bool>>) (liveItem => (int) liveItem.Status == 2 && liveItem.OriginalContentId == content.Id && liveItem.Visible)).FirstOrDefault<ContentItem>();
            if (contentItem2 == null || contentItem2.Content != content.Content)
              str1 = str1 + (string) content.Title + "\r\n";
          }
        }
        if (!string.IsNullOrWhiteSpace(str1))
        {
          string path = HostingEnvironment.MapPath("~/App_Data/Sitefinity/Logs/Generic Content items that should be saved manually.log");
          string str2 = string.Format("Upgrade performed on build {0} on {1: yyyy-MM-dd hh:mm:ss}\r\n----------------------------------------------------------", (object) upgradedFromSchemaVersionNumber.ToString(), (object) DateTime.Now) + "\r\nThe following content items were not published in their latest version before the upgrade, and will need to be saved again manually in order to be available on pages.\r\n\r\nItem Title\r\n-----------\r\n";
          StreamWriter streamWriter = new StreamWriter(path);
          streamWriter.WriteLine(str2 + str1);
          streamWriter.Close();
        }
      }
      if (upgradedFromSchemaVersionNumber >= 1700)
        return;
      OpenAccessConnection.FixDynamicLinks<ContentItem>(context, (IOpenAccessMetadataProvider) this);
    }

    /// <summary>Creates a language data item</summary>
    /// <returns></returns>
    public override LanguageData CreateLanguageData() => this.CreateLanguageData(this.GetNewGuid());

    /// <summary>Creates a language data item</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override LanguageData CreateLanguageData(Guid id)
    {
      LanguageData entity = new LanguageData(this.ApplicationName, id);
      ((IDataItem) entity).Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets language data item by its id</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override LanguageData GetLanguageData(Guid id)
    {
      LanguageData languageData = !(id == Guid.Empty) ? this.GetContext().GetItemById<LanguageData>(id.ToString()) : throw new ArgumentException("Argument 'id' cannot be empty GUID.");
      ((IDataItem) languageData).Provider = (object) this;
      return languageData;
    }

    /// <summary>Gets a query of all language data items</summary>
    /// <returns></returns>
    public override IQueryable<LanguageData> GetLanguageData()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<LanguageData>((DataProviderBase) this).Where<LanguageData>((Expression<Func<LanguageData, bool>>) (c => c.ApplicationName == appName));
    }
  }
}
