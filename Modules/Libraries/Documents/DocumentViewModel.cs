// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Documents.DocumentViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.Libraries.Documents
{
  /// <summary>Represents a document view model</summary>
  [DataContract]
  public class DocumentViewModel : MediaContentViewModel
  {
    private string categoryText;
    private string tagsText;
    private bool isManageable;
    private string lilbraryFullUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Documents.DocumentViewModel" /> class.
    /// </summary>
    public DocumentViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Documents.DocumentViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public DocumentViewModel(Telerik.Sitefinity.Libraries.Model.Document contentItem, ContentDataProviderBase provider)
      : base((MediaContent) contentItem, provider)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Documents.DocumentViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="liveItem">The live document related to the master document.</param>
    /// <param name="tempItem">The temp document related to the master document.</param>
    public DocumentViewModel(
      Telerik.Sitefinity.Libraries.Model.Document contentItem,
      ContentDataProviderBase provider,
      Telerik.Sitefinity.Libraries.Model.Document live,
      Telerik.Sitefinity.Libraries.Model.Document temp)
      : base((MediaContent) contentItem, provider, (MediaContent) live, (MediaContent) temp)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Documents.DocumentViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="isEditable">if set to <c>true</c> if the content is editable.</param>
    public DocumentViewModel(
      Telerik.Sitefinity.Libraries.Model.Document contentItem,
      ContentDataProviderBase provider,
      bool isEditable)
      : this(contentItem, provider)
    {
      this.isEditable = isEditable;
    }

    /// <summary>
    /// Helper Property for presenting the list of the selected Categories as string
    /// </summary>
    [DataMember]
    public string CategoryText
    {
      get => this.ContentItem != null ? MediaContentViewModel.GetTaxaText(this.ContentItem, "Category", "categoryFilter", typeof (Telerik.Sitefinity.Libraries.Model.Document)) : this.categoryText;
      set => this.categoryText = value;
    }

    /// <summary>
    /// Helper Property for presenting the list of selected Tags as string
    /// </summary>
    [DataMember]
    public string TagsText
    {
      get => this.ContentItem != null ? MediaContentViewModel.GetTaxaText(this.ContentItem, "Tags", "tagFilter", typeof (Telerik.Sitefinity.Libraries.Model.Document)) : this.tagsText;
      set => this.tagsText = value;
    }

    /// <summary>
    /// Gets or sets the value indicating whether this item is  manageable (editable/deleable)
    /// </summary>
    [DataMember]
    public override bool IsManageable
    {
      get
      {
        if (this.ContentItem == null)
          return this.isManageable;
        return ((ISecuredObject) this.ContentItem).IsGranted("Document", "ManageDocument");
      }
      set => this.isManageable = value;
    }

    /// <summary>Gets or sets the library full URL.</summary>
    [DataMember]
    public string LibraryFullUrl
    {
      get => this.ContentItem != null ? this.ResolveLibraryFullUrl((MediaContent) this.ContentItem, LibrariesModule.LibraryDocumentsPageId) : this.lilbraryFullUrl;
      set => this.lilbraryFullUrl = value;
    }

    /// <summary>
    /// Get live version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Live version of this.ContentItem</returns>
    protected override Content GetLive() => (Content) this.provider.GetLiveBase<Telerik.Sitefinity.Libraries.Model.Document>((Telerik.Sitefinity.Libraries.Model.Document) this.ContentItem);

    /// <summary>
    /// Get temp version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Temp version of this.ContentItem</returns>
    protected override Content GetTemp() => (Content) this.provider.GetTempBase<Telerik.Sitefinity.Libraries.Model.Document>((Telerik.Sitefinity.Libraries.Model.Document) this.ContentItem);
  }
}
