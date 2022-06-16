// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  /// <summary>
  /// Representing a class for managing the browse &amp; edit controls
  /// The controls should be registered on the each view (master/detail) that is required to have B&amp;E functionallity
  /// </summary>
  public class BrowseAndEditManager : SimpleScriptView
  {
    private const string browseAndEditCookieName = "browseAndEditState";
    private IList<IBrowseAndEditToolbar> toolbars = (IList<IBrowseAndEditToolbar>) new List<IBrowseAndEditToolbar>();
    private IList<string> dialogDefinitions = (IList<string>) new List<string>();
    private const string scriptName = "Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.Scripts.BrowseAndEditManager.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.BrowseAndEditManager.ascx");
    private const string workflowServiceUrl = "~/Sitefinity/Services/Workflow/WorkflowService.svc";

    public BrowseAndEditManager()
    {
    }

    public BrowseAndEditManager(Guid pageId, int pageVersion, ContentLifecycleStatus pageStatus)
    {
      this.PageId = pageId;
      this.PageVersion = pageVersion;
      this.PageStatus = pageStatus;
    }

    /// <summary>
    /// Gets the name of the cookie used for storing browse and edit state.
    /// </summary>
    public static string BrowseAndEditCookieName => "browseAndEditState";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? BrowseAndEditManager.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Represents the id of the current page data when loaded
    /// </summary>
    public Guid PageId { get; private set; }

    /// <summary>
    /// Represents te version of the current page data when loaded
    /// </summary>
    public int PageVersion { get; private set; }

    /// <summary>The content lifcecycle status of the page when loaded</summary>
    public ContentLifecycleStatus PageStatus { get; private set; }

    /// <summary>
    /// Gets the window manager that holds all dialog windows.
    /// </summary>
    protected RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>
    /// Gets the dialog that displays the warning messages when a page is modified in the backend
    /// </summary>
    protected PromptDialog PageChangedWarningDialog => this.Container.GetControl<PromptDialog>("pageChangedWarningDialog", true);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManager" /> instance for a given <see cref="T:System.Web.UI.Page" />
    /// object.
    /// </summary>
    /// <returns>
    /// The current <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManager" /> instance for the selected
    /// <see cref="T:System.Web.UI.Page" /> object, or null if no instance is defined.
    /// </returns>
    /// <param name="page">
    /// The page instance to retrieve the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManager" />
    /// from.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="page" /> is null.
    /// </exception>
    public static BrowseAndEditManager GetCurrent(Page page)
    {
      if (page == null)
        throw new ArgumentNullException(Res.Get<ErrorMessages>().PageIsNull);
      return (BrowseAndEditManager) page.Items[(object) typeof (BrowseAndEditManager).FullName];
    }

    /// <summary>
    /// Adds a toolbar to the manager to be prepared for browse and edit functionality
    /// </summary>
    /// <param name="toolbar"></param>
    public void Add(IBrowseAndEditToolbar toolbar) => this.toolbars.Add(toolbar);

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that
    /// corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.ConstructDialogsFromToolbars();
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.Initialize();
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.JQueryCookie;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (BrowseAndEditManager).FullName, this.ClientID);
      IEnumerable<string> strings = this.toolbars.Select<IBrowseAndEditToolbar, string>((Func<IBrowseAndEditToolbar, string>) (t => ((Control) t).ClientID));
      controlDescriptor.AddProperty("toolbarIds", (object) strings);
      controlDescriptor.AddProperty("workflowServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Workflow/WorkflowService.svc"));
      controlDescriptor.AddProperty("isMultilingual", (object) SystemManager.CurrentContext.AppSettings.Multilingual);
      controlDescriptor.AddProperty("uiCulture", (object) SystemManager.CurrentContext.Culture.Name);
      controlDescriptor.AddProperty("pageUrl", (object) this.GetPageUrl());
      controlDescriptor.AddProperty("browseAndEditCookieName", (object) BrowseAndEditManager.BrowseAndEditCookieName);
      controlDescriptor.AddProperty("pageVersion", (object) this.PageVersion);
      controlDescriptor.AddProperty("pageStatus", (object) this.PageStatus.ToString());
      controlDescriptor.AddProperty("pageId", (object) this.PageId);
      controlDescriptor.AddProperty("pagesServiceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Pages/PagesService.svc/"));
      controlDescriptor.AddComponentProperty("pageChangedWarningDialog", this.PageChangedWarningDialog.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = typeof (BrowseAndEditManager).Assembly.FullName;
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.Scripts.BrowseAndEditManager.js"
      });
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      });
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.SitefinityJS.Utility.DialogManager.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Constructs the dialogs from toolbars.</summary>
    protected void ConstructDialogsFromToolbars()
    {
      foreach (IBrowseAndEditToolbar toolbar in (IEnumerable<IBrowseAndEditToolbar>) this.toolbars)
      {
        foreach (BrowseAndEditCommand browseAndEditCommand in (IEnumerable<BrowseAndEditCommand>) toolbar.ToolbarControls.Values)
        {
          IDialogDefinition dialogDefinition = browseAndEditCommand.Arguments.DialogDefinition;
          if (dialogDefinition != null)
          {
            string dialogName = browseAndEditCommand.Arguments.DialogName;
            if (!this.dialogDefinitions.Contains(dialogName))
            {
              this.ConstructDialog(dialogDefinition, dialogName);
              this.dialogDefinitions.Add(dialogName);
            }
          }
        }
      }
    }

    /// <summary>Constructs the dialog.</summary>
    /// <param name="dialog">The dialog.</param>
    protected void ConstructDialog(IDialogDefinition dialog, string dialogKey)
    {
      Telerik.Web.UI.RadWindow control = new Telerik.Web.UI.RadWindow();
      control.ID = dialogKey;
      control.Behaviors = dialog.Behaviors;
      control.InitialBehaviors = dialog.InitialBehaviors;
      control.Width = dialog.Width;
      control.Height = dialog.Height;
      control.VisibleTitlebar = dialog.VisibleTitleBar;
      control.VisibleStatusbar = dialog.VisibleStatusBar;
      control.NavigateUrl = dialog.NavigateUrl;
      control.Modal = dialog.IsModal;
      control.ReloadOnShow = dialog.ReloadOnShow.HasValue && dialog.ReloadOnShow.Value;
      Telerik.Web.UI.RadWindow radWindow = control;
      bool? destroyOnClose = dialog.DestroyOnClose;
      int num;
      if (!destroyOnClose.HasValue)
      {
        num = 0;
      }
      else
      {
        destroyOnClose = dialog.DestroyOnClose;
        num = destroyOnClose.Value ? 1 : 0;
      }
      radWindow.DestroyOnClose = num != 0;
      if (!string.IsNullOrEmpty(dialog.Skin))
        control.Skin = dialog.Skin;
      if (!string.IsNullOrEmpty(dialog.CssClass))
        control.CssClass = dialog.CssClass;
      this.WindowManager.Windows.Add(control);
    }

    /// <summary>Initializes this instance.</summary>
    private void Initialize()
    {
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      if (this.DesignMode)
        return;
      if (BrowseAndEditManager.GetCurrent(this.Page) != null)
        throw new Exception("More than one BrowseAndEditManagers");
      this.Page.Items[(object) typeof (BrowseAndEditManager).FullName] = (object) this;
    }

    /// <summary>Gets the page URL.</summary>
    /// <returns></returns>
    protected string GetPageUrl()
    {
      string pageUrl = string.Empty;
      SiteMapNode currentNode = SiteMapBase.GetCurrentProvider().CurrentNode;
      if (currentNode != null)
        pageUrl = !(currentNode is PageSiteNode pageSiteNode) ? currentNode.Url : RouteHelper.ResolveUrl(PageManager.GetManager().GetPageNode(pageSiteNode.Id).GetUrl(), UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
      return pageUrl;
    }
  }
}
