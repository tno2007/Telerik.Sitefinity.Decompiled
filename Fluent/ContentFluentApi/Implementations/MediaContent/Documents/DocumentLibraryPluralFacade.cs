// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentLibraryPluralFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>Manages a collection of document libraries</summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  public class DocumentLibraryPluralFacade<TParentFacade> : 
    BaseContentPluralFacadeWithoutLifeCycle<DocumentLibraryPluralFacade<TParentFacade>, DocumentLibrarySingularFacade<TParentFacade>, TParentFacade, DocumentLibrary>
    where TParentFacade : BaseFacade
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentLibraryPluralFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="!:ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="!:ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public DocumentLibraryPluralFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentLibraryPluralFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="!:ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="!:ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public DocumentLibraryPluralFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) LibrariesManager.GetManager(this.settings.ContentProviderName, this.settings.TransactionName);
  }
}
