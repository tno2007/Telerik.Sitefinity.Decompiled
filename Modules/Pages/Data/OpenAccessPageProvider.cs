// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Data.OpenAccessPageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Data.SqlGenerators;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Pages.Model.PropertyLoaders;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.Web.UI;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Upgrades.To5100;
using Telerik.Sitefinity.Upgrades.To5200;
using Telerik.Sitefinity.Upgrades.To5400;
using Telerik.Sitefinity.Upgrades.To5612;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Pages.Data
{
  /// <summary>
  /// Represents page data provider that uses OpenAccess to store and retrieve page data.
  /// </summary>
  [ContentProviderDecorator(typeof (OpenAccessContentDecorator))]
  public class OpenAccessPageProvider : 
    PageDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IOpenAccessUpgradableProviderExtended,
    IOpenAccessUpgradableProvider,
    IOpenAccessCustomContextProvider,
    IDataEventProvider,
    IMultisiteEnabledOAProvider
  {
    private bool enablePageNodeReferences;
    internal const string EnablePageNodeReferencesConfigKey = "enablePageNodeReferences";
    internal const string TemplatePropertyName = "Template";

    /// <inheritdoc />
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      base.Initialize(providerName, config, managerType);
    }

    /// <summary>Creates new page.</summary>
    /// <returns>The new page.</returns>
    public override PageNode CreatePageNode() => this.CreatePageNode(this.GetNewGuid());

    /// <summary>Creates new page with the specified ID.</summary>
    /// <param name="id">The pageId of the new page.</param>
    /// <returns>The new page.</returns>
    public override PageNode CreatePageNode(Guid id)
    {
      PageNode entity = new PageNode(this.ApplicationName, id);
      entity.CanInheritPermissions = true;
      entity.InheritsPermissions = true;
      entity.AllowParametersValidation = true;
      entity.Owner = Telerik.Sitefinity.Security.SecurityManager.GetCurrentUserId();
      ((IDataItem) entity).Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets a page with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page data item.</returns>
    public override PageNode GetPageNode(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("Id cannot be an Empty Guid");
      PageNode pageNode = (PageNode) null;
      try
      {
        pageNode = this.GetContext().GetItemById<PageNode>(id.ToString());
      }
      catch (ItemNotFoundException ex1)
      {
        bool flag = true;
        try
        {
          PageNodeReference itemById = this.GetContext().GetItemById<PageNodeReference>(id.ToString());
          if (itemById.PageNode != null)
          {
            flag = false;
            pageNode = itemById.PageNode;
          }
        }
        catch (ItemNotFoundException ex2)
        {
          flag = true;
        }
        if (flag)
          throw;
      }
      ((IDataItem) pageNode).Provider = (object) this;
      return pageNode;
    }

    /// <summary>Gets a query for navigation items.</summary>
    /// <returns>The query for navigation items.</returns>
    public override IQueryable<PageNode> GetPageNodes()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<PageNode>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<PageNode>((Expression<Func<PageNode, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(PageNode item)
    {
      PageNode parent = item.Parent;
      if (parent != null)
        this.DeletePermissionsInheritanceAssociation((ISecuredObject) parent, (ISecuredObject) item);
      this.providerDecorator.DeletePermissions((object) item);
      item.StoreAvailableLanguages();
      item.StoreAvailableCultures();
      this.GetContext().Remove((object) item);
    }

    /// <summary>
    /// Moves the page node passed as first argument to one of the positions predefined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.MoveTo" /> enumeration
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="moveTo">The position to move to.</param>
    public override void MovePageNode(PageNode nodeToMove, MoveTo moveTo)
    {
      Guid parentId = nodeToMove.Parent.Id;
      if (moveTo != MoveTo.FirstInTheCurrentLevel)
      {
        if (moveTo != MoveTo.LastInTheCurrentLevel)
          return;
        PageNode pageNode = this.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pt => pt.Parent != default (object) && pt.Parent.Id == parentId)).OrderByDescending<PageNode, float>((Expression<Func<PageNode, float>>) (pt => pt.Ordinal)).First<PageNode>();
        nodeToMove.SetOrdinalBetween(new float?(pageNode.Ordinal), new float?(), false);
      }
      else
      {
        PageNode pageNode = this.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pt => pt.Parent != default (object) && pt.Parent.Id == parentId)).OrderBy<PageNode, float>((Expression<Func<PageNode, float>>) (pt => pt.Ordinal)).First<PageNode>();
        nodeToMove.SetOrdinalBetween(new float?(), new float?(pageNode.Ordinal));
      }
    }

    /// <summary>
    /// Moves the page node passed as first argument by the specified number of places, in the direction given by the
    /// <see cref="T:Telerik.Sitefinity.Modules.Pages.Move" /> enumeration.
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="move">A value representing the direction in which the node will be moved.</param>
    /// <param name="numberOfPlaces">The number of places to move.</param>
    public override void MovePageNode(PageNode nodeToMove, Move move, int numberOfPlaces)
    {
      if (numberOfPlaces < 1)
        throw new ArgumentException("numberOfPlaces argument must be larger than 0.");
      Guid parentId = nodeToMove.Parent.Id;
      float ordinal = nodeToMove.Ordinal;
      if (move != Move.Up)
      {
        if (move != Move.Down)
          throw new NotSupportedException();
        List<float> list = this.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pt => pt.Parent != default (object) && pt.Parent.Id == parentId && pt.Ordinal > ordinal)).OrderBy<PageNode, float>((Expression<Func<PageNode, float>>) (pt => pt.Ordinal)).Select<PageNode, float>((Expression<Func<PageNode, float>>) (pt => pt.Ordinal)).ToList<float>().Distinct<float>().Skip<float>(numberOfPlaces - 1).Take<float>(2).ToList<float>();
        if (list.Count < 2)
          this.MovePageNode(nodeToMove, MoveTo.LastInTheCurrentLevel);
        else
          nodeToMove.SetOrdinalBetween(new float?(list[0]), new float?(list[1]));
      }
      else
      {
        List<float> list = this.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pt => pt.Parent != default (object) && pt.Parent.Id == parentId && pt.Ordinal < ordinal)).OrderByDescending<PageNode, float>((Expression<Func<PageNode, float>>) (pt => pt.Ordinal)).Select<PageNode, float>((Expression<Func<PageNode, float>>) (pt => pt.Ordinal)).ToList<float>().Distinct<float>().Skip<float>(numberOfPlaces - 1).Take<float>(2).ToList<float>();
        if (list.Count < 2)
          this.MovePageNode(nodeToMove, MoveTo.FirstInTheCurrentLevel);
        else
          nodeToMove.SetOrdinalBetween(new float?(list[1]), new float?(list[0]));
      }
    }

    /// <summary>
    /// Moves the page node passed as first argument to the place defined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.Place" /> enumeration,
    /// relative to the supplied target page node
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="targetNode">An instance of the page that serves as a reference point to the new placing of the page.</param>
    /// <param name="place">A value representing the place to which the page ought to be moved.</param>
    public override void MovePageNode(PageNode nodeToMove, PageNode targetNode, Place place)
    {
      if (targetNode == null)
        throw new ArgumentNullException("targetPage");
      Guid targetParentId = targetNode.Parent.Id;
      float targetOrdinal = targetNode.Ordinal;
      if (place != Place.After)
      {
        if (place != Place.Before)
          throw new NotSupportedException();
        IQueryable<float> source = this.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pt => pt.Parent != default (object) && pt.Parent.Id == targetParentId && pt.Ordinal < targetOrdinal)).OrderByDescending<PageNode, float>((Expression<Func<PageNode, float>>) (pt => pt.Ordinal)).Select<PageNode, float>((Expression<Func<PageNode, float>>) (pt => pt.Ordinal));
        float num = source.FirstOrDefault<float>();
        if ((double) num == 0.0 && !source.Any<float>())
          nodeToMove.SetOrdinalBetween(new float?(), new float?(targetNode.Ordinal));
        else
          nodeToMove.SetOrdinalBetween(new float?(num), new float?(targetNode.Ordinal));
      }
      else
      {
        IQueryable<float> source = this.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pt => pt.Parent != default (object) && pt.Parent.Id == targetParentId && pt.Ordinal > targetOrdinal)).OrderBy<PageNode, float>((Expression<Func<PageNode, float>>) (pt => pt.Ordinal)).Select<PageNode, float>((Expression<Func<PageNode, float>>) (pt => pt.Ordinal));
        float num = source.FirstOrDefault<float>();
        if ((double) num == 0.0 && !source.Any<float>())
          nodeToMove.SetOrdinalBetween(new float?(targetOrdinal), new float?(), false);
        else
          nodeToMove.SetOrdinalBetween(new float?(targetOrdinal), new float?(num));
      }
    }

    /// <summary>
    /// Moves the page node passed as first argument to the place defined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.Place" /> enumeration,
    /// relative to the supplied target page node
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="targetNodeId">The ID of the page that serves as a reference point to the new placing of the page.</param>
    /// <param name="place">A value representing the direction in which the node will be moved.</param>
    public override void MovePageNode(PageNode nodeToMove, Guid targetNodeId, Place place)
    {
      PageNode pageNode = this.GetPageNode(targetNodeId);
      this.MovePageNode(nodeToMove, pageNode, place);
    }

    /// <summary>
    /// Make the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> passed as the first argument the child of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> passed as the second argument.
    /// </summary>
    /// <remarks>
    /// The node will be appended at the end of the hierarchy (the Ordinal value will be recalculated).
    /// </remarks>
    /// <param name="childNode">The <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> whose parent should be changed</param>
    /// <param name="newParent">The <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> representing the new parent</param>
    public override void ChangeParent(PageNode childNode, PageNode newParent)
    {
      if (newParent == null)
        throw new ArgumentNullException(nameof (newParent));
      if (childNode.Id == newParent.Id)
        throw new ArgumentException("The page cannot become the child of itself.", nameof (newParent));
      if (newParent.Parent != null && childNode.Id == newParent.Parent.Id)
        throw new ArgumentException("The child page cannot become parent of it's own parent.", "page");
      if (childNode.Parent != null)
        this.DeletePermissionsInheritanceAssociation((ISecuredObject) childNode.Parent, (ISecuredObject) childNode);
      this.providerDecorator.CreatePermissionInheritanceAssociation((ISecuredObject) newParent, (ISecuredObject) childNode);
      childNode.SupportedPermissionSets = newParent.SupportedPermissionSets;
      if (!this.SuppressSecurityChecks)
      {
        if (!childNode.IsGranted("Pages", "Create"))
        {
          this.SuppressSecurityChecks = true;
          this.Delete(childNode);
          this.SuppressSecurityChecks = false;
          throw new SecurityDemandFailException(string.Format(Telerik.Sitefinity.Localization.Res.Get<PageResources>().CannotCreateChildPages, (object) newParent.Title));
        }
      }
      if (childNode.Parent == null || childNode.Parent.Id != newParent.Id)
      {
        bool suppressSecurityChecks = this.SuppressSecurityChecks;
        this.SuppressSecurityChecks = true;
        childNode.Ordinal = 0.0f;
        this.SuppressSecurityChecks = suppressSecurityChecks;
      }
      childNode.Parent = newParent;
    }

    /// <summary>Creates new page.</summary>
    /// <returns>The new page.</returns>
    public override PageData CreatePageData() => this.CreatePageData(this.GetNewGuid());

    /// <summary>Creates new page with the specified ID.</summary>
    /// <param name="id">The pageId of the new page.</param>
    /// <returns>The new page.</returns>
    public override PageData CreatePageData(Guid id)
    {
      PageData entity = new PageData(this.ApplicationName, id);
      entity.Owner = Telerik.Sitefinity.Security.SecurityManager.GetCurrentUserId();
      entity.Provider = (object) this;
      entity.DateCreated = DateTime.UtcNow;
      entity.LastModified = DateTime.UtcNow;
      entity.LastModifiedBy = Telerik.Sitefinity.Security.SecurityManager.GetCurrentUserId();
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets a page with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page data item.</returns>
    public override PageData GetPageData(Guid id)
    {
      PageData pageData = !(id == Guid.Empty) ? (PageData) this.GetContext().GetObjectById(Database.OID.ParseObjectId(typeof (PageData), id.ToString())) : throw new ArgumentException("Id cannot be an Empty Guid");
      pageData.Provider = (object) this;
      return pageData;
    }

    /// <summary>Gets a query for pages.</summary>
    /// <returns>The query for pages.</returns>
    public override IQueryable<PageData> GetPageDataList()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<PageData>((DataProviderBase) this).Where<PageData>((Expression<Func<PageData, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The page to delete.</param>
    public override void Delete(PageData item)
    {
      if (item == null)
        return;
      if (item.PersonalizationMasterId != Guid.Empty)
      {
        PageData persMaster = this.GetPageData(item.PersonalizationMasterId);
        int num1 = this.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.PersonalizationMasterId == persMaster.Id && pd.Id != item.Id)).Count<PageData>();
        int num2 = 0;
        int num3 = 0;
        IEnumerable<PageData> source = this.GetDirtyItems().OfType<PageData>().Where<PageData>((Func<PageData, bool>) (pd => pd.PersonalizationMasterId == persMaster.Id && pd.Id != item.Id));
        foreach (object itemInTransaction in source)
        {
          switch (this.GetDirtyItemStatus(itemInTransaction))
          {
            case SecurityConstants.TransactionActionType.New:
              ++num2;
              continue;
            case SecurityConstants.TransactionActionType.Deleted:
              ++num3;
              continue;
            default:
              continue;
          }
        }
        bool flag = num2 > 0 || num1 > num3;
        if (persMaster.IsPersonalized != flag)
        {
          PageData pageData = source.FirstOrDefault<PageData>((Func<PageData, bool>) (i => i.Id == persMaster.Id));
          if (pageData != null)
          {
            if ((this.GetDirtyItemStatus((object) pageData) != SecurityConstants.TransactionActionType.Updated ? 0 : (this.IsFieldDirty((object) pageData, "IsPersonalized") ? 1 : 0)) == 0)
              persMaster.IsPersonalized = flag;
          }
          else
            persMaster.IsPersonalized = flag;
        }
      }
      else
      {
        PageNode navigationNode = item.NavigationNode;
        if (navigationNode != null)
        {
          bool flag = true;
          if (navigationNode.LocalizationStrategy == LocalizationStrategy.Split && navigationNode.PageDataList.Count > 1)
            flag = false;
          if (flag)
          {
            if (navigationNode.IsPermissionSetSupported("Pages"))
            {
              if (!navigationNode.IsGranted("Pages", nameof (Delete)))
                throw new SecurityException("You have no permissions to delete this page");
            }
            this.providerDecorator.DeletePermissions((object) navigationNode);
            this.GetContext().Remove((object) navigationNode);
          }
          else
            navigationNode.PrepareForPageDataDeleting(item);
        }
      }
      this.GetContext().Remove((object) item);
    }

    /// <summary>Changes the owner of a page.</summary>
    /// <param name="node">The page node.</param>
    /// <param name="newOwnerID">The new owner ID.</param>
    public override void ChangeOwner(PageNode node, Guid newOwnerID)
    {
      if (node == null || !(newOwnerID != Guid.Empty) || !(node.Owner != newOwnerID))
        return;
      node.Owner = newOwnerID;
    }

    /// <summary>Creates new page template.</summary>
    /// <returns>The new page template.</returns>
    public override PageTemplate CreateTemplate() => this.CreateTemplate(this.GetNewGuid());

    /// <summary>Creates new page template with the specified ID.</summary>
    /// <param name="id">The pageId of the new page template.</param>
    /// <returns>The new page template.</returns>
    public override PageTemplate CreateTemplate(Guid id)
    {
      if (id == Guid.Empty)
        this.CreateTemplate();
      PageTemplate entity = new PageTemplate(this.ApplicationName, id);
      entity.Owner = Telerik.Sitefinity.Security.SecurityManager.GetCurrentUserId();
      entity.Key = "T" + Telerik.Sitefinity.Security.SecurityManager.GetRandomKey(4);
      entity.CanInheritPermissions = true;
      entity.InheritsPermissions = true;
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Links the template to site.</summary>
    /// <param name="template">The template.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal override SiteItemLink LinkTemplateToSite(
      PageTemplate template,
      Guid siteId)
    {
      return this.AddItemLink(siteId, (IDataItem) template);
    }

    /// <summary>Gets the page template with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page template.</returns>
    public override PageTemplate GetTemplate(Guid id)
    {
      PageTemplate template = !(id == Guid.Empty) ? this.GetContext().GetItemById<PageTemplate>(id.ToString()) : throw new ArgumentException("Id cannot be an Empty Guid");
      ((IDataItem) template).Provider = (object) this;
      return template;
    }

    /// <summary>Gets a query for page templates.</summary>
    /// <returns>The query for page templates.</returns>
    public override IQueryable<PageTemplate> GetTemplates()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<PageTemplate>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Gets a query for page templates in a specific site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>The query for page templates.</returns>
    internal override IQueryable<PageTemplate> GetTemplates(Guid siteId) => this.GetSiteItems<PageTemplate>(siteId);

    /// <summary>
    /// Gets the links for all templates linked to the specified site.
    /// </summary>
    /// <returns>The query for SiteItemLink.</returns>
    [EnumeratorPermission("PageTemplates", new string[] {"View"})]
    internal override IQueryable<SiteItemLink> GetSiteTemplateLinks() => this.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemType == typeof (PageTemplate).FullName));

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The page template to delete.</param>
    public override void Delete(PageTemplate item)
    {
      this.DeleteLinksForItem((IDataItem) item);
      this.providerDecorator.DeletePermissions((object) item);
      this.GetContext().Remove((object) item);
    }

    /// <summary>Creates new draft page or template.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <returns>The new control.</returns>
    public override T CreateDraft<T>() => this.CreateDraft<T>(this.GetNewGuid());

    /// <summary>Creates new draft or page with the specified ID.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="id">The pageId of the new control.</param>
    /// <returns>The new control.</returns>
    public override T CreateDraft<T>(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("Id cannot be an Empty Guid");
      Type type = typeof (T);
      DraftData entity;
      if (typeof (PageDraft) == type)
        entity = (DraftData) new PageDraft(this.ApplicationName, id);
      else if (typeof (TemplateDraft) == type)
        entity = (DraftData) new TemplateDraft(this.ApplicationName, id);
      else
        throw new ArgumentException("Invalid Type \"{0}\".".Arrange((object) typeof (T).FullName));
      entity.Owner = Telerik.Sitefinity.Security.SecurityManager.GetCurrentUserId();
      entity.LastModified = DateTime.UtcNow;
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return (T) entity;
    }

    /// <summary>
    /// Gets the draft page or template with the specified ID.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="id">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public override T GetDraft<T>(Guid id)
    {
      T draft = !(id == Guid.Empty) ? this.GetContext().GetItemById<T>(id.ToString()) : throw new ArgumentException("Id cannot be an Empty Guid");
      draft.Provider = (object) this;
      return draft;
    }

    /// <summary>Gets a query for draft pages or templates.</summary>
    /// <typeparam name="T">The type of the data item.</typeparam>
    /// <returns>The query for controls.</returns>
    public override IQueryable<T> GetDrafts<T>()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<T>((DataProviderBase) this).Where<T>((Expression<Func<T, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(DraftData item)
    {
      this.providerDecorator.DeletePermissions((object) item);
      this.GetContext().Remove((object) item);
    }

    /// <summary>Creates new control.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="isBackendObject">Is backend object.</param>
    /// <returns>The new control.</returns>
    public override T CreateControl<T>(bool isBackendObject = false) => this.CreateControl<T>(this.GetNewGuid(), isBackendObject);

    /// <summary>Creates new control with the specified ID.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="id">The pageId of the new control.</param>
    /// <param name="isBackendObject">Is backend object.</param>
    /// <returns>The new control.</returns>
    public override T CreateControl<T>(Guid id, bool isBackendObject = false)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("Id cannot be an Empty Guid");
      SitefinityOAContext context = this.GetContext();
      Type type = typeof (T);
      ObjectData entity;
      if (typeof (PageControl) == type)
        entity = (ObjectData) new PageControl(this.ApplicationName, id, isBackendObject);
      else if (typeof (TemplateControl) == type)
        entity = (ObjectData) new TemplateControl(this.ApplicationName, id, isBackendObject);
      else if (typeof (PageDraftControl) == type)
        entity = (ObjectData) new PageDraftControl(this.ApplicationName, id, isBackendObject);
      else if (typeof (TemplateDraftControl) == type)
        entity = (ObjectData) new TemplateDraftControl(this.ApplicationName, id, isBackendObject);
      else if (typeof (ObjectData) == type)
        entity = new ObjectData(this.ApplicationName, id, isBackendObject);
      else
        throw new ArgumentException("Invalid Type \"{0}\".".Arrange((object) typeof (T).FullName));
      ((IDataItem) entity).Provider = (object) this;
      if (entity is IOwnership ownership)
        ownership.Owner = Telerik.Sitefinity.Security.SecurityManager.GetCurrentUserId();
      entity.Strategy = PropertyPersistenceStrategy.NotTranslatable;
      context.Add((object) entity);
      return (T) entity;
    }

    /// <summary>Gets the control with the specified ID.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="id">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public override T GetControl<T>(Guid id)
    {
      T forControl = !(id == Guid.Empty) ? this.GetContext().GetItemById<T>(id.ToString()) : throw new ArgumentException("Id cannot be an Empty Guid");
      this.DemandViewControlOrLayoutPermission((object) forControl);
      forControl.Provider = (object) this;
      return forControl;
    }

    /// <summary>Gets a query for controls.</summary>
    /// <typeparam name="T">The type of the data item.</typeparam>
    /// <returns>The query for controls.</returns>
    public override IQueryable<T> GetControls<T>()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<T>((DataProviderBase) this).Where<T>((Expression<Func<T, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public override void Delete(ObjectData item)
    {
      ControlData controlData = item as ControlData;
      if (controlData != null && controlData.PersonalizationMasterId != Guid.Empty)
      {
        ControlData persMaster = this.GetControl<ControlData>(controlData.PersonalizationMasterId);
        persMaster.IsPersonalized = (this.GetControls<ControlData>().Any<ControlData>((Expression<Func<ControlData, bool>>) (cd => cd.PersonalizationMasterId == persMaster.Id && cd.Id != controlData.Id)) ? 1 : 0) != 0;
        persMaster.PersonalizedControls.Remove(controlData);
      }
      if (!this.SuppressSecurityChecks)
        this.DemandDeleteControlOrLayoutPermission(item);
      this.GetContext().Remove((object) item);
    }

    /// <summary>Creates new page template.</summary>
    /// <returns>The new page template.</returns>
    public override ControlProperty CreateProperty() => this.CreateProperty(this.GetNewGuid());

    /// <summary>Creates new page template with the specified ID.</summary>
    /// <param name="id">The pageId of the new page template.</param>
    /// <returns>The new page template.</returns>
    public override ControlProperty CreateProperty(Guid id)
    {
      ControlProperty entity = !(id == Guid.Empty) ? new ControlProperty(this.ApplicationName, id) : throw new ArgumentException("Id cannot be an Empty Guid");
      ((IDataItem) entity).Provider = (object) this;
      entity.SetFlag(1);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the page template with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page template.</returns>
    public override ControlProperty GetProperty(Guid id)
    {
      ControlProperty property = !(id == Guid.Empty) ? this.GetContext().GetItemById<ControlProperty>(id.ToString()) : throw new ArgumentException("Id cannot be an Empty Guid");
      ((IDataItem) property).Provider = (object) this;
      return property;
    }

    /// <summary>Gets a query for page templates.</summary>
    /// <returns>The query for page templates.</returns>
    public override IQueryable<ControlProperty> GetProperties()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<ControlProperty>((DataProviderBase) this).Where<ControlProperty>((Expression<Func<ControlProperty, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(ControlProperty item)
    {
      this.providerDecorator.DeletePermissions((object) item);
      this.GetContext().Remove((object) item);
    }

    /// <summary>
    /// Creates new object for storing presentation information.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public override T CreatePresentationItem<T>() => this.CreatePresentationItem<T>(this.GetNewGuid());

    /// <summary>
    /// Creates new object for storing presentation information with the specified ID.
    /// </summary>
    /// <typeparam name="T">The type of the item</typeparam>
    /// <param name="id">The pageId of the new item.</param>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public override T CreatePresentationItem<T>(Guid id)
    {
      Type type = typeof (T);
      PresentationData entity;
      if (typeof (PagePresentation) == type)
        entity = (PresentationData) new PagePresentation(this.ApplicationName, id);
      else if (typeof (PageDraftPresentation) == type)
        entity = (PresentationData) new PageDraftPresentation(this.ApplicationName, id);
      else if (typeof (TemplatePresentation) == type)
        entity = (PresentationData) new TemplatePresentation(this.ApplicationName, id);
      else if (typeof (TemplateDraftPresentation) == type)
        entity = (PresentationData) new TemplateDraftPresentation(this.ApplicationName, id);
      else if (typeof (ControlPresentation) == type)
        entity = (PresentationData) new ControlPresentation(this.ApplicationName, id);
      else
        throw new ArgumentException("Invalid Type \"{0}\".".Arrange((object) typeof (T).FullName));
      entity.Owner = Telerik.Sitefinity.Security.SecurityManager.GetCurrentUserId();
      entity.DateCreated = DateTime.UtcNow;
      entity.LastModified = DateTime.UtcNow;
      ((IDataItem) entity).Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return (T) entity;
    }

    /// <summary>Links the template to site.</summary>
    /// <param name="presentationData">The presentation data.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal override SiteItemLink LinkPresentationItemToSite(
      PresentationData presentationData,
      Guid siteId)
    {
      return this.AddItemLink(siteId, (IDataItem) presentationData);
    }

    /// <summary>Gets the item with the specified ID.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="id">The ID to search for.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public override T GetPresentationItem<T>(Guid id)
    {
      T presentationItem = !(id == Guid.Empty) ? this.GetContext().GetItemById<T>(id.ToString()) : throw new ArgumentException("Id cannot be an Empty Guid");
      presentationItem.Provider = (object) this;
      return presentationItem;
    }

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <returns>The query for page templates.</returns>
    public override IQueryable<T> GetPresentationItems<T>()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<T>((DataProviderBase) this).Where<T>((Expression<Func<T, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <param name="siteId">The site id.</param>
    /// <returns>The query for page templates.</returns>
    internal override IQueryable<T> GetPresentationItems<T>(Guid siteId) => this.GetSiteItems<T>(siteId);

    /// <summary>Gets the links for all presentation items.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <returns>The query for SiteItemLink.</returns>
    internal override IQueryable<SiteItemLink> GetSitePresentationItemLinks<T>() => this.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemType == typeof (T).FullName));

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(PresentationData item)
    {
      this.DeleteLinksForItem((IDataItem) item);
      this.providerDecorator.DeletePermissions((object) item);
      this.GetContext().Remove((object) item);
    }

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The meta data source.</returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new PagesMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <inheritdoc />
    public virtual int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    /// <inheritdoc />
    public virtual void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
      if (upgradingFromSchemaVersionNumber <= 0)
        return;
      if (upgradingFromSchemaVersionNumber < 1600)
      {
        OpenAccessConnection.OracleUpgradeLStringColumn(context, "sf_draft_pages", "success_message_", "NCLOB", "OpenAccessPageProvider: Upgrade to 4.2 - sf_draft_pages success_message_ to NCLOB");
        string upgradeScript = context.DatabaseContext.DatabaseType == DatabaseType.Oracle ? "drop table \"sf_pg_nd_spprtd_prmission_sets\"" : "drop table sf_pg_nd_spprtd_prmission_sets";
        OpenAccessConnection.Upgrade(context, "OpenAccessPageProvider: Upgrade to 4.2 - Delete obsolete table 'sf_pg_nd_spprtd_prmission_sets'", upgradeScript);
      }
      if (upgradingFromSchemaVersionNumber < 3700 && context.DatabaseContext.DatabaseType == DatabaseType.MsSql)
        OpenAccessConnection.TryFixDuplicateRecords(context, "sf_page_node_sf_permissions", "id, id2", "seq", "min");
      if (upgradingFromSchemaVersionNumber < SitefinityVersion.Sitefinity6_2.Build && context.DatabaseContext.DatabaseType == DatabaseType.MsSql)
      {
        string str1 = "OpenAccessPageProvider: Delete obsolete control properties' dependencies";
        string str2 = "\r\nSELECT \r\n\tK_Table\r\nFROM\r\n(SELECT\r\n    K_Table = FK.TABLE_NAME,\r\n    FK_Column = CU.COLUMN_NAME,\r\n    PK_Table = PK.TABLE_NAME,\r\n    PK_Column = PT.COLUMN_NAME,\r\n    Constraint_Name = C.CONSTRAINT_NAME\r\nFROM\r\n    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C\r\nINNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK\r\n    ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME\r\nINNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK\r\n    ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME\r\nINNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU\r\n    ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME\r\nINNER JOIN (\r\n            SELECT\r\n                i1.TABLE_NAME,\r\n                i2.COLUMN_NAME\r\n            FROM\r\n                INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1\r\n            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2\r\n                ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME\r\n            WHERE\r\n                i1.CONSTRAINT_TYPE = 'PRIMARY KEY'\r\n           ) PT\r\n    ON PT.TABLE_NAME = PK.TABLE_NAME) as dependencies\r\nwhere \r\n\tPK_Table in ('sf_control_properties', 'sf_object_data')";
        try
        {
          using (UpgradingContext upgradingContext = new UpgradingContext(context))
          {
            List<string> stringList = new List<string>();
            using (OACommand command = ((OpenAccessContextBase) upgradingContext).Connection.CreateCommand())
            {
              command.CommandText = str2;
              using (OADataReader oaDataReader = command.ExecuteReader())
              {
                while (oaDataReader.Read())
                {
                  string str3 = oaDataReader.GetString(0);
                  if (str3.StartsWith("sf_control_properties", StringComparison.OrdinalIgnoreCase) || str3.StartsWith("sf_object_data", StringComparison.OrdinalIgnoreCase) && !str3.Equals("sf_object_data_sf_permissions", StringComparison.OrdinalIgnoreCase))
                    stringList.Add(str3);
                }
                oaDataReader.Close();
              }
            }
            foreach (string str4 in stringList)
            {
              upgradingContext.ExecuteNonQuery(string.Format("drop table {0}", (object) str4));
              upgradingContext.SaveChanges();
            }
          }
          Log.Write((object) string.Format("PASSED : {0}", (object) str1), ConfigurationPolicy.UpgradeTrace);
        }
        catch (Exception ex)
        {
          Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str1, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
      if (upgradingFromSchemaVersionNumber < 5000)
      {
        if (context.DatabaseContext.DatabaseType == DatabaseType.MsSql)
        {
          try
          {
            using (UpgradingContext oaContext = new UpgradingContext(context))
              PerformanceOptimization.ExecuteNonQueryCommand((OpenAccessContext) oaContext, "DBCC DBREINDEX (\"sf_control_properties\")", new int?(300));
          }
          catch (Exception ex)
          {
          }
        }
      }
      if (upgradingFromSchemaVersionNumber < SitefinityVersion.Sitefinity7_1.Build && context.DatabaseContext.DatabaseType == DatabaseType.MsSql)
        new ControlPropertyUpgrader().PrepareSchema(context);
      if (upgradingFromSchemaVersionNumber < SitefinityVersion.Sitefinity7_3.Build && context.DatabaseContext.DatabaseType == DatabaseType.MsSql)
        new ObjectDataUpgrader().PrepareSchema(context);
      if (upgradingFromSchemaVersionNumber >= SitefinityVersion.Sitefinity10_2.Build)
        return;
      try
      {
        string tableName = "sf_page_data";
        SqlGenerator sqlGenerator = SqlGenerator.Get(context.DatabaseContext.DatabaseType);
        string dropColumn1 = sqlGenerator.GetDropColumn(tableName, "cache_output");
        string dropColumn2 = sqlGenerator.GetDropColumn(tableName, "sliding_expiration");
        string dropColumn3 = sqlGenerator.GetDropColumn(tableName, "cache_duration");
        OpenAccessConnection.Upgrade(context, "OpenAccessPageProvider: Upgrade to 10.2 - Delete obsolete column 'cache_output'", dropColumn1);
        OpenAccessConnection.Upgrade(context, "OpenAccessPageProvider: Upgrade to 10.2 - Delete obsolete column 'sliding_expiration'", dropColumn2);
        OpenAccessConnection.Upgrade(context, "OpenAccessPageProvider: Upgrade to 10.2 - Delete obsolete column 'cache_duration'", dropColumn3);
      }
      catch (Exception ex)
      {
        Log.Write((object) "Failed to drop 'cache_output', 'sliding_expiration' and 'cache_duration' columns from 'sf_page_data' table.", ConfigurationPolicy.UpgradeTrace);
      }
    }

    /// <inheritdoc />
    public virtual void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
      if (upgradedFromSchemaVersionNumber <= 0)
        return;
      if (upgradedFromSchemaVersionNumber <= 1300)
        OpenAccessConnection.MsSqlUpgrade(context.Connection, "OpenAccessPageProvider: Upgrade to 1300", (System.Action<IDbCommand>) (cmd =>
        {
          cmd.ExecuteNonQuery("\r\n                        DELETE d\r\n                        FROM sf_page_data d\r\n                        LEFT OUTER JOIN sf_page_node n ON n.content_id = d.content_id\r\n                        WHERE n.content_id IS NULL\r\n                    ");
          cmd.ExecuteNonQuery("\r\n                        DELETE d\r\n                        FROM sf_draft_pages d\r\n                        LEFT OUTER JOIN sf_page_data p ON d.page_id = p.content_id\r\n                        WHERE p.content_id IS NULL\r\n                    ");
          cmd.ExecuteNonQuery("update \r\n                        sf_presentation_data\r\n                        set resource_assembly_name = substring(resource_assembly_name, CHARINDEX (',', resource_assembly_name ) + 2, len(resource_assembly_name) + 1 )\r\n                        where charindex(',',resource_assembly_name) > 0\r\n                    ");
          cmd.ExecuteNonQuery("UPDATE sf_page_node SET node_type = 1 WHERE content_id IS NULL");
          cmd.ExecuteNonQuery("UPDATE sf_page_data SET cache_profile = NULL WHERE cache_profile = 'Default' ");
          cmd.ExecuteNonQuery("\r\n                        IF 0 = (SELECT is_nullable FROM sys.columns \r\n\t\t                        WHERE name = 'addtnl_rls_rdrct_t_default_one'\r\n\t\t                        AND object_id = OBJECT_ID('sf_page_data'))\r\n                            ALTER TABLE sf_page_data ALTER COLUMN addtnl_rls_rdrct_t_default_one TINYINT NULL\r\n                    ");
          cmd.ExecuteNonQuery("\r\n                        IF 0 = (SELECT is_nullable FROM sys.columns \r\n\t\t                        WHERE name = 'allow_multiple_urls'\r\n\t\t                        AND object_id = OBJECT_ID('sf_page_data'))\r\n                            ALTER TABLE sf_page_data ALTER COLUMN allow_multiple_urls TINYINT NULL\r\n                    ");
        }));
      if (upgradedFromSchemaVersionNumber <= 1339)
        OpenAccessConnection.MsSqlUpgrade(context.Connection, "OpenAccessPageProvider: Upgrade to 1339", (System.Action<IDbCommand>) (cmd =>
        {
          cmd.ExecuteNonQuery("\r\n                        UPDATE sf_object_data \r\n                        SET sf_object_data.object_type = 'Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockBase' \r\n                        FROM sf_object_data INNER JOIN sf_page_templates ON sf_object_data.page_id = sf_page_templates.id\r\n                        WHERE object_type = 'Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock'\r\n                    ");
          cmd.ExecuteNonQuery("\r\n                        UPDATE sf_object_data SET sf_object_data.object_type = 'Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockBase' \r\n                        WHERE original_control_id in (SELECT sf_object_data.id \r\n                                                        FROM sf_object_data INNER JOIN sf_page_templates ON sf_object_data.page_id = sf_page_templates.id\r\n                                                        WHERE object_type = 'Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockBase' ) AND\r\n                                object_type = 'Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock'\r\n                    ");
        }));
      if (upgradedFromSchemaVersionNumber < 1700)
      {
        try
        {
          List<ControlProperty> source1 = new List<ControlProperty>();
          ParameterExpression parameterExpression;
          // ISSUE: method reference
          IQueryable<ControlProperty> source2 = context.GetAll<ControlProperty>().Where<ControlProperty>(Expression.Lambda<Func<ControlProperty, bool>>((Expression) Expression.AndAlso(p.Control.ObjectType == "Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock" && p.Name == "SharedContentID", (Expression) Expression.NotEqual(p.Value, (Expression) Expression.Call(Guid.Empty, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()))), parameterExpression));
          Expression<Func<ControlProperty, ObjectData>> selector = (Expression<Func<ControlProperty, ObjectData>>) (p => p.Control);
          foreach (ObjectData objectData in (IEnumerable<ObjectData>) source2.Select<ControlProperty, ObjectData>(selector).Distinct<ObjectData>())
          {
            foreach (ControlProperty controlProperty in objectData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Html")))
              source1.Add(controlProperty);
          }
          if (source1.Count > 0)
            OpenAccessConnection.FixDynamicLinks<ControlProperty>(context, (IOpenAccessMetadataProvider) this, source1.AsQueryable<ControlProperty>(), "MultilingualValue");
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
        try
        {
          foreach (PageNode pageNode in (IEnumerable<PageNode>) context.GetAll<PageNode>())
            pageNode.AllowParametersValidation = true;
          context.SaveChanges();
          Log.Write((object) string.Format("PASSED : {0}", (object) "OpenAccessPageProvider : Upgrad AllowParameterValidation"), ConfigurationPolicy.UpgradeTrace);
        }
        catch (Exception ex)
        {
          Log.Write((object) string.Format("FAILED: {0} - {1}", (object) "OpenAccessPageProvider : Upgrade AllowParameterValidation", (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
      if (upgradedFromSchemaVersionNumber < SitefinityVersion.Sitefinity6_2.Build)
      {
        string str = "OpenAccessPageProvider: Delete orphaned controls and properties";
        try
        {
          this.BulkDeleteItems<ControlProperty>((OpenAccessContext) context, (Expression<Func<ControlProperty, bool>>) (p => p.Control == default (object) && p.ParentProperty == default (object)));
          this.BulkDeleteItems<PageControl>((OpenAccessContext) context, (Expression<Func<PageControl, bool>>) (c => c.Page == default (object)));
          this.BulkDeleteItems<PageDraftControl>((OpenAccessContext) context, (Expression<Func<PageDraftControl, bool>>) (c => c.Page == default (object)));
          this.BulkDeleteItems<TemplateControl>((OpenAccessContext) context, (Expression<Func<TemplateControl, bool>>) (c => c.Page == default (object)));
          this.BulkDeleteItems<TemplateDraftControl>((OpenAccessContext) context, (Expression<Func<TemplateDraftControl, bool>>) (c => c.Page == default (object)));
          Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
        }
        catch (Exception ex)
        {
          Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
      if (upgradedFromSchemaVersionNumber < SitefinityVersion.Sitefinity7_0.Build)
      {
        SitefinityMetadataSourceBase metaDataSource = this.GetMetaDataSource(context.DatabaseContext) as SitefinityMetadataSourceBase;
        new PageUpgrader().Upgrade(context, metaDataSource, upgradedFromSchemaVersionNumber);
        string str = "OpenAccessPageProvider: Update of pages personalization status";
        try
        {
          IQueryable<PageData> all = context.GetAll<PageData>();
          IQueryable<PageData> queryable = all.Where<PageData>((Expression<Func<PageData, bool>>) (p => p.IsPersonalized));
          List<Guid> pagesIdForFlagChange = new List<Guid>();
          foreach (PageData pageData in (IEnumerable<PageData>) queryable)
          {
            PageData personalizedPage = pageData;
            if (!all.Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == personalizedPage.Id)).Any<PageData>())
              pagesIdForFlagChange.Add(personalizedPage.Id);
          }
          if (pagesIdForFlagChange.Count > 0)
            all.Where<PageData>((Expression<Func<PageData, bool>>) (p => pagesIdForFlagChange.Contains(p.Id))).UpdateAll<PageData>((Expression<Func<Telerik.OpenAccess.ExtensionMethods.UpdateDescription<PageData>, Telerik.OpenAccess.ExtensionMethods.UpdateDescription<PageData>>>) (u => u.Set<bool>((Expression<Func<PageData, bool>>) (p => p.IsPersonalized), (Expression<Func<PageData, bool>>) (p => false))));
          Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
        }
        catch (Exception ex)
        {
          context.ClearChanges();
          Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
      if (upgradedFromSchemaVersionNumber < SitefinityVersion.Sitefinity7_3_HF2.Build)
      {
        string str = "OpenAccessPageProvider: Upgrade to {0} -  Update of control properties flags".Arrange((object) SitefinityVersion.Sitefinity7_3_HF2.Build);
        try
        {
          new ControlPropertiesFlagsUpgrader().Upgrade(context, SitefinityVersion.Sitefinity7_3_HF2.Build);
          context.SaveChanges();
          Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
        }
        catch (Exception ex)
        {
          context.ClearChanges();
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
      if (upgradedFromSchemaVersionNumber >= 5715)
        return;
      try
      {
        context.GetAll<PageUrlData>().Where<PageUrlData>((Expression<Func<PageUrlData, bool>>) (u => !u.Node.AllowMultipleUrls && !u.IsDefault)).DeleteAll();
        context.GetAll<PageNode>().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => !p.AllowMultipleUrls)).UpdateAll<PageNode>((Expression<Func<Telerik.OpenAccess.ExtensionMethods.UpdateDescription<PageNode>, Telerik.OpenAccess.ExtensionMethods.UpdateDescription<PageNode>>>) (u => u.Set<bool>((Expression<Func<PageNode, bool>>) (p => p.AllowMultipleUrls), (Expression<Func<PageNode, bool>>) (p => true))));
      }
      catch (Exception ex)
      {
        context.ClearChanges();
        Log.Write((object) string.Format("FAILED: Delete orphaned page url data. Actual error: {0}", (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    private void BulkDeleteItems<TItem>(
      OpenAccessContext context,
      Expression<Func<TItem, bool>> expression,
      int pageSize = 20)
    {
      IQueryable<TItem> source = context.GetAll<TItem>().Where<TItem>(expression);
      int num1 = Queryable.Count<TItem>(source);
      if (num1 <= 0)
        return;
      int num2 = (int) Math.Ceiling((double) num1 / (double) pageSize);
      for (int index = 0; index < num2; ++index)
      {
        foreach (TItem entity in (IEnumerable<TItem>) Queryable.Take<TItem>(source, pageSize))
          context.Delete((object) entity);
        context.FlushChanges();
      }
      context.SaveChanges();
    }

    /// <inheritdoc />
    public void OnSchemaUpgrade(
      OpenAccessConnection oaConnection,
      ISchemaHandler schemaHandler,
      int upgradedFromSchemaVersionNumber)
    {
      if (upgradedFromSchemaVersionNumber >= 4110 || oaConnection.DbType != DatabaseType.MySQL)
        return;
      string ddl = "ALTER TABLE `sf_url_data` ADD INDEX `idx_sf_url_data`(`url`);";
      try
      {
        schemaHandler.ForceExecuteDDLScript(ddl);
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(new Exception("Failed to create index idx_sf_url_data' on columns ('url') of 'sf_url_data' table : {0}".Arrange((object) ex.Message), ex), ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    /// <summary>Creates a language data item</summary>
    /// <returns>The language data.</returns>
    public override LanguageData CreateLanguageData() => this.CreateLanguageData(this.GetNewGuid());

    /// <summary>Creates a language data item</summary>
    /// <param name="id">The id.</param>
    /// <returns>The language data.</returns>
    public override LanguageData CreateLanguageData(Guid id)
    {
      LanguageData entity = new LanguageData(this.ApplicationName, id);
      ((IDataItem) entity).Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets language data item by its id</summary>
    /// <param name="id">The id.</param>
    /// <returns>The language data.</returns>
    public override LanguageData GetLanguageData(Guid id)
    {
      LanguageData languageData = !(id == Guid.Empty) ? this.GetContext().GetItemById<LanguageData>(id.ToString()) : throw new ArgumentException("Argument 'id' cannot be empty GUID.");
      ((IDataItem) languageData).Provider = (object) this;
      return languageData;
    }

    /// <summary>Gets a query of all language data items</summary>
    /// <returns>A query of all language data items.</returns>
    public override IQueryable<LanguageData> GetLanguageData()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<LanguageData>((DataProviderBase) this).Where<LanguageData>((Expression<Func<LanguageData, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>
    /// Demand a permission for a secured object. Unit testing helper. Not testable by RhinoMocks
    /// </summary>
    /// <param name="forObject">Secured object to demand permissions for</param>
    /// <param name="setName">Required permission set</param>
    /// <param name="actions">Required security actions</param>
    protected internal virtual void DemandPermission(
      ISecuredObject forObject,
      string setName,
      params string[] actions)
    {
      forObject.Demand(setName, actions);
    }

    /// <summary>
    /// Demand layout element or control permission, depending on the type of control and its IsLayout property. Unit testing helper.
    /// </summary>
    /// <param name="forControl">Control to demand permissions for.</param>
    protected internal virtual void DemandDeleteControlOrLayoutPermission(ObjectData forControl)
    {
      ISecuredObject forObject1 = forControl as ISecuredObject;
      if (forControl is ControlData forObject2 && forObject2.IsLayoutControl)
      {
        this.DemandPermission((ISecuredObject) forObject2, "LayoutElement", "DeleteLayout");
      }
      else
      {
        if (forObject1 == null)
          return;
        this.DemandPermission(forObject1, "Controls", "DeleteControl");
      }
    }

    /// <summary>Demand view permission for a control or a layout</summary>
    /// <param name="forControl">Control to require permission for</param>
    protected internal virtual void DemandViewControlOrLayoutPermission(object forControl)
    {
      ISecuredObject forObject1 = forControl as ISecuredObject;
      if (forControl is ControlData forObject2 && forObject2.IsLayoutControl)
      {
        this.DemandPermission((ISecuredObject) forObject2, "LayoutElement", "ViewLayout");
      }
      else
      {
        if (forObject1 == null)
          return;
        this.DemandPermission(forObject1, "Controls", "ViewControl");
      }
    }

    /// <inheritdoc />
    protected override ICollection<IEvent> GetDataEventItems(
      Func<IDataItem, bool> filterPredicate)
    {
      IList dirtyItems = this.GetDirtyItems();
      List<IEvent> source1 = new List<IEvent>();
      SitefinityOAContext transaction = (SitefinityOAContext) this.GetTransaction();
      foreach (object itemInTransaction1 in (IEnumerable) dirtyItems)
      {
        SecurityConstants.TransactionActionType transactionActionType = this.GetDirtyItemStatus(itemInTransaction1);
        bool flag1 = false;
        IDataItem dataItem = itemInTransaction1 as IDataItem;
        SiteItemLink siteItemLink = itemInTransaction1 as SiteItemLink;
        if (siteItemLink != null)
        {
          Type siteItemType = TypeResolutionService.ResolveType(siteItemLink.ItemType);
          IDataItem itemInTransaction2 = dirtyItems.OfType<IDataItem>().Where<IDataItem>((Func<IDataItem, bool>) (i => i.Id == siteItemLink.ItemId && siteItemType.IsAssignableFrom(i.GetType()))).FirstOrDefault<IDataItem>();
          if (itemInTransaction2 == null)
            itemInTransaction2 = this.GetItemOrDefault(siteItemType, siteItemLink.ItemId) as IDataItem;
          else if (this.GetDirtyItemStatus((object) itemInTransaction2) == SecurityConstants.TransactionActionType.Deleted)
            continue;
          if (itemInTransaction2 != null)
          {
            transactionActionType = SecurityConstants.TransactionActionType.Updated;
            dataItem = itemInTransaction2;
          }
        }
        if ((!(dataItem is PageNode) || transactionActionType == SecurityConstants.TransactionActionType.Deleted || !transaction.IsFieldDirty((object) dataItem, "Nodes") || transaction.GetMemberNames((object) dataItem, ObjectState.Dirty).Count<string>() != 1) && dataItem != null && filterPredicate(dataItem) && this.ShouldProcessRecycleBinEvents(dataItem))
        {
          IEvent @event = (IEvent) null;
          Type type = dataItem.GetType();
          bool flag2 = dataItem is PageNode && (dataItem as PageNode).changedProperties.Count > 0;
          bool flag3 = dataItem is PageData;
          PageUrlData itemAsPageUrlData = dataItem as PageUrlData;
          if (itemAsPageUrlData != null && itemAsPageUrlData.Node != null && source1.OfType<PageEvent>().Where<PageEvent>((Func<PageEvent, bool>) (e => e.ItemId == itemAsPageUrlData.Node.Id)).FirstOrDefault<PageEvent>() == null)
            dataItem = (IDataItem) itemAsPageUrlData.Node;
          if (dataItem is IHasTrackingContext context)
          {
            List<string> source2 = new List<string>();
            bool flag4 = false;
            if (dataItem is PageData persistentObject)
            {
              if (persistentObject.NavigationNode != null)
              {
                if (transactionActionType == SecurityConstants.TransactionActionType.Updated && !transaction.IsFieldDirty((object) persistentObject, "Version"))
                  flag3 = false;
                if (transactionActionType == SecurityConstants.TransactionActionType.Deleted && dataItem is PageData pageData && !pageData.VariationTypeKey.IsNullOrEmpty())
                {
                  transactionActionType = SecurityConstants.TransactionActionType.Updated;
                  flag1 = true;
                }
                dataItem = (IDataItem) persistentObject.NavigationNode;
              }
              if (context.HasOperation(OperationStatus.Unpublished))
              {
                flag4 = true;
                context = (IHasTrackingContext) dataItem;
                context.CopyTrackingContext(((IHasTrackingContext) persistentObject).TrackingContext);
              }
            }
            if (!flag1)
            {
              PageTemplate pageTemplate = dataItem as PageTemplate;
              if (context.HasDeletedOperation())
              {
                transactionActionType = SecurityConstants.TransactionActionType.Deleted;
                source2 = context.GetLanguages();
                flag4 = true;
                if (!source2.Any<string>())
                {
                  if (persistentObject != null)
                    source2.Add(persistentObject.Culture);
                  else if (pageTemplate != null)
                    source2.Add(pageTemplate.Culture);
                }
              }
              else if (transactionActionType != SecurityConstants.TransactionActionType.Deleted)
              {
                flag4 = true;
                if (transactionActionType == SecurityConstants.TransactionActionType.None)
                  transactionActionType = SecurityConstants.TransactionActionType.Updated;
                source2 = context.GetLanguages();
                if (!source2.Any<string>() && SystemManager.CurrentContext.AppSettings.Multilingual)
                {
                  if (persistentObject != null)
                  {
                    if (persistentObject.NavigationNode != null && persistentObject.NavigationNode.IsSplitPage)
                      source2.Add(persistentObject.Culture);
                    else
                      source2.Add(SystemManager.CurrentContext.Culture.Name);
                  }
                  else if (dataItem is PageNode)
                    source2.Add(SystemManager.CurrentContext.Culture.Name);
                  else if (pageTemplate != null)
                    source2.Add(pageTemplate.UiCulture ?? SystemManager.CurrentContext.Culture.Name);
                }
              }
            }
            if (flag3 || flag2 || persistentObject == null || !transaction.IsFieldDirty((object) persistentObject, "LockedBy"))
            {
              if (source2.Any<string>((Func<string, bool>) (l => !string.IsNullOrEmpty(l))))
              {
                using (List<string>.Enumerator enumerator = source2.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    string current = enumerator.Current;
                    if (!current.IsNullOrEmpty())
                    {
                      IEvent eventItem = this.CreateEventItem(dataItem, transactionActionType.ToString(), current);
                      if (eventItem is PageEvent)
                      {
                        (eventItem as PageEvent).HasPageDataChanged = flag3;
                        (eventItem as PageEvent).HasPageNodeChanged = flag2;
                        (eventItem as PageEvent).TriggeredByType = type;
                      }
                      source1.Add(eventItem);
                    }
                  }
                  continue;
                }
              }
              else if (flag4)
              {
                @event = this.CreateEventItem(dataItem, transactionActionType.ToString());
                context.Clear();
              }
            }
            else
              continue;
          }
          if (dataItem is PageNode pageNode1 && transactionActionType != SecurityConstants.TransactionActionType.Deleted && @event == null)
            @event = this.CreateEventItem(dataItem, transactionActionType.ToString());
          if (pageNode1 != null && (pageNode1.NodeType == NodeType.Group || pageNode1.NodeType == NodeType.InnerRedirect || pageNode1.NodeType == NodeType.OuterRedirect) && @event == null)
            @event = this.CreateEventItem(dataItem, transactionActionType.ToString());
          if (dataItem is ControlPresentation && @event == null)
            @event = this.CreateEventItem(dataItem, transactionActionType.ToString());
          PageNode pageNode2 = dataItem as PageNode;
          IPropertyChangeDataEvent propertyChangeDataEvent = @event as IPropertyChangeDataEvent;
          if (pageNode2 != null && propertyChangeDataEvent != null)
          {
            PageData pageData = pageNode2.GetPageData();
            if (pageData != null && transaction.IsFieldDirty((object) pageData, "Template"))
            {
              PageTemplate originalValue = transaction.GetOriginalValue<PageTemplate>((object) pageData, "Template");
              PageTemplate template = pageData.Template;
              if (originalValue != null && !originalValue.Equals((object) template) || originalValue == null && template != null)
                propertyChangeDataEvent.ChangedProperties.Add("Template", new PropertyChange()
                {
                  PropertyName = "Template",
                  OldValue = (object) originalValue,
                  NewValue = (object) template
                });
            }
          }
          if (@event != null)
          {
            if (@event is PageEvent)
            {
              (@event as PageEvent).HasPageDataChanged = flag3;
              (@event as PageEvent).HasPageNodeChanged = flag2;
              (@event as PageEvent).TriggeredByType = type;
            }
            source1.Add(@event);
          }
        }
      }
      return (ICollection<IEvent>) source1;
    }

    private bool ShouldProcessRecycleBinEvents(IDataItem dataItem) => !(dataItem is IHasTrackingContext context) || !(dataItem is PageData) || !context.HasOperation(OperationStatus.MovedToRecycleBin) && !context.HasOperation(OperationStatus.MovedToRecycleBinWithParent) && !context.HasOperation(OperationStatus.RestoreFromRecycleBin);

    internal override IEvent CreateEventItem(
      IDataItem dataItem,
      string action,
      string language)
    {
      IEvent @event = base.CreateEventItem(dataItem, action, language);
      if (@event is DataEvent dataEvent)
      {
        if (dataItem is PageNode node)
        {
          PageEvent pageEvent = new PageEvent(node);
          PageData pageData = node.GetPageData();
          if (pageData != null)
          {
            string status = pageData.Status.ToString();
            dataEvent.SetLifecylceStatus(status);
          }
          pageEvent.CoppyFrom(dataEvent);
          @event = (IEvent) pageEvent;
          dataEvent = (DataEvent) pageEvent;
        }
        dataEvent.Visible = true;
        if (string.IsNullOrEmpty(dataEvent.ProviderName))
          dataEvent.ProviderName = this.Name;
        if (!string.IsNullOrEmpty(this.TransactionName))
          dataEvent.TransactionName = this.TransactionName;
      }
      if (dataItem is ILifecycleDataItem lifecycleDataItem && dataItem is PageTemplate)
        @event.SetLifecylceStatus(lifecycleDataItem.Status.ToString());
      return @event;
    }

    /// <inheritdoc />
    bool IDataEventProvider.DataEventsEnabled => true;

    /// <inheritdoc />
    bool IDataEventProvider.ApplyDataEventItemFilter(IDataItem item)
    {
      switch (item)
      {
        case PageNode _:
        case PageData _:
        case PageTemplate _:
        case ControlPresentation _:
          return true;
        default:
          return item is PageUrlData;
      }
    }

    public SitefinityOAContext GetContext(
      string connectionString,
      BackendConfiguration backendConfiguration,
      MetadataContainer metadataContainer)
    {
      return new SitefinityOAContext(connectionString, backendConfiguration, metadataContainer);
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> instance.
    /// </summary>
    /// <returns>The created site item link.</returns>
    public override SiteItemLink CreateSiteItemLink() => MultisiteExtensions.CreateSiteItemLink(this);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> for removal.
    /// </summary>
    /// <param name="link">The item link to delete.</param>
    public override void Delete(SiteItemLink link) => MultisiteExtensions.Delete(this, link);

    /// <summary>Deletes the links for item.</summary>
    /// <param name="item">The item.</param>
    public override void DeleteLinksForItem(IDataItem item) => MultisiteExtensions.DeleteLinksForItem(this, item);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public override IQueryable<SiteItemLink> GetSiteItemLinks() => MultisiteExtensions.GetSiteItemLinks(this);

    /// <summary>
    /// Adds the item link that associates the item with the site.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="item">The item.</param>
    /// <returns>The created SiteItemLink.</returns>
    public override SiteItemLink AddItemLink(Guid siteId, IDataItem item) => MultisiteExtensions.AddItemLink(this, siteId, item);

    /// <summary>Gets the items linked to the specified site.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="siteId">The site id.</param>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public override IQueryable<T> GetSiteItems<T>(Guid siteId) => this.GetSiteItems<T>(siteId, MethodBase.GetCurrentMethod());

    /// <summary>
    /// Gets the URL of the content item for the specified culture.
    /// </summary>
    /// <param name="item">The content item.</param>
    /// <param name="culture">The culture to retrieve the URL for.</param>
    /// <returns>The URL for the item.</returns>
    public override string GetItemUrl(ILocatable item, CultureInfo culture) => item != null ? ((PageNode) item).GetUrl(culture) : throw new ArgumentNullException(nameof (item));
  }
}
