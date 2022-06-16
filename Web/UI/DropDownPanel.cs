// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DropDownPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Represents expandable control container.</summary>
  [ParseChildren(false)]
  [PersistChildren(true)]
  [DefaultProperty("Text")]
  [ToolboxData("<{0}:DropDownPanel runat=server></{0}:DropDownPanel>")]
  public class DropDownPanel : Control, IPostBackDataHandler
  {
    private bool collapsed = true;
    private readonly string script = "\r\nfunction togleddpanel(id){\r\n\tvar el1 = document.getElementById(id + \"_cnt\");\r\n\tvar el2 = document.getElementById(id + \"_clp\");\r\n    var el3 = document.getElementById(id + \"_lnk\");\r\n\tif (el1){\r\n\t\tif (el1.style.display == \"none\"){\r\n\t\t\tel1.style.display = \"block\";\r\n\t\t\tel2.value = \"False\";\r\n            el3.className = \"ex\";\r\n\t\t} else {\r\n\t\t\tel1.style.display = \"none\";\r\n\t\t\tel2.value = \"True\";\r\n            el3.className = \"coll\";\r\n\t\t}\r\n\t}\r\n}\r\n";

    /// <summary>Gets or sets the text.</summary>
    /// <value>The text.</value>
    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Text
    {
      get => (string) this.ViewState[nameof (Text)] ?? string.Empty;
      set => this.ViewState[nameof (Text)] = (object) value;
    }

    /// <summary>Gets or sets the alternate text.</summary>
    /// <value>The alternate text.</value>
    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("")]
    [Localizable(true)]
    public virtual string AlternateText
    {
      get => (string) this.ViewState[nameof (AlternateText)] ?? string.Empty;
      set => this.ViewState[nameof (AlternateText)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.UI.DropDownPanel" /> is collapsed.
    /// </summary>
    /// <value><c>true</c> if collapsed; otherwise, <c>false</c>.</value>
    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue(true)]
    [Localizable(false)]
    public bool Collapsed
    {
      get => this.collapsed;
      set => this.collapsed = value;
    }

    /// <summary>Gets or sets the CSS class.</summary>
    /// <value>The CSS class.</value>
    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("")]
    public virtual string CssClass
    {
      get => (string) this.ViewState[nameof (CssClass)] ?? string.Empty;
      set => this.ViewState[nameof (CssClass)] = (object) value;
    }

    /// <summary>Gets the hidden ID.</summary>
    /// <value>The hidden ID.</value>
    protected string HiddenID => this.ClientID + "_clp";

    /// <summary>
    /// When implemented by a class, signals the server control to notify the ASP.NET application that the state of the control has changed.
    /// </summary>
    public void RaisePostDataChangedEvent()
    {
    }

    /// <summary>Loads the post data.</summary>
    /// <param name="postDataKey">The post data key.</param>
    /// <param name="postCollection">The post collection.</param>
    /// <returns></returns>
    public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
    {
      string post = postCollection[this.HiddenID];
      if (post == null || !(post != this.collapsed.ToString()))
        return false;
      this.collapsed = bool.Parse(post);
      return true;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        return;
      this.Page.RegisterRequiresPostBack((Control) this);
      ScriptManager.RegisterHiddenField((Control) this, this.HiddenID, this.collapsed.ToString());
      ScriptManager.RegisterClientScriptBlock((Control) this, this.GetType(), nameof (DropDownPanel), this.script, true);
    }

    /// <summary>
    /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.
    /// </summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      writer.AddAttribute(HtmlTextWriterAttribute.Class, !string.IsNullOrEmpty(this.CssClass) ? this.CssClass : "dropDownPanel");
      writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
      writer.RenderBeginTag(HtmlTextWriterTag.Div);
      string str = "javascript:togleddpanel('" + this.ClientID + "')";
      writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Collapsed ? "coll" : "ex");
      writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_lnk");
      writer.AddAttribute(HtmlTextWriterAttribute.Href, str);
      writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "needToConfirm = false;");
      writer.AddAttribute("allowDesignTime", "true");
      if (!string.IsNullOrEmpty(this.AlternateText))
        writer.AddAttribute(HtmlTextWriterAttribute.Title, this.AlternateText);
      writer.RenderBeginTag(HtmlTextWriterTag.A);
      writer.Write(this.Text);
      writer.RenderEndTag();
      writer.AddStyleAttribute(HtmlTextWriterStyle.Display, this.Collapsed ? "none" : "block");
      writer.AddAttribute(HtmlTextWriterAttribute.Class, "ddlContent");
      writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_cnt");
      writer.RenderBeginTag(HtmlTextWriterTag.Div);
      foreach (Control control in this.Controls)
        control.RenderControl(writer);
      writer.RenderEndTag();
      writer.RenderEndTag();
    }
  }
}
