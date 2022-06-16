// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.DummyDataProviderSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Represents the configuration elements associated with a provider.
  /// </summary>
  internal class DummyDataProviderSettings : IDataProviderSettings
  {
    public string Description { get; set; }

    public bool Enabled { get; set; }

    public string Name { get; set; }

    public NameValueCollection Parameters { get; set; }

    public Type ProviderType { get; set; }

    public string ResourceClassId { get; set; }
  }
}
