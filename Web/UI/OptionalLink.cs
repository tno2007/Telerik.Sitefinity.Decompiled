// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.OptionalLink
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Displays a link in a data-bound context in several modes, depending on the
  /// text source and navigate url source
  /// </summary>
  /// <remarks>
  /// The behaviour of this control depends on the <c>TextSource</c> and <c>NavigateUrlSource</c>
  /// properties. If both navigate url and text are empty or whitespace, nothing will be rendered.
  /// If there is navigate, but no text, a link will be rendered with text set to the url.
  /// If there is text, but no navigate url, only the text will be shown. In the latter case,
  /// the text will be wrapped in a tag specified by <c>TextTag</c>
  /// </remarks>
  /// <example>
  ///     &lt;asp:Repeater ID="stuff" runat="server"&gt;
  ///        &lt;HeaderTemplate&gt;
  ///            &lt;ul&gt;
  ///        &lt;/HeaderTemplate&gt;
  ///        &lt;ItemTemplate&gt;
  ///            &lt;sf:OptionalLink ID="ss" NavigateUrlSource="LastName" CssClass="ss" TextOnlyCssClass="ssd" TextSource="FirswtName" TextTag="h1" runat="server" /&gt;
  ///        &lt;/ItemTemplate&gt;
  ///        &lt;FooterTemplate&gt;
  ///            &lt;/ul&gt;
  ///        &lt;/FooterTemplate&gt;
  ///    &lt;/asp:Repeater&gt;
  /// </example>
  public class OptionalLink : Control
  {
    /// <summary>Data-bound item. Null of not in data-bound context</summary>
    private object dataItem;

    /// <summary>Css class to be applied on the link</summary>
    public string CssClass { get; set; }

    /// <summary>
    /// Css class to be applied on the text if no link is displayed. This requires the
    /// <see cref="P:Telerik.Sitefinity.Web.UI.OptionalLink.TextTag" /> property to be set.
    /// </summary>
    public string TextOnlyCssClass { get; set; }

    /// <summary>
    /// HTML tag to wrap around the text if there is no link tag displayed
    /// </summary>
    public string TextTag { get; set; }

    /// <summary>
    /// Name of the property whose value would be the text of the link
    /// </summary>
    public string TextSource { get; set; }

    /// <summary>
    /// Name of the property whose value would be the text of the link
    /// </summary>
    public string NavigateUrlSource { get; set; }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnDataBinding(EventArgs e)
    {
      base.OnDataBinding(e);
      IDataItemContainer dataItemContainer = this.GetDataItemContainer();
      if (dataItemContainer == null || dataItemContainer.DataItem == null)
        return;
      this.dataItem = dataItemContainer.DataItem;
    }

    /// <summary>
    /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.
    /// </summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.dataItem == null)
        return;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.dataItem);
      string propertyStringValue1 = this.GetPropertyStringValue(this.TextSource, properties);
      string propertyStringValue2 = this.GetPropertyStringValue(this.NavigateUrlSource, properties);
      if (!propertyStringValue2.IsNullOrWhitespace())
      {
        HtmlAnchor htmlAnchor = new HtmlAnchor();
        htmlAnchor.HRef = propertyStringValue2;
        string str = propertyStringValue1.IsNullOrWhitespace() ? propertyStringValue2 : propertyStringValue1;
        htmlAnchor.InnerText = str;
        if (!this.CssClass.IsNullOrWhitespace())
          htmlAnchor.Attributes.Add("class", this.CssClass);
        htmlAnchor.RenderControl(writer);
      }
      else
      {
        if (propertyStringValue1.IsNullOrWhitespace())
          return;
        if (!this.TextTag.IsNullOrWhitespace())
        {
          HtmlGenericControl htmlGenericControl = new HtmlGenericControl(this.TextTag);
          htmlGenericControl.InnerText = propertyStringValue1;
          if (!this.TextOnlyCssClass.IsNullOrWhitespace())
            htmlGenericControl.Attributes.Add("class", this.TextOnlyCssClass);
          else if (!this.CssClass.IsNullOrWhitespace())
            htmlGenericControl.Attributes.Add("class", this.CssClass);
          htmlGenericControl.RenderControl(writer);
        }
        else
          writer.Write(propertyStringValue1);
      }
    }

    /// <summary>
    /// Same as DataBinder.GetPropertyValue, but uses TypeDescriptors instead
    /// </summary>
    /// <param name="propertyName">Property name to get the value of</param>
    /// <param name="properties">Cahced properties of this.dataItem</param>
    /// <returns>Property value converted to string or string.Empty</returns>
    private string GetPropertyStringValue(
      string propertyName,
      PropertyDescriptorCollection properties)
    {
      if (propertyName != null)
      {
        PropertyDescriptor propertyDescriptor = properties.Find(propertyName, true);
        if (propertyDescriptor != null)
        {
          object obj = propertyDescriptor.GetValue(this.dataItem);
          if (obj != null)
            return obj.ToString();
        }
      }
      return string.Empty;
    }
  }
}
