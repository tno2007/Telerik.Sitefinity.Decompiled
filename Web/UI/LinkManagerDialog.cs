// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.LinkManagerDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Link Manager dialog</summary>
  public class LinkManagerDialog : AjaxDialogBase
  {
    private const string dialogScript = "Telerik.Sitefinity.Web.Scripts.LinkManagerDialog.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.LinkManagerDialog.ascx");
    private string uiCulture;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (LinkManagerDialog).FullName;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LinkManagerDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a dictionary containing all textToDisplayField controls.
    ///  </summary>
    protected virtual IList<string> TextToDisplayFieldsClientIDs => (IList<string>) this.Container.GetControls<TextField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (c => c.Key.StartsWith("textToDisplayField"))).Select<KeyValuePair<string, Control>, string>((Func<KeyValuePair<string, Control>, string>) (c => c.Value.ClientID)).ToList<string>();

    /// <summary>
    /// Gets a dictionary containing all moreOptionsSection controls.
    ///  </summary>
    protected virtual IList<string> MoreOptionsSectionsClientIDs => (IList<string>) this.Container.GetControls<MoreOptionsExpandableSection>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (c => c.Key.StartsWith("moreOptionsSection"))).Select<KeyValuePair<string, Control>, string>((Func<KeyValuePair<string, Control>, string>) (c => c.Value.ClientID)).ToList<string>();

    /// <summary>Gets or sets the UI culture.</summary>
    /// <value>The UI culture.</value>
    public string UICulture
    {
      get
      {
        if (this.uiCulture == null)
        {
          this.uiCulture = SystemManager.CurrentHttpContext.Request.QueryString["uiCulture"];
          if (this.uiCulture == null)
          {
            IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
            if (appSettings.Multilingual && string.IsNullOrEmpty(this.uiCulture))
              this.uiCulture = appSettings.DefaultFrontendLanguage.Name;
          }
        }
        return this.uiCulture;
      }
      set => this.uiCulture = value;
    }

    /// <summary>Gets the reference to the page selector.</summary>
    protected virtual GenericPageSelector PageSelector => this.Container.GetControl<GenericPageSelector>("selector", true);

    /// <summary>
    /// Gets the reference to the dialogModesSwitcher control.
    /// </summary>
    protected virtual ChoiceField DialogModesSwitcher => this.Container.GetControl<ChoiceField>("dialogModesSwitcher", true);

    /// <summary>Gets the reference to the webAddressField control.</summary>
    protected virtual TextField WebAddressField => this.Container.GetControl<TextField>("webAddressField", true);

    /// <summary>Gets the reference to the emailField control.</summary>
    protected virtual TextField EmailField => this.Container.GetControl<TextField>("emailField", true);

    /// <summary>Gets the reference to the insertLink control.</summary>
    protected virtual HyperLink InsertLink => this.Container.GetControl<HyperLink>("insertLink", true);

    /// <summary>Gets the reference to the webAddressSection control.</summary>
    protected virtual HtmlGenericControl WebAddressSection => this.Container.GetControl<HtmlGenericControl>("webAddressSection", true);

    /// <summary>
    /// Gets the reference to the pageSelectorSection control.
    /// </summary>
    protected virtual HtmlGenericControl PageSelectorSection => this.Container.GetControl<HtmlGenericControl>("pageSelectorSection", true);

    /// <summary>Gets the reference to the emailSection control.</summary>
    protected virtual HtmlGenericControl EmailSection => this.Container.GetControl<HtmlGenericControl>("emailSection", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      this.Controls.Add((Control) new FormManager());
      base.OnInit(e);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.PageSelector.UICulture = this.UICulture;
      this.PageSelector.IncludeSiteSelector = true;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("pageSelector", this.PageSelector.ClientID);
      controlDescriptor.AddComponentProperty("dialogModesSwitcher", this.DialogModesSwitcher.ClientID);
      controlDescriptor.AddComponentProperty("webAddressField", this.WebAddressField.ClientID);
      controlDescriptor.AddComponentProperty("emailField", this.EmailField.ClientID);
      controlDescriptor.AddComponentProperty("dialogModesSwitcher", this.DialogModesSwitcher.ClientID);
      controlDescriptor.AddElementProperty("insertLink", this.InsertLink.ClientID);
      controlDescriptor.AddElementProperty("webAddressSection", this.WebAddressSection.ClientID);
      controlDescriptor.AddElementProperty("pageSelectorSection", this.PageSelectorSection.ClientID);
      controlDescriptor.AddElementProperty("emailSection", this.EmailSection.ClientID);
      controlDescriptor.AddProperty("_textToDisplayFieldsClientIDs", (object) this.TextToDisplayFieldsClientIDs);
      controlDescriptor.AddProperty("_moreOptionsSectionsClientIDs", (object) this.MoreOptionsSectionsClientIDs);
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        controlDescriptor.AddProperty("uiCulture", (object) this.UICulture);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.LinkManagerDialog.js", typeof (LinkManagerDialog).Assembly.FullName)
    };
  }
}
