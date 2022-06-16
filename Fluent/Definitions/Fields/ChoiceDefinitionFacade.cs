// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.ChoiceDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  /// <summary>Fluent API wrapping ChoiceElement</summary>
  public class ChoiceDefinitionFacade<TParentFacade> where TParentFacade : class
  {
    private ChoiceElement element;
    private TParentFacade parentFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ChoiceDefinitionFacade" /> class.
    /// </summary>
    /// <param name="parentFacade">The parent facade. Cannot be null.</param>
    /// <param name="parentElement">The parent config element. Cannot be null.</param>
    /// <param name="resourceClassId">The resource class id. Set to null to use plain strings</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// When either <paramref name="parentElement" /> or <paramref name="parentFacade" /> is null.
    /// </exception>
    public ChoiceDefinitionFacade(
      TParentFacade parentFacade,
      ConfigElementList<ChoiceElement> parentElement,
      string resourceClassId)
    {
      if (parentElement == null)
        throw new ArgumentNullException(nameof (parentElement));
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      this.element = new ChoiceElement((ConfigElement) parentElement)
      {
        ResourceClassId = resourceClassId
      };
      parentElement.Add(this.element);
      this.parentFacade = parentFacade;
    }

    /// <summary>Sets the text of the choice item</summary>
    /// <param name="text">Text of the choice item</param>
    /// <returns>Current facade</returns>
    public ChoiceDefinitionFacade<TParentFacade> SetText(string text)
    {
      this.element.Text = text;
      return this;
    }

    /// <summary>Sets the value of the choice item</summary>
    /// <param name="value">This item's value</param>
    /// <returns>Current facade</returns>
    public ChoiceDefinitionFacade<TParentFacade> SetValue(string value)
    {
      this.element.Value = value;
      return this;
    }

    /// <summary>Sets the description of the choice item</summary>
    /// <param name="description">This item's description</param>
    /// <returns>Current facade</returns>
    public ChoiceDefinitionFacade<TParentFacade> SetDescription(
      string description)
    {
      this.element.Description = description;
      return this;
    }

    /// <summary>Enables this choice item</summary>
    /// <returns>Current facade</returns>
    public ChoiceDefinitionFacade<TParentFacade> Enable()
    {
      this.element.Enabled = true;
      return this;
    }

    /// <summary>Disables this choice item</summary>
    /// <returns>Current facade</returns>
    public ChoiceDefinitionFacade<TParentFacade> Disable()
    {
      this.element.Enabled = false;
      return this;
    }

    /// <summary>Selects this choice item</summary>
    /// <returns>Current facade</returns>
    public ChoiceDefinitionFacade<TParentFacade> Select()
    {
      this.element.Selected = true;
      return this;
    }

    /// <summary>Deselects this choice item</summary>
    /// <returns>Current facade</returns>
    public ChoiceDefinitionFacade<TParentFacade> Deselect()
    {
      this.element.Selected = false;
      return this;
    }

    /// <summary>
    /// Sets the localization class for the field that should be used
    /// for localizing the properties. When this method is called, all properties
    /// will start to behave as resource keys.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// The type of the class which should be used to localize the string properties.
    /// </typeparam>
    /// <returns>Current facade</returns>
    public ChoiceDefinitionFacade<TParentFacade> LocalizeUsing<TResource>() where TResource : Resource
    {
      this.element.ResourceClassId = typeof (TResource).Name;
      return this;
    }

    /// <summary>Get the current configuration element</summary>
    /// <returns>Wrapped ChoiceElement</returns>
    public ChoiceElement Get() => this.element;

    /// <summary>
    /// Stop customizing this choice item and go back to the parent facade
    /// </summary>
    /// <returns>Parent facade</returns>
    public TParentFacade Done() => this.parentFacade;
  }
}
