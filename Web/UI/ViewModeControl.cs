// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ViewModeControl`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Base class for multi view controls.</summary>
  /// <typeparam name="THost">
  /// Defines the host of the current instance.
  /// Usually the host is the immediate parent of the current control.
  /// If the host is at no importance, <see cref="T:System.Web.UI.Control" /> type maybe specified.
  /// </typeparam>
  public abstract class ViewModeControl<THost> : CompositeControl, IViewModeControl, IViewHostControl
    where THost : Control
  {
    private string name;
    private string title;
    private string description;
    private string paramKey;
    private string parentIdKey;
    private string viewMode;
    private ITemplate layoutTemplate;
    private GenericContainer container;
    private THost host;
    private IViewInfo currentView;
    private Type assemblyInfo;
    private string defaultView;
    private ViewCollection views;
    private static ControlsConfig config = Config.Get<ControlsConfig>();

    /// <summary>The name of the view.</summary>
    public virtual string Name
    {
      get
      {
        if (this.name == null)
          this.name = this.GetType().Name;
        return this.name;
      }
      set => this.name = value;
    }

    /// <summary>The title of the view.</summary>
    public virtual string Title
    {
      get => this.title;
      set => this.title = value;
    }

    /// <summary>Description of the view.</summary>
    public virtual string Description
    {
      get => this.description;
      set => this.description = value;
    }

    /// <summary>Gets or sets the current View Mode.</summary>
    public virtual string ViewMode
    {
      get
      {
        if (this.viewMode == null)
        {
          if (this is IPartialRouteHandler partialRouteHandler && partialRouteHandler.PartialRequestContext != null)
          {
            string routeHandlerName = partialRouteHandler.PartialRequestContext.RouteHandlerName;
            if (!string.IsNullOrEmpty(routeHandlerName))
            {
              string str = routeHandlerName + "View";
              foreach (IViewInfo view in (Collection<IViewInfo>) this.Views)
              {
                if (view.ViewName.Equals(routeHandlerName) || view.ViewName.Equals(str))
                  return view.ViewName;
              }
            }
          }
          if (this.viewMode == null)
            this.viewMode = this.DefaultViewMode;
        }
        return this.viewMode;
      }
      set
      {
        this.viewMode = value;
        this.currentView = (IViewInfo) null;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets the type of the view mode.</summary>
    /// <value>The type of the view mode.</value>
    [Browsable(false)]
    public virtual Type ViewModeType => this.GetViewType(this.ViewMode);

    /// <summary>Gets the default View Mode name.</summary>
    public virtual string DefaultViewMode
    {
      get
      {
        if (this.defaultView == null && this.Views.Count > 0)
          this.defaultView = this.Views[0].ViewName;
        return this.defaultView;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("viewName");
        this.defaultView = this.GetViewInfo(value) != null ? value : throw new ArgumentException(Res.Get<ErrorMessages>("ViewModeNotDefined", (object) value));
      }
    }

    /// <summary>
    /// Gets or sets the global resource class ID to use for localized strings.
    /// </summary>
    public virtual string ResourceClassId { get; set; }

    /// <summary>Gets the key for passing view mode name in the URL.</summary>
    public virtual string ViewModeKey => this.Name;

    /// <summary>Gets the key for passing parameter in the URL.</summary>
    public virtual string ParameterKey
    {
      get
      {
        if (this.paramKey == null)
          this.paramKey = "Param";
        return this.paramKey;
      }
    }

    /// <summary>Gets the parent pageId key.</summary>
    /// <value>The parent pageId key.</value>
    public virtual string ParentIdKey
    {
      get
      {
        if (this.parentIdKey == null)
          this.parentIdKey = "ParentId";
        return this.parentIdKey;
      }
    }

    /// <summary>
    /// When overridden, provides a key for loading additional external template
    /// instead of the default one.
    /// </summary>
    protected virtual string AdditionalTemplateKey => string.Empty;

    /// <summary>
    /// Gets a collection of available Views for this control.
    /// </summary>
    [Browsable(false)]
    public virtual ViewCollection Views
    {
      get
      {
        if (this.views == null)
        {
          this.views = new ViewCollection();
          this.CreateViews();
          this.InitializeViews();
        }
        return this.views;
      }
    }

    /// <summary>Gets the current view info.</summary>
    [Browsable(false)]
    protected virtual IViewInfo CurrentView
    {
      get
      {
        if (this.currentView == null && this.Views.Count > 0)
        {
          this.currentView = this.GetViewInfo(this.ViewMode);
          if (this.currentView == null)
            throw new InvalidOperationException(Res.Get<ErrorMessages>("ViewModeNotDefined", (object) this.ViewMode));
        }
        return this.currentView;
      }
    }

    /// <summary>Gets or sets the owner of the current instance.</summary>
    [Browsable(false)]
    public virtual THost Host
    {
      get
      {
        if ((object) this.host == null && this.Parent is THost)
          this.host = (THost) this.Parent;
        return this.host;
      }
      set => this.host = value;
    }

    [Browsable(false)]
    Control IViewHostControl.Host
    {
      get => (Control) this.Host;
      set => this.Host = (THost) value;
    }

    /// <summary>
    /// Gets or sets the path to a custom layout template for the control.
    /// </summary>
    public virtual string LayoutTemplatePath
    {
      get
      {
        string layoutTemplatePath = this.ViewState[nameof (LayoutTemplatePath)] as string;
        if (string.IsNullOrEmpty(layoutTemplatePath) && string.IsNullOrEmpty(this.LayoutTemplateName))
          layoutTemplatePath = this.DefaultLayoutTemplatePath;
        return layoutTemplatePath;
      }
      set => this.ViewState[nameof (LayoutTemplatePath)] = (object) value;
    }

    /// <summary>
    /// Default value of LayouteTemplatePath when its value is not yet set and LayouteTemplateName is null/empty
    /// </summary>
    protected virtual string DefaultLayoutTemplatePath => (string) null;

    /// <summary>
    /// Gets or sets the path to a custom layout template for the control.
    /// </summary>
    [Browsable(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DefaultValue(typeof (ITemplate), "")]
    [DescriptionResource(typeof (PageResources), "LayoutTemplateDescription")]
    [TemplateContainer(typeof (GenericContainer))]
    public virtual ITemplate LayoutTemplate
    {
      get
      {
        if (this.layoutTemplate == null)
          this.layoutTemplate = this.CreateLayoutTemplate();
        return this.layoutTemplate;
      }
      set => this.layoutTemplate = value;
    }

    /// <summary>
    /// Gets an instance of <see cref="T:Telerik.Sitefinity.Web.UI.GenericContainer" /> object that will be used as
    /// container control for the layout template.
    /// </summary>
    [Browsable(false)]
    public virtual GenericContainer Container
    {
      get
      {
        if (this.container == null)
        {
          this.container = this.CreateContainer();
          if (this.LayoutTemplate != null)
            this.LayoutTemplate.InstantiateIn((Control) this.container);
        }
        return this.container;
      }
    }

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    protected virtual string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the type from the assembly containing the embedded resources.
    /// </summary>
    public virtual Type AssemblyInfo
    {
      get => this.assemblyInfo;
      set => this.assemblyInfo = value;
    }

    /// <summary>
    /// Gets the HtmlTextWriterTag value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected virtual void InitializeControls(Control viewContainer)
    {
      if (this.Views.Count <= 0)
        return;
      Control child = this.LoadCurrentView((Control) this);
      if (child == null)
        return;
      viewContainer.Controls.Add(child);
    }

    /// <summary>Loads the current view.</summary>
    /// <param name="hostControl">
    /// Specifies the control that will host the view.
    /// By default a reference to this instance will be passed.
    /// Host is assigned only if the view implements <see cref="T:Telerik.Sitefinity.Web.UI.IViewHostControl" /> interface.
    /// </param>
    /// <returns>Control</returns>
    protected virtual Control LoadCurrentView(Control hostControl)
    {
      if (this.CurrentView == null)
        return (Control) null;
      Control control = this.CurrentView.LoadView();
      if (!(control is IViewHostControl viewHostControl))
        return control;
      viewHostControl.Host = hostControl;
      return control;
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based
    /// implementation to create any child controls they contain in preparation for
    /// posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      Control control = this.Container.GetControl<Control>("SubViewHolder", false) ?? (Control) this.Container;
      this.InitializeControls(control);
      this.Controls.Add(control);
      this.Controls.Add((Control) new UserPreferences());
    }

    /// <summary>
    /// Creates an instance of container control.
    /// The container must inherit from <see cref="T:Telerik.Sitefinity.Web.UI.GenericContainer" />.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Web.UI.GenericContainer" /> object.
    /// </returns>
    protected virtual GenericContainer CreateContainer() => new GenericContainer();

    /// <summary>
    /// Creates a layout template from a specified
    /// user control (external template) or embedded resource.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:System.Web.UI.ITemplate" /> object.
    /// </returns>
    protected virtual ITemplate CreateLayoutTemplate() => ControlUtilities.GetTemplate(new TemplateInfo()
    {
      TemplatePath = this.LayoutTemplatePath,
      TemplateName = this.LayoutTemplateName,
      TemplateResourceInfo = this.AssemblyInfo,
      ControlType = this.GetType(),
      ConfigAdditionalKey = this.AdditionalTemplateKey
    });

    /// <summary>Loads configured views.</summary>
    protected virtual void CreateViews()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void InitializeViews()
    {
      Type type = this.GetType();
      if (!ViewModeControl<THost>.config.ViewMap.ContainsKey(type))
        return;
      foreach (ViewElement view in ViewModeControl<THost>.config.ViewMap[type].Views)
      {
        string collectionItemName = view.CollectionItemName;
        if (!(collectionItemName == "add"))
        {
          if (!(collectionItemName == "remove"))
          {
            if (collectionItemName == "clear")
              this.views.Clear();
          }
          else
            this.RemoveView(view.Name);
        }
        else if (!string.IsNullOrEmpty(view.VirtualPath))
          this.AddView(view.Name, view.VirtualPath, view.Title, view.Description, (string) null);
        else
          this.AddView(view.ViewType, view.Name, view.Title, view.Description, (string) null);
      }
    }

    /// <summary>Sets the default view for this host control.</summary>
    /// <param name="viewName">The name of the view to set.</param>
    public void SetDefaultView(string viewName)
    {
      if (viewName == null)
        throw new ArgumentNullException(nameof (viewName));
      this.defaultView = this.GetViewInfo(viewName) != null ? viewName : throw new ArgumentException(Res.Get<ErrorMessages>("ViewModeNotDefined", (object) viewName));
    }

    /// <summary>
    /// Adds a view to the collection of available views for this control.
    /// </summary>
    /// <param name="viewName">The name of the view.</param>
    /// <param name="virtualPath">The virtual path to the view file.</param>
    /// <param name="viewTitle">The title of the view.</param>
    /// <param name="viewDescription">Description of the view.</param>
    /// <param name="viewCommandCssClass">
    /// CSS class name for the command generated for the view.
    /// CSS class is usually used to associate an image (icon) with the view.
    /// </param>
    public void AddView(
      string viewName,
      string virtualPath,
      string viewTitle,
      string viewDescription,
      string viewCommandCssClass)
    {
      if (viewName == null)
        throw new ArgumentNullException(nameof (viewName));
      this.Views.Add((IViewInfo) new ViewModeControl<THost>.UserControlViewInfo(BuildManager.GetCompiledType(virtualPath), viewName, virtualPath, viewTitle, viewDescription, viewCommandCssClass));
    }

    /// <summary>
    /// Adds a view to the collection of available views for this control.
    /// </summary>
    /// <typeparam name="TView">The type of the view to add.</typeparam>
    /// <param name="viewName">The name of the view.</param>
    /// <param name="viewTitle">The title of the view.</param>
    /// <param name="viewDescription">Description of the view.</param>
    /// <param name="viewCommandCssClass">
    /// CSS class name for the command generated for the view.
    /// CSS class is usually used to associate an image (icon) with the view.
    /// </param>
    public void AddView<TView>(
      string viewName,
      string viewTitle,
      string viewDescription,
      string viewCommandCssClass)
      where TView : Control, new()
    {
      if (string.IsNullOrEmpty(viewName))
        viewName = typeof (TView).Name;
      this.Views.Add((IViewInfo) new ViewModeControl<THost>.CustomControlViewInfo<TView>(viewName, viewTitle, viewDescription, viewCommandCssClass));
    }

    /// <summary>
    /// Adds a view to the collection of available views for this control.
    /// </summary>
    /// <param name="type">The type of the view to add.</param>
    /// <param name="viewName">The name of the view.</param>
    /// <param name="viewTitle">The title of the view.</param>
    /// <param name="viewDescription">Description of the view.</param>
    /// <param name="viewCommandCssClass">
    /// CSS class name for the command generated for the view.
    /// CSS class is usually used to associate an image (icon) with the view.
    /// </param>
    public void AddView(
      Type type,
      string viewName,
      string viewTitle,
      string viewDescription,
      string viewCommandCssClass)
    {
      if (string.IsNullOrEmpty(viewName))
        viewName = type.Name;
      this.Views.Add((IViewInfo) new ViewModeControl<THost>.CustomControlViewInfo(type, viewName, viewTitle, viewDescription, viewCommandCssClass));
    }

    /// <summary>
    /// Adds a view to the collection of available views for this control.
    /// </summary>
    /// <typeparam name="TView">The type of the view to add.</typeparam>
    public void AddView<TView>() where TView : Control, new() => this.AddView<TView>((string) null, (string) null, (string) null, (string) null);

    /// <summary>
    /// Removes a view form the collection of available views.
    /// </summary>
    /// <typeparam name="TView">The type of the view to remove.</typeparam>
    public void RemoveView<TView>() => this.RemoveView(typeof (TView).Name);

    /// <summary>
    /// Removes a view form the collection of available views.
    /// </summary>
    /// <param name="viewName">The name of the view to remove.</param>
    public void RemoveView(string viewName)
    {
      if (string.IsNullOrEmpty(viewName))
        return;
      this.Views.Remove(viewName);
    }

    /// <summary>Gets information about the specified view.</summary>
    /// <param name="viewName">The name of the view.</param>
    /// <returns>An <see cref="T:Telerik.Sitefinity.Web.UI.IViewInfo" /> object.</returns>
    protected IViewInfo GetViewInfo(string viewName)
    {
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentNullException(nameof (viewName));
      foreach (IViewInfo view in (Collection<IViewInfo>) this.Views)
      {
        if (view.ViewName.Equals(viewName, StringComparison.OrdinalIgnoreCase))
          return view;
      }
      return (IViewInfo) null;
    }

    /// <summary>Gets the type of the specified view.</summary>
    /// <param name="viewName">The name of the view.</param>
    /// <returns>An <see cref="T:System.Type" /> object.</returns>
    protected Type GetViewType(string viewName)
    {
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentNullException(nameof (viewName));
      foreach (IViewInfo view in (Collection<IViewInfo>) this.Views)
      {
        if (view.ViewName == viewName)
          return view.ViewType;
      }
      return (Type) null;
    }

    /// <summary>
    /// Proviedes information for creating User Control Views for ViewModeControl based controls.
    /// </summary>
    protected class UserControlViewInfo : IViewInfo
    {
      private string viewName;
      private string virtualPath;
      private string title;
      private string description;
      private string viewCommandCssClass;

      /// <summary>Creates an instance of UserControlViewInfo class.</summary>
      /// <param name="controlType"></param>
      /// <param name="viewName">The name of the view.</param>
      /// <param name="virtualPath">Specifies the virtual path for the User Control</param>
      /// <param name="title"></param>
      /// <param name="description"></param>
      /// <param name="viewCommandCssClass"></param>
      public UserControlViewInfo(
        Type controlType,
        string viewName,
        string virtualPath,
        string title,
        string description,
        string viewCommandCssClass)
      {
        this.viewName = viewName;
        this.virtualPath = virtualPath;
        this.title = title;
        this.description = description;
        this.viewCommandCssClass = viewCommandCssClass;
        this.ViewType = controlType;
      }

      /// <summary>The type of the view.</summary>
      public Type ViewType { get; private set; }

      /// <summary>The name of the veiw.</summary>
      public string ViewName => this.viewName;

      /// <summary>The name of the view;</summary>
      public string VirtualPath => this.virtualPath;

      /// <summary>The title of the veiw.</summary>
      public string Title => this.title;

      /// <summary>The name of the veiw.</summary>
      public string Description => this.description;

      /// <summary>Gets the view command CSS class.</summary>
      /// <value>The command CSS class.</value>
      public string ViewCommandCssClass
      {
        get
        {
          if (this.viewCommandCssClass == null)
            this.viewCommandCssClass = "all";
          return this.viewCommandCssClass;
        }
      }

      /// <summary>Loads the specified view control.</summary>
      /// <returns>Control</returns>
      public virtual Control LoadView()
      {
        TemplateControl templateControl = ControlUtilities.LoadControl(this.virtualPath);
        if (!(templateControl is IViewModeControl viewModeControl))
          return (Control) templateControl;
        viewModeControl.Name = this.ViewName;
        viewModeControl.Title = this.Title;
        viewModeControl.Description = this.Description;
        return (Control) templateControl;
      }
    }

    /// <summary>
    /// Proviedes information for creating Custom Control Views for ViewModeControl based controls.
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    protected class CustomControlViewInfo<TControl> : ViewModeControl<THost>.CustomControlViewInfo
      where TControl : Control, new()
    {
      /// <summary>Creates an instance of CustomControlViewInfo class.</summary>
      /// <param name="viewName">The name of the view.</param>
      /// <param name="title"></param>
      /// <param name="description"></param>
      /// <param name="viewCommandCssClass"></param>
      public CustomControlViewInfo(
        string viewName,
        string title,
        string description,
        string viewCommandCssClass)
        : base(typeof (TControl), viewName, title, description, viewCommandCssClass)
      {
      }

      /// <summary>Loads the specified view control.</summary>
      /// <returns>Control</returns>
      public override Control LoadView()
      {
        // ISSUE: variable of a boxed type
        __Boxed<TControl> local = (object) ObjectFactory.Resolve<TControl>();
        if (!(local is IViewModeControl viewModeControl))
          return (Control) local;
        viewModeControl.Name = this.ViewName;
        viewModeControl.Title = this.Title;
        viewModeControl.Description = this.Description;
        return (Control) local;
      }
    }

    /// <summary>
    /// Proviedes information for creating Custom Control Views for ViewModeControl based controls.
    /// </summary>
    protected class CustomControlViewInfo : IViewInfo
    {
      private string viewName;
      private string title;
      private string description;
      private string viewCommandCssClass;

      /// <summary>Creates an instance of CustomControlViewInfo class.</summary>
      /// <param name="controlType"></param>
      /// <param name="viewName">The name of the view.</param>
      /// <param name="title"></param>
      /// <param name="description"></param>
      /// <param name="viewCommandCssClass"></param>
      public CustomControlViewInfo(
        Type controlType,
        string viewName,
        string title,
        string description,
        string viewCommandCssClass)
      {
        if (controlType == (Type) null)
          throw new ArgumentNullException(nameof (controlType));
        this.ViewType = typeof (Control).IsAssignableFrom(controlType) ? controlType : throw new ArgumentException("Parameter \"controlType\" must inherit from \"Sysem.Web.UI.Control\".");
        this.viewName = viewName;
        this.title = title;
        this.description = description;
        this.viewCommandCssClass = viewCommandCssClass;
      }

      /// <summary>The type of the view.</summary>
      public Type ViewType { get; private set; }

      /// <summary>The name of the veiw.</summary>
      public virtual string ViewName
      {
        get
        {
          if (this.viewName == null)
            this.viewName = this.ViewType.Name;
          return this.viewName;
        }
      }

      /// <summary>The title of the veiw.</summary>
      public virtual string Title => this.title;

      /// <summary>The name of the veiw.</summary>
      public virtual string Description => this.description;

      /// <summary>
      /// Gets the view command CSS class which is used for automatically generated command panels.
      /// </summary>
      /// <value>The view command CSS class.</value>
      public virtual string ViewCommandCssClass
      {
        get
        {
          if (this.viewCommandCssClass == null)
            this.viewCommandCssClass = "all";
          return this.viewCommandCssClass;
        }
      }

      /// <summary>Loads the specified view control.</summary>
      /// <returns>Control</returns>
      public virtual Control LoadView()
      {
        Control control = (Control) ObjectFactory.Resolve(this.ViewType);
        if (!(control is IViewModeControl viewModeControl))
          return control;
        viewModeControl.Name = this.ViewName;
        viewModeControl.Title = this.Title;
        viewModeControl.Description = this.Description;
        return control;
      }
    }
  }
}
