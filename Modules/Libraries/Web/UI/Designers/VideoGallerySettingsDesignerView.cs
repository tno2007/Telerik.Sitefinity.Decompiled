// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGallerySettingsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Represents the Settings view for the Video Gallery control.
  /// </summary>
  public class VideoGallerySettingsDesignerView : MediaContentSettingsDesignerView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Videos.VideoGallerySettingsDesignerView.ascx");
    private const string settingsViewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.VideoGallerySettingsDesignerView.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? VideoGallerySettingsDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => "videoGallerySettingsDesignerView";

    /// <summary>
    /// Gets an array containing the name of all master views.
    /// </summary>
    /// <value></value>
    public override string[] MasterViewNameMap => new string[2]
    {
      "VideosFrontendThumbnailsList",
      "VideosFrontendThumbnailsLightBox"
    };

    /// <summary>
    /// Gets a dictionary containing the ClientID of all showEmbeddingOption controls.
    /// </summary>
    protected virtual IDictionary<string, string> ShowEmbeddingOptionClientIDs => (IDictionary<string, string>) this.Container.GetControls<ChoiceField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (c => c.Key.StartsWith("showEmbeddingOption"))).ToDictionary<KeyValuePair<string, Control>, string, string>((Func<KeyValuePair<string, Control>, string>) (i => i.Key.Substring(i.Key.Length - 1)), (Func<KeyValuePair<string, Control>, string>) (e => e.Value.ClientID));

    /// <summary>
    /// Gets a dictionary containing the ClientID of all showRelatedVideos controls.
    /// </summary>
    protected virtual IDictionary<string, string> ShowRelatedVideosClientIDs => (IDictionary<string, string>) this.Container.GetControls<ChoiceField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (c => c.Key.StartsWith("showRelatedVideos"))).ToDictionary<KeyValuePair<string, Control>, string, string>((Func<KeyValuePair<string, Control>, string>) (i => i.Key.Substring(i.Key.Length - 1)), (Func<KeyValuePair<string, Control>, string>) (e => e.Value.ClientID));

    /// <summary>
    /// Gets a dictionary containing the ClientID of all allowFullSize controls.
    /// </summary>
    protected virtual IDictionary<string, string> AllowFullSizeClientIDs => (IDictionary<string, string>) this.Container.GetControls<ChoiceField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (c => c.Key.StartsWith("allowFullSize"))).ToDictionary<KeyValuePair<string, Control>, string, string>((Func<KeyValuePair<string, Control>, string>) (i => i.Key.Substring(i.Key.Length - 1)), (Func<KeyValuePair<string, Control>, string>) (e => e.Value.ClientID));

    /// <summary>Get the link for editing the thumbnail list template</summary>
    protected virtual HyperLink EditThumbnailListTemplateLink => this.Container.GetControl<HyperLink>("editThumbnailListTemplateLink", true);

    /// <summary>
    /// Gets the link for editing the thumbnail lightbox template
    /// </summary>
    protected virtual HyperLink EditThumbnailLightboxTemplateLink => this.Container.GetControl<HyperLink>("editThumbnailLightboxTemplateLink", true);

    /// <summary>Get the link for editing the detail template</summary>
    protected virtual HyperLink EditDetailTemplateLink => this.Container.GetControl<HyperLink>("editDetailTemplateLink", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_showEmbeddingOptionClientIDs", (object) this.ShowEmbeddingOptionClientIDs);
      controlDescriptor.AddProperty("_showRelatedVideosClientIDs", (object) this.ShowRelatedVideosClientIDs);
      controlDescriptor.AddProperty("_allowFullSizeClientIDs", (object) this.AllowFullSizeClientIDs);
      controlDescriptor.AddElementProperty("editThumbnailListTemplateLink", this.EditThumbnailListTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editThumbnailLightboxTemplateLink", this.EditThumbnailLightboxTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editDetailTemplateLink", this.EditDetailTemplateLink.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          this.EditThumbnailListTemplateLink.ClientID,
          "DB92F414-1C8F-4F43-ABFE-000000000001"
        },
        {
          this.EditThumbnailLightboxTemplateLink.ClientID,
          "DB92F414-1C8F-4F43-ABFE-000000000002"
        },
        {
          this.EditDetailTemplateLink.ClientID,
          "DB92F414-1C8F-4F43-ABFE-000000000003"
        }
      };
      controlDescriptor.AddProperty("_templateLinkIdMap", (object) dictionary);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.VideoGallerySettingsDesignerView.js", typeof (VideoGallerySettingsDesignerView).Assembly.FullName)
    };
  }
}
