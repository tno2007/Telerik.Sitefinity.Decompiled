// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ProgressBinder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Class that defines the logic for binding progress</summary>
  public class ProgressBinder : SimpleScriptView
  {
    private static string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ClientBinders.ProgressBinder.ascx");
    internal const string ScriptName = "Telerik.Sitefinity.Web.Scripts.ProgressBinder.js";

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ProgressBinder.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets value indicating the refresh interval</summary>
    public virtual int RefreshInterval { get; set; }

    /// <summary>
    /// Gets or sets value indicating the Url of the service that checks the current state.
    /// </summary>
    public string ProgressServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets label to be displayed after the progress has finished
    /// </summary>
    public virtual string FinishedLabel { get; set; }

    /// <summary>
    /// Gets or sets label to be shown during the progress e.g.(57% CurrentStepLabel)
    /// </summary>
    public virtual string CurrentStepLabel { get; set; }

    /// <summary>
    /// Gets or sets the container id, where the progress bar(s) are placed
    /// </summary>
    public virtual string TargetId { get; set; }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string str = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.Scripts.ProgressBinder.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("_refreshInterval", (object) this.RefreshInterval);
      controlDescriptor.AddProperty("_progressServiceUrl", (object) VirtualPathUtility.ToAbsolute(this.ProgressServiceUrl ?? string.Empty));
      controlDescriptor.AddProperty("_currentStepResource", (object) (this.CurrentStepLabel ?? string.Empty));
      controlDescriptor.AddProperty("_completedResource", (object) (this.FinishedLabel ?? string.Empty));
      controlDescriptor.AddProperty("_targetId", (object) this.TargetId);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }
  }
}
