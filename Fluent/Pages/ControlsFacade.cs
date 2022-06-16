// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Pages.ControlsFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.Pages.Contracts;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Fluent.Pages
{
  /// <summary>
  /// Fluent API that provides most common functionality needed to work with a collection of Sitefinity controls of type <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" />.
  /// </summary>
  public class ControlsFacade<TParentFacade> : 
    BaseFacadeWithManager,
    ICollectionFacade<ControlsFacade<TParentFacade>, ControlData>,
    IFacade<ControlsFacade<TParentFacade>>
    where TParentFacade : IPageFacade
  {
    private IQueryable<ControlData> controls;
    private AppSettings appSettings;
    private PageManager pageManager;
    private TParentFacade parentFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ControlsFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public ControlsFacade(AppSettings appSettings)
      : base(appSettings)
    {
      this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ControlsFacade" /> class that has reference to parent page facade of type <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" />.
    /// </summary>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    public ControlsFacade(TParentFacade parentFacade, AppSettings appSettings)
      : this(appSettings)
    {
      this.parentFacade = parentFacade;
    }

    /// <summary>
    /// Gets or sets the query of all custom controls. This query is used
    /// by the fluent API and all methods are executed on this query.
    /// </summary>
    protected IQueryable<ControlData> Controls
    {
      get
      {
        if (this.controls == null)
        {
          this.controls = this.PageManager.GetControls<ControlData>();
          if ((object) this.parentFacade != null && this.parentFacade.CurrentState != null)
            this.controls = this.parentFacade.CurrentState.Controls.ToList<ControlData>().AsQueryable<ControlData>();
        }
        return this.controls;
      }
      set => this.controls = value;
    }

    /// <summary>
    /// Gets an instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.ControlsFacade`1.PageManager" /> to be used by this facade.
    /// </summary>
    /// <value>An initialized instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.ControlsFacade`1.PageManager" /> class.</value>
    protected internal virtual PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager(this.appSettings.PagesProviderName, this.appSettings.TransactionName);
        return this.pageManager;
      }
    }

    /// <summary>
    /// Gets the count of items in collection at current facade.
    /// </summary>
    /// <param name="result">The count of items.</param>
    /// <returns>An instance of <see cref="!:ControlsFacade" /> object.</returns>
    public virtual ControlsFacade<TParentFacade> Count(out int result)
    {
      result = this.controls.Count<ControlData>();
      return this;
    }

    /// <summary>
    /// Deletes all the controls currently selected by this instance of the controls fluent API.
    /// </summary>
    /// <returns>An instance of type <see cref="!:ControlsFacade" />.</returns>
    public virtual ControlsFacade<TParentFacade> Delete()
    {
      foreach (ControlData control in (IEnumerable<ControlData>) this.Controls)
        this.PageManager.Delete(control);
      return this;
    }

    /// <summary>
    /// Returns the parent facade that initialized this child facade.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if method is called and parentFacade is null; meaning that facade is not a child facade in this context.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> that initialized this facade as a child facade.</returns>
    public TParentFacade Done() => (object) this.parentFacade != null ? this.parentFacade : throw new InvalidOperationException("Done method can be called only when the facade has been initialized as a child facade.");

    /// <summary>
    /// Gets query with instances of type &lt;typeparam name="ControlData"&gt;.
    /// </summary>
    /// <returns>An instance of IQueryable[ControlData] objects.</returns>
    public IQueryable<ControlData> Get() => this.Controls;

    /// <summary>
    /// Performs an arbitrary action for each item of collection at facade.
    /// </summary>
    /// <param name="action">An action to be performed for each item of collection.</param>
    /// <returns>An instance of <see cref="!:ControlsFacade" /> object.</returns>
    public virtual ControlsFacade<TParentFacade> ForEach(Action<ControlData> action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      foreach (ControlData control in (IEnumerable<ControlData>) this.Controls)
        action(control);
      return this;
    }

    /// <summary>
    /// Filters all the instances of type specified at controlType parameter at collection of the controls fluent API.
    /// </summary>
    /// <param name="controlType">Type of the control.</param>
    /// <returns>
    /// An instance of the <see cref="!:ControlsFacade" /> object.
    /// </returns>
    public virtual ControlsFacade<TParentFacade> OfType(Type controlType)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ControlsFacade<TParentFacade>.\u003C\u003Ec__DisplayClass12_0 cDisplayClass120 = new ControlsFacade<TParentFacade>.\u003C\u003Ec__DisplayClass12_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.controlType = controlType;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass120.controlType == (Type) null)
        throw new ArgumentNullException(nameof (controlType));
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: type reference
      // ISSUE: method reference
      this.controls = this.controls.Where<ControlData>(Expression.Lambda<Func<ControlData, bool>>((Expression) Expression.Equal((Expression) Expression.Call(c, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.GetType)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass120, typeof (ControlsFacade<TParentFacade>.\u003C\u003Ec__DisplayClass12_0)), FieldInfo.GetFieldFromHandle(__fieldref (ControlsFacade<TParentFacade>.\u003C\u003Ec__DisplayClass12_0.controlType), __typeref (ControlsFacade<TParentFacade>.\u003C\u003Ec__DisplayClass12_0))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Type.op_Equality))), parameterExpression));
      return this;
    }

    /// <summary>
    /// Orders the items of collection in ascending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of <see cref="!:ControlsFacade" /> object.</returns>
    public virtual ControlsFacade<TParentFacade> OrderBy<TKey>(
      Expression<Func<ControlData, TKey>> keySelector)
    {
      this.controls = keySelector != null ? (IQueryable<ControlData>) this.Controls.OrderBy<ControlData, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>
    /// Orders the items of collection in descending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the <see cref="!:ControlsFacade" /> object.</returns>
    public virtual ControlsFacade<TParentFacade> OrderByDescending<TKey>(
      Expression<Func<ControlData, TKey>> keySelector)
    {
      this.controls = keySelector != null ? (IQueryable<ControlData>) this.Controls.OrderBy<ControlData, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>
    /// Sets the collection with items filtered with query parameter.
    /// </summary>
    /// <param name="query">The query to filter the items.</param>
    /// <returns>An instance of <see cref="!:ControlsFacade" /> object.</returns>
    public virtual ControlsFacade<TParentFacade> Set(IQueryable<ControlData> query)
    {
      this.Controls = query != null ? query : throw new ArgumentNullException(nameof (query));
      return this;
    }

    /// <summary>
    /// Sets the zone of every the instance of type <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> at collection of this instance of the controls fluent API.
    /// </summary>
    /// <param name="zoneName">Name of the zone.</param>
    /// <returns>An instance of the <see cref="!:ControlsFacade" /> object.</returns>
    public virtual ControlsFacade<TParentFacade> SetZone(string zoneName)
    {
      if (string.IsNullOrEmpty(zoneName))
        throw new ArgumentNullException(nameof (zoneName));
      foreach (ControlData control in (IEnumerable<ControlData>) this.Controls)
        control.PlaceHolder = zoneName;
      return this;
    }

    /// <summary>
    /// Bypasses the specified number of the items at collection.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns>An instance of <see cref="!:ControlsFacade" /> object.</returns>
    public virtual ControlsFacade<TParentFacade> Skip(int count)
    {
      this.controls = count >= 0 ? this.controls.Skip<ControlData>(count).ToList<ControlData>().AsQueryable<ControlData>() : throw new ArgumentException(nameof (count));
      return this;
    }

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a sequence.
    /// </summary>
    /// <param name="count">The number of items to return.</param>
    /// <returns>An instance of &lt;typeparam name="ControlsFacade"&gt; object.</returns>
    public virtual ControlsFacade<TParentFacade> Take(int count)
    {
      this.controls = count >= 0 ? this.controls.Take<ControlData>(count).ToList<ControlData>().AsQueryable<ControlData>() : throw new ArgumentException(nameof (count));
      return this;
    }

    /// <summary>
    /// Filters items of the collection by specified where clause at predicate parameter.
    /// </summary>
    /// <param name="predicate">The predicate to filter by.</param>
    /// <returns>An instance of <see cref="!:ControlsFacade" /> object.</returns>
    public virtual ControlsFacade<TParentFacade> Where(
      Func<ControlData, bool> predicate)
    {
      this.controls = predicate != null ? this.controls.Where<ControlData>(predicate).AsQueryable<ControlData>() : throw new ArgumentNullException(nameof (predicate));
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
    /// <returns>An instance of <see cref="!:ControlsFacade" /> object.</returns>
    public virtual ControlsFacade<TParentFacade> SaveChanges()
    {
      base.SaveChanges();
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
    /// <returns>An instance of <see cref="!:ControlsFacade" /> object.</returns>
    public virtual ControlsFacade<TParentFacade> CancelChanges()
    {
      base.CancelChanges();
      return this;
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) this.PageManager;
  }
}
