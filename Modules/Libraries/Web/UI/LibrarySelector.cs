// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// A control that is used to provide a user with the ability to select one or more libraries.
  /// </summary>
  public class LibrarySelector : ContentSelector
  {
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.LibrarySelector.ascx");
    private const string script = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.LibrarySelector.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelector" /> class.
    /// </summary>
    public LibrarySelector() => this.LayoutTemplatePath = LibrarySelector.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// A localized string representing the item in plural (for example Images).
    /// </summary>
    public virtual string ItemsName { get; set; }

    /// <summary>
    /// A localized string representing the item in singular (for example Image).
    /// </summary>
    public virtual string ItemName { get; set; }

    /// <summary>
    /// A localized string representing the library type (for example Album).
    /// </summary>
    public virtual string LibraryName { get; set; }

    /// <summary>A localized string for a warning message.</summary>
    public virtual string WarningMessage { get; set; }

    public virtual SitefinityLabel WarningMessageSitefinityLabel => this.Container.GetControl<SitefinityLabel>("warningMessageSitefinityLabel", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      if (string.IsNullOrWhiteSpace(this.WarningMessage))
        return;
      this.WarningMessageSitefinityLabel.Text = this.WarningMessage;
      this.WarningMessageSitefinityLabel.Visible = true;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = (ScriptBehaviorDescriptor) this.GetBaseScriptDescriptors().Last<ScriptDescriptor>();
      behaviorDescriptor.AddProperty("_itemsName", (object) this.ItemsName);
      behaviorDescriptor.AddProperty("_itemName", (object) this.ItemName);
      behaviorDescriptor.AddProperty("_libraryTypeName", (object) this.LibraryName);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(this.GetBaseScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.LibrarySelector.js", typeof (LibrarySelector).Assembly.FullName)
    };

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    internal virtual IEnumerable<ScriptReference> GetBaseScriptReferences() => base.GetScriptReferences();
  }
}
