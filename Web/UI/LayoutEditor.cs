// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.LayoutEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.ResponsiveDesign;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Control for editing the layout controls</summary>
  public class LayoutEditor : SimpleScriptView
  {
    private string zoneEditorId;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.LayoutEditor.ascx");
    private const string webServiceUrl = "~/Sitefinity/Services/Pages/ZoneEditorService.svc/Layout/Style/";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.LayoutEditor" /> class.
    /// </summary>
    /// <param name="zoneEditorId">The zone editor pageId.</param>
    public LayoutEditor(string zoneEditorId)
    {
      this.zoneEditorId = zoneEditorId;
      this.LayoutTemplatePath = LayoutEditor.layoutTemplatePath;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the reference to the RadTabStrip in charge of switching
    /// editors (e.g. sizes, spaces etc.).
    /// </summary>
    protected virtual RadTabStrip RadTabStrip1 => this.Container.GetControl<RadTabStrip>(nameof (RadTabStrip1), true);

    protected virtual ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets the reference to the control which represents the container for visual
    /// editing (e.g. resizing via dragging) of the layout controls
    /// </summary>
    protected virtual HtmlGenericControl VisualEditorContainer => this.Container.GetControl<HtmlGenericControl>("visualEditorContainer", true);

    /// <summary>
    /// Gets the reference to the control panel in which editors reside.
    /// </summary>
    protected virtual Control ControlPanel => this.Container.GetControl<Control>("controlPanel", true);

    /// <summary>
    /// Gets the reference to the button which saves changes made to the layout
    /// control.
    /// </summary>
    protected virtual LinkButton SaveLayoutButton => this.Container.GetControl<LinkButton>("saveLayoutButton", true);

    /// <summary>
    /// Gets the reference to the button which cancels the changes made to the
    /// layout control.
    /// </summary>
    protected virtual LinkButton CancelLayoutButton => this.Container.GetControl<LinkButton>("cancelLayoutButton", true);

    /// <summary>
    /// Gets the reference to the radio button which determines that sizes ought to be
    /// represented in percentages.
    /// </summary>
    protected virtual RadioButton SizesInPercentages => this.Container.GetControl<RadioButton>("sizesInPercentages", true);

    /// <summary>
    /// Gets the reference to the radio button which determines that sizes ought to be
    /// represented in pixels.
    /// </summary>
    protected virtual RadioButton SizesInPixels => this.Container.GetControl<RadioButton>("sizesInPixels", true);

    /// <summary>
    /// Gets the reference to the control which will hold the textboxes with column
    /// widths information.
    /// </summary>
    protected virtual Control ColumnWidthsContainer => this.Container.GetControl<Control>("columnWidthsContainer", true);

    /// <summary>
    /// Gets the reference to the link button which toggles the setting of the
    /// auto sized column.
    /// </summary>
    protected virtual LinkButton AutoSizedColumnButton => this.Container.GetControl<LinkButton>("autoSizedColumnButton", true);

    /// <summary>
    /// Gets the reference to the radio button which determines that spaces ought to be
    /// represented in percentages.
    /// </summary>
    protected virtual RadioButton SpacesInPercentages => this.Container.GetControl<RadioButton>("spacesInPercentages", true);

    /// <summary>
    /// Gets the reference to the radio button which determines that spaces ought to be
    /// represented in pixels.
    /// </summary>
    protected virtual RadioButton SpacesInPixels => this.Container.GetControl<RadioButton>("spacesInPixels", true);

    /// <summary>
    /// Gets the reference to the control which hosts the simple mode of editing spaces
    /// </summary>
    protected virtual HtmlGenericControl SimpleSpacesContainer => this.Container.GetControl<HtmlGenericControl>("simpleSpacesContainer", true);

    /// <summary>
    /// Gets the reference to the control which hosts the advanced mode of editing spaces
    /// </summary>
    protected virtual HtmlGenericControl AdvancedSpacesContainer => this.Container.GetControl<HtmlGenericControl>("advancedSpacesContainer", true);

    /// <summary>
    /// Gets the reference to the link button which switches the spaces editor into the advanced mode
    /// </summary>
    protected virtual LinkButton SpacesSideBySideButton => this.Container.GetControl<LinkButton>("spacesSideBySideButton", true);

    /// <summary>
    /// Gets the reference to the link button which switches the spaces editor into the simple mode
    /// </summary>
    protected virtual LinkButton EqualSpacesButton => this.Container.GetControl<LinkButton>("equalSpacesButton", true);

    /// <summary>
    /// Gets the reference to the control which will hold the list of textboxes for defining all
    /// margins
    /// </summary>
    protected virtual Control AdvancedMarginsContainer => this.Container.GetControl<Control>("advancedMarginsContainer", true);

    /// <summary>
    /// Gets the reference to the text box which holds the value determining the spacing
    /// between columns
    /// </summary>
    protected virtual TextBox HorizontalSpaceColumns => this.Container.GetControl<TextBox>("horizontalSpaceColumns", true);

    /// <summary>
    /// Gets the reference to the text box which holds the value determining the spacing
    /// above and below columns
    /// </summary>
    protected virtual TextBox VerticalSpaceColumns => this.Container.GetControl<TextBox>("verticalSpaceColumns", true);

    /// <summary>
    /// Gets the reference to the control which will hold the textboxes with column
    /// class names
    /// </summary>
    protected virtual Control ClassesContainer => this.Container.GetControl<Control>("classesContainer", true);

    /// <summary>
    /// Gets the reference to the control which will hold the textboxes with column
    /// class names
    /// </summary>
    protected virtual TextBox WrapperCssClassTextbox => this.Container.GetControl<TextBox>("wrapperCssClass", true);

    /// <summary>
    /// Gets the control that will contain the textboxes setting the placeholders(columns) labels.
    /// </summary>
    protected virtual Control PlaceholdersLabelsContainer => this.Container.GetControl<Control>("placeholdersLabelsContainer", true);

    /// <summary>
    /// Gets the reference to the button which opens the dialog for controlling column
    /// visibility.
    /// </summary>
    protected virtual LinkButton ChangeColumnVisibility => this.Container.GetControl<LinkButton>("changeColumnVisibility", true);

    /// <summary>
    /// Gets the reference to the repeater for choosing the group of rules in which
    /// column is visible.
    /// </summary>
    protected virtual Repeater GroupOfRulesRepeater => this.Container.GetControl<Repeater>("groupOfRulesRepeater", true);

    /// <summary>
    /// Gets the reference to the control which hosts the responsive design dialog
    /// </summary>
    protected virtual HtmlGenericControl ColumnVisibilityDialogArea => this.Container.GetControl<HtmlGenericControl>("columnVisibilityDialogArea", true);

    /// <summary>
    /// Gets the reference to the control which hosts the responsive design section
    /// </summary>
    protected virtual HtmlGenericControl ColumsVizibilityArea => this.Container.GetControl<HtmlGenericControl>("columsVizibilityArea", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The dialog container.</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      if (LicenseState.CheckIsModuleLicensedInCurrentDomain("01F89003-7A52-4C08-BA60-45C8B8824B38") && SystemManager.IsModuleAccessible("ResponsiveDesign"))
      {
        IQueryable<MediaQuery> mediaQueries = ResponsiveDesignManager.GetManager().GetMediaQueries();
        if (mediaQueries.Any<MediaQuery>())
        {
          this.GroupOfRulesRepeater.DataSource = (object) mediaQueries;
        }
        else
        {
          this.ColumnVisibilityDialogArea.Visible = false;
          this.ColumsVizibilityArea.Visible = false;
        }
      }
      else
      {
        this.ColumnVisibilityDialogArea.Visible = false;
        this.ColumsVizibilityArea.Visible = false;
      }
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.GroupOfRulesRepeater.DataBind();
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      this.EnsureChildControls();
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor("Telerik.Sitefinity.Web.UI.LayoutEditor", this.ClientID);
      behaviorDescriptor.AddProperty("_zoneEditorId", (object) this.zoneEditorId);
      behaviorDescriptor.AddProperty("_tabstripId", (object) this.RadTabStrip1.ClientID);
      behaviorDescriptor.AddProperty("_visualEditorContainerId", (object) this.VisualEditorContainer.ClientID);
      behaviorDescriptor.AddProperty("_controlPanelId", (object) this.ControlPanel.ClientID);
      behaviorDescriptor.AddProperty("_saveButtonId", (object) this.SaveLayoutButton.ClientID);
      behaviorDescriptor.AddProperty("_cancelButtonId", (object) this.CancelLayoutButton.ClientID);
      behaviorDescriptor.AddProperty("_webServiceUrl", (object) LayoutEditor.GetAbsoluteWebServiceUrl());
      behaviorDescriptor.AddProperty("_aValueBetweenLabel", (object) Res.Get<ControlResources>().AValueBetween);
      behaviorDescriptor.AddProperty("_sizesInPercentagesRadioId", (object) this.SizesInPercentages.ClientID);
      behaviorDescriptor.AddProperty("_sizesInPixelsRadioId", (object) this.SizesInPixels.ClientID);
      behaviorDescriptor.AddProperty("_columnWidthsContainerId", (object) this.ColumnWidthsContainer.ClientID);
      behaviorDescriptor.AddProperty("_columnLabel", (object) Res.Get<ControlResources>().Column);
      behaviorDescriptor.AddProperty("_autoSizedLabel", (object) Res.Get<ControlResources>().AutoSized);
      behaviorDescriptor.AddProperty("_makeThisAutoSizedLabel", (object) Res.Get<ControlResources>().MakeThisAutoSized);
      behaviorDescriptor.AddProperty("_autoSizedColumnButtonId", (object) this.AutoSizedColumnButton.ClientID);
      behaviorDescriptor.AddProperty("_changeAutoSizedColumnLabel", (object) Res.Get<ControlResources>().ChangeAutoSizedColumn);
      behaviorDescriptor.AddProperty("_cancelChangeAutoSizedColumnLabel", (object) Res.Get<ControlResources>().CancelChangeAutoSizedColumnLabel);
      behaviorDescriptor.AddProperty("_spacesInPercentagesRadioId", (object) this.SpacesInPercentages.ClientID);
      behaviorDescriptor.AddProperty("_spacesInPixelsRadioId", (object) this.SpacesInPixels.ClientID);
      behaviorDescriptor.AddProperty("_simpleSpacesContainerId", (object) this.SimpleSpacesContainer.ClientID);
      behaviorDescriptor.AddProperty("_advancedSpacesContainerId", (object) this.AdvancedSpacesContainer.ClientID);
      behaviorDescriptor.AddProperty("_spacesSideBySideButtonId", (object) this.SpacesSideBySideButton.ClientID);
      behaviorDescriptor.AddProperty("_equalSpacesButtonId", (object) this.EqualSpacesButton.ClientID);
      behaviorDescriptor.AddProperty("_advancedMarginsContainerId", (object) this.AdvancedMarginsContainer.ClientID);
      behaviorDescriptor.AddProperty("_horizontalSpaceColumnsId", (object) this.HorizontalSpaceColumns.ClientID);
      behaviorDescriptor.AddProperty("_verticalSpaceColumsId", (object) this.VerticalSpaceColumns.ClientID);
      behaviorDescriptor.AddProperty("_topLabel", (object) Res.Get<ControlResources>().Top);
      behaviorDescriptor.AddProperty("_rightLabel", (object) Res.Get<ControlResources>().Right);
      behaviorDescriptor.AddProperty("_bottomLabel", (object) Res.Get<ControlResources>().Bottom);
      behaviorDescriptor.AddProperty("_leftLabel", (object) Res.Get<ControlResources>().Left);
      behaviorDescriptor.AddComponentProperty("labelManager", this.LabelManager.ClientID);
      behaviorDescriptor.AddProperty("_classesContainerId", (object) this.ClassesContainer.ClientID);
      behaviorDescriptor.AddProperty("_wrapperCssClassTextboxId", (object) this.WrapperCssClassTextbox.ClientID);
      behaviorDescriptor.AddProperty("_placeholdersLabelsContainerId", (object) this.PlaceholdersLabelsContainer.ClientID);
      behaviorDescriptor.AddProperty("_changeColumnVisibilityButtonId", (object) this.ChangeColumnVisibility.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <summary>Gets the script references.</summary>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string str = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().ToString();
      ScriptReference scriptReference1 = new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js"
      };
      ScriptReference scriptReference2 = new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js"
      };
      ScriptReference scriptReference3 = new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.timezones.min.js"
      };
      ScriptReference scriptReference4 = new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery.imageareaselect-0.8.js"
      };
      ScriptReference scriptReference5 = new ScriptReference()
      {
        Assembly = this.GetType().Assembly.GetName().ToString(),
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      };
      ScriptReference scriptReference6 = new ScriptReference()
      {
        Assembly = this.GetType().Assembly.GetName().ToString(),
        Name = "Telerik.Sitefinity.Web.Scripts.LayoutEditor.js"
      };
      ScriptReference scriptReference7 = new ScriptReference()
      {
        Assembly = this.GetType().Assembly.GetName().ToString(),
        Name = "Telerik.Sitefinity.Web.Scripts.LayoutEditors.EditorBase.js"
      };
      ScriptReference scriptReference8 = new ScriptReference()
      {
        Assembly = this.GetType().Assembly.GetName().ToString(),
        Name = "Telerik.Sitefinity.Web.Scripts.LayoutEditors.AppearanceEditor.js"
      };
      ScriptReference scriptReference9 = new ScriptReference()
      {
        Assembly = this.GetType().Assembly.GetName().ToString(),
        Name = "Telerik.Sitefinity.Web.Scripts.LayoutEditors.MarginsEditor.js"
      };
      ScriptReference scriptReference10 = new ScriptReference()
      {
        Assembly = this.GetType().Assembly.GetName().ToString(),
        Name = "Telerik.Sitefinity.Web.Scripts.LayoutEditors.WidthEditor.js"
      };
      ScriptReference scriptReference11 = new ScriptReference()
      {
        Assembly = this.GetType().Assembly.GetName().ToString(),
        Name = "Telerik.Sitefinity.Web.Scripts.LayoutEditors.ColumnVisibilityEditor.js"
      };
      ScriptReference scriptReference12 = new ScriptReference()
      {
        Assembly = this.GetType().Assembly.GetName().ToString(),
        Name = "Telerik.Sitefinity.Web.Scripts.LayoutUpdater.js"
      };
      scriptReferences.Add(scriptReference1);
      scriptReferences.Add(scriptReference2);
      scriptReferences.Add(scriptReference3);
      scriptReferences.Add(scriptReference4);
      scriptReferences.Add(scriptReference5);
      scriptReferences.Add(scriptReference6);
      scriptReferences.Add(scriptReference7);
      scriptReferences.Add(scriptReference8);
      scriptReferences.Add(scriptReference9);
      scriptReferences.Add(scriptReference10);
      scriptReferences.Add(scriptReference11);
      scriptReferences.Add(scriptReference12);
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    private static string GetAbsoluteWebServiceUrl()
    {
      string virtualPath = "~/Sitefinity/Services/Pages/ZoneEditorService.svc/Layout/Style/";
      if (VirtualPathUtility.IsAppRelative(virtualPath))
        virtualPath = VirtualPathUtility.ToAbsolute(virtualPath);
      return virtualPath;
    }
  }
}
