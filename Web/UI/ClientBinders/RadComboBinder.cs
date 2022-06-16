// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.RadComboBinder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents ClientBinder implementation for the RadCombo control.
  /// </summary>
  [ToolboxBitmap(typeof (RadComboBinder), "Telerik.Sitefinity.Resources.Sitefinity.bmp")]
  [ToolboxData("<{0}:RadComboBinder runat=\"server\"></{0}:RadComboBinder>")]
  public class RadComboBinder : ClientBinder
  {
    private const string radComboBinderScript = "Telerik.Sitefinity.Web.Scripts.RadComboBinder.js";

    /// <summary>
    /// Gets or sets the text of the static item (first item in the combo).
    /// </summary>
    public string StaticItemText { get; set; }

    /// <summary>
    /// Gets or sets the value of the static item (first item in the combo).
    /// </summary>
    public string StaticItemValue { get; set; }

    /// <summary>
    /// Gets or sets the name of field that determines the items level; used in cases
    /// when combo is bound to a hierarchical data.
    /// </summary>
    public string LevelField { get; set; }

    /// <summary>
    /// Gets the name of the javascript class exposed by the concrete implementation of the
    /// ClientBinder name.
    /// </summary>
    /// <value></value>
    protected override string BinderName => typeof (RadComboBinder).FullName;

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
      if (!string.IsNullOrEmpty(this.StaticItemText))
        behaviorDescriptor.AddProperty("_staticItemText", (object) this.StaticItemText);
      if (!string.IsNullOrEmpty(this.StaticItemValue))
        behaviorDescriptor.AddProperty("_staticItemValue", (object) this.StaticItemValue);
      if (!string.IsNullOrEmpty(this.LevelField))
        behaviorDescriptor.AddProperty("_levelField", (object) this.LevelField);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
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
        Name = "Telerik.Sitefinity.Web.Scripts.RadComboBinder.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
