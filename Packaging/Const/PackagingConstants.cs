// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Const.PackagingConstants
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Packaging.Const
{
  /// <summary>Class containing packaging constants</summary>
  internal class PackagingConstants
  {
    internal const string ConverterPrefix = "Export/Import";
    internal const string RootId = "RootId";
    internal const string OriginalContentId = "OriginalContentId";
    internal const string ParentLibraryType = "ParentLibraryType";
    internal const string AddonNameKey = "sf-addon-name";
    internal const string PackageTypeKey = "sf-package-type";
    internal const string AddonModulePathKey = "sf-addon-module-path";
    internal const string AddonErrorStatusKey = "sf-addon-error-status-key";
    internal const string PackagingModuleName = "PackagingModule";
    internal const string SitefinityFolderPathName = "App_Data\\Sitefinity";
    internal const string ExportRootFolderPathName = "App_Data\\Sitefinity\\Export";
    internal const string ImportRootFolderPathName = "App_Data\\Sitefinity\\Import";
    internal const string AddonsRootFolderPathName = "App_Data\\Sitefinity\\AddOns";
    internal const string DefaultRootFolderPathName = "App_Data\\Sitefinity\\Deployment";
    internal const string ArchiveTempFolderName = "TEMP_ARCHIVE";
    internal const string ArchiveFileExtension = ".zip";
    internal const string ContentFileNameExtension = ".sfpackage.xml";
    internal const string RelativeRootPrefix = "~/";
    internal const string ContentFolderName = "Content";
    internal const string StructureFolderName = "Structure";
    internal const long UploadFileSizeLimitInBytes = 2147483648;
    internal const string ExportedArchiveNameFormat = "SitefinityExport-{0:yyyy-MM-dd}";
    internal const string ArchivingMessage = "Archiving...";
    internal const string ArchivedMessage = "Archiving completed";
    internal const string ExtractingMessage = "Extracting...";
    internal const string ExtractedMessage = "Extracting completed";
    internal const string UploadInvalidArchiveExceptionExceptionMessageFormat = "Archive for import is not valid {0} file.";
    internal const string UploadInvalidArchiveExtensionExceptionExceptionMessageFormat = "Archive for import must be in {0} format.";
    internal const string ExportedArchiveExceedsFileLimitExceptionMessage = "Export failed because ZIP file exceeds the 2Gb size limit";
    internal const string NoStructureOrContentImportedExceptionMessage = "No structure or content were imported from archive directory";
    internal const string ExportingStructureMessageFormat = "Exporting {0} structure";
    internal const string ExportedStructureMessageFormat = "Exported {0} structure";
    internal const string ExportingStructureFailedMessageFormat = "Exporting {0} structure failed";
    internal const string ExportedStructureUpToDatеMessageFormat = "{0} are up to date";
    internal const string ImportingStructureMessageFormat = "Importing {0} structure";
    internal const string ImportedStructureMessageFormat = "Imported {0} structure";
    internal const string ImportingStructureFailedMessageFormat = "Importing {0} structure failed";
    internal const string DeletingStructureMessageFormat = "Deleting {0} structure";
    internal const string DeletedStructureMessageFormat = "Deleted {0} structure";
    internal const string NotDeletedStructureMessageFormat = "{0} structure was not deleted.";
    internal const string ExportingContentMessageFormat = "Exporting {0}";
    internal const string ExportedContentMessageFormat = "Exported {0}";
    internal const string ExportingContentFailedMessageFormat = "Exporting {0} failed";
    internal const string ImportingContentMessageFormat = "Importing {0}";
    internal const string ImportedContentMessageFormat = "Imported {0}";
    internal const string ImportingContentFailedMessageFormat = "Importing {0} failed";
    internal const string UninstallingAddonMessageFormat = "Uninstalling {0}";
    internal const string UninstalledAddonMessageFormat = "Uininstalled {0}";
    internal const string UninstallAddonFailedMessageFormat = "Uninstalling {0} failed";
  }
}
