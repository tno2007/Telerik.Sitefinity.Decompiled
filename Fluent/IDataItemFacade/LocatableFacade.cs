// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.IDataItemFacade.LocatableFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Fluent.IDataItemFacade
{
  public class LocatableFacade<TParentFacade> : ILocatableFacade<TParentFacade> where TParentFacade : class
  {
    private AppSettings settings;
    private ILocatable item;
    private Type itemType;
    private TParentFacade parentFacade;

    public LocatableFacade(
      AppSettings settings,
      Type itemType,
      ILocatable item,
      IContentManager manager,
      TParentFacade parentFacade)
    {
      this.SetInitialState(settings, itemType, item, manager, parentFacade);
    }

    public void SetInitialState(
      AppSettings settings,
      Type itemType,
      ILocatable item,
      IContentManager manager,
      TParentFacade parentFacade)
    {
      FacadeHelper.AssertArgumentNotNull<AppSettings>(settings, nameof (settings));
      FacadeHelper.AssertArgumentNotNull<Type>(itemType, nameof (itemType));
      FacadeHelper.AssertArgumentNotNull<ILocatable>(item, nameof (item));
      FacadeHelper.AssertArgumentNotNull<IContentManager>(manager, nameof (manager));
      this.settings = settings;
      this.itemType = itemType;
      this.item = item;
      this.Manager = manager;
      this.parentFacade = parentFacade;
    }

    public IContentManager Manager { get; set; }

    public bool SaveChanges() => AllFacadesHelper.SaveChanges(this.settings, (IManager) this.Manager);

    public bool CancelChanges() => AllFacadesHelper.CancelChanges(this.settings);

    public ILocatableFacade<TParentFacade> SaveAndContinue()
    {
      AllFacadesHelper.SaveChanges(this.settings, (IManager) this.Manager);
      return (ILocatableFacade<TParentFacade>) this;
    }

    public ILocatableFacade<TParentFacade> CancelAndContinue()
    {
      AllFacadesHelper.CancelChanges(this.settings);
      return (ILocatableFacade<TParentFacade>) this;
    }

    public TParentFacade Done() => this.parentFacade;

    public ILocatableFacade<TParentFacade> ClearItemUrls()
    {
      FacadeHelper.Assert(this.item != null, "Not initialized item");
      FacadeHelper.Assert(this.Manager != null, "Not initialized manager");
      this.Manager.ClearItemUrls<ILocatable>(this.item);
      return (ILocatableFacade<TParentFacade>) this;
    }

    public ILocatableFacade<TParentFacade> RemoveItemUrls(
      Func<UrlData, bool> predicate)
    {
      FacadeHelper.Assert(this.item != null, "Not initialized item");
      FacadeHelper.Assert(this.Manager != null, "Not initialized manager");
      this.Manager.RemoveItemUrls<ILocatable>(this.item, predicate);
      return (ILocatableFacade<TParentFacade>) this;
    }

    public ILocatable Get() => this.item;

    public ILocatableFacade<TParentFacade> Set(ILocatable itemToSet)
    {
      FacadeHelper.AssertArgumentNotNull<ILocatable>(itemToSet, "item");
      FacadeHelper.Assert(itemToSet.GetType() == this.itemType, "Cannot change facade item type");
      this.item = itemToSet;
      return (ILocatableFacade<TParentFacade>) this;
    }
  }
}
