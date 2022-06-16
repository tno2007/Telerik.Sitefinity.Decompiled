// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentOutPipeDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Publishing.Web.UI.Designers
{
  /// <summary>
  /// The base class for content outbound pipe designer views that contain parent selection.
  /// </summary>
  internal abstract class ContentOutPipeDesignerView : ControlDesignerView
  {
    internal const string controlScript = "Telerik.Sitefinity.Publishing.Web.UI.Scripts.ContentOutPipeDesignerView.js";

    /// <summary>Gets or sets the selected blog post title.</summary>
    /// <value>The selected blog post title.</value>
    protected Label SelectedParentLabel => this.Container.GetControl<Label>("selectedParentContentTitle", true);

    /// <summary>Gets the button blog posts.</summary>
    /// <value>The button blog posts.</value>
    protected LinkButton SelectParentButton => this.Container.GetControl<LinkButton>("btnSelectParent", true);

    /// <summary>Gets or sets the blog posts content selector.</summary>
    /// <value>The blog posts content selector.</value>
    public ContentSelector ParentContentSelector => this.Container.GetControl<ContentSelector>("parentSelector", true);

    /// <summary>
    /// Gets the parent content type used in the parent selector.
    /// </summary>
    /// <value>The type of the selector content.</value>
    protected abstract string SelectorContentType { get; }

    /// <summary>
    /// Gets the selector service URL for the parent selector.
    /// </summary>
    /// <value>The selector service URL.</value>
    protected abstract string SelectorServiceUrl { get; }

    /// <summary>
    /// Gets the message that is shown when no parent content is selected.
    /// </summary>
    /// <value>The content not selected message.</value>
    protected abstract string ContentNotSelectedMessage { get; }

    /// <summary>
    /// Gets the user friendly name of the parent content type.
    /// </summary>
    /// <value>The name of the content type.</value>
    protected abstract string ContentTypeName { get; }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.ParentContentSelector.AllowMultipleSelection = false;
      this.ParentContentSelector.ServiceUrl = this.SelectorServiceUrl;
      this.ParentContentSelector.ItemType = this.SelectorContentType;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (ContentOutPipeDesignerView).FullName, this.ClientID);
      controlDescriptor.AddProperty("contentTypeName", (object) this.ContentTypeName);
      controlDescriptor.AddProperty("buttonParents", (object) this.SelectParentButton.ClientID);
      controlDescriptor.AddComponentProperty("parentContentSelector", this.ParentContentSelector.ClientID);
      controlDescriptor.AddElementProperty("selectedParentContentTitle", this.SelectedParentLabel.ClientID);
      controlDescriptor.AddProperty("_contentNotSelectedMessage", (object) this.ContentNotSelectedMessage);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.ContentOutPipeDesignerView.js", typeof (ContentOutPipeDesignerView).Assembly.FullName)
    };
  }
}
