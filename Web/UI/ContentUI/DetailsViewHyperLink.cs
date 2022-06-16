// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.DetailsViewHyperLink
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.DataResolving;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// Represents a hyperlink to details view for ContentView control.
  /// </summary>
  public class DetailsViewHyperLink : SitefinityHyperLink
  {
    /// <summary>
    /// Gets or sets the text data field to bind to or a list of fields separated with commas.
    /// </summary>
    /// <value>The text data field.</value>
    /// <example>FirstName, LastName</example>
    public virtual string TextDataField { get; set; }

    /// <summary>Gets or sets the</summary>
    public virtual string TextDataFormat { get; set; }

    /// <summary>Gets or sets the tooltip data field to bind to.</summary>
    /// <value>The text data field.</value>
    public virtual string ToolTipDataField { get; set; }

    /// <summary>
    /// Gets or sets the text caption for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The text caption for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control. The default value is an empty string ("").
    /// </returns>
    [PersistenceMode(PersistenceMode.Attribute)]
    public override string Text
    {
      get => base.Text;
      set => base.Text = value;
    }

    /// <summary>
    /// Gets or sets the relevance of the HyperLink.
    /// This will render the rel attribute of the hyperlink which
    /// specifies the relationship between the current document and the linked document.
    /// </summary>
    /// <value>The relevance.</value>
    public string Relevance { get; set; }

    /// <summary>
    /// Gets or sets the data item for data bound controls which do not implement <see cref="T:System.Web.UI.IDataItemContainer" />
    /// </summary>
    /// <value>The data item.</value>
    public object DataItem { get; set; }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnDataBinding(EventArgs e)
    {
      base.OnDataBinding(e);
      object dataItem;
      if (this.DataItem != null)
        dataItem = this.DataItem;
      else
        dataItem = (this.GetDataItemContainer() ?? throw new InvalidOperationException("This control can be used only within a data bound item template.")).DataItem;
      if (dataItem == null)
        return;
      IContentView hostControlLoose = this.GetHostControlLoose<IContentView>();
      IRelatedDataView relatedDataView = hostControlLoose != null ? hostControlLoose as IRelatedDataView : throw new InvalidOperationException("This control must be hosted by ContentView control or one that derives form it.");
      IContentViewMasterDefinition masterViewDefinition = hostControlLoose.MasterViewDefinition;
      string url = relatedDataView == null || !relatedDataView.DisplayRelatedData() || relatedDataView.RelatedDataDefinition.RelatedItemSource == RelatedItemSource.Url || !(dataItem is IDataItem) ? (masterViewDefinition == null || !(masterViewDefinition.DetailsPageId != Guid.Empty) ? DataResolver.Resolve(dataItem, "URL", hostControlLoose.UrlKeyPrefix, (string) null) : DataResolver.Resolve(dataItem, "URL", hostControlLoose.UrlKeyPrefix, masterViewDefinition.DetailsPageId.ToString())) : dataItem.GetDefaultUrl();
      if (Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls)
        this.NavigateUrl = UrlPath.ResolveUrl(url, true);
      else
        this.NavigateUrl = url;
      if (!string.IsNullOrEmpty(this.TextDataField))
      {
        if (string.IsNullOrEmpty(this.TextDataFormat))
          this.TextDataFormat = "{0}";
        string[] strArray = this.TextDataField.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        List<string> stringList = new List<string>();
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (this.IsDesignMode())
          {
            string str = Convert.ToString(DataBinder.Eval(dataItem, strArray[index].Trim(), "{0}"));
            stringList.Add(str);
          }
          else
          {
            string empty = string.Empty;
            try
            {
              empty = Convert.ToString(DataBinder.Eval(dataItem, strArray[index].Trim(), "{0}"));
            }
            catch
            {
            }
            finally
            {
              stringList.Add(empty);
            }
          }
        }
        this.Text = string.Format(this.TextDataFormat, (object[]) stringList.ToArray());
      }
      if (string.IsNullOrEmpty(this.ToolTipDataField))
        return;
      this.ToolTip = DataBinder.Eval(dataItem, this.ToolTipDataField, "{0}");
    }

    /// <summary>
    /// Adds the attributes of a <see cref="T:System.Web.UI.WebControls.HyperLink" /> control to the output stream for rendering.
    /// </summary>
    /// <param name="writer">The output stream to render on the client.</param>
    protected override void AddAttributesToRender(HtmlTextWriter writer)
    {
      base.AddAttributesToRender(writer);
      if (string.IsNullOrEmpty(this.Relevance))
        return;
      writer.AddAttribute("rel", this.Relevance);
    }
  }
}
