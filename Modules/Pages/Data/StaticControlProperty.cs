// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Data.StaticControlProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;

namespace Telerik.Sitefinity.Modules.Pages.Data
{
  /// <summary>
  /// 
  /// </summary>
  public class StaticControlProperty : ControlProperty
  {
    private Guid id;
    private string name;
    private string value;
    private string validation;
    private Lstring caption;
    private Lstring description;
    private StaticControlProperty parent;
    private IList<ControlProperty> childProperties;
    private StaticControlData control;

    /// <summary>Gets or sets the ID of the template.</summary>
    /// <value>The pageId.</value>
    public override Guid Id
    {
      get => this.id;
      set => this.id = value;
    }

    /// <summary>Gets or sets the name this property.</summary>
    /// <value>The name.</value>
    public override string Name
    {
      get => this.name;
      set => this.name = value;
    }

    /// <summary>Gets or sets validation expression for this property.</summary>
    /// <value>The validation expression.</value>
    public override string Validation
    {
      get => this.validation;
      set => this.validation = value;
    }

    /// <summary>Gets or sets the value of this property.</summary>
    /// <value>The value.</value>
    public override string Value
    {
      get => this.value;
      set => this.value = value;
    }

    /// <summary>
    /// Gets or sets the user interface caption (label) for this property.
    /// </summary>
    /// <value>The caption.</value>
    public override string Caption
    {
      get => (string) this.caption;
      set => this.caption = (Lstring) value;
    }

    /// <summary>Gets or sets the description of this property.</summary>
    /// <value>The description.</value>
    public override string Description
    {
      get => (string) this.description;
      set => this.description = (Lstring) value;
    }

    /// <summary>Gets or sets the parent control.</summary>
    /// <value>The parent control.</value>
    [NonSerializableProperty]
    public override ObjectData Control
    {
      get => (ObjectData) this.control;
      set
      {
        this.control = (StaticControlData) value;
        foreach (ControlProperty childProperty in (IEnumerable<ControlProperty>) this.ChildProperties)
          childProperty.Control = value;
      }
    }

    /// <summary>Gets or sets the parent property.</summary>
    /// <value>The parent property.</value>
    [NonSerializableProperty]
    public override ControlProperty ParentProperty
    {
      get => (ControlProperty) this.parent;
      set => this.parent = (StaticControlProperty) value;
    }

    /// <summary>Gets the child properties.</summary>
    /// <value>The child properties.</value>
    [NonSerializableProperty]
    public override IList<ControlProperty> ChildProperties
    {
      get
      {
        if (this.childProperties == null)
          this.childProperties = (IList<ControlProperty>) new List<ControlProperty>();
        return (IList<ControlProperty>) new ReadOnlyCollection<ControlProperty>(this.childProperties);
      }
    }

    internal IList<ControlProperty> ChildPropertiesSerialization
    {
      get => this.childProperties;
      set => this.childProperties = value;
    }

    /// <summary>Adds the child property.</summary>
    /// <param name="property">The property.</param>
    public void AddChildProperty(StaticControlProperty property)
    {
      if (this.childProperties == null)
        this.childProperties = (IList<ControlProperty>) new List<ControlProperty>();
      this.childProperties.Add((ControlProperty) property);
    }
  }
}
