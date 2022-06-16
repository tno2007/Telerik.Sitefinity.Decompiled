// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.DefaultPipe`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Strategies;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>
  /// The default pipe implementation used by publishing system api
  /// </summary>
  /// <typeparam name="T">The tipe of the objects for this pipe</typeparam>
  public class DefaultPipe<T> : 
    IPipe,
    IPullPipe,
    IPushPipe,
    IInboundPipe,
    IOutboundPipe,
    Telerik.Sitefinity.Publishing.Strategies.IDynamicPipe
  {
    private string pipeName = nameof (DefaultPipe<T>);
    private string publishingProviderName;

    public bool SupportsPull => this.strategy is IPullPipeInput<T>;

    /// <summary>Gets or sets the publishing point.</summary>
    /// <value>The publishing point.</value>
    public virtual IPublishingPointBusinessObject PublishingPoint { get; set; }

    /// <inheritdoc />
    public PipeSettings GetDefaultSettings() => PublishingSystemFactory.CreatePipeSettings(new ContentInboundPipe().Name, PublishingManager.GetManager(this.PublishingProviderName));

    /// <summary>Get/set the provider name to use</summary>
    public virtual string PublishingProviderName
    {
      get
      {
        if (string.IsNullOrEmpty(this.publishingProviderName))
          this.publishingProviderName = Config.Get<PublishingConfig>().DefaultProvider;
        return this.publishingProviderName;
      }
      set => this.publishingProviderName = value;
    }

    /// <inheritdoc />
    public IDefinitionField[] Definition { get; set; }

    /// <inheritdoc />
    public void Initialize(PipeSettings pipeSettings)
    {
      this.PipeSettings = pipeSettings;
      this.strategy = Activator.CreateInstance(TypeResolutionService.ResolveType(pipeSettings.AdditionalData["Strategy"]));
      this.PublishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettings.PublishingPoint);
    }

    /// <inheritdoc />
    public bool CanProcessItem(object item) => true;

    /// <inheritdoc />
    public string Name
    {
      get => this.pipeName;
      set => this.pipeName = value;
    }

    /// <inheritdoc />
    public PipeSettings PipeSettings { get; set; }

    /// <inheritdoc />
    public string GetPipeSettingsShortDescription(PipeSettings initSettings) => string.Empty;

    /// <inheritdoc />
    public Type PipeSettingsType => throw new NotImplementedException();

    /// <inheritdoc />
    public IQueryable<WrapperObject> GetData() => this.strategy is IPullPipeInput<T> strategy ? strategy.GetItems().Select<IPublishingEvent, WrapperObject>((Func<IPublishingEvent, WrapperObject>) (i => new WrapperObject((object) i))).AsQueryable<WrapperObject>() : throw new Exception("Pull not supported.");

    /// <inheritdoc />
    public void PushData(IList<PublishingSystemEventInfo> items)
    {
      List<WrapperObject> wrapperObjectList1 = new List<WrapperObject>();
      List<WrapperObject> wrapperObjectList2 = new List<WrapperObject>();
      foreach (IPublishingEvent publishingEvent in (IEnumerable<PublishingSystemEventInfo>) items)
      {
        if (!SystemManager.CurrentContext.AppSettings.Multilingual)
          publishingEvent.Language = "";
        if (!(this.strategy is IPipeInputFilter) || ((IPipeInputFilter) this.strategy).ShouldProcessItem(publishingEvent))
        {
          IPublishingEvent pubObj = publishingEvent;
          if (this.strategy is IPipeOutputMapping)
            pubObj = ((IPipeOutputMapping) this.strategy).FromPublishignObject((object) pubObj);
          string itemAction = pubObj.ItemAction;
          if (!(itemAction == "SystemObjectDeleted"))
          {
            if (!(itemAction == "SystemObjectAdded"))
            {
              if (itemAction == "SystemObjectModified")
              {
                WrapperObject wrapperObject = this.GetConvertedItemsForMapping((object) pubObj).First<WrapperObject>();
                wrapperObject.Language = pubObj.Language;
                wrapperObjectList1.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
                {
                  wrapperObject
                });
                wrapperObjectList2.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
                {
                  wrapperObject
                });
              }
            }
            else
            {
              WrapperObject wrapperObject = this.GetConvertedItemsForMapping((object) pubObj).First<WrapperObject>();
              wrapperObject.Language = pubObj.Language;
              wrapperObjectList1.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
              {
                wrapperObject
              });
            }
          }
          else
          {
            WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, pubObj.Item, pubObj.Language);
            wrapperObjectList2.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
            {
              wrapperObject
            });
          }
        }
      }
      if (this.PipeSettings.IsInbound)
      {
        if (wrapperObjectList2.Count > 0)
          this.PublishingPoint.RemoveItems((IList<WrapperObject>) wrapperObjectList2);
        if (wrapperObjectList1.Count <= 0)
          return;
        this.PublishingPoint.AddItems((IList<WrapperObject>) wrapperObjectList1);
      }
      else
      {
        if (!(this.strategy is IPushPipeOutput))
          return;
        ((IPushPipeOutput) this.strategy).PushItems((object) this, (IEnumerable<IPublishingObject>) wrapperObjectList1, (IEnumerable<IPublishingObject>) wrapperObjectList2);
      }
    }

    /// <summary>Gets the converted items for mapping.</summary>
    /// <param name="items">The items.</param>
    /// <returns></returns>
    public IEnumerable<WrapperObject> GetConvertedItemsForMapping(
      params object[] items)
    {
      IPipeInputMapping<IPublishingEvent> strategy = this.strategy as IPipeInputMapping<IPublishingEvent>;
      object[] objArray = items;
      for (int index = 0; index < objArray.Length; ++index)
      {
        object theInstance = objArray[index];
        WrapperObject wrapperObject;
        if (strategy != null)
          wrapperObject = (WrapperObject) strategy.ToPublishingObject((IPublishingEvent) theInstance);
        else
          wrapperObject = new WrapperObject((object) (PublishingSystemEventInfo) theInstance)
          {
            MappingSettings = this.PipeSettings.Mappings
          };
        yield return wrapperObject;
      }
      objArray = (object[]) null;
    }

    /// <inheritdoc />
    public void ToPublishingPoint()
    {
      IEnumerable<IPublishingEvent> publishingEvents = this.strategy is IPullPipeInput<T> strategy ? strategy.GetItems() : throw new Exception("Pull not supported.");
      List<PublishingSystemEventInfo> publishingSystemEventInfoList = new List<PublishingSystemEventInfo>();
      foreach (IPublishingEvent publishingEvent in publishingEvents)
        this.CreatePublishingSystemEventInfo(publishingSystemEventInfoList, (object) new WrapperObject(publishingEvent.Item), publishingEvent.Language);
      this.PushData((IList<PublishingSystemEventInfo>) publishingSystemEventInfoList);
    }

    private void CreatePublishingSystemEventInfo(
      List<PublishingSystemEventInfo> eventInfo,
      object item,
      string language)
    {
      PublishingSystemEventInfo publishingSystemEventInfo = new PublishingSystemEventInfo()
      {
        Item = item,
        ItemAction = "SystemObjectModified",
        Language = language
      };
      eventInfo.Add(publishingSystemEventInfo);
    }

    private object strategy { get; set; }
  }
}
