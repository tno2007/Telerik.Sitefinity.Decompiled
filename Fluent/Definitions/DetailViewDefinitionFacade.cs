// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Fluent API facade that defines a definition for DetailFormView.
  /// </summary>
  public class DetailViewDefinitionFacade : 
    BaseDetailDefinitionFacade<DetailFormViewElement, DetailViewDefinitionFacade>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public DetailViewDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementDictionary<string, ContentViewDefinitionElement> parentElement,
      string viewName,
      ContentViewControlDefinitionFacade parentFacade)
      : base(moduleName, definitionName, contentType, parentElement, viewName, parentFacade)
    {
    }

    /// <summary>Displays the top toolbar.</summary>
    /// <returns></returns>
    public DetailViewDefinitionFacade ShowTopToolbar()
    {
      this.ContentView.ShowTopToolbar = new bool?(true);
      return this;
    }

    /// <summary>Hides the top toolbar.</summary>
    /// <returns></returns>
    public DetailViewDefinitionFacade HideTopToolbar()
    {
      this.ContentView.ShowTopToolbar = new bool?(false);
      return this;
    }

    /// <summary>Sets the base service url of the detail view.</summary>
    /// <param name="serviceUrl">Service Url.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    public DetailViewDefinitionFacade SetServiceBaseUrl(string serviceUrl)
    {
      this.ContentView.WebServiceBaseUrl = !string.IsNullOrEmpty(serviceUrl) ? serviceUrl : throw new ArgumentNullException(nameof (serviceUrl));
      return this;
    }

    /// <summary>Shows the previous/next navigation buttons.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    public DetailViewDefinitionFacade ShowNavigation()
    {
      this.ContentView.ShowNavigation = new bool?(true);
      return this;
    }

    /// <summary>Hides the previous/next navigation buttons.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    public DetailViewDefinitionFacade HideNavigation()
    {
      this.ContentView.ShowNavigation = new bool?(false);
      return this;
    }

    /// <summary>
    /// Specifies that the view creates a blank item on the server.
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    public DetailViewDefinitionFacade CreateBlankItem()
    {
      this.ContentView.CreateBlankItem = new bool?(true);
      return this;
    }

    /// <summary>
    /// Specifies that the view does not create a blank item on the server.
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    public DetailViewDefinitionFacade DoNotCreateBlankItem()
    {
      this.ContentView.CreateBlankItem = new bool?(false);
      return this;
    }

    /// <summary>
    /// Specifies that the system will automatically unlock the currently edited item, when exiting the detail screen.
    /// Exiting the screen is registered on Back to all items, browser back button and browse window close
    /// Unlocking is executed only if the screen was opened in Write mode
    /// The unlocking uses web service REST convention to call the WebServiceBaseUrl/temp/itemid with DELETE HTTP verb
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    public DetailViewDefinitionFacade UnlockDetailItemOnExit()
    {
      this.ContentView.UnlockDetailItemOnExit = new bool?(true);
      return this;
    }

    /// <summary>
    /// Specifies that the system will not automatically unlock the currently edited item, when exiting the detail screen.
    /// Exiting the screen is registered on Back to all items, browser back button and browse window close
    /// Unlocking is executed only if the screen was opened in Write mode
    /// The unlocking uses web service REST convention to call the WebServiceBaseUrl/temp/itemid with DELETE HTTP verb
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    public DetailViewDefinitionFacade DoNotUnlockDetailItemOnExit()
    {
      this.ContentView.UnlockDetailItemOnExit = new bool?(false);
      return this;
    }

    /// <summary>Specifies that the translation view will be rendered.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    public DetailViewDefinitionFacade RenderTranslationView()
    {
      this.ContentView.IsToRenderTranslationView = new bool?(true);
      return this;
    }

    /// <summary>
    /// Specifies that the translation view will not be rendered.
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    public DetailViewDefinitionFacade DoNotRenderTranslationView()
    {
      this.ContentView.IsToRenderTranslationView = new bool?(false);
      return this;
    }

    /// <summary>Sets the alternative title for the detail view.</summary>
    /// <param name="title">The alternative title.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    /// <remarks>
    /// Used in the backEnd detail form view when the edit dialog is used for creating a different language version of the item.
    /// </remarks>
    public DetailViewDefinitionFacade SetAlternativeTitle(string title)
    {
      this.ContentView.AlternativeTitle = !string.IsNullOrEmpty(title) ? title : throw new ArgumentNullException(nameof (title));
      return this;
    }

    /// <summary>
    /// Specifies that the generated JSON will be posted as it is to the web service.
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    public DetailViewDefinitionFacade DoNotUseContentItemContext()
    {
      this.ContentView.DoNotUseContentItemContext = true;
      return this;
    }

    /// <summary>
    /// Specifies that the generated JSON on the client will be the value of the Item property of an 'item context' object.
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    public DetailViewDefinitionFacade UseContentItemContext()
    {
      this.ContentView.DoNotUseContentItemContext = false;
      return this;
    }

    /// <summary>
    /// Sets the item template string for the detail view (to be used to get the right item type, if required).
    /// </summary>
    /// <param name="template">The string representing the item template.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailViewDefinitionFacade" />.</returns>
    /// <remarks>
    /// Used in the backEnd detail form view when the edit dialog is used for getting a new item.
    /// </remarks>
    public DetailViewDefinitionFacade SetItemTemplate(string template)
    {
      this.ContentView.ItemTemplate = !string.IsNullOrEmpty(template) ? template : throw new ArgumentNullException(nameof (template));
      return this;
    }

    /// <summary>
    /// Specifies that the detail view should display Multilingual related data
    /// </summary>
    /// <returns></returns>
    public DetailViewDefinitionFacade SupportMultilingual()
    {
      this.ContentView.MultilingualMode = MultilingualMode.On;
      return this;
    }

    /// <summary>
    /// Specifies that the detail view should NOT display Multilingual related data
    /// </summary>
    /// <returns></returns>
    public DetailViewDefinitionFacade DoNotSupportMultilingual()
    {
      this.ContentView.MultilingualMode = MultilingualMode.Off;
      return this;
    }

    /// <summary>
    /// Sets a comma-delimited list of custom Create commands used internally by given module.
    /// </summary>
    /// <param name="commands">Array containing the custom Create commands</param>
    /// <returns></returns>
    public DetailViewDefinitionFacade SetAdditionalCreateCommands(
      params string[] commands)
    {
      this.ContentView.AdditionalCreateCommands = commands != null ? string.Join(",", commands) : throw new ArgumentNullException("additionalCreateCommands");
      return this;
    }

    /// <summary>Defines the content view.</summary>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    protected override void DefineContentView(
      ConfigElementDictionary<string, ContentViewDefinitionElement> parentElement,
      string viewName)
    {
      DetailFormViewElement detailFormViewElement = new DetailFormViewElement((ConfigElement) parentElement);
      detailFormViewElement.ViewName = viewName;
      detailFormViewElement.ViewType = typeof (DetailFormView);
      detailFormViewElement.DisplayMode = FieldDisplayMode.Write;
      detailFormViewElement.ShowSections = new bool?(true);
      detailFormViewElement.ShowTopToolbar = new bool?(true);
      this.ContentView = detailFormViewElement;
    }
  }
}
