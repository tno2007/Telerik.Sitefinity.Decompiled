// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.PageNodeServiceContractProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class PageNodeServiceContractProvider : ITypeSettingsProvider
  {
    public const string PagesUrl = "pages";
    public static readonly Type PageType = typeof (PageNode);
    private Dictionary<string, string> propertiesToRenamPairs = new Dictionary<string, string>()
    {
      {
        "RootNodeId",
        "RootId"
      }
    };

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    public IDictionary<string, ITypeSettings> GetTypeSettings()
    {
      ITypeSettings typeSettings = ContractFactory.Instance.Create(PageNodeServiceContractProvider.PageType, "pages");
      string[] strArray = new string[11]
      {
        LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (x => x.Nodes)),
        LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (x => (object) x.Ordinal)),
        LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (x => x.Name)),
        LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (x => (object) x.EnableDefaultCanonicalUrl)),
        LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (x => x.Description)),
        LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (x => (object) x.RenderAsLink)),
        LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (x => x.ApplicationName)),
        LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (x => x.RedirectUrl)),
        LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (x => (object) x.LinkedNodeId)),
        LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (x => x.LinkedNodeProvider)),
        LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (x => x.AvailableLanguages))
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
      CalculatedPropertyMappingProxy[] propertyMappingProxyArray1 = new CalculatedPropertyMappingProxy[8];
      CalculatedPropertyMappingProxy propertyMappingProxy1 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy1.Name = "HasChildren";
      propertyMappingProxy1.ResolverType = typeof (HasChildrenProperty).FullName;
      propertyMappingProxyArray1[0] = propertyMappingProxy1;
      CalculatedPropertyMappingProxy propertyMappingProxy2 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy2.Name = "Breadcrumb";
      propertyMappingProxy2.ResolverType = typeof (BreadcrumbProperty).FullName;
      propertyMappingProxyArray1[1] = propertyMappingProxy2;
      CalculatedPropertyMappingProxy propertyMappingProxy3 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy3.Name = "AvailableLanguages";
      propertyMappingProxy3.ResolverType = typeof (AvailableLanguagesProperty).FullName;
      propertyMappingProxyArray1[2] = propertyMappingProxy3;
      CalculatedPropertyMappingProxy propertyMappingProxy4 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy4.Name = "PublicationDate";
      propertyMappingProxy4.ResolverType = typeof (PublicationDateProperty).FullName;
      propertyMappingProxyArray1[3] = propertyMappingProxy4;
      CalculatedPropertyMappingProxy propertyMappingProxy5 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy5.Name = "ViewUrl";
      propertyMappingProxy5.ResolverType = typeof (ViewUrlProperty).FullName;
      propertyMappingProxyArray1[4] = propertyMappingProxy5;
      CalculatedPropertyMappingProxy propertyMappingProxy6 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy6.Name = "EditUrl";
      propertyMappingProxy6.ResolverType = typeof (EditUrlProperty).FullName;
      propertyMappingProxyArray1[5] = propertyMappingProxy6;
      CalculatedPropertyMappingProxy propertyMappingProxy7 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy7.Name = "IsHomePage";
      propertyMappingProxy7.ResolverType = typeof (HomepageProperty).FullName;
      propertyMappingProxyArray1[6] = propertyMappingProxy7;
      CalculatedPropertyMappingProxy propertyMappingProxy8 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy8.Name = "RelativeUrlPath";
      propertyMappingProxy8.ResolverType = typeof (ParentUrlPathProperty).FullName;
      propertyMappingProxyArray1[7] = propertyMappingProxy8;
      foreach (CalculatedPropertyMappingProxy propertyMappingProxy9 in propertyMappingProxyArray1)
        typeSettings.Properties.Add((IPropertyMapping) propertyMappingProxy9);
      PersistentPropertyMappingProxy[] propertyMappingProxyArray2 = new PersistentPropertyMappingProxy[19];
      PersistentPropertyMappingProxy propertyMappingProxy10 = new PersistentPropertyMappingProxy();
      propertyMappingProxy10.Name = "HtmlTitle";
      propertyMappingProxy10.AllowFilter = false;
      propertyMappingProxy10.AllowSort = false;
      propertyMappingProxy10.SelectedByDefault = false;
      propertyMappingProxyArray2[0] = propertyMappingProxy10;
      PersistentPropertyMappingProxy propertyMappingProxy11 = new PersistentPropertyMappingProxy();
      propertyMappingProxy11.Name = "Description";
      propertyMappingProxy11.AllowFilter = false;
      propertyMappingProxy11.AllowSort = false;
      propertyMappingProxy11.SelectedByDefault = false;
      propertyMappingProxyArray2[1] = propertyMappingProxy11;
      PersistentPropertyMappingProxy propertyMappingProxy12 = new PersistentPropertyMappingProxy();
      propertyMappingProxy12.Name = "HeadTagContent";
      propertyMappingProxy12.AllowFilter = false;
      propertyMappingProxy12.AllowSort = false;
      propertyMappingProxy12.SelectedByDefault = false;
      propertyMappingProxyArray2[2] = propertyMappingProxy12;
      PersistentPropertyMappingProxy propertyMappingProxy13 = new PersistentPropertyMappingProxy();
      propertyMappingProxy13.Name = "CodeBehindType";
      propertyMappingProxy13.AllowFilter = false;
      propertyMappingProxy13.AllowSort = false;
      propertyMappingProxy13.SelectedByDefault = false;
      propertyMappingProxyArray2[3] = propertyMappingProxy13;
      PersistentPropertyMappingProxy propertyMappingProxy14 = new PersistentPropertyMappingProxy();
      propertyMappingProxy14.Name = "IncludeScriptManager";
      propertyMappingProxy14.AllowFilter = false;
      propertyMappingProxy14.AllowSort = false;
      propertyMappingProxy14.SelectedByDefault = false;
      propertyMappingProxyArray2[4] = propertyMappingProxy14;
      PersistentPropertyMappingProxy propertyMappingProxy15 = new PersistentPropertyMappingProxy();
      propertyMappingProxy15.Name = "EnableViewState";
      propertyMappingProxy15.AllowFilter = false;
      propertyMappingProxy15.AllowSort = false;
      propertyMappingProxy15.SelectedByDefault = false;
      propertyMappingProxyArray2[5] = propertyMappingProxy15;
      PersistentPropertyMappingProxy propertyMappingProxy16 = new PersistentPropertyMappingProxy();
      propertyMappingProxy16.Name = "PageType";
      propertyMappingProxy16.AllowFilter = false;
      propertyMappingProxy16.AllowSort = false;
      propertyMappingProxy16.SelectedByDefault = false;
      propertyMappingProxyArray2[6] = propertyMappingProxy16;
      PersistentPropertyMappingProxy propertyMappingProxy17 = new PersistentPropertyMappingProxy();
      propertyMappingProxy17.Name = "RedirectPage";
      propertyMappingProxy17.AllowFilter = false;
      propertyMappingProxy17.AllowSort = false;
      propertyMappingProxy17.SelectedByDefault = false;
      propertyMappingProxyArray2[7] = propertyMappingProxy17;
      PersistentPropertyMappingProxy propertyMappingProxy18 = new PersistentPropertyMappingProxy();
      propertyMappingProxy18.Name = "TemplateId";
      propertyMappingProxy18.AllowFilter = false;
      propertyMappingProxy18.AllowSort = false;
      propertyMappingProxy18.SelectedByDefault = false;
      propertyMappingProxyArray2[8] = propertyMappingProxy18;
      PersistentPropertyMappingProxy propertyMappingProxy19 = new PersistentPropertyMappingProxy();
      propertyMappingProxy19.Name = "RequireSsl";
      propertyMappingProxy19.SelectedByDefault = false;
      propertyMappingProxyArray2[9] = propertyMappingProxy19;
      PersistentPropertyMappingProxy propertyMappingProxy20 = new PersistentPropertyMappingProxy();
      propertyMappingProxy20.Name = "AllowParametersValidation";
      propertyMappingProxy20.SelectedByDefault = false;
      propertyMappingProxyArray2[10] = propertyMappingProxy20;
      PersistentPropertyMappingProxy propertyMappingProxy21 = new PersistentPropertyMappingProxy();
      propertyMappingProxy21.Name = "Crawlable";
      propertyMappingProxy21.SelectedByDefault = false;
      propertyMappingProxyArray2[11] = propertyMappingProxy21;
      PersistentPropertyMappingProxy propertyMappingProxy22 = new PersistentPropertyMappingProxy();
      propertyMappingProxy22.Name = "IncludeInSearchIndex";
      propertyMappingProxy22.SelectedByDefault = false;
      propertyMappingProxyArray2[12] = propertyMappingProxy22;
      PersistentPropertyMappingProxy propertyMappingProxy23 = new PersistentPropertyMappingProxy();
      propertyMappingProxy23.Name = "Priority";
      propertyMappingProxy23.AllowFilter = false;
      propertyMappingProxy23.AllowSort = false;
      propertyMappingProxy23.SelectedByDefault = false;
      propertyMappingProxyArray2[13] = propertyMappingProxy23;
      PersistentPropertyMappingProxy propertyMappingProxy24 = new PersistentPropertyMappingProxy();
      propertyMappingProxy24.Name = "LocalizationStrategy";
      propertyMappingProxy24.AllowFilter = false;
      propertyMappingProxy24.AllowSort = false;
      propertyMappingProxy24.SelectedByDefault = false;
      propertyMappingProxy24.ReadOnly = true;
      propertyMappingProxyArray2[14] = propertyMappingProxy24;
      PersistentPropertyMappingProxy propertyMappingProxy25 = new PersistentPropertyMappingProxy();
      propertyMappingProxy25.Name = "CanonicalUrlBehaviour";
      propertyMappingProxy25.AllowFilter = false;
      propertyMappingProxy25.AllowSort = false;
      propertyMappingProxy25.SelectedByDefault = false;
      propertyMappingProxyArray2[15] = propertyMappingProxy25;
      PersistentPropertyMappingProxy propertyMappingProxy26 = new PersistentPropertyMappingProxy();
      propertyMappingProxy26.Name = "OutputCacheProfile";
      propertyMappingProxy26.AllowFilter = false;
      propertyMappingProxy26.AllowSort = false;
      propertyMappingProxy26.SelectedByDefault = false;
      propertyMappingProxyArray2[16] = propertyMappingProxy26;
      PersistentPropertyMappingProxy propertyMappingProxy27 = new PersistentPropertyMappingProxy();
      propertyMappingProxy27.Name = "Renderer";
      propertyMappingProxy27.SelectedByDefault = false;
      propertyMappingProxyArray2[17] = propertyMappingProxy27;
      PersistentPropertyMappingProxy propertyMappingProxy28 = new PersistentPropertyMappingProxy();
      propertyMappingProxy28.Name = "TemplateName";
      propertyMappingProxy28.SelectedByDefault = false;
      propertyMappingProxyArray2[18] = propertyMappingProxy28;
      foreach (PersistentPropertyMappingProxy propertyMappingProxy29 in propertyMappingProxyArray2)
        typeSettings.Properties.Add((IPropertyMapping) propertyMappingProxy29);
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
