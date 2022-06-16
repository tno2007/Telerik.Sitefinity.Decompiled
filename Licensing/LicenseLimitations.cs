// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.LicenseLimitations
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Licensing
{
  internal sealed class LicenseLimitations
  {
    public static int CountContentItems()
    {
      Dictionary<Type, int> source = new Dictionary<Type, int>();
      foreach (MetaType metaType in (IEnumerable<MetaType>) MetadataManager.GetManager().GetMetaTypes())
      {
        Type type = TypeResolutionService.ResolveType(metaType.Namespace + "." + metaType.ClassName, false);
        if (type != (Type) null && !type.IsAbstract && LicenseLimitations.IsLimitedContentType(type))
        {
          int num = LicenseLimitations.TotalCountOfItems(type);
          if (source.ContainsKey(type))
            source[type] = num;
          else
            source.Add(type, num);
        }
      }
      return source.Sum<KeyValuePair<Type, int>>((Func<KeyValuePair<Type, int>, int>) (item => item.Value));
    }

    public static int CountPublicPages()
    {
      int num1 = 0;
      foreach (PageDataProvider staticProvider in (Collection<PageDataProvider>) PageManager.GetManager().StaticProviders)
      {
        bool suppressSecurityChecks = staticProvider.SuppressSecurityChecks;
        try
        {
          staticProvider.SuppressSecurityChecks = true;
          Guid rootNodeId = SiteInitializer.CurrentFrontendRootNodeId;
          int num2 = PageManager.GetManager(string.Empty, staticProvider.Name).GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (pageData => pageData.NavigationNode.RootNode.Id == rootNodeId)).Where<PageData>((Expression<Func<PageData, bool>>) (pageData => pageData.Visible == true)).Count<PageData>();
          num1 += num2;
        }
        finally
        {
          staticProvider.SuppressSecurityChecks = suppressSecurityChecks;
        }
      }
      return num1;
    }

    public static void CanSaveItems(Type itemType, int newItemsCount)
    {
      if (newItemsCount < 0)
        throw new LicenseInfoExceptions();
      if (!Bootstrapper.IsSystemInitialized)
        return;
      string domain = string.Empty;
      int num;
      if (typeof (PageData).IsAssignableFrom(itemType))
      {
        LicenseState.Current.LicenseInfo.TryGetLicensedDomain(SystemManager.CurrentContext.CurrentSite.GetUri().Host, out domain);
        LicensedDomainItem licensedDomainItem = LicenseState.Current.LicenseInfo.LicensedDomainItems.FirstOrDefault<LicensedDomainItem>((Func<LicensedDomainItem, bool>) (di => di.Domains.Contains<string>(domain)));
        num = licensedDomainItem == null ? LicenseState.Current.LicenseInfo.TotalPublicPagesLimit : licensedDomainItem.TotalPublicPagesLimit;
      }
      else
      {
        num = LicenseState.Current.LicenseInfo.TotalPublicPagesLimit;
        domain = "current domain";
      }
      if (num != 0 && typeof (PageData).IsAssignableFrom(itemType))
      {
        if (LicenseLimitations.CountPublicPages() + newItemsCount > num)
          throw new TotalPublicPagesLimitExceeded(string.Format("Your license allows you to have up to {0} published pages for the {1} at the same time. To be able to publish a new page, delete or unpublish some of the currently published pages. If you need any help with licensing please, contact sitefinitysales@progress.com", (object) num, (object) domain));
      }
      else if (LicenseState.Current.LicenseInfo.TotalContentLimit != 0 && LicenseLimitations.IsLimitedContentType(itemType) && LicenseLimitations.CountContentItems() + newItemsCount > LicenseState.Current.LicenseInfo.TotalContentLimit)
        throw new TotalContentItemsLimitExceeded(string.Format("Your license allows you to have up to {0} published items at the same time. To be able to publish a new item, delete or unpublish some of the currently published items. If you need any help with licensing please, contact sitefinitysales@progress.com", (object) LicenseState.Current.LicenseInfo.TotalContentLimit));
    }

    public static string GetCurrentLicenseName()
    {
      switch (LicenseState.Current.LicenseInfo.LicenseType)
      {
        case "CL":
          return "Cloud";
        case "ISE":
          return "Intranet Standard edition";
        case "MS":
          return "Multisite Edition";
        case "OME":
          return "Online Marketing Edition";
        case "PE":
          return "Professional edition";
        case "PU":
          return "Enterprise edition";
        case "SB":
          return "Small Business edition";
        case "SE":
          return "Standard edition";
        default:
          return "";
      }
    }

    public static bool CanUseLoadBalancing(bool throwException)
    {
      int num = LicenseState.Current.LicenseInfo.SupportLoadBalancing ? 1 : 0;
      if (num != 0)
        return num != 0;
      string balancingIsDisabled = Res.Get<SecurityResources>().LoadBalancingIsDisabled;
      if (throwException)
        throw new LoadBalancingNotSupported(balancingIsDisabled);
      Log.Write((object) balancingIsDisabled, ConfigurationPolicy.ErrorLog);
      return num != 0;
    }

    public static Guid GetModuleId(Type moduleClass)
    {
      object[] objArray = !(moduleClass == (Type) null) ? moduleClass.GetCustomAttributes(typeof (ModuleIdAttribute), true) : throw new ArgumentNullException(nameof (moduleClass));
      return objArray.Length != 0 ? ((ModuleIdAttribute) objArray[0]).Id : Guid.Empty;
    }

    public static void ValidateModuleLicensed(IModule module)
    {
      string errorMessage = module != null ? "The \"{0}\" module requires license.".Arrange((object) module.Name) : throw new ArgumentNullException(nameof (module));
      LicenseLimitations.ValidateModuleLicensed(module.GetType(), errorMessage);
    }

    public static void ValidateModuleLicensed(object component, string errorMessage = null)
    {
      if (component == null)
        throw new ArgumentNullException(nameof (component));
      LicenseLimitations.ValidateModuleLicensed(component.GetType(), errorMessage);
    }

    public static void ValidateModuleLicensed(Type moduleType, string errorMessage)
    {
      if (moduleType == (Type) null)
        throw new ArgumentNullException(nameof (moduleType));
      if (!Bootstrapper.IsSystemInitialized)
        return;
      LicenseLimitations.ValidateModuleLicensed(LicenseLimitations.GetModuleId(moduleType), errorMessage);
    }

    public static void ValidateModuleLicensed(Guid moduleId, string errorMessage)
    {
      if (moduleId != Guid.Empty && !LicenseState.CheckIsModuleLicensedInAnyDomain(moduleId))
      {
        if (string.IsNullOrEmpty(errorMessage))
          errorMessage = "The \"{0}\" module requires license.".Arrange((object) (SystemManager.ApplicationModules.Values.FirstOrDefault<IModule>((Func<IModule, bool>) (m => m.ModuleId == moduleId)) ?? throw new ArgumentException("There is no registered module with the specified ID: \"{0}\".".Arrange((object) moduleId), nameof (moduleId))).Name);
        throw new LicenseInfoExceptions(errorMessage);
      }
    }

    public static bool ValidateWorkflow(bool throwsException)
    {
      if (LicenseState.Current.LicenseInfo.WorkflowFeaturesLevel != 0)
        return true;
      if (throwsException)
        throw new LicenseInfoExceptions("Your license does not allow this operation");
      return false;
    }

    public static bool ValidateWorkflow() => LicenseLimitations.ValidateWorkflow(true);

    public static void ThrowTheLicenseException(Exception ex)
    {
      if (typeof (LicenseInfoExceptions).IsAssignableFrom(ex.GetType()))
        throw ex;
      if (ex.InnerException == null)
        return;
      LicenseLimitations.ThrowTheLicenseException(ex.InnerException);
    }

    private static int TotalCountOfItems(Type itemType)
    {
      IManager mappedManager1;
      try
      {
        mappedManager1 = ManagerBase.GetMappedManager(itemType);
      }
      catch
      {
        return 0;
      }
      int num = 0;
      int? totalCount = new int?(0);
      foreach (string providerName in mappedManager1.GetProviderNames(ProviderBindingOptions.SkipSystem))
      {
        IManager mappedManager2 = ManagerBase.GetMappedManager(itemType, providerName);
        bool suppressSecurityChecks = mappedManager2.Provider.SuppressSecurityChecks;
        try
        {
          mappedManager2.Provider.SuppressSecurityChecks = true;
          if (itemType.SupportsContentLifeCycle())
            mappedManager2.GetItems(itemType, PredefinedFilters.PublishedItemsRegardlessSheduledDateFilter(), "", 0, 0, ref totalCount);
          num += totalCount.HasValue ? totalCount.Value : 0;
        }
        finally
        {
          mappedManager2.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        }
      }
      return num;
    }

    /// <summary>
    /// Determines whether the specified item type is limited content type.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    internal static bool IsLimitedContentType(Type itemType) => LicenseLimitations.IsLimitedContentType(itemType, LicenseState.Current.LicenseInfo.LicenseType);

    /// <summary>
    /// Determines whether the specified item type is limited content type.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="licenseEdition">The license edition.</param>
    internal static bool IsLimitedContentType(Type itemType, string licenseEdition)
    {
      if (typeof (DynamicContent).IsAssignableFrom(itemType))
        return true;
      if (typeof (Comment).IsAssignableFrom(itemType) || !typeof (Content).IsAssignableFrom(itemType))
        return false;
      object obj = ((IEnumerable<object>) itemType.GetCustomAttributes(typeof (UnlimitedContentAttribute), true)).FirstOrDefault<object>();
      return obj == null || !(obj as UnlimitedContentAttribute).IsValidForEdition(licenseEdition);
    }
  }
}
