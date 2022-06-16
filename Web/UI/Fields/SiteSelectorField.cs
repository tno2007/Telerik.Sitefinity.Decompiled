// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.SiteSelectorField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>A field control for selecting a site/sites</summary>
  public class SiteSelectorField : FieldControl
  {
    internal const string layoutTemplate = "Telerik.Sitefinity.Resources.Templates.Fields.SiteSelectorField.ascx";
    internal const string script = "Telerik.Sitefinity.Web.UI.Fields.Scripts.SiteSelectorField.js";
    private IEnumerable<ISite> sites;

    public SiteSelectorField() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.SiteSelectorField.ascx");

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The value.</value>
    public override object Value { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    public IEnumerable<ISite> Sites
    {
      get
      {
        IMultisiteContext context = SystemManager.CurrentContext.MultisiteContext;
        if (context != null)
        {
          if (this.sites == null)
          {
            SitefinityIdentity currentUser = ClaimsManager.GetCurrentIdentity();
            this.sites = context.GetSites().Where<ISite>((Func<ISite, bool>) (s => ((IEnumerable<Guid>) context.GetAllowedSites(currentUser.UserId, currentUser.MembershipProvider).ToArray<Guid>()).Contains<Guid>(s.Id)));
          }
        }
        else
          this.sites = (IEnumerable<ISite>) new List<ISite>()
          {
            SystemManager.CurrentContext.CurrentSite
          };
        return this.sites;
      }
    }

    public bool ShowSiteLanguageSelector { get; set; }

    public DropDownList SiteSelector => this.Container.GetControl<DropDownList>(nameof (SiteSelector), true);

    public Panel SelectorPanel => this.Container.GetControl<Panel>(nameof (SelectorPanel), true);

    public LinkButton ChangeLinkButton => this.Container.GetControl<LinkButton>("ChangeButton", true);

    public SitefinityLabel SelectedSiteName => this.Container.GetControl<SitefinityLabel>(nameof (SelectedSiteName), true);

    public Panel ValuePanel => this.Container.GetControl<Panel>(nameof (ValuePanel), true);

    public SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>(nameof (TitleLabel), true);

    public SitefinityLabel DescriptionLabel => this.Container.GetControl<SitefinityLabel>(nameof (DescriptionLabel), true);

    public DropDownList SiteLanguageSelector => this.Container.GetControl<DropDownList>(nameof (SiteLanguageSelector), true);

    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is ISiteSelectorFieldDefinition selectorFieldDefinition))
        return;
      this.ShowSiteLanguageSelector = selectorFieldDefinition.ShowSiteLanguageSelector;
    }

    protected override void InitializeControls(GenericContainer container)
    {
      this.TitleLabel.Text = this.Title;
      this.DescriptionLabel.Text = this.Description;
      this.SetupSiteSelector();
      if (this.ShowSiteLanguageSelector)
        return;
      this.SiteLanguageSelector.Visible = false;
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor scriptDescriptor = this.GetLastScriptDescriptor();
      scriptDescriptor.AddProperty("_currentSiteId", (object) SystemManager.CurrentContext.CurrentSite.Id);
      scriptDescriptor.AddProperty("_sites", (object) new JavaScriptSerializer().Serialize((object) this.Sites.ToDictionary((Func<ISite, string>) (s => s.Id.ToString()), s => new
      {
        Id = s.Id,
        Name = s.Id == SystemManager.CurrentContext.CurrentSite.Id ? string.Format("{0} ({1})", (object) s.Name, (object) Res.Get<PageResources>().ThisSite) : s.Name,
        SiteMapRootNodeId = s.SiteMapRootNodeId,
        PublicCultures = s.PublicCultures.ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (x => x.Value), (Func<KeyValuePair<string, string>, string>) (x => new CultureInfo(x.Value).DisplayName))
      })));
      scriptDescriptor.AddProperty("_visible", (object) (this.Sites.Count<ISite>() > 1));
      scriptDescriptor.AddProperty("_uiCulture", (object) SystemManager.CurrentContext.Culture.Name);
      scriptDescriptor.AddElementProperty("siteSelector", this.SiteSelector.ClientID);
      scriptDescriptor.AddElementProperty("selectorPanel", this.SelectorPanel.ClientID);
      scriptDescriptor.AddElementProperty("changeButton", this.ChangeLinkButton.ClientID);
      scriptDescriptor.AddElementProperty("selectedSiteName", this.SelectedSiteName.ClientID);
      scriptDescriptor.AddElementProperty("valuePanel", this.ValuePanel.ClientID);
      if (this.ShowSiteLanguageSelector)
        scriptDescriptor.AddElementProperty("siteLanguageSelector", this.SiteLanguageSelector.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        scriptDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference()
      {
        Assembly = typeof (SiteSelectorField).Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Fields.Scripts.SiteSelectorField.js"
      }
    }.ToArray();

    /// <summary>Gets the last script descriptor from the base class.</summary>
    /// <returns></returns>
    protected internal virtual ScriptControlDescriptor GetLastScriptDescriptor() => (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();

    private void SetupSiteSelector()
    {
      this.SiteSelector.DataTextField = "Name";
      this.SiteSelector.DataValueField = "Id";
      this.SiteSelector.DataSource = (object) this.Sites;
      this.SiteSelector.DataBind();
    }
  }
}
