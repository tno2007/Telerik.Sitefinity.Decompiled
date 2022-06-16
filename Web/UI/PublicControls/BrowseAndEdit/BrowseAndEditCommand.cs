// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditCommand
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  /// <summary>
  /// Represents the command which will be executed for browse-and-edit
  /// </summary>
  [DataContract]
  public class BrowseAndEditCommand
  {
    private BrowseAndEditCommandArgs arguments;
    private bool visible;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditCommand" /> class.
    /// </summary>
    public BrowseAndEditCommand() => this.visible = true;

    /// <summary>Gets or sets the name of the command.</summary>
    /// <value>The name of the command.</value>
    [DataMember]
    public string CommandName { get; set; }

    /// <summary>Title of the command to be shown in the toolbar</summary>
    [ScriptIgnore]
    public string CommandTitle { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditCommand" /> is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    public bool Visible
    {
      get => this.visible;
      set => this.visible = value;
    }

    /// <summary>Gets or sets the arguments.</summary>
    /// <value>The arguments.</value>
    [DataMember]
    public BrowseAndEditCommandArgs Arguments
    {
      get
      {
        if (this.arguments == null)
          this.arguments = new BrowseAndEditCommandArgs();
        return this.arguments;
      }
      set => this.arguments = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the toolbar will use the page permissions for security checks.
    /// </summary>
    /// <value><c>true</c> if the toolbar will use the page permissions for security checks; otherwise, <c>false</c>.</value>
    [ScriptIgnore]
    public bool UsesPagePermissions { get; set; }
  }
}
