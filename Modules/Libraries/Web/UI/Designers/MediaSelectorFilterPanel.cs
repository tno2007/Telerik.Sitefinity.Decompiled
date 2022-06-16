// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaSelectorFilterPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Filter panel for filtering displayed media content items by selected parent library or folder.
  /// The panel is used at <see cref="T:Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog" /> media selector dialog.
  /// </summary>
  public class MediaSelectorFilterPanel : SimpleScriptView
  {
    private bool bindOnLoad = true;
    private const string ViewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaSelectorFilterPanel.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.MediaSelectorFilterPanel.ascx");
    private bool showLibFilterWrp = true;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MediaSelectorFilterPanel.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    public string ContentType { get; set; }

    /// <summary>Gets or sets the type of the parent.</summary>
    /// <value>The type of the parent.</value>
    public string ParentType { get; set; }

    /// <summary>Gets or sets the provider name.</summary>
    /// <value>The type of the content.</value>
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets a localized string representing the item in plural (for example Images).
    /// </summary>
    public string ItemsName { get; set; }

    /// <summary>
    /// Gets or sets a localized string representing the item in singular (for example Image).
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>
    /// Gets the configured value of how many items should be displayed on the first load. This configuration enables the control to load items only when required.
    /// </summary>
    public int ItemsCount => Config.Get<LibrariesConfig>().ItemsCount;

    /// <summary>
    /// Gets or sets a value indicating whether the control will be bound on load.
    /// Default value is true.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the control is bind on load; otherwise, <c>false</c>.
    /// </value>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>Gets or sets the service URL used for the library.</summary>
    public string LibraryServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets whether library filter wrapper should be visible
    /// </summary>
    internal bool ShowLibFilterWrp
    {
      get => this.showLibFilterWrp;
      set => this.showLibFilterWrp = value;
    }

    /// <summary>
    /// Gets or sets the option to hide options switcher and use only select option.
    /// </summary>
    /// <value>The flag, indicating if only select mode will be used.</value>
    public bool UseOnlySelectMode { get; set; }

    /// <summary>
    /// Gets or sets the id where the uploaded content will be saved.
    /// </summary>
    /// <value>The id where to save the uploaded content.</value>
    public Guid TargetLibraryId { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the command bar.</summary>
    /// <value>The command bar.</value>
    protected internal virtual CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true);

    /// <summary>Gets a reference to the libraries filter panel.</summary>
    public virtual Panel LibraryFilterWrp => this.Container.GetControl<Panel>("libraryFilterWrp", true);

    /// <summary>Gets the reference to the library selector control.</summary>
    protected internal virtual GenericPageSelector LibrarySelector => this.Container.GetControl<GenericPageSelector>("librarySelector", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.ShowLibFilterWrp)
      {
        string str = VirtualPathUtility.AppendTrailingSlash(this.LibraryServiceUrl);
        this.LibrarySelector.WebServiceUrl = str;
        this.LibrarySelector.OrginalServiceBaseUrl = str;
        this.LibrarySelector.ServiceChildItemsBaseUrl = str;
        this.LibrarySelector.ServicePredecessorBaseUrl = str + "predecessors/";
        this.LibrarySelector.ServiceTreeUrl = str + "tree/";
        this.LibrarySelector.AllowSearch = false;
        this.LibrarySelector.ConstantFilter = (string) null;
        this.LibrarySelector.TargetLibraryId = this.TargetLibraryId;
        if (this.TargetLibraryId != Guid.Empty)
        {
          this.HideCommandToolboxItem(this.CommandBar, "showRecent");
          this.HideCommandToolboxItem(this.CommandBar, "showMy");
          this.HideCommandToolboxItem(this.CommandBar, "showAll");
        }
        foreach (ToolboxItemBase command in this.CommandBar.Commands)
        {
          if (command is ICommandButton)
          {
            CommandToolboxItem commandToolboxItem = (CommandToolboxItem) command;
            string commandName = commandToolboxItem.CommandName;
            if (!(commandName == "upload"))
            {
              if (!(commandName == "showRecent"))
              {
                if (!(commandName == "showMy"))
                {
                  if (commandName == "showAll")
                    commandToolboxItem.Text = string.Format(Res.Get<LibrariesResources>().ShowAll, (object) this.ItemsName.ToLower());
                }
                else
                  commandToolboxItem.Text = string.Format(Res.Get<LibrariesResources>().ShowMy, (object) this.ItemsName.ToLower());
              }
              else
                commandToolboxItem.Text = string.Format(Res.Get<LibrariesResources>().ShowRecent, (object) this.ItemsName.ToLower());
            }
            else
            {
              commandToolboxItem.Text = string.Format("{0} {1}", (object) Res.Get<LibrariesResources>().Upload, (object) this.ItemName.ToLower());
              commandToolboxItem.Visible = !this.UseOnlySelectMode;
            }
          }
          else if (command is LiteralToolboxItem && this.TargetLibraryId == Guid.Empty)
            ((LiteralToolboxItem) command).Text = string.Format("<h2 class=\"{1}sfPTop15 sfMRight20\">{0}</h2>", (object) string.Format(Res.Get<LibrariesResources>().SelectFromAlreadyUploaded, (object) this.ItemName.ToLower()), this.UseOnlySelectMode ? (object) string.Empty : (object) "sfHBorderTop ");
        }
      }
      else
        this.LibraryFilterWrp.Visible = false;
      this.LibrarySelector.BindOnLoad = this.BindOnLoad;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (MediaSelectorFilterPanel).FullName, this.ClientID);
      controlDescriptor.AddProperty("_parentType", (object) this.ParentType);
      controlDescriptor.AddProperty("_contentType", (object) this.ContentType);
      controlDescriptor.AddProperty("_provider", (object) this.ProviderName);
      controlDescriptor.AddProperty("_showLibFilterWrp", (object) this.ShowLibFilterWrp);
      controlDescriptor.AddProperty("_itemsCount", (object) this.ItemsCount);
      controlDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad);
      if (this.ShowLibFilterWrp)
      {
        controlDescriptor.AddComponentProperty("librarySelector", this.LibrarySelector.ClientID);
        controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      }
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaSelectorFilterPanel.js", typeof (MediaSelectorFilterPanel).Assembly.FullName)
    };

    /// <summary>Hides a command toolbox item.</summary>
    /// <param name="commandBar">The command bar.</param>
    /// <param name="commandName">Name of the command.</param>
    private void HideCommandToolboxItem(CommandBar commandBar, string commandName)
    {
      ICommandButton commandButton = commandBar.Commands.OfType<ICommandButton>().Where<ICommandButton>((Func<ICommandButton, bool>) (b => b.CommandName == commandName)).FirstOrDefault<ICommandButton>();
      if (commandButton == null)
        return;
      ((ToolboxItemBase) commandButton).Visible = false;
    }
  }
}
