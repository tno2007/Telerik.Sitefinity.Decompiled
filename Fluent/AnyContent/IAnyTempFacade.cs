// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.IAnyTempFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle.Fluent;

namespace Telerik.Sitefinity.Fluent.AnyContent
{
  public interface IAnyTempFacade : ITempLifecycleFacade
  {
    void SetInitialState(
      AppSettings settings,
      Type itemType,
      Content item,
      IAnyDraftFacade parentFacade);

    bool ReturnSuccess();

    bool SaveChanges();

    bool CancelChanges();

    IAnyTempFacade SaveAndContinue();

    IAnyTempFacade CancelAndContinue();

    IAnyDraftFacade Done();

    IAnyTempPropertyEditorFacade EditProperties();

    IAnyContentManager Manager { get; }

    IAnyTempFacade Get(out Content currentItem);

    Content Get();

    T GetAs<T>() where T : class;

    IAnyTempFacade GetAs<T>(out T currentItem) where T : class;

    IAnyTempFacade Set(Content item);

    IAnyTempFacade Do(Action<Content> setAction);

    IAnyTempFacade Delete();

    IAnyTempFacade CloneFrom(Content source);

    IOrganizationFacade<IAnyTempFacade> Organization();

    IVersionFacade<IAnyTempFacade> Versioning();

    IAnyDraftFacade CheckIn();

    IAnyDraftFacade CheckIn(bool excludeVersioning);

    IAnyPublicFacade CheckInAndPublish();

    IAnyPublicFacade CheckInAndPublish(bool excludeVersioning);

    IAnyDraftFacade CopyToMaster();

    IAnyDraftFacade CopyToMaster(bool excludeVersioning);
  }
}
