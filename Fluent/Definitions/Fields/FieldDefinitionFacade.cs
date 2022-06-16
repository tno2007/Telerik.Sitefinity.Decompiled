// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  /// <summary>
  /// Base fluent API facade that defines a definition for field element
  /// </summary>
  /// <typeparam name="TElement">The type of the element.</typeparam>
  /// <typeparam name="TActualFacade">The type of the actual facade.</typeparam>
  /// <typeparam name="TParentFacade">The type of the section parent facade.</typeparam>
  public abstract class FieldDefinitionFacade<TElement, TActualFacade, TParentFacade>
    where TElement : FieldDefinitionElement
    where TActualFacade : class
    where TParentFacade : class
  {
    protected string moduleName;
    protected string definitionName;
    protected Type contentType;
    protected string viewName;
    protected string sectionName;
    private TParentFacade parentFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" /> class.
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
    public FieldDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement,
      string viewName,
      string sectionName,
      TParentFacade parentFacade,
      string fieldName,
      string resourceClassId)
    {
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (string.IsNullOrEmpty(definitionName))
        throw new ArgumentNullException(nameof (definitionName));
      if (parentElement == null)
        throw new ArgumentNullException(nameof (parentElement));
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentNullException(nameof (viewName));
      if (string.IsNullOrEmpty(sectionName))
        throw new ArgumentNullException(nameof (sectionName));
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      this.moduleName = moduleName;
      this.definitionName = definitionName;
      this.contentType = contentType;
      this.viewName = viewName;
      this.parentFacade = parentFacade;
      this.sectionName = sectionName;
      this.Field = this.CreateConfig(parentElement);
      this.Field.FieldName = fieldName;
      this.Field.ResourceClassId = resourceClassId;
      parentElement.Add((FieldDefinitionElement) this.Field);
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
    public FieldDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElement singleParentElement,
      string viewName,
      string sectionName,
      TParentFacade parentFacade,
      string fieldName,
      string resourceClassId)
    {
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (string.IsNullOrEmpty(definitionName))
        throw new ArgumentNullException(nameof (definitionName));
      if (singleParentElement == null)
        throw new ArgumentNullException("parentElement");
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentNullException(nameof (viewName));
      if (string.IsNullOrEmpty(sectionName))
        throw new ArgumentNullException(nameof (sectionName));
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      this.moduleName = moduleName;
      this.definitionName = definitionName;
      this.contentType = contentType;
      this.viewName = viewName;
      this.parentFacade = parentFacade;
      this.sectionName = sectionName;
      this.Field = this.CreateConfigWithSingleParent(singleParentElement);
      this.Field.FieldName = fieldName;
      this.Field.ResourceClassId = resourceClassId;
    }

    /// <summary>
    /// Create an instance of the config element. Do not add it to the parent collection.
    /// </summary>
    /// <param name="parentElement">Parent element</param>
    /// <returns>New instance of the parent config element</returns>
    protected abstract TElement CreateConfig(
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement);

    /// <summary>Create an instance of the config element.</summary>
    /// <param name="parentElement">Parent config element (not a collection)</param>
    /// <returns>New instance of the config element</returns>
    /// <exception cref="T:System.NotSupportedException">Unless overridden in a child facade</exception>
    protected virtual TElement CreateConfigWithSingleParent(ConfigElement parentElement) => throw new NotSupportedException();

    /// <summary>Gets or sets the current field element.</summary>
    /// <value>The field element.</value>
    protected TElement Field { get; set; }

    /// <summary>
    /// Gets this <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.FieldDefinitionElement" /> instance.
    /// </summary>
    /// <returns></returns>
    public TElement Get() => this.Field;

    /// <summary>Sets the title of the field element.</summary>
    /// <param name="title">The title.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetTitle(string title)
    {
      this.Field.Title = title;
      return this as TActualFacade;
    }

    /// <summary>Sets the description of the field element.</summary>
    /// <param name="description">The description.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetDescription(string description)
    {
      this.Field.Description = !string.IsNullOrEmpty(description) ? description : throw new ArgumentNullException(nameof (description));
      return this as TActualFacade;
    }

    /// <summary>Sets the example of the field element.</summary>
    /// <param name="example">The example.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetExample(string example)
    {
      this.Field.Example = !string.IsNullOrEmpty(example) ? example : throw new ArgumentNullException(nameof (example));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the type of the control that represents the field.
    /// </summary>
    /// <remarks>
    /// Used for fields that are represented by custom controls.
    /// </remarks>
    /// <param name="cssClass">The title</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.</returns>
    public TActualFacade SetFieldType(Type type)
    {
      this.Field.FieldType = !(type == (Type) null) ? type : throw new ArgumentNullException(nameof (type));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the type of the control that represents the field
    /// </summary>
    /// <typeparam name="TFieldType">Type of the field</typeparam>
    /// <returns>Current facade</returns>
    public virtual TActualFacade SetFieldType<TFieldType>() where TFieldType : FieldControl
    {
      this.Field.FieldType = typeof (TFieldType);
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the the virtual path of the user control that represents the field.
    /// </summary>
    /// <remarks>
    /// Used for fields that are represented by user controls.
    /// </remarks>
    /// <param name="path">The virtual path.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.
    /// </returns>
    public TActualFacade SetFieldVirtualPath(string path)
    {
      this.Field.FieldVirtualPath = !string.IsNullOrEmpty(path) ? path : throw new ArgumentNullException(nameof (path));
      return this as TActualFacade;
    }

    /// <summary>Sets the css class of the field control.</summary>
    /// <param name="cssClass">The css class.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.</returns>
    public TActualFacade SetCssClass(string cssClass)
    {
      this.Field.CssClass = cssClass != null ? cssClass : throw new ArgumentNullException(nameof (cssClass));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the localization class for the field that should be used
    /// for localizing the properties. When this method is called, all properties
    /// will start to behave as resource keys.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// The type of the class which should be used to localize the string properties.
    /// </typeparam>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.</returns>
    public virtual TActualFacade LocalizeUsing<TResourceClass>() where TResourceClass : Resource
    {
      this.Field.ResourceClassId = typeof (TResourceClass).Name;
      return this as TActualFacade;
    }

    /// <summary>Hides the field element.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.</returns>
    public TActualFacade Hide()
    {
      this.Field.Hidden = new bool?(true);
      return this as TActualFacade;
    }

    /// <summary>Displays the field element.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.FieldDefinitionFacade`3" />.</returns>
    public TActualFacade Display()
    {
      this.Field.Hidden = new bool?(false);
      return this as TActualFacade;
    }

    /// <summary>Returns to parent facade.</summary>
    /// <returns>The parent facade which initialized this facade.</returns>
    public TParentFacade Done() => this.parentFacade;
  }
}
