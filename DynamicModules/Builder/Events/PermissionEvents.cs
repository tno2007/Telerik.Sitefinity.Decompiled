// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.PermissionDataEventBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>A base class for all permission related events</summary>
  public class PermissionDataEventBase : 
    IPermissionDataEvent,
    IDataEvent,
    IEvent,
    IModuleBuilderEvent,
    IHasTitle,
    IPropertyChangeDataEvent
  {
    private IDictionary<string, PropertyChange> changedPropertyNames;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.PermissionDataEventBase" /> class.
    /// </summary>
    public PermissionDataEventBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.PermissionDataEventBase" /> class.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="action">The action.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="userId">The user id.</param>
    internal PermissionDataEventBase(
      Guid itemId,
      Type itemType,
      string action,
      string dataProviderName,
      Guid userId)
    {
      this.Action = action;
      this.ItemType = itemType;
      this.ItemId = itemId;
      this.ProviderName = dataProviderName;
      this.UserId = userId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.PermissionDataEventBase" /> class.
    /// </summary>
    /// <param name="source">The source.</param>
    internal PermissionDataEventBase(PermissionDataEventBase source)
    {
      this.ItemId = source.ItemId;
      this.ItemType = source.ItemType;
      this.ProviderName = source.ProviderName;
      this.Action = source.Action;
    }

    /// <inheritdoc />
    public string Origin { get; set; }

    /// <inheritdoc />
    public string Action { get; set; }

    /// <inheritdoc />
    public Guid ItemId { get; set; }

    /// <inheritdoc />
    public Type ItemType { get; set; }

    /// <inheritdoc />
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the id of the user who raises the event.</summary>
    public Guid UserId { get; set; }

    /// <inheritdoc />
    public IDictionary<string, PropertyChange> ChangedProperties
    {
      get
      {
        if (this.changedPropertyNames == null)
          this.changedPropertyNames = (IDictionary<string, PropertyChange>) new Dictionary<string, PropertyChange>();
        return this.changedPropertyNames;
      }
    }

    internal string Title { get; set; }

    /// <inheritdoc />
    string IHasTitle.GetTitle(CultureInfo culture) => this.Title;
  }
}
