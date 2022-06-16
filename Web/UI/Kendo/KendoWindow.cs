// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Kendo.KendoWindow
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Sitefinity.Web.UI.Kendo
{
  /// <summary>Abstract class for dialogs opened in KendoWindow</summary>
  public abstract class KendoWindow : KendoView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.Kendo.Scripts.KendoWindow.js";

    /// <summary>
    /// Gets a reference to the outer div containing the window content.
    /// </summary>
    protected abstract HtmlContainerControl OuterDiv { get; }

    /// <summary>Gets or sets the width of the window.</summary>
    public virtual int Width { get; set; }

    /// <summary>Gets or sets the height of the window.</summary>
    public virtual int Height { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this window is resizable.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this window is resizable; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsResizable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this window is modal.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this window is modal; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsModal { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      container.Controls.Add((Control) new ResourceLinks()
      {
        UseEmbeddedThemes = true,
        Theme = "Default",
        Links = {
          new ResourceFile()
          {
            Name = "Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css",
            Static = true
          }
        }
      });
      container.Controls.Add((Control) new ResourceLinks()
      {
        Links = {
          new ResourceFile() { Name = "Styles/Window.css" }
        }
      });
      this.OuterDiv.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddElementProperty("outerDiv", this.OuterDiv.ClientID);
      controlDescriptor.AddProperty("width", (object) this.Width);
      controlDescriptor.AddProperty("height", (object) this.Height);
      controlDescriptor.AddProperty("isResizable", (object) this.IsResizable);
      controlDescriptor.AddProperty("isModal", (object) this.IsModal);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Kendo.Scripts.KendoWindow.js", typeof (KendoWindow).Assembly.FullName)
    };
  }
}
