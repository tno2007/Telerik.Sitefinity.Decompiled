// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.FlatSiteSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  public class FlatSiteSelector : SimpleScriptView
  {
    private static readonly string SiteSelectorKeyName = "Id";
    private static readonly string mutlisiteWebServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Multisite/Multisite.svc/");
    private const string script = "Telerik.Sitefinity.Web.UI.Backend.Scripts.FlatSiteSelector.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.FlatSiteSelector.ascx");
    private int visibleSitesCount = 5;
    private int visibleMoreSitesCount = 15;
    private IEnumerable<ISite> sites;

    public IEnumerable<ISite> Sites
    {
      get
      {
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        if (multisiteContext != null)
        {
          if (this.sites == null)
            this.sites = (IEnumerable<ISite>) multisiteContext.GetSites().OrderBy<ISite, string>((Func<ISite, string>) (x => x.Name));
          return this.sites;
        }
        return (IEnumerable<ISite>) new List<ISite>()
        {
          SystemManager.CurrentContext.CurrentSite
        };
      }
      set => this.sites = value;
    }

    public Guid CurrentSiteId => SystemManager.CurrentContext.CurrentSite.Id;

    public int VisibleSitesCount
    {
      get => this.visibleSitesCount;
      set => this.visibleSitesCount = value;
    }

    public bool MoreSitesMenuVisible => this.Sites.Count<ISite>() > this.VisibleSitesCount;

    public bool AllSitesLinkVisible => this.Sites.Count<ISite>() - this.VisibleSitesCount > this.visibleMoreSitesCount;

    /// <summary>Gets the sites selector.</summary>
    protected virtual Repeater RptSitesList => this.Container.GetControl<Repeater>("rptSitesList", true);

    protected virtual HyperLink MoreLink => this.Container.GetControl<HyperLink>("moreLink", true);

    protected virtual Repeater MoreSitesRepeater => this.Container.GetControl<Repeater>("moreSitesRepeater", true);

    protected virtual HyperLink AllSitesLink => this.Container.GetControl<HyperLink>("allSitesLink", true);

    /// <summary>
    /// Gets the html control that displays more providers menu.
    /// </summary>
    protected virtual HtmlGenericControl MoreSitesMenu => this.Container.GetControl<HtmlGenericControl>("moreSitesMenu", false);

    protected virtual FlatSelector AllSitesSelector => this.Container.GetControl<FlatSelector>("allSitesSelector", true);

    protected virtual HtmlGenericControl AllSites => this.Container.GetControl<HtmlGenericControl>("allSites", true);

    protected virtual LinkButton AllSitesDoneSelectingButton => this.Container.GetControl<LinkButton>("allSitesDoneSelectingButton", true);

    protected virtual LinkButton AllSitesCancelSelectingButton => this.Container.GetControl<LinkButton>("allSitesCancelSelectingButton", true);

    protected virtual HtmlGenericControl AllSitesMenu => this.Container.GetControl<HtmlGenericControl>("allSitesMenu", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string str = FlatSiteSelector.mutlisiteWebServiceUrl + "user/" + SecurityManager.GetCurrentUserId().ToString() + "/sites/?sortExpression=Name";
      this.AllSitesSelector.DataKeyNames = "Id";
      this.AllSitesSelector.ServiceUrl = str;
      this.AllSitesLink.Text = "Show all sites";
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FlatSiteSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.MicrosoftAjaxTemplates;

    protected override void OnPreRender(EventArgs e)
    {
      this.SetupSiteSelector();
      base.OnPreRender(e);
    }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      this.AddAttributesToRender(writer);
      writer.RenderBeginTag(HtmlTextWriterTag.Div);
    }

    private void SetupSiteSelector()
    {
      this.RptSitesList.DataSource = (object) this.Sites.Take<ISite>(this.VisibleSitesCount);
      this.RptSitesList.DataBind();
      this.MoreSitesRepeater.DataSource = (object) this.Sites.Skip<ISite>(this.VisibleSitesCount).Take<ISite>(this.visibleMoreSitesCount);
      this.MoreSitesRepeater.DataBind();
      if (this.MoreSitesMenuVisible)
      {
        int num = this.Sites.Count<ISite>() - this.VisibleSitesCount;
        this.MoreLink.Text = string.Format(Res.Get<Labels>().ShowMore, (object) num);
        this.MoreLink.Visible = true;
        this.AllSitesLink.Text = string.Format("All {0} sites", (object) this.Sites.Count<ISite>());
      }
      else
        this.MoreLink.Visible = false;
      this.AllSitesMenu.Visible = this.AllSitesLinkVisible;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddProperty("_siteSelectorVisible", (object) true);
      behaviorDescriptor.AddProperty("_currentSiteId", (object) Guid.Empty.ToString());
      behaviorDescriptor.AddProperty("_allSitesElementId", (object) Guid.Empty.ToString());
      behaviorDescriptor.AddProperty("_moreSitesMenuVisible", (object) this.MoreSitesMenuVisible);
      Dictionary<string, \u003C\u003Ef__AnonymousType42<string, string>> dictionary = this.Sites.Select(x => new
      {
        Id = x.Id.ToString(),
        Name = x.Name
      }).ToDictionary(x => x.Id, x => x);
      behaviorDescriptor.AddProperty("_dataItems", (object) new JavaScriptSerializer().Serialize((object) dictionary));
      behaviorDescriptor.AddElementProperty("rptSitesList", this.RptSitesList.ClientID);
      behaviorDescriptor.AddElementProperty("moreSitesMenu", this.MoreSitesMenu.ClientID);
      behaviorDescriptor.AddElementProperty("allSitesCancelSelectingButton", this.AllSitesCancelSelectingButton.ClientID);
      behaviorDescriptor.AddElementProperty("allSitesDoneSelectingButton", this.AllSitesDoneSelectingButton.ClientID);
      behaviorDescriptor.AddElementProperty("allSitesLink", this.AllSitesLink.ClientID);
      behaviorDescriptor.AddComponentProperty("allSitesSelector", this.AllSitesSelector.ClientID);
      behaviorDescriptor.AddElementProperty("allSites", this.AllSites.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>((IEnumerable<ScriptReference>) PageManager.GetScriptReferences(ScriptRef.JQueryUI));
      string str = typeof (FlatSiteSelector).Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Scripts.FlatSiteSelector.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
