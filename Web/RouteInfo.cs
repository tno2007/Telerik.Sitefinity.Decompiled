// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.RouteInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.Routing;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Represents child route handler.</summary>
  public sealed class RouteInfo
  {
    private bool initialized;
    private IModule module;
    private string key;
    private Type handlerType;
    private string handlerName;
    private RouteInfo parent;
    private RouteCollection routes;
    private RouteHandlerBase rootHandler;

    /// <summary>Gets or sets the key.</summary>
    /// <value>The key.</value>
    public string Key
    {
      get
      {
        if (!string.IsNullOrEmpty(this.key))
          return this.key;
        return this.HandlerType != (Type) null ? this.HandlerType.FullName + this.HandlerName : this.HandlerName;
      }
      set => this.key = value;
    }

    /// <summary>
    /// Gets or sets the type of the class implementing <see cref="T:Telerik.Sitefinity.Web.IPartialRouteHandler" /> interface.
    /// </summary>
    public Type HandlerType
    {
      get
      {
        this.Init();
        return this.handlerType;
      }
      set => this.handlerType = value;
    }

    /// <summary>The name of the route handler</summary>
    public string HandlerName
    {
      get => this.handlerName;
      set => this.handlerName = value;
    }

    /// <summary>The parent route info.</summary>
    public RouteInfo Parent
    {
      get => this.parent;
      set => this.parent = value;
    }

    /// <summary>A or sets collection of routes.</summary>
    public RouteCollection Routes
    {
      get
      {
        this.Init();
        return this.routes;
      }
      set => this.routes = value;
    }

    /// <summary>Gets or sets the root handler.</summary>
    public RouteHandlerBase RootHandler
    {
      get => this.rootHandler;
      set => this.rootHandler = value;
    }

    /// <summary>Gets or sets the module.</summary>
    /// <value>The module.</value>
    public IModule Module
    {
      get => this.module;
      set => this.module = value;
    }

    private void Init()
    {
      if (this.initialized)
        return;
      if (this.module != null && this.routes == null)
      {
        if (this.module is InactiveModule)
          this.module = SystemManager.GetApplicationModule(this.module.Name);
        if (this.module != null && this.module.GetControlPanel() is IPartialRouteHandler controlPanel)
        {
          this.HandlerName = controlPanel.Name;
          this.HandlerType = controlPanel.GetType();
          this.Routes = controlPanel.CreateRoutes();
        }
      }
      this.initialized = true;
    }
  }
}
