// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.WcfPropertyType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>
  /// Defines the types of WCF properties for which we need to differentiate the implementations
  /// for value setting operation.
  /// </summary>
  public enum WcfPropertyType
  {
    /// <summary>Indicates standard property.</summary>
    Standard,
    GenericCollection,
    /// <summary>Indicates property that is a collection.</summary>
    /// <remarks>
    /// Type of property is a type that implements <see cref="F:Telerik.Sitefinity.Modules.Pages.Web.Services.WcfPropertyType.IList" />.
    /// </remarks>
    IList,
    /// <summary>Indicates property that is dictionary.</summary>
    /// <remarks>
    /// Type of property is a type that implements <see cref="!:IDictionary" />.
    /// </remarks>
    Dictionary,
  }
}
