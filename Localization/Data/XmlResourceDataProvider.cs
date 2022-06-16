// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Data.XmlResourceDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Xml;

namespace Telerik.Sitefinity.Localization.Data
{
  /// <summary>
  /// Represents resource data provider that uses XML file to store and retrieve resource data.
  /// </summary>
  public class XmlResourceDataProvider : ResourceDataProvider
  {
    private int cacheExp;
    private string fileExt;
    private string resourceFolder;
    private readonly object cacheLock = new object();

    /// <summary>
    /// Gets the configured virtual path where global resource files will be stored.
    /// </summary>
    public virtual string GlobalResourcesFolder => this.resourceFolder;

    /// <inheritdoc />
    public override string[] GetAllClassIds()
    {
      List<string> stringList = new List<string>((IEnumerable<string>) base.GetAllClassIds());
      string path = HostingEnvironment.MapPath(this.resourceFolder);
      if (Directory.Exists(path))
      {
        foreach (string file in Directory.GetFiles(path, "*" + this.fileExt))
        {
          string fileName = Path.GetFileName(file);
          string str = fileName.Substring(0, fileName.IndexOf('.'));
          if (!stringList.Contains(str))
            stringList.Add(str);
        }
      }
      stringList.Sort();
      return stringList.ToArray();
    }

    /// <inheritdoc />
    public override ObjectInfoAttribute[] GetAllClassInfos()
    {
      List<ObjectInfoAttribute> source = new List<ObjectInfoAttribute>((IEnumerable<ObjectInfoAttribute>) base.GetAllClassInfos());
      string path = HostingEnvironment.MapPath(this.resourceFolder);
      if (Directory.Exists(path))
      {
        foreach (string file in Directory.GetFiles(path, "*" + this.fileExt))
        {
          string name = Path.GetFileNameWithoutExtension(file);
          if (!source.Any<ObjectInfoAttribute>((Func<ObjectInfoAttribute, bool>) (e => name.Equals(e.Name, StringComparison.OrdinalIgnoreCase))))
            source.Add(new ObjectInfoAttribute(name));
        }
      }
      source.Sort((Comparison<ObjectInfoAttribute>) ((x, y) => string.Compare(x.Name, y.Name)));
      return source.ToArray();
    }

    /// <inheritdoc />
    public override ResourceEntry GetResource(
      CultureInfo culture,
      string classId,
      string key)
    {
      return this.GetResources(culture, classId).Where<ResourceEntry>((Expression<Func<ResourceEntry, bool>>) (r => r.Key == key)).SingleOrDefault<ResourceEntry>();
    }

    /// <inheritdoc />
    public override ResourceEntry GetResourceOrEmpty(
      CultureInfo culture,
      string classId,
      string key)
    {
      return this.GetResources(culture, classId).Where<ResourceEntry>((Expression<Func<ResourceEntry, bool>>) (r => r.Key == key)).SingleOrDefault<ResourceEntry>() ?? new ResourceEntry(classId, culture, key, string.Empty, string.Empty, DateTime.UtcNow);
    }

    /// <inheritdoc />
    public override IQueryable<ResourceEntry> GetResources(
      CultureInfo culture)
    {
      List<ResourceEntry> source = new List<ResourceEntry>();
      foreach (string allClassId in this.GetAllClassIds())
        source.AddRange(this.GetResourcesWithTransaction(culture, allClassId));
      return source.AsQueryable<ResourceEntry>();
    }

    /// <inheritdoc />
    public override IQueryable<ResourceEntry> GetResources(
      CultureInfo culture,
      string classId)
    {
      return this.GetResourcesWithTransaction(culture, classId).AsQueryable<ResourceEntry>();
    }

    private IEnumerable<ResourceEntry> GetResourcesWithTransaction(
      CultureInfo culture,
      string classId)
    {
      XmlResourceEntryCollection resourcesWithTransaction = this.LoadCachedResourcesWithFallBack(classId, culture);
      foreach (XmlResourceEntry entry in (Collection<XmlResourceEntry>) resourcesWithTransaction)
        this.AddItemInternal(entry, DataItemStatus.Loaded);
      return (IEnumerable<ResourceEntry>) resourcesWithTransaction;
    }

    /// <inheritdoc />
    public override IQueryable<ResourceEntry> GetResourcesForCultures(
      IQueryable<ResourceEntry> invariantCultureResources,
      params CultureInfo[] cultures)
    {
      List<ResourceEntry> source = new List<ResourceEntry>((IEnumerable<ResourceEntry>) invariantCultureResources);
      foreach (ResourceEntry invariantCultureResource in (IEnumerable<ResourceEntry>) invariantCultureResources)
      {
        foreach (CultureInfo culture in cultures)
          source.Add(this.GetResourceOrEmpty(culture, invariantCultureResource.ClassId, invariantCultureResource.Key));
      }
      return source.AsQueryable<ResourceEntry>();
    }

    /// <inheritdoc />
    public override void DeleteResourceEntry(CultureInfo culture, string classId, string key)
    {
      ResourceEntry resource = this.GetResource(culture, classId, key);
      if (resource == null)
        return;
      this.DeleteItem((object) resource);
    }

    /// <inheritdoc />
    public override ResourceEntry AddItem(
      CultureInfo culture,
      string classId,
      string key,
      string value,
      string description)
    {
      ResourceEntry resource = this.GetResource(culture, classId, key);
      if (resource == null)
        culture = CultureInfo.InvariantCulture;
      XmlResourceEntry xmlResourceEntry = new XmlResourceEntry(classId, culture, key);
      xmlResourceEntry.Value = value;
      xmlResourceEntry.Description = description;
      xmlResourceEntry.LastModified = DateTime.UtcNow;
      XmlResourceEntry entry = xmlResourceEntry;
      if (resource != null && object.Equals((object) culture, (object) resource.Culture))
      {
        this.UpdateItemInternal(entry, DataItemStatus.Changed);
        return (ResourceEntry) entry;
      }
      this.AddItemInternal(entry, DataItemStatus.New, false);
      return (ResourceEntry) entry;
    }

    /// <inheritdoc />
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      string s = config["cacheExpirationInMinutes"];
      this.cacheExp = string.IsNullOrEmpty(s) ? 10 : int.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
      config.Remove("cacheExpirationInMinutes");
      string str1 = config["globalResourcesFileExtension"];
      this.fileExt = string.IsNullOrEmpty(str1) ? ".resx" : str1;
      config.Remove("globalResourcesFileExtension");
      string str2 = config["globalResourcesFolder"];
      this.resourceFolder = string.IsNullOrEmpty(str2) ? "~/App_Data/Sitefinity/GlobalResources/" : str2;
      config.Remove("globalResourcesFolder");
      base.Initialize(providerName, config, managerType);
    }

    /// <inheritdoc />
    public override string GetString(CultureInfo culture, string classId, string key)
    {
      XmlResourceEntry xmlResourceEntry = (XmlResourceEntry) null;
      CultureInfo cultureInfo = culture;
      while (!this.LoadCachedResources(classId, cultureInfo).TryGetValue(ResourceEntry.GetUniqueKey(classId, key, cultureInfo), out xmlResourceEntry) && !object.Equals((object) cultureInfo, (object) CultureInfo.InvariantCulture))
        cultureInfo = cultureInfo.Parent;
      return xmlResourceEntry?.Value;
    }

    public override void RefreshResource(CultureInfo culture, string classId)
    {
      string classCultureKey = this.GetClassCultureKey(classId, culture);
      CacheDependency.Notify((IList<CacheDependencyKey>) new List<CacheDependencyKey>()
      {
        new CacheDependencyKey()
        {
          Key = classCultureKey,
          Type = typeof (XmlResourceDataProvider.XmlCacheDependencyObj)
        }
      });
    }

    /// <inheritdoc />
    public override IDictionary<CultureInfo, IDictionary<string, string>> GetResourceSet(
      string classId)
    {
      return (IDictionary<CultureInfo, IDictionary<string, string>>) null;
    }

    /// <summary>
    /// Adds <see cref="T:Telerik.Sitefinity.Localization.Data.XmlResourceEntry" /> to the current transaction.
    /// The item is persisted when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be added.</param>
    [Obsolete]
    public virtual void AddItem(object item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      if (!(item is XmlResourceEntry xmlResourceEntry))
        throw new ArgumentException(string.Format("Cannot cast {0} to Telerik.Sitefinity.Localization.Data.XmlResourceEntry.", (object) item.GetType().FullName), nameof (item));
      this.AddItem(xmlResourceEntry.Culture, xmlResourceEntry.ClassId, xmlResourceEntry.Key, xmlResourceEntry.Value, xmlResourceEntry.Description);
    }

    private void AddItemInternal(XmlResourceEntry entry, DataItemStatus status, bool check = true)
    {
      bool flag = this.GetTransaction().Contains(entry.GetUniqueKey());
      if (!(check & flag))
        this.GetTransaction().Add(entry);
      if (entry.DataStatus != DataItemStatus.Undefined)
        return;
      entry.DataStatus = DataItemStatus.New;
    }

    private void UpdateItemInternal(XmlResourceEntry entry, DataItemStatus status, bool check = true)
    {
      bool flag = this.GetTransaction().Contains(entry.GetUniqueKey());
      if (!(check & flag))
        return;
      XmlResourceEntry xmlResourceEntry = this.GetTransaction()[entry.GetUniqueKey()];
      xmlResourceEntry.DataStatus = status;
      xmlResourceEntry.Value = entry.Value;
      xmlResourceEntry.Description = entry.Description;
      xmlResourceEntry.LastModified = entry.LastModified;
    }

    /// <inheritdoc />
    public override void DeleteItem(object item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      AppPermission.Demand(AppAction.ManageLabels);
      if (!(item is XmlResourceEntry xmlResourceEntry))
        throw new ArgumentException(string.Format("Cannot cast {0} to Telerik.Sitefinity.Localization.Data.XmlResourceEntry.", (object) item.GetType().FullName), nameof (item));
      this.GetTransaction().Remove(xmlResourceEntry);
    }

    private ISet<string> GetRequiredKeys(string classId)
    {
      this.GetClassCultureKey(classId, CultureInfo.InvariantCulture);
      return (ISet<string>) new HashSet<string>(this.LoadCachedResources(classId, CultureInfo.InvariantCulture).Select<XmlResourceEntry, string>((Func<XmlResourceEntry, string>) (x => x.Key)));
    }

    private XmlResourceEntryCollection LoadAndAddToCache(
      string classId,
      CultureInfo culture)
    {
      string classCultureKey = this.GetClassCultureKey(classId, culture);
      XmlResourceEntryCollection cache = this.LoadResources(this.GetPath(classId, culture, this.resourceFolder, this.fileExt), classId, culture);
      this.Cache.Add(classCultureKey, (object) cache, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes((double) this.cacheExp)), (ICacheItemExpiration) new DataItemCacheDependency(typeof (XmlResourceDataProvider.XmlCacheDependencyObj), classCultureKey));
      return cache;
    }

    private XmlResourceEntryCollection LoadCachedResourcesWithFallBack(
      string classId,
      CultureInfo culture)
    {
      ISet<string> requiredKeys = this.GetRequiredKeys(classId);
      XmlResourceEntryCollection resourceEntryCollection = new XmlResourceEntryCollection();
      CultureInfo cultureInfo = culture;
      while (requiredKeys.Count > 0)
      {
        foreach (XmlResourceEntry loadCachedResource in (Collection<XmlResourceEntry>) this.LoadCachedResources(classId, cultureInfo))
        {
          if (requiredKeys.Contains(loadCachedResource.Key))
          {
            resourceEntryCollection.Add(loadCachedResource);
            requiredKeys.Remove(loadCachedResource.Key);
          }
        }
        if (requiredKeys.Count != 0 && !object.Equals((object) cultureInfo, (object) CultureInfo.InvariantCulture))
          cultureInfo = cultureInfo.Parent;
        else
          break;
      }
      return resourceEntryCollection;
    }

    private XmlResourceEntryCollection LoadCachedResources(
      string classId,
      CultureInfo culture)
    {
      if (string.IsNullOrEmpty(classId))
        throw new ArgumentNullException(nameof (classId));
      string key = culture != null ? this.GetClassCultureKey(classId, culture) : throw new ArgumentNullException(nameof (culture));
      if (!(this.Cache.GetData(key) is XmlResourceEntryCollection resourceEntryCollection1))
      {
        lock (this.cacheLock)
        {
          if (!(this.Cache.GetData(key) is XmlResourceEntryCollection resourceEntryCollection1))
            resourceEntryCollection1 = this.LoadAndAddToCache(classId, culture);
        }
      }
      return resourceEntryCollection1;
    }

    private ICacheManager Cache => SystemManager.GetCacheManager(CacheManagerInstance.LocalizationResources);

    private XmlResourceEntryCollection LoadResources(
      string filePath,
      string classId,
      CultureInfo requestedCulture)
    {
      XmlResourceEntryCollection resourceEntryCollection = new XmlResourceEntryCollection();
      if (File.Exists(filePath))
      {
        foreach (XElement element in XDocument.Load(filePath).Root.Elements((XName) "data"))
        {
          XmlResourceEntry xmlResourceEntry = new XmlResourceEntry(classId, requestedCulture, element)
          {
            DataStatus = DataItemStatus.Loaded
          };
          resourceEntryCollection.Add(xmlResourceEntry);
        }
      }
      if (object.Equals((object) requestedCulture, (object) CultureInfo.InvariantCulture))
      {
        foreach (XmlResourceEntry embeddedResource in (Collection<XmlResourceEntry>) this.LoadEmbeddedResources(classId, requestedCulture))
        {
          if (!resourceEntryCollection.Contains(embeddedResource.GetUniqueKey()))
            resourceEntryCollection.Add(embeddedResource);
        }
      }
      return resourceEntryCollection;
    }

    private XmlResourceEntryCollection LoadEmbeddedResources(
      string classId,
      CultureInfo culture)
    {
      XmlResourceEntryCollection resourceEntryCollection = new XmlResourceEntryCollection();
      PropertyCollection properties = Res.GetProperties(classId, false);
      if (properties != null)
      {
        foreach (ResourceProperty resourceProperty in (Collection<ResourceProperty>) properties)
        {
          XmlResourceEntry xmlResourceEntry = new XmlResourceEntry(classId, culture, resourceProperty.Key);
          xmlResourceEntry.Value = resourceProperty.DefaultValue;
          xmlResourceEntry.Description = resourceProperty.Description;
          xmlResourceEntry.LastModified = resourceProperty.LastModified;
          xmlResourceEntry.DataStatus = DataItemStatus.Loaded;
          resourceEntryCollection.Add(xmlResourceEntry);
        }
      }
      return resourceEntryCollection;
    }

    internal string GetPath(
      string classId,
      CultureInfo culture,
      string resourceFolder,
      string extension)
    {
      string str = VirtualPathUtility.AppendTrailingSlash(resourceFolder);
      string virtualPath;
      if (culture.Equals((object) CultureInfo.InvariantCulture))
        virtualPath = str + classId + extension;
      else
        virtualPath = str + classId + "." + culture.Name + extension;
      return HostingEnvironment.MapPath(virtualPath);
    }

    public override IDictionary<string, string> LoadResource(
      CultureInfo culture,
      string classId)
    {
      return (IDictionary<string, string>) null;
    }

    /// <summary>Gets the transaction for the current scope.</summary>
    protected XmlResouceEntryContext GetTransaction() => (XmlResouceEntryContext) base.GetTransaction();

    /// <summary>This method returns new transaction object.</summary>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>The transaction object.</returns>
    protected internal override object CreateNewTransaction(string transactionName) => (object) new XmlResouceEntryContext();

    /// <summary>Commits the provided transaction.</summary>
    public override void CommitTransaction()
    {
      if (!this.SuppressSecurityChecks)
        AppPermission.Demand(AppAction.ManageLabels);
      XmlResouceEntryContext transaction = this.GetTransaction();
      this.SaveDocuments((IEnumerable<XmlResourceEntry>) transaction);
      transaction.Clear();
    }

    private void SaveDocuments(IEnumerable<XmlResourceEntry> entries)
    {
      if (entries.Count<XmlResourceEntry>() <= 0)
        return;
      string path = HostingEnvironment.MapPath(this.resourceFolder);
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      List<CacheDependencyKey> keys = new List<CacheDependencyKey>();
      this.ProcessEntries(entries, (Action<string, CultureInfo, IList<XmlResourceEntry>>) ((classId, culture, filtered) =>
      {
        if (!this.SaveDocument((IEnumerable<XmlResourceEntry>) filtered, classId, culture))
          return;
        keys.Add(new CacheDependencyKey()
        {
          Key = this.GetClassCultureKey(classId, culture),
          Type = typeof (XmlResourceDataProvider.XmlCacheDependencyObj)
        });
      }));
      CacheDependency.Notify((IList<CacheDependencyKey>) keys);
    }

    private bool SaveDocument(
      IEnumerable<XmlResourceEntry> entries,
      string classId,
      CultureInfo culture)
    {
      string path = this.GetPath(classId, culture, this.resourceFolder, this.fileExt);
      XDocument xdocument = (XDocument) null;
      if (File.Exists(path))
      {
        xdocument = XDocument.Load(path);
      }
      else
      {
        using (StreamReader streamReader = new StreamReader(typeof (XmlResourceDataProvider).Assembly.GetManifestResourceStream("Telerik.Sitefinity.Localization.Data.schema.xml")))
          xdocument = XDocument.Parse(streamReader.ReadToEnd());
      }
      bool flag1 = false;
      foreach (XmlResourceEntry xmlResourceEntry in entries.Where<XmlResourceEntry>((Func<XmlResourceEntry, bool>) (e => e.DataStatus != DataItemStatus.Loaded)))
      {
        XmlResourceEntry dirtyItem = xmlResourceEntry;
        bool flag2 = false;
        XElement xelement = xdocument.Root.Elements((XName) "data").FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Attribute((XName) "name").Value == dirtyItem.Key));
        if (this.SameAsParent(dirtyItem) || this.IsEmpty(dirtyItem) || dirtyItem.DataStatus == DataItemStatus.Deleted)
        {
          if (xelement != null)
          {
            xelement.Remove();
            flag1 = true;
            flag2 = true;
          }
        }
        else if (!this.SameAsFileSystem(dirtyItem, xelement) || dirtyItem.DataStatus == DataItemStatus.New)
        {
          if (xelement == null)
          {
            xelement = new XElement((XName) "data", (object) new XAttribute((XName) "name", (object) dirtyItem.Key));
            xdocument.Root.Add((object) xelement);
          }
          this.AddOrUpdateChildElement(xelement, "value", dirtyItem.Value);
          this.AddOrUpdateChildElement(xelement, "comment", dirtyItem.Description);
          this.AddOrUpdateChildElement(xelement, "lastModified", DateTime.UtcNow.ToString("s"));
          flag1 = true;
          flag2 = true;
        }
        if (flag2)
          this.FireEvent(dirtyItem);
      }
      if (!flag1)
        return false;
      xdocument.Document.Save(path);
      return true;
    }

    private void FireEvent(XmlResourceEntry entry)
    {
      string str = entry.DataStatus != DataItemStatus.New ? (entry.DataStatus != DataItemStatus.Deleted ? DataEventAction.Updated : DataEventAction.Deleted) : DataEventAction.Created;
      EventHub.Raise((IEvent) new ResourceEvent()
      {
        Action = str,
        ProviderName = this.Name,
        ClassId = entry.ClassId,
        Key = entry.Key,
        ItemType = typeof (ResourceEntry),
        Language = entry.CultureName
      }, false);
    }

    private bool SameAsFileSystem(XmlResourceEntry entry, XElement original) => original != null && string.Equals(original.Element((XName) "value").Value, entry.Value);

    private bool IsEmpty(XmlResourceEntry entry) => string.IsNullOrEmpty(entry.Value);

    private bool SameAsParent(XmlResourceEntry entry)
    {
      CultureInfo parent = entry.Culture.Parent;
      XmlResourceEntry xmlResourceEntry = this.LoadCachedResourcesWithFallBack(entry.ClassId, parent).FirstOrDefault<XmlResourceEntry>((Func<XmlResourceEntry, bool>) (x => x.Key == entry.Key));
      if (xmlResourceEntry != null)
      {
        if (!object.Equals((object) xmlResourceEntry.Culture, (object) entry.Culture) && string.Equals(xmlResourceEntry.Value, entry.Value))
          return true;
        if (object.Equals((object) xmlResourceEntry.Culture, (object) entry.Culture) && object.Equals((object) xmlResourceEntry.Culture, (object) CultureInfo.InvariantCulture))
        {
          PropertyCollection properties = Res.GetProperties(entry.ClassId, false);
          if (properties != null && properties.Contains(entry.Key) && string.Equals(properties[entry.Key].DefaultValue, entry.Value))
            return true;
        }
      }
      return false;
    }

    private void AddOrUpdateChildElement(XElement parent, string key, string value)
    {
      XElement content = parent.Element((XName) key);
      if (content == null)
      {
        content = new XElement((XName) key);
        parent.Add((object) content);
      }
      content.Value = value;
    }

    private string GetClassCultureKey(string classId, CultureInfo culture) => string.Format("{0}_{1}", (object) classId, (object) culture.Name);

    private void ProcessEntries(
      IEnumerable<XmlResourceEntry> entries,
      Action<string, CultureInfo, IList<XmlResourceEntry>> action)
    {
      foreach (IGrouping<string, XmlResourceEntry> source1 in entries.GroupBy<XmlResourceEntry, string>((Func<XmlResourceEntry, string>) (x => x.ClassId)))
      {
        foreach (IGrouping<CultureInfo, XmlResourceEntry> source2 in source1.GroupBy<XmlResourceEntry, CultureInfo>((Func<XmlResourceEntry, CultureInfo>) (x => x.Culture)))
          action(source1.Key, source2.Key, (IList<XmlResourceEntry>) source2.ToList<XmlResourceEntry>());
      }
    }

    /// <summary>Aborts the transaction for the current scope.</summary>
    public override void RollbackTransaction()
    {
      XmlResouceEntryContext transaction = this.GetTransaction();
      List<CacheDependencyKey> keys = new List<CacheDependencyKey>();
      this.ProcessEntries((IEnumerable<XmlResourceEntry>) transaction, (Action<string, CultureInfo, IList<XmlResourceEntry>>) ((classId, culture, filtered) => keys.Add(new CacheDependencyKey()
      {
        Type = typeof (XmlResourceDataProvider.XmlCacheDependencyObj),
        Key = this.GetClassCultureKey(classId, culture)
      })));
      transaction.Clear();
    }

    /// <summary>Gets the specified permission.</summary>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="objectId">The object pageId.</param>
    /// <param name="principalId">The principal pageId.</param>
    /// <returns></returns>
    public override Permission GetPermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      throw new NotSupportedException();
    }

    /// <summary>Gets a query for permissions.</summary>
    /// <returns></returns>
    public override IQueryable<Permission> GetPermissions() => throw new NotSupportedException();

    public override Permission CreatePermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      throw new NotImplementedException();
    }

    /// <summary>Deletes the permission.</summary>
    /// <param name="permission">The permission.</param>
    public override void DeletePermission(Permission permission) => throw new NotSupportedException();

    /// <summary>Gets the security root.</summary>
    /// <returns></returns>
    public override ISecuredObject GetSecurityRoot() => throw new NotSupportedException();

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    /// <returns></returns>
    public override ISecuredObject GetSecurityRoot(bool create) => throw new NotSupportedException();

    private class XmlCacheDependencyObj
    {
    }

    internal class Attributes
    {
      internal const string Data = "data";
      internal const string Name = "name";
      internal const string Value = "value";
      internal const string Comment = "comment";
      internal const string LastModified = "lastModified";
    }
  }
}
