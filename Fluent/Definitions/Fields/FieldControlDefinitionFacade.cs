// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.FieldControlDefinitionFacade`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions.Validation;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  /// <summary>
  /// Base fluent API facade that defines a definition for field control element
  /// </summary>
  /// <typeparam name="TElement">The type of the element.</typeparam>
  /// <typeparam name="TActualFacade">The type of the actual facade.</typeparam>
  /// <typeparam name="TParentFacade">The type of the section parent facade.</typeparam>
  public abstract class FieldControlDefinitionFacade<TElement, TActualFacade, TParentFacade> : 
    FieldDefinitionFacade<TElement, TActualFacade, TParentFacade>
    where TElement : FieldControlDefinitionElement
    where TActualFacade : class
    where TParentFacade : class
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:FieldDefinitionFacade&lt;TElement, TParentFacade&gt;" /> class.
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
    public FieldControlDefinitionFacade(
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
      : base(moduleName, definitionName, contentType, parentElement, viewName, sectionName, parentFacade, fieldName, resourceClassId)
    {
      this.Field.DisplayMode = new FieldDisplayMode?(mode);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="singleParentElement">The single parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="sectionName">Name of the section.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="resourceClassId">The resource class id.</param>
    /// <param name="addAction">Delegate that will add this element to the parent item's element.</param>
    /// <param name="mode">The field display mode.</param>
    public FieldControlDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElement singleParentElement,
      string viewName,
      string sectionName,
      TParentFacade parentFacade,
      string fieldName,
      string resourceClassId,
      FieldDisplayMode mode)
      : base(moduleName, definitionName, contentType, singleParentElement, viewName, sectionName, parentFacade, fieldName, resourceClassId)
    {
      this.Field.DisplayMode = new FieldDisplayMode?(mode);
    }

    /// <summary>
    /// Sets the programmatic identifier assigned to the field control.
    /// </summary>
    /// <param name="id">The programmatic identifier assigned to the field control.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldControlDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetId(string id)
    {
      this.Field.ID = !string.IsNullOrEmpty(id) ? id : throw new ArgumentNullException(nameof (id));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the name of the data item property the control will be bound to.
    /// </summary>
    /// <param name="dataFieldName">Name of the data field.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldControlDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetDataFieldName(string dataFieldName)
    {
      this.Field.DataFieldName = !string.IsNullOrEmpty(dataFieldName) ? dataFieldName : throw new ArgumentNullException(nameof (dataFieldName));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the name of the data item property the control will be bound to if the property type is Lstring
    /// </summary>
    /// <param name="dataFieldName">Name of the Lstring property</param>
    /// <returns>Current facade</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="dataFieldName" /> is null or empty</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="" /> has dot in its name</exception>
    /// <exception cref="T:System.NotSupportedException">When this facade has its content type set to null</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <para>When <paramref name="dataFieldName" /> does not represent a real property that <c>TypeDescriptor</c> can find</para>
    /// <para>or</para>
    /// <para>When the property identified by <paramref name="dataFieldName" /> is not of type <c>Lstring</c></para>
    /// </exception>
    public TActualFacade SetLocalizableDataFieldName(string dataFieldName)
    {
      if (string.IsNullOrEmpty(dataFieldName))
        throw new ArgumentNullException("dataFieldname");
      if (dataFieldName.Contains("."))
        throw new ArgumentException("dataFieldName should not contain dots (.)");
      PropertyDescriptor propertyDescriptor = !(this.contentType == (Type) null) ? TypeDescriptor.GetProperties(this.contentType).Find(dataFieldName, false) : throw new NotSupportedException("Cannot set data field name if content type is null.");
      if (propertyDescriptor == null)
        throw new InvalidOperationException("Type {0} does not have property {1}".Arrange((object) this.contentType, (object) dataFieldName));
      if (propertyDescriptor.PropertyType != typeof (Lstring))
        throw new InvalidOperationException("Property {0} is not an Lstring".Arrange((object) dataFieldName));
      string str = dataFieldName;
      FieldDisplayMode? displayMode = this.Field.DisplayMode;
      FieldDisplayMode fieldDisplayMode = FieldDisplayMode.Write;
      if (displayMode.GetValueOrDefault() == fieldDisplayMode & displayMode.HasValue)
        str = dataFieldName + ".PersistedValue";
      this.Field.DataFieldName = str;
      return this as TActualFacade;
    }

    /// <summary>Sets the value of the property.</summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldControlDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetValue(object value)
    {
      this.Field.Value = value != null ? value : throw new ArgumentNullException(nameof (value));
      return this as TActualFacade;
    }

    /// <summary>Sets the display mode of the control.</summary>
    /// <param name="mode">The display mode.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldControlDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetDisplayMode(FieldDisplayMode mode)
    {
      this.Field.DisplayMode = new FieldDisplayMode?(mode);
      return this as TActualFacade;
    }

    /// <summary>Adds a validation mechanism to the field control.</summary>
    public ValidatorDefinitionFacade<TActualFacade> AddValidation() => new ValidatorDefinitionFacade<TActualFacade>((FieldControlDefinitionElement) this.Field, this as TActualFacade, this.Field.ResourceClassId);

    /// <summary>Sets the tag that will be rendered as a wrapper.</summary>
    /// <param name="tag">The wrapper tag.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldControlDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetWrapperTag(HtmlTextWriterTag tag)
    {
      this.Field.WrapperTag = tag;
      return this as TActualFacade;
    }
  }
}
