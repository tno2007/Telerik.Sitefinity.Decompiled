// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownListWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Localization.Contracts;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>
  /// A widget encompassing the <see cref="T:Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownList" /> control.
  /// </summary>
  public class LanguagesDropDownListWidget : Widget
  {
    private const string notCorrectInterface = "The Definition of {0} control does not implement {1} interface.";
    private const string scriptName = "Telerik.Sitefinity.Localization.Web.UI.Scripts.LanguagesDropDownListWidget.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Localization.LanguagesDropDownListWidget.ascx");
    private LanguageSource languageSource;
    private IAppSettings appSettings;

    /// <summary>Gets the name of the embedded layout template.</summary>
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LanguagesDropDownListWidget.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the name of the command that widget fires.
    /// </summary>
    public string CommandName { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates whether a server control is rendered
    /// as UI on the page.</summary>
    /// <returns>true if the control is visible on the page; otherwise false.</returns>
    public override bool Visible
    {
      get => !base.Visible ? base.Visible : SystemManager.CurrentContext.AppSettings.Multilingual;
      set => base.Visible = value;
    }

    /// <summary>Gets or sets source for available languages.</summary>
    /// <value>The language source.</value>
    public LanguageSource LanguageSource
    {
      get => this.languageSource;
      set => this.languageSource = value;
    }

    /// <summary>
    /// Gets or sets the list of all listed cultures. This is only used if LanguageSource is set to Custom.
    /// </summary>
    /// <value>All available languages.</value>
    public IList<CultureInfo> AvailableCultures { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether an option for all languages should be added.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if an option for all languages is to be added; otherwise, <c>false</c>.
    /// </value>
    public bool AddAllLanguagesOption { get; set; }

    /// <summary>Gets the default application settings information.</summary>
    protected IAppSettings AppSettings
    {
      get
      {
        if (this.appSettings == null)
          this.appSettings = (IAppSettings) Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings;
        return this.appSettings;
      }
    }

    /// <summary>
    /// Reference to the providers list control in the toolbox item template
    /// </summary>
    protected virtual LanguagesDropDownList Languages => this.Container.GetControl<LanguagesDropDownList>("languagesDropDownList", true);

    public override void Configure(IWidgetDefinition definition)
    {
      base.Configure(definition);
      this.ConfigureFromDefinition();
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConfigureLanguagesDropDown();

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = base.GetScriptDescriptors().First<ScriptDescriptor>() as ScriptBehaviorDescriptor;
      behaviorDescriptor.AddProperty("languagesListId", (object) this.Languages.ClientID);
      behaviorDescriptor.AddProperty("commandName", (object) this.CommandName);
      yield return (ScriptDescriptor) behaviorDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (LanguagesDropDownListWidget).Assembly.FullName;
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Localization.Web.UI.Scripts.LanguagesDropDownListWidget.js"
      });
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.SitefinityJS.UI.ICommandWidget.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    private void ConfigureFromDefinition()
    {
      if (this.Definition == null)
        return;
      ILanguagesDropDownListWidgetDefinition widgetDefinition = typeof (ILanguagesDropDownListWidgetDefinition).IsAssignableFrom(this.Definition.GetType()) ? this.Definition as ILanguagesDropDownListWidgetDefinition : throw new InvalidOperationException(string.Format("The Definition of {0} control does not implement {1} interface.", (object) this.GetType().FullName, (object) typeof (ILanguagesDropDownListWidgetDefinition).FullName));
      this.LanguageSource = widgetDefinition.LanguageSource;
      this.AvailableCultures = (IList<CultureInfo>) widgetDefinition.AvailableCultures;
      this.AddAllLanguagesOption = widgetDefinition.AddAllLanguagesOption;
      this.CommandName = widgetDefinition.CommandName;
    }

    private void ConfigureLanguagesDropDown()
    {
      this.Languages.LanguageSource = this.LanguageSource;
      this.Languages.AvailableCultures = this.AvailableCultures;
      this.Languages.AddAllLanguagesOption = this.AddAllLanguagesOption;
    }
  }
}
