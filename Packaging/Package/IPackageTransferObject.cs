// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Package.IPackageTransferObject
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;

namespace Telerik.Sitefinity.Packaging.Package
{
  /// <summary>Object used for transferring package streams</summary>
  internal interface IPackageTransferObject
  {
    /// <summary>Gets or sets the name of the package transfer object</summary>
    string Name { get; set; }

    /// <summary>Gets the content of the package transfer object</summary>
    /// <param name="dateLastUpdated">The date that this package was updated for the last time</param>
    /// <param name="forceExport">A flag indicating whether to export regardless dateLastUpdated</param>
    /// <returns>The content of the package transfer object. If the content is unmodified it returns empty Stream. If the content is non existent it returns null.</returns>
    Stream GetStream(DateTime dateLastUpdated = default (DateTime), bool forceExport = false);
  }
}
