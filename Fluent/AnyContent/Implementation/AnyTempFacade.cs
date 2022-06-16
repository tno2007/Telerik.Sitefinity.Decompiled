// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.Implementation.AnyTempFacade
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
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Fluent.AnyContent.Implementation
{
  public class AnyTempFacade : IAnyTempFacade, ITempLifecycleFacade
  {
    private Telerik.Sitefinity.Fluent.AppSettings settings;
    private Content item;
    private Type itemType;
    private IAnyDraftFacade parentFacade;

    public AnyTempFacade()
    {
    }

    public AnyTempFacade(Telerik.Sitefinity.Fluent.AppSettings settings, Content item, IAnyDraftFacade parentFacade)
      : this(settings, item.GetType(), item, parentFacade)
    {
    }

    public AnyTempFacade(Telerik.Sitefinity.Fluent.AppSettings settings, Type itemType, IAnyDraftFacade parentFacade)
      : this(settings, itemType, (Content) null, parentFacade)
    {
    }

    public AnyTempFacade(
      Telerik.Sitefinity.Fluent.AppSettings settings,
      Type itemType,
      Content item,
      IAnyDraftFacade parentFacade)
    {
      this.SetInitialState(settings, itemType, item, parentFacade);
    }

    public void SetInitialState(
      Telerik.Sitefinity.Fluent.AppSettings settings,
      Type itemType,
      Content item,
      IAnyDraftFacade parentFacade)
    {
      FacadeHelper.AssertArgumentNotNull<Telerik.Sitefinity.Fluent.AppSettings>(settings, nameof (settings));
      FacadeHelper.AssertArgumentNotNull<Type>(itemType, nameof (itemType));
      FacadeHelper.AssertArgumentNotNull<IAnyDraftFacade>(parentFacade, nameof (parentFacade));
      FacadeHelper.Assert<ArgumentException>(item == null || item != null && item.SupportsContentLifecycle && (item.Status == ContentLifecycleStatus.Temp || item.Status == ContentLifecycleStatus.PartialTemp), "Argument item must be a temp record it is set to a value != null");
      this.settings = settings;
      this.itemType = itemType;
      this.item = item;
      this.parentFacade = parentFacade;
    }

    public IAnyDraftFacade Done()
    {
      FacadeHelper.AssertNotNull<IAnyDraftFacade>(this.parentFacade, "Parrent facade can not be null when you call Done()");
      return this.parentFacade;
    }

    public bool ReturnSuccess() => true;

    public bool SaveChanges() => AllFacadesHelper.SaveChanges(this.settings, (IManager) this.Manager);

    public bool CancelChanges() => AllFacadesHelper.CancelChanges(this.settings);

    public IAnyTempFacade SaveAndContinue()
    {
      AllFacadesHelper.SaveChanges(this.settings, (IManager) this.Manager);
      return (IAnyTempFacade) this;
    }

    public IAnyTempFacade CancelAndContinue()
    {
      AllFacadesHelper.SaveChanges(this.settings, (IManager) this.Manager);
      return (IAnyTempFacade) this;
    }

    public IAnyTempPropertyEditorFacade EditProperties()
    {
      IAnyTempPropertyEditorFacade propertyEditorFacade = ObjectFactory.Resolve<IAnyTempPropertyEditorFacade>();
      propertyEditorFacade.SetInitialState((IAnyTempFacade) this, this.item, (IManager) this.Manager);
      return propertyEditorFacade;
    }

    public IAnyContentManager Manager => AllFacadesHelper.GetManager(this.settings, this.itemType);

    public IAnyTempFacade Get(out Content currentItem)
    {
      currentItem = this.Get();
      return (IAnyTempFacade) this;
    }

    public Content Get()
    {
      FacadeHelper.Assert((this.item.SupportsContentLifecycle ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) this.itemType, (object) this.item.Id));
      FacadeHelper.Assert((this.item.Status == ContentLifecycleStatus.Temp ? 1 : (this.item.Status == ContentLifecycleStatus.PartialTemp ? 1 : 0)) != 0, "{0} with id {1} is not temp.".Arrange((object) this.itemType, (object) this.item.Id));
      return this.item;
    }

    public T GetAs<T>() where T : class => this.Get() as T;

    public IAnyTempFacade GetAs<T>(out T currentItem) where T : class
    {
      currentItem = this.GetAs<T>();
      return (IAnyTempFacade) this;
    }

    public IAnyTempFacade Set(Content itemToSet)
    {
      FacadeHelper.AssertArgumentNotNull<Content>(itemToSet, "item");
      FacadeHelper.Assert(itemToSet.GetType() == this.itemType, "Cannot change facade item type");
      FacadeHelper.Assert((itemToSet.SupportsContentLifecycle ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) itemToSet.GetType(), (object) itemToSet.Id));
      FacadeHelper.Assert((itemToSet.Status == ContentLifecycleStatus.Temp ? 1 : (this.item.Status == ContentLifecycleStatus.PartialTemp ? 1 : 0)) != 0, "{0} with id {1} is not temp.".Arrange((object) itemToSet.GetType(), (object) itemToSet.Id));
      this.item = itemToSet;
      return (IAnyTempFacade) this;
    }

    public IAnyTempFacade Do(Action<Content> setAction)
    {
      FacadeHelper.Assert(this.item != null, "Not initialized item");
      setAction(this.item);
      CommonMethods.RecompileItemUrls(this.item, (IManager) this.Manager);
      return (IAnyTempFacade) this;
    }

    public IAnyTempFacade Delete()
    {
      FacadeHelper.Assert(this.item != null, "Not initialized item");
      this.Manager.DeleteItem((object) this.item);
      return (IAnyTempFacade) this;
    }

    public IAnyTempFacade CloneFrom(Content source)
    {
      this.Manager.Copy(source, this.Get());
      return (IAnyTempFacade) this;
    }

    public IOrganizationFacade<IAnyTempFacade> Organization() => throw new NotImplementedException();

    public IVersionFacade<IAnyTempFacade> Versioning() => throw new NotImplementedException();

    public IAnyDraftFacade CheckIn() => this.CheckIn(false);

    public IAnyDraftFacade CheckIn(bool excludeVersioning)
    {
      Content content1 = this.Get();
      Content content2;
      if (this.IsLifecycle())
      {
        CultureInfo tempCulture = this.GetTempCulture();
        content2 = this.Manager.CheckIn(content1, tempCulture);
      }
      else
        content2 = this.Manager.CheckIn(content1);
      ++content2.Version;
      this.Done().Set(content2);
      if (!excludeVersioning)
        AllFacadesHelper.CreateVersion(this.settings, content2, ContentLifecycleStatus.Master);
      this.settings.TrackModifiedItem((IDataItem) content2);
      return this.Done();
    }

    public IAnyPublicFacade CheckInAndPublish()
    {
      if (!this.IsLifecycle())
        return this.CheckIn().Publish(false);
      CultureInfo tempCulture = this.GetTempCulture();
      return this.CheckIn().Publish(false, tempCulture);
    }

    public IAnyPublicFacade CheckInAndPublish(bool excludeVersioning)
    {
      if (!this.IsLifecycle())
        return this.CheckIn(excludeVersioning).Publish(excludeVersioning);
      CultureInfo tempCulture = this.GetTempCulture();
      return this.CheckIn(excludeVersioning).Publish(excludeVersioning, tempCulture);
    }

    public IAnyDraftFacade CopyToMaster() => this.CopyToMaster(false);

    public IAnyDraftFacade CopyToMaster(bool excludeVersioning)
    {
      Content destination = this.Done().Get();
      Content source = this.Get();
      if (this.IsLifecycle())
      {
        CultureInfo tempCulture = this.GetTempCulture();
        destination = this.Manager.CheckIn(source, tempCulture, false);
      }
      else
      {
        this.Manager.Copy(source, destination);
        ++destination.Version;
      }
      if (!excludeVersioning)
        AllFacadesHelper.CreateVersion(this.settings, destination, ContentLifecycleStatus.Master);
      return this.Done();
    }

    private bool IsLifecycle() => this.item is ILifecycleDataItemGeneric && this.Manager is AnyContentManager;

    private CultureInfo GetTempCulture()
    {
      CultureInfo defaultCulture = SystemManager.CurrentContext.CurrentSite.DefaultCulture;
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      ILifecycleDataItemGeneric lifecycleDataItemGeneric = (ILifecycleDataItemGeneric) this.item;
      CultureInfo objB = culture;
      LanguageData languageData = !object.Equals((object) defaultCulture, (object) objB) ? lifecycleDataItemGeneric.GetLanguageData(culture.GetSitefinityCulture()) : lifecycleDataItemGeneric.GetLanguageDataRaw(culture) ?? lifecycleDataItemGeneric.GetLanguageDataRaw((CultureInfo) null);
      return languageData != null && languageData.Language != null ? CultureInfo.GetCultureInfo(languageData.Language) : (CultureInfo) null;
    }

    IMasterLifecycleFacade ITempLifecycleFacade.CopyToMaster(
      bool excludeVersioning)
    {
      return (IMasterLifecycleFacade) this.CopyToMaster(excludeVersioning);
    }

    IMasterLifecycleFacade ITempLifecycleFacade.CheckIn(
      bool excludeVersioning)
    {
      return (IMasterLifecycleFacade) this.CheckIn(excludeVersioning);
    }
  }
}
