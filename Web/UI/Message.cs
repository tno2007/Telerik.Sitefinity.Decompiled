// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Message
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Control for showing animated messages.</summary>
  public class Message : WebControl, IScriptControl
  {
    private int removeAfter = 60000;
    private Color startPositiveColor = Color.FromArgb(1, 211, 234, 187);
    private Color endPositiveColor = Color.FromArgb(1, 211, 234, 187);
    private Color startNeutralColor = Color.FromArgb(1, (int) byte.MaxValue, (int) byte.MaxValue, 204);
    private Color endNeutralColor = Color.FromArgb(1, (int) byte.MaxValue, (int) byte.MaxValue, 204);
    private Color startNegativeColor = Color.FromArgb(1, (int) byte.MaxValue, 185, 185);
    private Color endNegativeColor = Color.FromArgb(1, (int) byte.MaxValue, 185, 185);
    private int fadeAnimation = 3000;
    private bool animate;
    private string messageText;
    private MessageType messageType = MessageType.Neutral;
    private HtmlTextWriterTag elementTag = HtmlTextWriterTag.Div;
    private const string messageControlScriptName = "Telerik.Sitefinity.Web.Scripts.Message.js";
    private const string jQueryUiScriptName = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";

    /// <summary>Gets or sets the status.</summary>
    /// <value>The status.</value>
    public MessageType Status
    {
      get => this.messageType;
      set => this.messageType = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether animate the message.
    /// </summary>
    /// <value><c>true</c> to animate; otherwise, <c>false</c>.</value>
    public bool Animate
    {
      get => this.animate;
      set => this.animate = value;
    }

    /// <summary>Gets or sets the message text.</summary>
    /// <value>The message text.</value>
    public string MessageText
    {
      get => this.messageText;
      set => this.messageText = value;
    }

    /// <summary>Gets or sets the start color of the positive message.</summary>
    /// <value>The start color of the positive message.</value>
    public Color StartPositiveColor
    {
      get => this.startPositiveColor;
      set => this.startPositiveColor = value;
    }

    /// <summary>Gets or sets the end color of the positive message.</summary>
    /// <value>The end color of the positive message.</value>
    public Color EndPositiveColor
    {
      get => this.endPositiveColor;
      set => this.endPositiveColor = value;
    }

    /// <summary>Gets or sets the start color of the neutral message.</summary>
    /// <value>The start color of the neutral message.</value>
    public Color StartNeutralColor
    {
      get => this.startNeutralColor;
      set => this.startNeutralColor = value;
    }

    /// <summary>Gets or sets the end color of the neutral message.</summary>
    /// <value>The end color of the neutral message.</value>
    public Color EndNeutralColor
    {
      get => this.endNeutralColor;
      set => this.endNeutralColor = value;
    }

    /// <summary>Gets or sets the start color of the negative message.</summary>
    /// <value>The start color of the negative message.</value>
    public Color StartNegativeColor
    {
      get => this.startNegativeColor;
      set => this.startNegativeColor = value;
    }

    /// <summary>Gets or sets the end color of the negative message.</summary>
    /// <value>The end color of the negative message.</value>
    public Color EndNegativeColor
    {
      get => this.endNegativeColor;
      set => this.endNegativeColor = value;
    }

    /// <summary>
    /// Gets or sets the duration in miliseconds after which to remove the message.
    /// Duration is measured from the start of fade animation.
    /// </summary>
    /// <value>The duration in miliseconds.</value>
    public int RemoveAfter
    {
      get => this.removeAfter;
      set => this.removeAfter = value;
    }

    /// <summary>Gets or sets the duration of the fade effect.</summary>
    /// <value>The duration of the fade effect in miliseconds.</value>
    public int FadeDuration
    {
      get => this.fadeAnimation;
      set => this.fadeAnimation = value;
    }

    /// <summary>Gets or sets the element tag.</summary>
    /// <value>The element tag.</value>
    public HtmlTextWriterTag ElementTag
    {
      get => this.elementTag;
      set => this.elementTag = value;
    }

    /// <summary>
    /// Gets or sets the on client-side JavaScript handler for load event.
    /// </summary>
    /// <value>The name of the client-side JavaScript function.</value>
    public string OnClientLoad { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => this.elementTag;

    /// <summary>Shows the positive message.</summary>
    /// <param name="message">The message.</param>
    public void ShowPositiveMessage(string message)
    {
      this.messageText = message;
      this.messageType = MessageType.Positive;
    }

    /// <summary>Shows the neutral message.</summary>
    /// <param name="message">The message.</param>
    public void ShowNeutralMessage(string message)
    {
      this.messageText = message;
      this.messageType = MessageType.Neutral;
    }

    /// <summary>Shows the negative message.</summary>
    /// <param name="message">The message.</param>
    public void ShowNegativeMessage(string message)
    {
      this.messageText = message;
      this.messageType = MessageType.Negative;
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
      if (string.IsNullOrEmpty(this.CssClass))
        this.CssClass = "sfMessage";
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.JQuery).RegisterScriptControl<Message>(this);
      base.OnPreRender(e);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor("Telerik.Sitefinity.Web.UI.Message", this.ClientID);
      controlDescriptor.AddProperty("_startPositiveColor", (object) ColorTranslator.ToHtml(this.StartPositiveColor));
      controlDescriptor.AddProperty("_endPositiveColor", (object) ColorTranslator.ToHtml(this.EndPositiveColor));
      controlDescriptor.AddProperty("_startNeutralColor", (object) ColorTranslator.ToHtml(this.StartNeutralColor));
      controlDescriptor.AddProperty("_endNeutralColor", (object) ColorTranslator.ToHtml(this.EndNeutralColor));
      controlDescriptor.AddProperty("_startNegativeColor", (object) ColorTranslator.ToHtml(this.StartNegativeColor));
      controlDescriptor.AddProperty("_endNegativeColor", (object) ColorTranslator.ToHtml(this.EndNegativeColor));
      controlDescriptor.AddProperty("_fadeDuration", (object) this.FadeDuration);
      controlDescriptor.AddProperty("_removeAfter", (object) this.RemoveAfter);
      controlDescriptor.AddProperty("_messageText", (object) this.messageText);
      controlDescriptor.AddProperty("_messageType", (object) Enum.GetName(typeof (MessageType), (object) this.messageType));
      controlDescriptor.AddProperty("_animate", (object) this.animate);
      controlDescriptor.AddProperty("_commandButtons", (object) this.GetCommandButtons());
      if (!string.IsNullOrEmpty(this.OnClientLoad))
        controlDescriptor.AddEvent("onClientLoad", this.OnClientLoad);
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
    public IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = this.GetType().Assembly.FullName;
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.Message.js", fullName));
      if (this.Animate)
      {
        string name = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name;
        scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", name));
      }
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    private string GetCommandButtons() => new JavaScriptSerializer().Serialize((object) Config.Get<AppearanceConfig>().StatusCommands.Values.Select(f => new
    {
      CommandName = f.CommandName,
      Title = this.GetLocalizedTitle(f.Title, f.ResourceKey, f.CommandName),
      CssClass = f.CssClass
    }).ToArray());

    private string GetLocalizedTitle(string title, string resourceKey, string commandName)
    {
      string empty = string.Empty;
      if (!string.IsNullOrEmpty(resourceKey))
      {
        if (string.IsNullOrEmpty(title))
        {
          string str = commandName + "Title";
        }
        Res.Get(resourceKey, title);
      }
      return title ?? commandName;
    }
  }
}
