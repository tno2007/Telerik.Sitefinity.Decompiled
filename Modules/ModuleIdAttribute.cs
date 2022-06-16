// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ModuleIdAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// Uniquely identifies a Sitefinity module OR identifies a functional part of Sitefinity that depends on
  /// the presence of a module
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
  public sealed class ModuleIdAttribute : Attribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ModuleIdAttribute" /> class.
    /// </summary>
    /// <param name="id">Module identity.</param>
    public ModuleIdAttribute(Guid id) => this.Id = id;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ModuleIdAttribute" /> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public ModuleIdAttribute(string id) => this.Id = Guid.Parse(id);

    /// <summary>
    /// Determines whether the specified <see cref="T:System.String" /> is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.String" /> to compare with this instance.</param>
    /// <returns>
    /// 	<c>true</c> if the specified <see cref="T:System.String" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object obj) => obj is string str ? this.Id.ToString() == str : base.Equals(obj);

    /// <inheritdoc />
    public override int GetHashCode() => this.Id.GetHashCode();

    /// <summary>Gets or sets the id.</summary>
    /// <value>The id.</value>
    public Guid Id { get; private set; }
  }
}
