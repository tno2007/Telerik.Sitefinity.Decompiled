// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Data.OpenAccessMultisiteProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Data.SqlGenerators;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Multisite.Data
{
  /// <summary>
  /// Implements the Multisite management data layer with OpenAccess
  /// </summary>
  public class OpenAccessMultisiteProvider : 
    MultisiteDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IOpenAccessUpgradableProvider
  {
    /// <inheritdoc />
    public override Site CreateSite() => this.CreateSite(this.GetNewGuid());

    /// <inheritdoc />
    public override Site CreateSite(Guid siteId)
    {
      Site site = new Site(this.ApplicationName, siteId);
      site.Provider = (object) this;
      this.providerDecorator.CreatePermissionInheritanceAssociation(this.GetSecurityRoot() ?? throw new InvalidOperationException(string.Format(Res.Get<SecurityResources>().NoSecurityRoot, (object) typeof (Site).AssemblyQualifiedName)), (ISecuredObject) site);
      site.Owner = SecurityManager.CurrentUserId;
      if (siteId != Guid.Empty)
        this.GetContext().Add((object) site);
      return site;
    }

    /// <inheritdoc />
    public override void Delete(Site site)
    {
      foreach (SiteDataSourceLink entity in site.SiteDataSourceLinks.ToArray<SiteDataSourceLink>())
        this.GetContext().Remove((object) entity);
      ISecuredObject securityRoot = this.GetSecurityRoot();
      if (securityRoot != null)
        this.providerDecorator.DeletePermissionsInheritanceAssociation(securityRoot, (ISecuredObject) site);
      this.GetContext().Remove((object) site);
    }

    /// <inheritdoc />
    public override Site GetSite(Guid siteId)
    {
      Site itemById = this.GetContext().GetItemById<Site>(siteId.ToString());
      itemById.Provider = (object) this;
      return itemById;
    }

    /// <inheritdoc />
    public override IQueryable<Site> GetSites()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Site>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Site>((Expression<Func<Site, bool>>) (c => c.ApplicationName == appName));
    }

    /// <inheritdoc />
    public override void ChangeOwner(Site site, Guid newOwnerID)
    {
      if (site == null || !(newOwnerID != Guid.Empty) || !(site.Owner != newOwnerID))
        return;
      site.Owner = newOwnerID;
    }

    /// <inheritdoc />
    public override SiteDataSource CreateDataSource(string name, string provider)
    {
      SiteDataSource entity = new SiteDataSource()
      {
        Id = this.GetNewGuid(),
        Name = name,
        Provider = provider
      };
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    public override IQueryable<SiteDataSource> GetDataSources() => this.GetContext().GetAll<SiteDataSource>();

    /// <inheritdoc />
    public override void Delete(SiteDataSource source) => this.GetContext().Remove((object) source);

    /// <inheritdoc />
    public override SiteDataSourceLink CreateSiteDataSourceLink() => this.CreateSiteDataSourceLink(this.GetNewGuid());

    /// <inheritdoc />
    public override SiteDataSourceLink CreateSiteDataSourceLink(Guid sourceId)
    {
      SiteDataSourceLink entity = new SiteDataSourceLink()
      {
        Id = sourceId
      };
      if (sourceId != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    public override void Delete(SiteDataSourceLink source) => this.GetContext().Remove((object) source);

    /// <inheritdoc />
    public override SiteDataSourceLink GetSiteDataSourceLink(
      Guid siteDataSourceLinkId)
    {
      return this.GetContext().GetItemById<SiteDataSourceLink>(siteDataSourceLinkId.ToString());
    }

    /// <inheritdoc />
    public override IQueryable<SiteDataSourceLink> GetSiteDataSourceLinks() => this.GetContext().GetAll<SiteDataSourceLink>();

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new MultisiteMetadataSource(context);

    /// <inheritdoc />
    public int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    /// <inheritdoc />
    public void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
    }

    /// <inheritdoc />
    public void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
      if (context == null || upgradedFromSchemaVersionNumber <= 0 || upgradedFromSchemaVersionNumber >= 7600)
        return;
      this.UpgradeDataSourceLinks(context);
      this.DropOrphanedTablesConstraint(context);
    }

    private void DropOrphanedTablesConstraint(UpgradingContext context)
    {
      try
      {
        using (OACommand command = ((OpenAccessContextBase) context).Connection.CreateCommand())
        {
          string dropConstraint = SqlGenerator.Get(context.DatabaseContext.DatabaseType).GetDropConstraint("sf_sts_sf_st_data_source_links", "ref_sf_sts_sf_st_dt_s_EA6AAC25");
          command.CommandText = dropConstraint;
          command.ExecuteNonQuery();
          context.SaveChanges();
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void UpgradeDataSourceLinks(UpgradingContext context)
    {
      List<Site> list1 = context.GetAll<Site>().ToList<Site>();
      List<SiteDataSource> list2 = context.GetAll<SiteDataSource>().ToList<SiteDataSource>();
      List<SiteDataSourceLink> list3 = context.GetAll<SiteDataSourceLink>().ToList<SiteDataSourceLink>();
      List<object> source = new List<object>();
      using (OACommand command = ((OpenAccessContextBase) context).Connection.CreateCommand())
      {
        SqlGenerator sqlGenerator = SqlGenerator.Get(context.DatabaseContext.DatabaseType);
        Telerik.Sitefinity.Data.SqlGenerators.Table table = new Telerik.Sitefinity.Data.SqlGenerators.Table("sf_site_data_source_links");
        List<Column> columnList = new List<Column>()
        {
          new Column("dataSource_name"),
          new Column("provider_name"),
          new Column("is_default"),
          new Column("site_id")
        };
        Telerik.Sitefinity.Data.SqlGenerators.Table fromTable = table;
        List<Column> columnNames = columnList;
        string select = sqlGenerator.GetSelect(fromTable, columnNames);
        command.CommandText = select;
        using (OADataReader oaDataReader = command.ExecuteReader())
        {
          while (oaDataReader.Read())
          {
            bool boolean = Convert.ToBoolean(oaDataReader.GetValue(2));
            object obj = oaDataReader.GetValue(3);
            if (!(obj is Guid guid1))
              guid1 = Guid.Parse(obj.ToString());
            Guid guid2 = guid1;
            source.Add((object) new
            {
              DataSourceName = oaDataReader.GetString(0),
              ProviderName = oaDataReader.GetString(1),
              IsDefault = boolean,
              SiteId = guid2
            });
          }
        }
      }
      foreach (IGrouping<\u003C\u003Ef__AnonymousType45<object, object>, object> grouping in source.GroupBy(l =>
      {
        // ISSUE: reference to a compiler-generated field
        if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ProviderName", typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj12 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__0.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__0, l);
        // ISSUE: reference to a compiler-generated field
        if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "DataSourceName", typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj13 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__1.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__1, l);
        return new
        {
          ProviderName = obj12,
          DataSourceName = obj13
        };
      }))
      {
        IGrouping<\u003C\u003Ef__AnonymousType45<object, object>, object> group = grouping;
        SiteDataSource siteDataSource = list2.FirstOrDefault<SiteDataSource>((Func<SiteDataSource, bool>) (sds =>
        {
          // ISSUE: reference to a compiler-generated field
          if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__6 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (OpenAccessMultisiteProvider)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, bool> target1 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__6.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, bool>> p6 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__6;
          // ISSUE: reference to a compiler-generated field
          if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__2 = CallSite<Func<CallSite, string, object, StringComparison, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Equals", (IEnumerable<Type>) null, typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__2.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__2, sds.Provider, group.Key.ProviderName, StringComparison.OrdinalIgnoreCase);
          // ISSUE: reference to a compiler-generated field
          if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          object obj2;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (!OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__5.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__5, obj1))
          {
            // ISSUE: reference to a compiler-generated field
            if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__4 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, object, object> target2 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__4.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, object, object>> p4 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__4;
            object obj3 = obj1;
            // ISSUE: reference to a compiler-generated field
            if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__3 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__3 = CallSite<Func<CallSite, string, object, StringComparison, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Equals", (IEnumerable<Type>) null, typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj4 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__3.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__3, sds.Name, group.Key.DataSourceName, StringComparison.OrdinalIgnoreCase);
            obj2 = target2((CallSite) p4, obj3, obj4);
          }
          else
            obj2 = obj1;
          return target1((CallSite) p6, obj2);
        }));
        if (siteDataSource == null)
        {
          SiteDataSource siteDataSource1 = new SiteDataSource();
          siteDataSource1.Id = Guid.NewGuid();
          // ISSUE: reference to a compiler-generated field
          if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__7 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (OpenAccessMultisiteProvider)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          siteDataSource1.Name = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__7.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__7, group.Key.DataSourceName);
          // ISSUE: reference to a compiler-generated field
          if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__8 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (OpenAccessMultisiteProvider)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          siteDataSource1.Provider = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__8.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__8, group.Key.ProviderName);
          siteDataSource = siteDataSource1;
          context.Add((object) siteDataSource);
        }
        foreach (object obj5 in (IEnumerable<object>) group)
        {
          object link = obj5;
          Site site = list1.SingleOrDefault<Site>((Func<Site, bool>) (s =>
          {
            // ISSUE: reference to a compiler-generated field
            if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__11 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (OpenAccessMultisiteProvider)));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, bool> target3 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__11.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, bool>> p11 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__11;
            // ISSUE: reference to a compiler-generated field
            if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__10 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__10 = CallSite<Func<CallSite, Guid, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, Guid, object, object> target4 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__10.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, Guid, object, object>> p10 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__10;
            Guid id = s.Id;
            // ISSUE: reference to a compiler-generated field
            if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__9 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "SiteId", typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj6 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__9.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__9, link);
            object obj7 = target4((CallSite) p10, id, obj6);
            return target3((CallSite) p11, obj7);
          }));
          if (site != null && !list3.Any<SiteDataSourceLink>((Func<SiteDataSourceLink, bool>) (sdsl =>
          {
            // ISSUE: reference to a compiler-generated field
            if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__16 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (OpenAccessMultisiteProvider)));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, bool> target5 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__16.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, bool>> p16 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__16;
            // ISSUE: reference to a compiler-generated field
            if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__13 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__13 = CallSite<Func<CallSite, Guid, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, Guid, object, object> target6 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__13.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, Guid, object, object>> p13 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__13;
            Guid siteId = sdsl.SiteId;
            // ISSUE: reference to a compiler-generated field
            if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__12 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "SiteId", typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj9 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__12.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__12, link);
            object obj10 = target6((CallSite) p13, siteId, obj9);
            // ISSUE: reference to a compiler-generated field
            if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__15 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            object obj11;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (!OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__15.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__15, obj10))
            {
              // ISSUE: reference to a compiler-generated field
              if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__14 == null)
              {
                // ISSUE: reference to a compiler-generated field
                OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              obj11 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__14.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__14, obj10, sdsl.DataSourceId == siteDataSource.Id);
            }
            else
              obj11 = obj10;
            return target5((CallSite) p16, obj11);
          })))
          {
            SiteDataSourceLink entity = new SiteDataSourceLink()
            {
              Id = Guid.NewGuid()
            };
            context.Add((object) entity);
            entity.DataSource = siteDataSource;
            SiteDataSourceLink siteDataSourceLink = entity;
            // ISSUE: reference to a compiler-generated field
            if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__18 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (OpenAccessMultisiteProvider)));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, bool> target = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__18.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, bool>> p18 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__18;
            // ISSUE: reference to a compiler-generated field
            if (OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__17 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "IsDefault", typeof (OpenAccessMultisiteProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj8 = OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__17.Target((CallSite) OpenAccessMultisiteProvider.\u003C\u003Eo__24.\u003C\u003Ep__17, link);
            int num = target((CallSite) p18, obj8) ? 1 : 0;
            siteDataSourceLink.IsDefault = num != 0;
            site.SiteDataSourceLinks.Add(entity);
          }
        }
      }
      context.SaveChanges();
    }
  }
}
