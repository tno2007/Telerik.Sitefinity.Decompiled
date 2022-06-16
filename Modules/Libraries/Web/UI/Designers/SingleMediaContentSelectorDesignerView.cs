// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentSelectorDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Base designer view for selecting a single media content item. Used as base
  /// for selecting a single image, video or document in the designers for Video, Image and Document controls
  /// </summary>
  [Obsolete("This class is not in use anymore.")]
  public class SingleMediaContentSelectorDesignerView : ContentViewDesignerView
  {
    private bool bindOnLoad = true;
    private RadListBoxBinder libraryBinder;
    private RadListBox libraryListBox;
    internal const string MediaContentSelectorJs = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentSelectorDesignerView.js";
    internal const string IDesignerViewControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the control will be bound on load.
    /// Default value is true.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the control is bind on load; otherwise, <c>false</c>.
    /// </value>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>Gets the image controls client binder.</summary>
    public RadListBoxBinder LibraryBinder
    {
      get
      {
        if (this.libraryBinder == null)
          this.libraryBinder = this.Container.GetControl<RadListBoxBinder>("libraryBinder", true);
        return this.libraryBinder;
      }
    }

    /// <summary>
    /// Reference to the RadListBox control that binds to the album data.
    /// </summary>
    protected RadListBox LibraryListBox
    {
      get
      {
        if (this.libraryListBox == null)
          this.libraryListBox = this.Container.GetControl<RadListBox>("libraryListBox", true);
        return this.libraryListBox;
      }
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.LibraryBinder.Provider = this.ProviderName;
      this.LibraryBinder.BindOnLoad &= this.BindOnLoad;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
    {
      this.GetMediaContentSelectorViewDescriptor()
    };

    /// <summary>Gets the media content selector view descriptor.</summary>
    protected ScriptDescriptor GetMediaContentSelectorViewDescriptor()
    {
      ScriptBehaviorDescriptor selectorViewDescriptor = new ScriptBehaviorDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      selectorViewDescriptor.AddComponentProperty("libraryBinder", this.LibraryBinder.ClientID);
      selectorViewDescriptor.AddComponentProperty("libraryListBox", this.LibraryListBox.ClientID);
      selectorViewDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad);
      return (ScriptDescriptor) selectorViewDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => this.GetMediaContentSelectorViewScriptReferences();

    /// <summary>
    /// Gets the media content selector view script references.
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<ScriptReference> GetMediaContentSelectorViewScriptReferences()
    {
      string fullName = typeof (SingleMediaContentSelectorDesignerView).Assembly.FullName;
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      ScriptReference scriptReference1 = new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentSelectorDesignerView.js"
      };
      ScriptReference scriptReference2 = new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js"
      };
      scriptReferences.Add(scriptReference1);
      scriptReferences.Add(scriptReference2);
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
