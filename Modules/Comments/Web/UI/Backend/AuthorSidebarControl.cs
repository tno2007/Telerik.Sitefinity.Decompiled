// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.AuthorSidebarControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Backend
{
  public class AuthorSidebarControl : SimpleScriptView
  {
    internal static readonly string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Backend.Comments.AuthorSidebarControl.ascx";
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Scripts.AuthorSidebarControl.js";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath(AuthorSidebarControl.layoutTemplateName);

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? AuthorSidebarControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the reference to the client label manager.</summary>
    protected ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the reference to the comment status label.</summary>
    protected Label CommentStatusLabel => this.Container.GetControl<Label>("commentStatusLabel", false);

    /// <summary>Gets the reference to the comment age label.</summary>
    protected Label CommentAgeLabel => this.Container.GetControl<Label>("commentAgeLabel", false);

    /// <summary>Gets the reference to the comment author panel.</summary>
    protected Panel AuthorPanel => this.Container.GetControl<Panel>("authorPanel", false);

    /// <summary>Gets the reference to the comment author link.</summary>
    protected Label AuthorLink => this.Container.GetControl<Label>("authorLink", false);

    /// <summary>
    /// Gets the reference to the comment author avatar image.
    /// </summary>
    protected Image AuthorAvatarImage => this.Container.GetControl<Image>("authorAvatarImage", false);

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      if (this.CommentStatusLabel != null)
        controlDescriptor.AddElementProperty("commentStatusLabel", this.CommentStatusLabel.ClientID);
      if (this.CommentAgeLabel != null)
        controlDescriptor.AddElementProperty("commentAgeLabel", this.CommentAgeLabel.ClientID);
      if (this.AuthorPanel != null)
        controlDescriptor.AddElementProperty("authorPanel", this.AuthorPanel.ClientID);
      if (this.AuthorLink != null)
        controlDescriptor.AddElementProperty("authorLink", this.AuthorLink.ClientID);
      if (this.AuthorAvatarImage != null)
        controlDescriptor.AddElementProperty("authorAvatarImage", this.AuthorAvatarImage.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Scripts.AuthorSidebarControl.js", typeof (AuthorSidebarControl).Assembly.FullName)
    };

    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
