// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.CommandBar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>This controls represents a bar with various command</summary>
  [ParseChildren(true)]
  public class CommandBar : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.CommandBar.ascx");
    private Collection<ToolboxItemBase> commands;
    private string wrapperTag;
    private string itemTag;
    private bool displayed = true;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommandBar.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the comma delimited list of toolbox items
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<ToolboxItemBase> Commands
    {
      get
      {
        if (this.commands == null)
          this.commands = new Collection<ToolboxItemBase>();
        return this.commands;
      }
    }

    /// <summary>
    /// Gets or sets the name of the client side function to be fired when client raises a command
    /// </summary>
    public string OnClientCommand { get; set; }

    /// <summary>Gets or sets the wrapper tag.</summary>
    /// <value>The wrapper tag.</value>
    public string WrapperTag
    {
      get
      {
        if (string.IsNullOrEmpty(this.wrapperTag))
          this.wrapperTag = "UL";
        return this.wrapperTag;
      }
      set => this.wrapperTag = value;
    }

    /// <summary>Gets or sets the wrapper CSS class.</summary>
    /// <value>The wrapper CSS class.</value>
    public string WrapperCssClass { get; set; }

    /// <summary>Gets or sets the item tag.</summary>
    /// <value>The item tag.</value>
    public string ItemTag
    {
      get
      {
        if (string.IsNullOrEmpty(this.itemTag))
          this.itemTag = "LI";
        return this.itemTag;
      }
      set => this.itemTag = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.UI.CommandBar" /> is displayed.
    /// </summary>
    /// <value><c>true</c> if displayed; otherwise, <c>false</c>.</value>
    public bool Displayed
    {
      get => this.displayed;
      set => this.displayed = value;
    }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      writer.AddAttribute("id", this.ClientID);
      writer.AddAttribute("class", this.WrapperCssClass);
      if (!this.Displayed)
        writer.AddAttribute("style", "display:none;");
      writer.RenderBeginTag(this.WrapperTag);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(typeof (CommandBar).FullName, this.ClientID);
      behaviorDescriptor.AddProperty("_commands", (object) scriptSerializer.Serialize((object) this.Commands));
      if (!string.IsNullOrEmpty(this.OnClientCommand))
        behaviorDescriptor.AddEvent("command", this.OnClientCommand);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.CommandBar.js", typeof (CommandBar).Assembly.FullName)
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (SystemManager.IsInlineEditingMode)
        this.SetButtonVisibility("viewAllSizes");
      foreach (ToolboxItemBase command in this.Commands)
      {
        if (command is ICommandButton)
        {
          if (string.IsNullOrEmpty(command.ContainerId))
          {
            command.ContainerId = this.ID;
            command.WrapperTagName = this.ItemTag;
            this.Controls.Add(((ICommandButton) command).GenerateCommandItem());
          }
        }
        else if (command is LiteralToolboxItem && string.IsNullOrEmpty(command.ContainerId))
        {
          command.ContainerId = this.ID;
          command.WrapperTagName = this.ItemTag;
          this.Controls.Add(((LiteralToolboxItem) command).GenerateItem());
        }
      }
    }

    /// <summary>Hides command button</summary>
    private void SetButtonVisibility(string commandName)
    {
      ICommandButton commandButton = this.Commands.OfType<ICommandButton>().Where<ICommandButton>((Func<ICommandButton, bool>) (b => b.CommandName == commandName)).FirstOrDefault<ICommandButton>();
      if (commandButton == null)
        return;
      ((ToolboxItemBase) commandButton).Visible = false;
    }
  }
}
