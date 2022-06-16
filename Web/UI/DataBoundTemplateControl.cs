// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DataBoundTemplateControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI
{
  public abstract class DataBoundTemplateControl : WebControl, INamingContainer
  {
    protected abstract ITemplate Template { get; set; }

    protected virtual object DataItem { get; set; }

    protected virtual int DataItemIndex { get; set; }

    protected virtual DataBoundTemplateControl.DataItemTemplateContainer Container { get; set; }

    /// <inheritdoc />
    protected override void OnDataBinding(EventArgs e)
    {
      base.OnDataBinding(e);
      if (this.DataItemContainer is IDataItemContainer dataItemContainer)
      {
        this.DataItem = dataItemContainer.DataItem;
        this.DataItemIndex = dataItemContainer.DataItemIndex;
      }
      this.EnsureChildControls();
    }

    /// <inheritdoc />
    protected override void CreateChildControls()
    {
      if (this.Template == null)
        return;
      this.Container = new DataBoundTemplateControl.DataItemTemplateContainer()
      {
        DataItem = this.DataItem,
        DataItemIndex = this.DataItemIndex
      };
      this.Template.InstantiateIn((Control) this.Container);
      this.Controls.Add((Control) this.Container);
    }

    /// <inheritdoc />
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <inheritdoc />
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    protected class DataItemTemplateContainer : Control, IDataItemContainer, INamingContainer
    {
      public object DataItem { get; set; }

      public int DataItemIndex { get; set; }

      int IDataItemContainer.DisplayIndex => this.DataItemIndex;
    }
  }
}
