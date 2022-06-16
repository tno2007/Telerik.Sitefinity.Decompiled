// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SelectorResultView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// A base class for controls that encapsulate a selector and a result view controls.
  /// </summary>
  public abstract class SelectorResultView : SimpleScriptView
  {
    private const string controlScript = "Telerik.Sitefinity.Web.Scripts.SelectorResultView.js";
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    private bool bindOnLoad = true;

    /// <summary>
    /// Gets or sets the option to automatically bind the selector when the control loads
    /// </summary>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>Gets the control that displays the title.</summary>
    protected virtual ITextControl TitleText => this.Container.GetControl<ITextControl>("lblTitle", false);

    /// <summary>Gets the client binder control.</summary>
    protected virtual ClientBinder ClientBinder => this.Container.GetControl<ClientBinder>("binder", false);

    /// <summary>Gets the control that opens the selector.</summary>
    /// <value>The open selector.</value>
    protected virtual Control OpenSelector => this.Container.GetControl<Control>("openSelector", false);

    /// <summary>Gets the control that contains the selector.</summary>
    protected virtual Control SelectorContainer => this.Container.GetControl<Control>("selectorContainer", false, TraverseMethod.DepthFirst);

    /// <summary>
    /// Gets the RadWindow control that contains the selector.
    /// </summary>
    protected virtual RadWindow SelectorWindow => this.Container.GetControl<RadWindow>("selectorWindow", false);

    /// <summary>
    /// Gets the <see cref="P:Telerik.Sitefinity.Web.UI.SelectorResultView.CommandBar" /> control.
    /// </summary>
    /// <value>The command bar.</value>
    protected virtual CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", false, TraverseMethod.DepthFirst);

    /// <summary>Gets the control that wraps the selector area.</summary>
    protected virtual Control SelectorWrapper => this.Container.GetControl<Control>("selectorWrapper", false);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      if (this.ClientBinder != null)
        controlDescriptor.AddComponentProperty("binder", this.ClientBinder.ClientID);
      if (this.OpenSelector != null)
        controlDescriptor.AddElementProperty("openSelector", this.OpenSelector.ClientID);
      if (this.SelectorWindow != null)
        controlDescriptor.AddComponentProperty("selectorWindow", this.SelectorWindow.ClientID);
      if (this.CommandBar != null)
        controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      if (this.SelectorWrapper != null)
        controlDescriptor.AddElementProperty("selectorWrapper", this.SelectorWrapper.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
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
      string fullName = typeof (SelectorResultView).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new ScriptReference[2]
      {
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.Scripts.SelectorResultView.js"
        },
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources")
      };
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }
  }
}
