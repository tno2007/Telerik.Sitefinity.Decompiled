// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.MasterDetailView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend
{
  /// <summary>View control holding one master and one detail views.</summary>
  public class MasterDetailView : ViewBase
  {
    private Control masterView;
    private Control detailView;
    private const string script = "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.MasterDetailView.js";
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentUI.MasterDetailView.ascx");

    /// <summary>
    /// Gets the default name of the layout template.
    /// It will be used if there is no LayoutTemplateName in the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewDefinition" /> definition.
    /// </summary>
    /// <value>The default name of the layout template.</value>
    public virtual string DefaultLayoutTemplateName => MasterDetailView.layoutTemplateName;

    /// <summary>Gets the master view place holder.</summary>
    /// <value>The master view place holder.</value>
    protected Control MasterViewPlaceHolder => this.Container.GetControl<Control>("masterViewPlaceHolder", true);

    /// <summary>Gets the detail view place holder.</summary>
    /// <value>The detail view place holder.</value>
    protected Control DetailViewPlaceHolder => this.Container.GetControl<Control>("detailViewPlaceHolder", true);

    /// <summary>Gets the toolbar for this view.</summary>
    /// <value>The toolbar.</value>
    protected virtual WidgetBar Toolbar => this.Container.GetControl<WidgetBar>("toolbar", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      if (!(definition is IContentViewMasterDetailDefinition detailDefinition))
        throw new NotSupportedException("The definition is not of type IContentViewMasterDetailDefinition.");
      this.LoadMasterView(detailDefinition.MasterDefinition);
      this.LoadDetailView(detailDefinition.DetailDefinition);
    }

    /// <summary>Loads the view specified master view in its holder.</summary>
    /// <param name="viewName">Name of the view.</param>
    protected virtual void LoadMasterView(IContentViewMasterDefinition masterDefinition)
    {
      this.masterView = this.LoadView((IContentViewDefinition) masterDefinition);
      this.MasterViewPlaceHolder.Controls.Add(this.masterView);
      this.SetMasterViewToolbar();
    }

    /// <summary>Loads the view specified detail view in its holder.</summary>
    /// <param name="viewName">Name of the view.</param>
    protected virtual void LoadDetailView(IContentViewDetailDefinition detailDefinition)
    {
      this.detailView = this.LoadView((IContentViewDefinition) detailDefinition);
      this.DetailViewPlaceHolder.Controls.Add(this.detailView);
    }

    /// <summary>Loads the view.</summary>
    /// <param name="masterDefinition">The master definition.</param>
    private Control LoadView(IContentViewDefinition viewDefinition)
    {
      if (viewDefinition == null)
        throw new ArgumentNullException(nameof (viewDefinition));
      Control control = (Control) null;
      if (!string.IsNullOrEmpty(viewDefinition.ViewVirtualPath))
        control = this.LoadUserControl(viewDefinition.ViewVirtualPath);
      else if (viewDefinition.ViewType != (Type) null)
        control = this.LoadControl(viewDefinition.ViewType);
      if (control == null)
        throw new Exception("Control could not be loaded from the specified definition.");
      if (control is IViewControl)
      {
        ((IViewControl) control).Host = this.Host;
        ((IViewControl) control).Definition = viewDefinition;
      }
      return control;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? this.DefaultLayoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Loads the control from the virtual path (user control).
    /// </summary>
    /// <param name="virtualPath">Virtual path of the control to be loaded.</param>
    /// <returns>An instance of the control.</returns>
    private Control LoadUserControl(string virtualPath) => (Control) ControlUtilities.LoadControl(virtualPath);

    /// <summary>Loads the control from the type.</summary>
    /// <param name="type">Type of the control that should be loaded.</param>
    /// <returns>An instance of the control.</returns>
    private Control LoadControl(Type type) => this.Page.LoadControl(type, (object[]) null);

    /// <summary>
    /// This is transfering the MasterGridView toolbar to the MasterDetailView toolbar.
    /// </summary>
    private void SetMasterViewToolbar()
    {
      if (!(this.masterView is MasterGridView))
        return;
      ((MasterGridView) this.masterView).Toolbar = this.Toolbar;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      MasterDetailView masterDetailView = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(masterDetailView.GetType().FullName, masterDetailView.ClientID);
      controlDescriptor.AddComponentProperty("detailView", masterDetailView.detailView.ClientID);
      controlDescriptor.AddComponentProperty("masterView", masterDetailView.masterView.ClientID);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (ScriptDescriptor) controlDescriptor;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.MasterDetailView.js", typeof (MasterDetailView).Assembly.FullName)
    };
  }
}
