// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// Base class for all content view designers. The class builds a basic structure
  /// of Content Selection tab, List Settings Tab and Single settings tab
  /// </summary>
  public class ContentViewDesignerBase : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.ContentViewDesignerBase.ascx");
    internal const string contentViewDesignerScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.ContentViewDesignerBase.js";
    private Dictionary<string, ControlDesignerView> designerViews;

    /// <summary>Gets or sets the multi page view.</summary>
    /// <value>The multi page.</value>
    protected RadMultiPage ViewsMultiPage => this.Container.GetControl<RadMultiPage>();

    /// <summary>Gets or sets the designer menu</summary>
    /// <value>The menu tab strip.</value>
    protected RadTabStrip MenuTabStrip => this.Container.GetControl<RadTabStrip>();

    /// <summary>Gets the message control.</summary>
    /// <value>The message control.</value>
    protected Message Message => this.Container.GetControl<Message>();

    /// <summary>
    /// Gets or sets the message text - used to show any warnings and errors on the designers
    /// </summary>
    /// <value>The message.</value>
    public string TopMessageText { get; set; }

    /// <summary>Gets or sets the type of the top message.</summary>
    /// <value>The type of the top message.</value>
    public MessageType TopMessageType { get; set; }

    /// <summary>Gets the designer views.</summary>
    /// <value>The designer views.</value>
    public Dictionary<string, ControlDesignerView> DesignerViews
    {
      get
      {
        if (this.designerViews == null)
          this.designerViews = new Dictionary<string, ControlDesignerView>();
        return this.designerViews;
      }
    }

    /// <summary>
    /// Gets the designer views whose tabs will be hidden in the MenuTabStrip.
    /// </summary>
    /// <value>The designer views.</value>
    protected virtual string[] HiddenDesignerViewNames => new string[0];

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ContentViewDesignerBase.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (ControlDesignerView controlDesignerView in this.DesignerViews.Values)
        dictionary.Add(controlDesignerView.ViewName, controlDesignerView.ClientID);
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("designerViewsMap", (object) dictionary);
      controlDescriptor.AddComponentProperty("menuTabStrip", this.MenuTabStrip.ClientID);
      controlDescriptor.AddScriptProperty("hiddenViews", this.SerializeHiddenViews());
      controlDescriptor.AddComponentProperty("message", this.Message.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    private string SerializeHiddenViews()
    {
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      return this.HiddenDesignerViewNames.Length == 0 ? "[]" : scriptSerializer.Serialize((object) this.HiddenDesignerViewNames);
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = typeof (ContentViewDesignerBase).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.ContentViewDesignerBase.js", assembly)
      };
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.AddViews(this.DesignerViews);
      this.BuildDesigner();
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Adds the views.</summary>
    /// <param name="views">The views.</param>
    protected virtual void AddViews(Dictionary<string, ControlDesignerView> views) => throw new NotImplementedException("Adding of Views not implemnted");

    /// <summary>
    /// Builds the navigation and adds the views to the designer.
    /// </summary>
    protected virtual void BuildDesigner()
    {
      foreach (ControlDesignerView child in this.DesignerViews.Values)
      {
        child.InitView((ControlDesignerBase) this);
        RadPageView pageView = new RadPageView();
        this.ViewsMultiPage.PageViews.Add(pageView);
        pageView.Controls.Add((Control) child);
        RadTab tab = new RadTab();
        this.MenuTabStrip.Tabs.Add(tab);
        tab.Text = child.ViewTitle;
        tab.Value = child.ViewName;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (string.IsNullOrEmpty(this.TopMessageText))
        return;
      this.Message.Visible = true;
      this.Message.MessageText = this.TopMessageText;
      this.Message.Status = this.TopMessageType;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    protected struct PropNames
    {
      public const string SelectorView = "selectorViewName";
      public const string SettingsView = "settingsViewName";
    }
  }
}
