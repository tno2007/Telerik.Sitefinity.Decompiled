// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailLightBoxView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos
{
  /// <summary>Represents the lightbox view for videos.</summary>
  [ControlTemplateInfo("LibrariesResources", "VideosMasterThumbnailLightboxViewFriendlyName", "VideosTitle")]
  public class MasterThumbnailLightBoxView : MasterThumbnailView
  {
    internal new const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Videos.MasterThumbnailLightBoxView.ascx";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Videos.MasterThumbnailLightBoxView.ascx");
    private const string masterThumbnailLightBoxViewScriptName = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.Scripts.MasterThumbnailLightBoxView.js";
    private bool? autoPlay;

    /// <summary>
    /// Gets the default name of the layout template.
    /// It will be used if the TemplateName of the <see cref="!:IContentViewDefinition" />
    /// Definition property is not set.
    /// </summary>
    /// <value>The default name of the layout template.</value>
    protected override string DefaultLayoutTemplateName => MasterThumbnailLightBoxView.layoutTemplatePath;

    /// <summary>
    /// Gets or sets a value indicating whether the video will be auto played.
    /// </summary>
    /// <value><c>true</c> if the video will be auto played; otherwise, <c>false</c>.</value>
    public virtual bool? AutoPlay
    {
      get => !this.autoPlay.HasValue && this.MasterViewDefinition is IVideosViewMasterLightBoxDefinition masterViewDefinition ? masterViewDefinition.AutoPlay : this.autoPlay;
      set => this.autoPlay = value;
    }

    /// <summary>Configures the detail link.</summary>
    /// <param name="singleItemLink">The single item link.</param>
    /// <param name="dataItem">The data item.</param>
    /// <param name="item">The item.</param>
    protected override void ConfigureDetailLink(
      HyperLink singleItemLink,
      Video dataItem,
      RadListViewItem item)
    {
      base.ConfigureDetailLink(singleItemLink, dataItem, item);
      HtmlGenericControl control1 = (HtmlGenericControl) item.FindControl("videoContainer");
      if (control1 != null)
        singleItemLink.NavigateUrl = "#" + control1.ClientID;
      MediaPlayerControl control2 = (MediaPlayerControl) item.FindControl("videoControl");
      if (control2 == null)
        return;
      if (!this.IsBackend() || this.IsPreviewMode())
      {
        control2.SetSilverlightContainerVisibility = false;
        control2.Attributes.Add("sf-media-url", dataItem.MediaUrl);
        control2.Attributes.Add("sf-mime-type", dataItem.MimeType);
        control2.MediaTitle = (string) dataItem.Title;
        control2.MediaDescription = (string) dataItem.Description;
      }
      else
      {
        control2.Controls.Clear();
        control2.Controls.Add((Control) new LiteralControl(Res.Get<VideosResources>().VideoNotAvailableInEditMode));
        control2.PlayerContainer.Visible = false;
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      MasterThumbnailLightBoxView thumbnailLightBoxView = this;
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(thumbnailLightBoxView.GetType().FullName, thumbnailLightBoxView.ClientID);
      if (thumbnailLightBoxView.AutoPlay.HasValue)
        controlDescriptor.AddProperty("videoAutoPlay", (object) thumbnailLightBoxView.AutoPlay.Value);
      yield return (ScriptDescriptor) controlDescriptor;
    }

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
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.Scripts.MasterThumbnailLightBoxView.js", fullName)
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
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.JQueryFancyBox;
  }
}
