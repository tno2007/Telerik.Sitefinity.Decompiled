// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.BaseDetailDefinitionFacade`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Base fluent API facade that defines a definition for content view detail element.
  /// </summary>
  /// <typeparam name="TElement">The type of the element.</typeparam>
  /// <typeparam name="TActualFacade">The type of the actual facade.</typeparam>
  public abstract class BaseDetailDefinitionFacade<TElement, TActualFacade> : 
    ContentViewDefinitionFacade<TElement, TActualFacade>
    where TElement : ContentViewDetailElement
    where TActualFacade : BaseDetailDefinitionFacade<TElement, TActualFacade>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseDetailDefinitionFacade`2" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public BaseDetailDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementDictionary<string, ContentViewDefinitionElement> parentElement,
      string viewName,
      ContentViewControlDefinitionFacade parentFacade)
      : base(moduleName, definitionName, contentType, parentElement, viewName, parentFacade)
    {
    }

    /// <summary>
    /// Sets the name of the tag in which the view should be wrapped.
    /// </summary>
    /// <param name="tagName">Name of the tag.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseDetailDefinitionFacade`2" />.</returns>
    public TActualFacade SetWrapperTagName(string tagName)
    {
      this.ContentView.WrapperTagName = !string.IsNullOrEmpty(tagName) ? tagName : throw new ArgumentNullException(nameof (tagName));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the CSS class that should be applied to the wrapper tag.
    /// </summary>
    /// <param name="cssClass">The CSS class.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseDetailDefinitionFacade`2" />.</returns>
    public TActualFacade SetWrapperCssClass(string cssClass)
    {
      this.ContentView.WrapperCssClass = !string.IsNullOrEmpty(cssClass) ? cssClass : throw new ArgumentNullException(nameof (cssClass));
      return this as TActualFacade;
    }

    /// <summary>Sets the CSS class for all fields.</summary>
    /// <param name="cssClass">The CSS class.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseDetailDefinitionFacade`2" />.</returns>
    public TActualFacade SetFieldCssClass(string cssClass)
    {
      this.ContentView.FieldCssClass = !string.IsNullOrEmpty(cssClass) ? cssClass : throw new ArgumentNullException(nameof (cssClass));
      return this as TActualFacade;
    }

    /// <summary>Set the the CSS class for all sections.</summary>
    /// <param name="cssClass">The CSS class.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseDetailDefinitionFacade`2" />.</returns>
    public TActualFacade SetSectionCssClass(string cssClass)
    {
      this.ContentView.SectionCssClass = !string.IsNullOrEmpty(cssClass) ? cssClass : throw new ArgumentNullException(nameof (cssClass));
      return this as TActualFacade;
    }

    /// <summary>Shows sections.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseDetailDefinitionFacade`2" />.</returns>
    public TActualFacade ShowSections()
    {
      this.ContentView.ShowSections = new bool?(true);
      return this as TActualFacade;
    }

    /// <summary>Hides sections.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseDetailDefinitionFacade`2" />.</returns>
    public TActualFacade HideSections()
    {
      this.ContentView.ShowSections = new bool?(false);
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the ID of the page that should display the master view.
    /// If this property is not set the current page is assumed.
    /// </summary>
    /// <param name="id">The master page id.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseDetailDefinitionFacade`2" />.</returns>
    public TActualFacade SetMasterPageId(Guid id)
    {
      this.ContentView.MasterPageId = !(id == Guid.Empty) ? id : throw new ArgumentException("id cannot be empty guid.");
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the data ID of the content item that should be displayed.
    /// </summary>
    /// <param name="id">The data item id.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseDetailDefinitionFacade`2" />.</returns>
    public TActualFacade SetDataItemId(Guid id)
    {
      this.ContentView.DataItemId = !(id == Guid.Empty) ? id : throw new ArgumentException(nameof (id));
      return this as TActualFacade;
    }

    /// <summary>Adds the section.</summary>
    /// <returns></returns>
    public SectionDefinitionFacade<TActualFacade> AddSection() => this.AddSection(Guid.NewGuid().ToString().Replace("-", "_"));

    /// <summary>Adds the section.</summary>
    /// <param name="name">The section name.</param>
    /// <returns></returns>
    public SectionDefinitionFacade<TActualFacade> AddSection(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(nameof (name));
      return new SectionDefinitionFacade<TActualFacade>(this.moduleName, this.definitionName, this.contentType, this.ContentView.Sections, this.viewName, this as TActualFacade, name, this.ContentView.ResourceClassId, this.ContentView.DisplayMode);
    }

    /// <summary>Adds the read only section.</summary>
    /// <param name="name">The section name.</param>
    /// <returns></returns>
    public SectionDefinitionFacade<TActualFacade> AddReadOnlySection(
      string name)
    {
      return this.AddSection(name).SetCssClass("sfItemReadOnlyInfo");
    }

    /// <summary>Adds header section.</summary>
    /// <param name="name">The section name.</param>
    /// <returns></returns>
    public SectionDefinitionFacade<TActualFacade> AddHeaderSection(
      string name)
    {
      return this.AddSection(name).SetCssClass("sfItemHeaderSection");
    }

    /// <summary>Adds the first section.</summary>
    /// <param name="name">The section name.</param>
    /// <returns></returns>
    public SectionDefinitionFacade<TActualFacade> AddFirstSection(string name) => this.AddSection(name).SetCssClass("sfFirstForm");

    /// <summary>Adds the expandable section.</summary>
    /// <param name="name">The section name.</param>
    /// <returns></returns>
    public SectionDefinitionFacade<TActualFacade> AddExpandableSection(
      string name)
    {
      return this.AddSection(name).SetCssClass("sfExpandableForm").AddExpandableBehavior().Collapse().Done();
    }
  }
}
