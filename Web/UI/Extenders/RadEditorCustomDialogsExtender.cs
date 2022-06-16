// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Extenders
{
  [TargetControlType(typeof (RadEditor))]
  public class RadEditorCustomDialogsExtender : ExtenderControl
  {
    public const string RadEditorCustomDialogExtenderScript = "Telerik.Sitefinity.Web.UI.Extenders.Scripts.RadEditorCustomDialogsExtender.js";
    private string uiCulture;

    /// <summary>
    /// Gets or sets a value indicating whether to override the RadEditor dialogs.
    /// </summary>
    /// <value>
    /// <c>true</c> if to override the RadEditor dialogs; otherwise, <c>false</c>.
    /// </value>
    public bool IsToOverrideDialogs { get; set; }

    /// <summary>Gets the image manager dialog URL.</summary>
    /// <value>The image manager dialog URL.</value>
    public string ImageManagerDialogUrl { get; set; }

    /// <summary>Gets the document manager dialog URL.</summary>
    /// <value>The document manager dialog URL.</value>
    public string DocumentManagerDialogUrl { get; set; }

    /// <summary>Gets the media manager dialog URL.</summary>
    /// <value>The media manager dialog URL.</value>
    public string MediaManagerDialogUrl { get; set; }

    /// <summary>Gets the link manager dialog URL.</summary>
    /// <value>The link manager dialog URL.</value>
    public string LinkManagerDialogUrl { get; set; }

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public string UICulture
    {
      get => this.uiCulture;
      set => this.uiCulture = value;
    }

    /// <summary>
    /// When overridden in a derived class, registers the <see cref="T:System.Web.UI.ScriptDescriptor" />
    /// objects for the control.
    /// </summary>
    /// <param name="targetControl">The server control to which the extender is associated.</param>
    /// <returns>
    /// An enumeration of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors(
      Control targetControl)
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor("Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender", targetControl.ClientID);
      behaviorDescriptor.AddComponentProperty("editor", targetControl.ClientID);
      behaviorDescriptor.AddProperty("uiCulture", (object) this.UICulture);
      behaviorDescriptor.AddProperty("isToOverrideDialogs", (object) this.IsToOverrideDialogs);
      behaviorDescriptor.AddProperty("imageManagerDialogUrl", (object) this.ImageManagerDialogUrl);
      behaviorDescriptor.AddProperty("documentManagerDialogUrl", (object) this.DocumentManagerDialogUrl);
      behaviorDescriptor.AddProperty("mediaManagerDialogUrl", (object) this.MediaManagerDialogUrl);
      behaviorDescriptor.AddProperty("linkManagerDialogUrl", (object) this.LinkManagerDialogUrl);
      yield return (ScriptDescriptor) behaviorDescriptor;
    }

    protected override IEnumerable<ScriptReference> GetScriptReferences()
    {
      yield return new ScriptReference("Telerik.Sitefinity.Web.UI.Extenders.Scripts.RadEditorCustomDialogsExtender.js", typeof (IExpandableControl).Assembly.FullName);
    }
  }
}
