// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Pages.ControlFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Fluent.Pages
{
  /// <summary>
  /// Fluent API that provides most common functionality related to a Sitefinity control.
  /// </summary>
  public class ControlFacade : 
    BaseFacadeWithManager,
    IItemFacade<ControlFacade, ControlData>,
    IFacade<ControlFacade>
  {
    private ControlData controlData;
    private AppSettings appSettings;
    private PageManager pageManager;
    private PageDraftFacade parentFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    public ControlFacade(AppSettings appSettings)
      : base(appSettings)
    {
      this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" /> class.
    /// </summary>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    public ControlFacade(PageDraftFacade parentFacade, AppSettings appSettings)
      : this(appSettings)
    {
      this.parentFacade = parentFacade;
    }

    /// <summary>
    /// Gets an instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.ControlFacade.PageManager" /> to be used by this facade.
    /// </summary>
    /// <value>An initialized instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.ControlFacade.PageManager" /> class.</value>
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
    /// Creates a new instance of type <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object at the specified zone.
    /// </summary>
    /// <param name="control">The control instance</param>
    /// <param name="zoneName">The name of the zone at which control need to be created in.</param>
    /// <returns>An instance of type <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" />.</returns>
    public virtual ControlFacade CreateNew(Control control, string zoneName)
    {
      PageDraftControl control1 = this.PageManager.CreateControl<PageDraftControl>(control, zoneName, false);
      this.controlData = (ControlData) control1;
      this.parentFacade.Get().Controls.Add(control1);
      return this;
    }

    /// <summary>
    /// Creates a new instance of type  <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" />
    /// </summary>
    /// <returns>An instance of type <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" />.</returns>
    public virtual ControlFacade CreateNew()
    {
      PageDraftControl control = this.PageManager.CreateControl<PageDraftControl>(false);
      this.controlData = (ControlData) control;
      this.parentFacade.Get().Controls.Add(control);
      return this;
    }

    /// <summary>Creates a new instance of control with specified id.</summary>
    /// <param name="itemId">The id of control to be created.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" />.</returns>
    public virtual ControlFacade CreateNew(Guid itemId)
    {
      PageDraftControl control = this.PageManager.CreateControl<PageDraftControl>(itemId, false);
      this.controlData = (ControlData) control;
      this.parentFacade.Get().Controls.Add(control);
      return this;
    }

    /// <summary>
    /// Creates a new instance of type <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object at specified zone and position.
    /// </summary>
    /// <param name="control">The control instance</param>
    /// <param name="zoneName">The name of the zone at which control need to be created in.</param>
    /// <param name="position">The position.</param>
    /// <returns></returns>
    public virtual ControlFacade CreateNew(
      Control control,
      string zoneName,
      int position)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Performs an arbitrary action on the instance of type <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object.
    /// </summary>
    /// <typeparam name="TControl">The type of the control.</typeparam>
    /// <param name="action">The action to be performed.</param>
    /// <param name="control">The instance of the control on which action will be performed.</param>
    /// <returns>
    /// An instance of type <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the ControlData object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    public virtual ControlFacade Do<TControl>(
      Action<ControlData, TControl> action,
      TControl control)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      this.EnsureState();
      action(this.controlData, control);
      return this;
    }

    /// <summary>
    /// Performs an arbitrary action on the <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object which is set as current control at Fluent API facade.
    /// </summary>
    /// <param name="setAction">An action to be performed on the <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> has not been initialized either through Facade(Guid pageId) constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" />object.</returns>
    public virtual ControlFacade Do(Action<ControlData> setAction)
    {
      if (setAction == null)
        throw new ArgumentNullException(nameof (setAction));
      this.EnsureState();
      setAction(this.controlData);
      return this;
    }

    /// <summary>
    /// Returns the parent facade that initialized this child facade.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if method is called and parentFacade is null; meaning that facade is not a child facade in this context.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> that initialized this facade as a child facade.</returns>
    public PageDraftFacade Done() => this.parentFacade != null ? this.parentFacade : throw new InvalidOperationException("Done method can be called only when the facade has been initialized as a child facade.");

    /// <summary>
    /// Deletes object of type <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> which is set as current control at Fluent API facade.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of  <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" /> object.</returns>
    public virtual ControlFacade Delete()
    {
      this.EnsureState();
      this.PageManager.Delete(this.controlData);
      return this;
    }

    /// <summary>
    /// Moves the control by specified number of places in the direction specified by the <see cref="!:Telerik.Sitefinity.Fluent.Move" />
    /// enumeration.
    /// </summary>
    /// <param name="move">
    /// A value representing the direction in which the control ought to be moved.
    /// </param>
    /// <param name="numberOfPlaces">
    /// Number of places by which the control ought to be moved.
    /// </param>
    /// <remarks>
    /// If the numer of places is larger than the number of pages in the given direction, the control will be moved to
    /// the first or last place the the level - depending on the <see cref="!:Telerik.Sitefinity.Fluent.Move" /> direction.
    /// </remarks>
    /// <exception cref="T:System.ArgumentException">thrown if parameter numberOfPlaces is not larger than 0.</exception>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" /> object. </returns>
    public virtual ControlFacade Move(Telerik.Sitefinity.Modules.Pages.Move move, int numberOfPlaces) => throw new NotImplementedException();

    /// <summary>
    /// Sets the zone of  the instance of type <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object.
    /// </summary>
    /// <param name="zoneName">Name of the zone.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" /> object.</returns>
    public virtual ControlFacade SetZone(string zoneName)
    {
      if (string.IsNullOrEmpty(zoneName))
        throw new ArgumentNullException(nameof (zoneName));
      this.EnsureState();
      this.controlData.PlaceHolder = zoneName;
      return this;
    }

    /// <summary>
    /// Returns the control currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object.</returns>
    public ControlData Get() => this.controlData;

    /// <summary>
    /// Sets the specified item parameter of type <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> as current control at Fluent API facade.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" /> object.</returns>
    public virtual ControlFacade Set(ControlData item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      this.controlData = this.PageManager.GetControl<ControlData>(item.Id);
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
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" /> object.</returns>
    public virtual ControlFacade SaveChanges()
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
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.ControlFacade" /> object.</returns>
    public virtual ControlFacade CancelChanges()
    {
      base.CancelChanges();
      return this;
    }

    /// <summary>
    /// Ensures that the state of the facade has been initialized and throws an exception if not.
    /// </summary>
    private void EnsureState()
    {
      if (this.controlData == null)
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
