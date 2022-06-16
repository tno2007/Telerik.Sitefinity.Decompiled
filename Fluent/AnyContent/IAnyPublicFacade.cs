// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.IAnyPublicFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle.Fluent;

namespace Telerik.Sitefinity.Fluent.AnyContent
{
  [Obsolete("Use Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade instead. To be deleted August 1st 2012")]
  public interface IAnyPublicFacade : ILiveLifecycleFacade
  {
    void SetInitialState(
      AppSettings settings,
      Type itemType,
      Content item,
      IAnyDraftFacade parentFacade);

    IAnyDraftFacade Done();

    bool ReturnSuccess();

    new bool SaveChanges();

    bool CancelChanges();

    IAnyPublicFacade SaveAndContinue();

    IAnyPublicFacade CancelAndContinue();

    IAnyPublicPropertyEditorFacade EditProperties();

    IAnyContentManager Manager { get; }

    IAnyPublicFacade Get(out Content currentItem);

    Content Get();

    T GetAs<T>() where T : class;

    IAnyPublicFacade GetAs<T>(out T currentItem) where T : class;

    IAnyPublicFacade Set(Content item);

    IAnyPublicFacade Do(Action<Content> setAction);

    IAnyPublicFacade Delete();

    IAnyPublicFacade Delete(CultureInfo culture = null);

    IAnyPublicFacade CloneFrom(Content source);

    IOrganizationFacade<IAnyPublicFacade> Organization();

    IVersionFacade<IAnyPublicFacade> Versioning();

    /// <summary>
    /// Copies the public state to the draft state of the content item. Warning: discards any changes in the draft
    /// </summary>
    /// <returns>Parent (draft) facade</returns>
    IAnyDraftFacade CopyToMaster();

    /// <summary>
    /// Copies the public state over the draft and then the temp states of the content item. Any changes in the draft and temp are discarded.
    /// </summary>
    /// <returns>Temp facade</returns>
    IAnyTempFacade CopyToMasterAndContinue();

    /// <summary>Makes the public state of the content item.</summary>
    /// <returns>Parent (draft) facade</returns>
    IAnyDraftFacade Unpublish();

    /// <summary>
    /// Makes the public state of the content item inaccessible for a specific culture.
    /// </summary>
    /// <returns>Parent (draft) facade</returns>
    IAnyDraftFacade Unpublish(CultureInfo culture);
  }
}
