// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxItemFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace Telerik.Sitefinity.Fluent.Modules.Toolboxes
{
  /// <summary>
  /// Fluent API facade for working with a single toolbox item.
  /// </summary>
  public class ToolboxItemFacade<TToolboxSectionFacade>
  {
    private ToolboxItem toolboxItem;
    private TToolboxSectionFacade parentFacade;

    /// <summary>
    /// Creates a new instance of the <see cref="!:ToolboxItemFacade" /> with specified toolbox
    /// item and parent facade.
    /// </summary>
    /// <param name="toolboxItem">An instance of the created toolbox item.</param>
    /// <param name="parentFacade">
    /// An instance of the <see cref="!:ToolboxSectionFacade" /> that initialized this facade.
    /// </param>
    public ToolboxItemFacade(ToolboxItem toolboxItem, TToolboxSectionFacade parentFacade)
    {
      if (toolboxItem == null)
        throw new ArgumentNullException(nameof (toolboxItem));
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      this.toolboxItem = toolboxItem;
      this.parentFacade = parentFacade;
    }

    /// <summary>
    /// Sets the localization class for the toolbox item that should be used
    /// for localizing the properties. When this method is called, all properties
    /// will start to behave as resource keys.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// The type of the class which should be used to localize the string properties of the toolbox item.
    /// </typeparam>
    /// <returns>An instance of the current <see cref="!:ToolboxItemFacade" />.</returns>
    public ToolboxItemFacade<TToolboxSectionFacade> LocalizeUsing<TResourceClass>() where TResourceClass : Resource
    {
      this.toolboxItem.ResourceClassId = typeof (TResourceClass).Name;
      return this;
    }

    /// <summary>
    /// Sets the title of the toolbox item. If you have called LocalizeUsing method,
    /// this is the key of the localization resource; otherwise this is the actual title.
    /// </summary>
    /// <param name="itemTitle">Title or the resource key of the title when localization is used.</param>
    /// <returns>An instance of the current <see cref="!:ToolboxItemFacade" />.</returns>
    public ToolboxItemFacade<TToolboxSectionFacade> SetTitle(string itemTitle)
    {
      this.toolboxItem.Title = !string.IsNullOrEmpty(itemTitle) ? itemTitle : throw new ArgumentNullException(nameof (itemTitle));
      return this;
    }

    /// <summary>
    /// Sets the description of the toolbox item. If you have called LocalizeUsing method,
    /// this is the key of the localization resource; otherwise this is the actual description.
    /// </summary>
    /// <param name="itemDescription">
    /// Description or the resource key of the description when localization is used.
    /// </param>
    /// <returns>An instance of the current <see cref="!:ToolboxItemFacade" />.</returns>
    public ToolboxItemFacade<TToolboxSectionFacade> SetDescription(
      string itemDescription)
    {
      this.toolboxItem.Description = !string.IsNullOrEmpty(itemDescription) ? itemDescription : throw new ArgumentNullException(nameof (itemDescription));
      return this;
    }

    /// <summary>Sets the toolbox item's parameters.</summary>
    /// <param name="parameters">The parameters of the toolbox item.</param>
    /// <returns>An instance of the current <see cref="!:ToolboxItemFacade" />.</returns>
    public ToolboxItemFacade<TToolboxSectionFacade> SetParameters(
      NameValueCollection parameters)
    {
      this.toolboxItem.Parameters = parameters != null ? parameters : throw new ArgumentNullException(nameof (parameters));
      return this;
    }

    /// <summary>
    /// Sets the CSS class of the toolbox item. Use this to set the custom icon.
    /// </summary>
    /// <param name="cssClass">The CssClass that should be applied to the toolbox item.</param>
    /// <returns>An instance of the current <see cref="!:ToolboxItemFacade" />.</returns>
    public ToolboxItemFacade<TToolboxSectionFacade> SetCssClass(string cssClass)
    {
      this.toolboxItem.CssClass = !string.IsNullOrEmpty(cssClass) ? cssClass : throw new ArgumentNullException(nameof (cssClass));
      return this;
    }

    /// <summary>
    /// Allows chaining of arbitrary <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.ToolboxItem" /> manipulation.
    /// </summary>
    /// <param name="action">An action delegate on the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.ToolboxItem" /> being worked with.</param>
    /// <returns>An instance of the current <see cref="!:ToolboxItemFacade" />.</returns>
    public ToolboxItemFacade<TToolboxSectionFacade> Do(Action<ToolboxItem> action)
    {
      action(this.toolboxItem);
      return this;
    }

    /// <summary>Returns back to the toolbox section fluent API.</summary>
    /// <returns>An instance of the <see cref="!:ToolboxSectionFacade" />.</returns>
    public TToolboxSectionFacade Done() => this.parentFacade;
  }
}
