// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Strategies.PageControlStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.InlineEditing;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.InlineEditing.Strategies
{
  /// <summary>
  /// Class that provides the functionality to manipulate(get/update/delete) page controls for the purpose of inline editing.
  /// (Content blocks, Image controls etc.)
  /// </summary>
  internal class PageControlStrategy : IInlineEditingStrategy
  {
    /// <inheritdoc />
    public virtual LifecycleStatusModel GetItemStatus(
      Guid id,
      Type type,
      string provider)
    {
      return this.GetItemStatus(this.GetItem(id, type, provider));
    }

    /// <inheritdoc />
    public virtual WorkflowOperationResultModel ExecuteOperation(
      Guid id,
      Type type,
      string provider,
      InlineEditingOperation workflowOperation,
      string customWorkflowOperation)
    {
      return new WorkflowOperationResultModel()
      {
        IsExecutedSuccessfully = true,
        RequiresPageOperation = true
      };
    }

    /// <inheritdoc />
    public virtual Guid GetEditableItemId(Guid id, Type type, string provider)
    {
      PageManager manager = PageManager.GetManager();
      ControlData control1 = manager.GetControl<ControlData>(id);
      switch (control1)
      {
        case PageDraftControl _:
          PageDraftControl control = control1 as PageDraftControl;
          PageDraft master = manager.PagesLifecycle.GetMaster(control.Page);
          PageDraft pageDraft1 = manager.PagesLifecycle.CheckOut(master, (CultureInfo) null);
          manager.SaveChanges();
          return (pageDraft1.Controls.SingleOrDefault<PageDraftControl>((Func<PageDraftControl, bool>) (c => c.OriginalControlId == control.OriginalControlId)) ?? pageDraft1.Controls.SingleOrDefault<PageDraftControl>((Func<PageDraftControl, bool>) (c => c.OriginalControlId == id))).Id;
        case Telerik.Sitefinity.Pages.Model.TemplateControl _:
          Uri urlReferrer = SystemManager.CurrentHttpContext.Request.UrlReferrer;
          if (urlReferrer != (Uri) null)
          {
            string absolutePath = urlReferrer.AbsolutePath;
            if (SiteMapBase.GetCurrentProvider().FindSiteMapNode(absolutePath) is PageSiteNode siteMapNode)
            {
              PageData pageData = manager.GetPageNode(siteMapNode.Id).GetPageData();
              PageDraft pageDraft2 = manager.GetPageDraft(pageData.Id);
              PageDraft pageDraft3 = manager.PagesLifecycle.CheckOut(pageDraft2, (CultureInfo) null);
              manager.SaveChanges();
              ControlData controlData = (ControlData) pageDraft3.Controls.SingleOrDefault<PageDraftControl>((Func<PageDraftControl, bool>) (c => c.BaseControlId == id));
              if (controlData == null)
              {
                controlData = new ControlPropertyService().GetOverridingControl(control1, pageDraft3.Id, manager);
                ++pageDraft3.Version;
                manager.SaveChanges();
              }
              return controlData.Id;
            }
            break;
          }
          break;
      }
      return Guid.Empty;
    }

    /// <inheritdoc />
    public virtual void SaveEditableItemFields(
      FieldValueModel[] fields,
      Guid id,
      Type type,
      string provider)
    {
      PageManager manager = PageManager.GetManager();
      ControlData control1 = manager.GetControl<ControlData>(id);
      Control control2 = manager.LoadControl((ObjectData) control1, (CultureInfo) null);
      object component = !(control2 is MvcProxyBase) ? (object) control2 : ((MvcProxyBase) control2).Settings;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
      foreach (FieldValueModel field in fields)
      {
        PropertyDescriptor propertyDescriptor = properties[field.Name];
        object obj = field.Value;
        if (field.Value != null && propertyDescriptor.Attributes.OfType<DynamicLinksContainerAttribute>().FirstOrDefault<DynamicLinksContainerAttribute>() != null)
          obj = (object) LinkParser.UnresolveLinks((string) obj);
        if (propertyDescriptor.CanResetValue(component))
          propertyDescriptor.ResetValue(component);
        if (propertyDescriptor.PropertyType == typeof (Guid))
          propertyDescriptor.SetValue(component, (object) Guid.Parse(obj as string));
        else if (propertyDescriptor.PropertyType == typeof (int))
          propertyDescriptor.SetValue(component, (object) int.Parse(obj as string));
        else
          propertyDescriptor.SetValue(component, obj);
      }
      control1.SetPersistanceStrategy();
      manager.ReadProperties((object) control2, (ObjectData) control1, SystemManager.CurrentContext.Culture, (object) null);
      if (control1 is PageDraftControl pageDraftControl)
      {
        manager.UpdatePropertiesInPage((object) control2, pageDraftControl, ((IEnumerable<FieldValueModel>) fields).Select<FieldValueModel, KeyValuePair<string, string>>((Func<FieldValueModel, KeyValuePair<string, string>>) (x => new KeyValuePair<string, string>(x.Name, (string) x.Value))));
        ++pageDraftControl.Page.Version;
      }
      manager.SaveChanges();
    }

    /// <inheritdoc />
    public virtual void DeleteEditableItem(Guid id, Type type, string provider)
    {
      PageManager manager = PageManager.GetManager();
      PageDraft page;
      try
      {
        page = manager.GetControl<PageDraftControl>(id).Page;
      }
      catch (ItemNotFoundException ex)
      {
        return;
      }
      PageDraft temp = manager.PagesLifecycle.GetTemp(page);
      if (temp != null)
        manager.DiscardPageDraft(temp.Id);
      manager.SaveChanges();
    }

    /// <inheritdoc />
    public virtual LifecycleStatusModel GetItemStatus(object item)
    {
      PageNode pageNode = (PageNode) null;
      if (item is PageDraftControl)
      {
        PageDraftControl pageDraftControl = item as PageDraftControl;
        pageNode = pageDraftControl.Page.IsPersonalized ? PageManager.GetManager().GetPageData(pageDraftControl.Page.PersonalizationMasterId).NavigationNode : pageDraftControl.Page.ParentPage.NavigationNode;
      }
      else if (item is Telerik.Sitefinity.Pages.Model.TemplateControl)
      {
        Telerik.Sitefinity.Pages.Model.TemplateControl templateControl = item as Telerik.Sitefinity.Pages.Model.TemplateControl;
        Uri urlReferrer = SystemManager.CurrentHttpContext.Request.UrlReferrer;
        if (urlReferrer != (Uri) null && templateControl.Editable)
        {
          string absolutePath = urlReferrer.AbsolutePath;
          if (SiteMapBase.GetCurrentProvider().FindSiteMapNode(absolutePath) is PageSiteNode siteMapNode && !siteMapNode.IsPersonalized())
            pageNode = PageManager.GetManager().GetPageNode(siteMapNode.Id);
        }
      }
      if (pageNode != null)
        return InlineEditingHelper.GetPageControlLifecycleStatus(pageNode);
      return new LifecycleStatusModel()
      {
        IsEditable = false,
        IsStatusEditable = false,
        IsPublished = true
      };
    }

    /// <inheritdoc />
    public virtual object GetItem(Guid id, Type type, string provider) => (object) PageManager.GetManager().GetControl<ControlData>(id);

    /// <inheritdoc />
    public virtual string GetDetailsViewUrl(Type itemType, PageNode pageNode)
    {
      if (pageNode == null)
        return string.Empty;
      using (SiteRegion.FromSiteMapRoot(pageNode.RootNodeId))
      {
        CultureInfo culture = (CultureInfo) null;
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
          culture = SystemManager.CurrentContext.Culture;
        string source = RouteHelper.ResolveUrl(pageNode.GetBackendUrl("Edit", culture), UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
        if (culture != null)
          source = source + "/" + culture.Name;
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        if (multisiteContext != null)
        {
          Telerik.Sitefinity.Multisite.ISite siteBySiteMapRoot = multisiteContext.GetSiteBySiteMapRoot(pageNode.RootNodeId);
          if (siteBySiteMapRoot != null)
          {
            string str = source.Contains<char>('?') ? "&" : "?";
            return source + str + ("sf_site=" + siteBySiteMapRoot.Id.ToString() + "&sf_site_temp=true");
          }
        }
        return source;
      }
    }
  }
}
