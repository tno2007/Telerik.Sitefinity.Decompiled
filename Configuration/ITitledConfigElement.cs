// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ITitledConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// ConfigElement that has <c>Name</c> (used as a key) and <c>Title</c> (user-firnely name) properites,
  /// and the <c>Title</c> can be optionally localized using the <c>ResourceClassId</c> property.
  /// </summary>
  public interface ITitledConfigElement
  {
    /// <summary>(Code) name, used as a key.</summary>
    string Name { get; }

    /// <summary>
    /// User-frinely name used in the UI; localizable through <see cref="P:Telerik.Sitefinity.Configuration.ITitledConfigElement.ResourceClassId" />.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Optional class name to be used to localize the <see cref="P:Telerik.Sitefinity.Configuration.ITitledConfigElement.Title" /> property; when <c>null</c>,
    /// <see cref="P:Telerik.Sitefinity.Configuration.ITitledConfigElement.Title" /> is used verbatim.
    /// </summary>
    string ResourceClassId { get; }
  }
}
