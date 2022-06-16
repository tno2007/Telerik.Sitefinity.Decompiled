// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.MediaField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  ///  Represents a media field control. Used for viewing of media content items in Sitefinity modules
  /// </summary>
  public class MediaField : FileField
  {
    private const string itemTemplateName = "MediaDescription";
    private const string resourceAssemblyName = "Telerik.Sitefinity.Resources";
    private const string videoDesriptionTemplate = "Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Videos.VideoItemDescriptionMultiLingualTemplate.htm";
    private const string fieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.MediaField.js";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.MediaField.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.MediaField" /> class.
    /// </summary>
    public MediaField() => this.LayoutTemplatePath = MediaField.layoutTemplatePath;

    /// <summary>Gets the reference to the media player control.</summary>
    /// <value>The instance of <see cref="P:Telerik.Sitefinity.Web.UI.Fields.MediaField.MediaPlayerControl" /> class.</value>
    protected virtual MediaPlayerControl MediaPlayerControl => this.GetConditionalControl<MediaPlayerControl>("mediaPlayerControl", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets or sets a value indicating whether [hide command bar].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [hide command bar]; otherwise, <c>false</c>.
    /// </value>
    public bool HideCommandBar { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.MediaPlayerControl.SetSilverlightContainerVisibility = false;
      if (this.DisplayMode != FieldDisplayMode.Write)
        return;
      this.PrepareMenuItems();
      this.InstantiateTemplate("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Videos.VideoItemDescriptionMultiLingualTemplate.htm", "MediaDescription");
      this.MediaContentItemsList.AllowMultipleSelection = false;
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition) => base.Configure(definition);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddComponentProperty("mediaPlayer", this.MediaPlayerControl.ClientID);
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
      string fullName = typeof (TextField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.MediaField.js", fullName)
      };
    }
  }
}
