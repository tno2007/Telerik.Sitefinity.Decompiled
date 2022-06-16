// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.FixMultilingualSiteTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Pages.Model.PropertyLoaders;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Scheduled task to clean and fix those pages, that have controls with excess properties or
  /// properties whose language is inconsistent with that of the current page.
  /// </summary>
  public class FixMultilingualSiteTask : ScheduledTask
  {
    private bool deleteRevisionHistory;
    private IList<Guid> includedPages;
    private Guid siteId;

    internal FixMultilingualSiteTask()
    {
    }

    public FixMultilingualSiteTask(Guid siteId) => this.siteId = !(siteId == Guid.Empty) ? siteId : throw new ArgumentException(nameof (siteId));

    /// <inheritdoc />
    public override void ExecuteTask() => this.StartInternal();

    /// <summary>
    /// Gets or sets a value weather to delete the revision history of the page that has
    /// inconsistent properties.
    /// </summary>
    public bool DeleteRevisionHistory
    {
      get => this.deleteRevisionHistory;
      set => this.deleteRevisionHistory = value;
    }

    /// <summary>
    /// Gets or sets a list of included pages from the site.
    /// The task will be executed on those pages only.
    /// </summary>
    public IList<Guid> IncludedPages
    {
      get
      {
        if (this.includedPages == null)
          this.includedPages = (IList<Guid>) new List<Guid>();
        return this.includedPages;
      }
      set => this.includedPages = value;
    }

    /// <inheritdoc />
    public override string GetCustomData()
    {
      string str = string.Empty;
      if (this.IncludedPages != null && this.IncludedPages.Count > 0)
        str = string.Join("|", this.IncludedPages.Select<Guid, string>((Func<Guid, string>) (x => x.ToString())));
      return string.Join("|", new string[3]
      {
        this.siteId.ToString(),
        this.DeleteRevisionHistory.ToString(),
        str
      });
    }

    /// <inheritdoc />
    public override void SetCustomData(string customData)
    {
      string[] strArray = customData.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      this.siteId = Guid.Parse(strArray[0]);
      this.DeleteRevisionHistory = bool.Parse(strArray[1]);
      if (strArray.Length <= 2)
        return;
      this.IncludedPages.Clear();
      for (int index = 2; index < strArray.Length; ++index)
        this.IncludedPages.Add(Guid.Parse(strArray[index]));
    }

    private void StartInternal(bool commit = true)
    {
      PageManager manager1 = PageManager.GetManager();
      VersionManager manager2 = VersionManager.GetManager();
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      Telerik.Sitefinity.Multisite.ISite site = multisiteContext == null ? SystemManager.CurrentContext.CurrentSite : multisiteContext.GetSiteById(this.siteId);
      using (new SiteRegion(site))
      {
        string[] array = site.PublicCultures.Values.ToArray<string>();
        string name = site.DefaultCulture.Name;
        bool isMultilingual = ((IEnumerable<string>) array).Count<string>() > 1;
        List<Guid> list1 = manager1.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (x => x.RootNodeId == site.SiteMapRootNodeId && (int) x.NodeType == 0)).Select<PageNode, Guid>((Expression<Func<PageNode, Guid>>) (x => x.Id)).ToList<Guid>();
        if (this.IncludedPages.Count > 0)
          list1 = list1.Where<Guid>((Func<Guid, bool>) (x => this.IncludedPages.Contains(x))).ToList<Guid>();
        for (int index = 0; index < list1.Count; ++index)
        {
          PageNode currentPage = manager1.GetPageNode(list1[index]);
          string[] availableLanguages = currentPage.AvailableLanguages;
          this.ClearPageNode(currentPage, array, name, isMultilingual);
          List<PageData> list2 = manager1.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (x => x.NavigationNode.Id == currentPage.Id)).ToList<PageData>();
          this.ClearPageData(manager1, (IList<PageData>) list2, availableLanguages, name, isMultilingual);
          foreach (PageData pageData in list2)
          {
            this.ClearProperties(manager1, pageData.Controls.Cast<ControlData>(), availableLanguages, name, isMultilingual, pageData.Culture, currentPage.LocalizationStrategy);
            foreach (PageDraft draft in (IEnumerable<PageDraft>) pageData.Drafts)
              this.ClearProperties(manager1, draft.Controls.Cast<ControlData>(), availableLanguages, name, isMultilingual, pageData.Culture, currentPage.LocalizationStrategy);
          }
          IList dirtyItems = manager1.Provider.GetDirtyItems();
          bool flag = this.CancelCommit((DataProviderBase) manager1.Provider);
          if (commit && !flag)
          {
            if (dirtyItems.Count > 0)
            {
              foreach (PageData pageData in list2)
                manager2.TruncateVersions(pageData.Id, DateTime.UtcNow);
              manager2.SaveChanges();
            }
            manager1.SaveChanges();
          }
          else
            manager1.CancelChanges();
        }
      }
    }

    private string[] GetCultureDisplayNames(IList<string> cultureKeys)
    {
      List<string> stringList = new List<string>(cultureKeys.Count);
      if (cultureKeys != null)
      {
        ResourcesConfig resourcesConfig = Config.Get<ResourcesConfig>();
        foreach (string cultureKey in (IEnumerable<string>) cultureKeys)
        {
          CultureElement cultureElement;
          if (resourcesConfig.Cultures.TryGetValue(cultureKey, out cultureElement))
            stringList.Add(cultureElement.Culture);
        }
      }
      return stringList.ToArray();
    }

    private void ClearPageNode(
      PageNode node,
      string[] siteLangs,
      string defaultLang,
      bool isMultilingual)
    {
      List<string> titlePropNames = ((IEnumerable<string>) siteLangs).Select<string, string>((Func<string, string>) (x => LstringPropertyDescriptor.GetFieldNameForCulture("Title", CultureInfo.GetCultureInfo(x)))).ToList<string>();
      titlePropNames.Add("Title_");
      foreach (PropertyDescriptor propertyDescriptor in LstringPropertyDescriptor.GetAllProperties((object) node).OfType<PropertyDescriptor>().Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.Name.StartsWith("Title_") && !titlePropNames.Contains(x.Name) && x.GetValue((object) node) != null)))
        propertyDescriptor.SetValue((object) node, (object) null);
      if (((IEnumerable<string>) node.AvailableLanguages).Count<string>() >= 2)
        return;
      node.LocalizationStrategy = LocalizationStrategy.NotSelected;
    }

    internal IList<ControlProperty> ClearPropertiesInMono(
      PageManager manager,
      IEnumerable<ControlData> controls)
    {
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      foreach (ControlData control in controls)
      {
        IList<ControlProperty> collection = this.ClearPropertiesInMono(manager, control);
        controlPropertyList.AddRange((IEnumerable<ControlProperty>) collection);
      }
      return (IList<ControlProperty>) controlPropertyList;
    }

    internal IList<ControlProperty> ClearPropertiesInMono(
      PageManager manager,
      ControlData control,
      bool isTranslatable = true)
    {
      if (control.Strategy != PropertyPersistenceStrategy.BackwardCompatible)
        return (IList<ControlProperty>) new List<ControlProperty>();
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      if (!isTranslatable)
      {
        foreach (string propertyName in control.Properties.Select<ControlProperty, string>((Func<ControlProperty, string>) (x => x.Name)).Distinct<string>().ToList<string>())
          controlPropertyList.AddRange((IEnumerable<ControlProperty>) this.EnsureProperty(control.Properties, propertyName));
      }
      else
      {
        this.EnsureProperty(control.Properties, "ID");
        this.EnsureProperty(control.Properties, "ControllerName");
      }
      controlPropertyList.AddRange(control.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language != null)));
      foreach (ControlProperty controlProperty in controlPropertyList)
        control.Properties.Remove(controlProperty);
      return (IList<ControlProperty>) controlPropertyList;
    }

    internal IList<ControlProperty> ClearPropertiesForSyncAndNotSelected(
      PageManager manager,
      IEnumerable<ControlData> controls,
      string[] pageLangs,
      string defaultLang)
    {
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      foreach (ControlData control in controls)
        controlPropertyList.AddRange((IEnumerable<ControlProperty>) this.ClearPropertiesForSyncAndNotSelected(manager, control, pageLangs, defaultLang));
      return (IList<ControlProperty>) controlPropertyList;
    }

    internal IList<ControlProperty> ClearPropertiesForSyncAndNotSelected(
      PageManager manager,
      ControlData control,
      string[] pageLangs,
      string defaultLang)
    {
      if (control.Strategy != PropertyPersistenceStrategy.BackwardCompatible)
        return (IList<ControlProperty>) new List<ControlProperty>();
      if (!control.IsTranslatable)
        return this.ClearPropertiesInMono(manager, control, false);
      List<ControlProperty> controlPropertyList1 = new List<ControlProperty>();
      List<ControlProperty> controlPropertyList2 = new List<ControlProperty>();
      controlPropertyList1.AddRange((IEnumerable<ControlProperty>) this.EnsureProperty(control.Properties, "ID"));
      if (((IEnumerable<string>) pageLangs).Contains<string>(defaultLang))
      {
        foreach (string propName in control.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name != "ID")).Select<ControlProperty, string>((Func<ControlProperty, string>) (x => x.Name)).Distinct<string>())
        {
          IList<ControlProperty> collection = this.SyncInvariantAndDefault(manager, control.Properties, propName, defaultLang);
          controlPropertyList2.AddRange((IEnumerable<ControlProperty>) collection);
        }
        controlPropertyList1.AddRange(control.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => !((IEnumerable<string>) pageLangs).Contains<string>(x.Language) && x.Language != null)));
      }
      else
        controlPropertyList1.AddRange(control.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => !((IEnumerable<string>) pageLangs).Contains<string>(x.Language) && x.Name != "ID")));
      foreach (string targetLang in ((IEnumerable<string>) pageLangs).Where<string>((Func<string, bool>) (x => x != defaultLang)).ToList<string>())
        this.ApplyAnyLanguageFallbackLogic(manager, control, targetLang, defaultLang);
      foreach (ControlProperty controlProperty in controlPropertyList2)
        control.Properties.Add(controlProperty);
      this.EnsureTranslatableProperty(manager, "ControllerName", control.Properties, (IList<string>) pageLangs);
      foreach (ControlProperty controlProperty in controlPropertyList1)
        control.Properties.Remove(controlProperty);
      return (IList<ControlProperty>) controlPropertyList1;
    }

    internal IList<ControlProperty> ClearPropertiesForSplit(
      PageManager manager,
      IEnumerable<ControlData> controls,
      string pageDataLang,
      string defaultLang)
    {
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      foreach (ControlData control in controls)
        controlPropertyList.AddRange((IEnumerable<ControlProperty>) this.ClearPropertiesForSplit(manager, control, pageDataLang, defaultLang));
      return (IList<ControlProperty>) controlPropertyList;
    }

    internal IList<ControlProperty> ClearPropertiesForSplit(
      PageManager manager,
      ControlData control,
      string pageDataLang,
      string defaultLang)
    {
      if (control.Strategy != PropertyPersistenceStrategy.BackwardCompatible)
        return (IList<ControlProperty>) new List<ControlProperty>();
      if (!control.IsTranslatable)
        return this.ClearPropertiesInMono(manager, control, false);
      if (pageDataLang == defaultLang)
      {
        string[] pageLangs = new string[1]{ pageDataLang };
        return this.ClearPropertiesForSyncAndNotSelected(manager, control, pageLangs, defaultLang);
      }
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      this.ApplyAnyLanguageFallbackLogic(manager, control, pageDataLang, defaultLang);
      controlPropertyList.AddRange((IEnumerable<ControlProperty>) this.EnsureProperty(control.Properties, "ControllerName", pageDataLang));
      controlPropertyList.AddRange((IEnumerable<ControlProperty>) this.EnsureProperty(control.Properties, "ID"));
      controlPropertyList.AddRange(control.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language != pageDataLang && x.Name != "ID")));
      foreach (ControlProperty controlProperty in controlPropertyList)
        control.Properties.Remove(controlProperty);
      return (IList<ControlProperty>) controlPropertyList;
    }

    private void ApplyAnyLanguageFallbackLogic(
      PageManager manager,
      ControlData control,
      string targetLang,
      string defaultLang)
    {
      if (control.Properties.Any<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == targetLang)))
        return;
      List<ControlProperty> list = control.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name != "ID" && x.Name != "ControllerName")).ToList<ControlProperty>();
      if (!control.Properties.Any<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == defaultLang)))
      {
        ControlProperty firstProp = control.Properties.FirstOrDefault<ControlProperty>();
        if (firstProp != null)
          list = list.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == firstProp.Language)).ToList<ControlProperty>();
      }
      else
        list = list.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == defaultLang)).ToList<ControlProperty>();
      if (list.Count <= 0)
        return;
      foreach (ControlProperty source in list)
      {
        ControlProperty property = manager.CreateProperty();
        property.Language = targetLang;
        ControlHelper.CopyProperty((IControlPropertyProvider) manager, source, property, (ControlPropertyDelegate) (args => args.TargetProperty.Language = targetLang));
        control.Properties.Add(property);
      }
    }

    private void ClearProperties(
      PageManager pageManager,
      IEnumerable<ControlData> controls,
      string[] pageLangs,
      string defaultLang,
      bool isMultilingual,
      string pageDataLang,
      LocalizationStrategy strategy)
    {
      IList<ControlProperty> controlPropertyList = !isMultilingual || strategy == LocalizationStrategy.Split ? (!isMultilingual || strategy != LocalizationStrategy.Split ? this.ClearPropertiesInMono(pageManager, controls) : this.ClearPropertiesForSplit(pageManager, controls, pageDataLang, defaultLang)) : this.ClearPropertiesForSyncAndNotSelected(pageManager, controls, pageLangs, defaultLang);
      foreach (ObjectData control in controls)
        this.SyncPropLanguages(control.Properties);
      foreach (ControlProperty controlProperty in (IEnumerable<ControlProperty>) controlPropertyList)
        pageManager.Delete(controlProperty);
    }

    private void ClearPageData(
      PageManager manager,
      IList<PageData> pageDataList,
      string[] pageLangs,
      string defaultLang,
      bool isMultilingual)
    {
      List<PageData> pageDataList1 = new List<PageData>((IEnumerable<PageData>) pageDataList);
      if (!isMultilingual && pageDataList.Count == 1 && pageDataList[0].Culture != defaultLang)
        pageDataList[0].Culture = defaultLang;
      if (pageDataList1.Count <= 1)
        return;
      foreach (PageData pageData in pageDataList1)
      {
        if (!((IEnumerable<string>) pageLangs).Contains<string>(pageData.Culture) && !pageData.Culture.IsNullOrEmpty() && pageData.Culture != defaultLang)
          manager.Delete(pageData);
      }
    }

    private IList<ControlProperty> EnsureProperty(
      IList<ControlProperty> properties,
      string propertyName,
      string targetLang = null)
    {
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      List<ControlProperty> list = properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name == propertyName)).ToList<ControlProperty>();
      if (list.Count > 1)
      {
        if (list.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == targetLang)) != null)
        {
          controlPropertyList.AddRange(list.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language != targetLang)));
        }
        else
        {
          ControlProperty randomProp = list.FirstOrDefault<ControlProperty>();
          randomProp.Language = targetLang;
          controlPropertyList.AddRange(list.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x != randomProp)));
        }
      }
      else
      {
        ControlProperty controlProperty = list.FirstOrDefault<ControlProperty>();
        if (controlProperty != null && controlProperty.Language != targetLang)
          controlProperty.Language = targetLang;
      }
      return (IList<ControlProperty>) controlPropertyList;
    }

    private void EnsureTranslatableProperty(
      PageManager manager,
      string propName,
      IList<ControlProperty> properties,
      IList<string> langs)
    {
      ControlProperty source = properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name == propName));
      if (source == null)
        return;
      foreach (string lang1 in (IEnumerable<string>) langs)
      {
        string lang = lang1;
        if (properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name == propName && x.Language == lang)) == null)
        {
          ControlProperty property = manager.CreateProperty();
          property.Language = lang;
          ControlHelper.CopyProperty((IControlPropertyProvider) manager, source, property, (ControlPropertyDelegate) (args => args.TargetProperty.Language = lang));
          properties.Add(property);
        }
      }
    }

    private IList<ControlProperty> SyncInvariantAndDefault(
      PageManager manager,
      IList<ControlProperty> properties,
      string propName,
      string defaultLang)
    {
      List<ControlProperty> list = properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name == propName)).ToList<ControlProperty>();
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      if (list.Count > 0)
      {
        ControlProperty controlProperty1 = list.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == null));
        ControlProperty controlProperty2 = list.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == defaultLang));
        int num = controlProperty1 != null ? 0 : (controlProperty2 == null ? 1 : 0);
        if (controlProperty1 == null)
        {
          controlProperty1 = manager.CreateProperty();
          controlPropertyList.Add(controlProperty1);
        }
        if (controlProperty2 == null)
        {
          controlProperty2 = manager.CreateProperty();
          controlProperty2.Language = defaultLang;
          controlPropertyList.Add(controlProperty2);
        }
        if (num != 0)
        {
          ControlProperty sourceProp = list.FirstOrDefault<ControlProperty>();
          this.ReplaceProperty(manager, sourceProp, controlProperty1);
          this.ReplaceProperty(manager, sourceProp, controlProperty2);
        }
        else
          this.SyncProps(manager, controlProperty1, controlProperty2);
      }
      return (IList<ControlProperty>) controlPropertyList;
    }

    /// <summary>
    /// Decides which of the two properties should be replaced with the other.
    /// </summary>
    /// <param name="manager">The manager for the properties.</param>
    /// <param name="inv">The invariant property.</param>
    /// <param name="def">The property from the default culture.</param>
    protected virtual void SyncProps(PageManager manager, ControlProperty inv, ControlProperty def)
    {
      if (inv.LastModified >= def.LastModified)
        this.ReplaceProperty(manager, inv, def);
      else
        this.ReplaceProperty(manager, def, inv);
    }

    /// <summary>
    /// Copies all data from the source property to the target property.
    /// </summary>
    /// <param name="manager">The manager for those properties.</param>
    /// <param name="sourceProp">The source property.</param>
    /// <param name="targetProp">The target property.</param>
    protected void ReplaceProperty(
      PageManager manager,
      ControlProperty sourceProp,
      ControlProperty targetProp)
    {
      ControlHelper.CopyProperty((IControlPropertyProvider) manager, sourceProp, targetProp, new FindControlProperty(this.FindProperty), (ControlPropertyDelegate) (args =>
      {
        if (!(args.TargetProperty.Language != targetProp.Language))
          return;
        args.TargetProperty.Language = targetProp.Language;
      }));
    }

    private ControlProperty FindProperty(
      IEnumerable<ControlProperty> targetProperties,
      string name,
      string language,
      IControlPropertyProvider provider,
      out bool isNew)
    {
      language = ControlHelper.NormalizeLanguage(language);
      foreach (ControlProperty targetProperty in targetProperties)
      {
        if (string.Equals(targetProperty.Name, name, StringComparison.OrdinalIgnoreCase))
        {
          isNew = false;
          return targetProperty;
        }
      }
      isNew = true;
      ControlProperty property = provider.CreateProperty();
      property.Name = name;
      property.Language = language;
      return property;
    }

    private void SyncPropLanguages(IList<ControlProperty> properties)
    {
      foreach (ControlProperty property in (IEnumerable<ControlProperty>) properties)
      {
        if (property.ChildProperties.Count > 0 && property.Language != property.ChildProperties[0].Language)
        {
          foreach (ControlProperty childProperty in (IEnumerable<ControlProperty>) property.ChildProperties)
          {
            childProperty.Language = property.Language;
            this.SyncPropLanguages(childProperty.ChildProperties);
          }
        }
      }
    }

    private bool CancelCommit(DataProviderBase provider)
    {
      if (this.OnValidate != null)
      {
        FixMultilingualSiteTask.ValidationEventArgs e = new FixMultilingualSiteTask.ValidationEventArgs(provider);
        foreach (EventHandler<FixMultilingualSiteTask.ValidationEventArgs> eventHandler in this.OnValidate.GetInvocationList().OfType<EventHandler<FixMultilingualSiteTask.ValidationEventArgs>>())
        {
          eventHandler((object) this, e);
          if (e.Cancel)
            return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Event, which is fired before the changes from the operation is send to the database.
    /// </summary>
    public event EventHandler<FixMultilingualSiteTask.ValidationEventArgs> OnValidate;

    internal XDocument GetReport()
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if ((multisiteContext == null ? SystemManager.CurrentContext.CurrentSite : multisiteContext.GetSiteById(this.siteId)).PublicCultures.Count <= 1)
        return (XDocument) null;
      this.StartInternal(false);
      return this.GenerateReport();
    }

    private XDocument GenerateReport()
    {
      PageDataProvider provider = PageManager.GetManager().Provider;
      IEnumerable<ControlProperty> controlProperties = provider.GetDirtyItems().OfType<ControlProperty>().Where<ControlProperty>((Func<ControlProperty, bool>) (x => !x.Language.IsNullOrEmpty()));
      List<XElement> xelementList = new List<XElement>();
      foreach (ControlProperty controlProperty in controlProperties)
      {
        if (provider.GetOriginalValue<string>((object) controlProperty, "Value") != controlProperty.Value && provider.GetDirtyItemStatus((object) controlProperty) == SecurityConstants.TransactionActionType.Updated)
          this.GenHierarchy(controlProperty, xelementList);
      }
      return new XDocument(new object[1]
      {
        (object) xelementList.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Name == (XName) "pages"))
      });
    }

    private void GenHierarchy(ControlProperty prop, List<XElement> allElements, XElement element = null)
    {
      if (element == null)
      {
        element = allElements.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Attribute((XName) "Id").Value == prop.Id.ToString()));
        if (element == null)
        {
          string originalValue = PageManager.GetManager().Provider.GetOriginalValue<string>((object) prop, "Value");
          element = new XElement((XName) nameof (prop), new object[4]
          {
            (object) new XAttribute((XName) "Id", (object) prop.Id.ToString()),
            (object) new XAttribute((XName) "name", (object) prop.Name),
            (object) new XAttribute((XName) "lang", (object) (prop.Language ?? "NULL")),
            (object) new XAttribute((XName) "lastModified", (object) prop.LastModified)
          });
          if (!originalValue.IsNullOrEmpty() || !prop.Value.IsNullOrEmpty())
          {
            element.Add((object) new XElement((XName) "new", (object) prop.Value));
            element.Add((object) new XElement((XName) "old", (object) originalValue));
          }
          allElements.Add(element);
        }
      }
      if (prop.ParentProperty != null)
      {
        ControlProperty parentProp = prop.ParentProperty;
        XElement xelement = allElements.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Attribute((XName) "Id").Value == parentProp.Id.ToString()));
        if (xelement == null)
        {
          xelement = new XElement((XName) nameof (prop), new object[4]
          {
            (object) new XAttribute((XName) "name", (object) parentProp.Name),
            (object) new XAttribute((XName) "Id", (object) parentProp.Id.ToString()),
            (object) new XAttribute((XName) "lang", (object) (parentProp.Language ?? "NULL")),
            (object) new XAttribute((XName) "lastModified", (object) parentProp.LastModified)
          });
          allElements.Add(xelement);
        }
        if (element.Parent == null)
          xelement.Add((object) element);
        this.GenHierarchy(parentProp, allElements);
      }
      else
      {
        if (prop.Control == null)
          return;
        ObjectData control = prop.Control;
        XElement controlElement = allElements.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Attribute((XName) "Id").Value == control.Id.ToString()));
        if (controlElement == null)
        {
          controlElement = new XElement((XName) "control", new object[2]
          {
            (object) new XAttribute((XName) "Id", (object) control.Id.ToString()),
            (object) new XAttribute((XName) "type", (object) control.ObjectType)
          });
          if (control is ControlData controlData)
            controlElement.Add((object) new XAttribute((XName) "caption", (object) controlData.Caption));
          else
            controlElement.Add((object) new XAttribute((XName) "caption", (object) "NULL"));
          allElements.Add(controlElement);
        }
        controlElement.Add((object) element);
        this.GenHierarchy(control, allElements, controlElement);
      }
    }

    private void GenHierarchy(
      ObjectData control,
      List<XElement> allElements,
      XElement controlElement)
    {
      if (control.ParentProperty != null)
      {
        ControlProperty parentProp = control.ParentProperty;
        XElement element = allElements.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Attribute((XName) "Id").Value == parentProp.Id.ToString()));
        if (element == null)
        {
          element = new XElement((XName) "prop", new object[4]
          {
            (object) new XAttribute((XName) "name", (object) parentProp.Name),
            (object) new XAttribute((XName) "Id", (object) parentProp.Id.ToString()),
            (object) new XAttribute((XName) "lang", (object) (parentProp.Language ?? "NULL")),
            (object) new XAttribute((XName) "lastModified", (object) parentProp.LastModified)
          });
          allElements.Add(element);
        }
        if (controlElement.Parent == null)
          element.Add((object) controlElement);
        this.GenHierarchy(parentProp, allElements, element);
      }
      else
      {
        if (control is PageControl pageControl)
        {
          PageData pageData = pageControl.Page;
          XElement content1 = allElements.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Attribute((XName) "Id").Value == pageData.Id.ToString()));
          if (content1 == null)
          {
            content1 = new XElement((XName) "pageData", new object[4]
            {
              (object) new XAttribute((XName) "Id", (object) pageData.Id),
              (object) new XAttribute((XName) "lang", (object) (pageData.Culture ?? "NULL")),
              (object) new XElement((XName) "controls"),
              (object) new XElement((XName) "drafts")
            });
            allElements.Add(content1);
            Guid pageNodeId = pageData.NavigationNodeId;
            XElement content2 = allElements.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Attribute((XName) "Id").Value == pageNodeId.ToString()));
            if (content2 == null)
            {
              content2 = new XElement((XName) "pageNode", (object) new XAttribute((XName) "Id", (object) pageNodeId));
              XElement xelement = allElements.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Name == (XName) "pages"));
              if (xelement == null)
              {
                xelement = new XElement((XName) "pages", (object) new XAttribute((XName) "Id", (object) string.Empty));
                allElements.Add(xelement);
              }
              xelement.Add((object) content2);
              allElements.Add(content2);
            }
            content2.Add((object) content1);
          }
          if (controlElement.Parent == null)
            content1.Element((XName) "controls").Add((object) controlElement);
        }
        if (!(control is PageDraftControl pageDraftControl))
          return;
        PageDraft pageDraft = pageDraftControl.Page;
        XElement content3 = allElements.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Attribute((XName) "Id").Value == pageDraft.Id.ToString()));
        if (content3 == null)
        {
          content3 = new XElement((XName) "pageDraft", new object[3]
          {
            (object) new XAttribute((XName) "Id", (object) pageDraft.Id),
            (object) new XAttribute((XName) "type", pageDraft.IsTempDraft ? (object) "Temp" : (object) "Master"),
            (object) new XElement((XName) "controls")
          });
          allElements.Add(content3);
          PageData pageData = pageDraft.ParentPage;
          XElement content4 = allElements.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Attribute((XName) "Id").Value == pageData.Id.ToString()));
          if (content4 == null)
          {
            content4 = new XElement((XName) "pageData", new object[4]
            {
              (object) new XAttribute((XName) "Id", (object) pageData.Id),
              (object) new XAttribute((XName) "lang", (object) (pageData.Culture ?? "NULL")),
              (object) new XElement((XName) "controls"),
              (object) new XElement((XName) "drafts")
            });
            allElements.Add(content4);
            Guid pageNodeId = pageData.NavigationNodeId;
            XElement content5 = allElements.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Attribute((XName) "Id").Value == pageNodeId.ToString()));
            if (content5 == null)
            {
              content5 = new XElement((XName) "pageNode", (object) new XAttribute((XName) "Id", (object) pageNodeId));
              XElement xelement = allElements.FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Name == (XName) "pages"));
              if (xelement == null)
              {
                xelement = new XElement((XName) "pages", (object) new XAttribute((XName) "Id", (object) string.Empty));
                allElements.Add(xelement);
              }
              xelement.Add((object) content5);
              allElements.Add(content5);
            }
            content5.Add((object) content4);
          }
          content4.Element((XName) "drafts").Add((object) content3);
        }
        if (controlElement.Parent != null)
          return;
        content3.Element((XName) "controls").Add((object) controlElement);
      }
    }

    /// <summary>
    /// The arguments that pass in the dirty properties from the provider and the provider itself.
    /// Depending on the Cancel property the operation can be aborted and the database will not be affected.
    /// </summary>
    public class ValidationEventArgs : EventArgs
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.FixMultilingualSiteTask.ValidationEventArgs" />.
      /// </summary>
      /// <param name="provider">The provider from which the dirty items are retrieved.</param>
      public ValidationEventArgs(DataProviderBase provider)
      {
        this.Provider = provider;
        this.DirtyItems = provider.GetDirtyItems();
      }

      /// <summary>
      /// Gets or sets a value whether to cancel the changes.
      /// No other attached event handlers will be executed.
      /// </summary>
      public bool Cancel { get; set; }

      /// <summary>Gets the dirty items from the provider.</summary>
      public IList DirtyItems { get; private set; }

      /// <summary>Gets the provider.</summary>
      public DataProviderBase Provider { get; private set; }
    }
  }
}
