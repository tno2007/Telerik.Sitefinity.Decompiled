// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.RadGridBinder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents ClientBinder implementation for the RadGrid control.
  /// </summary>
  [ToolboxBitmap(typeof (RadGridBinder), "Telerik.Sitefinity.Resources.Sitefinity.bmp")]
  [ToolboxData("<{0}:RadGridBinder runat=\"server\"></{0}:RadGridBinder>")]
  public class RadGridBinder : ClientBinder
  {
    /// <summary>
    /// Gets the name of the javascript class exposed by the concrete implementation of the
    /// ClientBinder name.
    /// </summary>
    /// <value></value>
    protected override string BinderName => "Telerik.Sitefinity.Web.UI.RadGridBinder";

    /// <summary>
    /// Text to display while waiting for asynchronous operations to finish
    /// </summary>
    public virtual string LoadingText => Res.Get<Labels>().Loading;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      RadGrid target = this.GetTarget<RadGrid>();
      target.ClientSettings.ClientEvents.OnDataSourceResolved = "";
      target.ClientSettings.ClientEvents.OnCommand = this.GetClientTargetCommandFunctionName();
      target.Style.Add("display", "none");
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
      ScriptComponentDescriptor componentDescriptor = base.GetScriptDescriptors().OfType<ScriptComponentDescriptor>().Last<ScriptComponentDescriptor>();
      componentDescriptor.AddProperty("_loadingText", (object) this.LoadingText);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) componentDescriptor
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string str = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.Scripts.RadGridBinder.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the
    /// event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        return;
      this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "customScript", this.GetDefaultTargetCommandScript(), true);
    }

    /// <summary>Gets the default target command script.</summary>
    /// <returns></returns>
    private string GetDefaultTargetCommandScript()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("function {0}(sender, args) {{", (object) this.GetClientTargetCommandFunctionName());
      stringBuilder.AppendFormat("$find('{0}').TargetCommand(sender, args);", (object) this.ClientID);
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }

    /// <summary>Gets the name of the client targer command function.</summary>
    /// <returns></returns>
    private string GetClientTargetCommandFunctionName() => this.ClientID + "_TargetCommand";
  }
}
