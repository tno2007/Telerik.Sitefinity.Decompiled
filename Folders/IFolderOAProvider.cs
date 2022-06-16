// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Folders.IFolderOAProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Folders
{
  /// <summary>
  /// Marks OpenAccessDataProviders that have mappings to Folders table. Its methods have implementations in FolderExtensions.
  /// </summary>
  internal interface IFolderOAProvider : 
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
  }
}
