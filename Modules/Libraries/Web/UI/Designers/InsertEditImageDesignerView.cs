// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.InsertEditImageDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A control representing designer view for editing and inserting an Image. This designer view is used in the ImageControl designer i.e. the ImageSettingsDesigner class.
  /// </summary>
  public class InsertEditImageDesignerView : ContentViewDesignerView
  {
    private readonly string _viewName = "insertEditImageDesignerView";
    internal const string ImageSelectorJs = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.InsertEditImageDesignerView.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.InsertEditImageDesignerView.ascx");

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => this._viewName;

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => "Insert/Edit Image";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? InsertEditImageDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// The control that actually that manages the editing of the image.
    /// </summary>
    protected virtual InsertEditImageView EditImageView => this.Container.GetControl<InsertEditImageView>("editImageView", true);

    /// <summary>
    /// Gets a reference to the ClientLabelManager of the designer.
    /// </summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("editImageView", this.EditImageView.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
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
      string fullName = typeof (InsertEditImageDesignerView).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.InsertEditImageDesignerView.js"
        }
      };
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.ParentDesigner.PropertyEditor.Control is ImageControl control)
      {
        this.EditImageView.ImageId = control.ImageId;
        this.EditImageView.ProviderName = control.ProviderName;
        this.EditImageView.AlternateText = control.AlternateText;
        this.EditImageView.ThumbnailName = control.ThumbnailName;
        this.EditImageView.ToolTip = control.ToolTip;
        this.EditImageView.Alignment = control.Alignment;
        this.EditImageView.MarginTop = control.MarginTop;
        this.EditImageView.MarginBottom = control.MarginBottom;
        this.EditImageView.MarginLeft = control.MarginLeft;
        this.EditImageView.MarginRight = control.MarginRight;
        this.EditImageView.OpenOriginalImageOnClick = control.OpenOriginalImageOnClick;
      }
      this.EditImageView.ShowAlignmentOptions = false;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
