// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Pages.PageGroupFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Fluent.Pages
{
  /// <summary>
  /// Fluent API that provides most common functionality related to a page group.
  /// </summary>
  public class PageGroupFacade : BaseFacadeWithManager
  {
    private PageNode currentState;
    private PageFacade parentFacade;
    private AppSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageGroupFacade" /> class.
    /// </summary>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="currentState">The current data item for the facade.</param>
    public PageGroupFacade(PageFacade parentFacade, AppSettings appSettings, PageNode currentState)
      : base(appSettings)
    {
      if (parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (appSettings == null)
        throw new ArgumentNullException(nameof (appSettings));
      if (currentState == null)
        throw new ArgumentNullException(nameof (currentState));
      this.parentFacade = parentFacade;
      this.appSettings = appSettings;
      this.currentState = currentState;
    }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Fluent.Pages.PageGroupFacade.PageManager" /> to be used by the facade.
    /// </summary>
    public virtual PageManager PageManager => this.parentFacade.PageManager;

    /// <summary>
    /// Converts the page group node to standard page node and creates new data storage for it.
    /// </summary>
    /// <returns></returns>
    public virtual StandardPageFacade ConvertToStandardPage()
    {
      this.EnsureState();
      PageData pageData = this.currentState.GetPageData();
      if (pageData != null)
      {
        this.currentState.NodeType = NodeType.Standard;
        if (pageData.NavigationNode == null)
          pageData.NavigationNode = this.currentState;
      }
      else
      {
        if (this.currentState.NodeType != NodeType.Standard)
          this.currentState.ApprovalWorkflowState.ClearAllValues(true);
        this.PageManager.ConvertToStandardPage(this.currentState);
      }
      return new StandardPageFacade(this.parentFacade, this.appSettings, this.currentState);
    }

    /// <summary>
    /// Converts the page group node to standard page node and assigns the provided page data to it.
    /// </summary>
    /// <param name="pageData">The page data.</param>
    /// <returns></returns>
    public virtual StandardPageFacade ConvertToStandardPage(PageData pageData)
    {
      if (pageData == null)
        throw new ArgumentNullException(nameof (pageData));
      this.EnsureState();
      pageData.NavigationNode = this.currentState;
      this.PageManager.ConvertToStandardPage(this.currentState, pageData);
      return new StandardPageFacade(this.parentFacade, this.appSettings, this.currentState);
    }

    /// <summary>Returns the page currently loaded by the fluent API.</summary>
    /// <returns>An instance of <see cref="!:Node" /> object.</returns>
    public virtual PageNode Get() => this.currentState;

    /// <summary>
    /// Performs an arbitrary action on the page of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> currently loaded at Fluent API.
    /// </summary>
    /// <param name="setAction">An action to be performed on the current page loaded at Fluent API.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> has not been initialized either through PageFacade(Guid pageId) constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageGroupFacade Do(Action<PageNode> setAction)
    {
      if (setAction == null)
        throw new ArgumentNullException(nameof (setAction));
      this.EnsureState();
      if (this.currentState.NodeType == NodeType.Group)
        setAction(this.currentState);
      return this;
    }

    /// <summary>
    /// Returns the parent facade that initialized this child facade.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> that initialized this facade as a child facade.</returns>
    public virtual PageFacade Done() => this.parentFacade;

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageGroupFacade" /> object.</returns>
    public new virtual bool SaveChanges()
    {
      base.SaveChanges();
      return true;
    }

    /// <summary>
    /// Commit the changes and return the current facade for additional fluent calls
    /// </summary>
    public virtual PageGroupFacade SaveAndContinue()
    {
      this.SaveChanges();
      return this;
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageGroupFacade" /> object.</returns>
    public virtual PageGroupFacade CancelChanges()
    {
      base.CancelChanges();
      return this;
    }

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
