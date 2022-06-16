// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ItemLists.ItemsList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ItemLists
{
  /// <summary>
  /// Implements <see cref="T:Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase" /> with <see cref="T:Telerik.Web.UI.RadGrid" /> and <see cref="T:Telerik.Sitefinity.Web.UI.RadGridBinder" />
  /// </summary>
  /// <remarks>
  /// <para>
  ///     ItemsGrid ignores WrapperTagName and WrapperClientId. Instead, it sets
  ///     WrapperTagCssClass to its inner RadGrid element.
  ///     Note that the class specified by WrapperCssClass is appended to the existing value
  ///     in RadGrid.CssClass.
  /// </para>
  /// </remarks>
  public class ItemsList : ItemsListBase
  {
    private const string scriptControlPath = "Telerik.Sitefinity.Web.UI.ItemLists.Scripts.ItemsList.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ItemLists.ItemsList.ascx");

    /// <summary>Reference to the client binder</summary>
    public virtual Pager Pager => this.Container.GetControl<Pager>("pager", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      Control control = this.Container.GetControl<Control>("ItemContainer", true);
      ((RadListViewBinder) this.Binder).TargetLayoutContainerId = control.ClientID;
      if (control is HtmlGenericControl htmlGenericControl)
      {
        string attribute = htmlGenericControl.Attributes["class"];
        string str = !attribute.IsNullOrWhitespace() ? attribute + " " + this.WrapperTagCssClass : this.WrapperTagCssClass;
        htmlGenericControl.Attributes["class"] = str;
      }
      base.OnPreRender(e);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      RadListView listView = this.ListView;
      listView.CssClass = listView.CssClass + " " + this.WrapperTagCssClass;
      this.ListView.AllowPaging = this.AllowPaging;
      this.ListView.PageSize = this.PageSize;
      if (this.AllowPaging)
      {
        this.Pager.NavigationMode = PagerNavigationModes.ClientSide;
        this.Pager.PageSize = this.PageSize;
      }
      else
        this.Pager.Visible = false;
      base.InitializeControls(container);
    }

    /// <summary>Creates the binder container.</summary>
    /// <param name="item">The item.</param>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    protected override BinderContainer CreateBinderContainer(
      ItemDescription item,
      int index)
    {
      BinderContainer binderContainer = base.CreateBinderContainer(item, index);
      binderContainer.ContainerTag = HtmlTextWriterTag.Li;
      binderContainer.TemplateHolderTag = HtmlTextWriterTag.Ol;
      return binderContainer;
    }

    /// <summary>
    /// Determines wheter ItemsListBase should override Render and manually insert
    /// a wrapper tag that uses <see cref="!:WrapperTagName" />, <see cref="!:WrapperTagClientId" />
    /// and <see cref="!:WrapperTagCssClass" />
    /// </summary>
    protected override bool AutoInsertWrapperTag => false;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ItemsList.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Reference to the grid containing the data items</summary>
    protected internal RadListView ListView => this.Container.GetControl<RadListView>("listView", true);

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("pagerId", (object) this.Pager.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ItemLists.Scripts.ItemsList.js", this.GetType().Assembly.FullName)
    };
  }
}
