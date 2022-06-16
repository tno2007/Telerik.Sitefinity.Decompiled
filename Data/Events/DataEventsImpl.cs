// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.DataEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.Events
{
  internal class DataEvent : 
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
    /// <see cref="P:Telerik.Sitefinity.Data.Events.DataEvent.RecycleBinAction" />.
    /// </summary>
    /// <value>The affected languages for the executed <see cref="P:Telerik.Sitefinity.Data.Events.DataEvent.RecycleBinAction" />.</value>
    public string[] AffectedLanguages { get; set; }

    /// <summary>
    /// Gets or sets whether the action performed on the item is the result of a parent related action.
    /// For example the parent was moved to the Recycle bin and for that reason the child is moved to the Recycle bin as well.
    /// </summary>
    /// <value><c>true</c>if the item was affected by a parent related action, otherwise <c>false</c></value>
    public bool WithParent { get; set; }

    public string TransactionName { get; set; }

    public void CoppyFrom(DataEvent evt)
    {
      this.Action = evt.Action;
      this.AffectedLanguages = evt.AffectedLanguages;
      this.ApprovalWorkflowState = evt.ApprovalWorkflowState;
      this.changedPropertyNames = evt.ChangedProperties;
      this.ItemId = evt.ItemId;
      this.ItemType = evt.ItemType;
      this.Language = evt.Language;
      this.Origin = evt.Origin;
      this.OriginalContentId = evt.OriginalContentId;
      this.ProviderName = evt.ProviderName;
      this.RecycleBinAction = evt.RecycleBinAction;
      this.Status = evt.Status;
      this.Title = evt.Title;
      this.TransactionName = evt.TransactionName;
      this.Visible = evt.Visible;
      this.WithParent = evt.WithParent;
    }
  }
}
