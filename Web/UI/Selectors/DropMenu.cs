// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Selectors.DropMenu
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Selectors
{
  /// <summary>
  /// Control which is very similar to DropDownList in its functionality, however, provides a drop down menu
  /// and is more user friendly.
  /// </summary>
  public class DropMenu : CompositeControl, IScriptControl
  {
    private IDictionary<string, string> selectorItems;
    private ITemplate layoutTemplate;
    private GenericContainer container;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.DropMenu.ascx");
    private string customLayoutTempalte;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public virtual string LayoutTemplatePath
    {
      get => !string.IsNullOrWhiteSpace(this.customLayoutTempalte) ? this.customLayoutTempalte : DropMenu.layoutTemplatePath;
      set => this.customLayoutTempalte = value;
    }

    /// <summary>Gets the layout template for the control.</summary>
    /// <value>The layout template of the control.</value>
    public virtual ITemplate LayoutTemplate
    {
      get
      {
        if (this.layoutTemplate == null)
          this.layoutTemplate = ControlUtilities.GetTemplate(this.LayoutTemplatePath, (string) null, Config.Get<ControlsConfig>().ResourcesAssemblyInfo, (string) null);
        return this.layoutTemplate;
      }
    }

    /// <summary>
    /// Gets or sets the text which is used to label the currently selected item.
    /// </summary>
    public virtual string CurrentItemText { get; set; }

    /// <summary>
    /// Gets or sets the text used as a link for changing the currently selected item.
    /// </summary>
    public virtual string ChangeText { get; set; }

    /// <summary>Gets or sets the selected text.</summary>
    /// <value>The selected text.</value>
    public virtual string SelectedText
    {
      get => this.CurrentItem.Text;
      set
      {
        if (this.SelectorItems == null)
          throw new InvalidOperationException("You cannot set the SelectedText before you have set the SelectorItems dictionary.");
        this.CurrentItem.Text = this.SelectorItems.ContainsKey(value) ? value : throw new InvalidOperationException("The key does not exist in the SelectorItems dictionary.");
        this.CurrentItemValue.Value = this.SelectorItems[value];
      }
    }

    /// <summary>Gets the selected value.</summary>
    /// <value>The selected value.</value>
    public virtual string SelectedValue => this.CurrentItemValue.Value;

    /// <summary>
    /// Gets or sets the name of the client side function to be called
    /// when user changes the selected item.
    /// </summary>
    /// <value>The name of the javascript function.</value>
    public string OnClientSelectedItemChanged { get; set; }

    /// <summary>
    /// Gets or sets the items among which selection can be made.
    /// </summary>
    public virtual IDictionary<string, string> SelectorItems
    {
      get
      {
        if (this.selectorItems == null)
          this.selectorItems = (IDictionary<string, string>) new Dictionary<string, string>();
        return this.selectorItems;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the change link needs to be
    /// clicked in order for items to appear.
    /// </summary>
    /// <value><c>true</c> if [click to open]; otherwise, <c>false</c>.</value>
    public virtual bool ClickToOpen
    {
      get
      {
        object obj = this.ViewState[nameof (ClickToOpen)];
        return obj == null || (bool) obj;
      }
      set => this.ViewState[nameof (ClickToOpen)] = (object) value;
    }

    /// <summary>Gets or sets the css class of the selected item.</summary>
    /// <value>The selected item CSS class.</value>
    public virtual string SelectedItemCssClass { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the reference to the control which represents a label for
    /// the currently selected item.
    /// </summary>
    /// <value>The current item label.</value>
    protected virtual ITextControl CurrentItemLabel => this.Container.GetControl<ITextControl>("currentItemLabel", false);

    /// <summary>
    /// Gets the reference to the control which represents the currently
    /// selected item.
    /// </summary>
    /// <value>The current item.</value>
    protected virtual ITextControl CurrentItem => this.Container.GetControl<ITextControl>("currentItem", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the value of
    /// the currently selected item.
    /// </summary>
    /// <value>The current item value.</value>
    protected virtual HiddenField CurrentItemValue => this.Container.GetControl<HiddenField>("currentItemValue", true);

    /// <summary>
    /// Gets the reference to the RadMenu control which holds all the possible
    /// items that can be selected.
    /// </summary>
    /// <value>The options menu.</value>
    protected virtual RadMenu OptionsMenu => this.Container.GetControl<RadMenu>("optionsMenu", true);

    /// <summary>
    /// Gets the reference to the GenericContainer control used to instantiate the
    /// control template.
    /// </summary>
    /// <value>The container.</value>
    protected virtual GenericContainer Container
    {
      get
      {
        if (this.container == null)
        {
          this.container = new GenericContainer();
          this.LayoutTemplate.InstantiateIn((Control) this.container);
        }
        return this.container;
      }
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      ScriptManager scriptManager = this.Page != null ? ScriptManager.GetCurrent(this.Page) : throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      if (scriptManager == null)
        throw new HttpException(Res.Get<ErrorMessages>().ScriptManagerIsNull);
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.JQuery);
      scriptManager.RegisterScriptControl<DropMenu>(this);
      this.OptionsMenu.OnClientItemClicking = this.GetOnClickingHandlerName();
      if (this.Page == null)
        return;
      this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "customScript", this.GetOnClickingScript(), true);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor("Telerik.Sitefinity.Web.UI.Selectors.DropMenu", this.ClientID);
      controlDescriptor.AddProperty("selectedTextElementId", (object) ((Control) this.CurrentItem).ClientID);
      controlDescriptor.AddProperty("selectedValueElementId", (object) this.CurrentItemValue.ClientID);
      controlDescriptor.AddProperty("optionsMenuElementId", (object) this.OptionsMenu.ClientID);
      controlDescriptor.AddProperty("selectedItemCssClass", (object) this.SelectedItemCssClass);
      if (!string.IsNullOrEmpty(this.OnClientSelectedItemChanged))
        controlDescriptor.AddEvent("onSelectedItemChanged", this.OnClientSelectedItemChanged);
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
    public IEnumerable<ScriptReference> GetScriptReferences()
    {
      string str = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new ScriptReference[1]
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.DropMenu.js"
        }
      };
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use
    /// composition-based implementation to create any child controls they contain
    /// in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      if (this.CurrentItemLabel != null)
        this.CurrentItemLabel.Text = !string.IsNullOrEmpty(this.CurrentItemText) ? this.CurrentItemText : Res.Get<Labels>().LabelsIn;
      this.SetMenu();
      this.Controls.Add((Control) this.Container);
    }

    /// <summary>Sets the menu.</summary>
    public void SetMenu()
    {
      this.OptionsMenu.ClickToOpen = this.ClickToOpen;
      this.OptionsMenu.Items.Clear();
      RadMenuItem radMenuItem1 = new RadMenuItem(Res.Get<Labels>().Change);
      this.OptionsMenu.Items.Add(radMenuItem1);
      if (this.SelectorItems == null)
        return;
      foreach (KeyValuePair<string, string> selectorItem in (IEnumerable<KeyValuePair<string, string>>) this.SelectorItems)
      {
        RadMenuItem radMenuItem2 = new RadMenuItem();
        radMenuItem2.Text = selectorItem.Key;
        radMenuItem2.Value = selectorItem.Value;
        radMenuItem1.Items.Add(radMenuItem2);
      }
    }

    private string GetOnClickingScript()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("function {0}(sender, args) {{", (object) this.GetOnClickingHandlerName());
      stringBuilder.AppendFormat("$find('{0}').OnMenuItemClicking(sender, args);", (object) this.ClientID);
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }

    private string GetOnClickingHandlerName() => this.ClientID + "_" + "Clicking";
  }
}
