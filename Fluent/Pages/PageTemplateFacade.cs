// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Fluent.Pages
{
  /// <summary>
  /// Fluent API that provides most common functionality related to a single Sitefinity page template.
  /// </summary>
  public class PageTemplateFacade : BaseFacadeWithManager
  {
    private PageTemplate currentState;
    private Telerik.Sitefinity.Fluent.AppSettings appSettings;
    private PageManager pageManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public PageTemplateFacade(Telerik.Sitefinity.Fluent.AppSettings appSettings)
      : base(appSettings)
    {
      this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="pageId">The page id.</param>
    public PageTemplateFacade(Telerik.Sitefinity.Fluent.AppSettings appSettings, Guid pageId)
      : this(appSettings)
    {
      if (pageId == Guid.Empty)
        throw new ArgumentNullException(nameof (pageId));
      this.LoadPageTemplate(pageId);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="page">The page template on which the fluent API functionality ought to be used.</param>
    public PageTemplateFacade(Telerik.Sitefinity.Fluent.AppSettings appSettings, PageTemplate template)
      : this(appSettings)
    {
      this.currentState = template != null ? template : throw new ArgumentNullException("page");
    }

    /// <summary>Needed for mocking.</summary>
    internal PageTemplateFacade()
    {
    }

    /// <summary>
    /// Gets an instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade.PageManager" /> to be used by this facade.
    /// </summary>
    /// <value>An initialized instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade.PageManager" /> class.</value>
    public virtual PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager(this.appSettings.PagesProviderName, this.appSettings.TransactionName);
        return this.pageManager;
      }
    }

    /// <summary>Creates a new page template</summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade" /> object.</returns>
    public virtual PageTemplateFacade CreateNew()
    {
      this.currentState = this.PageManager.CreateTemplate();
      return this;
    }

    /// <summary>
    /// Performs an arbitrary action on the page of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageTemplate" /> currently loaded at Fluent API.
    /// </summary>
    /// <param name="setAction">An action to be performed on the current page template loaded at Fluent API.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageTemplate" /> has not been initialized either through PageTemplateFacade(Guid pageId) constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade" /> object.</returns>
    public virtual PageTemplateFacade Do(Action<PageTemplate> setAction)
    {
      if (setAction == null)
        throw new ArgumentNullException(nameof (setAction));
      this.EnsureState();
      setAction(this.currentState);
      return this;
    }

    /// <summary>
    /// Performs deletion of the current page loaded by the fluent API.
    /// </summary>
    /// <param name="language">The language to delete.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the PageTaxon object has not been initialized either through PageTemplateFacade(Guid pageId) constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade" /> object.</returns>
    public virtual PageTemplateFacade Delete(CultureInfo language = null)
    {
      this.EnsureState();
      this.PageManager.Delete(this.currentState, language);
      return this;
    }

    /// <summary>
    /// Returns the page template currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Pages.Model.PageTemplate" /> object.</returns>
    public PageTemplate Get()
    {
      this.EnsureState();
      return this.currentState;
    }

    /// <summary>
    /// Performs setting the page template currently loaded by the fluent API to specified parameter object of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageTemplate" />.
    /// </summary>
    /// <param name="pageTemplate">The template to be set.</param>
    /// <returns></returns>
    public virtual PageTemplateFacade Set(PageTemplate pageTemplate)
    {
      this.currentState = pageTemplate;
      this.EnsureState();
      return this;
    }

    /// <summary>Sets the parent template of the current template.</summary>
    /// <param name="templateId">The template ID.</param>
    /// <returns></returns>
    public virtual PageTemplateFacade SetParentTemplateTo(Guid templateId)
    {
      this.EnsureState();
      if (templateId != Guid.Empty)
        this.currentState.SetParentTemplate(this.PageManager.GetTemplate(templateId) ?? throw new ArgumentException("There is no template with ID \"{0}\".".Arrange((object) templateId)));
      else
        this.currentState.SetParentTemplate((PageTemplate) null);
      return this;
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade" /> object.</returns>
    public new virtual bool SaveChanges()
    {
      base.SaveChanges();
      return true;
    }

    /// <summary>
    /// Duplicates the current template and loads it as current template in the facade.
    /// </summary>
    /// <param name="templateName">The name for the new template.</param>
    /// <returns></returns>
    public virtual PageTemplateFacade Duplicate(string templateName)
    {
      try
      {
        PageManager pageManager = this.PageManager;
        PageTemplate template = pageManager.CreateTemplate();
        pageManager.TemplatesLifecycle.GetMaster(template);
        PageTemplate pageTemplate = this.Get();
        TemplateDraft master = pageManager.TemplatesLifecycle.GetMaster(pageTemplate);
        if (master == null)
          pageManager.CopyTemplateData<TemplateControl, TemplatePresentation, TemplateControl, TemplatePresentation>((ITemplateData<TemplateControl, TemplatePresentation>) pageTemplate, (ITemplateData<TemplateControl, TemplatePresentation>) template, CopyDirection.Unspecified);
        else
          pageManager.CopyTemplateData<TemplateDraftControl, TemplateDraftPresentation, TemplateControl, TemplatePresentation>((ITemplateData<TemplateDraftControl, TemplateDraftPresentation>) master, (ITemplateData<TemplateControl, TemplatePresentation>) template, CopyDirection.Unspecified);
        template.Framework = pageTemplate.Framework;
        template.Title = (Lstring) templateName;
        template.SetParentTemplate(pageTemplate.ParentTemplate);
        template.Category = !pageTemplate.IsBackend ? SiteInitializer.CustomTemplatesCategoryId : SiteInitializer.BackendTemplatesCategoryId;
        this.Set(template);
        return this;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    /// Commit the changes and return the current facade for additional fluent calls
    /// </summary>
    public virtual PageTemplateFacade SaveAndContinue()
    {
      this.SaveChanges();
      return this;
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade" /> object.</returns>
    public virtual PageTemplateFacade CancelChanges()
    {
      base.CancelChanges();
      return this;
    }

    /// <summary>
    /// Loads the page template into the API state. This method should be called only if the class have been
    /// constructed with the template id parameter.
    /// </summary>
    /// <param name="pageId">Id of the page template that ought to be loaded in the API state.</param>
    protected virtual void LoadPageTemplate(Guid templateId) => this.currentState = !(templateId == Guid.Empty) ? this.PageManager.GetTemplate(templateId) : throw new ArgumentNullException(nameof (templateId));

    /// <summary>
    /// Ensures that the state of the facade has been initialized and throws an exception if not.
    /// </summary>
    protected virtual void EnsureState()
    {
      if (this.currentState == null)
        throw new InvalidOperationException("This method cannot be executed when the state of the facade is not initialized.");
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) this.PageManager;
  }
}
