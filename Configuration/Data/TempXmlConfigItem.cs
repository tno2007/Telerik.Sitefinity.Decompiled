// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Data.TempXmlConfigItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration.Model;

namespace Telerik.Sitefinity.Configuration.Data
{
  internal class TempXmlConfigItem : IXmlConfigItem
  {
    /// <inheritdoc />
    public string Path { get; set; }

    /// <inheritdoc />
    public string Data { get; set; }

    /// <inheritdoc />
    public DateTime LastModified { get; set; }
  }
}
