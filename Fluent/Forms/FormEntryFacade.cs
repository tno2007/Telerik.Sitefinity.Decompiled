// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Forms.FormEntryFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;

namespace Telerik.Sitefinity.Fluent.Forms
{
  /// <summary>
  /// This class provides fluent API with most common functionalities for working with a single form entry.
  /// </summary>
  public class FormEntryFacade : IItemFacade<FormEntryFacade, FormEntry>, IFacade<FormEntryFacade>
  {
    private FormDescription formDescription;
    private FormEntry formEntry;
    private AppSettings appSettings;
    private FormsManager formsManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntryFacade" /> class.
    /// </summary>
    /// <param name="appSettings">Instance of the <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> class used to configure the facade.</param>
    /// <param name="formDescription">The form description to which the form entry belongs to or will belong to.</param>
    public FormEntryFacade(AppSettings appSettings, FormDescription formDescription)
    {
      if (appSettings == null)
        throw new ArgumentNullException(nameof (appSettings));
      if (formDescription == null)
        throw new ArgumentNullException(nameof (formDescription));
      this.appSettings = appSettings;
      this.formDescription = formDescription;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntryFacade" /> class.
    /// </summary>
    /// <param name="appSettings">Instance of the <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> class used to configure the facade.</param>
    /// <param name="formDescription">The form description to which the form entry belongs to or will belong to.</param>
    /// <param name="formEntryId">The id of the form entry.</param>
    public FormEntryFacade(
      AppSettings appSettings,
      FormDescription formDescription,
      Guid formEntryId)
      : this(appSettings, formDescription)
    {
      if (formEntryId == Guid.Empty)
        throw new ArgumentNullException(nameof (formEntryId));
      this.LoadFormEntry(formEntryId);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntryFacade" /> class.
    /// </summary>
    /// <param name="appSettings">Instance of the <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> class used to configure the facade.</param>
    /// <param name="formDescription">The form description to which the form entry belongs to or will belong to.</param>
    /// <param name="formEntry">The form entry to be loaded into the state of the facade.</param>
    public FormEntryFacade(
      AppSettings appSettings,
      FormDescription formDescription,
      FormEntry formEntry)
      : this(appSettings, formDescription)
    {
      this.formEntry = formEntry != null ? formEntry : throw new ArgumentNullException(nameof (formEntry));
    }

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Fluent.Forms.FormEntryFacade.FormsManager" /> to be used by this facade.
    /// </summary>
    protected virtual FormsManager FormsManager
    {
      get
      {
        if (this.formsManager == null)
          this.formsManager = FormsManager.GetManager(this.appSettings.FormsProviderName, this.appSettings.TransactionName);
        return this.formsManager;
      }
    }

    public FormEntryFacade CreateNew()
    {
      this.formEntry = this.FormsManager.CreateFormEntry(string.Format("{0}.{1}", (object) this.FormsManager.Provider.FormsNamespace, (object) this.formDescription.Name));
      return this;
    }

    public FormEntryFacade CreateNew(Guid itemId)
    {
      this.formEntry = this.FormsManager.CreateFormEntry(string.Format("{0}.{1}", (object) this.FormsManager.Provider.FormsNamespace, (object) this.formDescription.Name), itemId);
      return this;
    }

    public FormEntry Get()
    {
      this.EnsureFormEnrtyLoaded(true);
      return this.formEntry;
    }

    public FormEntryFacade Set(FormEntry item)
    {
      this.formEntry = item;
      return this;
    }

    /// <summary>
    /// Performs an arbitrary action on the instance of type <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> currently loaded at Fluent API.
    /// </summary>
    /// <param name="setAction">An action to be performed on the current form entry loaded at Fluent API.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object of type <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> has not been initialized either through FormEnryFacade constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntryFacade" /> object.</returns>
    public virtual FormEntryFacade Do(Action<FormEntry> setAction)
    {
      if (setAction == null)
        throw new ArgumentNullException(nameof (setAction));
      this.EnsureFormEnrtyLoaded(true);
      setAction(this.formEntry);
      return this;
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntryFacade" /> object.</returns>
    public FormEntryFacade SaveChanges()
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
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntryFacade" /> object.</returns>
    public FormEntryFacade CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>
    /// Performs deletion of the form entry currntly loaded by the fluent API.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the FormEntry object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntryFacade" /> object.</returns>
    public FormEntryFacade Delete()
    {
      this.EnsureFormEnrtyLoaded(true);
      this.FormsManager.Delete(this.formEntry);
      return this;
    }

    /// <summary>
    /// Sets a value of a field with specified fieldName parameter name and fieldValue value.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="fieldValue">The field value.</param>
    /// <returns></returns>
    public FormEntryFacade SetFieldValue(string fieldName, object fieldValue)
    {
      this.EnsureFormEnrtyLoaded(true);
      this.formEntry.SetValue(fieldName, fieldValue);
      return this;
    }

    /// <summary>
    /// This method accepts the form entry id and loads it through the manager into the facade state.
    /// </summary>
    /// <param name="formEntryId">Id of the form entry to be loaded.</param>
    private void LoadFormEntry(Guid formEntryId)
    {
      if (formEntryId == Guid.Empty)
        throw new ArgumentNullException(nameof (formEntryId));
      this.formEntry = this.FormsManager.GetFormEntry(string.Format("{0}.{1}", (object) this.FormsManager.Provider.FormsNamespace, (object) this.formDescription.Name), formEntryId);
    }

    /// <summary>
    /// This method is called to ensure that a form entry has been loaded (either from id or created).
    /// </summary>
    /// <param name="throwExceptionIfNot">
    /// If this parameter is set to true, an exception will be thrown; otherwise method can be used in
    /// a less obtrusive way when it only returns the boolean value indicating whether a form entry has been loaded.
    /// </param>
    /// <returns>True if page has been loaded; otherwise false.</returns>
    protected internal virtual bool EnsureFormEnrtyLoaded(bool throwExceptionIfNot)
    {
      bool flag = this.formEntry != null;
      return !throwExceptionIfNot || flag ? flag : throw new InvalidOperationException("This method must not be called before the form entry object has been loaded either through the constructor or by calling the CreateNew method.");
    }
  }
}
