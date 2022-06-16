// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// A control that is used to provide a user with the ability to select one or more content
  /// items.
  /// </summary>
  public class ContentSelector : SimpleView, IScriptControl
  {
    private string itemType;
    private string serviceUrl;
    private string providerName;
    private int pageSize;
    private string defaultSortExpression;
    private bool showButtonArea = true;
    private bool showHeader = true;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentSelector.ascx");

    /// <summary>
    /// Gets or Sets the the name of the items to retrieve. Should be a type string.
    /// </summary>
    public virtual string ItemType
    {
      get
      {
        if (string.IsNullOrEmpty(this.itemType))
          this.itemType = this.ItemSelector.ItemType;
        return this.itemType;
      }
      set => this.itemType = value;
    }

    /// <summary>Allow or disallow multiple selection in the grid</summary>
    public bool AllowMultipleSelection { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether paging will be enabled on the selector.
    /// </summary>
    public virtual bool AllowPaging { get; set; }

    /// <summary>
    /// Gets or sets the value determining the page size if paging is enabled.
    /// </summary>
    public virtual int PageSize
    {
      get
      {
        if (this.pageSize == 0)
          this.pageSize = this.ItemSelector.PageSize;
        return this.pageSize;
      }
      set
      {
        this.pageSize = value;
        this.ItemSelector.PageSize = value;
      }
    }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be sorted.
    /// </summary>
    public bool AllowSorting { get; set; }

    /// <summary>Gets or sets the default sort expression.</summary>
    public string DefaultSortExpression
    {
      get
      {
        if (string.IsNullOrEmpty(this.defaultSortExpression))
          this.defaultSortExpression = this.ItemSelector.DefaultSortExpression;
        return this.defaultSortExpression;
      }
      set
      {
        this.defaultSortExpression = value;
        this.ItemSelector.DefaultSortExpression = value;
      }
    }

    /// <summary>Text for the control's title</summary>
    public virtual string TitleText { get; set; }

    /// <summary>
    /// Event handler for the selection's end (when one of the 2 buttons is clicked)
    /// </summary>
    public string OnDoneClientSelection { get; set; }

    /// <summary>
    /// Gets or sets the predefinied filter for the content selector.
    /// </summary>
    public string ItemsFilter { get; set; }

    /// <summary>
    /// Gets or sets the option to automatically bind the selector when the control loads
    /// </summary>
    public bool BindOnLoad { get; set; }

    /// <summary>Text which is shown in the search box by default</summary>
    public virtual string SearchBoxInnerText { get; set; }

    /// <summary>the text displayed infront of the search box</summary>
    public virtual string SearchBoxTitleText { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ContentSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the work mode - whether to render as a List or as a grid with column titles,etc
    /// </summary>
    /// <value>The work mode.</value>
    public ContentSelector.SelectorWorkMode WorkMode { get; set; }

    /// <summary>
    /// if Work mode is set to List this template is used to render it
    /// </summary>
    /// <value>The list mode client template.</value>
    public string ListModeClientTemplate { get; set; }

    /// <summary>Gets or sets the service URL.</summary>
    /// <value>The service URL.</value>
    public virtual string ServiceUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.serviceUrl))
          this.serviceUrl = this.ItemSelector.ServiceUrl;
        return this.serviceUrl;
      }
      set
      {
        this.ItemSelector.ServiceUrl = value;
        this.serviceUrl = value;
      }
    }

    /// <summary>Gets or sets the provider name for the content</summary>
    public string ProviderName
    {
      get
      {
        if (string.IsNullOrEmpty(this.providerName))
          this.providerName = this.ItemSelector.ProviderName;
        return this.providerName;
      }
      set
      {
        this.providerName = value;
        this.ItemSelector.ProviderName = value;
      }
    }

    /// <summary>
    /// Gets or set a value indicating whether the header item of the grid will be shown.
    /// </summary>
    /// <value><c>true</c> if the header item of the grid will be shown; otherwise, <c>false</c>.</value>
    public bool ShowHeader
    {
      get => this.showHeader;
      set => this.showHeader = value;
    }

    /// <summary>Gets or sets the Done button text.</summary>
    /// <value>The done button text.</value>
    public string DoneButtonText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the done selecting and cancel buttons.
    /// </summary>
    public bool ShowButtonArea
    {
      get => this.showButtonArea;
      set => this.showButtonArea = value;
    }

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public string UICulture { get; set; }

    /// <summary>
    /// Gets or sets the display state of the providers selection box. The default is not to show.
    /// </summary>
    public virtual bool ShowProvidersList { get; set; }

    /// <summary>
    /// The data member used to render the flat selector if the work mode is set to List.
    /// </summary>
    /// <value>The flat selector data member.</value>
    public DataMemberInfo ItemSelectorDataMember { get; set; }

    /// <summary>A flat selector for the retrieved items</summary>
    protected virtual FlatSelector ItemSelector => this.Container.GetControl<FlatSelector>("itemSelector", true);

    /// <summary>The title label</summary>
    protected virtual Label TitleLabel => this.Container.GetControl<Label>("lblTitle", true);

    /// <summary>The LinkButton for "Done"</summary>
    protected virtual LinkButton DoneSelectingButton => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    /// <summary>The Literal displaying "Done" button text.</summary>
    protected virtual ITextControl DoneWithSelectingLiteral => this.Container.GetControl<ITextControl>(nameof (DoneWithSelectingLiteral), false);

    /// <summary>The LinkButton for "Cancel"</summary>
    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>The button area control</summary>
    protected virtual Control ButtonArea => this.Container.GetControl<Control>("buttonAreaPanel", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container</param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ItemSelector.AllowMultipleSelection = this.AllowMultipleSelection;
      this.ItemSelector.BindOnLoad = this.BindOnLoad;
      this.ItemSelector.ItemType = this.ItemType;
      this.ItemSelector.UICulture = this.UICulture;
      this.ItemSelector.ShowProvidersList = this.ShowProvidersList;
      this.ItemSelector.AllowPaging = this.AllowPaging;
      this.ItemSelector.AllowSorting = this.AllowSorting;
      this.ItemSelector.PageSize = this.PageSize;
      this.ItemSelector.DefaultSortExpression = this.DefaultSortExpression;
      this.ItemSelector.ProviderName = this.ProviderName;
      if (this.WorkMode == ContentSelector.SelectorWorkMode.List)
      {
        if (this.ItemSelectorDataMember == null)
          this.ItemSelectorDataMember = this.ItemSelector.DataMembers[0];
        this.ItemSelectorDataMember.ColumnTemplate = this.ListModeClientTemplate;
        this.ItemSelector.DataMembers.Clear();
        this.ItemSelector.DataMembers.Add(this.ItemSelectorDataMember);
        this.ItemSelector.ShowHeader = false;
        this.ItemSelector.ShowSelectedFilter = false;
        GridColumn columnSafe = this.ItemSelector.SelectorGrid.MasterTableView.GetColumnSafe("ClientSelectColumn");
        if (columnSafe != null)
          columnSafe.Visible = false;
      }
      if (this.SearchBoxInnerText != null)
        this.ItemSelector.InnerSearchBoxText = this.SearchBoxInnerText;
      if (this.SearchBoxTitleText != null)
        this.ItemSelector.SearchBoxTitleText = this.SearchBoxTitleText;
      if (this.DoneWithSelectingLiteral != null && this.DoneButtonText != null)
        this.DoneWithSelectingLiteral.Text = this.DoneButtonText;
      if (this.ButtonArea != null)
        this.ButtonArea.Visible = this.ShowButtonArea;
      if (string.IsNullOrEmpty(this.ItemsFilter))
        return;
      this.ItemSelector.ConstantFilter = this.ItemsFilter;
    }

    /// <summary>Event handler for the PreRender lifecycle event</summary>
    /// <param name="e">Not used.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.serviceUrl != this.ItemSelector.ServiceUrl)
        this.ItemSelector.ServiceUrl = this.serviceUrl;
      (ScriptManager.GetCurrent(this.Page) ?? throw new HttpException(Res.Get<ErrorMessages>().ScriptManagerIsNull)).RegisterScriptControl<ContentSelector>(this);
    }

    /// <summary>Event handler for the Render lifecycle event</summary>
    /// <param name="writer">Not used.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddProperty("itemType", (object) this.ItemType);
      behaviorDescriptor.AddProperty("allowMultipleSelection", (object) this.AllowMultipleSelection);
      behaviorDescriptor.AddProperty("titleText", (object) this.TitleText);
      behaviorDescriptor.AddProperty("itemsFilter", (object) this.ItemsFilter);
      behaviorDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad.ToString().ToLower());
      behaviorDescriptor.AddProperty("providerName", (object) this.ProviderName);
      if (this.ShowHeader)
        behaviorDescriptor.AddElementProperty("lblTitle", this.TitleLabel.ClientID);
      if (this.ShowButtonArea)
      {
        behaviorDescriptor.AddElementProperty("lnkDoneSelecting", this.DoneSelectingButton.ClientID);
        behaviorDescriptor.AddElementProperty("lnkCancel", this.CancelButton.ClientID);
      }
      behaviorDescriptor.AddComponentProperty("itemSelector", this.ItemSelector.ClientID);
      if (!string.IsNullOrEmpty(this.OnDoneClientSelection))
        behaviorDescriptor.AddEvent("doneClientSelection", this.OnDoneClientSelection);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public virtual IEnumerable<ScriptReference> GetScriptReferences()
    {
      string str = typeof (ContentSelector).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new ScriptReference[2]
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.ControlDesign.ContentSelector.js"
        },
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
        }
      };
    }

    /// <summary>
    /// </summary>
    public enum SelectorWorkMode
    {
      /// <summary>renders as a grid</summary>
      Grid,
      /// <summary>renders as a list</summary>
      List,
    }
  }
}
