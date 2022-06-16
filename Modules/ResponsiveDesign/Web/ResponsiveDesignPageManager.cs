// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.ResponsiveDesignPageManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Data;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web
{
  /// <summary>
  /// This control provides the Sitefinity pages with the responsive design functionality.
  /// </summary>
  public class ResponsiveDesignPageManager : Control
  {
    private ResponsiveDesignManager responsiveDesignManager;

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Modules.ResponsiveDesign.Web.ResponsiveDesignPageManager.ResponsiveDesignManager" />.
    /// </summary>
    protected virtual ResponsiveDesignManager ResponsiveDesignManager
    {
      get
      {
        if (this.responsiveDesignManager == null)
          this.responsiveDesignManager = ResponsiveDesignManager.GetManager();
        return this.responsiveDesignManager;
      }
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.RegisterToWriteAdditionalCssTags();
    }

    /// <summary>
    /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" />
    /// object, which writes the content to be rendered on the client.
    /// </summary>
    /// <param name="writer">
    /// The <see cref="T:System.Web.UI.HtmlTextWriter" /> object
    /// that receives the server control content.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!Config.Get<ResponsiveDesignConfig>().Enabled)
        return;
      writer.AddAttribute("name", "viewport");
      writer.AddAttribute("content", "width=device-width, initial-scale=1.0");
      writer.RenderBeginTag(HtmlTextWriterTag.Meta);
      writer.RenderEndTag();
      if (!this.Page.IsBackend() && !this.Page.IsDesignMode())
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
        writer.RenderBeginTag(HtmlTextWriterTag.Script);
        this.GetScript((TextWriter) writer);
        writer.RenderEndTag();
      }
      this.RegisterToWriteAdditionalCssTags();
    }

    private void GetScript(TextWriter writer) => RelocationScriptsCache.GetInstance().GetRelocationScripts(this.GetPageDataProxy(), SystemManager.CurrentContext.Culture, writer);

    private PageDataProxy GetPageDataProxy()
    {
      PageSiteNode actualCurrentNode = SiteMapBase.GetActualCurrentNode();
      PageDataProxy pageDataProxy = (PageDataProxy) null;
      if (actualCurrentNode != null)
        pageDataProxy = actualCurrentNode.CurrentPageDataItem;
      return pageDataProxy;
    }

    private void RegisterToWriteAdditionalCssTags()
    {
      SitefinityStyleSheetManager current = SitefinityStyleSheetManager.GetCurrent(this.Page);
      if (current == null)
        return;
      current.StyleSheetsRendered += new EventHandler(this.styleSheetManager_StyleSheetsRendered);
    }

    private void styleSheetManager_StyleSheetsRendered(object sender, EventArgs e) => this.WriteAdditionalCssLinks();

    private void WriteAdditionalCssLinks()
    {
      foreach (CssRulesCache.Item obj in CssRulesCache.GetInstance().GetItems(this.GetPageDataProxy()))
      {
        HtmlLink child = new HtmlLink();
        child.Href = obj.Url;
        child.Attributes.Add("type", "text/css");
        child.Attributes.Add("rel", "stylesheet");
        child.Attributes.Add("media", obj.MediaRule);
        this.Page.Header.Controls.Add((Control) child);
      }
    }
  }
}
