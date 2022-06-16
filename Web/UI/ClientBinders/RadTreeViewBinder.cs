// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.RadTreeViewBinder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents ClientBinder implementation for the RadTreeViewBinder control.
  /// </summary>
  [ToolboxBitmap(typeof (RadTreeViewBinder), "Telerik.Sitefinity.Resources.Sitefinity.bmp")]
  [ToolboxData("<{0}:RadTreeViewBinder runat=\"server\"></{0}:RadTreeViewBinder>")]
  [Obsolete("This class is obsolete. Use RadTreeBinder instead.")]
  public class RadTreeViewBinder : ClientBinder
  {
    /// <summary>
    /// Gets the name of the javascript class exposed by the concrete implementation of the
    /// ClientBinder name.
    /// </summary>
    /// <value></value>
    protected override string BinderName => "Telerik.Sitefinity.Web.UI.RadTreeViewBinder";

    /// <summary>Gets/Sets client node expanding handler.</summary>
    public string OnClientNodeExpanding { get; set; }

    /// <summary>Gets/Set tree node bound handler.</summary>
    public string OnClientNodeItemBound { get; set; }

    /// <summary>Gets/Sets the each node item bound complete handler.</summary>
    public string OnClientNodeItemBoundComplete { get; set; }

    /// <summary>Gets /Sets node bound complete handler.</summary>
    public string OnClientNodeBoundComplete { get; set; }

    /// <summary>Gets / Sets the client node click handler.</summary>
    public string OnClientNodeClicked { get; set; }

    /// <summary>Gets/Sets client node expanding handler.</summary>
    public string OnBeforeLoading { get; set; }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      RadTreeView target = this.GetTarget<RadTreeView>();
      target.SingleExpandPath = true;
      target.OnClientNodeExpanding = this.OnClientNodeExpandingScript();
      target.OnClientNodeClicked = this.OnClientNodeClickedScript();
    }

    /// <summary>Gets the default item data bound command script.</summary>
    /// <returns></returns>
    private string OnClientNodeExpandingScript()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("function {0}(sender, args) {{", (object) this.GetClientTargetCommandFunctionName());
      stringBuilder.AppendFormat("$find('{0}').NodeExpanding(sender, args);", (object) this.ClientID);
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }

    /// <summary>Gets the default item data bound command script.</summary>
    /// <returns></returns>
    private string OnClientNodeClickedScript()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("function {0}(sender, args) {{", (object) this.GetClientTargetCommandFunctionName());
      stringBuilder.AppendFormat("$find('{0}').NodeClicked(sender, args);", (object) this.ClientID);
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }

    /// <summary>Gets the name of the client targer command function.</summary>
    /// <returns></returns>
    private string GetClientTargetCommandFunctionName() => this.ClientID + "_NodeExpanding";

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string str = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.Scripts.RadTreeViewBinder.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript)
    /// client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of
    /// <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = (ScriptBehaviorDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      if (!string.IsNullOrEmpty(this.OnClientNodeExpanding))
        behaviorDescriptor.AddEvent("onClientNodeExpanding", this.OnClientNodeExpanding);
      if (!string.IsNullOrEmpty(this.OnClientNodeItemBound))
        behaviorDescriptor.AddEvent("onClientNodeItemBound", this.OnClientNodeItemBound);
      if (!string.IsNullOrEmpty(this.OnClientNodeItemBoundComplete))
        behaviorDescriptor.AddEvent("onClientNodeItemBoundComplete", this.OnClientNodeItemBoundComplete);
      if (!string.IsNullOrEmpty(this.OnClientNodeClicked))
        behaviorDescriptor.AddEvent("onClientNodeClicked", this.OnClientNodeClicked);
      if (!string.IsNullOrEmpty(this.OnClientNodeBoundComplete))
        behaviorDescriptor.AddEvent("onClientNodeBoundComplete", this.OnClientNodeBoundComplete);
      if (!string.IsNullOrEmpty(this.OnBeforeLoading))
        behaviorDescriptor.AddEvent("onBeforeLoading", this.OnBeforeLoading);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }
  }
}
