// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>Base class to be implemented by all control designers</summary>
  public abstract class ControlDesignerBase : SimpleScriptView
  {
    private const string controlDesignerInterfaceScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IControlDesigner.js";
    private const string controlDesignerBaseScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.ControlDesignerBase.js";

    /// <summary>
    /// Gets or sets the reference to the parent property editor contro
    /// </summary>
    public virtual PropertyEditor PropertyEditor { get; set; }

    /// <summary>
    /// Gets or sets the mode in which the designer ought to be opened
    /// </summary>
    public ControlDesignerModes DesignerMode { get; set; }

    /// <summary>
    /// If set to true, the designer will use the advanced mode by default. This property is ignored
    /// if the DesignerMode property is set to simple.
    /// </summary>
    public bool AdvancedModeIsDefault { get; set; }

    /// <summary>
    /// Gets or sets the pageId of the control that the designer designs.
    /// </summary>
    public Guid ControlId { get; set; }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      if (this.PropertyEditor != null)
        controlDescriptor.AddComponentProperty("propertyEditor", this.PropertyEditor.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
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
      string assembly = typeof (ControlDesignerBase).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IControlDesigner.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.ControlDesignerBase.js", assembly)
      };
    }

    /// <summary>Gets the name of a custom mode for the designer.</summary>
    protected internal virtual string GetCustomMode() => SystemManager.CurrentHttpContext.Request.QueryString["mode"]?.ToLower();

    /// <summary>
    /// Calls <see cref="M:Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase.Customize(System.String)" /> with the nme of the custom mode when set, or <c>null</c> otherwise.
    /// </summary>
    protected internal virtual void SetupCustomMode() => this.Customize(this.GetCustomMode());

    /// <summary>
    /// Called from <see cref="M:Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase.SetupCustomMode" />, when it is called.
    /// </summary>
    /// <param name="mode">The nme of the custom mode when set, or <c>null</c> otherwise.</param>
    protected virtual void Customize(string mode)
    {
    }

    /// <summary>Retrieves the UI culture from the PropertyEditor.</summary>
    /// <returns></returns>
    protected virtual string GetUICulture()
    {
      string uiCulture = (string) null;
      if (this.PropertyEditor != null)
        uiCulture = this.PropertyEditor.PropertyValuesCulture;
      return uiCulture;
    }
  }
}
