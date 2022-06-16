// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.UserFiles.UserFilesConstants
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Libraries.UserFiles
{
  internal class UserFilesConstants
  {
    /// <summary>Identity of the page for User files</summary>
    public static readonly Guid UserFilesNodeId = new Guid("602D7B72-7A8C-48BB-B023-A98249C2D2F2");
    /// <summary>Identity of the page for User files</summary>
    public static readonly Guid HomePageId = new Guid("E76CCE07-8ACF-45C4-A452-A7F3D503DA52");
    /// <summary>Identity of the page for User files documents listing</summary>
    public static readonly Guid UserFilesDocumentsPageId = new Guid("3ABD34E0-C6CE-4D32-8FC6-991A964B2A70");
    /// <summary>
    /// Id of the default User file library for the Downloadable goods
    /// </summary>
    public static readonly Guid DefaultDownloadableGoodsLibraryId = new Guid("E5915E51-707E-4232-9830-68F2800067F8");
    /// <summary>
    /// TThe control id of the ContentView inside the UserFiles screen.
    /// </summary>
    public const string UserFilesContentViewControlId = "userFilesCntView";
    /// <summary>
    /// TThe control id of the ContentView inside the UserFiles documents screen.
    /// </summary>
    public const string UserFilesDocumentsContentViewControlId = "userFilesDocumentsCntView";
    /// <summary>Localization resources' class Id for User Files</summary>
    public static readonly string ResourceClassId = typeof (UserFilesResources).Name;
  }
}
