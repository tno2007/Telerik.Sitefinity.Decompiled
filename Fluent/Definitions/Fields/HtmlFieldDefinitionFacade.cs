// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.HtmlFieldDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  /// <summary>
  /// Fluent API for wrapping <c>HtmlFieldElement</c>
  /// </summary>
  /// <typeparam name="TParentFacade">Type of the parent (host) facade</typeparam>
  public class HtmlFieldDefinitionFacade<TParentFacade> : 
    FieldControlDefinitionFacade<HtmlFieldElement, HtmlFieldDefinitionFacade<TParentFacade>, TParentFacade>
    where TParentFacade : class
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.HtmlFieldDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="sectionName">Name of the section.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="fieldName">Configuration key.</param>
    /// <param name="resourceClassId">The resource class id.</param>
    /// <param name="mode">The mode.</param>
    public HtmlFieldDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement,
      string viewName,
      string sectionName,
      TParentFacade parentFacade,
      string fieldName,
      string resourceClassId,
      FieldDisplayMode mode)
      : base(moduleName, definitionName, contentType, parentElement, viewName, sectionName, parentFacade, fieldName, resourceClassId, mode)
    {
      this.Field.CssClass = "sfFormSeparator sfContentField";
      this.Field.WrapperTag = HtmlTextWriterTag.Li;
    }

    /// <summary>
    /// Create an instance of the config element. Do not add it to the parent collection.
    /// </summary>
    /// <param name="parentElement">Parent element</param>
    /// <returns>New instance of the parent config element</returns>
    protected override HtmlFieldElement CreateConfig(
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement)
    {
      return new HtmlFieldElement((ConfigElement) parentElement);
    }

    /// <summary>
    /// Use this method to disable overriding RadEditor dialogs
    /// </summary>
    /// <returns>Current facade</returns>
    public HtmlFieldDefinitionFacade<TParentFacade> DoNotOverrideRadEditorDialogs()
    {
      this.Field.IsToOverrideDialogs = new bool?(false);
      return this;
    }

    /// <summary>
    /// Use this method to enable overriding RadEditor dialogs
    /// </summary>
    /// <returns>Current facade</returns>
    public HtmlFieldDefinitionFacade<TParentFacade> DoOverrideRadEditorDialogs()
    {
      this.Field.IsToOverrideDialogs = new bool?(true);
      return this;
    }

    /// <summary>Set visible editing modes for the wrapped RadEditor</summary>
    /// <param name="bitwizeOrOfModes">Bitwize 'or' combination of html edit modes</param>
    /// <returns>Current facade</returns>
    public HtmlFieldDefinitionFacade<TParentFacade> SetEditModes(
      HtmlEditModes bitwizeOrOfModes)
    {
      this.Field.HtmlFieldEditModes = new HtmlEditModes?(bitwizeOrOfModes);
      return this;
    }

    /// <summary>
    /// Sets the CSS file that will be added to the RadEditor's content area
    /// </summary>
    /// <param name="file">CSS file that will be added to the RadEditor's content area</param>
    /// <returns>Current facade</returns>
    public HtmlFieldDefinitionFacade<TParentFacade> SetContentAreaCssFile(
      string file)
    {
      this.Field.ContentAreaCssFile = file;
      return this;
    }

    /// <summary>Sets the CSS class of the RadEditor</summary>
    /// <param name="cssClass">The RadEditor CSS class</param>
    /// <returns>Current facade</returns>
    public HtmlFieldDefinitionFacade<TParentFacade> SetEditorCssClass(
      string cssClass)
    {
      this.Field.EditorCssClass = cssClass;
      return this;
    }

    /// <summary>
    /// Sets the skin name for the RadEditor control user interface
    /// </summary>
    /// <param name="radEditorSkinName">Name of the RadEditor skin</param>
    /// <returns>Current facade</returns>
    public HtmlFieldDefinitionFacade<TParentFacade> SetEditorSkin(
      string radEditorSkinName)
    {
      this.Field.EditorSkin = radEditorSkinName;
      return this;
    }

    /// <summary>
    /// Sets a value indicating which content filters will be active when the
    /// RadEditor is loaded in the browser
    /// </summary>
    /// <param name="bitwizeOrOfFilters">Biwize 'or' combination of editor filters</param>
    /// <returns>Current facade</returns>
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
    public HtmlFieldDefinitionFacade<TParentFacade> SetContentFilters(
      EditorFilters bitwizeOrOfFilters)
    {
      this.Field.EditorContentFilters = new EditorFilters?(bitwizeOrOfFilters);
      return this;
    }

    /// <summary>
    /// Insert &lt;p&gt; tags instead of &lt;br&gt; on [Enter] when rendering in Internet Explorer
    /// </summary>
    /// <returns>Current facade</returns>
    /// <remarks>
    /// Sets <c>EditorNewLineBr</c> to false in RadEditor. This has effect only in IE.
    /// By default, &lt;br&gt; tags are inserted in both gecko and IE browsers.
    /// </remarks>
    public HtmlFieldDefinitionFacade<TParentFacade> InsertPTagOnEnterInIE()
    {
      this.Field.EditorNewLineBr = new bool?(false);
      return this;
    }

    /// <summary>
    /// Sets the value indicating how the RadEditor should clear the HTML formatting
    /// when the user pastes data into the content area.
    /// </summary>
    /// <param name="bitwizeOrOfOptions">Bitwize 'or' combination of strip formatting options</param>
    /// <returns>Current facade</returns>
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
    public HtmlFieldDefinitionFacade<TParentFacade> SetStripFormattingOptions(
      EditorStripFormattingOptions bitwizeOrOfOptions)
    {
      this.Field.EditorStripFormattingOptions = new EditorStripFormattingOptions?(bitwizeOrOfOptions);
      return this;
    }
  }
}
