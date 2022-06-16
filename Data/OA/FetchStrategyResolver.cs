// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.FetchStrategyResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Telerik.OpenAccess.FetchOptimization;
using Telerik.OpenAccess.SPI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.OA
{
  internal class FetchStrategyResolver
  {
    private const string SuffixDelimeter = "_";
    private readonly object fetchStrategyCacheLock = new object();
    private readonly Dictionary<string, FetchStrategy> fetchStrategiesCache = new Dictionary<string, FetchStrategy>();
    private readonly Dictionary<string, Lazy<FetchStrategyResolver.FragmentsInfo>> moduleFragmentsCache = new Dictionary<string, Lazy<FetchStrategyResolver.FragmentsInfo>>();

    internal static FetchStrategy Clone(FetchStrategy sourceStrategy)
    {
      FetchStrategy fetchStrategy = new FetchStrategy();
      FieldInfo field = typeof (FetchStrategy).GetField("fragments", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      HashSet<FetchPlanFragment> fetchPlanFragmentSet1 = field.GetValue((object) sourceStrategy) as HashSet<FetchPlanFragment>;
      HashSet<FetchPlanFragment> fetchPlanFragmentSet2 = field.GetValue((object) fetchStrategy) as HashSet<FetchPlanFragment>;
      foreach (FetchPlanFragment fetchPlanFragment in fetchPlanFragmentSet1)
        fetchPlanFragmentSet2.Add(fetchPlanFragment);
      return fetchStrategy;
    }

    internal FetchStrategy GetLocalizedFetchStrategy(
      string moduleName,
      Func<IEnumerable<CultureInfo>> culturesFunc,
      Func<IEnumerable<CultureInfo>> mainPropCulturesFunc = null)
    {
      if (SystemManager.Initializing || !this.moduleFragmentsCache.ContainsKey(moduleName) || culturesFunc == null)
        return (FetchStrategy) null;
      IEnumerable<CultureInfo> cultureInfos = culturesFunc().Where<CultureInfo>((Func<CultureInfo, bool>) (c => c.LCID != CultureInfo.InvariantCulture.LCID));
      if (cultureInfos.Count<CultureInfo>() == 0)
        return (FetchStrategy) null;
      string strategyCacheName = this.GetFetchStrategyCacheName(moduleName, cultureInfos);
      FetchStrategy sourceStrategy = (FetchStrategy) null;
      if (!this.fetchStrategiesCache.TryGetValue(strategyCacheName, out sourceStrategy))
      {
        lock (this.fetchStrategyCacheLock)
        {
          if (!this.fetchStrategiesCache.TryGetValue(strategyCacheName, out sourceStrategy))
          {
            IEnumerable<CultureInfo> mainPropCultures = mainPropCulturesFunc != null ? mainPropCulturesFunc() : (IEnumerable<CultureInfo>) null;
            sourceStrategy = this.CreateFetchStrategy(this.GetModuleFetchPlanFragments(moduleName, cultureInfos, mainPropCultures));
            this.fetchStrategiesCache.Add(strategyCacheName, sourceStrategy);
          }
        }
      }
      return FetchStrategyResolver.Clone(sourceStrategy);
    }

    internal void RegisterModuleFetchPlanFragments(
      string moduleName,
      IEnumerable<string> persistentTypes,
      IEnumerable<CultureInfo> cultures)
    {
      if (string.IsNullOrEmpty(moduleName) || persistentTypes == null || cultures == null)
        return;
      cultures = cultures.Where<CultureInfo>((Func<CultureInfo, bool>) (c => c.LCID != CultureInfo.InvariantCulture.LCID));
      if (cultures.Count<CultureInfo>() == 0 || persistentTypes.Count<string>() == 0)
        return;
      this.moduleFragmentsCache.Add(moduleName, new Lazy<FetchStrategyResolver.FragmentsInfo>((Func<FetchStrategyResolver.FragmentsInfo>) (() =>
      {
        FetchStrategyResolver.FragmentsInfo fragmentsInfo = new FetchStrategyResolver.FragmentsInfo();
        foreach (CultureInfo culture in cultures)
        {
          List<FetchPlanFragment> mainPropFragments;
          IEnumerable<FetchPlanFragment> fetchPlanFragments = this.CreateModuleFetchPlanFragments(persistentTypes, culture, moduleName, out mainPropFragments);
          if (fetchPlanFragments.Count<FetchPlanFragment>() != 0)
          {
            string cultureKey = this.GetCultureKey(culture);
            fragmentsInfo.AllFields.Add(cultureKey, fetchPlanFragments);
            fragmentsInfo.MainFields.Add(cultureKey, (IEnumerable<FetchPlanFragment>) mainPropFragments);
          }
        }
        return fragmentsInfo;
      })));
    }

    internal void UnregisterModuleFetchPlanFragments(string moduleName)
    {
      if (!this.moduleFragmentsCache.ContainsKey(moduleName))
        return;
      this.moduleFragmentsCache.Remove(moduleName);
    }

    private FetchStrategy CreateFetchStrategy(IEnumerable<FetchPlanFragment> fragments)
    {
      FetchStrategy fetchStrategy = new FetchStrategy();
      HashSet<FetchPlanFragment> fetchPlanFragmentSet = typeof (FetchStrategy).GetField(nameof (fragments), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue((object) fetchStrategy) as HashSet<FetchPlanFragment>;
      foreach (FetchPlanFragment fragment in fragments)
        fetchPlanFragmentSet.Add(fragment);
      return fetchStrategy;
    }

    private IEnumerable<FetchPlanFragment> GetModuleFetchPlanFragments(
      string moduleName,
      IEnumerable<CultureInfo> cultures,
      IEnumerable<CultureInfo> mainPropCultures)
    {
      List<FetchPlanFragment> fragments = new List<FetchPlanFragment>();
      Lazy<FetchStrategyResolver.FragmentsInfo> lazy;
      if (this.moduleFragmentsCache.TryGetValue(moduleName, out lazy))
      {
        this.AddModuleFetchPlanFragments(ref fragments, moduleName, cultures, lazy.Value.AllFields);
        if (mainPropCultures != null)
          this.AddModuleFetchPlanFragments(ref fragments, moduleName, mainPropCultures, lazy.Value.MainFields);
      }
      return (IEnumerable<FetchPlanFragment>) fragments;
    }

    private void AddModuleFetchPlanFragments(
      ref List<FetchPlanFragment> fragments,
      string moduleName,
      IEnumerable<CultureInfo> cultures,
      Dictionary<string, IEnumerable<FetchPlanFragment>> source)
    {
      foreach (CultureInfo culture in cultures)
      {
        string cultureKey = this.GetCultureKey(culture);
        IEnumerable<FetchPlanFragment> collection;
        if (!string.IsNullOrEmpty(cultureKey) && source.TryGetValue(cultureKey, out collection))
          fragments.AddRange(collection);
      }
    }

    private IEnumerable<FetchPlanFragment> CreateModuleFetchPlanFragments(
      IEnumerable<string> persistentTypes,
      CultureInfo culture,
      string moduleName,
      out List<FetchPlanFragment> mainPropFragments)
    {
      List<FetchPlanFragment> fetchPlanFragments1 = new List<FetchPlanFragment>();
      List<FetchPlanFragment> fetchPlanFragmentList = new List<FetchPlanFragment>();
      foreach (string persistentType in persistentTypes)
      {
        List<FetchPlanFragment> mainPropFragments1 = new List<FetchPlanFragment>();
        IEnumerable<FetchPlanFragment> fetchPlanFragments2 = this.CreateTypeFetchPlanFragments(persistentType, culture, out mainPropFragments1);
        if (fetchPlanFragments2 != null)
          fetchPlanFragments1.AddRange(fetchPlanFragments2);
        if (mainPropFragments1 != null)
          fetchPlanFragmentList.AddRange((IEnumerable<FetchPlanFragment>) mainPropFragments1);
      }
      mainPropFragments = fetchPlanFragmentList;
      return (IEnumerable<FetchPlanFragment>) fetchPlanFragments1;
    }

    private IEnumerable<FetchPlanFragment> CreateTypeFetchPlanFragments(
      string typeFullName,
      CultureInfo culture,
      out List<FetchPlanFragment> mainPropFragments)
    {
      List<string> stringList = new List<string>();
      mainPropFragments = new List<FetchPlanFragment>();
      Type type = this.ResolveType(typeFullName);
      string str1 = typeof (DynamicContent).IsAssignableFrom(type) ? this.GetDynamicTypeMainField(typeFullName) : type.GetMainFieldName();
      if (!type.ImplementsInterface(typeof (IDynamicFieldsContainer)))
        return (IEnumerable<FetchPlanFragment>) null;
      foreach (PropertyDescriptor property in this.GetProperties(type))
      {
        if (property.PropertyType.Equals(typeof (Lstring)))
          stringList.Add(property.Name);
      }
      if (stringList.Count == 0)
        return (IEnumerable<FetchPlanFragment>) null;
      List<FetchPlanFragment> fetchPlanFragments = new List<FetchPlanFragment>();
      for (int index = 0; index < stringList.Count; ++index)
      {
        string str2 = stringList[index];
        FetchPlanFragment fetchPlanFragment = new FetchPlanFragment(type, new string[1]
        {
          this.GetLstringName(str2, culture)
        }, true, (string) null);
        fetchPlanFragments.Add(fetchPlanFragment);
        if (str1 == str2)
          mainPropFragments.Add(fetchPlanFragment);
      }
      return (IEnumerable<FetchPlanFragment>) fetchPlanFragments;
    }

    private string GetFetchStrategyCacheName(string moduleName, IEnumerable<CultureInfo> cultures)
    {
      List<string> source = new List<string>();
      foreach (CultureInfo culture in cultures)
        source.Add(this.GetCultureKey(culture));
      source.Sort();
      return moduleName + "_" + source.Aggregate<string>((Func<string, string, string>) ((i, j) => i + "_" + j));
    }

    private string GetLstringName(string item, CultureInfo culture)
    {
      string cultureSuffix = this.GetCultureSuffix(culture);
      return item + "_" + cultureSuffix;
    }

    private string GetCultureKey(CultureInfo culture) => culture.Name;

    private string GetCultureSuffix(CultureInfo culture) => ResourcesConfig.GetCultureSuffix(culture);

    private Type ResolveType(string fullName) => TypeResolutionService.ResolveType(fullName);

    private PropertyDescriptorCollection GetProperties(Type type) => TypeDescriptor.GetProperties(type);

    private string GetDynamicTypeMainField(string typeFullName)
    {
      MetaType metaType = MetadataSourceAggregator.GetMetaTypes().Where<MetaType>((Func<MetaType, bool>) (t => t.FullTypeName == typeFullName)).FirstOrDefault<MetaType>();
      return metaType == null ? (string) null : metaType.MetaAttributes.Where<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => "mainPropertyName".Equals(a.Name))).Select<MetaTypeAttribute, string>((Func<MetaTypeAttribute, string>) (a => a.Value)).FirstOrDefault<string>();
    }

    internal class FragmentsInfo
    {
      private readonly Dictionary<string, IEnumerable<FetchPlanFragment>> allFields = new Dictionary<string, IEnumerable<FetchPlanFragment>>();
      private readonly Dictionary<string, IEnumerable<FetchPlanFragment>> mainFields = new Dictionary<string, IEnumerable<FetchPlanFragment>>();

      public Dictionary<string, IEnumerable<FetchPlanFragment>> AllFields => this.allFields;

      public Dictionary<string, IEnumerable<FetchPlanFragment>> MainFields => this.mainFields;
    }
  }
}
