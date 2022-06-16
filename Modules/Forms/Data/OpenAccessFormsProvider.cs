// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Data.OpenAccessFormsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.UI;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Forms.Events;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Forms.Data
{
  /// <summary>
  /// Represents Generic Content data provider that uses OpenAccess to store and retrieve content data.
  /// </summary>
  [ContentProviderDecorator(typeof (OpenAccessContentDecorator))]
  public class OpenAccessFormsProvider : 
    FormsDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IOpenAccessUpgradableProvider,
    IDataEventProvider,
    IMultisiteEnabledOAProvider
  {
    private string[] supportedPermissionSets = new string[2]
    {
      "Forms",
      "SitemapGeneration"
    };
    private ICounterDecorator counterDecorator;

    /// <summary>Gets the next referral code.</summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <returns>The next referral code.</returns>
    public override long GetNextReferralCode(string entryType) => this.CounterDecorator.GetNextValue(entryType);

    /// <summary>Sets the next referral code.</summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <param name="value">The next referral code.</param>
    public override void SetNextReferralCode(string entryType, long value) => this.CounterDecorator.InitCounter(entryType, value - 1L);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> with the given type.
    /// </summary>
    /// <param name="entryTypeName">The name of the type of the entry.</param>
    /// <returns></returns>
    public override FormEntry CreateFormEntry(string entryTypeName) => this.CreateFormEntry(entryTypeName, this.GetNewGuid());

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> with the given type and id.
    /// </summary>
    /// <param name="entryTypeName">The name of the type of the entry.</param>
    /// <param name="entryId">The entry id.</param>
    /// <returns></returns>
    public override FormEntry CreateFormEntry(string entryTypeName, Guid entryId)
    {
      FormEntry instance = (FormEntry) (this.GetContext().PersistentMetaData.GetPersistentTypeDescriptor(entryTypeName) ?? throw new ArgumentNullException("No persistent type with name " + entryTypeName)).CreateInstance((object) null);
      this.GetContext().Add((object) instance);
      instance.ApplicationName = this.ApplicationName;
      instance.Id = entryId;
      instance.DateCreated = DateTime.UtcNow;
      instance.Owner = SecurityManager.GetCurrentUserId();
      instance.PublicationDate = DateTime.UtcNow;
      instance.StartedOn = DateTime.UtcNow;
      instance.SubmittedOn = DateTime.UtcNow;
      instance.SourceSiteId = SystemManager.CurrentContext.CurrentSite.Id;
      instance.SourceSiteName = instance.SourceSiteDisplayName = SystemManager.CurrentContext.CurrentSite.Name;
      ((IDataItem) instance).Provider = (object) this;
      return instance;
    }

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> object with the given type and id.
    /// </summary>
    /// <param name="entryTypeName">The name of the type of the entry.</param>
    /// <param name="entryId">The entry id.</param>
    /// <returns></returns>
    public override FormEntry GetFormEntry(string entryTypeName, Guid entryId)
    {
      if (entryId == Guid.Empty)
        throw new ArgumentException("Id cannot be Empty Guid");
      Type entryType = this.GetEntryType(entryTypeName);
      FormEntry formEntry = this.GetFormEntries(entryTypeName).FirstOrDefault<FormEntry>((Expression<Func<FormEntry, bool>>) (x => x.Id == entryId));
      if (formEntry == null)
        throw new ItemNotFoundException(Res.Get<ErrorMessages>().ItemNotFound.Arrange((object) entryType.Name, (object) entryId));
      ((IDataItem) formEntry).Provider = (object) this;
      return formEntry;
    }

    /// <summary>
    /// Gets the form entries of by the given  <see cref="T:Telerik.Sitefinity.Forms.Model.FormDraft" />
    /// </summary>
    /// <param name="formDescription">The form description.</param>
    /// <returns>
    /// An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> objects
    /// </returns>
    public override IQueryable<FormEntry> GetFormEntries(
      FormDescription formDescription)
    {
      return this.GetFormEntries(string.Format("{0}.{1}", (object) this.FormsNamespace, (object) formDescription.Name));
    }

    /// <summary>Gets the form entries of the given type.</summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <returns>
    /// An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> objects
    /// </returns>
    public override IQueryable<FormEntry> GetFormEntries(string entryType)
    {
      this.AuthorizeFormEntries(entryType, "ViewResponses");
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<FormEntry>((this.GetContext().PersistentMetaData.GetPersistentTypeDescriptor(entryType) ?? throw new ArgumentException("There is no persitent type with the name: " + entryType)).DescribedType, (DataProviderBase) this).Where<FormEntry>((Expression<Func<FormEntry, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> for removal.
    /// </summary>
    /// <param name="formEntry">The form entry.</param>
    public override void Delete(FormEntry formEntry)
    {
      this.AuthorizeFormEntries(formEntry.GetType().FullName, "ManageResponses");
      this.GetContext()?.Remove((object) formEntry);
    }

    /// <summary>Gets the type of the entry.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <returns></returns>
    public override Type GetEntryType(FormDraft formDescription)
    {
      if (formDescription == null)
        throw new ArgumentNullException(nameof (formDescription));
      return this.GetEntryType(string.Format("{0}.{1}", (object) this.FormsNamespace, (object) formDescription.Name));
    }

    /// <summary>Gets the type of the entry.</summary>
    /// <param name="entryTypeName">Name of the entry type.</param>
    /// <returns></returns>
    public override Type GetEntryType(string entryTypeName) => (this.GetContext().PersistentMetaData.GetPersistentTypeDescriptor(entryTypeName) ?? throw new ArgumentNullException("No persistent type with name: " + entryTypeName)).DescribedType;

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object.
    /// </summary>
    /// <param name="formName">Name of the form. This name is used to construct the actual CLR type of the form entry
    /// and therefore the name should comply to CLR naming rules.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object
    /// </returns>
    public override FormDescription CreateForm(string formName) => this.CreateForm(formName, this.GetNewGuid());

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object with the given id.
    /// </summary>
    /// <param name="formName">Name of the form. This name is used to construct the actual CLR type of the form entry
    /// and therefore the name should comply to CLR naming rules.</param>
    /// <param name="id">The id.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Forms.Model.FormDraft" /> object
    /// </returns>
    public override FormDescription CreateForm(string formName, Guid id)
    {
      if (string.IsNullOrEmpty(formName))
        throw new ArgumentNullException(nameof (formName));
      FormDescription form = new FormDescription(this.ApplicationName, formName, id);
      form.DateCreated = DateTime.UtcNow;
      form.Owner = SecurityManager.GetCurrentUserId();
      form.Provider = (object) this;
      this.providerDecorator.CreatePermissionInheritanceAssociation(this.GetSecurityRoot() ?? throw new InvalidOperationException(string.Format(Res.Get<SecurityResources>().NoSecurityRoot, (object) typeof (FormDescription).AssemblyQualifiedName)), (ISecuredObject) form);
      if (id != Guid.Empty)
        this.GetContext().Add((object) form);
      return form;
    }

    /// <summary>Links the form to a specified site.</summary>
    /// <param name="form">The form.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal override SiteItemLink LinkFormToSite(FormDescription form, Guid siteId) => this.AddItemLink(siteId, (IDataItem) form);

    /// <summary>Gets the links for all forms.</summary>
    /// <returns>The query for SiteItemLink.</returns>
    internal override IQueryable<SiteItemLink> GetSiteFormLinks() => this.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemType == typeof (FormDescription).FullName));

    /// <summary>
    /// Deletes the specified <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object.
    /// </summary>
    /// <param name="form">The form description.</param>
    public override void Delete(FormDescription formDescription)
    {
      this.CounterDecorator.DeleteCounter(formDescription.EntriesTypeName);
      this.DeleteLinksForItem((IDataItem) formDescription);
      this.GetContext()?.Remove((object) formDescription);
    }

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object by ID.
    /// </summary>
    /// <param name="formId">The form description id.</param>
    /// <returns>
    /// The <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object with the given ID.
    /// </returns>
    public override FormDescription GetForm(Guid formId)
    {
      FormDescription form = !(formId == Guid.Empty) ? this.GetContext().GetItemById<FormDescription>(formId.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      form.Provider = (object) this;
      return form;
    }

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> objects.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> objects.
    /// </returns>
    public override IQueryable<FormDescription> GetForms()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<FormDescription>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<FormDescription>((Expression<Func<FormDescription, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Gets the forms of a specified site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> objects.</returns>
    internal override IQueryable<FormDescription> GetForms(Guid siteId) => this.GetSiteItems<FormDescription>(siteId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> objects not shared with any site.
    /// </summary>
    /// <returns></returns>
    internal override IQueryable<FormDescription> GetNotSharedForms() => this.GetNotLinkedItems<FormDescription>();

    /// <summary>Creates new form draft.</summary>
    /// <returns>The new control.</returns>
    public override FormDraft CreateDraft(string formName) => this.CreateDraft(formName, this.GetNewGuid());

    /// <summary>Creates new form draft or page with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new control.</param>
    /// <returns>The new control.</returns>
    public override FormDraft CreateDraft(string formName, Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("Id cannot be an Empty Guid");
      DraftData entity = (DraftData) new FormDraft(this.ApplicationName, formName, id);
      entity.Owner = SecurityManager.GetCurrentUserId();
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return (FormDraft) entity;
    }

    /// <summary>
    /// Gets the draft page or template with the specified ID.
    /// </summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public override FormDraft GetDraft(Guid id)
    {
      FormDraft draft = !(id == Guid.Empty) ? this.GetContext().GetItemById<FormDraft>(id.ToString()) : throw new ArgumentException("Id cannot be an Empty Guid");
      ((IDataItem) draft).Provider = (object) this;
      return draft;
    }

    /// <summary>Gets a query for draft pages or templates.</summary>
    /// <returns>The query for controls.</returns>
    public override IQueryable<FormDraft> GetDrafts()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<FormDraft>((DataProviderBase) this).Where<FormDraft>((Expression<Func<FormDraft, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(DraftData item)
    {
      this.providerDecorator.DeletePermissions((object) item);
      this.GetContext().Remove((object) item);
    }

    /// <summary>Creates new control.</summary>
    /// <returns>The new control.</returns>
    public override T CreateControl<T>(bool isBackendObject = false) => this.CreateControl<T>(this.GetNewGuid(), isBackendObject);

    /// <summary>Creates new control with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new control.</param>
    /// <returns>The new control.</returns>
    public override T CreateControl<T>(Guid id, bool isBackendObject = false)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("Id cannot be an Empty Guid");
      SitefinityOAContext context = this.GetContext();
      Type type = typeof (T);
      ObjectData entity;
      if (typeof (FormControl) == type)
        entity = (ObjectData) new FormControl(this.ApplicationName, id);
      else if (typeof (FormDraftControl) == type)
        entity = (ObjectData) new FormDraftControl(this.ApplicationName, id);
      else if (typeof (ObjectData) == type)
        entity = new ObjectData(this.ApplicationName, id, isBackendObject);
      else
        throw new ArgumentException("Invalid Type \"{0}\".".Arrange((object) typeof (T).FullName));
      ((IDataItem) entity).Provider = (object) this;
      if (entity is IOwnership ownership)
        ownership.Owner = SecurityManager.GetCurrentUserId();
      context.Add((object) entity);
      return (T) entity;
    }

    /// <summary>Gets the control with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public override T GetControl<T>(Guid id)
    {
      T control = !(id == Guid.Empty) ? this.GetContext().GetItemById<T>(id.ToString()) : throw new ArgumentException("Id cannot be an Empty Guid");
      control.Provider = (object) this;
      return control;
    }

    /// <summary>Gets a query for controls.</summary>
    /// <returns>The query for controls.</returns>
    public override IQueryable<T> GetControls<T>()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<T>((DataProviderBase) this).Where<T>((Expression<Func<T, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public override void Delete(ObjectData item) => this.GetContext().Remove((object) item);

    /// <summary>Creates new page template.</summary>
    /// <returns>The new page template.</returns>
    public override ControlProperty CreateProperty() => this.CreateProperty(this.GetNewGuid());

    /// <summary>Creates new page template with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new page template.</param>
    /// <returns>The new page template.</returns>
    public override ControlProperty CreateProperty(Guid id)
    {
      ControlProperty entity = !(id == Guid.Empty) ? new ControlProperty(this.ApplicationName, id) : throw new ArgumentException("Id cannot be an Empty Guid");
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the page template with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
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
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public override T CreatePresentationItem<T>() => this.CreatePresentationItem<T>(this.GetNewGuid());

    /// <summary>
    /// Creates new object for storing presentation information with the specified ID.
    /// </summary>
    /// <param name="pageId">The pageId of the new item.</param>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public override T CreatePresentationItem<T>(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("Id cannot be an Empty Guid");
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
      else if (typeof (FormPresentation) == type)
        entity = (PresentationData) new FormPresentation(this.ApplicationName, id);
      else
        throw new ArgumentException("Invalid Type \"{0}\".".Arrange((object) typeof (T).FullName));
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return (T) entity;
    }

    /// <summary>Links the template to site.</summary>
    /// <param name="template">The template.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal override SiteItemLink LinkPresentationItemToSite(
      PresentationData presentationData,
      Guid siteId)
    {
      return this.AddItemLink(siteId, (IDataItem) presentationData);
    }

    /// <summary>Gets the item with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
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
    /// <returns>The query for page templates.</returns>
    public override IQueryable<T> GetPresentationItems<T>()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<T>((DataProviderBase) this).Where<T>((Expression<Func<T, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Gets the links for all presentation items.</summary>
    /// <returns>The query for SiteItemLink.</returns>
    internal override IQueryable<SiteItemLink> GetSitePresentationItemLinks<T>() => this.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemType == typeof (T).FullName));

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items.
    /// </summary>
    /// <returns>The query for page templates.</returns>
    internal override IQueryable<T> GetPresentationItems<T>(Guid siteId) => this.GetSiteItems<T>(siteId);

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(PresentationData item)
    {
      this.providerDecorator.DeletePermissions((object) item);
      this.GetContext().Remove((object) item);
    }

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new FormsMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>Gets the items by taxon.</summary>
    /// <param name="taxonId">The taxon id.</param>
    /// <param name="isSingleTaxon">A value indicating if it is a single taxon.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">Items to skip.</param>
    /// <param name="take">Items to take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns></returns>
    public override IEnumerable GetItemsByTaxon(
      Guid taxonId,
      bool isSingleTaxon,
      string propertyName,
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the actual type of the <see cref="!:UrlData" /> implementation for the specified content type.
    /// </summary>
    /// <param name="itemType">Type of the content item.</param>
    /// <returns></returns>
    public override Type GetUrlTypeFor(Type itemType) => throw new NotImplementedException();

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    /// <value>The supported permission sets.</value>
    public override string[] SupportedPermissionSets
    {
      get => this.supportedPermissionSets;
      set => this.supportedPermissionSets = value;
    }

    /// <summary>Creates a language data item</summary>
    /// <returns></returns>
    public override LanguageData CreateLanguageData() => this.CreateLanguageData(this.GetNewGuid());

    /// <summary>Creates a language data item</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
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
    /// <returns></returns>
    public override LanguageData GetLanguageData(Guid id)
    {
      LanguageData languageData = !(id == Guid.Empty) ? this.GetContext().GetItemById<LanguageData>(id.ToString()) : throw new ArgumentException("Argument 'id' cannot be empty GUID.");
      ((IDataItem) languageData).Provider = (object) this;
      return languageData;
    }

    /// <summary>Gets a query of all language data items</summary>
    /// <returns></returns>
    public override IQueryable<LanguageData> GetLanguageData()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<LanguageData>((DataProviderBase) this).Where<LanguageData>((Expression<Func<LanguageData, bool>>) (c => c.ApplicationName == appName));
    }

    /// <inheritdoc />
    public int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    /// <inheritdoc />
    public void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
      if (upgradingFromSchemaVersionNumber <= 0 || upgradingFromSchemaVersionNumber >= 1600)
        return;
      OpenAccessConnection.OracleUpgradeLStringColumn(context, "sf_form_description", "success_message_", "NCLOB", "OpenAccessFormsProvider: Upgrade from < 1600. sf_form_description success_message_ to NCLOB");
      OpenAccessConnection.OracleUpgradeLStringColumn(context, "sf_draft_pages", "success_message_", "NCLOB", "OpenAccessFormsProvider: Upgrade from < 1600. sf_draft_pages success_message_ to NCLOB");
    }

    /// <inheritdoc />
    public void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
      if (context == null || upgradedFromSchemaVersionNumber >= 5300)
        return;
      string upgradeScript1 = context.DatabaseContext.DatabaseType == DatabaseType.Oracle ? "UPDATE \"sf_draft_pages\" SET \"submit_action_after_update\" = 0 WHERE \"submit_action_after_update\" is NULL" : "UPDATE sf_draft_pages SET submit_action_after_update = 0 WHERE submit_action_after_update is NULL";
      string upgradeScript2 = context.DatabaseContext.DatabaseType == DatabaseType.Oracle ? "UPDATE \"sf_draft_pages\" SET \"submit_action\" = 0 WHERE \"submit_action\" is NULL" : "UPDATE sf_draft_pages SET submit_action = 0 WHERE submit_action is NULL";
      string upgradeScript3 = context.DatabaseContext.DatabaseType == DatabaseType.Oracle ? "UPDATE \"sf_draft_pages\" SET \"submit_restriction\" = 0 WHERE \"submit_restriction\" is NULL" : "UPDATE sf_draft_pages SET submit_restriction = 0 WHERE submit_restriction is NULL";
      OpenAccessConnection.Upgrade(context, "OpenAccessFormsProvider: Upgrade from < 5300. sf_draft_pages submit_action_after_update from NULL to 0", upgradeScript1);
      OpenAccessConnection.Upgrade(context, "OpenAccessFormsProvider: Upgrade from < 5300. sf_draft_pages submit_action from NULL to 0", upgradeScript2);
      OpenAccessConnection.Upgrade(context, "OpenAccessFormsProvider: Upgrade from < 5300. sf_draft_pages submit_restriction from NULL to 0", upgradeScript3);
    }

    /// <summary>Gets the counter decorator.</summary>
    /// <value>The counter decorator.</value>
    protected internal ICounterDecorator CounterDecorator
    {
      get
      {
        if (this.counterDecorator == null)
          this.counterDecorator = this.CreateCounterDecorator();
        return this.counterDecorator;
      }
    }

    /// <summary>Creates a new counter decorator.</summary>
    /// <returns>The new counter decorator.</returns>
    protected virtual ICounterDecorator CreateCounterDecorator() => (ICounterDecorator) new OpenAccessCounterDecorator((IOpenAccessDataProvider) this);

    protected override ICollection<IEvent> GetDataEventItems(
      Func<IDataItem, bool> filterPredicate)
    {
      ICollection<IEvent> dataEventItems = base.GetDataEventItems(filterPredicate);
      IList dirtyItems = this.GetDirtyItems();
      IFormEntryCreatedEvent entryCreatedEvent = this.GetFormEntryCreatedEvent(dirtyItems);
      if (entryCreatedEvent != null)
      {
        entryCreatedEvent.Origin = this.GetEventOrigin();
        dataEventItems.Add((IEvent) entryCreatedEvent);
      }
      IFormEntryUpdatedEvent entryUpdatedEvent = this.GetFormEntryUpdatedEvent(dirtyItems);
      if (entryUpdatedEvent != null)
      {
        entryUpdatedEvent.Origin = this.GetEventOrigin();
        dataEventItems.Add((IEvent) entryUpdatedEvent);
      }
      return dataEventItems;
    }

    /// <inheritdoc />
    bool IDataEventProvider.DataEventsEnabled => true;

    /// <inheritdoc />
    bool IDataEventProvider.ApplyDataEventItemFilter(IDataItem item) => item is FormDescription;

    protected virtual IFormEntryCreatedEvent GetFormEntryCreatedEvent(
      IList dirtyItems)
    {
      return (IFormEntryCreatedEvent) this.GetFormEntryEventInternal<FormEntryCreatedEvent>(dirtyItems, SecurityConstants.TransactionActionType.New);
    }

    protected virtual IFormEntryUpdatedEvent GetFormEntryUpdatedEvent(
      IList dirtyItems)
    {
      return (IFormEntryUpdatedEvent) this.GetFormEntryEventInternal<FormEntryUpdatedEvent>(dirtyItems, SecurityConstants.TransactionActionType.Updated);
    }

    internal static IEnumerable<IFormEntryEventControl> GetEntryControls(
      FormEntry formEntry,
      FormDescription formDescription,
      string providerName = null,
      bool getEntryControlsOriginalValue = false)
    {
      List<IFormEntryEventControl> entryControls = new List<IFormEntryEventControl>();
      FormsManager manager = FormsManager.GetManager(providerName);
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) formEntry);
      foreach (FormControl sortFormControl in OpenAccessFormsProvider.SortFormControls((IEnumerable<ControlData>) formDescription.Controls))
      {
        FormEntryEventControlType? nullable = new FormEntryEventControlType?();
        FormEntryEventControl entryEventControl = new FormEntryEventControl()
        {
          Id = sortFormControl.Id,
          SiblingId = sortFormControl.SiblingId
        };
        CultureInfo culture = SystemManager.CurrentContext.AppSettings.Multilingual ? CultureInfo.GetCultureInfo(formEntry.Language ?? SystemManager.CurrentContext.Culture.Name) : SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
        Control control = manager.LoadControl((ObjectData) sortFormControl, culture);
        IFieldControl fieldControl = control as IFieldControl;
        IFormFieldControl behaviorObject = ControlUtilities.BehaviorResolver.GetBehaviorObject(control) as IFormFieldControl;
        IEnumerable<ControlProperty> persistedProperties = ControlUtilities.BehaviorResolver.GetPersistedProperties((ControlData) sortFormControl);
        entryEventControl.ControlType = sortFormControl?.Caption;
        entryEventControl.ClientID = control?.ID;
        if (behaviorObject != null)
        {
          object obj1 = (object) null;
          object obj2 = (object) null;
          nullable = new FormEntryEventControlType?(FormEntryEventControlType.FieldControl);
          IMetaField metaField = behaviorObject.MetaField;
          PropertyDescriptor propertyDescriptor = properties.Find(metaField.FieldName, false);
          if (propertyDescriptor != null)
          {
            object source1 = propertyDescriptor.GetValue((object) formEntry);
            if (metaField.ClrType == typeof (ContentLink[]).FullName)
            {
              nullable = new FormEntryEventControlType?(FormEntryEventControlType.FileFieldControl);
              obj1 = source1;
              if (getEntryControlsOriginalValue)
              {
                try
                {
                  ContentLink contentLink = ((IEnumerable<ContentLink>) (source1 as ContentLink[])).FirstOrDefault<ContentLink>();
                  if (contentLink != null)
                  {
                    Guid parentItemId = contentLink.ParentItemId;
                    IQueryable<ContentLink> source2 = manager.Provider.GetRelatedManager<ContentLinksManager>((string) null).GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (x => x.ParentItemId == parentItemId));
                    if (source2.Any<ContentLink>())
                      obj2 = (object) source2.ToArray<ContentLink>();
                  }
                }
                catch
                {
                }
              }
            }
            else
            {
              obj1 = (object) OpenAccessFormsProvider.GetTextValue(source1, fieldControl);
              if (getEntryControlsOriginalValue)
                obj2 = (object) OpenAccessFormsProvider.GetTextValue(manager.Provider.GetOriginalValue<object>((object) formEntry, metaField.FieldName), fieldControl);
            }
          }
          entryEventControl.Title = fieldControl == null ? metaField.Title ?? metaField.FieldName : fieldControl.Title;
          entryEventControl.FieldName = metaField.FieldName;
          entryEventControl.Value = obj1;
          entryEventControl.OldValue = obj2;
          ControlProperty controlProperty1 = persistedProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "FieldName" && p.Language == formEntry.Language)) ?? persistedProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "FieldName" && p.Language == null));
          if (controlProperty1 == null)
          {
            ControlProperty controlProperty2 = persistedProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Model" && p.Language == null));
            if (controlProperty2 != null)
              controlProperty1 = controlProperty2.ChildProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "TrackingFieldName" && p.Language == null));
          }
          if (controlProperty1 != null)
            entryEventControl.FieldControlName = controlProperty1.Value;
        }
        else if (persistedProperties != null && !persistedProperties.Any<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "MetaField")))
        {
          ControlProperty controlProperty3 = persistedProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Title" && p.Language == formEntry.Language));
          ControlProperty controlProperty4 = persistedProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Model" && p.Language == formEntry.Language));
          if (controlProperty3 == null && controlProperty4 != null && controlProperty4.ChildProperties != null)
            controlProperty3 = controlProperty4.ChildProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Text" && p.Language == formEntry.Language));
          if (controlProperty3 != null)
          {
            nullable = new FormEntryEventControlType?(FormEntryEventControlType.SectionHeader);
            entryEventControl.Title = controlProperty3.Value;
          }
          else
          {
            ControlProperty controlProperty5 = persistedProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => (p.Name == "Html" || p.Name == "Content") && p.Language == formEntry.Language));
            if (controlProperty5 != null)
            {
              nullable = new FormEntryEventControlType?(FormEntryEventControlType.InstructionalText);
              entryEventControl.Title = controlProperty5.Value;
            }
          }
        }
        if (nullable.HasValue)
        {
          entryEventControl.Type = nullable.Value;
          entryControls.Add((IFormEntryEventControl) entryEventControl);
        }
      }
      return (IEnumerable<IFormEntryEventControl>) entryControls;
    }

    /// <summary>Sorts the form controls.</summary>
    /// <param name="controls">The controls.</param>
    /// <returns></returns>
    internal static IEnumerable<ControlData> SortFormControls(
      IEnumerable<ControlData> controls)
    {
      controls = controls != null ? (IEnumerable<ControlData>) controls.ToList<ControlData>() : throw new ArgumentNullException(nameof (controls));
      IEnumerable<string> second = controls.SelectMany<ControlData, string>((Func<ControlData, IEnumerable<string>>) (c => (IEnumerable<string>) c.PlaceHolders)).Distinct<string>();
      return controls.Select<ControlData, string>((Func<ControlData, string>) (c => c.PlaceHolder)).Distinct<string>().Except<string>(second).SelectMany<string, ControlData>((Func<string, IEnumerable<ControlData>>) (ph => OpenAccessFormsProvider.SortControlsUnderSamePlaceholder(controls, ph)));
    }

    /// <summary>
    /// Finds the controls based on the placeholder name and sorts them.
    /// </summary>
    /// <param name="controls">The controls where to look.</param>
    /// <param name="placeholder">The placeholder name.</param>
    /// <returns></returns>
    private static IEnumerable<ControlData> SortControlsUnderSamePlaceholder(
      IEnumerable<ControlData> controls,
      string placeholder)
    {
      foreach (ControlData control in OpenAccessFormsProvider.SortControlsUnderSamePlaceholder(controls.Where<ControlData>((Func<ControlData, bool>) (x => x.PlaceHolder.Equals(placeholder, StringComparison.OrdinalIgnoreCase)))))
      {
        yield return control;
        foreach (ControlData controlData in ((IEnumerable<string>) control.PlaceHolders).SelectMany<string, ControlData>((Func<string, IEnumerable<ControlData>>) (ph => OpenAccessFormsProvider.SortControlsUnderSamePlaceholder(controls, ph))))
          yield return controlData;
      }
    }

    /// <summary>
    /// Sorts the controls which are located under the same placeholder.
    /// </summary>
    /// <param name="controls">The controls.</param>
    /// <returns></returns>
    private static IEnumerable<ControlData> SortControlsUnderSamePlaceholder(
      IEnumerable<ControlData> controls)
    {
      List<ControlData> remainingControls = controls.ToList<ControlData>();
      Guid siblingId = Guid.Empty;
      while (remainingControls.Count > 0)
      {
        ControlData controlData = (remainingControls.FirstOrDefault<ControlData>((Func<ControlData, bool>) (x => x.SiblingId == siblingId)) ?? remainingControls.FirstOrDefault<ControlData>((Func<ControlData, bool>) (x => x.SiblingId == Guid.Empty || remainingControls.All<ControlData>((Func<ControlData, bool>) (c => c.Id != x.SiblingId))))) ?? remainingControls.First<ControlData>();
        siblingId = controlData.Id;
        remainingControls.Remove(controlData);
        yield return controlData;
      }
    }

    private static string GetTextValue(object value, IFieldControl fieldControl)
    {
      TypeConverter typeConverter = (TypeConverter) null;
      PropertyDescriptor prop = TypeDescriptor.GetProperties((object) fieldControl).Find("Value", false);
      if (prop != null)
        typeConverter = prop.GetCustomTypeConverter();
      if (typeConverter == null && value != null)
        typeConverter = TypeDescriptor.GetConverter(value);
      return typeConverter != null && typeConverter.CanConvertTo(typeof (string)) ? (value != null ? (string) typeConverter.ConvertTo(value, typeof (string)) : (string) null) : value?.ToString();
    }

    private void AuthorizeFormEntries(string entryTypeName, string ation)
    {
      if (this.SuppressSecurityChecks)
        return;
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity != null && currentIdentity.IsUnrestricted)
        return;
      string formName = entryTypeName.Remove(0, string.Format("{0}.", (object) "Telerik.Sitefinity.DynamicTypes.Model").Length);
      FormDescription formDescription = this.GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Name == formName)).FirstOrDefault<FormDescription>();
      if (formDescription == null || this.SuppressSecurityChecks)
        return;
      if (!formDescription.IsGranted("Forms", ation))
        throw new UnauthorizedAccessException(string.Format("You are not authorized for '{0}' permissions!", (object) ation));
    }

    private T GetFormEntryEventInternal<T>(
      IList dirtyItems,
      SecurityConstants.TransactionActionType transactionType)
      where T : FormEntryEvent, new()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      OpenAccessFormsProvider.\u003C\u003Ec__DisplayClass77_0<T> cDisplayClass770 = new OpenAccessFormsProvider.\u003C\u003Ec__DisplayClass77_0<T>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass770.formEntry = dirtyItems.OfType<FormEntry>().FirstOrDefault<FormEntry>();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass770.formEntry != null && this.GetDirtyItemStatus((object) cDisplayClass770.formEntry) == transactionType)
      {
        ParameterExpression parameterExpression;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        // ISSUE: method reference
        FormDescription formDescription = this.GetForms().SingleOrDefault<FormDescription>(Expression.Lambda<Func<FormDescription, bool>>((Expression) Expression.Equal(f.Name, (Expression) Expression.Property((Expression) Expression.Call(cDisplayClass770.formEntry, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.GetType)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MemberInfo.get_Name)))), parameterExpression));
        if (formDescription != null)
        {
          T instance = (T) Activator.CreateInstance(typeof (T));
          // ISSUE: reference to a compiler-generated field
          instance.EntryId = cDisplayClass770.formEntry.Id;
          // ISSUE: reference to a compiler-generated field
          instance.ReferralCode = cDisplayClass770.formEntry.ReferralCode;
          // ISSUE: reference to a compiler-generated field
          instance.UserId = cDisplayClass770.formEntry.UserId;
          // ISSUE: reference to a compiler-generated field
          instance.Username = cDisplayClass770.formEntry.Username;
          // ISSUE: reference to a compiler-generated field
          instance.IpAddress = cDisplayClass770.formEntry.IpAddress;
          // ISSUE: reference to a compiler-generated field
          instance.SubmissionTime = cDisplayClass770.formEntry.SubmittedOn;
          instance.FormId = formDescription.Id;
          instance.FormName = formDescription.Name;
          instance.FormTitle = (string) formDescription.Title;
          instance.FormSubscriptionListId = formDescription.SubscriptionListId;
          instance.SendConfirmationEmail = formDescription.SendConfirmationEmail;
          // ISSUE: reference to a compiler-generated field
          instance.NotificationEmails = cDisplayClass770.formEntry.NotificationEmails;
          // ISSUE: reference to a compiler-generated field
          instance.Controls = OpenAccessFormsProvider.GetEntryControls(cDisplayClass770.formEntry, formDescription, getEntryControlsOriginalValue: (transactionType == SecurityConstants.TransactionActionType.Updated));
          return instance;
        }
      }
      return default (T);
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> instance.
    /// </summary>
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
    /// <param name="siteId">The site id.</param>
    /// <returns>
    ///  Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public override IQueryable<T> GetSiteItems<T>(Guid siteId) => MultisiteExtensions.GetSiteItems<T>(this, siteId);
  }
}
