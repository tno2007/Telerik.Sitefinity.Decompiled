// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicDetailContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Frontend
{
  /// <summary>
  /// This control is a boundable control that
  /// displays the dynamic content detail item.
  /// </summary>
  public class DynamicDetailContainer : CompositeDataBoundControl
  {
    private ITemplate layoutTemplate;

    /// <summary>Gets or sets the layout template of this control.</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (DetailItem))]
    public ITemplate LayoutTemplate
    {
      get => this.layoutTemplate;
      set => this.layoutTemplate = value;
    }

    /// <summary>
    /// When overridden in an abstract class, creates the control hierarchy
    /// that is used to render the composite data-bound control based on the values from
    /// the specified data source.
    /// </summary>
    /// <returns>
    /// The number of items created by the <see cref="M:System.Web.UI.WebControls.CompositeDataBoundControl.CreateChildControls(System.Collections.IEnumerable,System.Boolean)" />.
    /// </returns>
    /// <param name="dataSource">
    /// An <see cref="T:System.Collections.IEnumerable" /> that
    /// contains the values to bind to the control.
    /// </param>
    /// <param name="dataBinding">
    /// true to indicate that the <see cref="M:System.Web.UI.WebControls.CompositeDataBoundControl.CreateChildControls(System.Collections.IEnumerable,System.Boolean)" />
    /// is called during data binding; otherwise, false.
    /// </param>
    protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
    {
      this.Controls.Clear();
      int childControls = 0;
      if (dataBinding && dataSource != null)
      {
        foreach (object dataItem in dataSource)
        {
          if (this.LayoutTemplate != null)
          {
            DetailItem detailItem = new DetailItem(dataItem, childControls++);
            this.LayoutTemplate.InstantiateIn((Control) detailItem);
            this.Controls.Add((Control) detailItem);
            detailItem.DataBind();
          }
        }
      }
      return childControls;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
