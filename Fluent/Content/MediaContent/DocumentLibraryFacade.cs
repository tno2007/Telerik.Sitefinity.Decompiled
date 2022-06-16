// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Content.DocumentLibraryFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Fluent.Content
{
  [Obsolete]
  public class DocumentLibraryFacade : 
    ContentFacade<DocumentLibraryFacade, DocumentLibrary, DocumentLibraryFacade>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.DocumentLibraryFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public DocumentLibraryFacade(AppSettings appSettings)
      : base(appSettings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.DocumentLibraryFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="itemId">The content id.</param>
    public DocumentLibraryFacade(AppSettings appSettings, Guid itemId)
      : base(appSettings, itemId)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.DocumentLibraryFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public DocumentLibraryFacade(AppSettings appSettings, DocumentLibraryFacade parentFacade)
      : base(appSettings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.DocumentLibraryFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="itemId">The content id.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public DocumentLibraryFacade(
      AppSettings appSettings,
      Guid itemId,
      DocumentLibraryFacade parentFacade)
      : base(appSettings, itemId, parentFacade)
    {
    }

    /// <summary>
    /// Creates a facade of the documents in the library with a parent facade the current facade.
    /// </summary>
    /// <returns>
    /// The child facade of type <see cref="T:Telerik.Sitefinity.Fluent.Content.DocumentsFacade" />.
    /// </returns>
    public DocumentsFacade Documents()
    {
      this.EnsureExistence(true);
      return new DocumentsFacade(this.AppSettings, this.ContentItem.Documents(), this);
    }

    /// <summary>
    /// Creates a facade of a document in the library with a parent facade the current facade.
    /// </summary>
    /// <returns>
    /// The child facade of type <see cref="T:Telerik.Sitefinity.Fluent.Content.DocumentFacade" />.
    /// </returns>
    public DocumentFacade Document() => new DocumentFacade(this.AppSettings, this);

    /// <summary>
    /// Creates a facade of the given document in the library with a parent facade the current facade.
    /// </summary>
    /// <param name="itemId">The id of the document.</param>
    /// <returns>
    /// The child facade of type <see cref="T:Telerik.Sitefinity.Fluent.Content.DocumentFacade" />.
    /// </returns>
    public DocumentFacade Document(Guid itemId) => new DocumentFacade(this.AppSettings, itemId, this);
  }
}
