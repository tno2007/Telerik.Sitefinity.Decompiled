// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.ThumbnailMediaPlayerDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>A dialog for displaying media content.</summary>
  public class ThumbnailMediaPlayerDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.ThumbnailMediaPlayerDialog.ascx");
    private const string dialogScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ThumbnailMediaPlayerDialog.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.ThumbnailMediaPlayerDialog" /> class.
    /// </summary>
    public ThumbnailMediaPlayerDialog() => this.LayoutTemplatePath = ThumbnailMediaPlayerDialog.layoutTemplatePath;

    /// <summary>Gets the name of the layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the reference to the media player control.</summary>
    /// <value>The instance of <see cref="P:Telerik.Sitefinity.Modules.Libraries.Web.UI.ThumbnailMediaPlayerDialog.MediaPlayerControl" /> class.</value>
    protected virtual ThumbnailMediaPlayerControl MediaPlayerControl => this.Container.GetControl<ThumbnailMediaPlayerControl>("mediaPlayerControl", true);

    /// <summary>
    /// Gets the reference to the "done" button of the selection dialog
    /// </summary>
    protected internal virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("doneButton", true);

    /// <summary>
    /// Gets the reference to the "cancel" button of the selection dialog
    /// </summary>
    protected internal virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("cancelButton", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.MediaPlayerControl.SetSilverlightContainerVisibility = false;

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (ThumbnailMediaPlayerDialog).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("mediaPlayer", this.MediaPlayerControl.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ThumbnailMediaPlayerDialog.js", typeof (ThumbnailMediaPlayerDialog).Assembly.FullName)
    };
  }
}
