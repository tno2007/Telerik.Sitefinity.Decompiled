// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.HtmlField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Extenders;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  ///  This control provides the user with a way to enter formatted html content (RadEditor) in the write mode.
  ///  In the display mode the control displays the html content.
  /// </summary>
  [FieldDefinitionElement(typeof (HtmlFieldElement))]
  public class HtmlField : FieldControl, IExpandableControl
  {
    private bool? expanded = new bool?(true);
    private Unit width = Unit.Percentage(100.0);
    private Unit height = Unit.Pixel(550);
    private bool overrideRadEditorDialogs = true;
    private HtmlEditModes htmlEditModes;
    private string contentAreaCssFile;
    private string editorCssClass;
    private string editorSkin;
    private string uiCulture;
    private EditorFilters? editorContentFilters;
    private bool? editorNewLineBr;
    private Telerik.Web.UI.EditorStripFormattingOptions? editorStripFormattingOnPaste;
    private Telerik.Web.UI.EditorStripFormattingOptions? editorStripFormattingOptions;
    private EditorToolsConfiguration editorToolsConfiguration;
    private string editorToolsConfigurationKey;
    private string onClientLoad;
    private bool enabled = true;
    private object value;
    private const string relativeImageManagerDialogUrl = "~/Sitefinity/Dialog/MediaContentManagerDialog/?mode=Image";
    private const string relativeDocumentManagerDialogUrl = "~/Sitefinity/Dialog/MediaContentManagerDialog/?mode=Document";
    private const string relativeMediaManagerDialogUrl = "~/Sitefinity/Dialog/MediaContentManagerDialog/?mode=Media";
    private const string relativeLinkManagerDialogUrl = "~/Sitefinity/Dialog/LinkManagerDialog";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.HtmlField.ascx");
    private const string ILocalizableFieldControlScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js";
    private const string htmlFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.HtmlField.js";
    private const string fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.HtmlField" /> class.
    /// </summary>
    public HtmlField() => this.LayoutTemplatePath = HtmlField.layoutTemplatePath;

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The value.</value>
    [TypeConverter(typeof (ObjectStringConverter))]
    public override object Value
    {
      get
      {
        string str = string.Empty;
        switch (this.DisplayMode)
        {
          case FieldDisplayMode.Read:
            str = this.ViewControl.Text;
            break;
          case FieldDisplayMode.Write:
            str = this.EditControl.Content;
            break;
        }
        return (object) str;
      }
      set
      {
        string stringValue = this.GetStringValue(value);
        if (this.ChildControlsCreated)
        {
          switch (this.DisplayMode)
          {
            case FieldDisplayMode.Read:
              this.ViewControl.Text = stringValue;
              break;
            case FieldDisplayMode.Write:
              this.EditControl.Content = stringValue;
              break;
          }
          this.value = (object) null;
        }
        else
          this.value = (object) stringValue;
      }
    }

    /// <summary>Gets or sets the default value of the property.</summary>
    /// <value>The value.</value>
    /// <remarks>
    /// Used to check for changes in the values
    /// Implement in the iherited control if need some specific behavior
    /// </remarks>
    public override object DefaultValue
    {
      get => string.IsNullOrEmpty(base.DefaultValue as string) ? this.Value : base.DefaultValue;
      set => base.DefaultValue = value;
    }

    /// <inheritdoc />
    public override bool Enabled
    {
      get => this.enabled;
      set => this.enabled = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the key for searching the editor tools configuration.
    /// </summary>
    public virtual string EditorToolsConfigurationKey
    {
      get => this.EditorToolsConfiguration == EditorToolsConfiguration.Custom ? this.editorToolsConfigurationKey : (string) null;
      set => this.editorToolsConfigurationKey = value;
    }

    /// <summary>
    /// Gets or sets the mode for configuring rad editor tools.
    /// </summary>
    public virtual EditorToolsConfiguration EditorToolsConfiguration
    {
      get
      {
        int toolsConfiguration = (int) this.editorToolsConfiguration;
        return this.editorToolsConfiguration;
      }
      set => this.editorToolsConfiguration = value;
    }

    /// <summary>
    /// Gets or sets a JavaScript callback function that will be called the on editor client load.
    /// </summary>
    /// <value>The on client load function callback.</value>
    public string OnClientLoad
    {
      get => this.onClientLoad;
      set
      {
        if (value.IsNullOrEmpty())
          return;
        this.onClientLoad = value;
      }
    }

    /// <summary>
    /// Gets or sets a string, containing the location of the Rad Editor's content area CSS styles.
    /// </summary>
    /// <value>The content area CSS file.</value>
    public string ContentAreaCssFile
    {
      get
      {
        if (this.contentAreaCssFile == null)
        {
          string contentAreaCssFile = Telerik.Sitefinity.Configuration.Config.Get<AppearanceConfig>().EditorContentAreaCssFile;
          this.contentAreaCssFile = !string.IsNullOrEmpty(contentAreaCssFile) || this.Page == null ? contentAreaCssFile : this.Page.ClientScript.GetWebResourceUrl(Telerik.Sitefinity.Configuration.Config.Get<ControlsConfig>().ResourcesAssemblyInfo, "Telerik.Sitefinity.Resources.Themes.EditorContentArea.css");
        }
        return this.contentAreaCssFile;
      }
      set => this.contentAreaCssFile = value;
    }

    /// <summary>Gets or sets the CSS class of the RadEditor.</summary>
    /// <value>The editor CSS class.</value>
    public string EditorCssClass
    {
      get
      {
        if (this.editorCssClass == null)
          this.editorCssClass = Telerik.Sitefinity.Configuration.Config.Get<AppearanceConfig>().EditorCssClass;
        return this.editorCssClass;
      }
      set => this.editorCssClass = value;
    }

    /// <summary>
    /// Gets or sets the skin name for the RadEditor control user interface.</summary>
    /// <value>A string containing the skin name for the control user interface. The
    /// default is string.Empty.</value>
    /// <remarks>
    /// <para>
    /// If this property is not set, the control will render using the skin named "Default".
    /// If EnableEmbeddedSkins is set to false, the control will not render skin.
    /// </para>
    /// </remarks>
    public string EditorSkin
    {
      get
      {
        if (this.editorSkin == null)
          this.editorSkin = Telerik.Sitefinity.Configuration.Config.Get<AppearanceConfig>().EditorSkin;
        return this.editorSkin;
      }
      set => this.editorSkin = value;
    }

    /// <summary>
    /// Gets or sets a value indicating which content filters will be active when the
    /// RadEditor is loaded in the browser.
    /// </summary>
    /// <value>
    /// The default value is <strong>EditorFilters.DefaultFilters</strong>.
    /// </value>
    /// <remarks>
    /// <para><see cref="T:Telerik.Web.UI.EditorStripFormattingOptions">EditorFilters
    /// </see> enum members
    /// <list type="table">
    /// <listheader>
    /// <term>Member</term>
    /// <description>Description</description>
    /// </listheader>
    /// <item>
    /// <term><strong>RemoveScripts</strong></term>
    /// <description>This filter removes script tags from the editor content. Disable
    /// the filter if you want to insert script tags in the content.</description>
    /// </item>
    /// <item>
    /// <term><strong>MakeUrlsAbsolute</strong></term>
    /// <description>This filter makes all URLs in the editor content absolute (e.g.
    /// "http://server/page.html" instead of "page.html"). This filter is DISABLED by
    /// default.</description>
    /// </item>
    /// <item>
    /// <term><strong>FixUlBoldItalic</strong></term>
    /// <description>This filter changes the deprecated u tag to a span with CSS style.
    /// </description>
    /// </item>
    /// <item>
    /// <term><strong>IECleanAnchors</strong></term>
    /// <description>Internet Explorer only - This filter removes the current page url
    /// from all anchor(#) links to the same page.</description>
    /// </item>
    /// <item>
    /// <term><strong>FixEnclosingP</strong></term>
    /// <description>This filter removes a parent paragraph tag if the whole content
    /// is inside it.</description>
    /// </item>
    /// <item>
    /// <term><strong>MozEmStrong</strong></term>
    /// <description>This filter changes b to strong and i to em in Mozilla browsers.
    /// </description>
    /// </item>
    /// <item>
    /// <term><strong>ConvertFontToSpan</strong></term>
    /// <description>This filter changes deprecated font tags to compliant span tags.
    /// </description>
    /// </item>
    /// <item>
    /// <term><strong>ConvertToXhtml</strong></term>
    /// <description>This filter converts the HTML from the editor content area to XHTML.
    /// </description>
    /// </item>
    /// <item>
    /// <term><strong>IndentHTMLContent</strong></term>
    /// <description>This filter indents the HTML content so it is more readable when
    /// you view the code.</description>
    /// </item>
    /// <item>
    /// <term><strong>OptimizeSpans</strong></term>
    /// <description>This filter tries to decrease the number of nested spans in the
    /// editor content.</description>
    /// </item>
    /// <item>
    /// <term><strong>ConvertCharactersToEntities</strong></term>
    /// <description>This filter converts reserved characters to their html entity names.
    /// </description>
    /// </item>
    /// <item>
    /// <term><strong>DefaultFilters</strong></term>
    /// <description>The default editor behavior. All content filters except MakeUrlsAbsolute
    /// are activated.</description>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    public EditorFilters? EditorContentFilters
    {
      get
      {
        if (!this.editorContentFilters.HasValue)
          this.editorContentFilters = new EditorFilters?(Telerik.Sitefinity.Configuration.Config.Get<AppearanceConfig>().EditorContentFilters);
        return this.editorContentFilters;
      }
      set => this.editorContentFilters = value;
    }

    public bool? EditorNewLineBr
    {
      get
      {
        if (!this.editorNewLineBr.HasValue)
          this.editorNewLineBr = new bool?(Telerik.Sitefinity.Configuration.Config.Get<AppearanceConfig>().EditorNewLineBr);
        return this.editorNewLineBr;
      }
      set => this.editorNewLineBr = value;
    }

    /// <summary>
    /// This property is obsolete. Please, use the StripFormattingOptions property instead.
    /// </summary>
    /// <value>
    /// The default value is <strong>EditorStripFormattingOptions.None</strong>.
    /// </value>
    [Obsolete("Please, use the EditorStripFormattingOptions property instead.")]
    public Telerik.Web.UI.EditorStripFormattingOptions? EditorStripFormattingOnPaste
    {
      get
      {
        if (!this.editorStripFormattingOnPaste.HasValue)
          this.editorStripFormattingOnPaste = this.EditorStripFormattingOptions;
        return this.editorStripFormattingOnPaste;
      }
      set => this.editorStripFormattingOnPaste = value;
    }

    /// <summary>
    /// Gets or sets the value indicating how the RadEditor should clear the HTML formatting
    /// when the user pastes data into the content area.
    /// </summary>
    /// <value>
    /// The default value is <strong>EditorStripFormattingOptions.None</strong>.
    /// </value>
    /// <remarks>
    /// <para>
    /// <see cref="T:Telerik.Web.UI.EditorStripFormattingOptions">EditorStripFormattingOptions
    /// </see>
    /// enum members
    /// <list type="table">
    /// <listheader>
    /// <term>Member</term>
    /// <description>Description</description>
    /// </listheader>
    /// <item>
    /// <term><strong>None</strong></term>
    /// <description>Doesn't strip anything, asks a question when MS Word
    /// formatting was detected.</description>
    /// </item>
    /// <item>
    /// <term><strong>NoneSupressCleanMessage</strong></term>
    /// <description>Doesn't strip anything and does not ask a
    /// question.</description>
    /// </item>
    /// <item>
    /// <term><strong>MSWord</strong></term>
    /// <description>Strips only MSWord related attributes and
    /// tags.</description>
    /// </item>
    /// <item>
    /// <term><strong>MSWordNoFonts</strong></term>
    /// <description>Strips the MSWord related attributes and tags and font
    /// tags.</description>
    /// </item>
    /// <item>
    /// <term><strong>MSWordRemoveAll</strong></term>
    /// <description>Strips MSWord related attributes and tags, font tags and
    /// font size attributes.</description>
    /// </item>
    /// <item>
    /// <term><strong>Css</strong></term>
    /// <description>Removes style attributes.</description>
    /// </item>
    /// <item>
    /// <term><strong>Font</strong></term>
    /// <description>Removes Font tags.</description>
    /// </item>
    /// <item>
    /// <term><strong>Span</strong></term>
    /// <description>Clears Span tags.</description>
    /// </item>
    /// <item>
    /// <term><strong>AllExceptNewLines</strong></term>
    /// <description>Clears all tags except "br" and new lines (\n) on paste.</description>
    /// </item>
    /// <item>
    /// <term><strong>All</strong></term>
    /// <description>Remove all HTML formatting.</description>
    /// </item>
    /// </list>
    /// </para>
    /// <para><strong>Note:</strong> In Gecko-based browsers you will see the mandatory
    /// dialog box where you need to paste the content.</para>
    /// </remarks>
    public Telerik.Web.UI.EditorStripFormattingOptions? EditorStripFormattingOptions
    {
      get
      {
        if (!this.editorStripFormattingOptions.HasValue)
          this.editorStripFormattingOptions = new Telerik.Web.UI.EditorStripFormattingOptions?(Telerik.Sitefinity.Configuration.Config.Get<AppearanceConfig>().EditorStripFormattingOptions);
        return this.editorStripFormattingOptions;
      }
      set => this.editorStripFormattingOptions = value;
    }

    /// <summary>Gets or sets the width of the Web server control.</summary>
    public new virtual Unit Width
    {
      get => this.width;
      set => this.width = value;
    }

    /// <summary>Gets or sets the height of the Web server control.</summary>
    public new virtual Unit Height
    {
      get => this.height;
      set => this.height = value;
    }

    /// <summary>
    /// Set this property to true to fix the cursor focus issue with Firefox 3.6.
    /// https://bugzilla.mozilla.org/show_bug.cgi?id=542727
    /// </summary>
    public virtual bool FixCursorIssue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to override the RadEditor dialogs.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to override the RadEditor dialogs; otherwise, <c>false</c>.
    /// </value>
    public bool IsToOverrideDialogs
    {
      get => this.overrideRadEditorDialogs;
      set => this.overrideRadEditorDialogs = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Web.UI.RadEditor" /> to extend.
    /// </summary>
    /// <value></value>
    public RadEditor Editor => this.EditControl;

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Web.UI.RadEditor" /> edit modes (HTML, Design).
    /// </summary>
    /// <value></value>
    public HtmlEditModes HtmlFieldEditModes
    {
      get => this.htmlEditModes;
      set => this.htmlEditModes = value;
    }

    /// <summary>Gets the image manager dialog URL.</summary>
    /// <value>The image manager dialog URL.</value>
    public string ImageManagerDialogUrl => RouteHelper.ResolveUrl("~/Sitefinity/Dialog/MediaContentManagerDialog/?mode=Image", UrlResolveOptions.Rooted);

    /// <summary>Gets the document manager dialog URL.</summary>
    /// <value>The document manager dialog URL.</value>
    public string DocumentManagerDialogUrl => RouteHelper.ResolveUrl("~/Sitefinity/Dialog/MediaContentManagerDialog/?mode=Document", UrlResolveOptions.Rooted);

    /// <summary>Gets the media manager dialog URL.</summary>
    /// <value>The media manager dialog URL.</value>
    public string MediaManagerDialogUrl => RouteHelper.ResolveUrl("~/Sitefinity/Dialog/MediaContentManagerDialog/?mode=Media", UrlResolveOptions.Rooted);

    /// <summary>Gets the link manager dialog URL.</summary>
    /// <value>The link manager dialog URL.</value>
    public string LinkManagerDialogUrl => RouteHelper.ResolveUrl("~/Sitefinity/Dialog/LinkManagerDialog", UrlResolveOptions.Rooted);

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public string UICulture
    {
      get => this.uiCulture;
      set
      {
        this.uiCulture = value;
        this.ConfigureRadEditorDialogsExtender();
      }
    }

    /// <summary>
    /// Gets or sets the text that will be displayed on the control that can expand the hidden part.
    /// </summary>
    /// <value></value>
    public string ExpandText { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the control is expanded by default. If control is to
    /// be expanded by default true; otherwise false.
    /// </summary>
    /// <value></value>
    public bool? Expanded
    {
      get => this.expanded;
      set => this.expanded = value;
    }

    /// <summary>
    /// Gets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    /// <value></value>
    public WebControl ExpandControl => (WebControl) this.ExpandLink;

    /// <summary>
    /// Gets the reference to the control that is hidden when Expanded is false and displayed
    /// upon clicking of the ExpandControl.
    /// </summary>
    /// <value></value>
    public WebControl ExpandTarget => this.Container.GetControl<WebControl>("expandableTarget", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the label that displays the html content in read mode.
    /// </summary>
    protected internal virtual ITextControl ViewControl => this.Container.GetControl<ITextControl>("viewControl", this.DisplayMode == FieldDisplayMode.Read);

    /// <summary>
    /// Gets the reference to the rad editor for editing the html content in write mode.
    /// </summary>
    protected internal virtual RadEditor EditControl => this.Container.GetControl<RadEditor>("editControl", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label that displays the title of the html field.
    /// </summary>
    protected internal virtual Label TitleLabel => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<Label>("titleLabel_write", true) : this.Container.GetControl<Label>("titleLabel_read", false);

    /// <summary>
    /// Gets the reference to the link that expands the hidden part of the html field.
    /// </summary>
    protected internal virtual LinkButton ExpandLink => this.Container.GetControl<LinkButton>("expandLink", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label that displays the description of the html field.
    /// </summary>
    protected internal virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label that displays the example of the html field.
    /// </summary>
    protected internal virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the custom dialog extender for the RadEditor.</summary>
    /// <value>The editor custom dialogs extender.</value>
    protected internal virtual RadEditorCustomDialogsExtender EditorCustomDialogsExtender => this.Container.GetControl<RadEditorCustomDialogsExtender>("editorCustomDialogsExtender", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      switch (this.DisplayMode)
      {
        case FieldDisplayMode.Read:
          if (this.value != null)
            this.ViewControl.Text = this.Value.ToString();
          this.TitleLabel.Text = this.Title;
          break;
        case FieldDisplayMode.Write:
          this.LoadEditorToolsFile();
          this.ConfigureRadEditorDialogsExtender();
          this.EditControl.Width = this.Width;
          this.EditControl.Height = this.Height;
          this.EditControl.ContentAreaCssFile = this.ContentAreaCssFile;
          if (!this.onClientLoad.IsNullOrEmpty())
            this.EditControl.OnClientLoad = this.OnClientLoad;
          if (this.EditorCssClass != null)
            this.EditControl.CssClass = this.EditorCssClass;
          if (this.EditorSkin != null)
            this.EditControl.Skin = this.EditorSkin;
          if (this.EditorContentFilters.HasValue)
            this.EditControl.ContentFilters = this.EditorContentFilters.Value;
          if (this.EditorNewLineBr.HasValue)
            this.EditControl.NewLineBr = this.EditorNewLineBr.Value;
          if (this.EditorStripFormattingOnPaste.HasValue)
            this.EditControl.StripFormattingOnPaste = this.EditorStripFormattingOnPaste.Value;
          if (this.EditorStripFormattingOptions.HasValue)
            this.EditControl.StripFormattingOptions = this.EditorStripFormattingOptions.Value;
          this.ExampleLabel.Text = this.Example;
          this.TitleLabel.Text = this.Title;
          this.DescriptionLabel.Text = this.Description;
          this.LoadEditModes();
          if (!this.Expanded.GetValueOrDefault())
          {
            this.ExpandControl.TabIndex = this.TabIndex;
            break;
          }
          break;
      }
      string userAgent = HttpContext.Current.Request.UserAgent;
      if (userAgent == null)
        return;
      string lower = userAgent.ToLower();
      if (!lower.Contains("iphone") && !lower.Contains("ipad"))
        return;
      this.EditControl.ContentAreaMode = EditorContentAreaMode.Div;
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IHtmlFieldDefinition htmlFieldDefinition))
        return;
      if (htmlFieldDefinition.IsToOverrideDialogs.HasValue)
        this.IsToOverrideDialogs = htmlFieldDefinition.IsToOverrideDialogs.Value;
      if (htmlFieldDefinition.HtmlFieldEditModes.HasValue)
        this.HtmlFieldEditModes = htmlFieldDefinition.HtmlFieldEditModes.Value;
      if (htmlFieldDefinition.ContentAreaCssFile != null)
        this.ContentAreaCssFile = htmlFieldDefinition.ContentAreaCssFile;
      if (htmlFieldDefinition.EditorCssClass != null)
        this.EditorCssClass = htmlFieldDefinition.EditorCssClass;
      if (htmlFieldDefinition.EditorSkin != null)
        this.EditorSkin = htmlFieldDefinition.EditorSkin;
      if (htmlFieldDefinition.EditorContentFilters.HasValue)
        this.EditorContentFilters = new EditorFilters?(htmlFieldDefinition.EditorContentFilters.Value);
      if (htmlFieldDefinition.EditorNewLineBr.HasValue)
        this.EditorNewLineBr = new bool?(htmlFieldDefinition.EditorNewLineBr.Value);
      if (htmlFieldDefinition.EditorStripFormattingOnPaste.HasValue)
        this.EditorStripFormattingOnPaste = new Telerik.Web.UI.EditorStripFormattingOptions?(htmlFieldDefinition.EditorStripFormattingOnPaste.Value);
      if (htmlFieldDefinition.EditorStripFormattingOptions.HasValue)
        this.EditorStripFormattingOptions = new Telerik.Web.UI.EditorStripFormattingOptions?(htmlFieldDefinition.EditorStripFormattingOptions.Value);
      if (htmlFieldDefinition.EditorToolsConfiguration.HasValue)
        this.EditorToolsConfiguration = htmlFieldDefinition.EditorToolsConfiguration.Value;
      if (string.IsNullOrEmpty(htmlFieldDefinition.EditorToolsConfigurationKey))
        return;
      this.EditorToolsConfigurationKey = htmlFieldDefinition.EditorToolsConfigurationKey;
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (!(this.value is string) || string.IsNullOrEmpty((string) this.value))
        return;
      this.Value = this.value;
    }

    /// <summary>Gets string representation of Value field.</summary>
    public virtual string GetStringValue(object value)
    {
      string stringValue = string.Empty;
      if (value != null)
        stringValue = (object) (value as Lstring) == null ? value as string : ((Lstring) value).Value;
      return stringValue;
    }

    public event ToolsLoadedHandler ToolsLoaded;

    protected virtual void OnToolsLoaded(EventArgs e)
    {
      if (this.ToolsLoaded == null)
        return;
      this.ToolsLoaded((object) this, e);
    }

    protected internal virtual void LoadEditorToolsFile()
    {
      Assembly assembly = Telerik.Sitefinity.Configuration.Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly;
      XmlDocument doc = (XmlDocument) null;
      switch (this.EditorToolsConfiguration)
      {
        case EditorToolsConfiguration.Standard:
          doc = this.LoadStandardConfiguration_Private(assembly);
          break;
        case EditorToolsConfiguration.Minimal:
          doc = this.LoadMinimalConfiguration_Private(assembly);
          break;
        case EditorToolsConfiguration.Forums:
          doc = this.LoadForumsConfiguration_Private(assembly);
          break;
        case EditorToolsConfiguration.Custom:
          doc = this.LoadCustomConfiguration_Private(assembly);
          break;
      }
      if (doc != null)
        this.EditControl.LoadToolsFile(doc);
      this.OnToolsLoaded(EventArgs.Empty);
    }

    protected internal virtual void LoadEditModes()
    {
      if (this.HtmlFieldEditModes == HtmlEditModes.All)
        this.EditControl.EditModes = EditModes.All;
      if (this.HtmlFieldEditModes == HtmlEditModes.Preview)
        this.EditControl.EditModes = EditModes.Preview;
      if (this.HtmlFieldEditModes == HtmlEditModes.Html)
        this.EditControl.EditModes = EditModes.Html;
      if (this.HtmlFieldEditModes != HtmlEditModes.Design)
        return;
      this.EditControl.EditModes = EditModes.Design;
    }

    internal virtual XmlDocument LoadStandardConfiguration_Private(
      Assembly resourceAssembly)
    {
      return this.LoadConfigurationToolSet(Telerik.Sitefinity.Configuration.Config.Get<AppearanceConfig>().StandardEditorConfiguration, resourceAssembly) ?? this.LoadConfigurationToolSet("Telerik.Sitefinity.Resources.Themes.StandardToolsFile.xml", resourceAssembly);
    }

    internal virtual XmlDocument LoadMinimalConfiguration_Private(
      Assembly resourceAssembly)
    {
      return this.LoadConfigurationToolSet(Telerik.Sitefinity.Configuration.Config.Get<AppearanceConfig>().MinimalEditorConfiguration, resourceAssembly) ?? this.LoadConfigurationToolSet("Telerik.Sitefinity.Resources.Themes.MinimalToolsFile.xml", resourceAssembly);
    }

    internal virtual XmlDocument LoadForumsConfiguration_Private(
      Assembly resourceAssembly)
    {
      XmlDocument xmlDocument = this.LoadConfigurationToolSet(Telerik.Sitefinity.Configuration.Config.Get<AppearanceConfig>().ForumsEditorConfiguration, resourceAssembly) ?? this.LoadConfigurationToolSet("Telerik.Sitefinity.Resources.Themes.ForumsToolsFile.xml", resourceAssembly);
      XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("tool");
      for (int i = 0; i < elementsByTagName.Count; ++i)
      {
        XmlAttribute attribute = elementsByTagName[i].Attributes["name"];
        if (attribute != null && attribute.InnerText == "ImageManager")
        {
          xmlDocument.RemoveChild(elementsByTagName[i]);
          break;
        }
      }
      return xmlDocument;
    }

    internal virtual XmlDocument LoadCustomConfiguration_Private(
      Assembly resourceAssembly)
    {
      if (string.IsNullOrEmpty(this.EditorToolsConfigurationKey))
        throw new ArgumentException("EditorToolsConfigurationKey is empty!");
      ConfigValueDictionary editorConfigurations = Telerik.Sitefinity.Configuration.Config.Get<AppearanceConfig>().EditorConfigurations;
      string toolSetValue = editorConfigurations.ContainsKey(this.EditorToolsConfigurationKey) ? editorConfigurations[this.EditorToolsConfigurationKey] : throw new ConfigurationErrorsException("EditorToolsConfigurationKey is set to " + this.EditorToolsConfigurationKey + ". There is no defined value for this key");
      return !string.IsNullOrEmpty(toolSetValue) ? this.LoadConfigurationToolSet(toolSetValue, resourceAssembly) : throw new ConfigurationErrorsException("There is no defined value for the " + this.EditorToolsConfigurationKey + " key");
    }

    /// <summary>
    /// Generates the xmlDocument, which populates the editor's tools.
    /// If the toolSetValue value starts with &lt;, it is considered as XML string, which is loaded in the result XmlDocument.
    /// IF the toolSetValue value starts with ~, it is considerd as virtual path to an xml file, which is loaded in the result XmlDocument.
    /// Else the toolSetValue is considerd as an xml file, which is an assemby embeded resource.
    /// </summary>
    internal virtual XmlDocument LoadConfigurationToolSet(
      string toolSetValue,
      Assembly resourceAssembly)
    {
      XmlDocument xmlDocument;
      if (toolSetValue.Trim().StartsWith("<"))
      {
        xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(toolSetValue.Trim());
      }
      else
        xmlDocument = !toolSetValue.Trim().StartsWith("~") ? this.GetXmlDocument(resourceAssembly, toolSetValue) : this.GetXmlDocument(toolSetValue);
      return xmlDocument;
    }

    /// <summary>Configures the RAD editor dialogs extender.</summary>
    protected virtual void ConfigureRadEditorDialogsExtender()
    {
      this.EditorCustomDialogsExtender.IsToOverrideDialogs = this.IsToOverrideDialogs;
      this.EditorCustomDialogsExtender.ImageManagerDialogUrl = this.ImageManagerDialogUrl;
      this.EditorCustomDialogsExtender.DocumentManagerDialogUrl = this.DocumentManagerDialogUrl;
      this.EditorCustomDialogsExtender.MediaManagerDialogUrl = this.MediaManagerDialogUrl;
      this.EditorCustomDialogsExtender.LinkManagerDialogUrl = this.LinkManagerDialogUrl;
      this.EditorCustomDialogsExtender.UICulture = this.UICulture;
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.TelerikSitefinity;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) this.GetBaseScriptDescriptors().Last<ScriptDescriptor>();
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        scriptDescriptors.Add((ScriptDescriptor) this.GetExpandableExtenderDescriptor(this.ClientID));
        controlDescriptor.AddProperty("editControlId", (object) this.EditControl.ClientID);
        controlDescriptor.AddProperty("uiCulture", (object) this.UICulture);
        controlDescriptor.AddComponentProperty("editControl", this.EditControl.ClientID);
        controlDescriptor.AddProperty("_fixCursorIssue", (object) this.FixCursorIssue.ToString());
        controlDescriptor.AddProperty("editorToolbarMode", (object) this.EditorToolsConfiguration.ToString());
        if (this.EditorToolsConfiguration == EditorToolsConfiguration.Minimal || this.EditorToolsConfiguration == EditorToolsConfiguration.Forums)
        {
          controlDescriptor.AddProperty("_toolbarMoreToolsLabel", (object) Telerik.Sitefinity.Localization.Res.Get<Labels>().FormattingOptions);
          controlDescriptor.AddProperty("_toolbarLessToolsLabel", (object) Telerik.Sitefinity.Localization.Res.Get<Labels>().HideFormattingOptions);
        }
        else
        {
          controlDescriptor.AddProperty("_toolbarMoreToolsLabel", (object) Telerik.Sitefinity.Localization.Res.Get<Labels>().MoreFormattingTools);
          controlDescriptor.AddProperty("_toolbarLessToolsLabel", (object) Telerik.Sitefinity.Localization.Res.Get<Labels>().BasicToolsOnly);
        }
      }
      else
        controlDescriptor.AddElementProperty("viewControl", ((Control) this.ViewControl).ClientID);
      controlDescriptor.AddProperty("_enabled", (object) this.Enabled);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
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
      if (this.DisplayMode == FieldDisplayMode.Write)
        scriptReferences.Add(this.GetExpandableExtenderScript());
      string fullName = typeof (HtmlField).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.HtmlField.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Gets the base script descriptors.</summary>
    /// <returns></returns>
    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    /// <summary>
    /// Gets the XML document by the specified assembly and resource name.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <param name="resourceName">Name of the resource.</param>
    /// <returns></returns>
    protected internal virtual XmlDocument GetXmlDocument(
      Assembly assembly,
      string resourceName)
    {
      using (Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName))
      {
        if (manifestResourceStream == null)
          throw new FileNotFoundException("The file: \"" + resourceName + "\" in the assembly: \"" + assembly.FullName + "\" was not found!");
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(manifestResourceStream);
        return xmlDocument;
      }
    }

    /// <summary>Gets the XML document from the specified URL.</summary>
    /// <param name="virtualPath">The virtual path.</param>
    /// <returns></returns>
    protected internal virtual XmlDocument GetXmlDocument(string virtualPath)
    {
      if (VirtualPathManager.FileExists(virtualPath))
      {
        using (Stream inStream = VirtualPathManager.OpenFile(virtualPath))
        {
          if (inStream == null)
            throw new FileNotFoundException("The file: \"" + virtualPath + "\" was not found!");
          XmlDocument xmlDocument = new XmlDocument();
          xmlDocument.Load(inStream);
          return xmlDocument;
        }
      }
      else
      {
        string str = SystemManager.CurrentHttpContext.Server.MapPath(virtualPath);
        if (!File.Exists(str))
        {
          FileNotFoundException exceptionToHandle = new FileNotFoundException("The file: \"" + virtualPath + "\" was not found!");
          if (Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
            throw exceptionToHandle;
          return (XmlDocument) null;
        }
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(str);
        return xmlDocument;
      }
    }
  }
}
