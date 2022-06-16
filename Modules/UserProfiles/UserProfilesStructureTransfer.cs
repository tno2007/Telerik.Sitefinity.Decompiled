// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.UserProfilesStructureTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicModules.Builder.ExportImport;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.UserProfiles
{
  internal class UserProfilesStructureTransfer : StaticStructureTransferBase
  {
    private IEnumerable<ExportType> supportedTypes;
    private const string AreaName = "User profiles";
    private const string UserFriendlyNameAttribute = "UserFriendlyName";

    /// <inheritdoc />
    public override string Area => "User profiles";

    /// <inheritdoc />
    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        if (this.supportedTypes == null)
        {
          IList<ExportType> exportTypeList = (IList<ExportType>) new List<ExportType>();
          ExportType exportType = new ExportType("User profiles", "User profiles");
          exportTypeList.Add(exportType);
          this.supportedTypes = (IEnumerable<ExportType>) exportTypeList;
        }
        return this.supportedTypes;
      }
    }

    /// <inheritdoc />
    public override void Uninstall(string sourceName)
    {
      base.Uninstall(sourceName);
      this.DeleteUserProfiles(sourceName);
    }

    /// <inheritdoc />
    internal override Stream ExportModule(
      IExportableModule module,
      DateTime lastModified,
      bool forceExport)
    {
      return base.ExportModule(module, lastModified, true);
    }

    internal override Stream ExportModuleConfigs(
      IExportableModule module,
      DateTime lastModified,
      bool forceExport)
    {
      return base.ExportModuleConfigs(module, lastModified, true);
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
    protected override void ExportModuleConfigs(IExportableModule module, MemoryStream memoryStream)
    {
      this.ExportModuleConfig(memoryStream);
      this.ExportContentViewConfig(memoryStream);
    }

    /// <inheritdoc />
    protected override IExportableModule GetModule() => (IExportableModule) new UserProfileModuleProxy();

    /// <inheritdoc />
    protected override void UpdateModuleMetaData(
      MetadataManager metadataManager,
      IList<MetaType> importedMetaTypes,
      List<MetaType> persistedMetaTypes)
    {
      ModuleImporterHelper instance = ModuleImporterHelper.GetInstance();
      foreach (MetaType importedMetaType1 in (IEnumerable<MetaType>) importedMetaTypes)
      {
        MetaType importedMetaType = importedMetaType1;
        string className = importedMetaType.ClassName;
        MetaTypeAttribute metaTypeAttribute = importedMetaType.MetaAttributes.FirstOrDefault<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => a.Name == "UserFriendlyName"));
        if (metaTypeAttribute != null)
        {
          className = metaTypeAttribute.Value;
          importedMetaType.MetaAttributes.Remove(metaTypeAttribute);
        }
        MetaType persistedType = persistedMetaTypes.SingleOrDefault<MetaType>((Func<MetaType, bool>) (t => t.FullTypeName == importedMetaType.FullTypeName));
        MetaTypeDescription metaTypeDescription;
        if (persistedType == null)
        {
          persistedType = metadataManager.CreateMetaType(importedMetaType.Namespace, importedMetaType.ClassName);
          metaTypeDescription = metadataManager.CreateMetaTypeDescription(persistedType.Id);
          persistedType.Origin = importedMetaType.Origin;
        }
        else
          metaTypeDescription = metadataManager.GetMetaTypeDescriptions().Where<MetaTypeDescription>((Expression<Func<MetaTypeDescription, bool>>) (d => d.MetaTypeId == importedMetaType.Id)).FirstOrDefault<MetaTypeDescription>();
        if (metaTypeDescription != null)
          metaTypeDescription.UserFriendlyName = className;
        instance.UpdateMetaType(metadataManager, importedMetaType, persistedType);
      }
    }

    /// <inheritdoc />
    protected override MetaType CopyMetaType(
      MetaType metaType,
      MetadataManager metadataManager)
    {
      MetaType metaType1 = base.CopyMetaType(metaType, metadataManager);
      MetaTypeDescription descriptionForMetaType = metadataManager.GetMetaTypeDescriptionForMetaType(metaType.Id);
      if (descriptionForMetaType != null)
      {
        MetaTypeAttribute metaTypeAttribute = new MetaTypeAttribute();
        metaTypeAttribute.Name = "UserFriendlyName";
        metaTypeAttribute.Value = descriptionForMetaType.UserFriendlyName;
        metaType1.MetaAttributes.Add(metaTypeAttribute);
      }
      return metaType1;
    }

    /// <inheritdoc />
    protected override void DeleteMetaData(
      MetadataManager metadataManager,
      List<MetaType> deletedTypes)
    {
      foreach (MetaType deletedType in deletedTypes)
        this.DeleteUserProfile(metadataManager, deletedType);
    }

    /// <inheritdoc />
    protected override void ImportConfiguration(
      KeyValuePair<Type, string> config,
      ConfigManager configurationManager,
      string transactionName)
    {
      bool overrideOrigin = true;
      List<string> pathsOfElementsToOverride = (List<string>) null;
      if (config.Key == typeof (ContentViewConfig))
      {
        pathsOfElementsToOverride = new List<string>();
        string format = "contentViewConfig/contentViewControls/{0}";
        foreach (Type dynamicType in this.GetModule().GetModuleMetaTypes().Select<MetaType, Type>((Func<MetaType, Type>) (m => m.ClrType)))
        {
          string viewDefinitionName = UserProfilesHelper.GetContentViewDefinitionName(dynamicType);
          string str = string.Format(format, (object) viewDefinitionName);
          pathsOfElementsToOverride.Add(str);
        }
      }
      configurationManager.Import(config.Key, config.Value, StructureTransferBase.CurrentOrigin, overrideOrigin: overrideOrigin, pathsOfElementsToOverride: ((IEnumerable<string>) pathsOfElementsToOverride));
      this.RaiseItemImportedForConfigSection(config.Key.FullName, transactionName);
    }

    /// <inheritdoc />
    protected override void SetOrigin(IList<MetaType> metaTypes)
    {
      string currentOrigin = StructureTransferBase.CurrentOrigin;
      if (currentOrigin == null)
        return;
      foreach (MetaType metaType in (IEnumerable<MetaType>) metaTypes)
      {
        metaType.Origin = currentOrigin;
        foreach (MetaField field in (IEnumerable<MetaField>) metaType.Fields)
          field.Origin = currentOrigin;
      }
    }

    private void DeleteUserProfiles(string sourceName)
    {
      IExportableModule module = this.GetModule();
      if (module == null)
        return;
      string origin = new AddonOrigin(sourceName, (string) null).ToString();
      IList<MetaType> list = (IList<MetaType>) this.GetModuleMetaTypes(module).Where<MetaType>((Func<MetaType, bool>) (m => AddonOrigin.AddonNamesEqual(m.Origin, origin))).ToList<MetaType>();
      if (list == null || list.Count <= 0)
        return;
      foreach (MetaType metaType in (IEnumerable<MetaType>) list)
        this.DeleteUserProfile(MetadataManager.GetManager(), metaType);
    }

    private void DeleteUserProfile(MetadataManager metadataManager, MetaType metaType)
    {
      MetaTypeDescription metaTypeDescription = metadataManager.GetMetaTypeDescriptions().Where<MetaTypeDescription>((Expression<Func<MetaTypeDescription, bool>>) (d => d.MetaTypeId == metaType.Id)).FirstOrDefault<MetaTypeDescription>();
      if (metaTypeDescription == null)
        return;
      UserProfilesHelper.DeleteUserProfileType(metaTypeDescription.Id, (string) null, false);
    }

    private void ExportModuleConfig(MemoryStream memoryStream)
    {
      IList<ConfigElement> configElementsToBeExported = (IList<ConfigElement>) new List<ConfigElement>()
      {
        (ConfigElement) this.GetModule().ModuleConfig
      };
      ModuleImporterHelper.GetInstance().ExportContentModuleConfigurationElements((IEnumerable<ConfigElement>) configElementsToBeExported, (Stream) memoryStream, true, this.ExportMode);
    }

    private void ExportContentViewConfig(MemoryStream memoryStream)
    {
      ConfigManager manager = ConfigManager.GetManager();
      List<ConfigElement> configElementsToBeExported = new List<ConfigElement>();
      foreach (Type dynamicType in this.GetModule().GetModuleMetaTypes().Select<MetaType, Type>((Func<MetaType, Type>) (m => m.ClrType)))
      {
        string viewDefinitionName = UserProfilesHelper.GetContentViewDefinitionName(dynamicType);
        ContentViewControlElement contentViewControl = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls[viewDefinitionName];
        if (contentViewControl != null)
          configElementsToBeExported.Add((ConfigElement) contentViewControl);
      }
      ModuleExporter.WriteToStream(manager.Export((IEnumerable<ConfigElement>) configElementsToBeExported, true, this.ExportMode), (Stream) memoryStream);
    }
  }
}
