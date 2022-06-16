// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.IAnyDraftFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle.Fluent;

namespace Telerik.Sitefinity.Fluent.AnyContent
{
  public interface IAnyDraftFacade : IMasterLifecycleFacade
  {
    void SetInitialState(AppSettings settings, Type itemType, Content item, Guid? itemId);

    bool ReturnSuccess();

    bool SaveChanges();

    bool CancelChanges();

    IAnyDraftFacade SaveAndContinue();

    IAnyDraftFacade CancelAndContinue();

    IAnyDraftPropertyEditorFacade EditProperties();

    IAnyContentManager Manager { get; }

    IAnyDraftFacade Get(out Content currentItem);

    Content Get();

    T GetAs<T>() where T : class;

    IAnyDraftFacade GetAs<T>(out T currentItem) where T : class;

    IAnyDraftFacade Set(Content item);

    IAnyDraftFacade Do(Action<Content> setAction);

    IAnyDraftFacade Delete(CultureInfo language = null);

    IAnyDraftFacade CloneFrom(Content source);

    IOrganizationFacade<IAnyDraftFacade> Organization();

    IVersionFacade<IAnyDraftFacade> Versioning();

    IAnyDraftFacade CreateNew();

    IAnyDraftFacade CreateNew(Guid itemId);

    IAnyDraftFacade Unlock();

    IAnyPublicFacade GetLive();

    IAnyPublicFacade GetLive(CultureInfo culture);

    IAnyPublicFacade GetLiveIfExists();

    IAnyPublicFacade GetLiveIfExists(CultureInfo culture);

    IAnyTempFacade GetTemp();

    IAnyTempFacade GetTemp(CultureInfo culture);

    IAnyTempFacade GetTempIfExists();

    IAnyTempFacade GetTempIfExists(CultureInfo culture);

    IAnyTempFacade CheckOut();

    IAnyTempFacade CheckOut(CultureInfo culture);

    IAnyPublicFacade Publish();

    IAnyPublicFacade Publish(CultureInfo culture);

    IAnyPublicFacade Publish(bool excludeVersioning);

    IAnyPublicFacade Publish(bool excludeVersioning, CultureInfo culture);

    IAnyPublicFacade Publish(DateTime publicationDate);

    IAnyPublicFacade Publish(DateTime publicationDate, CultureInfo culture);

    IAnyPublicFacade Publish(bool excludeVersioning, DateTime publicationDate);

    IAnyPublicFacade Publish(
      bool excludeVersioning,
      DateTime? publicationDate,
      CultureInfo culture);

    IAnyPublicFacade Schedule(
      DateTime pubDate,
      DateTime? expDate,
      bool excludeVersioning);

    IAnyPublicFacade Schedule(DateTime pubDate, DateTime? expDate);

    IAnyPublicFacade Schedule(bool excludeVersioning);

    IAnyPublicFacade Schedule();

    new bool IsCheckedOut();

    bool IsCheckedOut(CultureInfo culture);

    IAnyDraftFacade IsCheckedOut(out bool result);

    bool IsPublished();

    bool IsPublished(CultureInfo culture);

    IAnyDraftFacade IsPublished(out bool result);

    bool Exists(Guid Id);

    IAnyDraftFacade Load(Guid Id);
  }
}
