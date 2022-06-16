// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PagesStructureTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.ExportImport;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.Pages
{
  internal class PagesStructureTransfer : StaticStructureTransferBase
  {
    private IEnumerable<ExportType> supportedTypes;
    private const string AreaName = "Pages";
    private static readonly string[] PathsToOverride = new string[2]
    {
      "contentViewConfig/contentViewControls/BackendPages",
      "contentViewConfig/contentViewControls/FrontendPages"
    };

    /// <inheritdoc />
    public override string Area => "Pages";

    /// <inheritdoc />
    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        if (this.supportedTypes == null)
        {
          IList<ExportType> exportTypeList = (IList<ExportType>) new List<ExportType>();
          ExportType exportType = new ExportType("Pages", "Pages");
          exportTypeList.Add(exportType);
          this.supportedTypes = (IEnumerable<ExportType>) exportTypeList;
        }
        return this.supportedTypes;
      }
    }

    /// <inheritdoc />
    protected override List<IPackageTransferObject> GetPackageTransferObjects(
      IExportableModule module)
    {
      List<IPackageTransferObject> packageTransferObjects = new List<IPackageTransferObject>();
      string name = this.Area + ".sf";
      packageTransferObjects.Add((IPackageTransferObject) new StaticModuleTransferObject(new Func<IExportableModule, DateTime, bool, Stream>(((StaticStructureTransferBase) this).ExportModule), module, name));
      packageTransferObjects.Add((IPackageTransferObject) new StaticModuleTransferObject(new Func<IExportableModule, DateTime, bool, Stream>(((StaticStructureTransferBase) this).ExportModuleConfigs), module, "configs.sf"));
      return packageTransferObjects;
    }

    /// <inheritdoc />
    protected override void ImportPackageTransferObjects(
      IEnumerable<IPackageTransferObject> packageTransferObjects,
      string transactionName)
    {
      this.ImportModule(packageTransferObjects, (string) null, transactionName);
      this.ImportModuleConfigurations(packageTransferObjects, (string) null, transactionName);
    }

    /// <inheritdoc />
    protected override void ImportConfiguration(
      KeyValuePair<Type, string> config,
      ConfigManager configurationManager,
      string transactionName)
    {
      bool overrideOrigin = !StructureTransferBase.CurrentOrigin.IsNullOrEmpty() || StructureTransferBase.PackageType == PackageType.Deployment && config.Key == typeof (ContentViewConfig);
      configurationManager.Import(config.Key, config.Value, StructureTransferBase.CurrentOrigin, overrideOrigin: overrideOrigin, pathsOfElementsToOverride: ((IEnumerable<string>) PagesStructureTransfer.PathsToOverride));
      this.RaiseItemImportedForConfigSection(config.Key.FullName, transactionName);
    }

    /// <inheritdoc />
    protected override void ExportModuleConfigs(IExportableModule module, MemoryStream memoryStream)
    {
      ConfigManager manager = ConfigManager.GetManager();
      List<ConfigElement> configElementList = new List<ConfigElement>();
      ContentViewControlElement contentViewControl1 = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls["FrontendPages"];
      ContentViewControlElement contentViewControl2 = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls["BackendPages"];
      configElementList.Add((ConfigElement) contentViewControl1);
      configElementList.Add((ConfigElement) contentViewControl2);
      List<ConfigElement> configElementsToBeExported = configElementList;
      int exportMode = (int) this.ExportMode;
      ModuleExporter.WriteToStream(manager.Export((IEnumerable<ConfigElement>) configElementsToBeExported, true, (ExportMode) exportMode), (Stream) memoryStream);
    }

    /// <inheritdoc />
    protected override IExportableModule GetModule() => (IExportableModule) new PagesModuleProxy();
  }
}
