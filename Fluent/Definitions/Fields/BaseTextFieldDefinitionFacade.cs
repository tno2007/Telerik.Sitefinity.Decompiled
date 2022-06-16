// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.BaseTextFieldDefinitionFacade`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  /// <summary>
  /// Base fluent API facade that defines a definition for text field element.
  /// </summary>
  /// <typeparam name="TElement">The type of the element.</typeparam>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  /// <typeparam name="TActualFacade">The type of the actual facade.</typeparam>
  public abstract class BaseTextFieldDefinitionFacade<TElement, TActualFacade, TParentFacade> : 
    FieldControlDefinitionFacade<TElement, TActualFacade, TParentFacade>
    where TElement : TextFieldDefinitionElement
    where TActualFacade : class
    where TParentFacade : class
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.BaseTextFieldDefinitionFacade`3" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="sectionName">Name of the section.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="resourceClassId">The resource class id.</param>
    /// <param name="mode">The field display mode.</param>
    public BaseTextFieldDefinitionFacade(
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
      this.Field.WrapperTag = HtmlTextWriterTag.Li;
    }

    /// <summary>
    /// Sets the number of rows displayed in a multiline text box.
    /// </summary>
    /// <param name="number">The number of rows.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.BaseTextFieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetRows(int rows)
    {
      this.Field.Rows = rows;
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the value which compared with the actual value of the Text Field, if equal hides the text.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.BaseTextFieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetHideIfValue(string value)
    {
      this.Field.HideIfValue = !string.IsNullOrEmpty(value) ? value : throw new ArgumentNullException(nameof (value));
      return this as TActualFacade;
    }

    /// <summary>The text field will be used as password field.</summary>
    /// <param name="number">The number of rows.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.BaseTextFieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade UsePasswordMode()
    {
      this.Field.IsPasswordMode = new bool?(true);
      return this as TActualFacade;
    }

    /// <summary>The text field will not be used as password field.</summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.BaseTextFieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade DoNotUsePasswordMode()
    {
      this.Field.IsPasswordMode = new bool?(false);
      return this as TActualFacade;
    }

    /// <summary>Recommended characters count.</summary>
    /// <param name="count">The count.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.BaseTextFieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade RecommendedCharactersCount(int count)
    {
      this.Field.RecommendedCharactersCount = count;
      return this as TActualFacade;
    }

    /// <summary>Sets the character counter description.</summary>
    /// <param name="characterCounterDescription">The character counter description.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.BaseTextFieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetCharacterCounterDescription(string characterCounterDescription)
    {
      this.Field.CharacterCounterDescription = Res.ResolveLocalizedValue(this.Field.ResourceClassId, characterCounterDescription);
      return this as TActualFacade;
    }

    /// <summary>Trims the spaces.</summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.BaseTextFieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade TrimSpaces()
    {
      this.Field.TrimSpaces = true;
      return this as TActualFacade;
    }

    /// <summary>Shows the character counter.</summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.BaseTextFieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade ShowCharacterCounter()
    {
      this.Field.ShowCharacterCounter = true;
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the object that defines the expandable behavior of the field control.
    /// </summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ExpandableControlDefinitionFacade`2" />.
    /// </returns>
    public ExpandableControlDefinitionFacade<TActualFacade, TElement> AddExpandableBehavior()
    {
      ExpandableControlDefinitionFacade<TActualFacade, TElement> definitionFacade = new ExpandableControlDefinitionFacade<TActualFacade, TElement>(this.moduleName, this.definitionName, this.contentType, this.Field, this.viewName, this as TActualFacade, this.Field.ResourceClassId);
      this.Field.ExpandableDefinitionConfig = definitionFacade.Get();
      return definitionFacade;
    }

    /// <summary>
    ///  Sets the object that defines the expandable behavior of the field control. By default it is expanded
    /// </summary>
    /// <returns>Current facade</returns>
    public TActualFacade AddExpandableBehaviorAndContinue() => this.AddExpandableBehavior().Done();

    /// <summary>Sets the tooltip visibility.</summary>
    /// <param name="visible">If <c>true</c> show a tooltip.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.</returns>
    public TActualFacade SetToolTipVisible(bool visible = true)
    {
      this.Field.ToolTipVisible = visible;
      return this as TActualFacade;
    }

    /// <summary>Sets the text of the tooltip target.</summary>
    /// <param name="text">The text of the tooltip target.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.</returns>
    public TActualFacade SetToolTipText(string text)
    {
      this.Field.ToolTipText = text;
      return this as TActualFacade;
    }

    /// <summary>Sets the title of the tooltip.</summary>
    /// <param name="title">The title of the tooltip.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.</returns>
    public TActualFacade SetToolTipTitle(string title)
    {
      this.Field.ToolTipTitle = title;
      return this as TActualFacade;
    }

    /// <summary>Sets the content of the tooltip.</summary>
    /// <param name="content">The content of the tooltip.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.</returns>
    public TActualFacade SetToolTipContent(string content)
    {
      this.Field.ToolTipContent = content;
      return this as TActualFacade;
    }

    /// <summary>Sets the css class of the tooltip.</summary>
    /// <param name="cssClass">The css class of the tooltip.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.</returns>
    public TActualFacade SetToolTipCssClass(string cssClass)
    {
      this.Field.ToolTipCssClass = cssClass;
      return this as TActualFacade;
    }

    /// <summary>Sets the css class of the tooltip target.</summary>
    /// <param name="targetCssClass">The css class of the tooltip target.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.</returns>
    public TActualFacade SetToolTipTargetCssClass(string targetCssClass)
    {
      this.Field.ToolTipTargetCssClass = targetCssClass;
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the read only replacement value of the control. The specified value will be replaced on client.
    /// </summary>
    /// <param name="readOnlyReplacementValue">The read only replacement value.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldControlDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetReadOnlyReplacement(string readOnlyReplacement)
    {
      this.Field.ReadOnlyReplacement = readOnlyReplacement;
      return this as TActualFacade;
    }

    /// <summary>Sets the unit value of the control.</summary>
    /// <param name="unit">The unit value.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldControlDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetUnit(string unit)
    {
      this.Field.Unit = unit;
      return this as TActualFacade;
    }
  }
}
