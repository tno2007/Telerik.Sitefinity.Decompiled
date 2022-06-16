// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.FlatSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Control for selecting one or more items from the flat data source
  /// </summary>
  public class FlatSelector : ItemSelector
  {
    private bool showProvidersList;
    private bool inclueAllProvidersOption;
    private string serviceUrl;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.FlatItemSelector.ascx");
    private const string defaultServiceUrl = "~/Sitefinity/Services/Content/ContentService.svc";
    private const string controlScript = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.FlatSelector.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FlatSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the value determining whether paging will be enabled on the selector
    /// </summary>
    public virtual bool AllowPaging { get; set; }

    /// <summary>
    /// Gets or sets the value determining the page size if paging is enabled
    /// </summary>
    public virtual int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the display state of the providers selection box. The default is not to show.
    /// </summary>
    public virtual bool ShowProvidersList
    {
      get => this.showProvidersList;
      set => this.showProvidersList = value;
    }

    /// <summary>
    /// Gets or sets the option to include "All Providers" in the providers selection box. The default is to include.
    /// </summary>
    public virtual bool InclueAllProvidersOption
    {
      get => this.inclueAllProvidersOption;
      set => this.inclueAllProvidersOption = value;
    }

    /// <summary>
    /// Gets or sets the client function to be called once the provider selection is changed
    /// </summary>
    public string OnProviderSelectionChanged { get; set; }

    /// <summary>the text displayed in front of the search box</summary>
    public string SearchBoxTitleText { get; set; }

    /// <summary>Text which is shown in the search box by default</summary>
    public string InnerSearchBoxText { get; set; }

    /// <summary>Gets or sets the service URL.</summary>
    /// <value>The service URL.</value>
    public virtual string ServiceUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.serviceUrl))
          this.serviceUrl = "~/Sitefinity/Services/Content/ContentService.svc";
        return this.serviceUrl;
      }
      set
      {
        this.serviceUrl = value;
        this.SelectorBinder.ServiceUrl = value;
      }
    }

    /// <summary>
    /// Gets or set a value indicating whether the header item of the grid will be shown.
    /// </summary>
    /// <value><c>true</c> if the header item of the grid will be shown; otherwise, <c>false</c>.</value>
    public bool ShowHeader { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets a value indicating whether to disable listing of providers. By default <c>false</c>.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if listing of providers is disabled; otherwise, <c>false</c>.
    /// </value>
    public virtual bool DisableProvidersListing { get; set; }

    /// <summary>Gets or sets the load more button text.</summary>
    /// <value>The load more button text.</value>
    public string LoadMoreButtonText { get; set; }

    public virtual bool? UseSkins { get; set; }

    /// <summary>
    /// Gets the reference to the selector grid, used in the flat mode
    /// </summary>
    protected internal virtual RadGrid SelectorGrid => this.Container.GetControl<RadGrid>("selectorGrid", true);

    /// <summary>
    /// Gets the reference to the selector binder, used in the flat mode
    /// </summary>
    protected internal virtual RadGridBinder SelectorBinder => this.Container.GetControl<RadGridBinder>("selectorBinder", true);

    /// <summary>Gets the reference to the providers list</summary>
    protected virtual DropDownList ProvidersList => this.Container.GetControl<DropDownList>("providersList", true);

    /// <summary>Gets the reference to the search box</summary>
    protected new virtual BinderSearchBox SelectorSearchBox => this.Container.GetControl<BinderSearchBox>("selectorSearchBox", true);

    /// <summary>The LinkButton for "Load more..."</summary>
    protected virtual LinkButton LoadMoreLinkButton => this.Container.GetControl<LinkButton>("loadMoreLinkButton", false);

    /// <summary>The Literal displaying "Done" button text.</summary>
    protected virtual ITextControl LoadMoreLiteral => this.Container.GetControl<ITextControl>(nameof (LoadMoreLiteral), false);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e) => base.OnPreRender(e);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">
    /// An instance of <see cref="T:Telerik.Sitefinity.Web.UI.GenericContainer" /> class in which the template was instantiated.
    /// </param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      bool flag1 = !this.UseSkins.HasValue || this.UseSkins.Value;
      if (!flag1)
      {
        string empty = string.Empty;
        this.SelectedItemsFilter.Skin = empty;
        this.SelectedItemsFilter.EnableEmbeddedSkins = !flag1;
        this.SelectorGrid.Skin = empty;
        this.SelectorGrid.EnableEmbeddedSkins = !flag1;
      }
      this.SelectorBinder.ServiceUrl = this.ServiceUrl;
      this.SelectorGrid.ShowHeader = this.ShowHeader;
      this.SelectorGrid.AllowMultiRowSelection = this.AllowMultipleSelection;
      if (!this.SelectorGrid.AllowMultiRowSelection)
      {
        GridColumn byUniqueNameSafe = this.SelectorGrid.Columns.FindByUniqueNameSafe("ClientSelectColumn");
        if (byUniqueNameSafe != null)
          byUniqueNameSafe.Visible = false;
      }
      this.SelectorGrid.AllowSorting = this.AllowSorting;
      this.SelectorGrid.AllowPaging = this.AllowPaging;
      this.SelectorGrid.PageSize = !this.AllowPaging || this.PageSize <= 0 ? 50 : this.PageSize;
      if (this.InclueAllProvidersOption)
        this.ProvidersList.Items.Add(new ListItem(Res.Get<Labels>().AllProvidersText, string.Empty));
      bool flag2 = false;
      if (!string.IsNullOrEmpty(this.ItemType))
      {
        if (string.IsNullOrEmpty(this.ItemSurrogateType))
          this.ItemSurrogateType = this.ItemType;
        if (!this.DisableProvidersListing)
        {
          IManager manager;
          if (ManagerBase.TryGetMappedManager(TypeResolutionService.ResolveType(this.ItemSurrogateType), (string) null, out manager))
          {
            IEnumerable<DataProviderBase> contextProviders = manager.GetContextProviders();
            foreach (DataProviderBase dataProviderBase in contextProviders)
            {
              if (!(dataProviderBase.ProviderGroup == "System"))
                this.ProvidersList.Items.Add(new ListItem(dataProviderBase.Title, dataProviderBase.Name));
            }
            flag2 = contextProviders.Count<DataProviderBase>() >= 2;
          }
          else
            flag2 = false;
          if (!this.ShowProvidersList)
            flag2 = false;
        }
      }
      if (!flag2)
        this.ProvidersList.Attributes.Add("style", "display:none");
      if (!string.IsNullOrEmpty(this.ProviderName))
        this.ProvidersList.SelectedValue = this.ProviderName;
      else
        this.ProvidersList.SelectedIndex = 0;
      if (this.InnerSearchBoxText != null)
        this.SelectorSearchBox.InnerSearchBoxText = this.InnerSearchBoxText;
      if (this.SearchBoxTitleText != null)
        this.SelectorSearchBox.BinderBoxLabelText = this.SearchBoxTitleText;
      this.SelectorSearchBox.Binder = (Control) this.SelectorBinder;
      if (this.LoadMoreLiteral != null && this.LoadMoreButtonText != null)
        this.LoadMoreLiteral.Text = this.LoadMoreButtonText;
      this.CreateSelector();
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("_allowPaging", (object) this.AllowPaging);
      controlDescriptor.AddProperty("_pageSize", (object) this.PageSize);
      controlDescriptor.AddProperty("_binderId", (object) this.SelectorBinder.ClientID);
      controlDescriptor.AddProperty("_gridId", (object) this.SelectorGrid.ClientID);
      controlDescriptor.AddProperty("_providersListId", (object) this.ProvidersList.ClientID);
      controlDescriptor.AddProperty("_selectorSearchBoxId", (object) this.SelectorSearchBox.ClientID);
      controlDescriptor.AddProperty("_allowMultipleSelection", (object) this.AllowMultipleSelection);
      if (!string.IsNullOrEmpty(this.OnProviderSelectionChanged))
        controlDescriptor.AddEvent("providerSelectionChanged", this.OnProviderSelectionChanged);
      if (!this.AllowPaging && this.LoadMoreLinkButton != null)
        controlDescriptor.AddElementProperty("lnkLoadMore", this.LoadMoreLinkButton.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script
    /// resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of
    /// <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferenceList = new List<ScriptReference>(base.GetScriptReferences());
      string str = this.GetType().Assembly.GetName().ToString();
      scriptReferenceList.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.FlatSelector.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferenceList.ToArray();
    }

    /// <summary>Creates the selector with specified data members</summary>
    private void CreateSelector()
    {
      for (int index = 0; index < this.DataMembers.Count; ++index)
      {
        GridTemplateColumn column = new GridTemplateColumn();
        column.UniqueName = "BinderContainer" + (object) index;
        column.HeaderText = this.DataMembers[index].HeaderText;
        this.SelectorGrid.MasterTableView.Columns.Add((GridColumn) column);
        this.SelectorBinder.Containers.Add(new BinderContainer()
        {
          Markup = string.IsNullOrEmpty(this.DataMembers[index].ColumnTemplate) ? "{{" + this.DataMembers[index].Name + "}}" : this.DataMembers[index].ColumnTemplate
        });
      }
    }
  }
}
