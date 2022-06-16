// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.MultisiteContentTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Multisite.Web.Services;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.Json;

namespace Telerik.Sitefinity.Multisite
{
  /// <summary>
  /// Implements functionality for converting items from news content in transferable format.
  /// </summary>
  internal class MultisiteContentTransfer : StaticContentTransfer
  {
    private IEnumerable<ExportType> supportedTypes;
    private const string AreaName = "Multisite";
    private bool dependenciesPopulated;
    private bool restartNeeded;
    private const string SiteConfigurationViewModelProp = "SiteConfigurationViewModelProp";

    /// <inheritdoc />
    public override void CreateItem(WrapperObject transferableObject, string transactionName)
    {
      bool createSiteDependencies = false;
      Guid property = transferableObject.GetProperty<Guid>("Id");
      MultisiteManager manager = MultisiteManager.GetManager((string) null, transactionName);
      Site site1 = (Site) null;
      try
      {
        site1 = manager.GetSite(property);
      }
      catch (Exception ex)
      {
      }
      if (site1 == null)
      {
        createSiteDependencies = true;
        site1 = manager.CreateSite(property);
        site1.SiteConfigurationMode = SiteConfigurationMode.ConfigureByDeployment;
      }
      else if (site1.SiteConfigurationMode == SiteConfigurationMode.ConfigureManually)
        return;
      SiteConfigurationViewModel site2 = JsonUtility.FromJson<SiteConfigurationViewModel>(transferableObject.GetProperty<string>("SiteConfigurationViewModelProp"));
      site2.LiveUrl = createSiteDependencies ? transferableObject.GetProperty<string>("LiveUrl") : site1.LiveUrl;
      site2.StagingUrl = createSiteDependencies ? transferableObject.GetProperty<string>("StagingUrl") : site1.StagingUrl;
      site2.DomainAliases = createSiteDependencies ? transferableObject.GetProperty<IList<string>>("DomainAliases") : site1.DomainAliases;
      site2.FrontEndLoginPageUrl = createSiteDependencies ? transferableObject.GetProperty<string>("FrontEndLoginPageUrl") : site1.FrontEndLoginPageUrl;
      site2.FrontEndLoginPageId = createSiteDependencies ? transferableObject.GetProperty<Guid>("FrontEndLoginPageId") : site1.FrontEndLoginPageId;
      site2.HomePageId = createSiteDependencies ? transferableObject.GetProperty<Guid>("HomePageId") : site1.HomePageId;
      site2.SiteConfigurationMode = site1.SiteConfigurationMode;
      MultisiteService multisiteService = new MultisiteService();
      bool restartNeeded;
      multisiteService.SaveSite(property.ToString(), site2, out restartNeeded, createSiteDependencies, new Guid?(transferableObject.GetProperty<Guid>("SiteMapRootNodeId")), transactionName);
      if (restartNeeded)
        this.restartNeeded = true;
      if (createSiteDependencies)
        return;
      multisiteService.SaveSiteDataSources(property.ToString(), site2, string.Empty);
    }

    /// <inheritdoc />
    public override string Area => "Multisite";

    /// <inheritdoc />
    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        if (this.supportedTypes == null)
        {
          IList<ExportType> exportTypeList = (IList<ExportType>) new List<ExportType>();
          ExportType exportType = new ExportType("Multisite", "Multisite", false);
          exportTypeList.Add(exportType);
          this.AddExportType("Site", typeof (Site), exportType.ChildTypes);
          this.supportedTypes = (IEnumerable<ExportType>) exportTypeList;
        }
        return this.supportedTypes;
      }
    }

    /// <inheritdoc />
    public override bool AllowToProcess(string typeName) => !PackagingOperations.IsMultisiteImportExportDisabled() && base.AllowToProcess(typeName);

    /// <inheritdoc />
    public override IEnumerable<IQueryable<object>> GetItemsQueries(
      ExportParams parameters)
    {
      if (!this.AllowToProcess(parameters.TypeName))
        yield return Enumerable.Empty<object>().AsQueryable<object>();
      else
        yield return (IQueryable<object>) MultisiteManager.GetManager(parameters.ProviderName).GetSites();
    }

    /// <inheritdoc />
    public override IDictionary<string, IEnumerable<string>> Dependencies
    {
      get
      {
        if (!this.dependenciesPopulated)
          this.PopulateDependencies();
        return base.Dependencies;
      }
    }

    /// <inheritdoc />
    public override void Count(Stream fileStream, ScanOperation operation)
    {
    }

    /// <inheritdoc />
    protected override void OnImportStart(ImportParams parameters)
    {
      base.OnImportStart(parameters);
      this.restartNeeded = false;
    }

    /// <inheritdoc />
    protected override void OnImportComplete(ImportParams parameters)
    {
      base.OnImportComplete(parameters);
      if (!this.restartNeeded)
        return;
      SystemManager.RestartApplication(OperationReason.FromKey("LocalizationChange"), SystemRestartFlags.ResetModel);
    }

    private void PopulateDependencies()
    {
      IEnumerable<ExportType> supportedTypes = this.SupportedTypes;
      Queue<ExportType> exportTypeQueue = new Queue<ExportType>(this.SupportedTypes);
      while (exportTypeQueue.Count > 0)
      {
        ExportType exportType = exportTypeQueue.Dequeue();
        foreach (ExportType childType in (IEnumerable<ExportType>) exportType.ChildTypes)
        {
          this.AddDependencies(childType.TypeName, exportType.TypeName);
          exportTypeQueue.Enqueue(childType);
        }
      }
      this.dependenciesPopulated = true;
    }

    private void AddExportType(string displayName, Type type, IList<ExportType> result)
    {
      ExportType exportType = new ExportType(displayName, type.FullName, false);
      result.Add(exportType);
    }
  }
}
