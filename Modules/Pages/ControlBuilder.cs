// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.ControlBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web.Compilation;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>Control Builder</summary>
  [DebuggerDisplay("[ControlBuilder] PlaceHolder={PlaceHolder}, ControlType={controlType}")]
  public class ControlBuilder
  {
    private bool isPartialRouteHandler;
    private bool isUserControl;
    private string virtualPath;
    private string controllerName;
    private PropertyDescriptor controllerNameDescriptor;
    private List<PropertyBuilder> properties;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.ControlBuilder" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public ControlBuilder(ObjectData data)
      : this(data, new Guid())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.ControlBuilder" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="pageDataId">The Id of the current page data.</param>
    public ControlBuilder(ObjectData data, Guid pageDataId)
    {
      this.isUserControl = data.ObjectType.StartsWith("~/");
      Type type;
      if (this.isUserControl)
      {
        this.virtualPath = data.ObjectType;
        type = BuildManager.GetCompiledType(this.virtualPath);
      }
      else
      {
        this.ControlType = TypeResolutionService.ResolveType(data.ObjectType, true);
        type = this.ControlType;
      }
      this.isPartialRouteHandler = typeof (IPartialRouteHandler).IsAssignableFrom(type) || type == typeof (ControlPanelBuilder);
      Control instance = (Control) null;
      if (data is ControlData controlData)
      {
        this.Shared = controlData.Shared;
        this.PlaceHolder = controlData.PlaceHolder;
        this.SiblingId = controlData.SiblingId;
        this.ControlId = controlData.Id;
        this.ContainerType = controlData.ContainerType;
        if (controlData.AllowSecurityTrimming)
        {
          List<Guid> guidList = new List<Guid>(controlData.Permissions.Count);
          foreach (Telerik.Sitefinity.Security.Model.Permission permission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) controlData.Permissions)
          {
            if (permission.SetName == "General")
            {
              if (permission.IsGranted("View"))
                guidList.Add(permission.PrincipalId);
            }
          }
          this.Roles = (IList<Guid>) guidList;
        }
        this.IsPersonalized = controlData.IsPersonalized;
        this.PageDataId = pageDataId;
        if (this.ControlType != (Type) null && typeof (MvcProxyBase).IsAssignableFrom(this.ControlType))
          this.controllerName = controlData.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name.Equals("ControllerName") && !p.Value.IsNullOrEmpty())).Value;
        instance = PageManager.GetManager().LoadControl((ObjectData) controlData, (CultureInfo) null);
      }
      if (data is ISecuredObject source)
        this.SecurityObject = source.CreateProxy();
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
      this.properties = new List<PropertyBuilder>(properties.Count);
      foreach (ControlProperty property in data.GetProperties(true))
      {
        PropertyDescriptor descriptor = properties.Find(property.Name, true);
        if (descriptor != null)
        {
          TypeConverter converter = descriptor.Converter;
          if (converter.CanConvertFrom(typeof (string)) || converter.GetPropertiesSupported() || property.HasListItems())
          {
            this.properties.Add(new PropertyBuilder(property, descriptor, (object) instance));
            if (property.Name.Equals("ControllerName"))
              this.controllerNameDescriptor = descriptor;
          }
        }
      }
      this.CollectionIndex = data.CollectionIndex;
    }

    /// <summary>Gets the secured object</summary>
    public ISecuredObject SecurityObject { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:Telerik.Sitefinity.Modules.Pages.ControlBuilder" /> is shared.
    /// </summary>
    /// <value><c>true</c> if shared; otherwise, <c>false</c>.</value>
    public bool Shared { get; private set; }

    /// <summary>Gets the place holder.</summary>
    /// <value>The place holder.</value>
    public string PlaceHolder { get; private set; }

    /// <summary>Gets the ID of the sibling.</summary>
    /// <value>The ID of the sibling.</value>
    public Guid SiblingId { get; private set; }

    /// <summary>Gets the ID of the control.</summary>
    /// <value>The ID of the control.</value>
    public Guid ControlId { get; private set; }

    /// <summary>Gets the culture.</summary>
    /// <value>The culture.</value>
    public CultureInfo Culture { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this instance is partial route handler.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this instance is partial route handler; otherwise, <c>false</c>.
    /// </value>
    public bool IsPartialRouteHandler => this.isPartialRouteHandler;

    /// <summary>Gets the properties.</summary>
    /// <value>The properties.</value>
    public IList<PropertyBuilder> Properties
    {
      get
      {
        if (this.properties == null)
          this.properties = new List<PropertyBuilder>();
        return (IList<PropertyBuilder>) this.properties;
      }
    }

    /// <summary>
    /// Gets a list of IDs of principals that have General View permissions for this control.
    /// </summary>
    /// <value>The roles.</value>
    public IList<Guid> Roles { get; private set; }

    /// <summary>Gets the type of the control</summary>
    public Type ControlType { get; private set; }

    /// <summary>Gets the control if created</summary>
    public Control Control { get; private set; }

    /// <summary>Gets the index of the collection.</summary>
    /// <value>The index of the collection.</value>
    internal int CollectionIndex { get; private set; }

    /// <summary>
    /// Gets the type of the container holding the control to build.
    /// </summary>
    internal Type ContainerType { get; private set; }

    /// <summary>Creates the control.</summary>
    /// <param name="page">The page.</param>
    /// <returns>The control.</returns>
    public Control CreateControl(Page page)
    {
      Control control;
      if (this.IsPersonalized && !page.IsIndexingMode() && !ControlBuilder.IsTemplatePreview(page) && !ControlBuilder.IsVersionPreview())
      {
        Type t = ObjectFactory.Resolve<IPersonalizedWidgetResolver>().ResolveWrapperType(this.ControlType);
        string str = this.ControlType.FullName;
        if (!this.controllerName.IsNullOrEmpty())
          str = this.controllerName;
        control = page.LoadControl(t, new object[3]
        {
          (object) this.ControlId,
          (object) this.PageDataId,
          (object) str
        });
        this.PupulateControllerName((object) control);
      }
      else
      {
        control = !this.isUserControl ? page.LoadControl(this.ControlType, (object[]) null) : page.LoadControl(this.virtualPath);
        this.PupulateProperties((object) control);
      }
      object behaviorObject = ControlUtilities.BehaviorResolver.GetBehaviorObject(control);
      if (typeof (IHasContainerType).IsAssignableFrom(behaviorObject.GetType()))
        (behaviorObject as IHasContainerType).ContainerType = this.ContainerType;
      this.Control = control;
      return control;
    }

    /// <summary>Creates new object.</summary>
    /// <returns>The new object.</returns>
    public object CreateObject()
    {
      object instance = Activator.CreateInstance(this.ControlType);
      this.PupulateProperties(instance);
      return instance;
    }

    internal void PupulateControllerName(object obj)
    {
      if (string.IsNullOrWhiteSpace(this.controllerName) || this.controllerNameDescriptor == null)
        return;
      this.controllerNameDescriptor.SetValue(obj, (object) this.controllerName);
    }

    internal void PupulateProperties(object obj)
    {
      foreach (PropertyBuilder orderedProperty in this.properties.GetOrderedProperties<PropertyBuilder>())
        orderedProperty.SetProperty(obj);
    }

    /// <summary>
    /// Gets a value indicating whether the control is personalized or not. If true the control is
    /// personalized; otherwise it is not personalized.
    /// </summary>
    internal bool IsPersonalized { get; private set; }

    /// <summary>Gets the page data Id.</summary>
    /// <value>The page data Id.</value>
    internal Guid PageDataId { get; private set; }

    private static bool IsVersionPreview() => SystemManager.CurrentHttpContext != null && SystemManager.CurrentHttpContext.Items.Contains((object) "versionpreview") && SystemManager.CurrentHttpContext.Items[(object) "versionpreview"] is bool && (bool) SystemManager.CurrentHttpContext.Items[(object) "versionpreview"];

    private static bool IsTemplatePreview(Page page) => page.IsPreviewMode() && page.Items.Contains((object) "IsTemplate") && page.Items[(object) "IsTemplate"] is bool && (bool) page.Items[(object) "IsTemplate"];
  }
}
