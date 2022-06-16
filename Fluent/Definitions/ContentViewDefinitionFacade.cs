// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Base Fluent API facade that defines a definition for ContentView.
  /// </summary>
  /// <typeparam name="TElement">The type of the element.</typeparam>
  /// <typeparam name="TActualFacade">The type of the actual facade.</typeparam>
  public abstract class ContentViewDefinitionFacade<TElement, TActualFacade>
    where TElement : ContentViewDefinitionElement
    where TActualFacade : class
  {
    protected string moduleName;
    protected string definitionName;
    protected Type contentType;
    protected ConfigElementDictionary<string, ContentViewDefinitionElement> parentElement;
    protected string viewName;
    private ContentViewControlDefinitionFacade parentFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public ContentViewDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementDictionary<string, ContentViewDefinitionElement> parentElement,
      string viewName,
      ContentViewControlDefinitionFacade parentFacade)
    {
      if (parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (string.IsNullOrEmpty(definitionName))
        throw new ArgumentNullException(nameof (definitionName));
      if (parentElement == null)
        throw new ArgumentNullException(nameof (parentElement));
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentNullException(nameof (viewName));
      this.moduleName = moduleName;
      this.definitionName = definitionName;
      this.contentType = contentType;
      this.parentElement = parentElement;
      this.viewName = viewName;
      this.parentFacade = parentFacade;
      this.DefineContentView(parentElement, viewName);
    }

    /// <summary>Gets or sets the current content view.</summary>
    /// <value>The content view.</value>
    protected TElement ContentView { get; set; }

    /// <summary>Returns the current content view element.</summary>
    /// <returns></returns>
    public TElement Get() => this.ContentView;

    /// <summary>Sets the type of the view.</summary>
    /// <param name="type">The type of the view.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetViewType(Type type)
    {
      this.ContentView.ViewType = !(type == (Type) null) ? type : throw new ArgumentNullException(nameof (type));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the description of the content view. If you have called LocalizeUsing method,
    /// this is the key of the localization resource; otherwise this is the actual description.
    /// </summary>
    /// <param name="viewDescription">
    /// Description or the resource key of the description when localization is used.
    /// </param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetDescription(string viewDescription)
    {
      this.ContentView.Description = !string.IsNullOrEmpty(viewDescription) ? viewDescription : throw new ArgumentNullException(nameof (viewDescription));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the display mode in which the FieldControls of the view ought to be rendered.
    /// </summary>
    /// <param name="mode">The display mode.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetDisplayMode(FieldDisplayMode mode)
    {
      this.ContentView.DisplayMode = mode;
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the localization class for the detail view that should be used
    /// for localizing the properties. When this method is called, all properties
    /// will start to behave as resource keys.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// The type of the class which should be used to localize the string properties.
    /// </typeparam>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade LocalizeUsing<TResourceClass>() where TResourceClass : Resource
    {
      this.ContentView.ResourceClassId = typeof (TResourceClass).Name;
      return this as TActualFacade;
    }

    /// <summary>Sets the name of the view template.</summary>
    /// <param name="templateName">The name of the view template.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetTemplateName(string templateName)
    {
      this.ContentView.TemplateName = !string.IsNullOrEmpty(templateName) ? templateName : throw new ArgumentNullException(nameof (templateName));
      return this as TActualFacade;
    }

    /// <summary>Sets the path of the view template.</summary>
    /// <param name="templatePath">The path of the view template.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetTemplatePath(string templatePath)
    {
      this.ContentView.TemplatePath = !string.IsNullOrEmpty(templatePath) ? templatePath : throw new ArgumentNullException(nameof (templatePath));
      return this as TActualFacade;
    }

    /// <summary>Sets the template key.</summary>
    /// <param name="templateKey">The template key.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetTemplateKey(string templateKey)
    {
      this.ContentView.TemplateKey = !string.IsNullOrEmpty(templateKey) ? templateKey : throw new ArgumentNullException(nameof (templateKey));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the title of the content view. If you have called LocalizeUsing method,
    /// this is the key of the localization resource; otherwise this is the actual title.
    /// </summary>
    /// <param name="viewTitle">Title or the resource key of the title when localization is used.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetTitle(string viewTitle)
    {
      this.ContentView.Title = !string.IsNullOrEmpty(viewTitle) ? viewTitle : throw new ArgumentNullException(nameof (viewTitle));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the format for the Title generated from the parent content item name.
    /// </summary>
    /// <param name="format">The parent title format.</param>
    /// <remarks>
    /// This format will be used if the Content type has a parent.
    /// Set it to null if you want the view title to be configured from the definition's Title property.
    /// </remarks>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetParentTitleFormat(string format)
    {
      this.ContentView.ParentTitleFormat = !string.IsNullOrEmpty(format) ? format : throw new ArgumentNullException(nameof (format));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the virtual path of the view if the view is implemented
    /// as a user control.
    /// </summary>
    /// <param name="path">The virtual path of the view.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetViewVirtualPath(string path)
    {
      this.ContentView.ViewVirtualPath = !string.IsNullOrEmpty(path) ? path : throw new ArgumentNullException(nameof (path));
      return this as TActualFacade;
    }

    /// <summary>The view will use workflow.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade UseWorkflow()
    {
      this.ContentView.UseWorkflow = new bool?(true);
      return this as TActualFacade;
    }

    /// <summary>The view will not use workflow.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade DoNotUseWorkflow()
    {
      this.ContentView.UseWorkflow = new bool?(false);
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the value for the <see cref="T:System.Web.UI.Control" /> ID property of the control that will be constructed based on this definition.
    /// </summary>
    /// <param name="id">The control id.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetControlId(string id)
    {
      this.ContentView.ControlId = !string.IsNullOrEmpty(id) ? id : throw new ArgumentNullException(nameof (id));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets a dictionary of external scripts to use with the MasterView and DetailFormView. The key of each
    /// element is the virtual path to the external script, while the value is the name of a method in
    /// that external script that will handle the ViewLoaded event.
    /// </summary>
    /// <param name="scripts">The dictionary of external client scripts.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetExternalClientScripts(Dictionary<string, string> scripts)
    {
      this.ContentView.ExternalClientScripts = scripts != null ? scripts : throw new ArgumentNullException(nameof (scripts));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets a dictionary defining custom client command replacing the standard ones, if applicable.
    /// Key: the name of the standard command (e.g. "view")
    /// Value: the custom command name (e.g. "viewOriginalImage")
    /// </summary>
    /// <param name="commandNames">The client mapped command names.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetClientMappedCommnadNames(Dictionary<string, string> commandNames)
    {
      this.ContentView.ClientMappedCommnadNames = commandNames != null ? commandNames : throw new ArgumentNullException(nameof (commandNames));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets a dictionary of localization properties
    /// Key identifies the localization string, and value is the current culture's translation
    /// </summary>
    /// <param name="localization">The dictionary of localization strings.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.ContentViewDefinitionFacade`2" />.</returns>
    public TActualFacade SetLocalization(Dictionary<string, string> localization)
    {
      this.ContentView.Localization = localization != null ? localization : throw new ArgumentNullException(nameof (localization));
      return this as TActualFacade;
    }

    /// <summary>Returns to parent facade.</summary>
    /// <returns>The parent facade which initialized this facade.</returns>
    public ContentViewControlDefinitionFacade Done() => this.parentFacade;

    /// <summary>Defines the content view.</summary>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    protected abstract void DefineContentView(
      ConfigElementDictionary<string, ContentViewDefinitionElement> parentElement,
      string viewName);
  }
}
