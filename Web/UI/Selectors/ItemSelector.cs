// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ItemSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Web.UI.Components;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Control for selecting arbitrary items</summary>
  [ParseChildren(true, "DataMembers")]
  [RequireScriptManager]
  public abstract class ItemSelector : SimpleScriptView
  {
    private Collection<DataMemberInfo> dataMembers;
    private string selectedItemsText;
    private string allItemsText;
    private bool bindOnLoad = true;

    /// <summary>
    /// Gets or sets the value determining whether more then one item can be selected
    /// </summary>
    public bool AllowMultipleSelection { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be sorted
    /// </summary>
    public bool AllowSorting { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be searched
    /// </summary>
    public virtual bool AllowSearching { get; set; }

    /// <summary>
    /// Gets or sets the comma delimited list of data members to be be displayed in the selector
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<DataMemberInfo> DataMembers
    {
      get
      {
        if (this.dataMembers == null)
          this.dataMembers = new Collection<DataMemberInfo>();
        return this.dataMembers;
      }
    }

    /// <summary>
    /// Gets or sets the name of the type that is to be selected
    /// </summary>
    public string ItemType { get; set; }

    /// <summary>
    /// Gets or sets the name of the type that is used in place of original
    /// type - generally for the serialization purposes
    /// </summary>
    public string ItemSurrogateType { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider that selector ought to use
    /// </summary>
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether all items / selected items filter should be
    /// displayed.
    /// </summary>
    public virtual bool ShowSelectedFilter { get; set; }

    /// <summary>
    /// Gets or sets the type of search that should be performed
    /// </summary>
    public SearchTypes SearchType { get; set; }

    /// <summary>Gets or sets the text of "all items" filter button</summary>
    public string AllItemsText
    {
      get
      {
        if (string.IsNullOrEmpty(this.allItemsText))
          this.allItemsText = Res.Get<Labels>().AllItems;
        return this.allItemsText;
      }
      set => this.allItemsText = value;
    }

    /// <summary>
    /// Gets or sets the text of "selected items" filter button
    /// </summary>
    public string SelectedItemsText
    {
      get
      {
        if (string.IsNullOrEmpty(this.selectedItemsText))
          this.selectedItemsText = Res.Get<Labels>().SelectedItems;
        return this.selectedItemsText;
      }
      set => this.selectedItemsText = value;
    }

    /// <summary>
    /// Get/set the client callback which is executed whenever the selector needs to choose which of its items in the "all items" mode is selected
    /// </summary>
    public string OnClientNeedsSelectedInPageServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the data key names used by the selector data source
    /// </summary>
    public string DataKeyNames { get; set; }

    /// <summary>
    /// Gets or sets the client function to be called once the selector has bound data and is ready
    /// </summary>
    public string OnClientSelectorReady { get; set; }

    /// <summary>
    /// Gets or sets the client function to be called once an item has been selected
    /// </summary>
    public string OnItemSelected { get; set; }

    /// <summary>
    /// Subscribe to the client-side event that is raised whenever the selector needs a service url for getting the selected items
    /// </summary>
    public string OnClientNeedsSelectedItemsServiceUrl { get; set; }

    /// <summary>
    /// Subscribe to the client-side event raised whenever an error occurs
    /// </summary>
    public string OnClientError { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => throw new NotImplementedException("This property must be implemented on the derived class.");

    /// <summary>
    /// Gets or sets the option to automatically bind the selector when the control loads
    /// </summary>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>
    /// Gets or sets the client function to be called once the binder is bound to new data.
    /// </summary>
    public string OnBinderDataBound { get; set; }

    /// <summary>
    /// Get/set a filter expression that is always applied to the item selector
    /// </summary>
    public string ConstantFilter { get; set; }

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public string UICulture { get; set; }

    /// <summary>Gets or sets the default sort expression.</summary>
    public string DefaultSortExpression { get; set; }

    /// <summary>
    /// Gets the reference to the client binder used to bind the selector control
    /// </summary>
    protected virtual ClientBinder SelectorBinder => this.Container.GetControl<ClientBinder>("selectorBinder", true);

    /// <summary>Gets the reference to the search box of the selector</summary>
    protected virtual BinderSearchBox SelectorSearchBox => this.Container.GetControl<BinderSearchBox>("selectorSearchBox", false);

    /// <summary>
    /// Gets the reference to the rad tab strip with all items / selected items filter
    /// </summary>
    protected internal virtual RadTabStrip SelectedItemsFilter => this.Container.GetControl<RadTabStrip>("selectedItemsFilter", true);

    /// <summary>
    /// Gets the reference to the label control that displays the number of selected items.
    /// </summary>
    protected virtual Label NumberOfSelectedItems => (Label) this.SelectedItemsFilter.Tabs[1].FindControl("numberOfSelectedItems");

    /// <summary>
    /// Gets the reference to the text control that displays the text which instructs
    /// that all items should be displayed.
    /// </summary>
    protected virtual ITextControl AllItemsLiteral => (ITextControl) this.SelectedItemsFilter.Tabs[0].FindControl("allItemsLiteral");

    /// <summary>
    /// Gets the reference to the text control that displays the text which instructs
    /// that only selected items should be displayed.
    /// </summary>
    protected virtual ITextControl SelectedItemsLiteral => (ITextControl) this.SelectedItemsFilter.Tabs[1].FindControl("selectedItemsLiteral");

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SelectorSearchBox.Visible = this.AllowSearching;
      this.SelectedItemsFilter.Visible = this.ShowSelectedFilter;
      if (this.ShowSelectedFilter)
      {
        this.SelectedItemsFilter.Tabs[0].Text = this.AllItemsText;
        this.SelectedItemsFilter.Tabs[1].Text = this.SelectedItemsText;
        this.AllItemsLiteral.Text = this.AllItemsText;
        this.SelectedItemsLiteral.Text = this.SelectedItemsText;
      }
      this.SelectorBinder.DataKeyNames = this.DataKeyNames;
      this.SelectorBinder.UICulture = this.UICulture;
      if (!string.IsNullOrWhiteSpace(this.DefaultSortExpression))
        this.SelectorBinder.DefaultSortExpression = this.DefaultSortExpression;
      if (!this.AllowSearching)
        return;
      this.SetUpSearch();
    }

    private void SetUpSearch()
    {
      if (this.DataMembers.Where<DataMemberInfo>((Func<DataMemberInfo, bool>) (dm => dm.IsSearchField || dm.IsExtendedSearchField)).Count<DataMemberInfo>() == 0)
        throw new InvalidOperationException(Res.Get<ControlResources>().SearchingWithNoSearchFields);
      this.SelectorSearchBox.SearchFields = string.Join(",", this.DataMembers.Where<DataMemberInfo>((Func<DataMemberInfo, bool>) (dm => dm.IsSearchField)).Select<DataMemberInfo, string>((Func<DataMemberInfo, string>) (dm => dm.Name)));
      this.SelectorSearchBox.ExtendedSearchFields = string.Join(",", this.DataMembers.Where<DataMemberInfo>((Func<DataMemberInfo, bool>) (dm => dm.IsExtendedSearchField)).Select<DataMemberInfo, string>((Func<DataMemberInfo, string>) (dm => dm.Name)));
      this.SelectorSearchBox.SearchType = this.SearchType;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      if (string.IsNullOrEmpty(this.ItemType))
        throw new InvalidOperationException(string.Format(Res.Get<ControlResources>().PropertyNotSet, (object) "ItemType"));
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      controlDescriptor.AddProperty("_itemType", (object) this.ItemType);
      controlDescriptor.AddProperty("_itemSurrogateType", (object) this.ItemSurrogateType);
      controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      controlDescriptor.AddProperty("_dataKeyNames", (object) scriptSerializer.Serialize((object) this.DataKeyNames.Split(',')));
      controlDescriptor.AddProperty("_showSelectedFilter", (object) this.ShowSelectedFilter);
      if (this.ShowSelectedFilter)
      {
        controlDescriptor.AddProperty("_selectedItemsFilterId", (object) this.SelectedItemsFilter.ClientID);
        controlDescriptor.AddProperty("_numberOfSelectedItemsId", (object) this.NumberOfSelectedItems.ClientID);
      }
      controlDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad.ToString().ToLower());
      controlDescriptor.AddProperty("constantFilter", (object) this.ConstantFilter);
      if (this.SelectorSearchBox != null)
        controlDescriptor.AddProperty("_selectorSearchBoxId", (object) this.SelectorSearchBox.ClientID);
      if (!string.IsNullOrEmpty(this.OnClientSelectorReady))
        controlDescriptor.AddEvent("selectorReady", this.OnClientSelectorReady);
      if (!string.IsNullOrEmpty(this.OnItemSelected))
        controlDescriptor.AddEvent("itemSelected", this.OnItemSelected);
      if (!string.IsNullOrEmpty(this.OnBinderDataBound))
        controlDescriptor.AddEvent("binderDataBound", this.OnBinderDataBound);
      if (!string.IsNullOrEmpty(this.OnClientNeedsSelectedItemsServiceUrl))
        controlDescriptor.AddEvent("needsSelectedSeviceUrl", this.OnClientNeedsSelectedItemsServiceUrl);
      if (!this.OnClientError.IsNullOrWhitespace())
        controlDescriptor.AddEvent("error", this.OnClientError);
      if (!this.OnClientNeedsSelectedInPageServiceUrl.IsNullOrWhitespace())
        controlDescriptor.AddEvent("needsSelectedInPageServiceUrl", this.OnClientNeedsSelectedInPageServiceUrl);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string str = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new ScriptReference[1]
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.ItemSelector.js"
        }
      };
    }
  }
}
