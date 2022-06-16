// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ProfileImageUploadField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  ///  Represents a file field control for presenting and managing image files in the profile.
  /// </summary>
  public class ProfileImageUploadField : FileField
  {
    private string providerName;
    private Guid albumId;
    private string replacePhotoButtonLabel;
    private string deletePhotoButtonLabel;
    internal const string fieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ProfileImageUploadField.js";
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ProfileImageUploadField.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ProfileImageUploadField" /> class.
    /// </summary>
    public ProfileImageUploadField() => this.LayoutTemplatePath = ProfileImageUploadField.layoutTemplateName;

    /// <summary>
    /// Gets the reference image control which should be displayed
    /// </summary>
    protected internal virtual HtmlImage ImageControl => this.GetConditionalControl<HtmlImage>("img", true);

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the name of the provider used for the Album in which the image is stored.
    /// </summary>
    public string ProviderName
    {
      get => this.providerName;
      set => this.providerName = value;
    }

    /// <summary>
    /// Gets or sets the ID of the Album in which the image is stored.
    /// </summary>
    public Guid AlbumId
    {
      get => this.albumId;
      set => this.albumId = value;
    }

    /// <summary>
    /// Gets or sets the text of the label for the Replace photo button.
    /// </summary>
    public string ReplacePhotoButtonLabel
    {
      get => this.replacePhotoButtonLabel;
      set => this.replacePhotoButtonLabel = value;
    }

    /// <summary>
    /// Gets or sets the text of the label for the Delete photo button.
    /// </summary>
    public string DeletePhotoButtonLabel
    {
      get => this.deletePhotoButtonLabel;
      set => this.deletePhotoButtonLabel = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
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
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      (scriptDescriptors.Last<ScriptDescriptor>() as ScriptControlDescriptor).AddElementProperty("imageControl", this.ImageControl.ClientID);
      return scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = this.GetType().Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ProfileImageUploadField.js", fullName)
      };
    }
  }
}
