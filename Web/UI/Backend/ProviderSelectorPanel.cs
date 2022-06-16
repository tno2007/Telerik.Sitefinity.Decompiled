// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.ProviderSelectorPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>
  /// Represents a command panel that automatically displays availble data
  /// providers for the current module.
  /// </summary>
  public class ProviderSelectorPanel : CommandPanel
  {
    private string selectedProvider;
    private string[] providerNames;
    private string defaultProvider;
    /// <summary>The name of the layout template.</summary>
    public static readonly string DefinitionListTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ProviderSelectorDefList.ascx");
    /// <summary>The name of the layout template.</summary>
    public static readonly string UnorderedListTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ProviderSelectorUnorderdList.ascx");
    private const string changeProviderFunction = " \r\nfunction changeProvider(option){\r\n    window.location.href = option;\r\n}\r\n";
    /// <summary>
    /// Defines the key that will be used to pass data provider name in query string.
    /// </summary>
    public const string DataProviderKey = "DataProvider";

    /// <summary>Gets or sets the currently selected provider.</summary>
    public virtual string SelectedProvider
    {
      get
      {
        if (this.selectedProvider == null)
        {
          if (this.Page != null)
            this.SelectedProvider = this.Page.Request["DataProvider"];
          if (this.selectedProvider == null)
            this.SelectedProvider = this.DefaultProvider;
        }
        return this.selectedProvider;
      }
      set
      {
        this.selectedProvider = value;
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext == null)
          return;
        currentHttpContext.Items[(object) "DataProvider"] = (object) this.Container.Providers.SelectedValue;
      }
    }

    /// <summary>Gets or sets the default provider.</summary>
    public virtual string DefaultProvider
    {
      get => this.defaultProvider;
      set => this.defaultProvider = value;
    }

    /// <summary>
    /// Gets an <see cref="T:System.Array" /> of available provider names.
    /// </summary>
    public virtual string[] ProviderNames
    {
      get
      {
        if (this.providerNames == null)
          this.providerNames = this.GetProviderNames();
        return this.providerNames;
      }
    }

    /// <summary>
    /// Gets or sets the path to a custom layout template for the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets an instance of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ProviderSelectorPanel.ProviderSelectorContainer" /> object that will be used as
    /// container control for the layout template.
    /// </summary>
    protected ProviderSelectorPanel.ProviderSelectorContainer Container => (ProviderSelectorPanel.ProviderSelectorContainer) base.Container;

    /// <summary>Gets or sets the route which is passed in the url.</summary>
    public string Route
    {
      get => (string) this.ViewState[nameof (Route)] ?? string.Empty;
      set => this.ViewState[nameof (Route)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the parameter which is passed in the url.
    /// </summary>
    public string Param
    {
      get => (string) this.ViewState[nameof (Param)] ?? string.Empty;
      set => this.ViewState[nameof (Param)] = (object) value;
    }

    /// <summary>Gets or sets the ParentId which is passed in the url.</summary>
    public string ParentId
    {
      get => (string) this.ViewState[nameof (ParentId)] ?? string.Empty;
      set => this.ViewState[nameof (ParentId)] = (object) value;
    }

    /// <summary>Gets or sets the mode which is passed in the url.</summary>
    public string Mode
    {
      get => (string) this.ViewState[nameof (Mode)] ?? string.Empty;
      set => this.ViewState[nameof (Mode)] = (object) value;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs"></see> object that contains the event data.
    /// </param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (this.Page == null)
        return;
      this.Page.RegisterRequiresControlState((Control) this);
    }

    /// <summary>
    /// Restores control-state information from a previous page request that was
    /// saved by the <see cref="M:System.Web.UI.Control.SaveControlState" /> method.
    /// </summary>
    /// <param name="savedState">
    /// An <see cref="T:System.Object"></see> that represents the control state to be restored.
    /// </param>
    protected override void LoadControlState(object savedState)
    {
      if (savedState == null)
        return;
      object[] objArray = (object[]) savedState;
      this.selectedProvider = (string) objArray[0];
      this.defaultProvider = (string) objArray[1];
    }

    /// <summary>
    /// Saves any server control state changes that have occurred since the time the
    /// page was posted back to the server.
    /// </summary>
    /// <returns>Returns the server control's current state.
    /// If there is no state associated with the control, this method returns null.
    /// </returns>
    protected override object SaveControlState() => (object) new object[2]
    {
      (object) this.selectedProvider,
      (object) this.defaultProvider
    };

    /// <summary>Sets the available providers.</summary>
    /// <param name="name">
    /// The names of the availble providers for the current module.
    /// </param>
    public virtual void SetProviderNames(params string[] name)
    {
      if (name == null)
        name = new string[0];
      this.providerNames = name;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs"></see> object that contains the event data.
    /// </param>
    protected override void OnPreRender(EventArgs e) => base.OnPreRender(e);

    /// <summary>
    /// Initializes all controls instantiated in the layout container.
    /// This method is called at appropriate time for setting initial values and
    /// subscribing for events of layout controls.
    /// </summary>
    protected override void InitializeControls()
    {
      base.InitializeControls();
      if (this.Page != null)
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SelectPanel", " \r\nfunction changeProvider(option){\r\n    window.location.href = option;\r\n}\r\n", true);
      if (this.Container.ProvidersPlaceholder != null)
        this.Container.ProvidersPlaceholder.Visible = this.ProviderNames.Length > 1;
      if (this.ProviderNames.Length <= 1)
        return;
      this.Container.Providers.DataSource = (object) this.ProviderNames;
      if (!string.IsNullOrEmpty(this.SelectedProvider))
      {
        for (int index = 0; index < this.ProviderNames.Length; ++index)
        {
          if (this.SelectedProvider.Equals(this.ProviderNames[index], StringComparison.OrdinalIgnoreCase))
            this.Container.Providers.SelectedIndex = index;
        }
      }
      this.Container.Providers.DataBind();
      if (this.Container.ToolTip == null)
        return;
      this.Container.ToolTip.Text = "";
    }

    /// <summary>
    /// When overridden, gets a string array of available provider names.
    /// </summary>
    /// <returns>A string array of available provider names.</returns>
    protected virtual string[] GetProviderNames() => new string[0];

    /// <summary>
    /// Creates an instance of container control.
    /// The container must inherit from <see cref="T:Telerik.Sitefinity.Web.UI.GenericContainer" />.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Web.UI.GenericContainer" /> object.
    /// </returns>
    protected override CommandPanel.LayoutContainer CreateContainer() => (CommandPanel.LayoutContainer) new ProviderSelectorPanel.ProviderSelectorContainer();

    /// <summary>Gets the default layoute template path.</summary>
    /// <value>The default layoute template path.</value>
    protected override string DefaultLayouteTemplatePath => !this.HasDescription ? ProviderSelectorPanel.UnorderedListTemplateName : ProviderSelectorPanel.DefinitionListTemplateName;

    /// <summary>
    /// Creates a layout template from a specified
    /// user control (external template) or embedded resource.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:System.Web.UI.ITemplate" /> object.
    /// </returns>
    protected override ITemplate CreateLayoutTemplate() => ControlUtilities.GetTemplate(new TemplateInfo()
    {
      TemplatePath = this.LayoutTemplatePath,
      TemplateName = this.LayoutTemplateName,
      ControlType = this.GetType()
    });

    /// <summary>
    /// Handles the <see cref="E:System.Web.UI.WebControls.ListControl.SelectedIndexChanged" /> event.
    /// This allows you to provide a custom handler for the event.
    /// </summary>
    /// <param name="sender">
    /// The <see cref="T:System.Web.UI.WebControls.ListControl" />.
    /// </param>
    /// <param name="e">
    /// A <see cref="T:System.EventArgs"></see> that contains the event data.
    /// </param>
    protected virtual void Providers_SelectedIndexChanged(object sender, EventArgs e) => this.SelectedProvider = this.Container.Providers.SelectedValue;

    /// <summary>
    /// Represents the layout template conatainer for <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ProviderSelectorPanel" />.
    /// </summary>
    protected class ProviderSelectorContainer : CommandPanel.LayoutContainer
    {
      /// <summary>
      /// Optional control used as place holder for "providersList" ListControl.
      /// </summary>
      public Control ProvidersPlaceholder => this.GetControl<Control>("providersPanel", false);

      /// <summary>
      /// Required control that mus inherit from ListControl control.
      /// </summary>
      public ListControl Providers => this.GetControl<ListControl>("providersList", true);

      /// <summary>Optional control for tool tips.</summary>
      public ITextControl ToolTip
      {
        get
        {
          switch (this.GetControl<Control>("providersTooltip", false))
          {
            case ITextControl toolTip:
              return toolTip;
            case RadToolTip radToolTip:
              return (ITextControl) new ProviderSelectorPanel.ProviderSelectorContainer.RadToolTipWraper(radToolTip);
            default:
              return (ITextControl) null;
          }
        }
      }

      private class RadToolTipWraper : Control, ITextControl
      {
        private RadToolTip radToolTip;

        public RadToolTipWraper(RadToolTip radToolTip)
        {
          this.radToolTip = radToolTip;
          this.Controls.Add((Control) radToolTip);
        }

        public string Text
        {
          get => this.radToolTip.Text;
          set => this.radToolTip.Text = value;
        }
      }
    }
  }
}
