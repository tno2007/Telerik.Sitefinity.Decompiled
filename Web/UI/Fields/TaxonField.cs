// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.TaxonField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>A common class for the taxonomy field controls</summary>
  [RequiresDataItem]
  public abstract class TaxonField : FieldControl, IRequiresDataItem
  {
    private ITaxonomy taxonomy;
    private const string TaxonFieldScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.TaxonField.js";
    private const string ILocalizableFieldControlScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js";
    private IDataItem dataItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.TaxonField" /> class.
    /// </summary>
    public TaxonField() => this.HideWhenNoTaxaFound = false;

    /// <summary>
    /// Gets or sets a value indicating whether the field allows multiple selection.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the field allows multiple selection; otherwise, <c>false</c>.
    /// </value>
    public virtual bool AllowMultipleSelection { get; set; }

    internal virtual bool AllowCreating { get; set; }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    public virtual string TaxonomyProvider { get; set; }

    /// <summary>Gets or sets the taxonomy pageId.</summary>
    /// <value>The taxonomy pageId.</value>
    public virtual Guid TaxonomyId { get; set; }

    /// <summary>
    /// Gets or sets the web service URL which will be used to bind the selector.
    /// </summary>
    /// <value>The web service URL.</value>
    public virtual string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this control will bind on server.
    /// </summary>
    /// <value><c>true</c> if to bind on the server; otherwise, <c>false</c>.</value>
    public bool BindOnServer { get; set; }

    /// <summary>
    /// Gets the name of the metafield in the content item, which returns the taxa associated with the item
    /// </summary>
    public string TaxonomyMetafieldName { get; set; }

    /// <summary>
    /// Gets the formatted message for the client which states that currently no taxa exists in the taxonomy.
    /// </summary>
    protected internal string NoTaxaExistsMessage => Res.Get<TaxonomyResources>().NoTaxaExistsYet.Arrange((object) this.Taxonomy.Title);

    /// <summary>
    /// Gets or sets a value indicating whether to hide the field when the data source contains no items.
    /// </summary>
    /// <value>
    /// 	<c>true</c> to hide the field when the data source contains no items; otherwise, <c>false</c>.
    /// </value>
    public bool HideWhenNoTaxaFound { get; set; }

    /// <summary>
    /// Gets the instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Model.ITaxonomy" /> representing the taxonomy to which the taxon field is bound to.
    /// </summary>
    protected internal virtual ITaxonomy Taxonomy
    {
      get
      {
        if (this.taxonomy == null)
          this.taxonomy = TaxonomyManager.GetManager(this.TaxonomyProvider).GetTaxonomy(this.TaxonomyId);
        return this.taxonomy;
      }
    }

    /// <summary>
    /// Converts a control ID used in conditional templates accoding to this.DisplayMode
    /// </summary>
    /// <param name="originalName">Original ID of the control</param>
    /// <returns>Unique control ID</returns>
    protected virtual string GetConditionalControlName(string originalName)
    {
      string lower = this.DisplayMode.ToString().ToLower();
      return originalName + "_" + lower;
    }

    /// <summary>
    /// Shortcut for this.Container.GetControl(this.GetConditionalControlName(originalName), required)
    /// </summary>
    /// <typeparam name="T">Type of the control to load</typeparam>
    /// <param name="originalName">Original ID of the control</param>
    /// <param name="required">Throw exception if control is not found and this parameter is true</param>
    /// <returns>Loaded control</returns>
    protected T GetConditionalControl<T>(string originalName, bool required) => this.Container.GetControl<T>(this.GetConditionalControlName(originalName), required);

    /// <summary>
    /// Gets the reference to the the client binder that binds the selected taxa.
    /// </summary>
    protected internal abstract ClientBinder SelectedTaxaBinder { get; }

    protected virtual Repeater TaxaList => this.Container.GetControl<Repeater>("taxaList", true);

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is ITaxonFieldDefinition taxonFieldDefinition))
        return;
      this.AllowMultipleSelection = taxonFieldDefinition.AllowMultipleSelection;
      this.TaxonomyId = taxonFieldDefinition.TaxonomyId;
      this.TaxonomyProvider = taxonFieldDefinition.TaxonomyProvider;
      this.WebServiceUrl = taxonFieldDefinition.WebServiceUrl;
      this.BindOnServer = taxonFieldDefinition.BindOnServer;
      this.AllowCreating = taxonFieldDefinition.AllowCreating;
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.DisplayMode != FieldDisplayMode.Read)
        return;
      IDataItem dataItem = this.DataItem;
      if (dataItem == null && this.Parent is IDataItemContainer parent)
        dataItem = (IDataItem) parent.DataItem;
      if (dataItem == null)
        return;
      string name = this.TaxonomyMetafieldName;
      if (name.IsNullOrEmpty())
        name = this.DataFieldName;
      if (!(OrganizerBase.GetProperty(dataItem.GetType(), name) is TaxonomyPropertyDescriptor property))
        return;
      IList<Guid> guidList = property.GetValue((object) dataItem) as IList<Guid>;
      TaxonomyManager manager = TaxonomyManager.GetManager(this.TaxonomyProvider);
      List<string> stringList = new List<string>();
      foreach (Guid id in (IEnumerable<Guid>) guidList)
      {
        ITaxon taxon = manager.GetTaxon(id);
        if (taxon != null)
          stringList.Add((string) taxon.Title);
      }
      if (stringList != null && stringList.Count > 0)
      {
        this.TaxaList.DataSource = (object) stringList;
        this.TaxaList.ItemDataBound += new RepeaterItemEventHandler(this.TaxaList_ItemDataBound);
        this.TaxaList.DataBind();
      }
      if (stringList.Count != 0 || !this.HideWhenNoTaxaFound)
        return;
      this.Visible = false;
    }

    private void TaxaList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.FindControl("taxonName") is SitefinityLabel control))
        return;
      control.Text = (string) e.Item.DataItem;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor descriptorPrivate = this.GetLastScriptDescriptor_Private();
      descriptorPrivate.AddProperty("allowMultipleSelection", (object) this.AllowMultipleSelection);
      descriptorPrivate.AddProperty("allowCreating", (object) this.AllowCreating);
      descriptorPrivate.AddProperty("taxonomyId", (object) this.TaxonomyId);
      descriptorPrivate.AddProperty("taxonomyProvider", (object) this.TaxonomyProvider);
      descriptorPrivate.AddProperty("bindOnServer", (object) this.BindOnServer);
      descriptorPrivate.AddProperty("_enabled", (object) this.Enabled);
      string absolute = VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash(this.WebServiceUrl));
      descriptorPrivate.AddProperty("webServiceUrl", (object) absolute);
      if (this.SelectedTaxaBinder != null)
        descriptorPrivate.AddComponentProperty("selectedTaxaBinder", this.SelectedTaxaBinder.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        descriptorPrivate
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference()
      {
        Assembly = typeof (TaxonField).Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js"
      },
      new ScriptReference()
      {
        Assembly = typeof (TaxonField).Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Fields.Scripts.TaxonField.js"
      }
    }.ToArray();

    /// <summary>Gets the last script descriptor.</summary>
    /// <returns></returns>
    protected internal virtual ScriptControlDescriptor GetLastScriptDescriptor_Private() => (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();

    /// <summary>
    /// The DataItem field from the IRequiresDataItem interface.
    /// </summary>
    public IDataItem DataItem
    {
      get => this.dataItem;
      set => this.dataItem = value;
    }
  }
}
