// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.Responses.DataTypeResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Services.RelatedData.Responses
{
  /// <summary>
  /// Represents the response returned for type used for creating relations.
  /// </summary>
  public class DataTypeResponse
  {
    /// <summary>Gets or sets the ClrType.</summary>
    /// <value>The ClrType.</value>
    public string ClrType { get; set; }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>Gets or sets the name of the parent type if exists.</summary>
    /// <value>The name of the parent type.</value>
    public string ParentName { get; set; }

    /// <summary>
    /// Gets or sets a collection with available providers for this type.
    /// </summary>
    /// <value>The providers list.</value>
    public List<ProviderModel> Providers { get; set; }
  }
}
