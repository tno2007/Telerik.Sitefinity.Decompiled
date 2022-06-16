// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Content.Web.Services.ContentService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services;

namespace Telerik.Sitefinity.Services.Content.Web.Services
{
  /// <summary>
  /// The WCF web service that is used to work with all types that inherit from base <see cref="N:Telerik.Sitefinity.Services.Content" />
  /// class.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class ContentService : ContentServiceBase<ContentItem, ContentViewModel, ContentManager>
  {
    /// <summary>Gets the content items.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override IQueryable<ContentItem> GetContentItems(string providerName) => this.GetManager(providerName).GetContent();

    /// <summary>Gets the child content items.</summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override IQueryable<ContentItem> GetChildContentItems(
      Guid parentId,
      string providerName)
    {
      throw new NotSupportedException();
    }

    /// <summary>Gets the content item.</summary>
    /// <param name="id">The id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override ContentItem GetContentItem(Guid id, string providerName)
    {
      try
      {
        return this.GetManager(providerName).GetContent(id);
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException(Res.Get<ErrorMessages>().ItemNotFound.Arrange((object) "Content", (object) id), ex);
      }
    }

    /// <summary>Gets the parent content item.</summary>
    /// <param name="id">The id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override ContentItem GetParentContentItem(Guid id, string providerName) => throw new NotImplementedException();

    /// <summary>Gets the manager.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override ContentManager GetManager(string providerName) => ContentManager.GetManager(providerName);

    /// <summary>Gets the view model list.</summary>
    /// <param name="contentList">The content list.</param>
    /// <param name="dataProvider">The data provider.</param>
    /// <param name="liveItem">The dictionary  of live content item related to the master content item</param>
    /// <param name="tempItem">The dictionary  of temp content item related to the master content item</param>
    /// <returns></returns>
    public override IEnumerable<ContentViewModel> GetViewModelList(
      IEnumerable<ContentItem> contentList,
      ContentDataProviderBase dataProvider,
      IDictionary<Guid, ContentItem> liveContentDictionary,
      IDictionary<Guid, ContentItem> tempContentDictionary)
    {
      List<ContentViewModel> viewModelList = new List<ContentViewModel>();
      foreach (ContentItem content in contentList)
      {
        ContentItem itemFromDictionary1 = this.GetItemFromDictionary<Guid, ContentItem>(liveContentDictionary, content.Id);
        ContentItem itemFromDictionary2 = this.GetItemFromDictionary<Guid, ContentItem>(tempContentDictionary, content.Id);
        viewModelList.Add(new ContentViewModel(content, dataProvider, itemFromDictionary1, itemFromDictionary2));
      }
      return (IEnumerable<ContentViewModel>) viewModelList;
    }

    /// <summary>Gets the view model list.</summary>
    /// <param name="contentList">The content list.</param>
    /// <param name="dataProvider">The data provider.</param>
    /// <returns></returns>
    [Obsolete("Please use GetViewModelList with four args. Date: 2011/5/20.")]
    public override IEnumerable<ContentViewModel> GetViewModelList(
      IEnumerable<ContentItem> contentList,
      ContentDataProviderBase dataProvider)
    {
      List<ContentViewModel> viewModelList = new List<ContentViewModel>();
      foreach (ContentItem content in contentList)
        viewModelList.Add(new ContentViewModel((Telerik.Sitefinity.GenericContent.Model.Content) content, dataProvider));
      return (IEnumerable<ContentViewModel>) viewModelList;
    }
  }
}
