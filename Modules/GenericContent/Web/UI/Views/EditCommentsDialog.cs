// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.Dialogs.EditCommentsDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Xml;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI.Dialogs
{
  /// <summary>Provides UI for editing a comment</summary>
  [Obsolete("The backend UI for comments is configurable via CommentsModuleDefinitions.")]
  public class EditCommentsDialog : AjaxDialogBase
  {
    public static readonly string UiPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.GenericContent.EditCommentDialog.ascx");

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      using (Stream manifestResourceStream = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetManifestResourceStream("Telerik.Sitefinity.Resources.Themes.ToolsFile.xml"))
      {
        XmlDocument doc = new XmlDocument();
        doc.Load(manifestResourceStream);
        this.HtmlEditor.LoadToolsFile(doc);
      }
    }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? EditCommentsDialog.UiPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Reference to the control in the template that edits the comment text
    /// </summary>
    /// <remarks>This control reference is required.</remarks>
    protected virtual RadEditor HtmlEditor => this.Container.GetControl<RadEditor>("htmlEditor", false);
  }
}
