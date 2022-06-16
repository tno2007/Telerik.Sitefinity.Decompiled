// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Maybe.ConditionalTemplateHolder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Web.UI.Maybe
{
  /// <summary>Summary description for ConditionalTemplate</summary>
  /// <example>
  ///    &lt;asp:Repeater ID="stuff" runat="server"&gt;
  ///        &lt;ItemTemplate&gt;
  ///            &lt;sf:ConditionalTemplateHolder ID="wtf" runat="server"&gt;
  ///                &lt;sf:ConditionalTemplate LeftOperand="FirstName" Condition="IsDefined" runat="server"&gt;
  ///                    &lt;Template&gt;
  ///                    &lt;asp:Literal ID="ss" runat="server" Text='&lt;%# "Hello, " + Eval("FirstName") %&gt;' /&gt;
  ///                    &lt;/Template&gt;
  /// 
  ///                &lt;/sf:ConditionalTemplate&gt;
  ///                &lt;sf:ConditionalTemplate LeftOperand="Age" Condition="IsNotDefaultValue" runat="server"&gt;
  ///                    &lt;asp:Literal ID="ss1" runat="server" Text="Hello, again" /&gt;
  ///                &lt;/sf:ConditionalTemplate&gt;
  ///            &lt;/sf:ConditionalTemplateHolder&gt;
  ///        &lt;/ItemTemplate&gt;
  ///    &lt;/asp:Repeater&gt;
  /// </example>
  [ParseChildren(true, DefaultProperty = "Templates")]
  public class ConditionalTemplateHolder : WebControl, INamingContainer
  {
    private List<int> templateIndicesToRender = new List<int>();
    private Collection<ConditionalTemplate> templates;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      foreach (ConditionalTemplate template in this.Templates)
      {
        if (template.Template != null)
          template.Template.InstantiateIn((Control) template);
      }
    }

    /// <summary>All conditional tempaltes</summary>
    [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
    public Collection<ConditionalTemplate> Templates
    {
      get
      {
        if (this.templates == null)
          this.templates = new Collection<ConditionalTemplate>();
        return this.templates;
      }
    }

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
      object dataItem = dataItemContainer.DataItem;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
      for (int index = 0; index < this.Templates.Count; ++index)
      {
        ConditionalTemplate template = this.Templates[index];
        template.DataItem = dataItem;
        template.DataItemIndex = dataItemContainer.DataItemIndex;
        template.DisplayIndex = dataItemContainer.DisplayIndex;
        template.DataBind();
        PropertyDescriptor propertyDescriptor = properties.Find(template.LeftOperand, true);
        switch (template.Condition)
        {
          case RenderConditions.Default:
            this.templateIndicesToRender.Add(index);
            break;
          case RenderConditions.IsDefined:
            if (propertyDescriptor != null)
            {
              this.templateIndicesToRender.Add(index);
              break;
            }
            break;
          case RenderConditions.IsNotDefined:
            if (propertyDescriptor == null)
            {
              this.templateIndicesToRender.Add(index);
              break;
            }
            break;
          case RenderConditions.IsDefaultValue:
            if (propertyDescriptor != null && Utility.IsNullOrDefaultValue(propertyDescriptor.GetValue(dataItem), propertyDescriptor.PropertyType))
            {
              this.templateIndicesToRender.Add(index);
              break;
            }
            break;
          case RenderConditions.IsNotDefaultValue:
            if (propertyDescriptor != null && !Utility.IsNullOrDefaultValue(propertyDescriptor.GetValue(dataItem), propertyDescriptor.PropertyType))
            {
              this.templateIndicesToRender.Add(index);
              break;
            }
            break;
          case RenderConditions.IsNotWhitespaceString:
            if (propertyDescriptor != null)
            {
              object obj = propertyDescriptor.GetValue(dataItem);
              if (!(obj != null ? obj.ToString() : string.Empty).IsNullOrWhitespace())
              {
                this.templateIndicesToRender.Add(index);
                break;
              }
              break;
            }
            break;
          case RenderConditions.IsWhitespaceString:
            if (propertyDescriptor != null)
            {
              object obj = propertyDescriptor.GetValue(dataItem);
              if ((obj != null ? obj.ToString() : string.Empty).IsNullOrWhitespace())
              {
                this.templateIndicesToRender.Add(index);
                break;
              }
              break;
            }
            break;
          default:
            throw new NotImplementedException();
        }
      }
    }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    /// <summary>Renders the control to the specified HTML writer.</summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      base.Render(writer);
      foreach (int index in this.templateIndicesToRender)
      {
        ConditionalTemplate template = this.Templates[index];
        if (template.Template != null)
          template.RenderControl(writer);
      }
    }
  }
}
