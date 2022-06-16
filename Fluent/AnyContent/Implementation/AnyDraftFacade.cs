// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.Implementation.AnyDraftFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Lifecycle.Fluent;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Fluent.AnyContent.Implementation
{
  public class AnyDraftFacade : IAnyDraftFacade, IMasterLifecycleFacade
  {
    private Telerik.Sitefinity.Fluent.AppSettings settings;
    private Content item;
    private Type itemType;

    public AnyDraftFacade()
    {
    }

    public AnyDraftFacade(Telerik.Sitefinity.Fluent.AppSettings settings, Content item)
      : this(settings, item.GetType(), item)
    {
    }

    public AnyDraftFacade(Telerik.Sitefinity.Fluent.AppSettings settings, Type itemType)
      : this(settings, itemType, (Content) null)
    {
    }

    public AnyDraftFacade(Telerik.Sitefinity.Fluent.AppSettings settings, Type itemType, Content item) => this.SetInitialState(settings, itemType, item, new Guid?());

    public void SetInitialState(Telerik.Sitefinity.Fluent.AppSettings settings, Type itemType, Content item, Guid? itemID)
    {
      FacadeHelper.AssertArgumentNotNull<Telerik.Sitefinity.Fluent.AppSettings>(settings, nameof (settings));
      FacadeHelper.AssertArgumentNotNull<Type>(itemType, nameof (itemType));
      FacadeHelper.Assert<ArgumentException>(item == null || item != null && AllFacadesHelper.SupportsContentLifecycle(item) && item.Status == ContentLifecycleStatus.Master, "Argument item must be a master record it is set to a value != null");
      this.settings = settings;
      this.itemType = itemType;
      if (item != null)
      {
        this.item = item;
      }
      else
      {
        if (!itemID.HasValue)
          return;
        this.item = (Content) this.Manager.GetItem(itemType, itemID.Value);
        FacadeHelper.Assert<ArgumentException>(this.item != null && AllFacadesHelper.SupportsContentLifecycle(this.item) && this.item.Status == ContentLifecycleStatus.Master, "Argument itemID must be primary key of a master record if it is not null");
      }
    }

    public bool ReturnSuccess() => true;

    public bool SaveChanges() => AllFacadesHelper.SaveChanges(this.settings, (IManager) this.Manager);

    public bool CancelChanges() => AllFacadesHelper.CancelChanges(this.settings);

    public IAnyDraftFacade SaveAndContinue()
    {
      AllFacadesHelper.SaveChanges(this.settings, (IManager) this.Manager);
      return (IAnyDraftFacade) this;
    }

    public IAnyDraftFacade CancelAndContinue()
    {
      AllFacadesHelper.CancelChanges(this.settings);
      return (IAnyDraftFacade) this;
    }

    public IAnyDraftPropertyEditorFacade EditProperties()
    {
      IAnyDraftPropertyEditorFacade propertyEditorFacade = ObjectFactory.Resolve<IAnyDraftPropertyEditorFacade>();
      propertyEditorFacade.SetInitialState((IAnyDraftFacade) this, this.item, (IManager) this.Manager);
      return propertyEditorFacade;
    }

    public IAnyContentManager Manager => AllFacadesHelper.GetManager(this.settings, this.itemType);

    public IAnyDraftFacade Get(out Content currentItem)
    {
      currentItem = this.Get();
      return (IAnyDraftFacade) this;
    }

    public Content Get()
    {
      FacadeHelper.Assert((AllFacadesHelper.SupportsContentLifecycle(this.item) ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) this.itemType, (object) this.item.Id));
      FacadeHelper.Assert((this.item.Status == ContentLifecycleStatus.Master ? 1 : 0) != 0, "{0} with id {1} is not a draft.".Arrange((object) this.itemType, (object) this.item.Id));
      return this.item;
    }

    public T GetAs<T>() where T : class => this.Get() as T;

    public IAnyDraftFacade GetAs<T>(out T currentItem) where T : class
    {
      currentItem = this.GetAs<T>();
      return (IAnyDraftFacade) this;
    }

    public IAnyDraftFacade Set(Content itemToSet)
    {
      FacadeHelper.AssertArgumentNotNull<Content>(itemToSet, "item");
      FacadeHelper.Assert(itemToSet.GetType() == this.itemType, "Cannot change facade item type");
      FacadeHelper.Assert((AllFacadesHelper.SupportsContentLifecycle(itemToSet) ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) itemToSet.GetType(), (object) itemToSet.Id));
      FacadeHelper.Assert((itemToSet.Status == ContentLifecycleStatus.Master ? 1 : 0) != 0, "{0} with id {1} is not a draft.".Arrange((object) itemToSet.GetType(), (object) itemToSet.Id));
      this.item = itemToSet;
      return (IAnyDraftFacade) this;
    }

    public IAnyDraftFacade Do(Action<Content> setAction)
    {
      FacadeHelper.Assert(this.item != null, "Not initialized item");
      setAction(this.item);
      CommonMethods.RecompileItemUrls(this.item, (IManager) this.Manager);
      return (IAnyDraftFacade) this;
    }

    public IAnyDraftFacade Delete(CultureInfo language = null)
    {
      FacadeHelper.Assert(this.item != null, "Not initialized item");
      if (language == null)
        this.settings.TrackFullyDeletedItem((IDataItem) this.item);
      else
        this.settings.TrackDeletedItem((IDataItem) this.item);
      this.Manager.DeleteItem((object) this.item, language);
      return (IAnyDraftFacade) this;
    }

    public IAnyDraftFacade CloneFrom(Content source)
    {
      this.Manager.Copy(source, this.Get());
      return (IAnyDraftFacade) this;
    }

    public IOrganizationFacade<IAnyDraftFacade> Organization() => throw new NotImplementedException();

    public IVersionFacade<IAnyDraftFacade> Versioning() => throw new NotImplementedException();

    public IAnyDraftFacade CreateNew() => this.CreateNew(this.Manager.Provider.GetNewGuid());

    public IAnyDraftFacade CreateNew(Guid itemId)
    {
      this.item = (Content) this.Manager.CreateItem(this.itemType, itemId);
      this.settings.TrackModifiedItem((IDataItem) this.item);
      return (IAnyDraftFacade) this;
    }

    public IAnyDraftFacade Unlock()
    {
      Content temp = this.Manager.GetTemp(this.Get());
      if (temp != null && temp.Owner != Guid.Empty)
        temp.Owner = Guid.Empty;
      return (IAnyDraftFacade) this;
    }

    public IAnyPublicFacade GetLive() => this.GetLive((CultureInfo) null);

    public IAnyPublicFacade GetLive(CultureInfo culture) => AllFacadesHelper.GetPublicFacade(this.settings, this.Manager.GetLive(this.Get(), culture), (IAnyDraftFacade) this);

    public IAnyTempFacade GetTemp() => this.GetTemp((CultureInfo) null);

    public IAnyTempFacade GetTemp(CultureInfo culture) => AllFacadesHelper.GetTempFacade(this.settings, this.Manager.GetTemp(this.Get(), culture), (IAnyDraftFacade) this);

    public IAnyPublicFacade GetLiveIfExists() => this.GetLiveIfExists((CultureInfo) null);

    public IAnyPublicFacade GetLiveIfExists(CultureInfo culture)
    {
      Content live = this.Manager.GetLive(this.Get(), culture);
      IAnyPublicFacade liveIfExists = (IAnyPublicFacade) null;
      if (live != null)
        liveIfExists = AllFacadesHelper.GetPublicFacade(this.settings, live, (IAnyDraftFacade) this);
      return liveIfExists;
    }

    public IAnyTempFacade GetTempIfExists() => this.GetTempIfExists((CultureInfo) null);

    public IAnyTempFacade GetTempIfExists(CultureInfo culture)
    {
      Content temp = this.Manager.GetTemp(this.Get(), culture);
      IAnyTempFacade tempIfExists = (IAnyTempFacade) null;
      if (temp != null)
        tempIfExists = AllFacadesHelper.GetTempFacade(this.settings, temp, (IAnyDraftFacade) this);
      return tempIfExists;
    }

    public IAnyTempFacade CheckOut() => this.CheckOut((CultureInfo) null);

    public IAnyTempFacade CheckOut(CultureInfo culture) => AllFacadesHelper.GetTempFacade(this.settings, this.Manager.CheckOut(this.Get(), culture), (IAnyDraftFacade) this);

    public IAnyPublicFacade Publish() => this.Publish((CultureInfo) null);

    public IAnyPublicFacade Publish(bool excludeVersioning) => this.Publish(excludeVersioning, new DateTime?(), (CultureInfo) null);

    public IAnyPublicFacade Publish(CultureInfo culture) => this.Publish(false, new DateTime?(), culture);

    public IAnyPublicFacade Publish(DateTime publicationDate) => this.Publish(false, publicationDate);

    public IAnyPublicFacade Publish(bool excludeVersioning, CultureInfo culture) => this.Publish(excludeVersioning, new DateTime?(), culture);

    public IAnyPublicFacade Publish(DateTime publicationDate, CultureInfo culture) => this.Publish(false, new DateTime?(publicationDate), culture);

    public IAnyPublicFacade Publish(
      bool excludeVersioning,
      DateTime publicationDate)
    {
      return this.Publish(excludeVersioning, new DateTime?(publicationDate), (CultureInfo) null);
    }

    public IAnyPublicFacade Publish(
      bool excludeVersioning,
      DateTime? publicationDate,
      CultureInfo culture)
    {
      Content content = this.Manager.Publish(this.Get(), publicationDate, culture);
      if (!excludeVersioning)
        AllFacadesHelper.CreateVersion(this.settings, content, ContentLifecycleStatus.Live);
      this.settings.TrackPublishedItem((IDataItem) content);
      return AllFacadesHelper.GetPublicFacade(this.settings, content, (IAnyDraftFacade) this);
    }

    public IAnyPublicFacade Schedule(
      DateTime pubDate,
      DateTime? expDate,
      bool excludeVersioning)
    {
      FacadeHelper.Assert<ArgumentException>(pubDate != DateTime.MinValue && pubDate != DateTime.MaxValue, "Publication date should not be DateTime.Max or DateTime.Min");
      Content content = this.Manager.Schedule(this.Get(), pubDate, expDate);
      if (!excludeVersioning)
        AllFacadesHelper.CreateVersion(this.settings, content, ContentLifecycleStatus.Live);
      this.settings.TrackPublishedItem((IDataItem) content);
      return AllFacadesHelper.GetPublicFacade(this.settings, content, (IAnyDraftFacade) this);
    }

    public IAnyPublicFacade Schedule(DateTime pubDate, DateTime? expDate) => this.Schedule(pubDate, expDate, false);

    public IAnyPublicFacade Schedule(bool excludeVersioning) => this.Schedule(this.Get().PublicationDate, this.Get().ExpirationDate, excludeVersioning);

    public IAnyPublicFacade Schedule() => this.Schedule(false);

    public bool IsCheckedOut() => this.IsCheckedOut((CultureInfo) null);

    public bool IsCheckedOut(CultureInfo culture) => this.Manager.IsCheckedOut(this.Get(), culture);

    public IAnyDraftFacade IsCheckedOut(out bool result)
    {
      result = this.IsCheckedOut();
      return (IAnyDraftFacade) this;
    }

    public bool IsPublished() => this.IsPublished((CultureInfo) null);

    public bool IsPublished(CultureInfo culture)
    {
      if (!(this.Manager.GetLive(this.Get()) is ILifecycleDataItem live))
        return false;
      CultureInfo culture1 = culture ?? SystemManager.CurrentContext.Culture;
      return LifecycleExtensions.IsItemPublished<ILifecycleDataItem>(live, culture1);
    }

    public IAnyDraftFacade IsPublished(out bool result)
    {
      result = this.IsPublished((CultureInfo) null);
      return (IAnyDraftFacade) this;
    }

    public bool Exists(Guid Id)
    {
      try
      {
        if (this.Manager.GetItem(this.itemType, Id) != null)
          return true;
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public IAnyDraftFacade Load(Guid Id)
    {
      object itemToSet = this.Manager.GetItem(this.itemType, Id);
      FacadeHelper.AssertArgumentNotNull<object>(itemToSet, nameof (Id));
      return this.Set((Content) itemToSet);
    }

    bool IMasterLifecycleFacade.IsCheckedOut() => this.IsCheckedOut();

    ILiveLifecycleFacade IMasterLifecycleFacade.Publish() => (ILiveLifecycleFacade) this.Publish();
  }
}
