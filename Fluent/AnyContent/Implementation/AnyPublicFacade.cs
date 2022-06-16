// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.Implementation.AnyPublicFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle.Fluent;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Fluent.AnyContent.Implementation
{
  public class AnyPublicFacade : IAnyPublicFacade, ILiveLifecycleFacade
  {
    private Telerik.Sitefinity.Fluent.AppSettings settings;
    private Content item;
    private Type itemType;
    private IAnyDraftFacade parentFacade;

    public AnyPublicFacade()
    {
    }

    public AnyPublicFacade(Telerik.Sitefinity.Fluent.AppSettings settings, Content item, IAnyDraftFacade parentFacade)
      : this(settings, item.GetType(), item, parentFacade)
    {
    }

    public AnyPublicFacade(Telerik.Sitefinity.Fluent.AppSettings settings, Type itemType, IAnyDraftFacade parentFacade)
      : this(settings, itemType, (Content) null, parentFacade)
    {
    }

    public AnyPublicFacade(
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
      FacadeHelper.Assert<ArgumentException>(item == null || item != null && item.SupportsContentLifecycle && item.Status == ContentLifecycleStatus.Live, "Argument item must be a live record it is set to a value != null");
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

    public IAnyPublicFacade SaveAndContinue()
    {
      AllFacadesHelper.SaveChanges(this.settings, (IManager) this.Manager);
      return (IAnyPublicFacade) this;
    }

    public IAnyPublicFacade CancelAndContinue()
    {
      AllFacadesHelper.CancelChanges(this.settings);
      return (IAnyPublicFacade) this;
    }

    public IAnyPublicPropertyEditorFacade EditProperties()
    {
      IAnyPublicPropertyEditorFacade propertyEditorFacade = ObjectFactory.Resolve<IAnyPublicPropertyEditorFacade>();
      propertyEditorFacade.SetInitialState((IAnyPublicFacade) this, this.item, (IManager) this.Manager);
      return propertyEditorFacade;
    }

    public IAnyContentManager Manager => AllFacadesHelper.GetManager(this.settings, this.itemType);

    public IAnyPublicFacade Get(out Content currentItem)
    {
      currentItem = this.Get();
      return (IAnyPublicFacade) this;
    }

    public Content Get()
    {
      FacadeHelper.Assert((this.item.SupportsContentLifecycle ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) this.itemType, (object) this.item.Id));
      FacadeHelper.Assert((this.item.Status == ContentLifecycleStatus.Live ? 1 : 0) != 0, "{0} with id {1} is not live.".Arrange((object) this.itemType, (object) this.item.Id));
      return this.item;
    }

    public T GetAs<T>() where T : class => this.Get() as T;

    public IAnyPublicFacade GetAs<T>(out T currentItem) where T : class
    {
      currentItem = this.GetAs<T>();
      return (IAnyPublicFacade) this;
    }

    public IAnyPublicFacade Set(Content itemToSet)
    {
      FacadeHelper.AssertArgumentNotNull<Content>(itemToSet, "item");
      FacadeHelper.Assert(itemToSet.GetType() == this.itemType, "Cannot change facade item type");
      FacadeHelper.Assert((itemToSet.SupportsContentLifecycle ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) itemToSet.GetType(), (object) itemToSet.Id));
      FacadeHelper.Assert((itemToSet.Status == ContentLifecycleStatus.Live ? 1 : 0) != 0, "{0} with id {1} is not live.".Arrange((object) itemToSet.GetType(), (object) itemToSet.Id));
      this.item = itemToSet;
      return (IAnyPublicFacade) this;
    }

    public IAnyPublicFacade Do(Action<Content> setAction)
    {
      FacadeHelper.Assert(this.item != null, "Not initialized item");
      setAction(this.item);
      CommonMethods.RecompileItemUrls(this.item, (IManager) this.Manager);
      return (IAnyPublicFacade) this;
    }

    public IAnyPublicFacade Delete() => this.Delete((CultureInfo) null);

    public IAnyPublicFacade Delete(CultureInfo culture = null)
    {
      FacadeHelper.Assert(this.item != null, "Not initialized item");
      this.settings.TrackDeletedItem((IDataItem) this.item);
      this.Manager.DeleteItem((object) this.item, culture);
      return (IAnyPublicFacade) this;
    }

    public IAnyPublicFacade CloneFrom(Content source)
    {
      this.Manager.Copy(source, this.Get());
      CommonMethods.RecompileItemUrls(this.item, (IManager) this.Manager);
      return (IAnyPublicFacade) this;
    }

    public IOrganizationFacade<IAnyPublicFacade> Organization() => throw new NotImplementedException();

    public IVersionFacade<IAnyPublicFacade> Versioning() => throw new NotImplementedException();

    /// <summary>
    /// Copies the public state to the draft state of the content item. Warning: discards any changes in the draft
    /// </summary>
    /// <returns>Parent (draft) facade</returns>
    public IAnyDraftFacade CopyToMaster()
    {
      Content content = this.Manager.Edit(this.Get());
      this.Done().Set(content);
      return this.Done();
    }

    /// <summary>
    /// Copies the public state over the draft and then the temp states of the content item. Any changes in the draft and temp are discarded.
    /// </summary>
    /// <returns>Temp facade</returns>
    public IAnyTempFacade CopyToMasterAndContinue()
    {
      Content content = this.Manager.Edit(this.Get());
      this.Done().Set(content);
      return this.Done().CheckOut();
    }

    /// <summary>
    /// Makes the public state of the content item inaccessible.
    /// </summary>
    /// <returns>Parent (draft) facade</returns>
    public IAnyDraftFacade Unpublish() => this.Unpublish((CultureInfo) null);

    /// <summary>
    /// Makes the public state of the content item inaccessible for a specific culture.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>Parent (draft) facade</returns>
    public IAnyDraftFacade Unpublish(CultureInfo culture = null)
    {
      Content content = this.Manager.Unpublish(this.Get());
      if (content != null)
        this.Done().Set(content);
      this.settings.TrackUnpublishedItem((IDataItem) content);
      return this.Done();
    }
  }
}
