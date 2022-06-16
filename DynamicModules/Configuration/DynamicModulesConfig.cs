// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Configuration.DynamicModulesConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.DynamicModules.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.DynamicModules.Configuration
{
  /// <summary>The configuration class for the catalog module.</summary>
  internal class DynamicModulesConfig : ContentModuleConfigBase
  {
    private static RegexStrategy regexStrategy;

    /// <summary>Gets a collection of data provider settings.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Providers")]
    [ConfigurationProperty("providers")]
    public new virtual ConfigElementDictionary<string, DataProviderSettings> Providers => (ConfigElementDictionary<string, DataProviderSettings>) this["providers"];

    /// <summary>Gets or sets the name of the default data provider.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultProvider")]
    [ConfigurationProperty("defaultProvider", DefaultValue = "OpenAccessProvider")]
    public new virtual string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value, indicating whether to ignore the structure validation during sync operation.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SkipSiteSyncStructureValidationDescription", Title = "SkipSiteSyncStructureValidationCaption")]
    [ConfigurationProperty("skipSiteSyncStructureValidation", DefaultValue = false)]
    public virtual bool SkipSiteSyncStructureValidation
    {
      get => (bool) this["skipSiteSyncStructureValidation"];
      set => this["skipSiteSyncStructureValidation"] = (object) value;
    }

    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      if (this.Providers.Count != 0)
        return;
      this.Providers.Add(new DataProviderSettings()
      {
        Name = "OpenAccessProvider",
        Title = "Default",
        Description = "The data provider for dynamic modules on the Telerik OpenAccess ORM.",
        ProviderType = typeof (OpenAccessDynamicModuleProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/DynamicModule"
          }
        }
      });
    }

    protected override void InitializeDefaultViews(
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
    {
      contentViewControls.RegisterDelayedInitializer(new ConfigElementCollection.TryInitializeDefaults(this.TryInitializeDefaultViews));
    }

    protected internal override bool TryGenerateLazyElementFileName(
      ConfigElement element,
      out string fileName,
      int maxLength = 0)
    {
      ContentViewControlElement contentViewControlElement;
      DynamicModulesConfig dynamicModulesConfig;
      if (!(element is ContentViewDefinitionElement contentViewLazyElement) || !this.TryGetValidatedParentAndSection(contentViewLazyElement, out contentViewControlElement, out dynamicModulesConfig))
        return base.TryGenerateLazyElementFileName(element, out fileName, maxLength);
      string name1 = dynamicModulesConfig.GetType().Name;
      string name2 = contentViewControlElement.LinkModuleName.Replace(" ", "");
      if (name2.Length > 10)
        name2 = BackendNamingHelper.Shrink(name2, 10);
      string name3 = contentViewControlElement.ContentTypeName;
      if (name3.Length > 8)
      {
        int num = name3.LastIndexOf('.');
        if (num > 0)
          name3 = name3.Substring(num + 1);
        string str = (contentViewControlElement.LinkModuleName + name3).GetHashCode().ToString();
        name3 = BackendNamingHelper.Shrink(name3, 8) + str;
      }
      string source = contentViewLazyElement.ViewName.Replace(" ", "");
      if (source.Length > 12)
        source = new string(BackendNamingHelper.Shrink(new string(source.Reverse<char>().ToArray<char>()), 12).Reverse<char>().ToArray<char>());
      fileName = name1 + "." + name2 + "." + name3 + "_" + source;
      return true;
    }

    public override void Upgrade(Version oldVersion, Version newVersion)
    {
      base.Upgrade(oldVersion, newVersion);
      if (oldVersion.Build >= SitefinityVersion.Sitefinity9_0.Build)
        return;
      this.UpgradeTo90();
    }

    internal override void InitUpgradeContext(
      Version oldVersion,
      ConfigUpgradeContext upgradeContext)
    {
      base.InitUpgradeContext(oldVersion, upgradeContext);
      if (oldVersion.Build >= SitefinityVersion.Sitefinity9_0.Build)
        return;
      this.PrepareUpgradeTo90(upgradeContext);
    }

    private bool TryGetValidatedParentAndSection(
      ContentViewDefinitionElement contentViewLazyElement,
      out ContentViewControlElement contentViewControlElement,
      out DynamicModulesConfig dynamicModulesConfig)
    {
      contentViewControlElement = (ContentViewControlElement) null;
      dynamicModulesConfig = (DynamicModulesConfig) null;
      if (contentViewLazyElement.Parent == null)
        return false;
      contentViewControlElement = contentViewLazyElement.Parent.Parent as ContentViewControlElement;
      if (contentViewControlElement == null || contentViewControlElement.LinkModuleName.IsNullOrEmpty())
        return false;
      dynamicModulesConfig = contentViewControlElement.Section as DynamicModulesConfig;
      return dynamicModulesConfig != null;
    }

    private bool TryInitializeDefaultViews(ConfigElement element)
    {
      ConfigElementDictionary<string, ContentViewControlElement> parent = element as ConfigElementDictionary<string, ContentViewControlElement>;
      IEnumerable<ContentViewControlElement> items;
      if (!DynamicModulesDefinitions.TryDefineContentViewControls((ConfigElement) parent, out items))
        return false;
      foreach (ContentViewControlElement element1 in items)
        parent.Add(element1);
      return true;
    }

    private void UpgradeTo90()
    {
      IDictionary<string, List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>>> dictionary = (IDictionary<string, List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>>>) null;
      object obj;
      if (this.UpgradeContext != null && this.UpgradeContext.Items.TryGetValue("UpgrTo90_Views", out obj))
        dictionary = obj as IDictionary<string, List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>>>;
      if (dictionary == null)
        return;
      foreach (ContentViewControlElement viewControlElement in (IEnumerable<ContentViewControlElement>) this.ContentViewControls.Values)
      {
        foreach (ContentViewDefinitionElement definitionElement in (IEnumerable<ContentViewDefinitionElement>) viewControlElement.ViewsConfig.Values)
        {
          List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>> source;
          if (definitionElement is ContentViewDetailElement viewDetailElement && dictionary.TryGetValue(viewDetailElement.GetPath(), out source))
          {
            IList<ContentViewSectionElement> sortedSections1 = this.GetSortedSections(source.Select<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>, ContentViewSectionElement>((Func<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>, ContentViewSectionElement>) (t => t.Item1)));
            IList<ContentViewSectionElement> sortedSections2 = this.GetSortedSections((IEnumerable<ContentViewSectionElement>) viewDetailElement.Sections.Values);
            bool flag = false;
            int num1 = 0;
            foreach (ContentViewSectionElement viewSectionElement in (IEnumerable<ContentViewSectionElement>) sortedSections2)
            {
              ContentViewSectionElement section = viewSectionElement;
              int num2 = sortedSections1.IndexOf(section);
              if (num2 == -1)
              {
                viewDetailElement.Sections.Remove(section);
              }
              else
              {
                if (num1 != num2)
                  flag = true;
                ++num1;
                IList<FieldDefinitionElement> sortedFields = this.GetSortedFields((IEnumerable<FieldDefinitionElement>) source.Single<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>>((Func<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>, bool>) (s => s.Item1 == section)).Item2);
                foreach (FieldDefinitionElement sortedField in (IEnumerable<FieldDefinitionElement>) this.GetSortedFields((IEnumerable<FieldDefinitionElement>) section.Fields.Values))
                {
                  if (!sortedFields.Contains(sortedField))
                    section.Fields.Remove(sortedField);
                  if (sortedField is MirrorTextFieldElement && sortedField.FieldName == "UrlName")
                  {
                    MirrorTextFieldElement textFieldElement = (MirrorTextFieldElement) sortedField;
                    string str = "^[" + DynamicModulesConfig.RgxStrategy.UnicodeCategories + "–\\'’…„“”\"\\:\\-\\!\\$\\(\\)\\=\\@\\d_\\'~\\.]*[" + DynamicModulesConfig.RgxStrategy.UnicodeCategories + "–\\'’…„“”\"\\:\\-\\!\\$\\(\\)\\=\\@\\d_\\'~]+$";
                    if (textFieldElement.ValidatorConfig.RegularExpression.Equals(str))
                      textFieldElement.ValidatorConfig.RegularExpression = DefinitionsHelper.UrlRegularExpressionFilterForContentValidator;
                  }
                }
              }
            }
            if (flag)
            {
              foreach (ContentViewSectionElement sortedSection in (IEnumerable<ContentViewSectionElement>) this.GetSortedSections((IEnumerable<ContentViewSectionElement>) viewDetailElement.Sections.Values))
                sortedSection.Ordinal = new int?(sortedSections1.IndexOf(sortedSection));
            }
          }
        }
      }
    }

    private void PrepareUpgradeTo90(ConfigUpgradeContext upgradeContext) => upgradeContext.AddElementLoadHandler((Action<ConfigUpgradeContext, ConfigElement>) ((context, element) =>
    {
      object obj;
      IDictionary<string, List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>>> dictionary;
      if (!context.Items.TryGetValue("UpgrTo90_Views", out obj))
      {
        dictionary = (IDictionary<string, List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>>>) new Dictionary<string, List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>>>();
        context.Items.Add("UpgrTo90_Views", (object) dictionary);
      }
      else
        dictionary = (IDictionary<string, List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>>>) (obj as Dictionary<string, List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>>>);
      switch (element)
      {
        case ContentViewDetailElement _:
          dictionary.Add(element.GetPath(), new List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>>());
          break;
        case ContentViewSectionElement _:
          List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>> tupleList;
          if (!(element.Parent.Parent is ContentViewDetailElement parent3) || !dictionary.TryGetValue(parent3.GetPath(), out tupleList))
            break;
          List<FieldDefinitionElement> definitionElementList = new List<FieldDefinitionElement>();
          Tuple<ContentViewSectionElement, List<FieldDefinitionElement>> tuple = new Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>((ContentViewSectionElement) element, definitionElementList);
          tupleList.Add(tuple);
          break;
        case FieldDefinitionElement _:
          ContentViewSectionElement parentSection = element.Parent.Parent as ContentViewSectionElement;
          List<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>> source;
          if (parentSection == null || !(parentSection.Parent.Parent is ContentViewDetailElement parent4) || !dictionary.TryGetValue(parent4.GetPath(), out source))
            break;
          source.Single<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>>((Func<Tuple<ContentViewSectionElement, List<FieldDefinitionElement>>, bool>) (t => t.Item1 == parentSection)).Item2.Add((FieldDefinitionElement) element);
          break;
      }
    }));

    private IList<ContentViewSectionElement> GetSortedSections(
      IEnumerable<ContentViewSectionElement> sections)
    {
      List<ContentViewSectionElement> sortedSections = new List<ContentViewSectionElement>(sections.Where<ContentViewSectionElement>((Func<ContentViewSectionElement, bool>) (s => !s.Ordinal.HasValue)));
      sortedSections.AddRange((IEnumerable<ContentViewSectionElement>) sections.Where<ContentViewSectionElement>((Func<ContentViewSectionElement, bool>) (s => s.Ordinal.HasValue)).OrderBy<ContentViewSectionElement, int?>((Func<ContentViewSectionElement, int?>) (s => s.Ordinal)));
      return (IList<ContentViewSectionElement>) sortedSections;
    }

    private IList<FieldDefinitionElement> GetSortedFields(
      IEnumerable<FieldDefinitionElement> fields)
    {
      return (IList<FieldDefinitionElement>) new List<FieldDefinitionElement>(fields);
    }

    private static RegexStrategy RgxStrategy
    {
      get
      {
        if (DynamicModulesConfig.regexStrategy == null)
          DynamicModulesConfig.regexStrategy = ObjectFactory.Resolve<RegexStrategy>();
        return DynamicModulesConfig.regexStrategy;
      }
    }
  }
}
