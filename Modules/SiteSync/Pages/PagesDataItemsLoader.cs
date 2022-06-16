// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.PagesDataItemsLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.SiteSync
{
  internal class PagesDataItemsLoader
  {
    public virtual IList<object> LoadDataItem(
      object item,
      string language,
      out Dictionary<Guid, ObjectData> controlDependency)
    {
      List<object> objectList = new List<object>();
      controlDependency = new Dictionary<Guid, ObjectData>();
      if (item is PageNode pageNode)
      {
        foreach (PageData pageData in (IEnumerable<PageData>) this.GetPageDataList(pageNode, language))
        {
          objectList.Add((object) pageData);
          IList<object> controlObjects = this.GetControlObjects(pageData, pageNode, language, ref controlDependency);
          objectList.AddRange((IEnumerable<object>) controlObjects);
        }
        IList<object> contentLocations = this.GetContentLocations(pageNode);
        objectList.AddRange((IEnumerable<object>) contentLocations);
      }
      if (item is PageTemplate pageTemplate)
      {
        IList<object> controlObjects = this.GetControlObjects(pageTemplate, ref controlDependency);
        objectList.AddRange((IEnumerable<object>) controlObjects);
      }
      return (IList<object>) objectList;
    }

    public virtual IList<PageData> GetPageDataList(
      PageNode pageNode,
      string language,
      bool loadPersonalizedVersions = true)
    {
      if (pageNode == null)
        throw new ArgumentNullException(nameof (pageNode), "Cannot get PageDataList of null");
      if (pageNode.NodeType != NodeType.Standard && pageNode.NodeType != NodeType.External)
        return (IList<PageData>) new List<PageData>();
      PageManager manager = PageManager.GetManager();
      List<PageData> pageDataList = new List<PageData>();
      PageData mainPageData;
      if (pageNode.LocalizationStrategy == LocalizationStrategy.Split)
      {
        if (language == null && !SystemManager.CurrentContext.AppSettings.Multilingual)
          language = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
        mainPageData = pageNode.PageDataList.Where<PageData>((Func<PageData, bool>) (x => x.Culture == language)).FirstOrDefault<PageData>();
      }
      else
        mainPageData = pageNode.PageDataList.FirstOrDefault<PageData>();
      if (mainPageData != null)
      {
        pageDataList.Add(mainPageData);
        if (loadPersonalizedVersions)
          pageDataList.AddRange((IEnumerable<PageData>) manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == mainPageData.Id)));
      }
      return (IList<PageData>) pageDataList;
    }

    public virtual IList<object> GetControlObjects(
      PageData pageData,
      PageNode pageNode,
      string language,
      ref Dictionary<Guid, ObjectData> controlDependency,
      bool loadPersonalizedVersions = true)
    {
      IList<object> controlPropertyCollection = (IList<object>) new List<object>();
      PageManager manager = PageManager.GetManager();
      if (controlDependency == null)
        controlDependency = new Dictionary<Guid, ObjectData>();
      foreach (PageControl control1 in (IEnumerable<PageControl>) pageData.Controls)
      {
        PageControl control = control1;
        controlPropertyCollection.Add((object) control);
        this.GetControlProperties((ObjectData) control, controlPropertyCollection, controlDependency);
        if (loadPersonalizedVersions)
        {
          IQueryable<PageControl> controls = manager.GetControls<PageControl>();
          Expression<Func<PageControl, bool>> predicate = (Expression<Func<PageControl, bool>>) (c => c.PersonalizationMasterId == control.Id);
          foreach (PageControl control2 in (IEnumerable<PageControl>) controls.Where<PageControl>(predicate))
          {
            controlPropertyCollection.Add((object) control2);
            this.GetControlProperties((ObjectData) control2, controlPropertyCollection, controlDependency);
          }
        }
      }
      CultureInfo culture = !language.IsNullOrEmpty() ? CultureInfo.GetCultureInfo(language) : CultureInfo.InvariantCulture;
      IStatusInfo status;
      if (pageNode != null && pageNode.TryGetExternalStatus(out status, culture) && status.Data is ISchedulingStatus)
      {
        PageDraft pageDraft = pageData.Drafts.Where<PageDraft>((Func<PageDraft, bool>) (d => !d.IsTempDraft)).LastOrDefault<PageDraft>();
        if (pageDraft != null)
        {
          controlPropertyCollection.Add((object) pageDraft);
          foreach (PageDraftControl control in (IEnumerable<PageDraftControl>) pageDraft.Controls)
          {
            controlPropertyCollection.Add((object) control);
            this.GetControlProperties((ObjectData) control, controlPropertyCollection, controlDependency);
          }
        }
      }
      return controlPropertyCollection;
    }

    public virtual IList<object> GetControlObjects(
      PageTemplate pageTemplate,
      ref Dictionary<Guid, ObjectData> controlDependency,
      bool loadPersonalizedVersions = true)
    {
      List<object> controlPropertyCollection = new List<object>();
      if (controlDependency == null)
        controlDependency = new Dictionary<Guid, ObjectData>();
      if (pageTemplate != null)
      {
        foreach (TemplateControl control1 in (IEnumerable<TemplateControl>) pageTemplate.Controls)
        {
          TemplateControl control = control1;
          controlPropertyCollection.Add((object) control);
          this.GetControlProperties((ObjectData) control, (IList<object>) controlPropertyCollection, controlDependency);
          if (loadPersonalizedVersions)
          {
            IQueryable<TemplateControl> controls = PageManager.GetManager().GetControls<TemplateControl>();
            Expression<Func<TemplateControl, bool>> predicate = (Expression<Func<TemplateControl, bool>>) (c => c.PersonalizationMasterId == control.Id);
            foreach (TemplateControl control2 in (IEnumerable<TemplateControl>) controls.Where<TemplateControl>(predicate))
            {
              controlPropertyCollection.Add((object) control2);
              this.GetControlProperties((ObjectData) control2, (IList<object>) controlPropertyCollection, controlDependency);
            }
          }
        }
      }
      return (IList<object>) controlPropertyCollection;
    }

    public virtual IList<object> GetContentLocations(PageNode pageNode)
    {
      IList<object> contentLocations = (IList<object>) new List<object>();
      if (pageNode != null && (pageNode.NodeType == NodeType.Standard || pageNode.NodeType == NodeType.External))
      {
        ContentLocationsManager manager = ContentLocationsManager.GetManager();
        IQueryable<ContentLocationDataItem> locations = manager.GetLocations();
        Expression<Func<ContentLocationDataItem, bool>> predicate1 = (Expression<Func<ContentLocationDataItem, bool>>) (l => l.PageId == pageNode.Id);
        foreach (ContentLocationDataItem locationDataItem in (IEnumerable<ContentLocationDataItem>) locations.Where<ContentLocationDataItem>(predicate1))
        {
          ContentLocationDataItem location = locationDataItem;
          contentLocations.Add((object) location);
          IQueryable<ContentLocationFilterDataItem> contentFilters = manager.GetContentFilters();
          Expression<Func<ContentLocationFilterDataItem, bool>> predicate2 = (Expression<Func<ContentLocationFilterDataItem, bool>>) (f => f.ContentLocation.Id == location.Id);
          foreach (ContentLocationFilterDataItem locationFilterDataItem in (IEnumerable<ContentLocationFilterDataItem>) contentFilters.Where<ContentLocationFilterDataItem>(predicate2))
            contentLocations.Add((object) locationFilterDataItem);
        }
      }
      return contentLocations;
    }

    protected virtual void GetControlProperties(
      ObjectData control,
      IList<object> controlPropertyCollection,
      Dictionary<Guid, ObjectData> controlDependency)
    {
      foreach (ControlProperty property in (IEnumerable<ControlProperty>) control.Properties)
      {
        controlPropertyCollection.Add((object) property);
        if (!controlDependency.ContainsKey(property.Id))
          controlDependency.Add(property.Id, control);
        this.GetChildControlProperties(control, property, controlPropertyCollection, controlDependency);
      }
    }

    protected virtual void GetChildControlProperties(
      ObjectData control,
      ControlProperty controlProperty,
      IList<object> controlPropertyCollection,
      Dictionary<Guid, ObjectData> controlDependency)
    {
      foreach (ControlProperty childProperty in (IEnumerable<ControlProperty>) controlProperty.ChildProperties)
      {
        controlPropertyCollection.Add((object) childProperty);
        if (!controlDependency.ContainsKey(childProperty.Id))
          controlDependency.Add(childProperty.Id, control);
        this.GetChildControlProperties(control, childProperty, controlPropertyCollection, controlDependency);
      }
      IList<ObjectData> listItems = controlProperty.ListItems;
      if (listItems == null || listItems.Count <= 0)
        return;
      foreach (ObjectData control1 in (IEnumerable<ObjectData>) listItems)
      {
        controlPropertyCollection.Add((object) control1);
        this.GetControlProperties(control1, controlPropertyCollection, controlDependency);
      }
    }

    /// <summary>Sets the media properties of an item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="item">The item.</param>
    /// <param name="dataItem">The data item.</param>
    public virtual void SetPageProperties(
      Type itemType,
      string providerName,
      WrapperObject item,
      object dataItem)
    {
      if (typeof (IRendererCommonData).IsAssignableFrom(itemType))
      {
        IRendererCommonData rendererCommonData = dataItem as IRendererCommonData;
        item.AddProperty("TemplateName", (object) rendererCommonData.TemplateName);
        item.AddProperty("Renderer", (object) rendererCommonData.Renderer);
      }
      if (typeof (IFlagsContainer).IsAssignableFrom(itemType))
      {
        IFlagsContainer flagsContainer = dataItem as IFlagsContainer;
        item.AddProperty("Flags", (object) flagsContainer.Flags);
      }
      if (typeof (PageData).IsAssignableFrom(itemType))
      {
        PageData pageData = (PageData) dataItem;
        if (pageData.Template != null)
          item.AddProperty("PageTemplateId", (object) pageData.Template.Id);
        if (pageData.IsPersonalizationPage)
          return;
        item.AddProperty("PageNodeId", (object) pageData.NavigationNode.Id);
      }
      else if (typeof (PageNode).IsAssignableFrom(itemType))
      {
        PageNode pageNode = (PageNode) dataItem;
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        ISite site = multisiteContext != null ? multisiteContext.GetSiteBySiteMapRoot(pageNode.RootNodeId) : SystemManager.CurrentContext.CurrentSite;
        if (site != null)
        {
          Guid homePageId = site.HomePageId;
          if (pageNode.Id == homePageId)
            item.AddProperty("HomePageId", (object) homePageId);
        }
        item.AddProperty("SitemapPriority", (object) pageNode.Priority);
      }
      else
      {
        if (!typeof (PageTemplate).IsAssignableFrom(itemType))
          return;
        PageTemplate pageTemplate = (PageTemplate) dataItem;
        List<Guid> list = PageManager.GetManager().GetSiteTemplateLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == pageTemplate.Id)).Select<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (l => l.SiteId)).ToList<Guid>();
        item.AddProperty("SiteIds", (object) list);
        Image relatedImage = PageTemplateHelper.GetRelatedImage(pageTemplate);
        if (relatedImage != null)
          item.AddProperty("ThumbnailTitle", (object) relatedImage.Title);
        item.AddProperty("PageTemplateTheme", (object) pageTemplate.Themes.GetString(CultureInfo.InvariantCulture, false));
      }
    }

    public virtual void SetControlPropertiesData(
      Type itemType,
      string providerName,
      WrapperObject item,
      object dataItem)
    {
      if (typeof (ControlProperty).IsAssignableFrom(itemType))
      {
        ControlProperty controlProperty = (ControlProperty) dataItem;
        if (controlProperty.ParentProperty != null)
        {
          item.AdditionalProperties.Add("ParentControlPropertyType", (object) controlProperty.ParentProperty.GetType().FullName);
          item.AdditionalProperties.Add("ParentControlPropertyId", (object) controlProperty.ParentProperty.Id);
        }
        if (controlProperty.Control != null)
        {
          item.AdditionalProperties.Add("ParentControlType", (object) controlProperty.Control.GetType().FullName);
          item.AdditionalProperties.Add("ParentControlId", (object) controlProperty.Control.Id);
        }
      }
      else if (typeof (ObjectData).IsAssignableFrom(itemType))
      {
        ObjectData objectData = (ObjectData) dataItem;
        if (objectData.ParentProperty != null)
        {
          item.AdditionalProperties.Add("ParentControlPropertyType", (object) objectData.ParentProperty.GetType().FullName);
          item.AdditionalProperties.Add("ParentControlPropertyId", (object) objectData.ParentProperty.Id);
        }
        else if (dataItem is PageDraftControl)
          item.AdditionalProperties.Add("PageDraftId", (object) ((PageDraftControl) dataItem).Page.Id);
        item.AdditionalProperties.Add("ParentObjectDataId", (object) objectData.ParentId);
      }
      else if (typeof (ControlPresentation).IsAssignableFrom(itemType))
      {
        ControlPresentation controlPresentation = (ControlPresentation) dataItem;
        List<Guid> list = PageManager.GetManager().GetSitePresentationItemLinks<ControlPresentation>().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == controlPresentation.Id)).Select<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (l => l.SiteId)).ToList<Guid>();
        item.AddProperty("SiteIds", (object) list);
      }
      if (!typeof (TemplateControl).IsAssignableFrom(itemType))
        return;
      TemplateControl templateControl = (TemplateControl) dataItem;
      if (templateControl.Page == null)
        return;
      item.AdditionalProperties.Add("ParentTemplateId", (object) templateControl.Page.Id);
    }

    public virtual void SetContentLocationFilterDataItemData(
      Type itemType,
      WrapperObject item,
      object dataItem)
    {
      if (!typeof (ContentLocationFilterDataItem).IsAssignableFrom(itemType))
        return;
      ContentLocationFilterDataItem locationFilterDataItem = (ContentLocationFilterDataItem) dataItem;
      item.AdditionalProperties.Add("ContentLocationId", (object) locationFilterDataItem.ContentLocation.Id);
    }
  }
}
