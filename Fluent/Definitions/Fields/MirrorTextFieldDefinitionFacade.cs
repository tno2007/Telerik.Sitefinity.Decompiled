// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  /// <summary>
  /// Fluent API facade that defines a definition for mirror text field element.
  /// </summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  public class MirrorTextFieldDefinitionFacade<TParentFacade> : 
    BaseTextFieldDefinitionFacade<MirrorTextFieldElement, MirrorTextFieldDefinitionFacade<TParentFacade>, TParentFacade>
    where TParentFacade : class
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" /> class.
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
    public MirrorTextFieldDefinitionFacade(
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
      this.Field.propertyResolver = (PropertyResolverBase) new MirrorTextFieldPropertyResolver();
    }

    /// <summary>
    /// Create an instance of the config element. Do not add it to the parent collection.
    /// </summary>
    /// <param name="parentElement">Parent element</param>
    /// <returns>New instance of the parent config element</returns>
    protected override MirrorTextFieldElement CreateConfig(
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement)
    {
      return new MirrorTextFieldElement((ConfigElement) parentElement);
    }

    /// <summary>
    /// Sets the regular expression for filtering the value of the mirror text field.
    /// </summary>
    /// <param name="filter">The regular expression filter.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" />.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TParentFacade> SetRegularExpressionFilter(
      string filter)
    {
      this.Field.RegularExpressionFilter = !string.IsNullOrEmpty(filter) ? filter : throw new ArgumentNullException(nameof (filter));
      return this;
    }

    /// <summary>
    /// Sets the value that will be replaced for every Regular expression filter match.
    /// </summary>
    /// <param name="value">The value to replace with.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" />.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TParentFacade> SetReplaceWithValue(
      string value)
    {
      this.Field.ReplaceWith = value != null ? value : throw new ArgumentNullException(nameof (value));
      return this;
    }

    /// <summary>Sets the id of the mirrored control.</summary>
    /// <param name="id">The mirrored control id.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" />.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TParentFacade> SetMirroredControlId(
      string id)
    {
      this.Field.MirroredControlId = !string.IsNullOrEmpty(id) ? id : throw new ArgumentNullException(nameof (id));
      return this;
    }

    /// <summary>
    /// Shows a button that must be clicked in order to change the value of the control.
    /// </summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" />.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TParentFacade> EnableChangeButton()
    {
      this.Field.EnableChangeButton = true;
      return this;
    }

    /// <summary>
    /// Hides a button that must be clicked in order to change the value of the control.
    /// </summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" />.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TParentFacade> DisableChangeButton()
    {
      this.Field.EnableChangeButton = false;
      return this;
    }

    /// <summary>Sets the text of the change button.</summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" />.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TParentFacade> SetChangeButtonText(
      string text)
    {
      this.Field.ChangeButtonText = !string.IsNullOrEmpty(text) ? text : throw new ArgumentNullException(nameof (text));
      return this;
    }

    /// <summary>Lowers the value of the control.</summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" />.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TParentFacade> ToLower()
    {
      this.Field.ToLower = new bool?(true);
      return this;
    }

    /// <summary>Uppers the value of the control.</summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" />.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TParentFacade> ToUpper()
    {
      this.Field.ToLower = new bool?(false);
      return this;
    }

    /// <summary>Trims the value of this control.</summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" />.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TParentFacade> Trim()
    {
      this.Field.Trim = new bool?(true);
      return this;
    }

    /// <summary>Does not trim the value of this control.</summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" />.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TParentFacade> DoNotTrim()
    {
      this.Field.Trim = new bool?(false);
      return this;
    }

    /// <summary>
    /// Sets the prefix that will be appended to the mirrored value.
    /// </summary>
    /// <param name="text">The text that will be appended to the mirrored text.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.MirrorTextFieldDefinitionFacade`1" />.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TParentFacade> SetPrefixText(
      string text)
    {
      this.Field.PrefixText = text != null ? text : throw new ArgumentNullException(nameof (text));
      return this;
    }
  }
}
