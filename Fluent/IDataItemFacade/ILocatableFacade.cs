// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.IDataItemFacade.ILocatableFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Fluent.IDataItemFacade
{
  public interface ILocatableFacade<TParentFacade>
  {
    void SetInitialState(
      AppSettings settings,
      Type itemType,
      ILocatable item,
      IContentManager manager,
      TParentFacade parentFacade);

    IContentManager Manager { get; set; }

    bool SaveChanges();

    bool CancelChanges();

    ILocatableFacade<TParentFacade> SaveAndContinue();

    ILocatableFacade<TParentFacade> CancelAndContinue();

    TParentFacade Done();

    ILocatableFacade<TParentFacade> ClearItemUrls();

    ILocatableFacade<TParentFacade> RemoveItemUrls(Func<UrlData, bool> predicate);

    ILocatable Get();

    ILocatableFacade<TParentFacade> Set(ILocatable itemToSet);
  }
}
