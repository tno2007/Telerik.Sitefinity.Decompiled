// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend
{
  /// <summary>
  /// Base class for all views displaying content at public and backend site.
  /// </summary>
  public abstract class ViewBase : CompositeControl, IViewControl, IScriptControl
  {
    private const string viewBaseScript = "Telerik.Sitefinity.Web.UI.ContentUI.Views.Scripts.ViewBase.js";
    private GenericContainer container;
    private ITemplate layoutTemplate;
    private IContentViewDefinition definition;
    private ContentView host;
    private IDictionary<string, string> externalClientScripts;
    private string layoutTemplatePath;
    private string templateKey;
    private bool isEmptyView;

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.container = (GenericContainer) null;
      this.Controls.Clear();
      try
      {
        this.InitializeControls(this.Container, this.Definition);
        this.Controls.Add((Control) this.Container);
        this.Controls.Add((Control) new UserPreferences());
      }
      catch (ThreadAbortException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        this.ProcessInitializationException(ex);
      }
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected abstract void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition);

    /// <summary>
    /// Displays an error message when an exception is thrown during the rendering of the control.
    /// </summary>
    /// <param name="ex">The thrown exception.</param>
    protected virtual void ProcessInitializationException(Exception ex)
    {
      if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
        throw ex;
      if (!this.IsBackend())
        return;
      Label child = new Label();
      child.Text = Res.Get<ErrorMessages>().ErrorParsingTheTemplate + "Error: " + ex.Message;
      child.CssClass = "sfError";
      this.Controls.Add((Control) child);
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected virtual string LayoutTemplateName => this.definition != null ? this.definition.TemplateName : string.Empty;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public virtual string LayoutTemplatePath
    {
      get
      {
        if (this.layoutTemplatePath == null)
        {
          if (this.definition != null)
            this.layoutTemplatePath = this.definition.TemplatePath;
          if (string.IsNullOrEmpty(this.layoutTemplatePath) && string.IsNullOrEmpty(this.LayoutTemplateName))
            this.layoutTemplatePath = this.DefaultLayoutTemplatePath;
        }
        return this.layoutTemplatePath;
      }
      set => this.layoutTemplatePath = value;
    }

    /// <summary>
    /// Value of LayoutTemplatePath if LayoutTemplateName and LayoutTemplatePath both would otherwize have null or empty values
    /// </summary>
    protected virtual string DefaultLayoutTemplatePath => (string) null;

    /// <summary>Gets or sets the layout template.</summary>
    public virtual ITemplate LayoutTemplate
    {
      get
      {
        if (this.layoutTemplate == null)
          this.layoutTemplate = ControlUtilities.GetTemplate(new TemplateInfo()
          {
            TemplatePath = this.LayoutTemplatePath,
            TemplateName = this.LayoutTemplateName,
            TemplateResourceInfo = Config.Get<ControlsConfig>().ResourcesAssemblyInfo,
            ControlType = this.GetType(),
            Key = this.TemplateKey
          });
        return this.layoutTemplate;
      }
      set => this.layoutTemplate = value;
    }

    /// <summary>Gets or sets the template key.</summary>
    /// <value>The template key.</value>
    public virtual string TemplateKey
    {
      get
      {
        if (this.templateKey == null && this.definition != null)
          this.templateKey = this.definition.TemplateKey;
        return this.templateKey;
      }
      set => this.templateKey = value;
    }

    /// <summary>Gets or sets the container.</summary>
    /// <value>The container.</value>
    protected internal virtual GenericContainer Container
    {
      get
      {
        if (this.container == null)
          this.container = this.CreateContainer(this.LayoutTemplate);
        return this.container;
      }
    }

    /// <summary>Creates the container.</summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    protected internal virtual GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = new GenericContainer();
      template.InstantiateIn((Control) container);
      return container;
    }

    /// <summary>Gets or sets the content view definition.</summary>
    /// <value>The definition.</value>
    public IContentViewDefinition Definition
    {
      get => this.definition;
      set
      {
        this.definition = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the container for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <value>The host.</value>
    public ContentView Host
    {
      get => this.host;
      set
      {
        this.host = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets the master view definition.</summary>
    /// <value>The master view definition.</value>
    public IContentViewMasterDefinition MasterViewDefinition => this.Host.MasterViewDefinition;

    /// <summary>Gets the detail view definition.</summary>
    /// <value>The detail view definition.</value>
    public IContentViewDetailDefinition DetailViewDefinition => this.Host.DetailViewDefinition;

    /// <summary>
    /// Gets a value indicating whether this view is empty, e.g. renders no objects.
    /// This is used with public controls to determine whether to display an icon+link
    /// in design mode. This property should be set to TRUE from inherted View classes
    /// in case an icon must be rendered in page editor.
    /// </summary>
    public bool IsEmptyView
    {
      get => this.isEmptyView;
      set => this.isEmptyView = value;
    }

    protected string GetLabel(string classId, string key) => !string.IsNullOrEmpty(classId) ? Res.ResolveLocalizedValue(classId, key) : key;

    /// <summary>Determines whether the current view is detail view.</summary>
    /// <returns>
    /// 	<c>true</c> if the current view is detail view; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDetailView() => typeof (IContentViewDetailDefinition).IsAssignableFrom(this.Definition.GetType());

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the event data.
    /// </param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      PageManager.ConfigureScriptManager(this.Page, this.GetRequiredCoreScripts(), false)?.RegisterScriptControl<ViewBase>(this);
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the
    /// specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents
    /// the output stream to render HTML content on the client.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Gets a base descriptor containing only the base properties that need to be passed to the client.
    /// This method should be called by the derived classes with the proper clientType and clientID to
    /// ensure that there's only one descriptor instantiated in the inheritance hierarchy
    /// </summary>
    /// <param name="clientType">The client type.</param>
    /// <param name="clientId">The client pageId.</param>
    protected virtual ScriptControlDescriptor GetBaseDescriptor(
      string clientType,
      string clientId)
    {
      ScriptControlDescriptor baseDescriptor = new ScriptControlDescriptor(clientType, clientId);
      baseDescriptor.AddProperty("onLoadEvents", (object) this.ExternalClientScripts.Values);
      baseDescriptor.AddProperty("baseUrl", (object) this.ResolveUrl("~/"));
      return baseDescriptor;
    }

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected virtual ScriptRef GetRequiredCoreScripts() => ScriptRef.MicrosoftAjax | ScriptRef.MicrosoftAjaxWebForms | ScriptRef.JQuery | ScriptRef.JQueryValidate | ScriptRef.TelerikSitefinity | ScriptRef.JQueryCookie | ScriptRef.QueryString;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) null;

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public virtual IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ContentUI.Views.Scripts.ViewBase.js", typeof (ViewBase).Assembly.FullName));
      foreach (string key in (IEnumerable<string>) this.ExternalClientScripts.Keys)
      {
        if (!string.IsNullOrEmpty(key))
        {
          if (key.IndexOf(',') > 0)
          {
            string[] strArray = key.Split(',');
            string name = strArray[0].Trim();
            string assembly = strArray[1].Trim();
            scriptReferences.Add(new ScriptReference(name, assembly));
          }
          else if (key.StartsWith("~"))
            scriptReferences.Add(new ScriptReference(key));
          else
            scriptReferences.Add(new ScriptReference(VirtualPathUtility.ToAppRelative(key)));
        }
      }
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>
    /// Gets or sets a dictionary of external scripts to use with the MasterView. The key of each
    /// element is the virtual path to the external script, while the value is the name of a method in
    /// that external script that will handle the MasterView's ViewLoaded event.
    /// </summary>
    /// <value>The dictionary of external client scripts.</value>
    public IDictionary<string, string> ExternalClientScripts
    {
      get
      {
        if (this.externalClientScripts == null)
          this.externalClientScripts = (IDictionary<string, string>) new Dictionary<string, string>();
        return this.externalClientScripts;
      }
      set => this.externalClientScripts = value;
    }
  }
}
