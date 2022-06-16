// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Content.ImportParams
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;

namespace Telerik.Sitefinity.Packaging.Content
{
  /// <summary>Defines import settings for particular type.</summary>
  internal class ImportParams
  {
    /// <summary>Gets or sets the name of the type.</summary>
    /// <value>The name of the type.</value>
    public string TypeName { get; set; }

    /// <summary>Gets or sets the provider name.</summary>
    /// <value>The provider name.</value>
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the output stream from where items will be read.
    /// </summary>
    /// <value>The stream.</value>
    public Stream Stream { get; set; }
  }
}
