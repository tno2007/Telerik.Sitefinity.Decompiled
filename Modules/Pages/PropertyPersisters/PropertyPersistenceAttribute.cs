// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersistenceAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Pages.PropertyPersisters
{
  /// <summary>
  /// An attribute applied to properties of persistent classes to specify persistence settings.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class PropertyPersistenceAttribute : Attribute
  {
    /// <summary>
    /// Read-only field which determines the location to which the value of a property is persisted.
    /// If true, the property is persisted in the <see cref="!:PageData" /> object and is valid for
    /// the whole page, not only for the control on which it is set.
    /// </summary>
    public readonly bool PersistInPage;

    public PropertyPersistenceAttribute()
      : this(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersistenceAttribute" /> class.
    /// </summary>
    /// <param name="persistInPage">if set to <c>true</c> the property is persisted in the page, not the control.</param>
    public PropertyPersistenceAttribute(bool persistInPage) => this.PersistInPage = persistInPage;

    /// <summary>
    /// If the corresponding property is going to be persisted in the page (PersistInPage = true),
    /// this specifies the name of the property of the page, which handles the persistence
    /// </summary>
    public string PagePropertyName { get; set; }

    /// <summary>Gets or sets the is key.</summary>
    /// <value>The is key.</value>
    public bool IsKey { get; set; }
  }
}
