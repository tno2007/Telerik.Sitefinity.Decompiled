// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Mvc.Proxy.MvcProxyBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Mvc.Proxy
{
  /// <summary>Represents the base MVC proxy control.</summary>
  [TypeDescriptionProvider(typeof (MvcProxyDescriptionProvider))]
  [ParseChildren(ChildrenAsProperties = true, DefaultProperty = "SerializedSettings")]
  public abstract class MvcProxyBase : Control
  {
    private string serializedSettings;
    internal const string PageTemplateFrameworkKey = "PageTemplateFramework";
    private string controllerName;
    private string contentTypeName;
    private bool? isInPureMode;

    /// <summary>
    /// Gets or sets the serialized settings of the controller.
    /// </summary>
    public virtual string ControllerSettings { get; set; }

    /// <summary>
    /// Gets or sets the name of the controller that is being executed by through the control.
    /// </summary>
    [PropertyPersistence(IsKey = true)]
    [NonMultilingual]
    public virtual string ControllerName
    {
      get => this.controllerName;
      set => this.controllerName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the parent page is rendered in <see cref="T:Telerik.Sitefinity.Pages.Model.PageTemplateFramework" /> MVC mode.
    /// </summary>
    public virtual bool IsInPureMode
    {
      get
      {
        if (this.isInPureMode.HasValue)
          return this.isInPureMode.Value;
        if (HttpContext.Current != null && HttpContext.Current.Items != null)
        {
          PageTemplateFramework? nullable = HttpContext.Current.Items[(object) "PageTemplateFramework"] as PageTemplateFramework?;
          if (nullable.HasValue)
            return nullable.Value == PageTemplateFramework.Mvc;
        }
        return false;
      }
      set => this.isInPureMode = new bool?(value);
    }

    /// <summary>Gets or sets the page title set by the controller.</summary>
    /// <remarks>
    /// The title of the page is set by setting the
    /// Title property of the ViewBag in the controller.
    /// </remarks>
    public virtual string PageTitle { get; set; }

    /// <summary>
    /// Gets or sets a dynamic object which is used to persist the properties of the controller.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
    public virtual object Settings { get; set; }

    /// <summary>
    /// Gets or sets the serialized properties of the controller.
    /// </summary>
    public virtual string SerializedSettings
    {
      get => this.serializedSettings;
      set => this.serializedSettings = value;
    }

    /// <summary>
    /// Gets or sets the name of the content type that the control will be handling.
    /// </summary>
    [Browsable(true)]
    public virtual string ContentTypeName
    {
      get => this.contentTypeName;
      set => this.contentTypeName = value;
    }

    /// <summary>
    /// Gets or sets the page description set by the controller
    /// </summary>
    /// <remarks>
    /// The description of the page is set by setting the
    /// Description property of the ViewBag in the controller
    /// </remarks>
    public virtual string PageDescription { get; set; }

    /// <summary>Gets or sets the page keywords set by the controller</summary>
    /// <remarks>
    /// The keywords of the page are set by setting the
    /// Keywords property of the ViewBag in the controller
    /// </remarks>
    public virtual string PageKeywords { get; set; }

    /// <summary>Gets or sets the control data identifier.</summary>
    /// <value>The control data identifier.</value>
    public virtual string ControlDataId { get; set; }

    /// <summary>
    /// Gets or sets the instance of the <see cref="P:Telerik.Sitefinity.Mvc.Proxy.MvcProxyBase.RequestContext" /> which is built
    /// from the original <see cref="P:Telerik.Sitefinity.Mvc.Proxy.MvcProxyBase.RequestContext" /> and modified for the MVC controllers
    /// to use it.
    /// </summary>
    [Obsolete("Use HttpContext.Current.Request.RequestContext instead.")]
    public virtual RequestContext RequestContext { get; set; }
  }
}
