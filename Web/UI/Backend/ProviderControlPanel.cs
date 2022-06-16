// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.ProviderControlPanel`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>
  /// This class is intended to serve as base for Control Panels
  /// that supporting multiple data provides.
  /// </summary>
  /// <typeparam name="THost">
  /// Defines the host of the current instance.
  /// Usually the host is the immediate parent of the current control.
  /// If the host is at no importance, <see cref="T:System.Web.UI.Control" /> type maybe specified.
  /// </typeparam>
  public class ProviderControlPanel<THost> : ControlPanel<THost> where THost : Control
  {
    private string providerName;

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ProviderControlPanel`1" />.
    /// </summary>
    public ProviderControlPanel()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ProviderControlPanel`1" />.
    /// </summary>
    /// <param name="autoGenerateViewCommands">
    /// Indicates if view command should be automatically generated.
    /// </param>
    public ProviderControlPanel(bool autoGenerateViewCommands)
      : base(autoGenerateViewCommands)
    {
    }

    /// <summary>
    /// Gets or sets the name of the data provider that will be used to retrieve and manipulate data.
    /// </summary>
    public virtual string ProviderName
    {
      get
      {
        if (this.providerName == null && SystemManager.CurrentHttpContext.Request.QueryString["DataProvider"] != null)
          this.providerName = SystemManager.CurrentHttpContext.Request.QueryString["DataProvider"];
        if (string.IsNullOrEmpty(this.providerName))
          this.providerName = this.GetDefaultProviderName();
        return this.providerName;
      }
      set
      {
        if (!(value != this.providerName))
          return;
        this.providerName = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the title of the panel containing the data provider selector.
    /// Usually this is the first command panel on the top right side of the control panel.
    /// </summary>
    public virtual string ProviderSelectorPanelTitle
    {
      get => (string) this.ViewState[nameof (ProviderSelectorPanelTitle)] ?? string.Empty;
      set => this.ViewState[nameof (ProviderSelectorPanelTitle)] = (object) value;
    }

    /// <summary>
    /// When overridden, provides a key for loading additional external template instead of the default one.
    /// </summary>
    protected override string AdditionalTemplateKey => this.ProviderName;

    /// <summary>
    /// When overridden this method returns a list of custom Command Panels.
    /// </summary>
    /// <param name="viewMode">Specifies the current View Mode</param>
    /// <param name="list">A list of custom command panels.</param>
    /// <returns>A list of command panels containing the defined commands for the current View Mode.</returns>
    protected override void CreateCustomCommandPanels(string viewMode, IList<ICommandPanel> list)
    {
      ICommandPanel providerSelectorPanel = this.CreateProviderSelectorPanel();
      if (providerSelectorPanel == null)
        return;
      list.Insert(0, providerSelectorPanel);
    }

    /// <summary>
    /// Creates a command panel that displays available data provides and indicates the current one.
    /// </summary>
    /// <returns><see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> object.</returns>
    protected virtual ICommandPanel CreateProviderSelectorPanel()
    {
      ProviderSelectorPanel providerSelectorPanel = new ProviderSelectorPanel();
      providerSelectorPanel.Name = "ProviderSelector";
      providerSelectorPanel.Title = this.ProviderSelectorPanelTitle;
      providerSelectorPanel.DefaultProvider = this.GetDefaultProviderName();
      string str1 = this.Page.Request.QueryString["route"];
      string str2 = this.Page.Request.QueryString[this.ParameterKey];
      string str3 = this.Page.Request.QueryString[this.ParentIdKey];
      string str4 = this.Page.Request.QueryString["mode"];
      if (!string.IsNullOrEmpty(str1))
        providerSelectorPanel.Route = "route=" + HttpUtility.UrlEncode(str1);
      if (!string.IsNullOrEmpty(str2))
        providerSelectorPanel.Param = this.ParameterKey + "=" + HttpUtility.UrlEncode(str2);
      if (!string.IsNullOrEmpty(str3))
        providerSelectorPanel.ParentId = this.ParentIdKey + "=" + str3;
      if (!string.IsNullOrEmpty(str4) && str4 == "insert")
        providerSelectorPanel.Mode = "mode=" + str4;
      providerSelectorPanel.SetProviderNames(this.GetProviderNames());
      return (ICommandPanel) providerSelectorPanel;
    }

    /// <summary>
    /// When overridden this method returns the name of the default data provider for the module.
    /// Default data provider is used when no current provider is selected.
    /// The default implementation returns empty string.
    /// </summary>
    /// <returns>A string representing the name of the default data provider for the module.</returns>
    protected virtual string GetDefaultProviderName() => string.Empty;

    /// <summary>
    /// When overridden this method returns a string array containing the names of available data providers.
    /// </summary>
    /// <returns>A string array containing the names of available data providers.</returns>
    protected virtual string[] GetProviderNames() => new string[0];
  }
}
