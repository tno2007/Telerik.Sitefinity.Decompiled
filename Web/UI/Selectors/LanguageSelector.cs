// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.LanguageSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Components;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Control for selecting one or more languages.</summary>
  public class LanguageSelector : SimpleScriptView
  {
    private string languagesServiceUrl;
    private string culturesServiceUrl;
    private bool showButtonArea = true;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.LanguageSelector.ascx");
    private const string defaultLanguagesServiceUrl = "~/Sitefinity/Services/Configuration/ConfigSectionItems.svc/languages/";
    private const string defaultCulturesServiceUrl = "~/Sitefinity/Services/Configuration/ConfigSectionItems.svc/cultures/";
    private const string languageBasicSettingsUrl = "~/Sitefinity/Administration/Settings/Basic/Languages/";
    public const string controlScript = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.LanguageSelector.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LanguageSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the languages service URL.</summary>
    /// <value>The service URL.</value>
    public virtual string LanguagesServiceUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.languagesServiceUrl))
          this.languagesServiceUrl = "~/Sitefinity/Services/Configuration/ConfigSectionItems.svc/languages/";
        return this.languagesServiceUrl;
      }
      set => this.languagesServiceUrl = value;
    }

    /// <summary>Gets or sets the cultures service URL.</summary>
    /// <value>The service URL.</value>
    public virtual string CulturesServiceUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.culturesServiceUrl))
          this.culturesServiceUrl = "~/Sitefinity/Services/Configuration/ConfigSectionItems.svc/cultures/";
        return this.culturesServiceUrl;
      }
      set => this.culturesServiceUrl = value;
    }

    /// <summary>Allow or disallow multiple selection in the grid</summary>
    public bool AllowMultipleSelection { get; set; }

    /// <summary>
    /// Gets or sets the option to automatically bind the selector when the control loads
    /// </summary>
    public bool BindOnLoad { get; set; }

    /// <summary>
    /// Gets or Sets the the name of the items to retrieve. Should be a type string.
    /// </summary>
    public virtual string ItemType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the done selecting and cancel buttons.
    /// </summary>
    public bool ShowButtonArea
    {
      get => this.showButtonArea;
      set => this.showButtonArea = value;
    }

    /// <summary>
    /// Gets or sets the data key names used by the selector data source
    /// </summary>
    public string DataKeyNames { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether all items / selected items filter should be
    /// displayed.
    /// </summary>
    public virtual bool ShowSelectedFilter { get; set; }

    public bool UseGlobalLocalization { get; set; }

    /// <summary>
    /// Gets or sets the type of search that should be performed
    /// </summary>
    public SearchTypes SearchType { get; set; }

    /// <summary>A flat selector for the retrieved items</summary>
    protected virtual FlatSelector ItemSelector => this.Container.GetControl<FlatSelector>("itemSelector", true);

    /// <summary>The title label</summary>
    protected virtual Label TitleLabel => this.Container.GetControl<Label>("lblTitle", true);

    /// <summary>The LinkButton for "Done"</summary>
    protected virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("lnkDone", true);

    /// <summary>The LinkButton for "Cancel"</summary>
    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>
    /// Gets the reference to the link that switches to specific cultures
    /// </summary>
    protected internal virtual Control ShowCultures => this.Container.GetControl<Control>("showCultures", true);

    /// <summary>
    /// Gets the reference to the link that switches to neutral cultures
    /// </summary>
    protected internal virtual Control ShowLanguages => this.Container.GetControl<Control>("showLanguages", true);

    /// <summary>The button area control</summary>
    protected virtual Control ButtonArea => this.Container.GetControl<Control>("buttonAreaPanel", false);

    /// <summary>The button area control</summary>
    protected virtual Label MultisiteMultilingualMessage => this.Container.GetControl<Label>("multisiteMultilingualMessage", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">
    /// An instance of <see cref="T:Telerik.Sitefinity.Web.UI.GenericContainer" /> class in which the template was instantiated.
    /// </param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ItemSelector.ServiceUrl = this.LanguagesServiceUrl;
      this.ItemSelector.AllowMultipleSelection = this.AllowMultipleSelection;
      this.ItemSelector.BindOnLoad = this.BindOnLoad;
      this.ItemSelector.DataKeyNames = this.DataKeyNames;
      if (this.ButtonArea != null)
        this.ButtonArea.Visible = this.ShowButtonArea;
      this.ItemSelector.DisableProvidersListing = true;
      this.ItemSelector.ShowSelectedFilter = this.ShowSelectedFilter;
      this.ItemSelector.SearchType = this.SearchType;
      if (!this.UseGlobalLocalization)
        return;
      this.MultisiteMultilingualMessage.Text = string.Format(Res.Get<Labels>().EnableSystemLanguagesMessage, (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Administration/Settings/Basic/Languages/"));
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (LanguageSelector).FullName, this.ClientID);
      if (this.ShowButtonArea)
      {
        controlDescriptor.AddElementProperty("lnkDone", this.DoneButton.ClientID);
        controlDescriptor.AddElementProperty("lnkCancel", this.CancelButton.ClientID);
      }
      controlDescriptor.AddComponentProperty("itemSelector", this.ItemSelector.ClientID);
      controlDescriptor.AddElementProperty("showCultures", this.ShowCultures.ClientID);
      controlDescriptor.AddElementProperty("showLanguages", this.ShowLanguages.ClientID);
      controlDescriptor.AddProperty("languagesServiceUrl", (object) RouteHelper.ResolveUrl(this.LanguagesServiceUrl, UrlResolveOptions.Rooted));
      controlDescriptor.AddProperty("culturesServiceUrl", (object) RouteHelper.ResolveUrl(this.CulturesServiceUrl, UrlResolveOptions.Rooted));
      controlDescriptor.AddProperty("useGlobal", (object) this.UseGlobalLocalization);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script
    /// resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of
    /// <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = this.GetType().Assembly.FullName;
      return (IEnumerable<ScriptReference>) new ScriptReference[1]
      {
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.LanguageSelector.js"
        }
      };
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.languagesServiceUrl != this.ItemSelector.ServiceUrl)
        this.ItemSelector.ServiceUrl = this.languagesServiceUrl;
      if (string.IsNullOrEmpty(this.ItemType))
        return;
      this.ItemSelector.ItemType = this.ItemType;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
