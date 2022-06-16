// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DecisionScreen
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Control used for presenting a user with choices of available actions at a given moment
  /// </summary>
  [ParseChildren(true)]
  public class DecisionScreen : SimpleView, IScriptControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.DecisionScreen.ascx");
    private const string decisionScreenScript = "Telerik.Sitefinity.Web.Scripts.DecisionScreen.js";
    private Collection<ActionItem> actionItems;

    /// <summary>Gets or sets the text of the message</summary>
    public DecisionType DecisionType { get; set; }

    /// <summary>Gets or sets the text of the message</summary>
    public string MessageText { get; set; }

    /// <summary>Gets or sets the type of the message</summary>
    public MessageType MessageType { get; set; }

    /// <summary>Gets or sets the title of the decision screen</summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets the collection of action items to be displayed on the decision screen.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<ActionItem> ActionItems
    {
      get
      {
        if (this.actionItems == null)
          this.actionItems = new Collection<ActionItem>();
        return this.actionItems;
      }
    }

    /// <summary>
    /// Determines whether control will be displayed or not. Different from visible, because the control will
    /// be rendered (though hidden on the client) when the property is set to false
    /// </summary>
    public bool Displayed { get; set; }

    /// <summary>
    /// Gets or sets the name of the client side function to be executed upon an action command
    /// </summary>
    public string OnClientActionCommand { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DecisionScreen.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the reference to the message control</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the reference to the title literal control</summary>
    protected virtual Literal TitleLiteral => this.Container.GetControl<Literal>("titleLiteral", true);

    /// <summary>
    /// Gets the reference to the repeater control which displayes the possible
    /// actions user can take when presented with a given decision.
    /// </summary>
    protected virtual Repeater ActionsRepeater => this.Container.GetControl<Repeater>("actionsRepeater", true);

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the
    /// specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents
    /// the output stream to render HTML content on the client.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the event data.
    /// </param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.MicrosoftAjax | ScriptRef.MicrosoftAjaxWebForms | ScriptRef.JQuery | ScriptRef.JQueryValidate | ScriptRef.TelerikSitefinity).RegisterScriptControl<DecisionScreen>(this);
      if (!this.Displayed)
        this.Style.Add("display", "none");
      else
        this.Style.Add("display", "");
      this.ActionsRepeater.DataBind();
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.MessageControl.MessageText = this.MessageText;
      this.MessageControl.Status = this.MessageType;
      this.TitleLiteral.Text = this.Title;
      this.ActionsRepeater.DataSource = (object) this.ActionItems;
      this.ActionsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ActionsRepeater_ItemDataBound);
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Handles the ItemDataBound event of the ActionsRepeater control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    protected void ActionsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      ActionItem dataItem = (ActionItem) e.Item.DataItem;
      HtmlGenericControl control1 = (HtmlGenericControl) e.Item.FindControl("actionItem");
      LinkButton control2 = (LinkButton) e.Item.FindControl("actionLink");
      if (!dataItem.Visible)
        control1.Attributes.Add("style", "display:none");
      control1.Attributes["class"] = dataItem.CssClass;
      control2.Text = dataItem.Title;
      control2.OnClientClick = "return false;";
      dataItem.LinkClientId = control2.ClientID;
      if (string.IsNullOrEmpty(dataItem.NavigateUrl))
        return;
      dataItem.NavigateUrl = VirtualPathUtility.ToAbsolute(dataItem.NavigateUrl);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(typeof (DecisionScreen).FullName, this.ClientID);
      if (this.ActionItems.Count > 0)
      {
        JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
        behaviorDescriptor.AddProperty("_actionItems", (object) scriptSerializer.Serialize((object) this.ActionItems.ToArray<ActionItem>()));
        if (!string.IsNullOrEmpty(this.OnClientActionCommand))
          behaviorDescriptor.AddEvent("actionCommand", this.OnClientActionCommand);
        behaviorDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      }
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
    public IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.DecisionScreen.js", typeof (DecisionScreen).Assembly.FullName)
    };
  }
}
