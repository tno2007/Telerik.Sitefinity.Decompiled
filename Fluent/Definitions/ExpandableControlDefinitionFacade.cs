// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.ExpandableControlDefinitionFacade`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Extenders.Config;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Fluent API facade that defines a definition for expandable control.
  /// </summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  public class ExpandableControlDefinitionFacade<TParentFacade, TParentElement>
    where TParentFacade : class
    where TParentElement : ConfigElement
  {
    private ExpandableControlElement expandableControl;
    private TParentFacade parentFacade;
    private string moduleName;
    private string definitionName;
    private Type contentType;
    private TParentElement parentElement;
    private string viewName;
    private string resourceClassId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ExpandableControlDefinitionFacade`2" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="resourceClassId">The resource class id.</param>
    public ExpandableControlDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      TParentElement parentElement,
      string viewName,
      TParentFacade parentFacade,
      string resourceClassId)
    {
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (string.IsNullOrEmpty(definitionName))
        throw new ArgumentNullException(nameof (definitionName));
      if ((object) parentElement == null)
        throw new ArgumentNullException(nameof (parentElement));
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentNullException(nameof (viewName));
      this.moduleName = moduleName;
      this.definitionName = definitionName;
      this.contentType = contentType;
      this.parentElement = parentElement;
      this.viewName = viewName;
      this.parentFacade = parentFacade;
      this.resourceClassId = resourceClassId;
      this.expandableControl = new ExpandableControlElement((ConfigElement) parentElement)
      {
        ResourceClassId = resourceClassId
      };
    }

    /// <summary>
    /// Gets this <see cref="T:Telerik.Sitefinity.Web.UI.Extenders.Config.ExpandableControlElement" /> instance.
    /// </summary>
    /// <returns></returns>
    public ExpandableControlElement Get() => this.expandableControl;

    /// <summary>
    /// Sets the text to be displayed on the element that expands the control.
    /// </summary>
    /// <param name="text">The expand text.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ExpandableControlDefinitionFacade`2" />.</returns>
    public ExpandableControlDefinitionFacade<TParentFacade, TParentElement> SetExpandText(
      string text)
    {
      this.expandableControl.ExpandText = !string.IsNullOrEmpty(text) ? text : throw new ArgumentNullException(nameof (text));
      return this;
    }

    /// <summary>Expands the control.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ExpandableControlDefinitionFacade`2" />.</returns>
    public ExpandableControlDefinitionFacade<TParentFacade, TParentElement> Expand()
    {
      this.expandableControl.Expanded = new bool?(true);
      return this;
    }

    /// <summary>Collapses the control.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ExpandableControlDefinitionFacade`2" />.</returns>
    public ExpandableControlDefinitionFacade<TParentFacade, TParentElement> Collapse()
    {
      this.expandableControl.Expanded = new bool?(false);
      return this;
    }

    /// <summary>
    /// Sets the localization class for the expandable control that should be used
    /// for localizing the properties. When this method is called, all properties
    /// will start to behave as resource keys.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// The type of the class which should be used to localize the string properties.
    /// </typeparam>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ExpandableControlDefinitionFacade`2" />.</returns>
    public ExpandableControlDefinitionFacade<TParentFacade, TParentElement> LocalizeUsing<TResourceClass>() where TResourceClass : Resource
    {
      this.expandableControl.ResourceClassId = typeof (TResourceClass).Name;
      return this;
    }

    /// <summary>Returns to parent facade.</summary>
    /// <returns>The parent facade which initialized this facade.</returns>
    public TParentFacade Done() => this.parentFacade;
  }
}
