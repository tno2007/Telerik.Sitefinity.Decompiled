// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSettingsDesignerView
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
  /// <summary>
  /// Base abstract class that provides common implementation for media content Settings view.
  /// </summary>
  public abstract class MediaContentSettingsDesignerView : ContentViewDesignerView
  {
    private const string settingsViewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaContentSettingsDesignerView.js";
    private const string designerViewControlScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";

    /// <summary>
    /// Gets an array containing the name of all master views.
    /// </summary>
    public abstract string[] MasterViewNameMap { get; }

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<LibrariesResources>().Settings;

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");

    /// <summary>Gets the mode radio buttons.</summary>
    /// <value>The mode radio buttons.</value>
    protected virtual IDictionary<string, Control> ModeRadioButtons => (IDictionary<string, Control>) this.Container.GetControls<RadioButton>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (r => r.Key.StartsWith("rbMode"))).ToDictionary<KeyValuePair<string, Control>, string, Control>((Func<KeyValuePair<string, Control>, string>) (i => i.Key), (Func<KeyValuePair<string, Control>, Control>) (e => e.Value));

    /// <summary>
    /// Gets a dictionary containing the ClientID of all rbPaging controls.
    /// </summary>
    protected virtual IDictionary<string, string> PagingRadioButtonsClientIDs => (IDictionary<string, string>) this.Container.GetControls<RadioButton>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (c => c.Key.StartsWith("rbPaging"))).ToDictionary<KeyValuePair<string, Control>, string, string>((Func<KeyValuePair<string, Control>, string>) (i => i.Key.Substring(i.Key.Length - 1)), (Func<KeyValuePair<string, Control>, string>) (e => e.Value.ClientID));

    /// <summary>Gets the limit radio buttons.</summary>
    /// <value>The limit radio buttons.</value>
    protected virtual IDictionary<string, Control> LimitRadioButtons => (IDictionary<string, Control>) this.Container.GetControls<CheckBox>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (r => r.Key.StartsWith("rbLimit"))).ToDictionary<KeyValuePair<string, Control>, string, Control>((Func<KeyValuePair<string, Control>, string>) (i => i.Key), (Func<KeyValuePair<string, Control>, Control>) (e => e.Value));

    protected internal virtual IDictionary<string, string> NoLimitAndPagingRadioButtonsClientIDs => (IDictionary<string, string>) this.Container.GetControls<RadioButton>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (r => r.Key.StartsWith("rbNoLimitAndPaging"))).ToDictionary<KeyValuePair<string, Control>, string, string>((Func<KeyValuePair<string, Control>, string>) (i => i.Key.Substring(i.Key.Length - 1)), (Func<KeyValuePair<string, Control>, string>) (e => e.Value.ClientID));

    /// <summary>
    /// Gets a dictionary containing the ClientID of all tfPaging controls.
    ///  </summary>
    protected virtual IDictionary<string, string> PagingTextFieldsClientIDs => (IDictionary<string, string>) this.Container.GetControls<TextField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (c => c.Key.StartsWith("tfPaging"))).ToDictionary<KeyValuePair<string, Control>, string, string>((Func<KeyValuePair<string, Control>, string>) (i => i.Key.Substring(i.Key.Length - 1)), (Func<KeyValuePair<string, Control>, string>) (e => e.Value.ClientID));

    /// <summary>Gets the limit text fields.</summary>
    /// <value>The limit text fields.</value>
    protected virtual IDictionary<string, Control> LimitTextFields => (IDictionary<string, Control>) this.Container.GetControls<TextField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (r => r.Key.StartsWith("tfLimit"))).ToDictionary<KeyValuePair<string, Control>, string, Control>((Func<KeyValuePair<string, Control>, string>) (i => i.Key), (Func<KeyValuePair<string, Control>, Control>) (e => e.Value));

    /// <summary>
    /// Gets the RadMultiPage containing all panels for the different modes.
    /// </summary>
    protected virtual RadMultiPage MultiPage => this.Container.GetControl<RadMultiPage>("mpRightPane", true);

    /// <summary>Gets all selectThumbSize controls.</summary>
    protected virtual IDictionary<string, Control> SelectThumbSize => (IDictionary<string, Control>) this.Container.GetControls<ChoiceField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (c => c.Key.StartsWith("selectThumbSize"))).ToDictionary<KeyValuePair<string, Control>, string, Control>((Func<KeyValuePair<string, Control>, string>) (i => i.Key), (Func<KeyValuePair<string, Control>, Control>) (e => e.Value));

    /// <summary>Gets all selectBigItemSize controls.</summary>
    protected virtual IDictionary<string, Control> SelectBigItemSize => (IDictionary<string, Control>) this.Container.GetControls<ChoiceField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (c => c.Key.StartsWith("selectBigItemSize"))).ToDictionary<KeyValuePair<string, Control>, string, Control>((Func<KeyValuePair<string, Control>, string>) (i => i.Key), (Func<KeyValuePair<string, Control>, Control>) (e => e.Value));

    /// <summary>Gets the correct instance of RadWindowManager</summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>
    /// Represents the manager that controls the localization strings.
    /// </summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      List<string> stringList1 = new List<string>();
      foreach (KeyValuePair<string, Control> modeRadioButton in (IEnumerable<KeyValuePair<string, Control>>) this.ModeRadioButtons)
        stringList1.Add(modeRadioButton.Value.ClientID);
      controlDescriptor.AddProperty("_modeRadioButtonClientIDs", (object) stringList1);
      controlDescriptor.AddProperty("_pagingRadioButtonClientIDs", (object) this.PagingRadioButtonsClientIDs);
      controlDescriptor.AddProperty("_noLimitAndPagingRadioButtonsClientIDs", (object) this.NoLimitAndPagingRadioButtonsClientIDs);
      controlDescriptor.AddProperty("_pagingTextFieldClientIDs", (object) this.PagingTextFieldsClientIDs);
      List<string> stringList2 = new List<string>();
      foreach (KeyValuePair<string, Control> limitRadioButton in (IEnumerable<KeyValuePair<string, Control>>) this.LimitRadioButtons)
        stringList2.Add(limitRadioButton.Value.ClientID);
      controlDescriptor.AddProperty("_limitRadioButtonClientIDs", (object) stringList2);
      List<string> stringList3 = new List<string>();
      foreach (KeyValuePair<string, Control> limitTextField in (IEnumerable<KeyValuePair<string, Control>>) this.LimitTextFields)
        stringList3.Add(limitTextField.Value.ClientID);
      controlDescriptor.AddProperty("_limitTextFieldClientIDs", (object) stringList3);
      controlDescriptor.AddProperty("_masterViewNameMap", (object) this.MasterViewNameMap);
      controlDescriptor.AddComponentProperty("multiPage", this.MultiPage.ClientID);
      List<string> stringList4 = new List<string>();
      foreach (KeyValuePair<string, Control> keyValuePair in (IEnumerable<KeyValuePair<string, Control>>) this.SelectThumbSize)
        stringList4.Add(keyValuePair.Value.ClientID);
      controlDescriptor.AddProperty("_selectThumbSizeClientIDs", (object) stringList4);
      List<string> stringList5 = new List<string>();
      foreach (KeyValuePair<string, Control> keyValuePair in (IEnumerable<KeyValuePair<string, Control>>) this.SelectBigItemSize)
        stringList5.Add(keyValuePair.Value.ClientID);
      controlDescriptor.AddProperty("_selectBigItemSizeClientIDs", (object) stringList5);
      controlDescriptor.AddComponentProperty("radWindowManager", this.RadWindowManager.ClientID);
      controlDescriptor.AddProperty("_widgetEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute(string.Format("~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName)));
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      string virtualPath = string.Format("~/{0}", (object) "Sitefinity/Services/ThumbnailService.svc");
      controlDescriptor.AddProperty("thumbnailServiceUrl", (object) VirtualPathUtility.ToAbsolute(virtualPath));
      controlDescriptor.AddProperty("_modifyWidgetTemplatePermission", (object) this.ModifyWidgetTemplatePermission);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = typeof (MediaContentSettingsDesignerView).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaContentSettingsDesignerView.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
