// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;

namespace Telerik.Sitefinity.Fluent.Forms
{
  /// <summary>
  /// This class provides fluent API with most common functionality for working with a collection of form entries.
  /// </summary>
  public class FormEntriesFacade : 
    ICollectionFacade<FormEntriesFacade, FormEntry>,
    IFacade<FormEntriesFacade>
  {
    private FormDescription formDescription;
    private IQueryable<FormEntry> formEntries;
    private AppSettings appSettings;
    private FormsManager formsManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade" /> class.
    /// </summary>
    /// <param name="appSettings">Instance of the <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> class used to configure the facade.</param>
    /// <param name="formDescription">The form description to which the form entries belong to.</param>
    public FormEntriesFacade(AppSettings appSettings, FormDescription formDescription)
    {
      if (appSettings == null)
        throw new ArgumentNullException(nameof (appSettings));
      if (formDescription == null)
        throw new ArgumentNullException(nameof (formDescription));
      this.appSettings = appSettings;
      this.formDescription = formDescription;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade" /> class.
    /// </summary>
    /// <param name="appSettings">Instance of the <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> class used to configure the facade.</param>
    /// <param name="formDescription">The form description to which the form entries belong to.</param>
    /// <param name="formEntries">The form entries to be loaded into the facade.</param>
    public FormEntriesFacade(
      AppSettings appSettings,
      FormDescription formDescription,
      IQueryable<FormEntry> formEntries)
      : this(appSettings, formDescription)
    {
      this.formEntries = formEntries != null ? formEntries : throw new ArgumentNullException(nameof (formEntries));
    }

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade.FormsManager" /> to be used by this facade.
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

    /// <summary>
    /// Gets or sets the query of all form entries the given forms entry provider. This query is used
    /// by the fluent API and all methods are executed on this query.
    /// </summary>
    protected virtual IQueryable<FormEntry> FormEntries
    {
      get
      {
        if (this.formEntries == null)
          this.formEntries = this.FormsManager.GetFormEntries(this.formDescription);
        return this.formEntries;
      }
      set => this.formEntries = value;
    }

    /// <summary>
    /// Gets the count of items in collection loaded at current facade.
    /// </summary>
    /// <param name="result">The count of items.</param>
    /// <returns>An instance of the type <see cref="!:FormEntiesFacade" />.</returns>
    public FormEntriesFacade Count(out int result)
    {
      result = this.formEntries.Count<FormEntry>();
      return this;
    }

    /// <summary>
    /// Performs an arbitrary action for each item of the collection loaded at facade.
    /// </summary>
    /// <param name="action">An action to be performed for each item of collection.</param>
    /// <returns>An instance of the type <see cref="!:FormEntiesFacade" />.</returns>
    public FormEntriesFacade ForEach(Action<FormEntry> action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      foreach (FormEntry formEntry in (IEnumerable<FormEntry>) this.formEntries)
        action(formEntry);
      return this;
    }

    /// <summary>
    /// Gets query with instances of type <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" />.
    /// </summary>
    /// <returns>An instance of IQueryable form entries that are loaded into the facade state.</returns>
    public IQueryable<FormEntry> Get() => this.FormEntries;

    /// <summary>
    /// Orders the items of collection in ascending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the type <see cref="!:FormEntiesFacade" />.</returns>
    public FormEntriesFacade OrderBy<TKey>(
      Expression<Func<FormEntry, TKey>> keySelector)
    {
      this.formEntries = keySelector != null ? (IQueryable<FormEntry>) this.FormEntries.OrderBy<FormEntry, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>
    /// Orders the items of collection in descending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the type <see cref="!:FormEntiesFacade" />.</returns>
    public FormEntriesFacade OrderByDescending<TKey>(
      Expression<Func<FormEntry, TKey>> keySelector)
    {
      this.formEntries = keySelector != null ? (IQueryable<FormEntry>) this.FormEntries.OrderBy<FormEntry, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>
    /// Sets the collection with items filtered with query parameter.
    /// </summary>
    /// <param name="query">The query to filter the items.</param>
    /// <returns>An instance of the type <see cref="!:FormEntiesFacade" />.</returns>
    public FormEntriesFacade Set(IQueryable<FormEntry> query)
    {
      this.FormEntries = query != null ? query : throw new ArgumentNullException(nameof (query));
      return this;
    }

    /// <summary>
    /// Bypasses a specified number of items of collection and then returns the remaining elements.
    /// </summary>
    /// <param name="count">The number of items to bypass.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade" /> object.</returns>
    public FormEntriesFacade Skip(int count)
    {
      this.formEntries = count >= 0 ? this.FormEntries.Skip<FormEntry>(count).ToList<FormEntry>().AsQueryable<FormEntry>() : throw new ArgumentException(nameof (count));
      return this;
    }

    /// <summary>
    /// Takes items from collection number of which is specified with the count parameter.
    /// </summary>
    /// <param name="count">The count of items to be taken.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade" /> object.</returns>
    public FormEntriesFacade Take(int count)
    {
      this.formEntries = count >= 0 ? this.FormEntries.Take<FormEntry>(count).ToList<FormEntry>().AsQueryable<FormEntry>() : throw new ArgumentException(nameof (count));
      return this;
    }

    /// <summary>
    /// Filters items of the collection by specified where clause at predicate parameter of the method.
    /// </summary>
    /// <param name="predicate">The predicate to filter ty.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade" /> object.</returns>
    public FormEntriesFacade Where(Func<FormEntry, bool> predicate)
    {
      this.formEntries = predicate != null ? this.formEntries.Where<FormEntry>(predicate).AsQueryable<FormEntry>() : throw new ArgumentNullException(nameof (predicate));
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
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade" /> object.</returns>
    public FormEntriesFacade SaveChanges()
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
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade" /> object.</returns>
    public FormEntriesFacade CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>
    /// Deletes all the form entries currently selected by this instance of fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormEntriesFacade" />.</returns>
    public FormEntriesFacade Delete() => throw new NotImplementedException();
  }
}
