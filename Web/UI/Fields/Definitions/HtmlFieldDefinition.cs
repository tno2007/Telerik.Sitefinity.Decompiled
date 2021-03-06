// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.HtmlFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  ///  A class that provides all information needed to construct html field control.
  /// </summary>
  public class HtmlFieldDefinition : 
    FieldControlDefinition,
    IHtmlFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private bool? isToOverrideDialogs;
    private HtmlEditModes? htmlFieldEditModes;
    private string contentAreaCssFile;
    private string editorCssClass;
    private string editorSkin;
    private EditorFilters? editorContentFilters;
    private bool? editorNewLineBr;
    private Telerik.Web.UI.EditorStripFormattingOptions? editorStripFormattingOnPaste;
    private Telerik.Web.UI.EditorStripFormattingOptions? editorStripFormattingOptions;
    private Telerik.Sitefinity.Web.UI.Fields.Enums.EditorToolsConfiguration? editorToolsConfiguration;
    private string editorToolsConfigurationKey;
    private bool isLocalizable;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ColumnDefinition" /> class.
    /// </summary>
    public HtmlFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public HtmlFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public HtmlFieldDefinition GetDefinition() => this;

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (HtmlField);

    /// <summary>
    /// Gets or sets a value indicating whether to override the RadEditor dialogs.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to override the RadEditor dialogs; otherwise, <c>false</c>.
    /// </value>
    public bool? IsToOverrideDialogs
    {
      get => this.ResolveProperty<bool?>(nameof (IsToOverrideDialogs), this.isToOverrideDialogs);
      set => this.isToOverrideDialogs = value;
    }

    /// <summary>
    /// Gets or sets a value indicating in which mode the RadEditor to load (HTML/Design/Preview/All)
    /// </summary>
    /// <value>
    /// 	<c>Design (1)</c> if design mode, <c>HTML (2)</c> if HTML mode <c>Preview (4)</c> if preview mode and <c>All (7)</c> if all modes.
    /// </value>
    public HtmlEditModes? HtmlFieldEditModes
    {
      get => this.ResolveProperty<HtmlEditModes?>(nameof (HtmlFieldEditModes), this.htmlFieldEditModes);
      set => this.htmlFieldEditModes = value;
    }

    /// <summary>
    /// Gets or sets the CSS file that will be added to the RadEditor's content area.
    /// </summary>
    /// <value>The content area CSS file.</value>
    public string ContentAreaCssFile
    {
      get => this.ResolveProperty<string>(nameof (ContentAreaCssFile), this.contentAreaCssFile);
      set => this.contentAreaCssFile = value;
    }

    /// <summary>Gets or sets the CSS class of the RadEditor.</summary>
    /// <value>The editor CSS class.</value>
    public string EditorCssClass
    {
      get => this.ResolveProperty<string>(nameof (EditorCssClass), this.editorCssClass);
      set => this.editorCssClass = value;
    }

    /// <summary>
    /// Gets or sets the skin name for the RadEditor control user interface.
    /// </summary>
    /// <value>
    /// A string containing the skin name for the control user interface. The
    /// default is string.Empty.
    /// </value>
    /// <remarks>
    /// If this property is not set, the control will render using the skin named "Default".
    /// If EnableEmbeddedSkins is set to false, the control will not render skin.
    /// </remarks>
    public string EditorSkin
    {
      get => this.ResolveProperty<string>(nameof (EditorSkin), this.editorSkin);
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
    /// 	<see cref="T:Telerik.Web.UI.EditorStripFormattingOptions">EditorFilters
    /// </see> enum members
    /// <list type="table">
    /// 		<listheader>
    /// 			<term>Member</term>
    /// 			<description>Description</description>
    /// 		</listheader>
    /// 		<item>
    /// 			<term><strong>RemoveScripts</strong></term>
    /// 			<description>This filter removes script tags from the editor content. Disable
    /// the filter if you want to insert script tags in the content.</description>
    /// 		</item>
    /// 		<item>
    /// 			<term><strong>MakeUrlsAbsolute</strong></term>
    /// 			<description>This filter makes all URLs in the editor content absolute (e.g.
    /// "http://server/page.html" instead of "page.html"). This filter is DISABLED by
    /// default.</description>
    /// 		</item>
    /// 		<item>
    /// 			<term><strong>FixUlBoldItalic</strong></term>
    /// 			<description>This filter changes the deprecated u tag to a span with CSS style.
    /// </description>
    /// 		</item>
    /// 		<item>
    /// 			<term><strong>IECleanAnchors</strong></term>
    /// 			<description>Internet Explorer only - This filter removes the current page url
    /// from all anchor(#) links to the same page.</description>
    /// 		</item>
    /// 		<item>
    /// 			<term><strong>FixEnclosingP</strong></term>
    /// 			<description>This filter removes a parent paragraph tag if the whole content
    /// is inside it.</description>
    /// 		</item>
    /// 		<item>
    /// 			<term><strong>MozEmStrong</strong></term>
    /// 			<description>This filter changes b to strong and i to em in Mozilla browsers.
    /// </description>
    /// 		</item>
    /// 		<item>
    /// 			<term><strong>ConvertFontToSpan</strong></term>
    /// 			<description>This filter changes deprecated font tags to compliant span tags.
    /// </description>
    /// 		</item>
    /// 		<item>
    /// 			<term><strong>ConvertToXhtml</strong></term>
    /// 			<description>This filter converts the HTML from the editor content area to XHTML.
    /// </description>
    /// 		</item>
    /// 		<item>
    /// 			<term><strong>IndentHTMLContent</strong></term>
    /// 			<description>This filter indents the HTML content so it is more readable when
    /// you view the code.</description>
    /// 		</item>
    /// 		<item>
    /// 			<term><strong>OptimizeSpans</strong></term>
    /// 			<description>This filter tries to decrease the number of nested spans in the
    /// editor content.</description>
    /// 		</item>
    /// 		<item>
    /// 			<term><strong>ConvertCharactersToEntities</strong></term>
    /// 			<description>This filter converts reserved characters to their html entity names.
    /// </description>
    /// 		</item>
    /// 		<item>
    /// 			<term><strong>DefaultFilters</strong></term>
    /// 			<description>The default editor behavior. All content filters except MakeUrlsAbsolute
    /// are activated.</description>
    /// 		</item>
    /// 	</list>
    /// </remarks>
    public EditorFilters? EditorContentFilters
    {
      get => this.ResolveProperty<EditorFilters?>(nameof (EditorContentFilters), this.editorContentFilters);
      set => this.editorContentFilters = value;
    }

    /// <summary>
    /// Gets or sets the value indicating whether the RadEditor will insert a new line
    /// or
    /// a paragraph when the [Enter] key is pressed.
    /// </summary>
    /// <value>
    /// 	<strong>true</strong> when the RadEditor will insert <br /> tag when the
    /// [Enter] key is pressed; otherwise <strong>false</strong>. The default
    /// value is <strong>true</strong>.
    /// </value>
    /// <remarks>
    /// 	<strong>Note</strong>: this property is intended for use only in Internet
    /// Explorer.
    /// The gecko-based browsers always insert <br /> tags.
    /// </remarks>
    public bool? EditorNewLineBr
    {
      get => this.ResolveProperty<bool?>(nameof (EditorNewLineBr), this.editorNewLineBr);
      set => this.editorNewLineBr = value;
    }

    /// <summary>
    /// This property is obsolete. Please, use the StripFormattingOptions property instead.
    /// </summary>
    /// <value>
    /// The default value is <strong>EditorStripFormattingOptions.None</strong>.
    /// </value>
    public Telerik.Web.UI.EditorStripFormattingOptions? EditorStripFormattingOnPaste
    {
      get => this.ResolveProperty<Telerik.Web.UI.EditorStripFormattingOptions?>(nameof (EditorStripFormattingOnPaste), this.editorStripFormattingOnPaste);
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
    /// 	<para>
    /// 		<see cref="T:Telerik.Web.UI.EditorStripFormattingOptions">EditorStripFormattingOptions
    /// </see>
    /// enum members
    /// <list type="table">
    /// 			<listheader>
    /// 				<term>Member</term>
    /// 				<description>Description</description>
    /// 			</listheader>
    /// 			<item>
    /// 				<term><strong>None</strong></term>
    /// 				<description>Doesn't strip anything, asks a question when MS Word
    /// formatting was detected.</description>
    /// 			</item>
    /// 			<item>
    /// 				<term><strong>NoneSupressCleanMessage</strong></term>
    /// 				<description>Doesn't strip anything and does not ask a
    /// question.</description>
    /// 			</item>
    /// 			<item>
    /// 				<term><strong>MSWord</strong></term>
    /// 				<description>Strips only MSWord related attributes and
    /// tags.</description>
    /// 			</item>
    /// 			<item>
    /// 				<term><strong>MSWordNoFonts</strong></term>
    /// 				<description>Strips the MSWord related attributes and tags and font
    /// tags.</description>
    /// 			</item>
    /// 			<item>
    /// 				<term><strong>MSWordRemoveAll</strong></term>
    /// 				<description>Strips MSWord related attributes and tags, font tags and
    /// font size attributes.</description>
    /// 			</item>
    /// 			<item>
    /// 				<term><strong>Css</strong></term>
    /// 				<description>Removes style attributes.</description>
    /// 			</item>
    /// 			<item>
    /// 				<term><strong>Font</strong></term>
    /// 				<description>Removes Font tags.</description>
    /// 			</item>
    /// 			<item>
    /// 				<term><strong>Span</strong></term>
    /// 				<description>Clears Span tags.</description>
    /// 			</item>
    /// 			<item>
    /// 				<term><strong>AllExceptNewLines</strong></term>
    /// 				<description>Clears all tags except "br" and new lines (\n) on paste.</description>
    /// 			</item>
    /// 			<item>
    /// 				<term><strong>All</strong></term>
    /// 				<description>Remove all HTML formatting.</description>
    /// 			</item>
    /// 		</list>
    /// 	</para>
    /// 	<para><strong>Note:</strong> In Gecko-based browsers you will see the mandatory
    /// dialog box where you need to paste the content.</para>
    /// </remarks>
    public Telerik.Web.UI.EditorStripFormattingOptions? EditorStripFormattingOptions
    {
      get => this.ResolveProperty<Telerik.Web.UI.EditorStripFormattingOptions?>(nameof (EditorStripFormattingOptions), this.editorStripFormattingOptions);
      set => this.editorStripFormattingOptions = value;
    }

    public Telerik.Sitefinity.Web.UI.Fields.Enums.EditorToolsConfiguration? EditorToolsConfiguration
    {
      get => this.ResolveProperty<Telerik.Sitefinity.Web.UI.Fields.Enums.EditorToolsConfiguration?>(nameof (EditorToolsConfiguration), this.editorToolsConfiguration);
      set => this.editorToolsConfiguration = value;
    }

    /// <summary>
    /// Gets or sets the key for searching the editor custom toolset configuration,
    /// when EditorToolsConfiguration is set to Custom.
    /// </summary>
    public string EditorToolsConfigurationKey
    {
      get => this.ResolveProperty<string>(nameof (EditorToolsConfigurationKey), this.editorToolsConfigurationKey);
      set => this.editorToolsConfigurationKey = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable.
    /// </summary>
    public bool IsLocalizable
    {
      get => this.ResolveProperty<bool>(nameof (IsLocalizable), this.isLocalizable);
      set => this.isLocalizable = value;
    }
  }
}
