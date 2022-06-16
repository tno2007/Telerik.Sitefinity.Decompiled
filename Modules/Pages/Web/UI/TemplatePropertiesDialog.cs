// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.TemplatePropertiesDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>
  /// Represents a dialog for creating templates and editing template properties.
  /// </summary>
  public class TemplatePropertiesDialog : PropertiesBaseDialog
  {
    private PageManager pageManager;
    private TaxonomyManager taxonomyManager;
    private Guid templateId;
    private DialogModes mode;
    private bool modeChanged;
    private PageTemplate currentTemplate;
    /// <summary>
    /// Gets the name of resource file representing the dialog for creating templates.
    /// </summary>
    public static readonly string CreateDialogTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.CreateTemplatePropertiesDialog.ascx");
    /// <summary>
    /// Gets the name of resource file representing the dialog for editing template properties.
    /// </summary>
    public static readonly string EditDialogTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.EditTemplatePropertiesDialog.ascx");
    /// <summary>
    /// The identifier of the default backend templates category.
    /// </summary>
    public static readonly Guid BackendTemplatesCategoryId = SiteInitializer.BackendTemplatesCategoryId;
    /// <summary>
    /// The identifier of the default custom templates category.
    /// </summary>
    public static readonly Guid CustomTemplatesCategoryId = new Guid("F669D9A7-009D-4d83-AABB-000000000002");
    /// <summary>The path to the default image of Custom templates.</summary>
    public const string CustomImagePath = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.Custom.gif";

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
      get
      {
        if (!string.IsNullOrWhiteSpace(base.LayoutTemplatePath))
          return base.LayoutTemplatePath;
        return this.Mode == DialogModes.Create || this.Mode == DialogModes.SelectTemplate ? TemplatePropertiesDialog.CreateDialogTemplateName : TemplatePropertiesDialog.EditDialogTemplateName;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the dialog mode.</summary>
    /// <value>The mode.</value>
    public override DialogModes Mode
    {
      get
      {
        if (!this.modeChanged)
        {
          string str = this.Page.Request.QueryString["dialogMode"];
          string g = this.Page.Request.QueryString["Id"];
          if (!string.IsNullOrEmpty(g))
            this.templateId = new Guid(g);
          if (!(str == "TemplateProperties"))
          {
            if (str == "ChangeParentTemplate" && this.templateId != Guid.Empty)
              this.mode = DialogModes.ChangeTemplate;
          }
          else
            this.mode = !(this.templateId != Guid.Empty) ? DialogModes.Create : DialogModes.EditProperties;
        }
        return this.mode;
      }
      set
      {
        this.mode = value;
        this.modeChanged = true;
      }
    }

    private PageTemplate CurrentTemplate
    {
      get
      {
        if (this.currentTemplate == null && this.templateId != Guid.Empty)
          this.currentTemplate = this.PageManager.GetTemplate(this.templateId);
        return this.currentTemplate;
      }
    }

    /// <summary>Gets the saveTemplateLink button.</summary>
    /// <value>The saveTemplateLink button.</value>
    protected virtual IButtonControl SaveTemplateLink => this.Container.GetControl<IButtonControl>("saveTemplateLink", true);

    /// <summary>Gets the templatePropertiesPanel container.</summary>
    /// <value>The templatePropertiesPanel container.</value>
    protected virtual Control TemplatePropertiesPanel => this.Container.GetControl<Control>("templatePropertiesPanel", true);

    /// <summary>Gets the selectParentTemplatePanel container.</summary>
    /// <value>The selectParentTemplatePanel container.</value>
    protected virtual Control SelectParentTemplatePanel => this.Container.GetControl<Control>("selectParentTemplatePanel", true);

    /// <summary>Gets the showInNavigation control.</summary>
    /// <value>The showInNavigation control.</value>
    protected virtual ICheckBoxControl ShowInNavigation => this.Container.GetControl<ICheckBoxControl>("showInNavigation", true);

    /// <summary>Gets the title control.</summary>
    /// <value>The title control.</value>
    protected virtual ITextControl Title => this.Container.GetControl<ITextControl>("title", true);

    /// <summary>Gets the message control.</summary>
    /// <value>The message control.</value>
    protected virtual Message MsgControl => this.Container.GetControl<Message>("message", false);

    /// <summary>Gets the templateView control.</summary>
    /// <value>The templateView control.</value>
    protected virtual SelectParentTemplateView TemplateView => this.Container.GetControl<SelectParentTemplateView>("templateView", false);

    /// <summary>
    /// Restores control-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveControlState" /> method.
    /// </summary>
    /// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
    protected override void LoadControlState(object savedState)
    {
      if (savedState == null)
        return;
      object[] objArray = (object[]) savedState;
      this.templateId = (Guid) objArray[0];
      this.mode = (DialogModes) objArray[1];
      this.modeChanged = (bool) objArray[2];
    }

    /// <summary>
    /// Saves any server control state changes that have occurred since the time the page was posted back to the server.
    /// </summary>
    /// <returns>
    /// Returns the server control's current state. If there is no state associated with the control, this method returns null.
    /// </returns>
    protected override object SaveControlState() => (object) new object[3]
    {
      (object) this.templateId,
      (object) this.mode,
      (object) this.modeChanged
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      switch (this.Mode)
      {
        case DialogModes.Create:
        case DialogModes.EditProperties:
          this.SetTemplatePropertiesMode();
          break;
        case DialogModes.SelectTemplate:
        case DialogModes.ChangeTemplate:
          this.SetParentTemplateMode();
          break;
      }
    }

    private void SetTemplateProperties()
    {
      if (this.CurrentTemplate == null)
        return;
      this.Title.Text = (string) this.CurrentTemplate.Title;
      this.ShowInNavigation.Checked = this.CurrentTemplate.ShowInNavigation;
    }

    private void SetParentTemplateMode()
    {
      this.TemplatePropertiesPanel.Visible = false;
      this.SelectParentTemplatePanel.Visible = true;
      this.TemplateView.CurrentTemplate = this.CurrentTemplate;
      this.TemplateView.PageManager = this.PageManager;
      this.TemplateView.TaxonomyManager = this.TaxonomyManager;
      this.TemplateView.Mode = this.Mode;
      this.TemplateView.RootTaxon = this.RootTaxon;
      this.TemplateView.ReturnToPropertiesCommand += new CommandEventHandler(this.TemplateView_ReturnToPropertiesCommand);
    }

    private void SetTemplatePropertiesMode()
    {
      this.TemplatePropertiesPanel.Visible = true;
      this.SelectParentTemplatePanel.Visible = false;
      this.SaveTemplateLink.Command += new CommandEventHandler(this.SaveTemplateLink_Command);
      this.SaveTemplateLink.CommandName = this.Mode != DialogModes.Create ? "SaveChanges" : "CreateTemplate";
      this.SetTemplateProperties();
    }

    private void SaveTemplateLink_Command(object sender, CommandEventArgs e)
    {
      Guid category = Guid.Empty;
      if (this.templateId == Guid.Empty)
        category = this.RootTaxon != RootTaxonType.Backend ? TemplatePropertiesDialog.CustomTemplatesCategoryId : TemplatePropertiesDialog.BackendTemplatesCategoryId;
      string str = TemplateBrowserContentProvider.SaveTemplate(this.templateId, category, this.Title.Text, this.ShowInNavigation.Checked, this.TaxonomyManager, this.PageManager);
      if (Utility.IsGuid(str))
      {
        this.templateId = new Guid(str);
        string commandName = e.CommandName;
        if (!(commandName == "CreateTemplate"))
        {
          if (!(commandName == "SaveChanges"))
            return;
          ScriptManager.RegisterStartupScript((Control) sender, this.GetType(), "saveChanges", "closeDialog('reload***" + string.Format(Res.Get<PageResources>().TemplateSavedMsg, (object) this.Title.Text) + "');", true);
        }
        else
        {
          this.Mode = DialogModes.SelectTemplate;
          this.SetParentTemplateMode();
        }
      }
      else
      {
        if (this.MsgControl == null)
          return;
        this.MsgControl.ShowNegativeMessage(str);
      }
    }

    private void TemplateView_ReturnToPropertiesCommand(object sender, CommandEventArgs e)
    {
      this.Mode = DialogModes.Create;
      this.SetTemplatePropertiesMode();
    }
  }
}
