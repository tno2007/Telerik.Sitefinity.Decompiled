// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingPointBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  public abstract class PublishingPointBase : IPublishingPointBusinessObject
  {
    private PublishingPoint model;
    private string publishingProviderName;
    private List<IInboundPipe> inboundPipes;
    private List<IOutboundPipe> outboundPipes;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PublishingPointBase" /> class.
    /// </summary>
    public PublishingPointBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PublishingPointBase" /> class.
    /// </summary>
    /// <param name="model">The model.</param>
    public PublishingPointBase(PublishingPoint model) => this.model = model != null ? model : throw new ArgumentNullException(nameof (model));

    /// <summary>Gets the model.</summary>
    /// <value>The model.</value>
    protected virtual PublishingPoint Model => this.model;

    /// <summary>Get/set the provider name to use</summary>
    public string PublishingProviderName
    {
      get
      {
        if (string.IsNullOrEmpty(this.publishingProviderName))
          this.publishingProviderName = Config.Get<PublishingConfig>().DefaultProvider;
        return this.publishingProviderName;
      }
      set => this.publishingProviderName = value;
    }

    /// <summary>Gets the publishing point items.</summary>
    /// <returns></returns>
    public abstract IQueryable<WrapperObject> GetPublishingPointItems();

    /// <summary>Initializes the specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public void Initialize(PublishingPoint settings) => this.model = settings;

    /// <summary>Gets the inbound pipes.</summary>
    /// <value>The inbound pipes.</value>
    public virtual List<IInboundPipe> InboundPipes
    {
      get
      {
        if (this.inboundPipes == null)
          this.inboundPipes = this.GetPipes<IInboundPipe>();
        return this.inboundPipes;
      }
    }

    /// <summary>Gets the outbound pipes.</summary>
    /// <value>The outbound pipes.</value>
    public virtual List<IOutboundPipe> OutboundPipes
    {
      get
      {
        if (this.outboundPipes == null)
          this.outboundPipes = this.GetPipes<IOutboundPipe>();
        return this.outboundPipes;
      }
    }

    public virtual void CallInboundPipes(IList<PublishingSystemEventInfo> items) => this.CallPipes<IInboundPipe>(this.InboundPipes, items);

    /// <summary>Calls the outbound pipes.</summary>
    /// <param name="items">The items.</param>
    public virtual void CallOutboundPipes(IList<PublishingSystemEventInfo> items) => this.CallPipes<IOutboundPipe>(this.OutboundPipes, items);

    /// <summary>Adds the items.</summary>
    /// <param name="items">The items.</param>
    public virtual void AddItems(IList<WrapperObject> items) => this.CallPushPipes(items, "SystemObjectAdded");

    /// <summary>Removes the items.</summary>
    /// <param name="items">The items.</param>
    public virtual void RemoveItems(IList<WrapperObject> items) => this.CallPushPipes(items, "SystemObjectDeleted");

    /// <summary>Puts the items.</summary>
    /// <param name="items">The items.</param>
    public virtual void PutItems(IList<PublishingSystemEventInfo> items) => this.CallInboundPipes(items);

    /// <summary>Gets the publishing manager.</summary>
    /// <returns></returns>
    protected virtual PublishingManager GetPublishingManager() => PublishingManager.GetManager(this.PublishingProviderName);

    /// <summary>Gets the publishing manager.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns></returns>
    protected virtual PublishingManager GetPublishingManager(
      string providerName,
      string transactionName)
    {
      return PublishingManager.GetManager(providerName, transactionName);
    }

    /// <summary>Gets the publishing point data item manager.</summary>
    /// <returns></returns>
    protected virtual PublishingPointDynamicTypeManager GetPublishingPointDataItemManager() => this.GetPublishingManager().GetDynamicTypeManager((IPublishingPoint) this.Model);

    /// <summary>Gets the publishing manager.</summary>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns></returns>
    protected virtual PublishingManager GetPublishingManager(string transactionName) => PublishingManager.GetManager(string.Empty, transactionName);

    /// <summary>Gets the pipes.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected virtual List<T> GetPipes<T>() where T : class
    {
      List<T> pipes = new List<T>();
      bool isInbound = typeof (T) == typeof (IInboundPipe);
      foreach (PipeSettings pipe1 in this.Model.PipeSettings.Where<PipeSettings>((Func<PipeSettings, bool>) (s => s.IsInbound == isInbound)))
      {
        if (PublishingSystemFactory.IsPipeRegistered(pipe1.PipeName) && PublishingSystemFactory.GetPipe(pipe1) is T pipe2)
          pipes.Add(pipe2);
      }
      return pipes;
    }

    /// <summary>Calls the pipes.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pipes">The pipes.</param>
    /// <param name="items">The items.</param>
    protected virtual void CallPipes<T>(List<T> pipes, IList<PublishingSystemEventInfo> items)
    {
      foreach (T pipe1 in pipes)
      {
        IPipe pipe2 = (IPipe) (object) pipe1;
        if (pipe2 is IPushPipe)
          (pipe2 as IPushPipe).PushData(items);
      }
    }

    /// <summary>Calls the push pipes.</summary>
    /// <param name="items">The items.</param>
    /// <param name="action">The action.</param>
    protected virtual void CallPushPipes(IList<WrapperObject> items, string action)
    {
      foreach (IOutboundPipe outboundPipe in this.OutboundPipes)
      {
        if (outboundPipe is IPushPipe)
          (outboundPipe as IPushPipe).PushData((IList<PublishingSystemEventInfo>) items.Select<WrapperObject, PublishingSystemEventInfo>((Func<WrapperObject, PublishingSystemEventInfo>) (i => new PublishingSystemEventInfo()
          {
            ItemAction = action,
            Item = (object) i,
            Language = i.Language
          })).ToList<PublishingSystemEventInfo>());
      }
    }
  }
}
