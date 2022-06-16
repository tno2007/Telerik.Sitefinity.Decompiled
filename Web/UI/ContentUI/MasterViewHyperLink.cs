// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.MasterViewHyperLink
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// Represents a hyperlink to master view for ContentView control.
  /// </summary>
  public class MasterViewHyperLink : SitefinityHyperLink
  {
    private const string pageNotAvailable = "The specified page {0} is not available. Please make sure the page exists and you have the right to view it.";

    /// <summary>
    /// Gets or sets the URL or the ID of the master view page to navigate for all items.
    /// </summary>
    /// <value>The master view page URL or ID.</value>
    public string MasterViewPage { get; set; }

    /// <summary>Gets or sets the text data field to bind to.</summary>
    /// <value>The text data field.</value>
    public virtual string TextDataField { get; set; }

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
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (string.IsNullOrEmpty(this.NavigateUrl))
      {
        Guid guid = Guid.Empty;
        if (!string.IsNullOrEmpty(this.MasterViewPage))
        {
          if (ControlUtilities.IsGuid(this.MasterViewPage))
            guid = new Guid(this.MasterViewPage);
          else
            this.NavigateUrl = this.MasterViewPage;
        }
        if (string.IsNullOrEmpty(this.NavigateUrl))
        {
          if (guid == Guid.Empty)
          {
            ContentView hostControl = this.GetHostControl<ContentView>();
            if (hostControl != null && hostControl.DetailViewDefinition != null)
              guid = hostControl.DetailViewDefinition.MasterPageId;
          }
          SiteMapProvider currentProvider = SiteMapBase.GetCurrentProvider();
          if (currentProvider != null)
          {
            SiteMapNode siteMapNode;
            if (guid != Guid.Empty)
            {
              siteMapNode = currentProvider.FindSiteMapNodeFromKey(guid.ToString());
              if (siteMapNode == null)
                throw new InvalidOperationException("The specified page {0} is not available. Please make sure the page exists and you have the right to view it.".Arrange((object) guid));
            }
            else
              siteMapNode = currentProvider.CurrentNode;
            if (siteMapNode != null)
              this.NavigateUrl = siteMapNode.Url;
          }
        }
      }
      if (string.IsNullOrEmpty(this.TextDataField) && string.IsNullOrEmpty(this.ToolTipDataField))
        return;
      object dataItem = (this.GetDataItemContainer() ?? throw new InvalidOperationException("This control can be used only within a data bound item template.")).DataItem;
      if (dataItem == null)
        return;
      if (!string.IsNullOrEmpty(this.TextDataField))
        this.Text = DataBinder.Eval(dataItem, this.TextDataField, "{0}");
      if (string.IsNullOrEmpty(this.ToolTipDataField))
        return;
      this.ToolTip = DataBinder.Eval(dataItem, this.ToolTipDataField, "{0}");
    }
  }
}
