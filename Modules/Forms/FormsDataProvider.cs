// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormsDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>Represents base class for Forms data providers.</summary>
  [ModuleId("A64410F7-2F1E-4068-81D0-E28D864DE323")]
  public abstract class FormsDataProvider : 
    ContentDataProviderBase,
    ILanguageDataProvider,
    IMultisiteEnabledProvider,
    IControlPropertyProvider
  {
    /// <summary>Gets or sets the namespace for forms dynamic types.</summary>
    /// <value>The forms namespace.</value>
    public virtual string FormsNamespace { get; set; }

    /// <summary>Gets the next referral code.</summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <returns>The next referral code.</returns>
    public abstract long GetNextReferralCode(string entryType);

    /// <summary>Sets the next referral code.</summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <param name="value">The next referral code.</param>
    public abstract void SetNextReferralCode(string entryType, long value);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> with the given type.
    /// </summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <returns></returns>
    public abstract FormEntry CreateFormEntry(string entryType);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> with the given type and id.
    /// </summary>
    /// <param name="entryTypeName">The name of the type of the entry.</param>
    /// <param name="entryId">The entry id.</param>
    /// <returns></returns>
    public abstract FormEntry CreateFormEntry(string entryTypeName, Guid entryId);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> for removal.
    /// </summary>
    /// <param name="formEntry">The form entry.</param>
    public abstract void Delete(FormEntry formEntry);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> object with the given type and id.
    /// </summary>
    /// <param name="entryTypeName">The name of the type of the entry.</param>
    /// <param name="entryId">The entry id.</param>
    /// <returns></returns>
    public abstract FormEntry GetFormEntry(string entryTypeName, Guid entryId);

    /// <summary>
    /// Gets the form entries of by the given  <see cref="T:Telerik.Sitefinity.Forms.Model.FormDraft" />
    /// </summary>
    /// <param name="formDescription">The form description.</param>
    /// <returns>
    /// An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> objects
    /// </returns>
    public abstract IQueryable<FormEntry> GetFormEntries(
      FormDescription formDescription);

    /// <summary>Gets the form entries of the given type.</summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <returns>An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> objects</returns>
    public abstract IQueryable<FormEntry> GetFormEntries(string entryType);

    /// <summary>Gets the type of the entry.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <returns></returns>
    public abstract Type GetEntryType(FormDraft formDescription);

    /// <summary>Gets the type of the entry.</summary>
    /// <param name="entryTypeName">Name of the entry type.</param>
    /// <returns></returns>
    public abstract Type GetEntryType(string entryTypeName);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object.
    /// </summary>
    /// <param name="formName">Name of the form. This name is used to construct the actual CLR type of the form entry
    /// and therefore the name should comply to CLR naming rules.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object
    /// </returns>
    [MethodPermission("Forms", new string[] {"Create"})]
    public abstract FormDescription CreateForm(string formName);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object with the given id.
    /// </summary>
    /// <param name="formName">Name of the form. This name is used to construct the actual CLR type of the form entry
    /// and therefore the name should comply to CLR naming rules.</param>
    /// <param name="id">The id.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Forms.Model.FormDraft" /> object
    /// </returns>
    [MethodPermission("Forms", new string[] {"Create"})]
    public abstract FormDescription CreateForm(string formName, Guid id);

    /// <summary>Links the form to a specified site.</summary>
    /// <param name="form">The form.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    [MethodPermission("Forms", new string[] {"Create"})]
    internal abstract SiteItemLink LinkFormToSite(FormDescription form, Guid siteId);

    /// <summary>Gets the links for all forms.</summary>
    /// <returns>The query for SiteItemLink.</returns>
    [MethodPermission("Forms", new string[] {"View"})]
    internal abstract IQueryable<SiteItemLink> GetSiteFormLinks();

    /// <summary>
    /// Deletes the specified <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object.
    /// </summary>
    /// <param name="formDescription">The form description.</param>
    [ParameterPermission("formDescription", "Forms", new string[] {"Delete"})]
    public abstract void Delete(FormDescription formDescription);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Forms.Model.FormDraft" /> object by ID.
    /// </summary>
    /// <param name="formDescriptionId">The form description id.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object with the given ID.</returns>
    [ValuePermission("Forms", new string[] {"View"})]
    public abstract FormDescription GetForm(Guid formDescriptionId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> objects.
    /// </summary>
    /// <returns>An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> objects.</returns>
    [EnumeratorPermission("Forms", new string[] {"View"})]
    public abstract IQueryable<FormDescription> GetForms();

    /// <summary>Gets the forms of a specified site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> objects.</returns>
    [EnumeratorPermission("Forms", new string[] {"View"})]
    internal abstract IQueryable<FormDescription> GetForms(Guid siteId);

    /// <summary>Gets the forms not shared with any site.</summary>
    /// <returns></returns>
    internal abstract IQueryable<FormDescription> GetNotSharedForms();

    /// <summary>Creates new draft page or template.</summary>
    /// <returns>The new control.</returns>
    public abstract FormDraft CreateDraft(string formName);

    /// <summary>Creates new draft or page with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new control.</param>
    /// <returns>The new control.</returns>
    public abstract FormDraft CreateDraft(string formName, Guid id);

    /// <summary>Gets the draft form with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public abstract FormDraft GetDraft(Guid id);

    /// <summary>Gets a query for draft form definitions.</summary>
    /// <returns>The query for controls.</returns>
    public abstract IQueryable<FormDraft> GetDrafts();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    [TypedParameterPermission(typeof (FormDraft), "item", "Forms", new string[] {"Delete"})]
    public abstract void Delete(DraftData item);

    /// <summary>Creates new control.</summary>
    /// <returns>The new control.</returns>
    public abstract T CreateControl<T>(bool isBackendObject = false) where T : ObjectData;

    /// <summary>Creates new control with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new control.</param>
    /// <returns>The new control.</returns>
    public abstract T CreateControl<T>(Guid id, bool isBackendObject = false) where T : ObjectData;

    /// <summary>Gets the control with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public abstract T GetControl<T>(Guid id) where T : ObjectData;

    /// <summary>Gets a query for controls.</summary>
    /// <returns>The query for controls.</returns>
    public abstract IQueryable<T> GetControls<T>() where T : ObjectData;

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public abstract void Delete(ObjectData item);

    /// <summary>Creates new page template.</summary>
    /// <returns>The new page template.</returns>
    public abstract ControlProperty CreateProperty();

    /// <summary>Creates new page template with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new page template.</param>
    /// <returns>The new page template.</returns>
    public abstract ControlProperty CreateProperty(Guid id);

    /// <summary>Gets the page template with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>A page template.</returns>
    public abstract ControlProperty GetProperty(Guid id);

    /// <summary>Gets a query for page templates.</summary>
    /// <returns>The query for page templates.</returns>
    public abstract IQueryable<ControlProperty> GetProperties();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public abstract void Delete(ControlProperty item);

    /// <summary>
    /// Creates new object for storing presentation information.
    /// </summary>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public abstract T CreatePresentationItem<T>() where T : PresentationData;

    /// <summary>
    /// Creates new object for storing presentation information with the specified ID.
    /// </summary>
    /// <param name="id">The id of the new item.</param>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public abstract T CreatePresentationItem<T>(Guid id) where T : PresentationData;

    /// <summary>Links the template to site.</summary>
    /// <param name="template">The template.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal abstract SiteItemLink LinkPresentationItemToSite(
      PresentationData presentationData,
      Guid siteId);

    /// <summary>Gets the item with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public abstract T GetPresentationItem<T>(Guid id) where T : PresentationData;

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items.
    /// </summary>
    /// <returns>The query for page templates.</returns>
    public abstract IQueryable<T> GetPresentationItems<T>() where T : PresentationData;

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items.
    /// </summary>
    /// <returns>The query for page templates.</returns>
    internal abstract IQueryable<T> GetPresentationItems<T>(Guid siteId) where T : PresentationData;

    /// <summary>Gets the links for all presentation items.</summary>
    /// <returns>The query for SiteItemLink.</returns>
    internal abstract IQueryable<SiteItemLink> GetSitePresentationItemLinks<T>() where T : PresentationData;

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public abstract void Delete(PresentationData item);

    /// <summary>Commits the provided transaction.</summary>
    [TransactionPermission(typeof (FormDraft), "Forms", SecurityConstants.TransactionActionType.Updated, new string[] {"Modify"})]
    public override void CommitTransaction() => base.CommitTransaction();

    /// <summary>
    /// Flush all dirty and new instances to the database and evict all instances from the local cache.
    /// </summary>
    [TransactionPermission(typeof (FormDraft), "Forms", SecurityConstants.TransactionActionType.Updated, new string[] {"Modify"})]
    public override void FlushTransaction() => base.FlushTransaction();

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (FormsDataProvider);

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (typeof (FormEntry).IsAssignableFrom(itemType))
        return (object) this.CreateFormEntry(itemType.FullName, id);
      if (itemType == typeof (LanguageData))
        return (object) this.CreateLanguageData(id);
      if (itemType == typeof (FormControl))
        return (object) this.CreateControl<FormControl>(id);
      if (itemType == typeof (FormDraftControl))
        return (object) this.CreateControl<FormDraftControl>(id);
      if (itemType == typeof (ControlProperty))
        return (object) this.CreateProperty(id);
      if (itemType == typeof (ObjectData))
        return (object) this.CreateControl<ObjectData>(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (typeof (FormEntry).IsAssignableFrom(itemType))
        return (object) this.GetFormEntry(itemType.FullName, id);
      if (typeof (FormDescription).IsAssignableFrom(itemType))
        return (object) this.GetForm(id);
      if (typeof (FormControl).IsAssignableFrom(itemType))
        return (object) this.GetControl<FormControl>(id);
      if (typeof (FormDraftControl).IsAssignableFrom(itemType))
        return (object) this.GetControl<FormDraftControl>(id);
      if (typeof (ControlProperty).IsAssignableFrom(itemType))
        return (object) this.GetProperty(id);
      return typeof (ObjectData).IsAssignableFrom(itemType) ? (object) this.GetControl<ObjectData>(id) : base.GetItem(itemType, id);
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (typeof (FormEntry).IsAssignableFrom(itemType))
        return (object) this.GetFormEntries(itemType.FullName).Where<FormEntry>((Expression<Func<FormEntry, bool>>) (f => f.Id == id)).FirstOrDefault<FormEntry>();
      if (!typeof (FormDescription).IsAssignableFrom(itemType))
        return base.GetItemOrDefault(itemType, id);
      return (object) this.GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Id == id)).FirstOrDefault<FormDescription>();
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Comment))
        return (IEnumerable) DataProviderBase.SetExpressions<Comment>(this.GetComments(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (FormEntry).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<FormEntry>(this.GetFormEntries(itemType.ToString()), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (FormDraft))
        return (IEnumerable) DataProviderBase.SetExpressions<FormDraft>(this.GetDrafts(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (FormDescription).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<FormDescription>(this.GetForms(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (FormControl).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<FormControl>(this.GetControls<FormControl>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (LanguageData).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<LanguageData>(this.GetLanguageData(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      Type c = item != null ? item.GetType() : throw new ArgumentNullException(nameof (item));
      this.providerDecorator.DeletePermissions(item);
      if (typeof (FormEntry).IsAssignableFrom(c))
        this.Delete((FormEntry) item);
      else if (c == typeof (FormDraft))
        this.Delete((DraftData) item);
      else if (c == typeof (FormControl))
        this.Delete((ObjectData) item);
      else if (c == typeof (FormDraftControl))
        this.Delete((ObjectData) item);
      else if (c == typeof (FormDescription))
        this.Delete((FormDescription) item);
      else if (typeof (ControlProperty).IsAssignableFrom(c))
      {
        this.Delete((ControlProperty) item);
      }
      else
      {
        if (!typeof (ObjectData).IsAssignableFrom(c))
          throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), this.GetKnownTypes());
        this.Delete((ObjectData) item);
      }
    }

    /// <summary>Get a list of types served by this provider</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => new Type[8]
    {
      typeof (FormEntry),
      typeof (FormDraft),
      typeof (FormControl),
      typeof (LanguageData),
      typeof (FormDraftControl),
      typeof (FormDescription),
      typeof (ControlProperty),
      typeof (ObjectData)
    };

    /// <summary>Initializes the specified provider name.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="config">The configuration collection.</param>
    /// <param name="managerType">Type of the manager.</param>
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      base.Initialize(providerName, config, managerType);
      string str = config["formsNamespace"];
      this.FormsNamespace = string.IsNullOrEmpty(str) ? "Telerik.Sitefinity.DynamicTypes.Model" : str;
    }

    /// <summary>Sets the root permissions.</summary>
    /// <param name="root">The root.</param>
    public override void SetRootPermissions(SecurityRoot root)
    {
      if (root.Permissions != null || root.Permissions.Count > 0)
        root.Permissions.Clear();
      Permission permission1 = this.CreatePermission("Forms", root.Id, SecurityManager.EveryoneRole.Id);
      permission1.GrantActions(false, "View", "ViewResponses");
      root.Permissions.Add(permission1);
      Permission permission2 = this.CreatePermission("Forms", root.Id, SecurityManager.OwnerRole.Id);
      permission2.GrantActions(false, "Modify", "Delete");
      root.Permissions.Add(permission2);
      Permission permission3 = this.CreatePermission("Forms", root.Id, SecurityManager.EditorsRole.Id);
      permission3.GrantActions(false, "Create", "Modify", "Delete", "ChangeOwner");
      root.Permissions.Add(permission3);
      Permission permission4 = this.CreatePermission("Forms", root.Id, SecurityManager.AuthorsRole.Id);
      permission4.GrantActions(false, "Create");
      root.Permissions.Add(permission4);
      Permission permission5 = this.CreatePermission("Forms", root.Id, SecurityManager.BackEndUsersRole.Id);
      permission5.GrantActions(false, "ManageResponses");
      root.Permissions.Add(permission5);
    }

    /// <summary>Creates a language data item</summary>
    /// <returns></returns>
    public abstract LanguageData CreateLanguageData();

    /// <summary>Creates a language data item</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public abstract LanguageData CreateLanguageData(Guid id);

    /// <summary>Gets language data item by its id</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public abstract LanguageData GetLanguageData(Guid id);

    /// <summary>Gets a query of all language data items</summary>
    /// <returns></returns>
    public abstract IQueryable<LanguageData> GetLanguageData();

    /// <summary>Gets the item from URL.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <returns></returns>
    [TypedValuePermission(typeof (Form), "Forms", new string[] {"View"})]
    public override IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      out string redirectUrl)
    {
      return base.GetItemFromUrl(itemType, url, out redirectUrl);
    }

    /// <summary>Gets the item from URL.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="published">The published.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <returns></returns>
    [TypedValuePermission(typeof (Form), "Forms", new string[] {"View"})]
    public override IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl)
    {
      return base.GetItemFromUrl(itemType, url, published, out redirectUrl);
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> instance.
    /// </summary>
    public abstract SiteItemLink CreateSiteItemLink();

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> for removal.
    /// </summary>
    /// <param name="link">The item link to delete.</param>
    public abstract void Delete(SiteItemLink link);

    /// <summary>Deletes the links for item.</summary>
    /// <param name="item">The item.</param>
    public abstract void DeleteLinksForItem(IDataItem item);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public abstract IQueryable<SiteItemLink> GetSiteItemLinks();

    /// <summary>
    /// Adds the item link that associates the item with the site.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="item">The item.</param>
    /// <returns>The created SiteItemLink.</returns>
    public abstract SiteItemLink AddItemLink(Guid siteId, IDataItem item);

    /// <summary>Gets the items linked to the specified site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>
    ///  Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public abstract IQueryable<T> GetSiteItems<T>(Guid siteId) where T : IDataItem;

    void IControlPropertyProvider.DeleteProperty(ControlProperty item) => this.Delete(item);

    void IControlPropertyProvider.DeleteObject(ObjectData item) => this.Delete(item);

    void IControlPropertyProvider.DeletePresentation(PresentationData item) => this.Delete(item);

    internal void Upgrade(int upgradeFrom)
    {
    }
  }
}
