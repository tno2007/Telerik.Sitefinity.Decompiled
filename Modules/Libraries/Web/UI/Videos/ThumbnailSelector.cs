// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos
{
  /// <summary>
  /// A view which lets you capture a thumbnail image from a video.
  /// </summary>
  public class ThumbnailSelector : SimpleScriptView
  {
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.ThumbnailSelector.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ThumbnailSelector.js";

    /// <summary>Obsolete. Use LayoutTemplatePath instead.</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ThumbnailSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets if the media content item to be autoplayed.
    /// </summary>
    /// <value>The MediaDescription.</value>
    public bool AutoPlay { get; set; }

    /// <summary>Gets or sets the URL of the media content item.</summary>
    /// <value>The MediaUrl.</value>
    public string MediaUrl { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the reference to the HtmlGenericControl that contains a video/audio player supporting HTML5 media formats.
    /// </summary>
    protected virtual RadMediaPlayer MediaPlayer => this.Container.GetControl<RadMediaPlayer>("mediaPlayer", true);

    /// <summary>
    /// Gets the reference to the error HtmlGenericControl that is displayed whenever the MediaPlayer cannot open a file.
    /// </summary>
    protected virtual HtmlGenericControl MediaPlayerError => this.Container.GetControl<HtmlGenericControl>("unableToPlayVideo", false);

    /// <summary>Gets the button for getting the current frame.</summary>
    /// <value>The get current frame button.</value>
    protected virtual HtmlAnchor CurrentFrameButton => this.Container.GetControl<HtmlAnchor>("currentFrameButton", true);

    /// <summary>
    /// Gets the container element which holds the thumbnail preview image.
    /// </summary>
    /// <value>The container element which holds the thumbnail preview image.</value>
    protected virtual HtmlGenericControl SelectedThumbnailContainer => this.Container.GetControl<HtmlGenericControl>("thumbnailContainer", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.MediaPlayer.AutoPlay = this.AutoPlay;
      if (string.IsNullOrEmpty(this.MediaUrl))
        return;
      this.MediaPlayer.Source = this.MediaUrl;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      ThumbnailSelector thumbnailSelector = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(thumbnailSelector.ScriptDescriptorTypeName, thumbnailSelector.ClientID);
      controlDescriptor.AddComponentProperty("mediaPlayer", thumbnailSelector.MediaPlayer.ClientID);
      controlDescriptor.AddElementProperty("mediaPlayerError", thumbnailSelector.MediaPlayerError.ClientID);
      controlDescriptor.AddElementProperty("currentFrameButton", thumbnailSelector.CurrentFrameButton.ClientID);
      controlDescriptor.AddElementProperty("selectedThumbnailContainer", thumbnailSelector.SelectedThumbnailContainer.ClientID);
      controlDescriptor.AddProperty("videoAutoPlay", (object) thumbnailSelector.AutoPlay);
      controlDescriptor.AddProperty("videoUrl", (object) thumbnailSelector.MediaUrl);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (ScriptDescriptor) controlDescriptor;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ThumbnailSelector.js", typeof (ThumbnailSelector).Assembly.FullName)
    };

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
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.Empty;
  }
}
