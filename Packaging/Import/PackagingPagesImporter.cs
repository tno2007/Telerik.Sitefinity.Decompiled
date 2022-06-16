// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Import.PackagingPagesImporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.SiteSync;

namespace Telerik.Sitefinity.Packaging.Import
{
  /// <summary>Packaging pages importer</summary>
  internal class PackagingPagesImporter : PagesImporter
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.Import.PackagingPagesImporter" /> class.
    /// </summary>
    public PackagingPagesImporter()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.Import.PackagingPagesImporter" /> class.
    /// </summary>
    /// <param name="registrationPrefix">The registration prefix.</param>
    public PackagingPagesImporter(string registrationPrefix)
      : base(registrationPrefix)
    {
    }

    /// <inheritdoc />
    internal override void SetPageDataProperties(
      IDataItem dataItem,
      WrapperObject wrapperObject,
      PageManager manager)
    {
    }
  }
}
