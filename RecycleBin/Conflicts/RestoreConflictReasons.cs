// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Conflicts.RestoreConflictReasons
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.RecycleBin.Conflicts
{
  /// <summary>List of Sitefinity known restore conflicts.</summary>
  internal enum RestoreConflictReasons
  {
    /// <summary>Default value meaning that the restore move is valid.</summary>
    None,
    /// <summary>
    /// The restore operation is invalid because there is already an content item with this URL.
    /// </summary>
    ExistingContentItemUrl,
    /// <summary>
    /// The restore operation is invalid because there is already an page with this URL.
    /// </summary>
    ExistingPageUrl,
    /// <summary>
    /// The restore operation is invalid because the template of the page that is being restored is missing.
    /// </summary>
    MissingTemplatePage,
    /// <summary>
    /// The restore operation is invalid because the parent page of the page that is being restored is missing.
    /// </summary>
    MissingParentPage,
    /// <summary>
    /// The restore operation is invalid because the parent item of the dynamic content that is being restored is also deleted.
    /// </summary>
    MissingParentDynamicContent,
    /// <summary>
    /// The batch restore operation is invalid because there are any conflicts when items are being restored.
    /// </summary>
    BatchRestoreConflicts,
  }
}
