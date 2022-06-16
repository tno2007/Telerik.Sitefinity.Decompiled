// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.IContentItemControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// Classes that implement this interface can be associated with <see cref="!:ContentItem" />.
  /// </summary>
  public interface IContentItemControl
  {
    /// <summary>
    /// Gets or sets the ID of the <see cref="!:ContentItem" /> if the HTML is shared across multiple controls.
    /// </summary>
    Guid SharedContentID { get; set; }

    /// <summary>
    /// Gets or sets the name of the <see cref="!:ContentItem" /> provider.
    /// </summary>
    string ProviderName { get; set; }
  }
}
