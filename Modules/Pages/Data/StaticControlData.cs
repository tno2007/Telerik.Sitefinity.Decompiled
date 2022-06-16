// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Data.StaticControlData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.Data
{
  /// <summary>
  /// 
  /// </summary>
  [DebuggerDisplay("StaticControlData, Id={Id}, SiblingId={SiblingId}")]
  public class StaticControlData : ControlData
  {
    private Guid id;
    private Guid pageId;
    private string placeHolder;
    private string container;
    private string objectType;
    private bool shared;
    private bool allowSecurity;
    private Lstring caption;
    private Lstring description;
    private int ordinal;
    private IList<ControlProperty> properties;

    /// <summary>Gets or sets the pageId.</summary>
    /// <value>The pageId.</value>
    public override Guid Id
    {
      get => this.id;
      set => this.id = value;
    }

    /// <summary>Gets or sets the page pageId.</summary>
    /// <value>The page pageId.</value>
    public Guid DraftId
    {
      get => this.pageId;
      set => this.pageId = value;
    }

    /// <summary>Gets or sets the place holder of this control.</summary>
    /// <value>The place holder.</value>
    public override string PlaceHolder
    {
      get => this.placeHolder;
      set => this.placeHolder = value;
    }

    /// <summary>Gets or sets the container.</summary>
    /// <value>The container.</value>
    public string Container
    {
      get => this.container;
      set => this.container = value;
    }

    /// <summary>Gets or sets the type of the control.</summary>
    /// <value>The type of the control.</value>
    public override string ObjectType
    {
      get => this.objectType;
      set => this.objectType = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> is shared
    /// between all pages using the template which this control belongs to.
    /// This property is valid only for control added to page templates.
    /// The default value is true.
    /// </summary>
    /// <value><c>true</c> if shared; otherwise, <c>false</c>.</value>
    public override bool Shared
    {
      get => this.shared;
      set => this.shared = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to allow security trimming for this control.
    /// If this value is set to true people that do not have General View permissions will not see this control.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [allow security trimming]; otherwise, <c>false</c>.
    /// </value>
    public override bool AllowSecurityTrimming
    {
      get => this.allowSecurity;
      set => this.allowSecurity = value;
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

    /// <summary>Gets the child properties.</summary>
    /// <value>The child properties.</value>
    public override IList<ControlProperty> Properties
    {
      get
      {
        if (this.properties == null)
          this.properties = (IList<ControlProperty>) new List<ControlProperty>();
        return (IList<ControlProperty>) new ReadOnlyCollection<ControlProperty>(this.properties);
      }
    }

    /// <summary>Adds the property.</summary>
    /// <param name="property">The property.</param>
    public void AddProperty(StaticControlProperty property)
    {
      if (this.properties == null)
        this.properties = (IList<ControlProperty>) new List<ControlProperty>();
      this.properties.Add((ControlProperty) property);
    }

    /// <summary>Gets the ID of the container of this control.</summary>
    /// <value>The container pageId.</value>
    public override Guid ContainerId => this.pageId;
  }
}
