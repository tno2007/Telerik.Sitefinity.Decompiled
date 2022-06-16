// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailStripView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Images
{
  [ControlTemplateInfo("LibrariesResources", "ImagesMasterThumbnailStripViewFriendlyName", "ImagesTitle")]
  public class MasterThumbnailStripView : MasterThumbnailView
  {
    internal new const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailStripView.ascx";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailStripView.ascx");
    internal const string MasterThumbnailStripViewScriptName = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.Scripts.MasterThumbnailStripView.js";
    internal const string GalleriaJs = "Telerik.Sitefinity.Resources.Scripts.galleria.js";
    internal const string GalleriaHistoryJs = "Telerik.Sitefinity.Resources.Scripts.galleria.history.js";
    internal const string GalleriaClassicTemplateJs = "Telerik.Sitefinity.Resources.Scripts.galleria.classic.js";
    internal const string GalleriaClassicCss = "Telerik.Sitefinity.Resources.Themes.Basic.Styles.galleria.classic.css";

    /// <summary>
    /// Gets the default name of the layout template.
    /// It will be used if the TemplateName of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewDefinition" />
    /// Definition property is not set.
    /// </summary>
    /// <value>The default name of the layout template.</value>
    protected override string DefaultLayoutTemplateName => MasterThumbnailStripView.layoutTemplatePath;

    /// <summary>
    /// Gets or sets a value indicating whether previous and next links are enabled.
    /// </summary>
    protected bool? EnablePrevNextLinks { get; set; }

    /// <summary>Gets the images container.</summary>
    /// <value>The images container.</value>
    protected virtual HtmlGenericControl ImagesContainer => this.Container.GetControl<HtmlGenericControl>("imagesContainer", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      base.InitializeControls(container, definition);
      this.EnablePrevNextLinks = (definition as IImagesViewMasterDefinition).EnablePrevNextLinks;
    }

    /// <summary>Configures the detail link.</summary>
    /// <param name="singleItemLink">The single item link.</param>
    /// <param name="dataItem">The data item.</param>
    /// <param name="item">The item.</param>
    protected override void ConfigureDetailLink(
      HyperLink singleItemLink,
      Telerik.Sitefinity.Libraries.Model.Image dataItem,
      RadListViewItem item)
    {
      base.ConfigureDetailLink(singleItemLink, dataItem, item);
      if (!(this.MasterViewDefinition is MediaContentMasterDefinition masterViewDefinition))
        return;
      if (string.IsNullOrWhiteSpace(masterViewDefinition.SingleItemThumbnailsName))
        singleItemLink.NavigateUrl = dataItem.ResolveMediaUrl(false, (CultureInfo) null);
      else
        singleItemLink.NavigateUrl = dataItem.ResolveThumbnailUrl(masterViewDefinition.SingleItemThumbnailsName);
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
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.TelerikSitefinity;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      ClientScriptManager clientScript = this.Page.ClientScript;
      controlDescriptor.AddProperty("_galleriaTemplateUrl", (object) clientScript.GetWebResourceUrl(Config.Get<ControlsConfig>().ResourcesAssemblyInfo, "Telerik.Sitefinity.Resources.Scripts.galleria.classic.js"));
      controlDescriptor.AddProperty("_imagesContainerId", (object) this.ImagesContainer.ClientID);
      controlDescriptor.AddProperty("_galleriaCssUrl", (object) clientScript.GetWebResourceUrl(Config.Get<ControlsConfig>().ResourcesAssemblyInfo, "Telerik.Sitefinity.Resources.Themes.Basic.Styles.galleria.classic.css"));
      if (this.EnablePrevNextLinks.HasValue)
        controlDescriptor.AddProperty("_enablePrevNextLinks", (object) this.EnablePrevNextLinks);
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
      string fullName1 = typeof (MasterThumbnailStripView).Assembly.FullName;
      string fullName2 = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.Scripts.MasterThumbnailStripView.js", fullName1),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.galleria.js", fullName2)
      }.ToArray();
    }
  }
}
