// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingSystemEventInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data.Events;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>Pipe item info object</summary>
  public class PublishingSystemEventInfo : IPublishingEvent
  {
    private object item;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PublishingSystemEventInfo" /> class.
    /// </summary>
    public PublishingSystemEventInfo()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PublishingSystemEventInfo" /> class.
    /// </summary>
    /// <param name="eventInfo">The event information.</param>
    internal PublishingSystemEventInfo(PublishingSystemEventInfo eventInfo)
    {
      this.ItemAction = eventInfo.ItemAction;
      this.Language = eventInfo.Language;
      if (eventInfo.Item is WrapperObjectWithDataItemLoader)
      {
        WrapperObjectWithDataItemLoader withDataItemLoader1 = eventInfo.Item as WrapperObjectWithDataItemLoader;
        WrapperObjectWithDataItemLoader withDataItemLoader2 = new WrapperObjectWithDataItemLoader();
        withDataItemLoader2.parent = this;
        withDataItemLoader2.ItemId = withDataItemLoader1.ItemId;
        withDataItemLoader2.ProviderName = withDataItemLoader1.ProviderName;
        withDataItemLoader2.Language = withDataItemLoader1.Language;
        withDataItemLoader2.TransactionName = (string) null;
        this.Item = (object) withDataItemLoader2;
      }
      else
        this.Item = eventInfo.Item;
      this.ItemType = eventInfo.ItemType;
    }

    /// <summary>Gets or sets the item action.</summary>
    /// <value>The item action.</value>
    public virtual string ItemAction { get; set; }

    /// <summary>Gets or sets the item.</summary>
    /// <value>The item.</value>
    public virtual object Item
    {
      get => this.item;
      set
      {
        this.item = value;
        if (this.item == null)
          return;
        this.ItemType = this.item.GetType().FullName;
      }
    }

    /// <summary>Gets or sets the language.</summary>
    /// <value>The language.</value>
    public virtual string Language { get; set; }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    public virtual string ItemType { get; set; }

    /// <summary>
    /// Creates <see cref="T:Telerik.Sitefinity.Publishing.PublishingSystemEventInfo" /> from <see cref="T:Telerik.Sitefinity.Data.Events.IDataEvent" /> event
    /// </summary>
    /// <param name="dataEvent">The data event</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Publishing.PublishingSystemEventInfo" /> object</returns>
    public static PublishingSystemEventInfo FromDataEvent(
      IDataEvent dataEvent)
    {
      return dataEvent.ToPubSysEventInfo();
    }
  }
}
