// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SearchPanelToolboxItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Provides the search panel for backend views (simple and advanced search)
  /// </summary>
  public class SearchPanelToolboxItem : ToolboxItemBase
  {
    private GenericContainer container;
    private ITemplate itemTemplate;
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Search.SearchPanel.ascx");
    private bool panelInitialized;

    /// <summary>Gets or sets the item template for the toolbox item</summary>
    /// <value></value>
    public override ITemplate ItemTemplate
    {
      get
      {
        if (this.itemTemplate == null)
          this.itemTemplate = ControlUtilities.GetTemplate(this.ItemTemplatePath, (string) null, Config.Get<ControlsConfig>().ResourcesAssemblyInfo, (string) null);
        return this.itemTemplate;
      }
      set => this.itemTemplate = value;
    }

    /// <inheritdoc />
    public override string ItemTemplatePath
    {
      get => string.IsNullOrWhiteSpace(base.ItemTemplatePath) ? SearchPanelToolboxItem.layoutTemplateName : base.ItemTemplatePath;
      set => base.ItemTemplatePath = value;
    }

    /// <summary>
    /// Exposes the PersistentTypeName property of the QueryBuilder for advanced search
    /// </summary>
    /// <value>The name of the persistent type.</value>
    public string PersistentTypeName
    {
      get => this.QueryBuilder.PersistentTypeName;
      set
      {
        this.InitializeControls();
        this.QueryBuilder.PersistentTypeName = value;
      }
    }

    /// <summary>
    /// Specify which fields of the model should be included in the simple search.
    /// </summary>
    /// <value>A comma delimited list of field names.</value>
    public string SimpleSearchFields
    {
      get => this.SimpleSearchBox.SearchFields;
      set
      {
        this.InitializeControls();
        this.SimpleSearchBox.SearchFields = value;
      }
    }

    /// <summary>
    /// Exposes the BinderClientId property of the BinderSearchBox control for simple search
    /// </summary>
    /// <value>The binder client ID.</value>
    public string BinderClientID
    {
      get => this.SimpleSearchBox.BinderClientId;
      set
      {
        this.InitializeControls();
        this.SimpleSearchBox.BinderClientId = value;
        this.BinderHiddenField.Value = value;
      }
    }

    /// <summary>Gets a container for the toolbox item</summary>
    /// <value>The container.</value>
    protected GenericContainer Container
    {
      get
      {
        if (this.container == null)
          this.container = new GenericContainer();
        return this.container;
      }
    }

    /// <summary>
    /// Generates the command item and returns instantiated control.
    /// </summary>
    /// <returns>Instance of the command button</returns>
    public Control GenerateItem()
    {
      this.InitializeControls();
      return (Control) this.Container;
    }

    public void InitializeControls()
    {
      if (this.panelInitialized)
        return;
      this.ItemTemplate.InstantiateIn((Control) this.Container);
      this.panelInitialized = true;
    }

    /// <summary>Gets the simple search box.</summary>
    /// <value>The simple search box.</value>
    protected virtual BinderSearchBox SimpleSearchBox => this.Container.GetControl<BinderSearchBox>("simpleSearch", true);

    /// <summary>Gets the query builder for advanced search.</summary>
    /// <value>The query builder.</value>
    protected virtual QueryBuilder QueryBuilder => this.Container.GetControl<QueryBuilder>("qbSearch", true);

    /// <summary>Gets the simple search box.</summary>
    /// <value>The simple search box.</value>
    protected virtual HiddenField BinderHiddenField => this.Container.GetControl<HiddenField>("binderHiddenField", true);
  }
}
