// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersistenceType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Pages.PropertyPersisters
{
  /// <summary>
  /// Enumeration that represents the type of persistence Sitefinity ought to use to persist
  /// a given property.
  /// </summary>
  public enum PropertyPersistenceType
  {
    /// <summary>Property cannot be persisted by Sitefinity.</summary>
    NotPersistable,
    /// <summary>
    /// Property can be converted to a string and will be persisted as a string.
    /// </summary>
    StringConvertable,
    /// <summary>
    /// Property is a complex property which has its own properties and its child
    /// properties will be persisted recursively.
    /// </summary>
    ComplexProperty,
    /// <summary>
    /// Property type implements <see cref="F:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersistenceType.IEnumerable" /> interface, but does not implement
    /// neither <see cref="F:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersistenceType.IList" /> nor <see cref="F:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersistenceType.IDictionary" /> interfaces and will be
    /// persisted as collection. Note that Sitefinity can only modify the child properties of
    /// the items of this property, but cannot add or remove the items.
    /// </summary>
    IEnumerable,
    /// <summary>
    /// Property type implements <see cref="F:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersistenceType.IList" /> interface, which means that Sitefinity will
    /// persist the items of the list and recursively persist the child properties of each item.
    /// Sitefinity is also able to add or remove items from this property during runtime.
    /// </summary>
    IList,
    /// <summary>
    /// Property type implements a generic IList interface, which means that Sitefinity will persist
    /// the items of the list and recursively persist the child properties of each item. Sitefinity
    /// is also able to add or remove items from this property during runtime.
    /// </summary>
    IListGeneric,
    /// <summary>
    /// Property type implements <see cref="F:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersistenceType.IDictionary" /> interface, which means that Sitefinity
    /// will persist the entries of the dictionary and recursively persist the child properties
    /// of each entry. Sitefinity is also able to add or remove items from this property during runtime.
    /// </summary>
    IDictionary,
  }
}
