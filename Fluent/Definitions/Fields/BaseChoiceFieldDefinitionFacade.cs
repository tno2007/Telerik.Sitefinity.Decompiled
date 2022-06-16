// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.BaseChoiceFieldDefinitionFacade`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  /// <summary>Base Fluent API for wrapping of ChoiceFieldElement</summary>
  /// <typeparam name="TParentFacade">Type of the parent facade</typeparam>
  /// <typeparam name="TActualFacade">Type of the class implementing this abstract class</typeparam>
  /// <typeparam name="TConfig">Type of the configuration element</typeparam>
  public abstract class BaseChoiceFieldDefinitionFacade<TConfig, TActualFacade, TParentFacade> : 
    FieldControlDefinitionFacade<TConfig, TActualFacade, TParentFacade>
    where TConfig : ChoiceFieldElement
    where TActualFacade : class
    where TParentFacade : class
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.BaseChoiceFieldDefinitionFacade`3" /> class.
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
    /// <param name="mode">The mode.</param>
    public BaseChoiceFieldDefinitionFacade(
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
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.BaseChoiceFieldDefinitionFacade`3" /> class.
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
    /// <param name="mode">The mode.</param>
    public BaseChoiceFieldDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElement parentElement,
      string viewName,
      string sectionName,
      TParentFacade parentFacade,
      string fieldName,
      string resourceClassId,
      FieldDisplayMode mode)
      : base(moduleName, definitionName, contentType, parentElement, viewName, sectionName, parentFacade, fieldName, resourceClassId, mode)
    {
    }

    /// <summary>
    /// At most only a single choice item will be selected at a time.
    /// </summary>
    /// <returns>Current facade</returns>
    public TActualFacade MakeMutuallyExclusive()
    {
      this.Field.MutuallyExclusive = true;
      return this as TActualFacade;
    }

    /// <summary>
    /// Make choice items selection inclusive. This is the default behaviour.
    /// </summary>
    /// <returns>Current facade</returns>
    public TActualFacade MakeMutuallyInclusive()
    {
      this.Field.MutuallyExclusive = false;
      return this as TActualFacade;
    }

    /// <summary>Define how the control is rendered</summary>
    /// <param name="renderAs">How the control and its choice items are rendered</param>
    /// <returns>Current facade</returns>
    public TActualFacade SetRenderChoiceAs(RenderChoicesAs renderAs)
    {
      this.Field.RenderChoiceAs = renderAs;
      return this as TActualFacade;
    }

    /// <summary>Hide the title</summary>
    /// <returns>Current facade</returns>
    public TActualFacade HideTitle()
    {
      this.Field.HideTitle = true;
      return this as TActualFacade;
    }

    /// <summary>Show the title. This is the default behaviour</summary>
    /// <returns>Current facade</returns>
    public TActualFacade ShowTitle()
    {
      this.Field.HideTitle = false;
      return this as TActualFacade;
    }

    /// <summary>Adds a choice item to the control</summary>
    /// <param name="text">Text of the choice item</param>
    /// <returns>Child facade for further customization</returns>
    public ChoiceDefinitionFacade<TActualFacade> AddChoice(string text) => new ChoiceDefinitionFacade<TActualFacade>(this as TActualFacade, this.Field.ChoicesConfig, this.Field.ResourceClassId).SetText(text);

    /// <summary>Adds a choice item to the control</summary>
    /// <param name="text">Text of the choice item</param>
    /// <param name="selected">True if the item is going to be selected - false by default</param>
    /// <returns>Child facade for further customization</returns>
    public ChoiceDefinitionFacade<TActualFacade> AddChoice(
      string text,
      bool selected)
    {
      return selected ? this.AddChoice(text).Select() : this.AddChoice(text).Deselect();
    }

    /// <summary>Adds a choice item to the control</summary>
    /// <param name="text">Text of the choice item</param>
    /// <param name="value">Value of the choice item</param>
    /// <returns>Child facade for further customization</returns>
    public ChoiceDefinitionFacade<TActualFacade> AddChoice(
      string text,
      string value)
    {
      return this.AddChoice(text).SetValue(value);
    }

    /// <summary>Adds a choice item to the control</summary>
    /// <param name="text">Text of the choice item</param>
    /// <param name="value">Value of the choice item</param>
    /// <param name="selected">True if the item is going to be selected - false by default</param>
    /// <returns>Child facade for further customization</returns>
    public ChoiceDefinitionFacade<TActualFacade> AddChoice(
      string text,
      string value,
      bool selected)
    {
      return selected ? this.AddChoice(text, value).Select() : this.AddChoice(text, value).Deselect();
    }

    /// <summary>Adds a choice item to the control</summary>
    /// <param name="text">Text of the choice item</param>
    /// <returns>Current facade</returns>
    public TActualFacade AddChoiceAndContinue(string text) => this.AddChoice(text).Done();

    /// <summary>Adds a choice item to the control</summary>
    /// <param name="text">Text of the choice item</param>
    /// <param name="selected">True if the item is going to be selected - false by default</param>
    /// <returns>Current facade</returns>
    public TActualFacade AddChoiceAndContinue(string text, bool selected) => this.AddChoice(text, selected).Done();

    /// <summary>Adds a choice item to the control</summary>
    /// <param name="text">Text of the choice item</param>
    /// <param name="value">Value of the choice item</param>
    /// <returns>Current facade</returns>
    public TActualFacade AddChoiceAndContinue(string text, string value) => this.AddChoice(text, value).Done();

    /// <summary>Adds a choice item to the control</summary>
    /// <param name="text">Text of the choice item</param>
    /// <param name="value">Value of the choice item</param>
    /// <param name="selected">True if the item is going to be selected - false by default</param>
    /// <returns>Current facade</returns>
    public TActualFacade AddChoiceAndContinue(string text, string value, bool selected) => this.AddChoice(text, value, selected).Done();

    /// <summary>Adds a choice item to the control</summary>
    /// <param name="text">Text of the choice item</param>
    /// <param name="value">Value of the choice item</param>
    /// <param name="description">Description/explanation of the choice item</param>
    /// <returns>Current facade</returns>
    public TActualFacade AddChoiceAndContinue(string text, string value, string description) => this.AddChoice(text, value).SetDescription(description).Done();

    /// <summary>Adds a choice item to the control</summary>
    /// <param name="text">Text of the choice item</param>
    /// <param name="value">Value of the choice item</param>
    /// <param name="description">Description/explanation of the choice item</param>
    /// <param name="selected">True if the item is going to be selected - false by default</param>
    /// <returns>Current facade</returns>
    public TActualFacade AddChoiceAndContinue(
      string text,
      string value,
      string description,
      bool selected)
    {
      return this.AddChoice(text, value, selected).SetDescription(description).Done();
    }

    /// <summary>
    /// Add choices for all members of <typeparamref name="TEnum" />
    /// </summary>
    /// <typeparam name="TEnum">Enumeration type whose members to add</typeparam>
    /// <param name="getValueCallback">Function to extract the value of an enum</param>
    /// <returns>Current facade</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// When <typeparamref name="TEnum" /> is not an enum
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="getValueCallback" /> is null</exception>
    public TActualFacade AddChoicesFromEnum<TEnum>(Func<TEnum, int, string> getValueCallback)
    {
      if (getValueCallback == null)
        throw new ArgumentNullException(nameof (getValueCallback));
      if (!typeof (TEnum).IsEnum)
        throw new InvalidOperationException("Generic parameter TEnum must point to an enum");
      Array values = Enum.GetValues(typeof (TEnum));
      for (int index = 0; index < values.Length; ++index)
      {
        TEnum @enum = (TEnum) values.GetValue(index);
        this.AddChoiceAndContinue(@enum.ToString(), getValueCallback(@enum, index));
      }
      return this as TActualFacade;
    }

    /// <summary>
    /// Add choices for all members of <typeparamref name="TEnum" /> as enum member names
    /// </summary>
    /// <typeparam name="TEnum">Enumeration type whose members to add</typeparam>
    /// <returns>Current facade</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// When <typeparamref name="TEnum" /> is not an enum
    /// </exception>
    public TActualFacade AddChoicesFromEnumAsNames<TEnum>()
    {
      this.AddChoicesFromEnum<TEnum>((Func<TEnum, int, string>) ((val, ignored) => val.ToString()));
      return this as TActualFacade;
    }

    /// <summary>
    /// Add choices for all members of <typeparamref name="TEnum" /> as int values
    /// </summary>
    /// <typeparam name="TEnum">Enumeration type whose members to add</typeparam>
    /// <returns>Current facade</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// When <typeparamref name="TEnum" /> is not an enum
    /// </exception>
    public TActualFacade AddChoicesFromEnumAsInts<TEnum>()
    {
      this.AddChoicesFromEnum<TEnum>((Func<TEnum, int, string>) ((val, idx) => Convert.ToDecimal((object) val).ToString()));
      return this as TActualFacade;
    }
  }
}
