// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.Services.ContentViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using System.Text;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.Services
{
  /// <summary>Represents summary information about a content item.</summary>
  [DataContract]
  public class ContentViewModel : ContentViewModelBase
  {
    private int? countOfPagesUsingThisContent;
    private int? countOfPageTemplatesUsingThisContent;
    private ContentManager manager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.Services.ContentViewModel" /> class.
    /// </summary>
    public ContentViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.Services.ContentViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public ContentViewModel(Telerik.Sitefinity.GenericContent.Model.Content contentItem, ContentDataProviderBase provider)
      : base(contentItem, provider)
    {
      this.Initialize(contentItem);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.Services.ContentViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="isEditable">if set to <c>true</c> if the content is editable.</param>
    public ContentViewModel(Telerik.Sitefinity.GenericContent.Model.Content contentItem, ContentDataProviderBase provider, bool isEditable)
      : this(contentItem, provider)
    {
      this.isEditable = isEditable;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.Services.ContentViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="live">The live content item related to the master content item.</param>
    /// <param name="temp">The temp content item related to the master content item.</param>
    public ContentViewModel(
      ContentItem contentItem,
      ContentDataProviderBase provider,
      ContentItem live,
      ContentItem temp)
      : base((Telerik.Sitefinity.GenericContent.Model.Content) contentItem, provider, (Telerik.Sitefinity.GenericContent.Model.Content) live, (Telerik.Sitefinity.GenericContent.Model.Content) temp)
    {
      this.Initialize((Telerik.Sitefinity.GenericContent.Model.Content) contentItem);
    }

    /// <summary>Gets or sets the content.</summary>
    /// <value>The content.</value>
    [DataMember]
    [Obsolete("To get content, use ContentItemService GetContent method.")]
    public Lstring Content { get; set; }

    /// <summary>
    /// Gets or sets the number of pages that use this content.
    /// </summary>
    [DataMember]
    public int PagesCount
    {
      get
      {
        if (!this.countOfPagesUsingThisContent.HasValue)
          this.countOfPagesUsingThisContent = new int?(this.manager.GetCountOfPagesThatUseContent(this.LiveContentId.Value, this.PageProviderName));
        return this.countOfPagesUsingThisContent.Value;
      }
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets the number of pages that use this content.
    /// </summary>
    [DataMember]
    public int PageTemplatesCount
    {
      get
      {
        if (!this.countOfPageTemplatesUsingThisContent.HasValue)
          this.countOfPageTemplatesUsingThisContent = new int?(this.manager.GetCountOfPageTemplatesThatUseContent(this.LiveContentId.Value));
        return this.countOfPageTemplatesUsingThisContent.Value;
      }
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets the date on which the <see cref="T:Telerik.Sitefinity.GenericContent.Model.ContentItem" /> was modified
    /// </summary>
    [DataMember]
    public DateTime? LastModified
    {
      get => new DateTime?(this.ContentItem.LastModified);
      set
      {
      }
    }

    /// <summary>Gets or sets the pages and templates count UI string.</summary>
    [DataMember]
    public string PagesCountUIString { get; set; }

    /// <summary>Gets or sets the page provider name.</summary>
    [DataMember]
    public string PageProviderName { get; set; }

    /// <summary>Gets or sets the publication status enumeration.</summary>
    [DataMember]
    public override string Status { get; set; }

    /// <summary>
    /// Get live version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Live version of this.ContentItem</returns>
    protected override Telerik.Sitefinity.GenericContent.Model.Content GetLive() => (Telerik.Sitefinity.GenericContent.Model.Content) this.provider.GetLiveBase<ContentItem>((ContentItem) this.ContentItem);

    /// <summary>
    /// Get temp version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Temp version of this.ContentItem</returns>
    protected override Telerik.Sitefinity.GenericContent.Model.Content GetTemp() => (Telerik.Sitefinity.GenericContent.Model.Content) this.provider.GetTempBase<ContentItem>((ContentItem) this.ContentItem);

    private void Initialize(Telerik.Sitefinity.GenericContent.Model.Content contentItem)
    {
      this.manager = ContentManager.GetManager(this.ProviderName);
      this.ContentItem = contentItem;
      this.UIStatus = string.Empty;
      this.Status = string.Empty;
      Telerik.Sitefinity.GenericContent.Model.Content temp = this.GetTemp();
      if (temp != null && temp.Owner != Guid.Empty)
      {
        this.UIStatus = ContentUIStatus.PrivateCopy.ToString();
        this.Status = string.Format(Res.Get<ContentResources>().LockedByFormat, (object) CommonMethods.GetUserName(temp.Owner));
      }
      if (this.UIStatus.IsNullOrEmpty())
        this.UIStatus = ContentUIStatus.NotSupported.ToString();
      this.PagesCountUIString = ContentViewModel.GetStatisticsText(this.PagesCount, this.PageTemplatesCount);
    }

    /// <summary>Gets the statistics text.</summary>
    /// <param name="pagesCount">The pages count.</param>
    /// <param name="templatesCount">The templates count.</param>
    /// <param name="separator">The separator.</param>
    /// <returns>Formatted text with statistics for the content.</returns>
    internal static string GetStatisticsText(int pagesCount, int templatesCount, string separator = ", ")
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      if (pagesCount == 0 && templatesCount == 0)
        return Res.Get<PageResources>().ContentBlockNotUsedViewInstructions;
      if (pagesCount > 0)
      {
        StringBuilder stringBuilder2 = stringBuilder1;
        string str;
        if (pagesCount != 1)
          str = Res.Get<PageResources>().PagesCount.Arrange((object) pagesCount);
        else
          str = Res.Get<PageResources>().PageCount;
        stringBuilder2.Append(str);
      }
      if (templatesCount > 0)
      {
        if (pagesCount > 0)
          stringBuilder1.Append(separator);
        StringBuilder stringBuilder3 = stringBuilder1;
        string str;
        if (templatesCount != 1)
          str = Res.Get<ContentResources>().TemplatesCount.Arrange((object) templatesCount);
        else
          str = Res.Get<ContentResources>().TemplateCount;
        stringBuilder3.Append(str);
      }
      return stringBuilder1.ToString();
    }
  }
}
