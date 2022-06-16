// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ProvidersSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Control for selecting multiple pages either internal or external.
  /// </summary>
  public class ProvidersSelector : SimpleScriptView
  {
    private int listedProvidersCount = 5;
    private string moduleName;
    private string dataKeyName = "Name";
    private string dataTextField = "Title";
    private string selectedProviderTitle;
    private IList<DataProviderBase> providers;
    private FlatSelector providersSelector;
    private bool allowMultipleSelection;
    private string dataSourceName;
    private int selectedProviderNameMaxLength = 18;
    private string webServiceUrlFormat = "~/" + "Sitefinity/Services/DataSourceService" + "/providers/?siteId={0}&dataSourceName={1}&sortExpression={2}";
    internal const string selectorScript = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.ProvidersSelector.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.ProvidersSelector.ascx");

    public ProvidersSelector() => this.LayoutTemplatePath = ProvidersSelector.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the web service URL format.</summary>
    /// <value>The web service URL format.</value>
    public string WebServiceUrlFormat
    {
      get => this.webServiceUrlFormat;
      set => this.webServiceUrlFormat = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to allow multiple selection.
    /// </summary>
    public virtual bool AllowMultipleSelection
    {
      get => this.allowMultipleSelection;
      set => this.allowMultipleSelection = value;
    }

    /// <summary>
    /// Get a filter expression that is always applied to the generic page selector
    /// </summary>
    public string ConstantFilter { get; set; }

    /// <summary>
    /// Gets or sets the data key name used by the flat selector.
    /// Default value is "ProviderName".
    /// </summary>
    /// <value>The data key name.</value>
    public string DataKeyName
    {
      get => this.dataKeyName;
      set => this.dataKeyName = value;
    }

    /// <summary>
    /// Gets or sets the name of the field that will be displayed.
    /// Default value is "ProviderTitle".
    /// </summary>
    /// <value>The name of the data text field.</value>
    public string DataTextField
    {
      get => this.dataTextField;
      set => this.dataTextField = value;
    }

    /// <summary>
    /// Gets or sets the manager associated with the providers listed by this selector.
    /// </summary>
    /// <value>Instance of the manager.</value>
    public IManager Manager { get; set; }

    /// <summary>
    /// Gets all providers for the current site and specified manager.
    /// </summary>
    public IList<DataProviderBase> Providers
    {
      get
      {
        if (this.providers == null)
        {
          IEnumerable<DataProviderBase> source = !(this.Manager is DynamicModuleManager) ? this.Manager.GetContextProviders() : ((DynamicModuleManager) this.Manager).GetContextProviders(this.DynamicModuleName);
          if (source != null)
            this.providers = (IList<DataProviderBase>) source.Where<DataProviderBase>((Func<DataProviderBase, bool>) (p => !p.IsSystemProvider())).ToList<DataProviderBase>();
        }
        return this.providers;
      }
      set => this.providers = value;
    }

    /// <summary>Gets or sets the name of the data source.</summary>
    /// <value>The name of the data source.</value>
    public string DataSourceName
    {
      get
      {
        if (this.dataSourceName.IsNullOrEmpty())
          this.dataSourceName = !(this.Manager is DynamicModuleManager) ? this.Manager.GetType().FullName : (this.DynamicModuleName == null ? string.Empty : this.DynamicModuleName);
        return this.dataSourceName;
      }
      set => this.dataSourceName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ProvidersSelector will be rendered as a click menu, otherwise it will be rendered as selector.
    /// The default value is false - the control is rendered as selector.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the ProvidersSelector is rendered as a click menu; otherwise, <c>false</c>.
    /// </value>
    public bool RenderAsClickMenu { get; set; }

    /// <summary>
    /// Gets or sets the number of providers displayed in the click menu.
    /// 5 by default
    /// </summary>
    /// <value>The number of displayed providers.</value>
    public int ListedProvidersCount
    {
      get => this.listedProvidersCount;
      set => this.listedProvidersCount = value;
    }

    /// <summary>
    /// Gets or sets the max length of the selected provider name.
    /// </summary>
    /// <value>The max length of the selected provider name.</value>
    public int SelectedProviderNameMaxLength
    {
      get => this.selectedProviderNameMaxLength;
      set => this.selectedProviderNameMaxLength = value;
    }

    /// <summary>The name of the module</summary>
    public string ModuleName
    {
      get
      {
        if (this.moduleName.IsNullOrEmpty() && this.Manager != null)
        {
          if (this.Manager is DynamicModuleManager)
          {
            this.moduleName = this.DynamicModuleName;
          }
          else
          {
            PropertyInfo property = this.Manager.GetType().GetProperty(nameof (ModuleName));
            if (property != (PropertyInfo) null)
              this.moduleName = property.GetValue((object) this.Manager, (object[]) null) as string;
          }
        }
        return this.moduleName;
      }
      set => this.moduleName = value;
    }

    /// <summary>The name of the module of dynamic content view</summary>
    public string DynamicModuleName { get; set; }

    /// <summary>Gets or sets the name of the selected provider.</summary>
    /// <value>Selected provider name.</value>
    public string SelectedProviderName { get; set; }

    /// <summary>Gets or sets the title of the selected provider.</summary>
    /// <value>Selected provider title.</value>
    public string SelectedProviderTitle
    {
      get
      {
        if (this.selectedProviderTitle.IsNullOrEmpty())
        {
          if (!this.SelectedProviderName.IsNullOrEmpty())
          {
            DataProviderBase dataProviderBase = this.Providers.FirstOrDefault<DataProviderBase>((Func<DataProviderBase, bool>) (p => p.Name == this.SelectedProviderName));
            this.selectedProviderTitle = dataProviderBase == null ? this.SelectedProviderName : dataProviderBase.Title;
          }
          else
            this.selectedProviderTitle = Res.Get<Labels>().DefaultSource;
        }
        return this.selectedProviderTitle;
      }
      set => this.selectedProviderTitle = value;
    }

    /// <summary>Gets the change selected provider button.</summary>
    /// <value>The change selected provider button.</value>
    private LinkButton SelectProviderButton => this.Container.GetControl<LinkButton>("selectProviderButton", true);

    /// <summary>Gets the "Done selecting" button.</summary>
    /// <value>The "Done selecting" button.</value>
    private LinkButton DoneSelectingButton => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    /// <summary>Gets the cancel button.</summary>
    /// <value>The cancel button.</value>
    private LinkButton CancelButton => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>Gets the providers selector placeholder.</summary>
    protected PlaceHolder ProvidersSelectorPlaceholder => this.Container.GetControl<PlaceHolder>("providersSelectorPlaceholder", true);

    /// <summary>
    /// Gets the html control that displays providers selector dialog.
    /// </summary>
    protected virtual HtmlGenericControl ProvidersSelectorDialog => this.Container.GetControl<HtmlGenericControl>("providersSelectorDialog", true);

    /// <summary>
    /// Gets the label control that displays the selected source title.
    /// </summary>
    protected virtual Label SelectedProviderTitleLabel => this.Container.GetControl<Label>("selectedProviderTitle", true);

    /// <summary>
    /// Gets the panel in which the controls required for the selector are rendered.
    /// </summary>
    protected virtual Panel SelectorPanel => this.Container.GetControl<Panel>("selectorPanel", true);

    /// <summary>
    /// Gets the panel in which the controls required for the click menu are rendered.
    /// </summary>
    protected virtual Panel ClickMenuPanel => this.Container.GetControl<Panel>("clickMenuPanel", true);

    /// <summary>Gets the HyperLink for providers menu.</summary>
    protected virtual HyperLink ProvidersMenuLink => this.Container.GetControl<HyperLink>("providersMenuLink", true);

    /// <summary>Gets the repeater that lists the more providers.</summary>
    protected virtual Repeater MoreProvidersRepeater => this.Container.GetControl<Repeater>("moreProvidersRepeater", true);

    /// <summary>Gets the HyperLink for all providers.</summary>
    protected virtual HyperLink AllProvidersLink => this.Container.GetControl<HyperLink>("allProvidersLink", true);

    /// <summary>
    /// Gets the html control that displays all providers selector.
    /// </summary>
    protected virtual HtmlGenericControl AllProvidersMenu => this.Container.GetControl<HtmlGenericControl>("allProvidersMenu", true);

    /// <summary>Gets the html control that displays providers menu.</summary>
    protected virtual HtmlGenericControl ProvidersMenu => this.Container.GetControl<HtmlGenericControl>("providersMenu", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Manager == null || this.Providers == null || this.Providers.Count<DataProviderBase>() < 2)
      {
        this.Visible = false;
      }
      else
      {
        this.providersSelector = this.InitializeFlatSelector(string.Format(this.WebServiceUrlFormat, (object) SystemManager.CurrentContext.CurrentSite.Id, (object) this.DataSourceName, (object) this.DataTextField));
        this.ProvidersSelectorPlaceholder.Controls.Add((Control) this.providersSelector);
        if (this.RenderAsClickMenu)
        {
          this.ClickMenuPanel.Visible = true;
          this.ProvidersMenuLink.Text = HttpUtility.HtmlEncode(this.GetUIProviderTitle(this.SelectedProviderTitle));
          if (this.Providers.Count<DataProviderBase>() > this.ListedProvidersCount)
          {
            this.AllProvidersLink.Text = HttpUtility.HtmlEncode(string.Format(Res.Get<Labels>().AllSources, (object) this.Providers.Count<DataProviderBase>()));
            this.MoreProvidersRepeater.DataSource = (object) this.Providers.OrderBy<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Title)).Take<DataProviderBase>(this.ListedProvidersCount);
          }
          else
          {
            this.AllProvidersMenu.Visible = false;
            this.MoreProvidersRepeater.DataSource = (object) this.Providers.OrderBy<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Title));
          }
          this.MoreProvidersRepeater.ItemDataBound += new RepeaterItemEventHandler(this.MoreProvidersRepeater_ItemDataBound);
          this.MoreProvidersRepeater.DataBind();
        }
        else
        {
          this.SelectorPanel.Visible = true;
          this.SelectedProviderTitleLabel.Text = HttpUtility.HtmlEncode(this.SelectedProviderTitle);
        }
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (ProvidersSelector).FullName, this.ClientID);
      controlDescriptor.AddProperty("dataKeyName", (object) this.DataKeyName);
      controlDescriptor.AddProperty("dataTextField", (object) this.DataTextField);
      controlDescriptor.AddProperty("selectedProvider", (object) this.SelectedProviderName);
      controlDescriptor.AddProperty("selectedProviderNameMaxLength", (object) this.SelectedProviderNameMaxLength);
      controlDescriptor.AddProperty("moduleName", (object) this.ModuleName);
      controlDescriptor.AddComponentProperty("providersSelector", this.providersSelector.ClientID);
      if (!this.RenderAsClickMenu || this.AllProvidersMenu.Visible)
      {
        controlDescriptor.AddElementProperty("selectedProviderTitle", this.SelectedProviderTitleLabel.ClientID);
        controlDescriptor.AddElementProperty("providersSelectorDialog", this.ProvidersSelectorDialog.ClientID);
        controlDescriptor.AddElementProperty("doneButton", this.DoneSelectingButton.ClientID);
        controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      }
      if (this.RenderAsClickMenu)
      {
        controlDescriptor.AddElementProperty("selectProviderButton", this.AllProvidersLink.ClientID);
        controlDescriptor.AddElementProperty("providersMenu", this.ProvidersMenu.ClientID);
        controlDescriptor.AddElementProperty("selectedProviderTitle", this.ProvidersMenuLink.ClientID);
      }
      else
        controlDescriptor.AddElementProperty("selectProviderButton", this.SelectProviderButton.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Selectors.Scripts.ProvidersSelector.js", typeof (ProvidersSelector).Assembly.FullName)
    };

    private FlatSelector InitializeFlatSelector(string serviceUrl)
    {
      FlatSelector flatSelector = new FlatSelector();
      flatSelector.ServiceUrl = serviceUrl;
      flatSelector.ItemType = typeof (SiteDataSourceLinkViewModel).FullName;
      flatSelector.DataKeyNames = this.DataKeyName;
      flatSelector.AllowSearching = true;
      flatSelector.ShowSelectedFilter = false;
      flatSelector.InclueAllProvidersOption = false;
      flatSelector.AllowMultipleSelection = this.AllowMultipleSelection;
      flatSelector.ConstantFilter = this.ConstantFilter;
      flatSelector.ProviderName = this.SelectedProviderName;
      flatSelector.AllowPaging = true;
      flatSelector.DataMembers.Add(new DataMemberInfo()
      {
        Name = this.DataTextField,
        HeaderText = this.DataTextField,
        IsExtendedSearchField = true
      });
      return flatSelector;
    }

    private void MoreProvidersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
        return;
      HyperLink control = e.Item.FindControl("providerLink") as HyperLink;
      if (DataBinder.Eval(e.Item.DataItem, this.DataKeyName) is string str)
        control.Attributes.Add("name", str);
      object obj = DataBinder.Eval(e.Item.DataItem, this.DataTextField);
      if (obj != null)
        control.Text = obj.ToString();
      else if (str != null)
        control.Text = str;
      if (!(str == this.SelectedProviderName))
        return;
      control.CssClass += " sfSel";
    }

    private string GetUIProviderTitle(string providerTitle)
    {
      string valueToTruncate = providerTitle;
      if (valueToTruncate.IsNullOrWhitespace())
        return valueToTruncate;
      if (this.ModuleName != null && providerTitle.EndsWith(this.ModuleName))
      {
        int startIndex = providerTitle.Length - this.ModuleName.Length;
        if (char.IsWhiteSpace(providerTitle.ElementAt<char>(startIndex - 1)))
          valueToTruncate = providerTitle.Remove(startIndex).Trim();
      }
      return valueToTruncate.TruncateString(this.SelectedProviderNameMaxLength, SitefinityExtensions.TruncateOptions.IncludeEllipsis);
    }
  }
}
