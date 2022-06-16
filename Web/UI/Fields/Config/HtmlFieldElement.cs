// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.HtmlFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for html fields.</summary>
  public class HtmlFieldElement : 
    FieldControlDefinitionElement,
    IHtmlFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.HtmlFieldElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public HtmlFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal HtmlFieldElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new HtmlFieldDefinition((ConfigElement) this);

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (HtmlField);

    /// <summary>
    /// Gets or sets a value indicating in which mode the RadEditor to load (HTML/Design/Preview/All)
    /// </summary>
    /// <value>
    /// 	<c>Design (1)</c> if design mode, <c>HTML (2)</c> if HTML mode <c>Preview (4)</c> if preview mode and <c>All (7)</c> if all modes.
    /// </value>
    [ConfigurationProperty("HtmlFieldEditModes")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HtmlFieldEditModesDescription", Title = "HtmlFieldEditModesCaption")]
    public HtmlEditModes? HtmlFieldEditModes
    {
      get => (HtmlEditModes?) this[nameof (HtmlFieldEditModes)];
      set => this[nameof (HtmlFieldEditModes)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to override the RadEditor dialogs.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to override the RadEditor dialogs; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("IsToOverrideDialogs")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsToOverrideDialogsDescription", Title = "IsToOverrideDialogsCaption")]
    public bool? IsToOverrideDialogs
    {
      get => (bool?) this[nameof (IsToOverrideDialogs)];
      set => this[nameof (IsToOverrideDialogs)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the CSS file that will be added to the RadEditor's content area.
    /// </summary>
    /// <value>The content area CSS file.</value>
    [ConfigurationProperty("ContentAreaCssFile")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorContentAreaCssFileDescription", Title = "EditorContentAreaCssFileCaption")]
    public string ContentAreaCssFile
    {
      get => (string) this[nameof (ContentAreaCssFile)];
      set => this[nameof (ContentAreaCssFile)] = (object) value;
    }

    /// <summary>Gets or sets the CSS class of the RadEditor.</summary>
    /// <value>The editor CSS class.</value>
    [ConfigurationProperty("EditorCssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorCssClassDescription", Title = "EditorCssClassCaption")]
    public string EditorCssClass
    {
      get => (string) this[nameof (EditorCssClass)];
      set => this[nameof (EditorCssClass)] = (object) value;
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
    [ConfigurationProperty("EditorSkin")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorSkinDescription", Title = "EditorSkinCaption")]
    public string EditorSkin
    {
      get => (string) this[nameof (EditorSkin)];
      set => this[nameof (EditorSkin)] = (object) value;
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
    [ConfigurationProperty("EditorContentFilters")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorContentFiltersDescription", Title = "EditorContentFiltersCaption")]
    public EditorFilters? EditorContentFilters
    {
      get => (EditorFilters?) this[nameof (EditorContentFilters)];
      set => this[nameof (EditorContentFilters)] = (object) value;
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
    [ConfigurationProperty("EditorNewLineBr")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorNewLineBrDescription", Title = "EditorNewLineBrCaption")]
    public bool? EditorNewLineBr
    {
      get => (bool?) this[nameof (EditorNewLineBr)];
      set => this[nameof (EditorNewLineBr)] = (object) value;
    }

    /// <summary>
    /// This property is obsolete. Please, use the StripFormattingOptions property instead.
    /// </summary>
    /// <value>
    /// The default value is <strong>EditorStripFormattingOptions.None</strong>.
    /// </value>
    [ConfigurationProperty("EditorStripFormattingOnPaste")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorStripFormattingOnPasteDescription", Title = "EditorStripFormattingOnPasteCaption")]
    public Telerik.Web.UI.EditorStripFormattingOptions? EditorStripFormattingOnPaste
    {
      get => (Telerik.Web.UI.EditorStripFormattingOptions?) this[nameof (EditorStripFormattingOnPaste)];
      set => this[nameof (EditorStripFormattingOnPaste)] = (object) value;
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
    [ConfigurationProperty("EditorStripFormattingOptions")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorStripFormattingOptionsDescription", Title = "EditorStripFormattingOptionsCaption")]
    public Telerik.Web.UI.EditorStripFormattingOptions? EditorStripFormattingOptions
    {
      get => (Telerik.Web.UI.EditorStripFormattingOptions?) this[nameof (EditorStripFormattingOptions)];
      set => this[nameof (EditorStripFormattingOptions)] = (object) value;
    }

    [ConfigurationProperty("EditorToolsConfiguration")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorToolsConfigurationDescription", Title = "EditorToolsConfigurationCaption")]
    public Telerik.Sitefinity.Web.UI.Fields.Enums.EditorToolsConfiguration? EditorToolsConfiguration
    {
      get => (Telerik.Sitefinity.Web.UI.Fields.Enums.EditorToolsConfiguration?) this[nameof (EditorToolsConfiguration)];
      set => this[nameof (EditorToolsConfiguration)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the key for searching the editor custom toolset configuration,
    /// when EditorToolsConfiguration is set to Custom.
    /// </summary>
    [ConfigurationProperty("EditorToolsConfigurationKey")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditorToolsConfigurationKeyDescription", Title = "EditorToolsConfigurationKeyCaption")]
    public string EditorToolsConfigurationKey
    {
      get => (string) this[nameof (EditorToolsConfigurationKey)];
      set => this[nameof (EditorToolsConfigurationKey)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable.
    /// </summary>
    [ConfigurationProperty("IsLocalizable")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsLocalizableDescription", Title = "IsLocalizableCaption")]
    public bool IsLocalizable
    {
      get => (bool) this[nameof (IsLocalizable)];
      set => this[nameof (IsLocalizable)] = (object) value;
    }
  }
}
