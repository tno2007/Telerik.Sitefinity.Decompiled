// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Events.DynamicContentEventBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Events
{
  public class DynamicContentEventBase : 
    IDataEvent,
    IEvent,
    ILifecycleEvent,
    IApprovalWorkflowEvent,
    IMultilingualEvent,
    IHasTitle,
    IPropertyChangeDataEvent,
    IRecyclableDataEvent
  {
    private IDictionary<string, PropertyChange> changedPropertyNames;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Events.DynamicContentEventBase" /> class.
    /// </summary>
    public DynamicContentEventBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Events.DynamicContentEventBase" /> class.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="action">The action.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="originalContentId">The original content id.</param>
    /// <param name="status">The status.</param>
    /// <param name="language">The language.</param>
    internal DynamicContentEventBase(
      Guid itemId,
      Type itemType,
      string action,
      string dataProviderName,
      Guid originalContentId,
      string status,
      string language)
    {
      this.Action = action;
      this.ItemType = itemType;
      this.ItemId = itemId;
      this.OriginalContentId = originalContentId;
      this.ProviderName = dataProviderName;
      this.Status = status;
      this.Language = language;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Events.DynamicContentEventBase" /> class.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="action">The action.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="originalContentId">The original content id.</param>
    /// <param name="status">The status.</param>
    internal DynamicContentEventBase(
      Guid itemId,
      Type itemType,
      string action,
      string dataProviderName,
      Guid originalContentId,
      string status)
      : this(itemId, itemType, action, dataProviderName, originalContentId, status, (string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Events.DynamicContentEventBase" /> class.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="action">The action.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="originalContentId">The original content id.</param>
    internal DynamicContentEventBase(
      Guid itemId,
      Type itemType,
      string action,
      string dataProviderName,
      Guid originalContentId)
      : this(itemId, itemType, action, dataProviderName, originalContentId, (string) null, (string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Events.DynamicContentEventBase" /> class.
    /// </summary>
    /// <param name="source">The source.</param>
    internal DynamicContentEventBase(DynamicContentEventBase source)
    {
      this.ItemId = source.ItemId;
      this.Language = source.Language;
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

    /// <inheritdoc />
    public string Status { get; set; }

    /// <inheritdoc />
    public bool Visible { get; set; }

    /// <inheritdoc />
    public Guid OriginalContentId { get; set; }

    /// <inheritdoc />
    public string ApprovalWorkflowState { get; set; }

    /// <inheritdoc />
    public string Language { get; set; }

    internal string Title { get; set; }

    /// <inheritdoc />
    string IHasTitle.GetTitle(CultureInfo culture) => this.Title;

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

    /// <summary>
    /// Gets or sets whether a delete event means permanent deletion
    /// or the item was marked as deleted (a.k.a. sent to the Recycle Bin).
    /// </summary>
    /// <value>
    /// <c>true</c>if the item was permanently deleted, <c>false</c> if the item
    /// was marked as deleted and <c>null</c> for events with action different from delete.
    /// </value>
    public RecycleBinAction RecycleBinAction { get; set; }

    /// <summary>
    /// Gets or sets the affected languages for the executed
    /// <see cref="P:Telerik.Sitefinity.DynamicModules.Events.DynamicContentEventBase.RecycleBinAction" />.
    /// </summary>
    /// <value>The affected languages for the executed <see cref="P:Telerik.Sitefinity.DynamicModules.Events.DynamicContentEventBase.RecycleBinAction" />.</value>
    public string[] AffectedLanguages { get; set; }

    /// <summary>
    /// Gets or sets whether the action performed on the item is the result of a parent related action.
    /// For example the parent was moved to the Recycle bin and for that reason the child is moved to the Recycle bin as well.
    /// </summary>
    /// <value><c>true</c>if the item was affected by a parent related action, otherwise <c>false</c></value>
    public bool WithParent { get; set; }
  }
}
