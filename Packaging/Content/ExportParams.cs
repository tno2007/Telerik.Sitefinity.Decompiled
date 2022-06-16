// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Content.ExportParams
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.IO;

namespace Telerik.Sitefinity.Packaging.Content
{
  /// <summary>Defines export settings for particular type.</summary>
  internal class ExportParams
  {
    private int bufferSize = 100;
    private string itemSortExpression = "Id";

    /// <summary>Gets or sets the name of the type.</summary>
    /// <value>The name of the type.</value>
    public string TypeName { get; set; }

    /// <summary>Gets or sets the provider name.</summary>
    /// <value>The provider name.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the languages.</summary>
    /// <value>The languages.</value>
    public IEnumerable<string> Languages { get; set; }

    /// <summary>
    /// Gets or sets the output stream where items will be written.
    /// </summary>
    /// <value>The stream.</value>
    public Stream Stream { get; set; }

    /// <summary>Gets or sets the items filter expression.</summary>
    /// <value>The items filter expression.</value>
    public string ItemsFilterExpression { get; set; }

    /// <summary>Gets or sets the items sort expression.</summary>
    /// <value>The items sort expression.</value>
    public string ItemsSortExpression
    {
      get => this.itemSortExpression;
      set => this.itemSortExpression = value;
    }

    /// <summary>Gets or sets the size of the buffer.</summary>
    /// <value>The size of the buffer.</value>
    public int BufferSize
    {
      get => this.bufferSize;
      set => this.bufferSize = value;
    }
  }
}
