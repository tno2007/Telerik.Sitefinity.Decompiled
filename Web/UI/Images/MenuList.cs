// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Images.MenuList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Images
{
  /// <summary>This control represents menu with different choices.</summary>
  public class MenuList : SimpleScriptView
  {
    private List<MenuListItem> items;
    /// <summary>Layout template path</summary>
    public static readonly string TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Images.MenuList.ascx");
    /// <summary>Script path</summary>
    public const string ScriptPath = "Telerik.Sitefinity.Web.Scripts.MenuList.js";

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MenuList.TemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the items collection of the menu control.</summary>
    public List<MenuListItem> Items
    {
      get
      {
        if (this.items == null)
          this.items = new List<MenuListItem>();
        return this.items;
      }
    }

    /// <summary>Gets the menu control</summary>
    public RadMenu Menu => this.Container.GetControl<RadMenu>("optionsMenu", true);

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
    {
      (ScriptDescriptor) new ScriptBehaviorDescriptor(typeof (MenuList).FullName, this.ClientID)
    };

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.MenuList.js", typeof (MenuList).Assembly.FullName)
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">Container in which the controls should be initialized.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      RadMenuItem radMenuItem1 = new RadMenuItem("Options");
      foreach (MenuListItem menuListItem in this.Items)
      {
        RadMenuItem radMenuItem2 = new RadMenuItem(menuListItem.Text);
        radMenuItem2.Attributes["command"] = menuListItem.Command;
        if (menuListItem.Command == "replace")
          radMenuItem2.OuterCssClass = "sfSimpleSeparator";
        radMenuItem1.Items.Add(radMenuItem2);
      }
      this.Menu.Items.Add(radMenuItem1);
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
