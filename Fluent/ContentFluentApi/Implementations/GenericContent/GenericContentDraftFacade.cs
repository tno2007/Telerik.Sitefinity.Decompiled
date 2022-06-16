// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.GenericContentDraftFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>Manages the draft state of a generic content item</summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  public class GenericContentDraftFacade<TParentFacade> : 
    BaseDraftFacade<GenericContentDraftFacade<TParentFacade>, TParentFacade, ContentItem, GenericContentTempFacade<GenericContentDraftFacade<TParentFacade>>, GenericContentPublicFacade<GenericContentDraftFacade<TParentFacade>>>,
    ISupportVersioning<GenericContentDraftFacade<TParentFacade>>
    where TParentFacade : BaseFacade
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.GenericContentDraftFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public GenericContentDraftFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.GenericContentDraftFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public GenericContentDraftFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.GenericContentDraftFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public GenericContentDraftFacade(AppSettings settings, TParentFacade parentFacade, Guid itemID)
      : base(settings, parentFacade, itemID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.GenericContentDraftFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public GenericContentDraftFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      ContentItem item)
      : base(settings, parentFacade, item)
    {
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) ContentManager.GetManager(this.settings.ContentProviderName, this.settings.TransactionName);

    /// <summary>Manage this item's versions</summary>
    /// <returns>Versioning facade</returns>
    public virtual VersioningFacade<GenericContentDraftFacade<TParentFacade>> Versioning() => new VersioningFacade<GenericContentDraftFacade<TParentFacade>>(this.settings, this, (IDataItem) this.Get());
  }
}
