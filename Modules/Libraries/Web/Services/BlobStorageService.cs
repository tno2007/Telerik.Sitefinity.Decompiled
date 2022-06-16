// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.BlobStorageService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Basic;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>The WCF web service for blob storage management.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class BlobStorageService : IBlobStorageProviderService
  {
    /// <inheritdoc />
    public CollectionContext<DataProviderSettingsViewModel> GetProviders()
    {
      ServiceUtility.RequestBackendUserAuthentication();
      BlobStorageConfigElement blobStorageConfig = Config.Get<LibrariesConfig>().BlobStorage;
      IEnumerable<DataProviderSettingsViewModel> settingsViewModels = blobStorageConfig.Providers.Values.Select<DataProviderSettings, DataProviderSettingsViewModel>((Func<DataProviderSettings, DataProviderSettingsViewModel>) (provider => new DataProviderSettingsViewModel(provider)
      {
        IsDefault = provider.Name == blobStorageConfig.DefaultProvider,
        HasSettings = blobStorageConfig.BlobStorageTypes.Values.Where<BlobStorageTypeConfigElement>((Func<BlobStorageTypeConfigElement, bool>) (t => t.ProviderType.Equals(provider.ProviderType) && !string.IsNullOrEmpty(t.SettingsViewTypeOrPath))).Any<BlobStorageTypeConfigElement>()
      }));
      ServiceUtility.DisableCache();
      return new CollectionContext<DataProviderSettingsViewModel>(settingsViewModels)
      {
        TotalCount = settingsViewModels.Count<DataProviderSettingsViewModel>()
      };
    }

    /// <inheritdoc />
    public bool BatchDeleteProviders(string[] providers)
    {
      ConfigManager manager = ConfigManager.GetManager();
      LibrariesConfig section = manager.GetSection<LibrariesConfig>();
      ConfigElementDictionary<string, DataProviderSettings> providers1 = section.BlobStorage.Providers;
      foreach (string provider in providers)
      {
        if (!LibrariesManager.IsBlobStorageProviderUsed(provider))
          providers1.Remove(provider);
        else
          throw new ApplicationException(Res.Get<LibrariesResources>().BlobStorageProviderIsUsedMessage.Arrange((object) provider));
      }
      manager.SaveSection((ConfigSection) section, true);
      return true;
    }

    /// <inheritdoc />
    public bool SetDefault(string newDefaultProvider)
    {
      ConfigManager manager = ConfigManager.GetManager();
      LibrariesConfig section = manager.GetSection<LibrariesConfig>();
      section.BlobStorage.DefaultProvider = newDefaultProvider;
      manager.SaveSection((ConfigSection) section, true);
      return true;
    }

    /// <inheritdoc />
    public CollectionContext<WcfBlobStorageProvider> GetBlobStorageProviderStats(
      string libraryType,
      string provider)
    {
      if (libraryType == null)
        return (CollectionContext<WcfBlobStorageProvider>) null;
      LibrariesManager manager = LibrariesManager.GetManager(provider);
      if (libraryType.Equals(typeof (Album).FullName))
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: method reference
        return new CollectionContext<WcfBlobStorageProvider>((IEnumerable<WcfBlobStorageProvider>) manager.GetAlbums().GroupBy<Album, string>((Expression<Func<Album, string>>) (library => library.BlobStorageProvider)).OrderBy<IGrouping<string, Album>, string>((Expression<Func<IGrouping<string, Album>, string>>) (g => g.Key)).Select<IGrouping<string, Album>, WcfBlobStorageProvider>(Expression.Lambda<Func<IGrouping<string, Album>, WcfBlobStorageProvider>>((Expression) Expression.MemberInit(Expression.New(typeof (WcfBlobStorageProvider)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_Id)), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.NewGuid)), Array.Empty<Expression>())), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_Name)), (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IGrouping<string, Album>.get_Key), __typeref (IGrouping<string, Album>)))), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_Title)), (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IGrouping<string, Album>.get_Key), __typeref (IGrouping<string, Album>)))), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_TotalItemsCount)), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Count)), (Expression) parameterExpression))), parameterExpression)).ToArray<WcfBlobStorageProvider>());
      }
      if (libraryType.Equals(typeof (VideoLibrary).FullName))
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: method reference
        return new CollectionContext<WcfBlobStorageProvider>((IEnumerable<WcfBlobStorageProvider>) manager.GetVideoLibraries().GroupBy<VideoLibrary, string>((Expression<Func<VideoLibrary, string>>) (library => library.BlobStorageProvider)).OrderBy<IGrouping<string, VideoLibrary>, string>((Expression<Func<IGrouping<string, VideoLibrary>, string>>) (g => g.Key)).Select<IGrouping<string, VideoLibrary>, WcfBlobStorageProvider>(Expression.Lambda<Func<IGrouping<string, VideoLibrary>, WcfBlobStorageProvider>>((Expression) Expression.MemberInit(Expression.New(typeof (WcfBlobStorageProvider)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_Id)), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.NewGuid)), Array.Empty<Expression>())), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_Name)), (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IGrouping<string, VideoLibrary>.get_Key), __typeref (IGrouping<string, VideoLibrary>)))), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_Title)), (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IGrouping<string, VideoLibrary>.get_Key), __typeref (IGrouping<string, VideoLibrary>)))), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_TotalItemsCount)), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Count)), (Expression) parameterExpression))), parameterExpression)).ToArray<WcfBlobStorageProvider>());
      }
      if (libraryType.Equals(typeof (DocumentLibrary).FullName))
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: method reference
        return new CollectionContext<WcfBlobStorageProvider>((IEnumerable<WcfBlobStorageProvider>) manager.GetDocumentLibraries().GroupBy<DocumentLibrary, string>((Expression<Func<DocumentLibrary, string>>) (library => library.BlobStorageProvider)).OrderBy<IGrouping<string, DocumentLibrary>, string>((Expression<Func<IGrouping<string, DocumentLibrary>, string>>) (g => g.Key)).Select<IGrouping<string, DocumentLibrary>, WcfBlobStorageProvider>(Expression.Lambda<Func<IGrouping<string, DocumentLibrary>, WcfBlobStorageProvider>>((Expression) Expression.MemberInit(Expression.New(typeof (WcfBlobStorageProvider)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_Id)), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.NewGuid)), Array.Empty<Expression>())), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_Name)), (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IGrouping<string, DocumentLibrary>.get_Key), __typeref (IGrouping<string, DocumentLibrary>)))), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_Title)), (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IGrouping<string, DocumentLibrary>.get_Key), __typeref (IGrouping<string, DocumentLibrary>)))), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfBlobStorageProvider.set_TotalItemsCount)), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Count)), (Expression) parameterExpression))), parameterExpression)).ToArray<WcfBlobStorageProvider>());
      }
      throw new ArgumentOutOfRangeException("The provided libraryType {0} is not valid ".Arrange((object) libraryType));
    }

    /// <inheritdoc />
    public BlobStorageProviderSettingsViewModel GetBlobStorageProviderSettings(
      string blobStorageProviderName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      try
      {
        BlobStorageManager manager = BlobStorageManager.GetManager(blobStorageProviderName);
        return new BlobStorageProviderSettingsViewModel()
        {
          CustomImageSizeAllowed = manager.Provider.CustomImageSizeAllowed
        };
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw;
      }
      return (BlobStorageProviderSettingsViewModel) null;
    }
  }
}
