// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailMediaPlayerControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos
{
  /// <summary>Public control for displaying rich media content.</summary>
  [ControlDesigner(typeof (VideoSettingsDesigner))]
  [PropertyEditorTitle(typeof (VideosResources), "MediaPlayerControlPropertyEditorTitle")]
  public class ThumbnailMediaPlayerControl : SimpleScriptView
  {
    private bool setSilverlightContainerVisibility = true;
    private bool isPlayerVisible = true;
    public static readonly string TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.ThumbnailMediaPlayerControl.ascx");
    internal const string Script = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ThumbnailMediaPlayerControl.js";
    internal Type TypeName = typeof (ThumbnailMediaPlayerControl);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailMediaPlayerControl" /> class.
    /// </summary>
    public ThumbnailMediaPlayerControl() => this.LayoutTemplatePath = ThumbnailMediaPlayerControl.TemplatePath;

    /// <summary>Gets or sets the URL of the media content item.</summary>
    /// <value>The MediaUrl.</value>
    public string MediaUrl { get; set; }

    /// <summary>Gets or sets the title of the media content item.</summary>
    /// <value>The MediaTitle.</value>
    public string MediaTitle { get; set; }

    /// <summary>
    /// Gets or sets the Description of the media content item.
    /// </summary>
    /// <value>The MediaDescription.</value>
    public string MediaDescription { get; set; }

    /// <summary>
    /// Gets or sets if the media content item to be autoplayed.
    /// </summary>
    /// <value>The MediaDescription.</value>
    public bool AutoPlay { get; set; }

    /// <summary>Gets or sets the media content id.</summary>
    /// <value>The media content id.</value>
    public Guid MediaContentId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to set silverlight container visibility.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if silverlight container visibility will be explicitly set; otherwise, <c>false</c>.
    /// </value>
    public bool SetSilverlightContainerVisibility
    {
      get => this.setSilverlightContainerVisibility;
      set => this.setSilverlightContainerVisibility = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the reference to the HtmlGenericControl that contains the ThumbnailSelector.
    /// </summary>
    protected internal virtual HtmlGenericControl ThumbnailSelectorContainer => this.Container.GetControl<HtmlGenericControl>("thumbnailSelectorContainer", true);

    /// <summary>Gets the reference to the ThumbnailSelector.</summary>
    protected internal virtual ThumbnailSelector ThumbnailSelector => this.Container.GetControl<ThumbnailSelector>("thumbnailSelector", false);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.SetSilverlightContainerVisibility)
      {
        if (this.IsDesignMode() && !this.IsPreviewMode())
        {
          this.Controls.Clear();
          this.Controls.Add((Control) new LiteralControl(Res.Get<VideosResources>().VideoNotAvailableInEditMode));
          this.isPlayerVisible = false;
        }
        if (this.MediaContentId != Guid.Empty)
        {
          LibrariesManager manager = LibrariesManager.GetManager();
          Guid id = this.MediaContentId;
          if (manager.GetVideos().Where<Video>((Expression<Func<Video, bool>>) (v => v.Id == id)).Where<Video>(PredefinedFilters.PublishedItemsFilter<Video>()).SingleOrDefault<Video>() == null)
          {
            if (this.IsDesignMode())
            {
              this.Controls.Clear();
              this.Controls.Add((Control) new LiteralControl(Res.Get<VideosResources>().VideoWasNotSelectedOrHasBeenDeleted));
            }
            this.isPlayerVisible = false;
          }
        }
        else
          this.isPlayerVisible = false;
      }
      if (this.isPlayerVisible)
        return;
      this.ThumbnailSelector.Visible = false;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.isPlayerVisible)
        return;
      this.ThumbnailSelector.AutoPlay = this.AutoPlay;
      this.ThumbnailSelector.MediaUrl = this.MediaUrl;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      if (this.isPlayerVisible)
      {
        ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
        controlDescriptor.AddProperty("_domain", (object) UrlPath.GetDomainUrl());
        controlDescriptor.AddProperty("url", (object) this.MediaUrl);
        controlDescriptor.AddProperty("title", (object) this.MediaTitle);
        controlDescriptor.AddProperty("description", (object) this.MediaDescription);
        controlDescriptor.AddProperty("autoPlay", (object) this.AutoPlay);
        controlDescriptor.AddElementProperty("playerContainer", this.ThumbnailSelectorContainer.ClientID);
        controlDescriptor.AddComponentProperty("thumbnailSelector", this.ThumbnailSelector.ClientID);
        scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      }
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
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
      if (this.isPlayerVisible)
        scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ThumbnailMediaPlayerControl.js", this.TypeName.Assembly.FullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
