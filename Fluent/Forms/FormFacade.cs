// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Forms.FormFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Fluent.Content;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.MessageTemplates;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;

namespace Telerik.Sitefinity.Fluent.Forms
{
  /// <summary>
  /// Fluent API that provides most common functionality related to a single Sitefinity form.
  /// </summary>
  public class FormFacade : IItemFacade<FormFacade, FormDescription>, IFacade<FormFacade>
  {
    private FormDescription formDescription;
    private AppSettings appSettings;
    private FormsManager formsManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public FormFacade(AppSettings appSettings) => this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="itemId">The unique Identifier of the form description that need to be loaded.</param>
    public FormFacade(AppSettings appSettings, Guid itemId)
      : this(appSettings)
    {
      if (itemId == Guid.Empty)
        throw new ArgumentNullException(nameof (itemId));
      this.LoadFormDescription(itemId);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="itemId">The unique name of the form description that need to be loaded.</param>
    public FormFacade(AppSettings appSettings, string formName)
      : this(appSettings)
    {
      if (string.IsNullOrEmpty(formName))
        throw new ArgumentNullException(nameof (formName));
      this.LoadForm(formName);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="formDescription">The form description on which the fluent API functionality ought to be used.</param>
    public FormFacade(AppSettings appSettings, FormDescription formDescription)
      : this(appSettings)
    {
      this.formDescription = formDescription != null ? formDescription : throw new ArgumentNullException(nameof (formDescription));
    }

    /// <summary>
    /// Gets the form description on which all the methods of the fluent API are being
    /// performed.
    /// </summary>
    protected internal FormDescription FormDescription => this.formDescription;

    /// <summary>
    /// Gets an instance of the <see cref="!:PageManager" /> to be used by this facade.
    /// </summary>
    /// <value>An initialized instance of the <see cref="!:PageManager" /> class.</value>
    protected internal virtual FormsManager FormsManager
    {
      get
      {
        if (this.formsManager == null)
          this.formsManager = FormsManager.GetManager(this.appSettings.PagesProviderName, this.appSettings.TransactionName);
        return this.formsManager;
      }
    }

    FormFacade IItemFacade<FormFacade, FormDescription>.CreateNew()
    {
      this.formDescription = this.FormsManager.CreateForm("Name");
      return this;
    }

    FormFacade IItemFacade<FormFacade, FormDescription>.CreateNew(
      Guid itemId)
    {
      this.formDescription = this.FormsManager.CreateForm("Name", itemId);
      return this;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="P:Telerik.Sitefinity.Fluent.Forms.FormFacade.FormDescription" /> object with specified form name.
    /// </summary>
    /// <param name="formName">Name of the form. This name is used to construct the actual CLR type of the form entry
    /// and therefore the name should comply to CLR naming rules.</param>
    /// <returns>
    /// An instance of type <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" />.
    /// </returns>
    public FormFacade CreateNew(string formName)
    {
      this.formDescription = this.FormsManager.CreateForm(formName);
      return this;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="P:Telerik.Sitefinity.Fluent.Forms.FormFacade.FormDescription" /> object with specified id.
    /// </summary>
    /// <param name="formName">Name of the form. This name is used to construct the actual CLR type of the form entry
    /// and therefore the name should comply to CLR naming rules.</param>
    /// <param name="itemId">The item id.</param>
    /// <returns>
    /// An instance of type <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" />.
    /// </returns>
    public FormFacade CreateNew(string formName, Guid itemId)
    {
      this.formDescription = this.FormsManager.CreateForm(formName, itemId);
      return this;
    }

    /// <summary>Deletes a form description.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the FormData object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> object.</returns>
    public virtual FormFacade Delete()
    {
      this.EnsureFormDescriptionLoaded(true);
      this.FormsManager.Delete(this.formDescription);
      this.DeleteMetaTypeAndEntries();
      this.DeleteFormMessageTemplates();
      return this;
    }

    /// <summary>
    /// Deletes a translation of Form description if a culture is specified
    /// If no culture is given the whole form description is deleted
    /// </summary>
    /// <param name="culture"></param>
    /// <returns></returns>
    public virtual FormFacade Delete(CultureInfo culture)
    {
      if (culture == null)
        return this.Delete();
      if (this.FormsManager.Delete(this.Get(), culture))
        this.DeleteMetaTypeAndEntries();
      this.DeleteFormMessageTemplates();
      return this;
    }

    /// <summary>
    /// Returns the form description currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of <see cref="!:Node" /> object.</returns>
    public FormDescription Get()
    {
      this.EnsureFormDescriptionLoaded(true);
      return this.FormDescription;
    }

    /// <summary>
    /// Sets the specified formDescription parameter of type <see cref="P:Telerik.Sitefinity.Fluent.Forms.FormFacade.FormDescription" /> as current form description at Fluent API form facade.
    /// </summary>
    /// <param name="formDescription">The node to be set.</param>
    /// <returns></returns>
    public virtual FormFacade Set(FormDescription formDescription)
    {
      this.formDescription = formDescription;
      this.EnsureFormDescriptionLoaded(true);
      return this;
    }

    /// <summary>
    /// Performs an arbitrary action on the form description object.
    /// </summary>
    /// <param name="setAction">An action to be performed on the form description object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the PageTaxon object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> object.</returns>
    public virtual FormFacade Do(Action<FormDescription> setAction)
    {
      if (setAction == null)
        throw new ArgumentNullException(nameof (setAction));
      this.EnsureFormDescriptionLoaded(true);
      setAction(this.formDescription);
      return this;
    }

    /// <summary>Sets the framework on the form description object.</summary>
    /// <param name="framework">The framework of the form.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// Thrown if the form description object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> object.</returns>
    public virtual FormFacade SetFramework(FormFramework framework) => this.Do((Action<FormDescription>) (f => f.Framework = framework));

    /// <summary>
    /// Provides the methods for woring with a single form entry.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntryFacade" /> class.</returns>
    public FormEntryFacade Entry()
    {
      this.EnsureFormDescriptionLoaded(true);
      return new FormEntryFacade(this.appSettings, this.FormDescription);
    }

    /// <summary>
    /// Provides the methods for working with a single form entry and automatically loads the form
    /// entry with the specified id.
    /// </summary>
    /// <param name="formEntryId">Id of the form entry to be loaded into the state of the facade.</param>
    /// <returns>An instace of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntryFacade" /> class.</returns>
    public FormEntryFacade Entry(Guid formEntryId)
    {
      this.EnsureFormDescriptionLoaded(true);
      return new FormEntryFacade(this.appSettings, this.FormDescription, formEntryId);
    }

    /// <summary>
    /// Provides the methods for working with a single form entry and automatically loads the form
    /// entry that was passed to the method.
    /// </summary>
    /// <param name="formEntry">
    /// An instance of the form entry to be loaded into the state of the facade.
    /// </param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntryFacade" /> class.</returns>
    public FormEntryFacade Entry(FormEntry formEntry)
    {
      this.EnsureFormDescriptionLoaded(true);
      return new FormEntryFacade(this.appSettings, this.FormDescription, formEntry);
    }

    /// <summary>
    /// Provides the method for working with the collection of the form entries.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade" /> class.</returns>
    public FormEntriesFacade Entries()
    {
      this.EnsureFormDescriptionLoaded(true);
      return new FormEntriesFacade(this.appSettings, this.FormDescription);
    }

    /// <summary>
    /// Provides the methods for working with the collection of the form entries and automatically
    /// loads the collection of form entries passed to the method into the state of the facade.
    /// </summary>
    /// <param name="formEntries">An instance of IQueryable form entries that is to be loaded into the state of the facade.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade" /> class.</returns>
    public FormEntriesFacade Entries(IQueryable<FormEntry> formEntries)
    {
      this.EnsureFormDescriptionLoaded(true);
      return new FormEntriesFacade(this.appSettings, this.FormDescription, formEntries);
    }

    /// <summary>Deletes the meta type and the entris of the form</summary>
    public void DeleteMetaTypeAndEntries()
    {
      MetaType metaType = MetadataManager.GetManager(this.appSettings.MetadataProviderName, this.appSettings.TransactionName).GetMetaType(this.FormsManager.Provider.FormsNamespace, this.formDescription.Name);
      if (metaType == null)
        return;
      foreach (object formEntry in (IEnumerable<FormEntry>) this.FormsManager.GetFormEntries(this.FormDescription))
        this.FormsManager.DeleteItem(formEntry);
      metaType.IsDeleted = true;
    }

    /// <summary>
    /// Duplicates the current form and uses the newly created duplicate as the current form loaded by the fluent API.
    /// </summary>
    /// <param name="formName">Name of the duplicated form</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> object.</returns>
    public virtual FormFacade Duplicate(string formName)
    {
      try
      {
        FormsManager formsManager = this.FormsManager;
        FormDescription form = formsManager.CreateForm(formName);
        FormDraft master = formsManager.Lifecycle.GetMaster(this.Get());
        Guid id;
        if (master == null)
        {
          FormDescription formDescription = this.Get();
          formsManager.CopyFormCommonData<FormControl, FormControl>((IFormData<FormControl>) formDescription, (IFormData<FormControl>) form, CopyDirection.Unspecified);
          form.CopySecurityFrom((ISecuredObject) formDescription, (IDataProviderBase) null, (IDataProviderBase) null);
          id = formDescription.Id;
        }
        else
        {
          formsManager.CopyFormCommonData<FormDraftControl, FormControl>((IFormData<FormDraftControl>) master, (IFormData<FormControl>) form, CopyDirection.Unspecified);
          form.CopySecurityFrom((ISecuredObject) master.ParentForm, (IDataProviderBase) null, (IDataProviderBase) null);
          id = master.ParentForm.Id;
        }
        this.DuplicateFormMessageTemplates(id, form.Id);
        form.Controls.ToList<FormControl>().ForEach((Action<FormControl>) (c => c.Published = false));
        this.Set(form);
        return this;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void DuplicateFormMessageTemplates(Guid parentFormId, Guid duplicateFormId)
    {
      string name = SystemManager.CurrentContext.Culture.Name;
      IActionMessageTemplate[] actionMessageTemplateArray = new IActionMessageTemplate[3];
      NewFormResponseMessageTemplate responseMessageTemplate1 = new NewFormResponseMessageTemplate();
      responseMessageTemplate1.ParentId = parentFormId;
      responseMessageTemplate1.Language = name;
      actionMessageTemplateArray[0] = (IActionMessageTemplate) responseMessageTemplate1;
      ModifiedFormResponseMessageTemplate responseMessageTemplate2 = new ModifiedFormResponseMessageTemplate();
      responseMessageTemplate2.ParentId = parentFormId;
      responseMessageTemplate2.Language = name;
      actionMessageTemplateArray[1] = (IActionMessageTemplate) responseMessageTemplate2;
      FormConfirmationMessageTemplate confirmationMessageTemplate = new FormConfirmationMessageTemplate();
      confirmationMessageTemplate.ParentId = parentFormId;
      confirmationMessageTemplate.Language = name;
      actionMessageTemplateArray[2] = (IActionMessageTemplate) confirmationMessageTemplate;
      INotificationService notificationService = SystemManager.GetNotificationService();
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      foreach (IActionMessageTemplate actionMessageTemplate in actionMessageTemplateArray)
      {
        IMessageTemplateResponse systemMessageTemplate = notificationService.GetSystemMessageTemplate(serviceContext, actionMessageTemplate.GetKey());
        if (systemMessageTemplate != null)
        {
          actionMessageTemplate.ApplyVariations(("parentid", duplicateFormId.ToString()));
          systemMessageTemplate.ResolveKey = actionMessageTemplate.GetKey();
          notificationService.CreateMessageTemplate(serviceContext, (IMessageTemplateRequest) systemMessageTemplate);
        }
      }
    }

    private void DeleteFormMessageTemplates()
    {
      this.EnsureFormDescriptionLoaded(true);
      INotificationService notificationService = SystemManager.GetNotificationService();
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      IActionMessageTemplate[] actionMessageTemplateArray1 = new IActionMessageTemplate[3];
      NewFormResponseMessageTemplate responseMessageTemplate1 = new NewFormResponseMessageTemplate();
      responseMessageTemplate1.ParentId = this.FormDescription.Id;
      actionMessageTemplateArray1[0] = (IActionMessageTemplate) responseMessageTemplate1;
      ModifiedFormResponseMessageTemplate responseMessageTemplate2 = new ModifiedFormResponseMessageTemplate();
      responseMessageTemplate2.ParentId = this.FormDescription.Id;
      actionMessageTemplateArray1[1] = (IActionMessageTemplate) responseMessageTemplate2;
      FormConfirmationMessageTemplate confirmationMessageTemplate = new FormConfirmationMessageTemplate();
      confirmationMessageTemplate.ParentId = this.FormDescription.Id;
      actionMessageTemplateArray1[2] = (IActionMessageTemplate) confirmationMessageTemplate;
      IActionMessageTemplate[] actionMessageTemplateArray2 = actionMessageTemplateArray1;
      foreach (string availableLanguage in this.FormDescription.AvailableLanguages)
      {
        foreach (IActionMessageTemplate actionMessageTemplate in actionMessageTemplateArray2)
        {
          actionMessageTemplate.ApplyVariations(("language", availableLanguage));
          IMessageTemplateResponse systemMessageTemplate = notificationService.GetSystemMessageTemplate(serviceContext, actionMessageTemplate.GetKey());
          if (systemMessageTemplate != null)
            notificationService.DeleteMessageTemplate(serviceContext, systemMessageTemplate.Id);
        }
      }
    }

    public virtual FormFacade Share(IEnumerable<Guid> sites)
    {
      if (sites == null)
        throw new ArgumentNullException(sites.ToString());
      IQueryable<SiteItemLink> siteFormLinks = this.FormsManager.GetSiteFormLinks();
      Expression<Func<SiteItemLink, bool>> predicate = (Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == this.FormDescription.Id);
      foreach (SiteItemLink link in siteFormLinks.Where<SiteItemLink>(predicate).ToArray<SiteItemLink>())
        this.FormsManager.Delete(link);
      this.SaveChanges();
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      foreach (Guid site in sites)
        this.FormsManager.LinkFormToSite(this.FormDescription, site);
      return this;
    }

    /// <summary>
    /// An instance of child facade of type <see cref="!:ControlFacade" />.
    /// </summary>
    /// <returns>
    /// An instance of child facade of type <see cref="!:ControlFacade" />.
    /// </returns>
    public virtual FormControlFacade Control() => new FormControlFacade(this, this.appSettings);

    /// <summary>
    /// An instance of child facade collection of type <see cref="!:ControlsFacade" />.
    /// </summary>
    /// <returns></returns>
    public virtual FormControlFacade Controls() => new FormControlFacade(this, this.appSettings);

    public virtual OrganizationFacade<FormFacade> Organization() => new OrganizationFacade<FormFacade>(this.appSettings, (IOrganizable) this.formDescription, this);

    /// <summary>
    /// Loads the form description into the API state. This method should be called only if the class have been
    /// constructed with the itemId id parameter.
    /// </summary>
    /// <param name="itemId))">Id of the form description that ought to be loaded in the API state.</param>
    protected internal virtual void LoadFormDescription(Guid itemId) => this.formDescription = !(itemId == Guid.Empty) ? this.FormsManager.GetForm(itemId) : throw new ArgumentNullException(nameof (itemId));

    /// <summary>
    /// Loads the form description with the specified name into the API state.
    /// </summary>
    /// <param name="formName">The name of the form.</param>
    protected internal virtual void LoadForm(string formName) => this.formDescription = !string.IsNullOrEmpty(formName) ? this.FormsManager.GetFormByName(formName) : throw new ArgumentNullException(nameof (formName));

    /// <summary>
    /// This method is called to ensure that a form description has been loaded (either from id or created).
    /// </summary>
    /// <param name="throwExceptionIfNot">
    /// If this parameter is set to true, an exception will be thrown; otherwise method can be used in
    /// a less obtrusive way when it only returns the boolean value indicating whether a form description has been loaded.
    /// </param>
    /// <returns>
    /// True if form description has been loaded; otherwise false.
    /// </returns>
    protected internal virtual bool EnsureFormDescriptionLoaded(bool throwExceptionIfNot)
    {
      bool flag = this.FormDescription != null;
      return !throwExceptionIfNot || flag ? flag : throw new InvalidOperationException("This method must not be called before the form description object has been loaded either through the constructor or by calling the CreateNew method.");
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> object.</returns>
    public virtual FormFacade SaveChanges()
    {
      TransactionManager.CommitTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> object.</returns>
    public virtual FormFacade CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.appSettings.TransactionName);
      return this;
    }
  }
}
