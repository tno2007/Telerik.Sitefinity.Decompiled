// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.MultiSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Security.Principals;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// Displays many selectors and provides a client-side API for their interaction
  /// </summary>
  [ParseChildren(true, DefaultProperty = "Selectors")]
  public class MultiSelector : SimpleScriptView, IWidget
  {
    /// <summary>Name of the JavaScript component, including namespace</summary>
    internal static readonly string JsName = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.MultiSelector";
    /// <summary>Resource path to the embedded JavaScript component</summary>
    internal const string JsPath = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.MultiSelector.js";
    /// <summary>Resource path to the default template</summary>
    public static readonly string TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.MultiSelector.ascx");
    private Dictionary<string, MultiSelector.ClientSelector> clientSelectors;
    private Collection<SelectorItem> selectors;
    private bool areSelectorsReady;
    private Dictionary<string, ItemSelector> itemSelectors;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.MultiSelector" /> class.
    /// </summary>
    public MultiSelector() => this.LayoutTemplatePath = MultiSelector.TemplatePath;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.clientSelectors = new Dictionary<string, MultiSelector.ClientSelector>();
      this.itemSelectors = new Dictionary<string, ItemSelector>();
      IEnumerable<SelectorItem> selectorItems = this.ResolveSelectors();
      this.HeadsRepeater.DataSource = (object) selectorItems;
      this.HeadsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.Heads_ItemDataBound);
      this.HeadsRepeater.DataBind();
      this.BodiesRepeater.DataSource = (object) selectorItems;
      this.BodiesRepeater.ItemDataBound += new RepeaterItemEventHandler(this.Bodies_ItemDataBound);
      this.BodiesRepeater.Load += new EventHandler(this.BodiesRepeater_Load);
      this.BodiesRepeater.DataBind();
      ItemSelector itemSelector = this.itemSelectors.Values.Where<ItemSelector>((Func<ItemSelector, bool>) (x => x.GetType() == typeof (RoleSelector))).FirstOrDefault<ItemSelector>();
      if (itemSelector == null)
        return;
      ((RoleSelector) itemSelector).HideAnonymousRole = this.HideAnonymousRole;
    }

    /// <summary>Handles the Load event of the BodiesRepeater control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void BodiesRepeater_Load(object sender, EventArgs e)
    {
      this.areSelectorsReady = true;
      if (this.SelectorsReady == null)
        return;
      this.SelectorsReady();
    }

    /// <summary>
    /// Extension point: decide which selectors to use : from template or definition
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerable<SelectorItem> ResolveSelectors() => (IEnumerable<SelectorItem>) this.Selectors;

    private void Bodies_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item || !(e.Item.DataItem is SelectorItem dataItem))
        return;
      Control control1 = e.Item.FindControl("bodies");
      Control control2 = e.Item.FindControl("body");
      if (control1 == null || control2 == null)
        return;
      dataItem.Body.InstantiateIn(control2);
      ItemSelector firstSelector = this.FindFirstSelector(control2);
      this.itemSelectors[dataItem.Name] = firstSelector != null ? firstSelector : throw new InvalidDataException();
      firstSelector.BindOnLoad = false;
      if (!this.clientSelectors.ContainsKey(dataItem.Name))
      {
        this.clientSelectors.Add(dataItem.Name, new MultiSelector.ClientSelector((Control) null, (Control) null, control2, firstSelector, dataItem));
      }
      else
      {
        MultiSelector.ClientSelector clientSelector = this.clientSelectors[dataItem.Name];
        clientSelector.Body = control2;
        clientSelector.SelectorComponent = firstSelector;
      }
    }

    private void Heads_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item || !(e.Item.DataItem is SelectorItem dataItem))
        return;
      Control control1 = e.Item.FindControl("heads");
      Control control2 = e.Item.FindControl("title");
      if (control1 == null || control2 == null)
        return;
      dataItem.Header.InstantiateIn(control2);
      if (!this.clientSelectors.ContainsKey(dataItem.Name))
        this.clientSelectors.Add(dataItem.Name, new MultiSelector.ClientSelector((Control) null, control2, (Control) null, (ItemSelector) null, dataItem));
      else
        this.clientSelectors[dataItem.Name].Title = control2;
    }

    private ItemSelector FindFirstSelector(Control container)
    {
      if (container == null)
        return (ItemSelector) null;
      if (typeof (ItemSelector).IsAssignableFrom(container.GetType()))
        return (ItemSelector) container;
      foreach (Control control in container.Controls)
      {
        ItemSelector firstSelector = this.FindFirstSelector(control);
        if (firstSelector != null)
          return firstSelector;
      }
      return (ItemSelector) null;
    }

    public event MultiSelector.SelectorsReadyHandler SelectorsReady;

    /// <summary>
    /// Retrieves a dictionary (pageId, instance) of item selectors
    /// </summary>
    public Dictionary<string, ItemSelector> ItemSelectors => this.itemSelectors;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Selectors defined declaratively in the template</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<SelectorItem> Selectors
    {
      get
      {
        if (this.selectors == null)
          this.selectors = new Collection<SelectorItem>();
        return this.selectors;
      }
    }

    /// <summary>
    /// Command name to use when raising the command event on the client - when a selector has changed its selection
    /// </summary>
    public string CommandName { get; set; }

    /// <summary>
    /// Name of javascript function to be invoked when the client-side component raises the client "command" event
    /// </summary>
    public string OnClientCommand { get; set; }

    /// <summary>
    /// Name of javascript function to be invoked when the client-side component raises the client "activated" event
    /// </summary>
    public string OnClientActivated { get; set; }

    /// <summary>
    /// Name of javascript function to be invoked when the client-side component raises the client "deactivated" event
    /// </summary>
    public string OnClientDeactivated { get; set; }

    /// <summary>Get/set whether to hide the anonymous role</summary>
    internal bool HideAnonymousRole { get; set; }

    /// <summary>Gets or sets the generic widget definition.</summary>
    /// <value>The generic widget definition.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual IWidgetDefinition Definition { get; set; }

    public void Configure(IWidgetDefinition definition) => this.Definition = definition;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(MultiSelector.JsName, this.ClientID);
      string str = new JavaScriptSerializer().Serialize((object) this.clientSelectors);
      controlDescriptor.AddProperty("_selectors", (object) str);
      controlDescriptor.AddProperty("commandName", (object) this.CommandName);
      if (!this.OnClientActivated.IsNullOrWhitespace())
        controlDescriptor.AddEvent("activated", this.OnClientActivated);
      if (!this.OnClientDeactivated.IsNullOrWhitespace())
        controlDescriptor.AddEvent("deactivated", this.OnClientDeactivated);
      if (!this.OnClientCommand.IsNullOrWhitespace())
        controlDescriptor.AddEvent("command", this.OnClientCommand);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.SitefinityJS.UI.IWidget.js", typeof (IWidget).Assembly.GetName().FullName),
      new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.MultiSelector.js", typeof (MultiSelector).Assembly.GetName().FullName)
    };

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets the Cascading Style Sheet (CSS) class rendered by the Web server control on the client.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.
    /// </returns>
    public override string CssClass
    {
      get => this.Definition.CssClass;
      set => this.Definition.CssClass = value;
    }

    /// <summary>Reference to the repeater that renders templates</summary>
    protected virtual Repeater HeadsRepeater => this.Container.GetControl<Repeater>(MultiSelector.ControlRefs.headsRepeater, true);

    protected virtual Repeater BodiesRepeater => this.Container.GetControl<Repeater>(MultiSelector.ControlRefs.bodiesRepeater, true);

    public delegate void SelectorsReadyHandler();

    /// <summary>Control references</summary>
    internal static class ControlRefs
    {
      public static readonly string headsRepeater = nameof (headsRepeater);
      public static readonly string bodiesRepeater = nameof (bodiesRepeater);
    }

    /// <summary>
    /// Determines the data to send to the client about an individual selector
    /// </summary>
    private class ClientSelector
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.MultiSelector.ClientSelector" /> struct.
      /// </summary>
      /// <param name="sectionId">Id of the tag that holds the title template</param>
      /// <param name="titleId">Id of the tag that holds the title template</param>
      /// <param name="bodyId">Id of the tag that holds the selector template</param>
      /// <param name="selectorId">Id of the tag that is associated with the client-side selector (component)</param>
      /// <param name="notSelectedClass">Css class to apply to the header wrapper when not selected</param>
      /// <param name="selectedClass">Css class to apply to the header wrapper when selected</param>
      public ClientSelector(
        Control section,
        Control title,
        Control body,
        ItemSelector selector,
        SelectorItem item)
      {
        this.Section = section;
        this.Title = title;
        this.Body = body;
        this.SelectorComponent = selector;
        this.Name = item.Name;
        this.SelectedCssClass = item.SelectedCssClass;
        this.NotSelectedCssClass = item.NotSelectedCssClass;
        this.InvalidatesNames = !item.InvalidatesNames.IsNullOrWhitespace() ? item.InvalidatesNames.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) : new string[0];
        this.InvalidatesNames = ((IEnumerable<string>) this.InvalidatesNames).Select<string, string>((Func<string, string>) (s => s.Trim())).ToArray<string>();
      }

      /// <summary>Name associated with the selector</summary>
      public string Name { get; private set; }

      /// <summary>Optional pageId of the wrapper encompassing tag</summary>
      public string SectionClientId => this.Section != null ? this.Section.ClientID : (string) null;

      /// <summary>Id of the tag that holds the title template</summary>
      public string TitleClientId => this.Title != null ? this.Title.ClientID : (string) null;

      /// <summary>Id of the tag that holds the selector template</summary>
      public string BodyClientId => this.Body != null ? this.Body.ClientID : (string) null;

      /// <summary>
      /// Id of the tag that is associated with the client-side selector (component)
      /// </summary>
      public string SelectorComponentId => this.SelectorComponent != null ? this.SelectorComponent.ClientID : (string) null;

      /// <summary>
      /// Css class to apply to the header wrapper when selected
      /// </summary>
      public string SelectedCssClass { get; set; }

      /// <summary>
      /// Css class to apply to the header wrapper when not selected
      /// </summary>
      public string NotSelectedCssClass { get; private set; }

      /// <summary>List of names to invalidate when selection changes</summary>
      public string[] InvalidatesNames { get; private set; }

      /// <summary>Gets or sets the body.</summary>
      /// <value>The body.</value>
      [ScriptIgnore]
      public Control Body { get; set; }

      /// <summary>Gets or sets the selector component.</summary>
      /// <value>The selector component.</value>
      [ScriptIgnore]
      public ItemSelector SelectorComponent { get; set; }

      /// <summary>Gets or sets the section.</summary>
      /// <value>The section.</value>
      [ScriptIgnore]
      public Control Section { get; set; }

      /// <summary>Gets or sets the title.</summary>
      /// <value>The title.</value>
      [ScriptIgnore]
      public Control Title { get; set; }
    }
  }
}
