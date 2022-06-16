// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSettingsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  public class DownloadListSettingsDesignerView : ContentViewDesignerView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Documents.DownloadListSettingsDesignerView.ascx");
    internal const string settingsViewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.DownloadListSettingsDesignerView.js";
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DownloadListSettingsDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => nameof (DownloadListSettingsDesignerView);

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<LibrariesResources>().Settings;

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");

    /// <summary>Gets the mode radio buttons.</summary>
    /// <value>The mode radio buttons.</value>
    protected internal virtual IDictionary<string, Control> ModeRadioButtons => (IDictionary<string, Control>) this.Container.GetControls<RadioButton>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (r => r.Key.StartsWith("rbMode"))).ToDictionary<KeyValuePair<string, Control>, string, Control>((Func<KeyValuePair<string, Control>, string>) (i => i.Key), (Func<KeyValuePair<string, Control>, Control>) (e => e.Value));

    /// <summary>Gets the paging radio buttons.</summary>
    /// <value>The paging radio buttons.</value>
    protected internal virtual IDictionary<string, Control> PagingRadioButtons => (IDictionary<string, Control>) this.Container.GetControls<RadioButton>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (r => r.Key.StartsWith("rbPaging"))).ToDictionary<KeyValuePair<string, Control>, string, Control>((Func<KeyValuePair<string, Control>, string>) (i => i.Key), (Func<KeyValuePair<string, Control>, Control>) (e => e.Value));

    protected internal virtual IDictionary<string, Control> NoLimitAndPagingRadioButtons => (IDictionary<string, Control>) this.Container.GetControls<RadioButton>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (r => r.Key.StartsWith("rbNoLimitAndPaging"))).ToDictionary<KeyValuePair<string, Control>, string, Control>((Func<KeyValuePair<string, Control>, string>) (i => i.Key), (Func<KeyValuePair<string, Control>, Control>) (e => e.Value));

    /// <summary>Gets the limit radio buttons.</summary>
    /// <value>The limit radio buttons.</value>
    protected internal virtual IDictionary<string, Control> LimitRadioButtons => (IDictionary<string, Control>) this.Container.GetControls<RadioButton>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (r => r.Key.StartsWith("rbLimit"))).ToDictionary<KeyValuePair<string, Control>, string, Control>((Func<KeyValuePair<string, Control>, string>) (i => i.Key), (Func<KeyValuePair<string, Control>, Control>) (e => e.Value));

    /// <summary>Gets the paging text fields.</summary>
    /// <value>The paging text fields.</value>
    protected internal virtual IDictionary<string, Control> PagingTextFields => (IDictionary<string, Control>) this.Container.GetControls<TextField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (r => r.Key.StartsWith("tfPaging"))).ToDictionary<KeyValuePair<string, Control>, string, Control>((Func<KeyValuePair<string, Control>, string>) (i => i.Key), (Func<KeyValuePair<string, Control>, Control>) (e => e.Value));

    /// <summary>Gets the limit text fields.</summary>
    /// <value>The limit text fields.</value>
    protected internal virtual IDictionary<string, Control> LimitTextFields => (IDictionary<string, Control>) this.Container.GetControls<TextField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (r => r.Key.StartsWith("tfLimit"))).ToDictionary<KeyValuePair<string, Control>, string, Control>((Func<KeyValuePair<string, Control>, string>) (i => i.Key), (Func<KeyValuePair<string, Control>, Control>) (e => e.Value));

    /// <summary>
    /// Gets the RadMultiPage containing all panels for the different modes.
    /// </summary>
    protected internal virtual RadMultiPage MultiPage => this.Container.GetControl<RadMultiPage>("mpRightPane", true);

    /// <summary>Radio button representing the big icons thumbnails</summary>
    protected internal virtual RadioButton BigIconsRadioButton => this.Container.GetControl<RadioButton>("rbBigIcons", true);

    /// <summary>Radio button representing the small icons thumbnails</summary>
    protected internal virtual RadioButton SmallIconsRadioButton => this.Container.GetControl<RadioButton>("rbSmallIcons", true);

    /// <summary>
    /// Radio button that disables the showing of the thumbnails
    /// </summary>
    protected internal virtual RadioButton NoIconsRadioButton => this.Container.GetControl<RadioButton>("rbNoIcons", true);

    /// <summary>Checkbox setting the visibility of the icons</summary>
    protected internal virtual IEnumerable<Control> DisplayIconsCheckBoxes => this.Container.GetControls<CheckBox>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (cf => cf.Key.StartsWith("cbDisplayIcons"))).Select<KeyValuePair<string, Control>, Control>((Func<KeyValuePair<string, Control>, Control>) (c => c.Value));

    /// <summary>
    /// Choicefield that sets the position of the download links
    /// </summary>
    protected internal virtual IEnumerable<Control> DownloadLinkPositionChoiceFields => this.Container.GetControls<ChoiceField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (cf => cf.Key.StartsWith("downloadLinkPositionChoiceField"))).Select<KeyValuePair<string, Control>, Control>((Func<KeyValuePair<string, Control>, Control>) (c => c.Value));

    /// <summary>Gets the correct instance of RadWindowManager</summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>
    /// Gets the link that opens up the List template for editing
    /// </summary>
    protected virtual HyperLink EditListTemplateLink => this.Container.GetControl<HyperLink>("editListTemplateLink", true);

    /// <summary>
    /// Get the link that opens up the Table template for editing
    /// </summary>
    protected virtual HyperLink EditTableTemplateLink => this.Container.GetControl<HyperLink>("editTableTemplateLink", true);

    /// <summary>
    /// Get the link that opens up the ListDetail template for editing
    /// </summary>
    protected virtual HyperLink EditListDetailTemplateLink => this.Container.GetControl<HyperLink>("editListDetailTemplateLink", true);

    /// <summary>
    /// Get the link that opens up the Detail template for editing
    /// </summary>
    protected virtual HyperLink EditDetailTemplateLink => this.Container.GetControl<HyperLink>("editDetailTemplateLink", true);

    protected virtual HyperLink EditTableDetailTemplateLink => this.Container.GetControl<HyperLink>("editTableDetailTemplateLink", true);

    protected virtual HyperLink EditDetailTemplateLink2 => this.Container.GetControl<HyperLink>("editDetailTemplateLink2", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (DownloadListSettingsDesignerView).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("multiPage", this.MultiPage.ClientID);
      controlDescriptor.AddProperty("_modeRadioButtonClientIDs", (object) this.ModeRadioButtons.Select<KeyValuePair<string, Control>, string>((Func<KeyValuePair<string, Control>, string>) (s => s.Value.ClientID)).ToList<string>());
      controlDescriptor.AddProperty("_pagingRadioButtonClientIDs", (object) this.PagingRadioButtons.Select<KeyValuePair<string, Control>, string>((Func<KeyValuePair<string, Control>, string>) (s => s.Value.ClientID)).ToList<string>());
      controlDescriptor.AddProperty("_noLimitAndPagingRadioButtonsClientIDs", (object) this.NoLimitAndPagingRadioButtons.Select<KeyValuePair<string, Control>, string>((Func<KeyValuePair<string, Control>, string>) (s => s.Value.ClientID)).ToList<string>());
      controlDescriptor.AddProperty("_displayIconsCheckBoxIDs", (object) this.DisplayIconsCheckBoxes.Select<Control, string>((Func<Control, string>) (s => s.ClientID)).ToList<string>());
      controlDescriptor.AddProperty("_limitRadioButtonClientIDs", (object) this.LimitRadioButtons.Select<KeyValuePair<string, Control>, string>((Func<KeyValuePair<string, Control>, string>) (s => s.Value.ClientID)).ToList<string>());
      controlDescriptor.AddProperty("_pagingTextFieldClientIDs", (object) this.PagingTextFields.Select<KeyValuePair<string, Control>, string>((Func<KeyValuePair<string, Control>, string>) (s => s.Value.ClientID)).ToList<string>());
      controlDescriptor.AddProperty("_limitTextFieldClientIDs", (object) this.LimitTextFields.Select<KeyValuePair<string, Control>, string>((Func<KeyValuePair<string, Control>, string>) (s => s.Value.ClientID)).ToList<string>());
      controlDescriptor.AddProperty("_downloadLinkPositionChoiceFieldIDs", (object) this.DownloadLinkPositionChoiceFields.Select<Control, string>((Func<Control, string>) (s => s.ClientID)).ToList<string>());
      controlDescriptor.AddProperty("_masterViewNameMap", (object) new string[4]
      {
        "MasterListView",
        "MasterTableView",
        "MasterListDetailView",
        "MasterTableDetailView"
      });
      controlDescriptor.AddProperty("_iconSizesRadioButtonsClientIDs", (object) new string[3]
      {
        this.BigIconsRadioButton.ClientID,
        this.SmallIconsRadioButton.ClientID,
        this.NoIconsRadioButton.ClientID
      });
      controlDescriptor.AddElementProperty("editListTemplateLink", this.EditListTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editTableTemplateLink", this.EditTableTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editListDetailTemplateLink", this.EditListDetailTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editTableDetailTemplateLink", this.EditTableDetailTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editDetailTemplateLink", this.EditDetailTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editDetailTemplateLink2", this.EditDetailTemplateLink2.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          this.EditListTemplateLink.ClientID,
          "57D8E0F2-8B3D-4CBF-96A4-000000000001"
        },
        {
          this.EditTableTemplateLink.ClientID,
          "57D8E0F2-8B3D-4CBF-96A4-000000000002"
        },
        {
          this.EditListDetailTemplateLink.ClientID,
          "57D8E0F2-8B3D-4CBF-96A4-000000000004"
        },
        {
          this.EditTableDetailTemplateLink.ClientID,
          "57D8E0F2-8B3D-4CBF-96A4-000000000005"
        },
        {
          this.EditDetailTemplateLink.ClientID,
          "57D8E0F2-8B3D-4CBF-96A4-000000000003"
        },
        {
          this.EditDetailTemplateLink2.ClientID,
          "57D8E0F2-8B3D-4CBF-96A4-000000000003"
        }
      };
      controlDescriptor.AddProperty("_templateLinkIdMap", (object) dictionary);
      controlDescriptor.AddComponentProperty("radWindowManager", this.RadWindowManager.ClientID);
      controlDescriptor.AddProperty("_widgetEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute(string.Format("~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName)));
      controlDescriptor.AddProperty("_modifyWidgetTemplatePermission", (object) this.ModifyWidgetTemplatePermission);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", typeof (DownloadListSettingsDesignerView).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.DownloadListSettingsDesignerView.js", typeof (DownloadListSettingsDesignerView).Assembly.FullName)
    };
  }
}
