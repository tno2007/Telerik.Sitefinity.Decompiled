// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.ICustomSurrogateDescriptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>
  /// Interface for creating a custom surrogate using specific properties.
  /// </summary>
  internal interface ICustomSurrogateDescriptor
  {
    /// <summary>The properties that the surrogate will possess.</summary>
    PropertyDescriptorCollection Properties { get; set; }

    /// <summary>
    /// Indicates weather the surrogate will inherit from the source type.
    /// </summary>
    bool InheritsFromSourceType { get; set; }
  }
}
