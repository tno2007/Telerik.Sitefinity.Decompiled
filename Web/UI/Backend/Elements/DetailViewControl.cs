// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.DetailViewControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements
{
  internal abstract class DetailViewControl : SimpleScriptView, IDefinedControl
  {
    public List<WidgetBar> widgetBars;
    protected List<SectionControl> sectionControls;
    private static string detailFormViewScript = "Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.DetailViewControl.js";

    /// <summary>Gets or sets the definition.</summary>
    public IViewDefinition Definition { get; set; }

    /// <summary>
    /// Gets the reference to the repeater which binds the sections of the form
    /// </summary>
    protected abstract Repeater Sections { get; }

    /// <summary>
    /// Gets the reference to the wrapper that holds top toolbar
    /// </summary>
    protected abstract Control TopToolbarWrapper { get; }

    /// <summary>
    /// Gets the reference to the wrapper that holds bottom toolbar
    /// </summary>
    protected abstract Control BottomToolbarWrapper { get; }

    /// <summary>Gets a reference to the message control.</summary>
    protected abstract Message MessageControl { get; }

    /// <summary>Gets the loading view.</summary>
    protected abstract HtmlGenericControl LoadingView { get; }

    /// <summary>Gets the back button.</summary>
    protected abstract HtmlAnchor BackButton { get; }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      IDetailViewControlDefinition definition = (IDetailViewControlDefinition) this.Definition;
      this.SetUpSections(definition.Sections);
      this.SetUpWidgetToolbars(definition);
    }

    internal void SetUpSections(
      IEnumerable<IContentViewSectionDefinition> sections)
    {
      this.Sections.DataSource = (object) sections;
      this.Sections.ItemCreated += new RepeaterItemEventHandler(this.Sections_ItemCreated);
      this.Sections.DataBind();
    }

    private void SetUpWidgetToolbars(IDetailViewControlDefinition def)
    {
      this.widgetBars = new List<WidgetBar>();
      ButtonBar child1 = new ButtonBar();
      this.widgetBars.Add((WidgetBar) child1);
      child1.WidgetBarDefiniton = def.Toolbar;
      this.BottomToolbarWrapper.Controls.Add((Control) child1);
      ButtonBar child2 = new ButtonBar();
      child2.WidgetBarDefiniton = def.Toolbar;
      this.TopToolbarWrapper.Controls.Add((Control) child2);
      this.widgetBars.Add((WidgetBar) child2);
      child2.TabIndex = (short) 2000;
      child1.TabIndex = (short) 1000;
    }

    /// <summary>
    /// Handles the ItemCreated event of the Sections control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    protected virtual void Sections_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.FindControl("section") is SectionControl control) || !(e.Item.DataItem is IContentViewSectionDefinition dataItem))
        return;
      control.SectionDefinition = dataItem;
      string name = string.IsNullOrEmpty(dataItem.ControlId) ? dataItem.Name : dataItem.ControlId;
      if (!string.IsNullOrEmpty(name))
        ControlUtilities.SetControlIdFromName(name, (Control) control);
      this.sectionControls.Add(control);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.sectionControls = new List<SectionControl>();

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      List<string> list = this.sectionControls.Select<SectionControl, string>((Func<SectionControl, string>) (n => n.ClientID)).ToList<string>();
      if (list.Count > 0)
        controlDescriptor.AddProperty("sectionIds", (object) scriptSerializer.Serialize((object) list));
      if (this.widgetBars != null && this.widgetBars.Count > 0)
      {
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        foreach (WidgetBar widgetBar in this.widgetBars)
        {
          stringList1.Add(widgetBar.ClientID);
          if (widgetBar is ButtonBar)
            stringList2.Add(widgetBar.ClientID);
        }
        string str1 = scriptSerializer.Serialize((object) stringList1);
        string str2 = scriptSerializer.Serialize((object) stringList1);
        controlDescriptor.AddProperty("widgetBarIds", (object) str1);
        controlDescriptor.AddProperty("buttonBarIds", (object) str2);
        controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
        controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
        controlDescriptor.AddElementProperty("backButton", this.BackButton.ClientID);
      }
      return (IEnumerable<ScriptDescriptor>) new List<ScriptDescriptor>()
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = typeof (DetailViewControl).Assembly.FullName;
      scriptReferences.Add(new ScriptReference(DetailViewControl.detailFormViewScript, fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
