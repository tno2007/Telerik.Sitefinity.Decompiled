// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibrariesStructureTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Libraries
{
  internal class LibrariesStructureTransfer : StaticStructureTransferBase
  {
    private IEnumerable<ExportType> supportedTypes;
    private const string AreaName = "Libraries";

    public override string Area => "Libraries";

    /// <inheritdoc />
    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        if (this.supportedTypes == null)
        {
          IList<ExportType> exportTypeList = (IList<ExportType>) new List<ExportType>();
          ExportType exportType = new ExportType("Libraries", "Libraries");
          exportTypeList.Add(exportType);
          this.AddExportType("Images", typeof (Album), exportType.ChildTypes);
          this.AddExportType("Videos", typeof (VideoLibrary), exportType.ChildTypes);
          this.AddExportType("Documents and files", typeof (DocumentLibrary), exportType.ChildTypes);
          this.supportedTypes = (IEnumerable<ExportType>) exportTypeList;
        }
        return this.supportedTypes;
      }
    }

    /// <inheritdoc />
    public override bool IsAvailableForCurrentSite() => SystemManager.IsModuleAccessible("Libraries");

    /// <inheritdoc />
    internal override IList<string> MvcWidgetTemplatesAreaNames => (IList<string>) new List<string>()
    {
      "ImageGallery",
      "VideoGallery",
      "DocumentsList"
    };

    /// <inheritdoc />
    protected override void ExportModuleConfigs(IExportableModule module, MemoryStream memoryStream)
    {
      ModuleImporterHelper instance = ModuleImporterHelper.GetInstance();
      bool exportProviders = this.ExportMode == ExportMode.Deployment;
      instance.ExportContentModuleWorkflowConfiguration(module.ModuleName, (Stream) memoryStream, this.ExportMode);
      instance.ExportContentModuleToolboxConfiguration(module.ModuleName, (Stream) memoryStream, this.ExportMode);
      instance.ExportContentModuleContentViewConfiguration(module.ModuleName, (Stream) memoryStream);
      IList<ConfigElement> list = (IList<ConfigElement>) instance.GetContentModuleBaseConfigurationElements(module.ModuleConfig, exportProviders).ToList<ConfigElement>();
      list.Add((ConfigElement) Config.Get<LibrariesConfig>().Images.Thumbnails);
      list.Add((ConfigElement) Config.Get<LibrariesConfig>().Videos.Thumbnails);
      instance.ExportContentModuleConfigurationElements((IEnumerable<ConfigElement>) list, (Stream) memoryStream, !exportProviders, this.ExportMode);
    }

    private void AddExportType(string displayName, Type type, IList<ExportType> result)
    {
      ExportType exportType = new ExportType(displayName, type.FullName);
      result.Add(exportType);
    }
  }
}
