// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.ResponsiveLayoutField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.ResponsiveDesign;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI.Fields
{
  /// <summary>
  /// This field control is used in the Layout toolbox in PageEditor to select what Media queries are applied to the current page/template
  /// </summary>
  public class ResponsiveLayoutField : FieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.ResponsiveLayoutField.ascx");
    public static readonly string ScriptReference = "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.ResponsiveLayoutField.js";
    public static readonly string serviceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/ResponsiveDesign/MediaQuery.svc");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.ResponsiveLayoutField" /> class.
    /// </summary>
    public ResponsiveLayoutField() => this.LayoutTemplatePath = ResponsiveLayoutField.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the full type name of the item which will be associated with the Media Query Rules
    /// </summary>
    public string ItemType { get; set; }

    /// <summary>
    /// Gets or sets the Id of the item which will be associated with the Media Query Rules
    /// (currently Page or Template)
    /// </summary>
    public Guid ItemId { get; set; }

    /// <summary>
    /// Gets or sets the ids of the selected Media Queries for the flat selector
    /// </summary>
    public Guid[] SelectedMediaQueryIds { get; set; }

    /// <summary>
    /// Gets or sets the type of relation between the page and the media queries
    /// </summary>
    public string PageQueriesLinkType { get; set; }

    /// <summary>The LinkButton for select MediaQuery</summary>
    protected internal virtual LinkButton SelectButtonMediaQuery => this.Container.GetControl<LinkButton>("selectButtonMediaQuery", true);

    /// <summary>The Flat Selector for MediaQuery</summary>
    protected internal virtual FlatSelector MediaQueryItemSelector => this.Container.GetControl<FlatSelector>(nameof (MediaQueryItemSelector), true);

    /// <summary>The LinkButton for "Done"</summary>
    protected internal virtual LinkButton DoneButtonMediaQuery => this.Container.GetControl<LinkButton>("lnkDoneMediaQuery", true);

    /// <summary>The LinkButton for "Cancel"</summary>
    protected internal virtual LinkButton CancelButtonMediaQuery => this.Container.GetControl<LinkButton>("lnkCancelMediaQuery", true);

    /// <summary>
    /// The label that is initially shown in the layout toolbox, that shows what is the current selected responsive layout mode
    /// </summary>
    protected internal virtual Literal SelectedLinkTypeText => this.Container.GetControl<Literal>("selectedLinkTypeText", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      MediaQueryLink mediaQueryLink = ResponsiveDesignManager.GetManager().GetMediaQueryLinks().Where<MediaQueryLink>((Expression<Func<MediaQueryLink, bool>>) (mql => mql.ItemId == this.ItemId)).SingleOrDefault<MediaQueryLink>();
      if (mediaQueryLink == null)
      {
        this.SelectedMediaQueryIds = new Guid[0];
        if (this.ItemType == DesignMediaType.Template.ToString())
        {
          this.PageQueriesLinkType = MediaQueryLinkType.All.ToString();
          this.SelectedLinkTypeText.Text = Res.Get<ResponsiveDesignResources>("AllGroupsAreApplied");
        }
        else
        {
          this.PageQueriesLinkType = MediaQueryLinkType.Inherit.ToString();
          this.SelectedLinkTypeText.Text = Res.Get<ResponsiveDesignResources>("AsSetInTemplate");
        }
      }
      else
      {
        this.SelectedMediaQueryIds = mediaQueryLink.MediaQueries.Select<MediaQuery, Guid>((Func<MediaQuery, Guid>) (m => m.Id)).ToArray<Guid>();
        MediaQueryLinkType linkType = mediaQueryLink.LinkType;
        this.PageQueriesLinkType = linkType.ToString();
        linkType = mediaQueryLink.LinkType;
        switch (linkType)
        {
          case MediaQueryLinkType.All:
            this.SelectedLinkTypeText.Text = Res.Get<ResponsiveDesignResources>("AllGroupsAreApplied");
            break;
          case MediaQueryLinkType.Selected:
            this.SelectedLinkTypeText.Text = Res.Get<ResponsiveDesignResources>("SelectedRulesAreApplied");
            break;
          case MediaQueryLinkType.None:
            this.SelectedLinkTypeText.Text = Res.Get<ResponsiveDesignResources>("NoGroupsAreApplied");
            break;
        }
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript)
    /// client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" />
    /// objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = scriptDescriptors.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddElementProperty("selectButtonMediaQuery", this.SelectButtonMediaQuery.ClientID);
      controlDescriptor.AddComponentProperty("MediaQueryItemSelector", this.MediaQueryItemSelector.ClientID);
      controlDescriptor.AddElementProperty("lnkDoneMediaQuery", this.DoneButtonMediaQuery.ClientID);
      controlDescriptor.AddElementProperty("lnkCancelMediaQuery", this.CancelButtonMediaQuery.ClientID);
      controlDescriptor.AddProperty("itemType", (object) this.ItemType);
      controlDescriptor.AddProperty("itemId", (object) this.ItemId);
      controlDescriptor.AddProperty("serviceUrl", (object) ResponsiveLayoutField.serviceUrl);
      controlDescriptor.AddProperty("selectedKeys", (object) this.SelectedMediaQueryIds);
      controlDescriptor.AddProperty("mediaQueryLinkType", (object) this.PageQueriesLinkType);
      return scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects
    /// that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" />
    /// objects.
    /// </returns>
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
    {
      List<System.Web.UI.ScriptReference> scriptReferences = new List<System.Web.UI.ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (TextField).Assembly.FullName;
      scriptReferences.Add(new System.Web.UI.ScriptReference(ResponsiveLayoutField.ScriptReference, fullName));
      return (IEnumerable<System.Web.UI.ScriptReference>) scriptReferences;
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
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.JQueryUI;
  }
}
