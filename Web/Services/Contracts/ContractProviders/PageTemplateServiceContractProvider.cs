// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.PageTemplateServiceContractProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.Services.Extensibility.Templates;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class PageTemplateServiceContractProvider : ITypeSettingsProvider
  {
    public const string PagesUrl = "templates";
    public static readonly Type Type = typeof (PageTemplate);
    private Dictionary<string, string> propertiesToRenamPairs = new Dictionary<string, string>();

    public static bool GetAreTempaltesAvailable() => string.Equals(ConfigurationManager.AppSettings.Get("sf:enableRendererFeatures14"), "true", StringComparison.OrdinalIgnoreCase) || PageTemplateServiceContractProvider.EnableRendererFeatures14;

    internal static bool EnableRendererFeatures14 { get; set; }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    public IDictionary<string, ITypeSettings> GetTypeSettings()
    {
      ITypeSettings typeSettings = ContractFactory.Instance.Create(PageTemplateServiceContractProvider.Type, "templates");
      string[] strArray = new string[2]
      {
        "Status",
        "Visible"
      };
      foreach (PersistentPropertyMappingProxy propertyMappingProxy in typeSettings.Properties.Where<IPropertyMapping>((Func<IPropertyMapping, bool>) (p => this.propertiesToRenamPairs.Keys.Contains<string>(p.Name))))
      {
        propertyMappingProxy.PersistentName = propertyMappingProxy.Name;
        propertyMappingProxy.Name = this.propertiesToRenamPairs[propertyMappingProxy.PersistentName];
      }
      foreach (string str in strArray)
      {
        string propName = str;
        IPropertyMapping propertyMapping = typeSettings.Properties.FirstOrDefault<IPropertyMapping>((Func<IPropertyMapping, bool>) (x => x.Name == propName));
        typeSettings.Properties.Remove(propertyMapping);
      }
      CalculatedPropertyMappingProxy[] propertyMappingProxyArray1 = new CalculatedPropertyMappingProxy[9];
      CalculatedPropertyMappingProxy propertyMappingProxy1 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy1.Name = "ViewUrl";
      propertyMappingProxy1.ResolverType = typeof (PreviewUrlProperty).FullName;
      propertyMappingProxyArray1[0] = propertyMappingProxy1;
      CalculatedPropertyMappingProxy propertyMappingProxy2 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy2.Name = "EditUrl";
      propertyMappingProxy2.ResolverType = typeof (EditUrlProperty).FullName;
      propertyMappingProxyArray1[1] = propertyMappingProxy2;
      CalculatedPropertyMappingProxy propertyMappingProxy3 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy3.Name = "ParentTemplate";
      propertyMappingProxy3.ResolverType = typeof (ParentTemplatesProperty).FullName;
      propertyMappingProxyArray1[2] = propertyMappingProxy3;
      CalculatedPropertyMappingProxy propertyMappingProxy4 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy4.Name = PageTemplate.ThumbnailFieldName;
      propertyMappingProxy4.ResolverType = typeof (TemplateThumbnailsProperty).FullName;
      propertyMappingProxyArray1[3] = propertyMappingProxy4;
      CalculatedPropertyMappingProxy propertyMappingProxy5 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy5.Name = "Renderer";
      propertyMappingProxy5.ResolverType = typeof (TemplateRenderersProperty).FullName;
      propertyMappingProxyArray1[4] = propertyMappingProxy5;
      CalculatedPropertyMappingProxy propertyMappingProxy6 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy6.Name = "IsPersonalized";
      propertyMappingProxy6.ResolverType = typeof (IsPersonalizedProperty).FullName;
      propertyMappingProxyArray1[5] = propertyMappingProxy6;
      CalculatedPropertyMappingProxy propertyMappingProxy7 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy7.Name = "PagesCount";
      propertyMappingProxy7.ResolverType = typeof (UsedOnPagesProperty).FullName;
      propertyMappingProxyArray1[6] = propertyMappingProxy7;
      CalculatedPropertyMappingProxy propertyMappingProxy8 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy8.Name = "DisplayStatus";
      propertyMappingProxy8.ResolverType = typeof (TemplateStatusProperty).FullName;
      propertyMappingProxyArray1[7] = propertyMappingProxy8;
      CalculatedPropertyMappingProxy propertyMappingProxy9 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy9.Name = "LastModifiedBy";
      propertyMappingProxy9.ResolverType = typeof (LastModifiedByProperty).FullName;
      propertyMappingProxyArray1[8] = propertyMappingProxy9;
      foreach (CalculatedPropertyMappingProxy propertyMappingProxy10 in propertyMappingProxyArray1)
        typeSettings.Properties.Add((IPropertyMapping) propertyMappingProxy10);
      PersistentPropertyMappingProxy[] propertyMappingProxyArray2 = new PersistentPropertyMappingProxy[9];
      PersistentPropertyMappingProxy propertyMappingProxy11 = new PersistentPropertyMappingProxy();
      propertyMappingProxy11.Name = "Title";
      propertyMappingProxy11.AllowFilter = false;
      propertyMappingProxy11.AllowSort = true;
      propertyMappingProxy11.SelectedByDefault = false;
      propertyMappingProxyArray2[0] = propertyMappingProxy11;
      PersistentPropertyMappingProxy propertyMappingProxy12 = new PersistentPropertyMappingProxy();
      propertyMappingProxy12.Name = "LastModified";
      propertyMappingProxy12.AllowFilter = false;
      propertyMappingProxy12.AllowSort = true;
      propertyMappingProxy12.SelectedByDefault = false;
      propertyMappingProxyArray2[1] = propertyMappingProxy12;
      PersistentPropertyMappingProxy propertyMappingProxy13 = new PersistentPropertyMappingProxy();
      propertyMappingProxy13.Name = "Owner";
      propertyMappingProxy13.AllowFilter = false;
      propertyMappingProxy13.AllowSort = false;
      propertyMappingProxy13.SelectedByDefault = false;
      propertyMappingProxyArray2[2] = propertyMappingProxy13;
      PersistentPropertyMappingProxy propertyMappingProxy14 = new PersistentPropertyMappingProxy();
      propertyMappingProxy14.Name = "Status";
      propertyMappingProxy14.AllowFilter = false;
      propertyMappingProxy14.AllowSort = false;
      propertyMappingProxy14.SelectedByDefault = false;
      propertyMappingProxyArray2[3] = propertyMappingProxy14;
      PersistentPropertyMappingProxy propertyMappingProxy15 = new PersistentPropertyMappingProxy();
      propertyMappingProxy15.Name = "Framework";
      propertyMappingProxy15.AllowFilter = false;
      propertyMappingProxy15.AllowSort = false;
      propertyMappingProxy15.SelectedByDefault = false;
      propertyMappingProxyArray2[4] = propertyMappingProxy15;
      PersistentPropertyMappingProxy propertyMappingProxy16 = new PersistentPropertyMappingProxy();
      propertyMappingProxy16.Name = "TemplateId";
      propertyMappingProxy16.AllowFilter = false;
      propertyMappingProxy16.AllowSort = false;
      propertyMappingProxyArray2[5] = propertyMappingProxy16;
      PersistentPropertyMappingProxy propertyMappingProxy17 = new PersistentPropertyMappingProxy();
      propertyMappingProxy17.Name = "Renderer";
      propertyMappingProxy17.SelectedByDefault = false;
      propertyMappingProxyArray2[6] = propertyMappingProxy17;
      PersistentPropertyMappingProxy propertyMappingProxy18 = new PersistentPropertyMappingProxy();
      propertyMappingProxy18.Name = "TemplateName";
      propertyMappingProxy18.SelectedByDefault = false;
      propertyMappingProxyArray2[7] = propertyMappingProxy18;
      PersistentPropertyMappingProxy propertyMappingProxy19 = new PersistentPropertyMappingProxy();
      propertyMappingProxy19.Name = "DateCreated";
      propertyMappingProxy19.SelectedByDefault = false;
      propertyMappingProxyArray2[8] = propertyMappingProxy19;
      foreach (PersistentPropertyMappingProxy propertyMappingProxy20 in propertyMappingProxyArray2)
        typeSettings.Properties.Add((IPropertyMapping) propertyMappingProxy20);
      return (IDictionary<string, ITypeSettings>) new Dictionary<string, ITypeSettings>()
      {
        {
          typeSettings.ClrType,
          typeSettings
        }
      };
    }
  }
}
