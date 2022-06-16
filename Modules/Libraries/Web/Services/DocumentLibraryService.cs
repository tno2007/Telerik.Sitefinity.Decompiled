// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.DocumentLibraryService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Libraries.Model;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>Service for document libraries</summary>
  public class DocumentLibraryService : 
    LibraryService<DocumentLibrary, Document, LibraryViewModel, LibrariesManager>
  {
    internal override Guid LandingPageId => LibrariesModule.LibraryDocumentsPageId;
  }
}
