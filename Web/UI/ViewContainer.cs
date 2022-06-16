// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ViewContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI
{
  public class ViewContainer : CompositeControl
  {
    private string configTypeUrlKey = nameof (ConfigType);
    private string viewNameUrlKey = "ViewName";
    private ViewsModuleConfigBase configuration;
    private string controlDefinitionName;
    private IViewContainerDefinition controlDefinition;
    private const string controlDefinitionDoesNotExist = "The ViewDefinition with name '{0}' does not exist in configuration files. \r\n                        You should either add the ViewDefiniton with such name to the configuration;\r\n                        change ControlDefinitionName or load ControlDefinition programmatically by setting \r\n                        the ControlDefinition property.";

    public virtual string ConfigType { get; set; }

    internal virtual ViewsModuleConfigBase Configuration
    {
      get
      {
        if (this.configuration == null)
          this.configuration = this.GetCurrentConfig();
        return this.configuration;
      }
    }

    /// <summary>
    /// Gets or sets the name of the query string parameter that passes the template to be used.
    /// </summary>
    public virtual string ViewNameUrlKey
    {
      get => this.viewNameUrlKey;
      set => this.viewNameUrlKey = value;
    }

    /// <summary>Gets or sets the configuration type URL key.</summary>
    public virtual string ConfigTypeUrlKey
    {
      get => this.configTypeUrlKey;
      set => this.configTypeUrlKey = value;
    }

    /// <summary>
    /// Gets or sets the name of the module which initialization should be ensured prior to rendering this control.
    /// </summary>
    /// <value>The name of the module.</value>
    public virtual string ModuleName { get; set; }

    public virtual string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set
      {
        if (!(this.controlDefinitionName != value))
          return;
        this.controlDefinitionName = value;
        this.controlDefinition = (IViewContainerDefinition) null;
      }
    }

    [TypeConverter(typeof (ExpandableObjectConverter))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    internal virtual IViewContainerDefinition ControlDefinition
    {
      get
      {
        if (this.controlDefinition == null)
        {
          if (!string.IsNullOrEmpty(this.ControlDefinitionName))
          {
            if (!this.Configuration.ViewControls.ContainsKey(this.ControlDefinitionName))
              throw new InvalidOperationException(string.Format("The ViewDefinition with name '{0}' does not exist in configuration files. \r\n                        You should either add the ViewDefiniton with such name to the configuration;\r\n                        change ControlDefinitionName or load ControlDefinition programmatically by setting \r\n                        the ControlDefinition property.", (object) this.ControlDefinitionName));
            this.controlDefinition = (IViewContainerDefinition) (this.Configuration.ViewControls[this.ControlDefinitionName].GetDefinition() as ViewContainerDefinition);
          }
          else
            this.controlDefinition = (IViewContainerDefinition) new ViewContainerDefinition();
        }
        return this.controlDefinition;
      }
      set
      {
        if (this.controlDefinition != value)
          this.ChildControlsCreated = false;
        this.controlDefinition = value;
      }
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based
    /// implementation to create any child controls they contain in preparation for posting back
    /// or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      if (!this.Visible)
        return;
      this.LoadView();
    }

    /// <summary>Loads the control from the type.</summary>
    /// <param name="type">Type of the control that should be loaded.</param>
    /// <returns>An instance of the control.</returns>
    protected virtual Control LoadControl(Type type) => this.Page.LoadControl(type, (object[]) null);

    internal virtual IViewDefinition GetCurrentView()
    {
      string viewNameFromUrl = this.GetViewNameFromUrl();
      if (string.IsNullOrEmpty(viewNameFromUrl))
        return this.ControlDefinition.Views.First<IViewDefinition>();
      IViewDefinition view;
      if (!this.ControlDefinition.TryGetView(viewNameFromUrl, out view))
        throw new ArgumentOutOfRangeException("viewName");
      return view;
    }

    private ViewsModuleConfigBase GetCurrentConfig()
    {
      TypeResolutionService resolutionService = new TypeResolutionService();
      string name = SystemManager.CurrentHttpContext.Request.QueryStringGet(this.ConfigTypeUrlKey);
      return Config.Get(name.IsNullOrEmpty() ? resolutionService.GetType(this.ConfigType) : resolutionService.GetType(name)) as ViewsModuleConfigBase;
    }

    protected virtual void LoadView()
    {
      IViewDefinition currentView = this.GetCurrentView();
      Control control = (Control) null;
      if (currentView.ViewType != (Type) null)
        control = this.LoadControl(currentView.ViewType);
      if (control == null)
        return;
      string name = string.IsNullOrEmpty(currentView.ControlId) ? currentView.ViewName : currentView.ControlId;
      if (!string.IsNullOrEmpty(name))
        ControlUtilities.SetControlIdFromName(name, control);
      if (control.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      if (control is IDefinedControl definedControl)
        definedControl.Definition = currentView;
      this.Controls.Add(control);
    }

    /// <summary>Gets the name of the view from the url parameters</summary>
    /// <returns>Name of the view if found; otherwise null</returns>
    protected internal virtual string GetViewNameFromUrl() => SystemManager.CurrentHttpContext.Request.QueryStringGet(this.ViewNameUrlKey);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
