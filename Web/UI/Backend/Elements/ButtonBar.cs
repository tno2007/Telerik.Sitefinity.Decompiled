// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.ButtonBar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements
{
  /// <summary>
  /// Represents an extended widget bar container of widget controls that will handle <see cref="!:StartProcessingCommand" /> and
  /// <see cref="!:EndProcessingCommand" /> command events.
  /// </summary>
  public class ButtonBar : WidgetBar
  {
    internal const string OnEndProcessingEvent = "onEndProcessingCommand";
    internal const string OnStartProcessingEvent = "onStartProcessingCommand";
    internal const string IAsyncCommandSenderScript = "Telerik.Sitefinity.Web.Scripts.IAsyncCommandSender.js";
    internal const string ButtonBarScript = "Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.ButtonBar.js";

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client OnStartProcessingevent is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public virtual string OnStartProcessingCommand { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client OnEndProcessing event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public virtual string OnEndProcessingCommand { get; set; }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor scriptDescriptor = this.GetLastBaseScriptDescriptor();
      if (!string.IsNullOrEmpty(this.OnStartProcessingCommand))
        scriptDescriptor.AddEvent("onStartProcessingCommand", this.OnStartProcessingCommand);
      if (!string.IsNullOrEmpty(this.OnEndProcessingCommand))
        scriptDescriptor.AddEvent("onEndProcessingCommand", this.OnEndProcessingCommand);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        scriptDescriptor
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
      string fullName = this.GetType().Assembly.FullName;
      List<ScriptReference> scriptReferenceList = new List<ScriptReference>(base.GetScriptReferences());
      ScriptReference scriptReference1 = new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.ButtonBar.js"
      };
      ScriptReference scriptReference2 = new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.Scripts.IAsyncCommandSender.js"
      };
      scriptReferenceList.Add(scriptReference1);
      scriptReferenceList.Add(scriptReference2);
      return (IEnumerable<ScriptReference>) scriptReferenceList.ToArray();
    }

    internal virtual ScriptBehaviorDescriptor GetLastBaseScriptDescriptor() => base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptBehaviorDescriptor;
  }
}
