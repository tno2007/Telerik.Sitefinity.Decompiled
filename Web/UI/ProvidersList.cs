// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ProvidersList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Control that lists all providers of a manager</summary>
  [ParseChildren(true)]
  public class ProvidersList : SimpleScriptView
  {
    internal const string JsPath = "Telerik.Sitefinity.Web.Scripts.ProvidersList.js";
    public static readonly string UiPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ProviderList.ascx");
    private Collection<ProvidersListItem> items;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ProvidersList" /> class.
    /// </summary>
    public ProvidersList() => this.LayoutTemplatePath = ProvidersList.UiPath;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.Message.Text = this.SelectProviderMessage;
      this.Message.CssClass = this.SelectProviderMessageCssClass;
    }

    /// <summary>
    /// Type name of the manager to whose provers we want to bind
    /// </summary>
    public string ManagerTypeName { get; set; }

    /// <summary>Type name of the item whose</summary>
    public string ItemTypeName { get; set; }

    /// <summary>
    /// Message to display next to the combo box that says something like "Select a provider"
    /// </summary>
    public string SelectProviderMessage { get; set; }

    /// <summary>
    /// Css class to apply to the element encompassing the "Select a provider" message
    /// </summary>
    public string SelectProviderMessageCssClass { get; set; }

    /// <summary>
    /// Add a handler to the client-side clientProviderChanging event
    /// </summary>
    public string OnClientProviderChanging { get; set; }

    /// <summary>
    /// Add a handler to the client-side clientProviderChanged event
    /// </summary>
    public string OnClientProviderChanged { get; set; }

    /// <summary>List of items</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<ProvidersListItem> Items
    {
      get
      {
        if (this.items == null)
          this.items = new Collection<ProvidersListItem>();
        return this.items;
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Get a list of items to send to the client</summary>
    /// <returns>Collection of items to send to the client</returns>
    protected virtual Collection<ProvidersListItem> ResolveItemsList()
    {
      if (this.Items.Count > 0)
        return this.Items;
      IManager manager = (IManager) null;
      if (!string.IsNullOrEmpty(this.ManagerTypeName))
        manager = ManagerBase.GetManager(this.ManagerTypeName);
      else if (!string.IsNullOrEmpty(this.ItemTypeName))
        manager = ManagerBase.GetMappedManager(this.ItemTypeName);
      if (manager == null)
        return this.Items;
      Collection<ProvidersListItem> collection = new Collection<ProvidersListItem>();
      foreach (string providerName in manager.GetProviderNames(ProviderBindingOptions.NoFilter))
        collection.Add(new ProvidersListItem()
        {
          ProviderName = providerName
        });
      return collection;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ProvidersList providersList = this;
      string str = new JavaScriptSerializer().Serialize((object) providersList.ResolveItemsList());
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (ProvidersList).FullName, providersList.ClientID);
      controlDescriptor.AddProperty("_providersNamesList", (object) str);
      controlDescriptor.AddProperty("_radComboId", (object) providersList.ComboBox.ClientID);
      if (!string.IsNullOrEmpty(providersList.OnClientProviderChanged))
        controlDescriptor.AddEvent("providerNameChanging", providersList.OnClientProviderChanged);
      if (!string.IsNullOrEmpty(providersList.OnClientProviderChanging))
        controlDescriptor.AddEvent("providerNameChanged", providersList.OnClientProviderChanging);
      yield return (ScriptDescriptor) controlDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      yield return new ScriptReference("Telerik.Sitefinity.Web.Scripts.ProvidersList.js", typeof (ProvidersList).Assembly.GetName().FullName);
    }

    /// <summary>Get a reference to the RadComboBox in the template</summary>
    protected virtual RadComboBox ComboBox => this.Container.GetControl<RadComboBox>("comboBox", true);

    /// <summary>
    /// Get a refrence to the label control in the template for displaying a message
    /// </summary>
    protected virtual Label Message => this.Container.GetControl<Label>("message", false);
  }
}
