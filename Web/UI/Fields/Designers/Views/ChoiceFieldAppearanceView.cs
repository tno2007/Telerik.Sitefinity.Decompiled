// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldAppearanceView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.ModuleEditor.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views;

namespace Telerik.Sitefinity.Web.UI.Fields.Designers.Views
{
  /// <summary>The appearance view of ChoiceFieldDesigner</summary>
  public class ChoiceFieldAppearanceView : FormChoiceFieldColumnsAppearanceView
  {
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Fields.ChoiceFieldAppearanceView.ascx");
    internal new const string designerViewJs = "Telerik.Sitefinity.Web.UI.Fields.Designers.Scripts.ChoiceFieldAppearanceView.js";

    public ChoiceFieldAppearanceView() => this.LayoutTemplatePath = ChoiceFieldAppearanceView.layoutTemplatePath;

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => this.GetType().Name;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the views selector.</summary>
    /// <value>The views selector.</value>
    protected virtual ViewsSelector ViewsSelector => this.Container.GetControl<ViewsSelector>("viewsSelector", true);

    /// <summary>
    /// Represents the choicefield for setting the size of the texbox
    /// </summary>
    protected override ChoiceField ColumnsModeChoiceField => this.Container.GetControl<ChoiceField>("columnsModeChoiceField", false);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("viewsSelector", this.ViewsSelector.ClientID);
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
      string fullName = typeof (ChoiceFieldAppearanceView).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Designers.Scripts.ChoiceFieldAppearanceView.js", fullName)
      };
    }
  }
}
