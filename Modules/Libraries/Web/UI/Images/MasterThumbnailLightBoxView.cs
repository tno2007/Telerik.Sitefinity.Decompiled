// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailLightBoxView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Images
{
  [ControlTemplateInfo("LibrariesResources", "ImagesMasterThumbnailLightboxViewFriendlyName", "ImagesTitle")]
  public class MasterThumbnailLightBoxView : MasterThumbnailView
  {
    internal new const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailLightBoxView.ascx";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailLightBoxView.ascx");
    private const string masterThumbnailLightBoxViewScriptName = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.Scripts.MasterThumbnailLightBoxView.js";

    /// <summary>
    /// Gets the default name of the layout template.
    /// It will be used if the TemplateName of the <see cref="!:IContentViewDefinition" />
    /// Definition property is not set.
    /// </summary>
    /// <value>The default name of the layout template.</value>
    protected override string DefaultLayoutTemplateName => MasterThumbnailLightBoxView.layoutTemplatePath;

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
      {
        singleItemLink.NavigateUrl = dataItem.ResolveMediaUrl(false, (CultureInfo) null);
      }
      else
      {
        singleItemLink.NavigateUrl = dataItem.ResolveThumbnailUrl(masterViewDefinition.SingleItemThumbnailsName);
        if (!dataItem.IsVectorGraphics())
          return;
        dataItem.ApplySingleItemThumbnailProfileToControl((WebControl) singleItemLink, masterViewDefinition.SingleItemThumbnailsName);
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
    {
      new ScriptControlDescriptor(this.GetType().FullName, this.ClientID)
    };

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (MasterThumbnailLightBoxView).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.Scripts.MasterThumbnailLightBoxView.js", fullName)
      };
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
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.TelerikSitefinity | ScriptRef.JQueryFancyBox;
  }
}
