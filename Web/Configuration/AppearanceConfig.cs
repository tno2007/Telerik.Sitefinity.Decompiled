// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.AppearanceConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.Configuration
{
  /// <summary>
  /// Represents the configuration section which defines the appearance of the
  /// Sitefinity backend.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "AppearanceConfig", Title = "AppearanceTitle")]
  public class AppearanceConfig : ConfigSection
  {
    private const string FrontendResourcesCacheDurationPropName = "frontendResourcesCacheDuration";
    internal const string StandardEditorConfigurationDefaultValue = "Telerik.Sitefinity.Resources.Themes.StandardToolsFile.xml";
    internal const string ForumsEditorConfigurationDefaultValue = "Telerik.Sitefinity.Resources.Themes.ForumsToolsFile.xml";
    internal const string MinimalEditorConfigurationDefaultValue = "Telerik.Sitefinity.Resources.Themes.MinimalToolsFile.xml";
    internal const string StandardEditorConfigurationKey = "Default tool set";
    internal const string MinimalEditorConfigurationKey = "Tool set for comments";
    internal const string ForumsEditorConfigurationKey = "Tool set for forums";

    /// <summary>
    /// Gets or sets the default theme for the Sitefinity backend.
    /// </summary>
    /// <value>The name of the default theme.</value>
    [ConfigurationProperty("backendTheme", DefaultValue = "Light", IsRequired = false)]
    public string BackendTheme
    {
      get => this["backendTheme"].ToString();
      set => this["backendTheme"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the default theme for the Sitefinity frontend pages.
    /// </summary>
    [ConfigurationProperty("defaultFrontendTheme", DefaultValue = "Basic", IsRequired = true)]
    public string DefaultFrontendTheme
    {
      get => (string) this["defaultFrontendTheme"];
      set => this["defaultFrontendTheme"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the path to the file that specifies the standard editor configuration.
    /// </summary>
    /// <value>The standard editor configuration.</value>
    [ConfigurationProperty("standardEditorConfiguration", DefaultValue = "Telerik.Sitefinity.Resources.Themes.StandardToolsFile.xml", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "StandardEditorConfigurationDescription", Title = "StandardEditorConfigurationTitle")]
    public string StandardEditorConfiguration
    {
      get => (string) this["standardEditorConfiguration"];
      set => this["standardEditorConfiguration"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the path to the file that specifies the forums editor configuration.
    /// </summary>
    /// <value>The editor configuration used at forums content type.</value>
    [ConfigurationProperty("forumsEditorConfiguration", DefaultValue = "Telerik.Sitefinity.Resources.Themes.ForumsToolsFile.xml", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ForumsEditorConfigurationDescription", Title = "ForumsEditorConfigurationTitle")]
    public string ForumsEditorConfiguration
    {
      get => (string) this["forumsEditorConfiguration"];
      set => this["forumsEditorConfiguration"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the path to the file that specifies the minimal editor configuration.
    /// </summary>
    /// <value>The minimal editor configuration.</value>
    [ConfigurationProperty("minimalEditorConfiguration", DefaultValue = "Telerik.Sitefinity.Resources.Themes.MinimalToolsFile.xml", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MinimalEditorConfigurationDescription", Title = "MinimalEditorConfigurationTitle")]
    public string MinimalEditorConfiguration
    {
      get => (string) this["minimalEditorConfiguration"];
      set => this["minimalEditorConfiguration"] = (object) value;
    }

    /// <summary>Returns a collection of editor configurations.</summary>
    [ConfigurationProperty("editorConfigurations")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorConfigurationsDescription", Title = "EditorConfigurationsTitle")]
    public ConfigValueDictionary EditorConfigurations => (ConfigValueDictionary) this["editorConfigurations"];

    /// <summary>Returns a collection of registered frontend themes</summary>
    [ConfigurationProperty("frontendThemes")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FrontendThemesDescription", Title = "FrontendThemesTitle")]
    [ConfigurationCollection(typeof (ThemeElement), AddItemName = "add")]
    public ConfigElementDictionary<string, ThemeElement> FrontendThemes => (ConfigElementDictionary<string, ThemeElement>) this["frontendThemes"];

    /// <summary>Returns a collection of registered backend themes</summary>
    [ConfigurationProperty("backendThemes")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendThemesDescription", Title = "BackendThemesTitle")]
    [ConfigurationCollection(typeof (ThemeElement), AddItemName = "add")]
    public ConfigElementDictionary<string, ThemeElement> BackendThemes => (ConfigElementDictionary<string, ThemeElement>) this["backendThemes"];

    /// <summary>
    /// Gets or sets a value indicating which content filters will be active when the
    /// Content Block widget is used. If the value is different from None filter setting
    /// applys to all ContentBlock controls unless you explicitly set EditorContentFilters
    /// property of Content Block widget. The control property has higer priorty and overrides
    /// config settings. This allows you to set different filters per control instance.
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
    [ConfigurationProperty("contentBlockContentFilters", DefaultValue = EditorFilters.DefaultFilters)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentBlockFiltersDescription", Title = "ContentBlockFiltersCaption")]
    public EditorFilters ContentBlockFilters
    {
      get => (EditorFilters) this["contentBlockContentFilters"];
      set => this["contentBlockContentFilters"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a string, containing the location of the Rad Editor's content area CSS styles.
    /// </summary>
    /// <value>The content area CSS file.</value>
    [ConfigurationProperty("editorContentAreaCssFile")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorContentAreaCssFileDescription", Title = "EditorContentAreaCssFileCaption")]
    public string EditorContentAreaCssFile
    {
      get => (string) this["editorContentAreaCssFile"];
      set => this["editorContentAreaCssFile"] = (object) value;
    }

    /// <summary>Gets or sets the CSS class of the RadEditor.</summary>
    /// <value>The editor CSS class.</value>
    [ConfigurationProperty("editorCssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorCssClassDescription", Title = "EditorCssClassCaption")]
    public string EditorCssClass
    {
      get => (string) this["editorCssClass"];
      set => this["editorCssClass"] = (object) value;
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
    [ConfigurationProperty("editorSkin", DefaultValue = "Default")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorSkinDescription", Title = "EditorSkinCaption")]
    public string EditorSkin
    {
      get => (string) this["editorSkin"];
      set => this["editorSkin"] = (object) value;
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
    [ConfigurationProperty("editorContentFilters", DefaultValue = EditorFilters.DefaultFilters)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorContentFiltersDescription", Title = "EditorContentFiltersCaption")]
    public EditorFilters EditorContentFilters
    {
      get => (EditorFilters) this["editorContentFilters"];
      set => this["editorContentFilters"] = (object) value;
    }

    [ConfigurationProperty("editorNewLineBr", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorNewLineBrDescription", Title = "EditorNewLineBrCaption")]
    public bool EditorNewLineBr
    {
      get => (bool) this["editorNewLineBr"];
      set => this["editorNewLineBr"] = (object) value;
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
    [ConfigurationProperty("editorStripFormattingOptions", DefaultValue = EditorStripFormattingOptions.MSWordRemoveAll | EditorStripFormattingOptions.Css | EditorStripFormattingOptions.Font | EditorStripFormattingOptions.Span | EditorStripFormattingOptions.ConvertWordLists)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorStripFormattingOptionsDescription", Title = "EditorStripFormattingOptionsCaption")]
    public EditorStripFormattingOptions EditorStripFormattingOptions
    {
      get => (EditorStripFormattingOptions) this["editorStripFormattingOptions"];
      set => this["editorStripFormattingOptions"] = (object) value;
    }

    /// <summary>Gets the status command descriptions.</summary>
    /// <value>The status command descriptions.</value>
    [ConfigurationProperty("statusCommandDescriptions")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "StatusCommandsDescription", Title = "StatusCommandsTitle")]
    public ConfigElementDictionary<string, StatusCommandDescription> StatusCommands => (ConfigElementDictionary<string, StatusCommandDescription>) this["statusCommandDescriptions"];

    /// <summary>
    /// Gets or sets the time duration before the web resources used in themes expire.
    /// </summary>
    /// <value>The time duration of the web resources cache.</value>
    [ConfigurationProperty("frontendResourcesCacheDuration", DefaultValue = "20160")]
    [TimeSpanValidator(MaxValueString = "10675199.02:48:05.4775807", MinValueString = "00:00:00")]
    [TypeConverter(typeof (TimeSpanMinutesConverter))]
    [DescriptionResource(typeof (ConfigDescriptions), "FrontendResourcesCacheDurationDescription")]
    public virtual TimeSpan FrontendResourcesCacheDuration
    {
      get => (TimeSpan) this["frontendResourcesCacheDuration"];
      set => this["frontendResourcesCacheDuration"] = (object) value;
    }

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      if (this.FrontendThemes.Count == 0)
        this.FrontendThemes.Add(new ThemeElement((ConfigElement) this.FrontendThemes)
        {
          Name = "Basic",
          Namespace = "Telerik.Sitefinity.Resources.Themes.Basic",
          AssemblyInfo = "Telerik.Sitefinity.Resources.Reference, Telerik.Sitefinity.Resources"
        });
      if (this.BackendThemes.Count != 0)
        return;
      this.BackendThemes.Add(new ThemeElement((ConfigElement) this.BackendThemes)
      {
        Name = "Default",
        Namespace = "Telerik.Sitefinity.Resources.Themes.Default",
        AssemblyInfo = "Telerik.Sitefinity.Resources.Reference, Telerik.Sitefinity.Resources"
      });
      this.BackendThemes.Add(new ThemeElement((ConfigElement) this.BackendThemes)
      {
        Name = "Light",
        Namespace = "Telerik.Sitefinity.Resources.Themes.Light",
        AssemblyInfo = "Telerik.Sitefinity.Resources.Reference, Telerik.Sitefinity.Resources"
      });
    }
  }
}
