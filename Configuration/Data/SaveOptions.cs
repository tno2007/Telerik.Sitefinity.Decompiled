// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Data.SaveOptions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Packaging.Package;

namespace Telerik.Sitefinity.Configuration.Data
{
  internal class SaveOptions
  {
    public SaveOptions(
      bool databaseMode = false,
      bool skipLoadFromFile = false,
      bool inheritDatabase = false,
      ExportMode exportMode = ExportMode.Default)
    {
      this.DatabaseMode = databaseMode;
      this.SkipLoadFromFile = skipLoadFromFile;
      this.InheritDatabase = inheritDatabase;
      this.ExportMode = exportMode;
    }

    public IEnumerable<ConfigElement> ElementsToExport { get; set; }

    public bool SkipLoadFromFile { get; private set; }

    public bool DatabaseMode { get; private set; }

    public bool InheritDatabase { get; private set; }

    public ExportMode ExportMode { get; private set; }

    public OperationType OperationType { get; set; }

    public bool MoveWithParent { get; internal set; }

    public bool IsRuntime { get; set; }

    public Guid SiteId { get; set; }
  }
}
