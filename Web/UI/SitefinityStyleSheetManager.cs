// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SitefinityStyleSheetManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.PublicControls;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  public class SitefinityStyleSheetManager : Control
  {
    protected List<CssEmbedControl> widgetStyleSheets = new List<CssEmbedControl>();
    protected List<StyleSheetDefinition> themeStyleSheets = new List<StyleSheetDefinition>();
    protected List<StyleSheetDefinition> nonThemeStyleSheets = new List<StyleSheetDefinition>();
    private List<StyleSheetDefinition> systemStyleSheets = new List<StyleSheetDefinition>();
    private Dictionary<string, Control> registeredStyles = new Dictionary<string, Control>();

    /// <summary>
    /// This event is fired when all necessary HtmlLink controls have been added to the page.
    /// </summary>
    public event EventHandler StyleSheetsRendered;

    public static SitefinityStyleSheetManager GetCurrent(Page page) => page != null ? page.Items[(object) typeof (SitefinityStyleSheetManager)] as SitefinityStyleSheetManager : throw new ArgumentNullException(nameof (page));

    protected virtual void OnStyleSheetsRendered(EventArgs e)
    {
      EventHandler styleSheetsRendered = this.StyleSheetsRendered;
      if (styleSheetsRendered == null)
        return;
      styleSheetsRendered((object) this, e);
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.Page.PreRenderComplete += new EventHandler(this.Page_PreRenderComplete);
      if (this.DesignMode)
        return;
      if (SitefinityStyleSheetManager.GetCurrent(this.Page) != null)
        throw new InvalidOperationException("There must be only one instance of SitefinityStyleSheetManager per page.");
      this.Page.Items[(object) typeof (SitefinityStyleSheetManager)] = (object) this;
    }

    public void ProcessAdditionalStyleSheets(RadStyleSheetManager styleSheetManager)
    {
      foreach (StyleSheetDefinition styleSheet in this.nonThemeStyleSheets.Where<StyleSheetDefinition>((Func<StyleSheetDefinition, bool>) (ssr => ssr.StyleSheet != null)))
      {
        if (!this.TryRegisterInPage(styleSheet, styleSheetManager))
          styleSheetManager.StyleSheets.Add(styleSheet.StyleSheet);
        styleSheet.IsProcessed = true;
      }
      foreach (StyleSheetDefinition styleSheet in this.themeStyleSheets.Where<StyleSheetDefinition>((Func<StyleSheetDefinition, bool>) (ssr => ssr.StyleSheet != null)))
      {
        if (!this.TryRegisterInPage(styleSheet, styleSheetManager))
          styleSheetManager.StyleSheets.Add(styleSheet.StyleSheet);
        styleSheet.IsProcessed = true;
      }
    }

    protected override void Render(HtmlTextWriter writer)
    {
      base.Render(writer);
      if (this.DesignMode || this.Page.Header != null)
        return;
      this.RenderCssWidgetControls(writer);
    }

    private void Page_PreRenderComplete(object sender, EventArgs e)
    {
      if (this.Page.Header == null)
        return;
      RadStyleSheetManager current = RadStyleSheetManager.GetCurrent(this.Page);
      this.InsertSystemStylesheets(this.systemStyleSheets);
      this.InsertStyleSheets(this.nonThemeStyleSheets, current);
      this.InsertStyleSheets(this.themeStyleSheets, current);
      foreach (CssEmbedControl widgetStyleSheet in this.widgetStyleSheets)
      {
        if (!string.IsNullOrEmpty(widgetStyleSheet.Url))
          this.AddStyleResourceToPage(this.GetLinkControl(widgetStyleSheet.Url, CssEmbedControl.ConvertMediaTypeToString(widgetStyleSheet.MediaType)));
        else if (!string.IsNullOrEmpty(widgetStyleSheet.CustomCssCode))
          this.Page.Header.Controls.Add((Control) new Literal()
          {
            Text = string.Format("<style type=\"text/css\" media=\"{1}\">{0}</style>", (object) widgetStyleSheet.CustomCssCode, (object) CssEmbedControl.ConvertMediaTypeToString(widgetStyleSheet.MediaType))
          });
      }
      this.OnStyleSheetsRendered(new EventArgs());
    }

    protected HtmlLink GetLinkControl(string url, string mediaType)
    {
      HtmlLink linkControl = new HtmlLink();
      linkControl.Href = url;
      linkControl.Attributes.Add("type", "text/css");
      linkControl.Attributes.Add("rel", "stylesheet");
      if (!string.IsNullOrEmpty(mediaType))
        linkControl.Attributes.Add("media", mediaType);
      return linkControl;
    }

    protected virtual void InsertStyleSheets(
      List<StyleSheetDefinition> styleSheets,
      RadStyleSheetManager styleSheetManager)
    {
      foreach (StyleSheetDefinition styleSheet in styleSheets)
      {
        if (!styleSheet.IsProcessed)
        {
          if ((styleSheet.StyleSheet == null || styleSheetManager == null) && !this.TryRegisterInPage(styleSheet, styleSheetManager))
            this.AddStyleResourceToPage(this.GetLinkControl(styleSheet.Url, (string) null));
        }
      }
    }

    private void InsertSystemStylesheets(List<StyleSheetDefinition> styleSheets)
    {
      styleSheets.Reverse();
      foreach (StyleSheetDefinition styleSheet in styleSheets)
      {
        if (!this.TryRegisterInPage(styleSheet, (RadStyleSheetManager) null))
        {
          HtmlLink linkControl = this.GetLinkControl(styleSheet.Url, (string) null);
          string href = linkControl.Href;
          if (!this.registeredStyles.ContainsKey(href))
          {
            this.Page.Header.Controls.AddAt(0, (Control) linkControl);
            this.registeredStyles.Add(href, (Control) linkControl);
          }
        }
      }
    }

    private void AddStyleResourceToPage(HtmlLink control)
    {
      string href = control.Href;
      if (this.registeredStyles.ContainsKey(href))
        return;
      this.Page.Header.Controls.Add((Control) control);
      this.registeredStyles.Add(href, (Control) control);
    }

    protected virtual void RenderCssWidgetControls(HtmlTextWriter writer)
    {
      foreach (CssEmbedControl widgetStyleSheet in this.widgetStyleSheets)
        this.RenderCssWidgetControl(widgetStyleSheet, writer);
    }

    protected virtual void RenderCssWidgetControl(CssEmbedControl cssControl, HtmlTextWriter writer)
    {
      if (!string.IsNullOrEmpty(cssControl.Url))
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
        writer.AddAttribute("media", CssEmbedControl.ConvertMediaTypeToString(cssControl.MediaType));
        writer.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
        writer.AddAttribute(HtmlTextWriterAttribute.Href, cssControl.Url);
        writer.RenderBeginTag(HtmlTextWriterTag.Link);
        writer.RenderEndTag();
      }
      else
      {
        if (string.IsNullOrEmpty(cssControl.CustomCssCode))
          return;
        writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
        writer.AddAttribute("media", CssEmbedControl.ConvertMediaTypeToString(cssControl.MediaType));
        writer.RenderBeginTag(HtmlTextWriterTag.Style);
        writer.WriteEncodedText(cssControl.CustomCssCode);
        writer.RenderEndTag();
      }
    }

    public void RegisterCssWidgetControl(CssEmbedControl c) => this.widgetStyleSheets.Add(c);

    public void RegisterThemeStyleSheet(StyleSheetDefinition styleSheet) => this.themeStyleSheets.Add(styleSheet);

    public void RegisterNonThemeStyleSheet(StyleSheetDefinition styleSheet) => this.nonThemeStyleSheets.Add(styleSheet);

    internal void RegisterSystemStyleSheet(StyleSheetDefinition styleSheet) => this.systemStyleSheets.Add(styleSheet);

    private bool TryRegisterInPage(
      StyleSheetDefinition styleSheet,
      RadStyleSheetManager styleSheetManager)
    {
      if (this.Context.Request.UserAgent != null && (this.Context.Request.UserAgent.Contains("Trident/7.0") || this.Context.Request.UserAgent.Contains("Trident/6.0") || this.Context.Request.UserAgent.Contains("Trident/5.0") || this.Context.Request.UserAgent.Contains("Trident/4.0")) && styleSheet.StyleSheet != null)
      {
        if (styleSheet.StyleSheet.Name.EndsWith("kendo_common_min.css"))
        {
          Type type = TypeResolutionService.ResolveType("Telerik.Sitefinity.Resources.Reference", false);
          if (type == (Type) null)
            return false;
          for (int index = 0; index < 2; ++index)
          {
            string str = "Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.{0}.css".Arrange((object) (index + 1));
            if (styleSheetManager == null)
            {
              this.AddStyleResourceToPage(this.GetLinkControl(UrlPath.ResolveUrl(this.Page.ClientScript.GetWebResourceUrl(type, str), true), (string) null));
            }
            else
            {
              StyleSheetReference styleSheetReference = new StyleSheetReference(str, type.Assembly.FullName);
              styleSheetManager.StyleSheets.Add(styleSheetReference);
            }
          }
          if (!this.registeredStyles.ContainsKey(styleSheet.Url))
            this.registeredStyles.Add(styleSheet.Url, (Control) null);
          return true;
        }
        if (styleSheetManager == null && (styleSheet.StyleSheet.Name.EndsWith("Grid.css") || styleSheet.StyleSheet.Name.EndsWith("Layout.css") || styleSheet.StyleSheet.Name.EndsWith("Colors.css") || styleSheet.StyleSheet.Name.EndsWith("InlineEditing.css") || styleSheet.StyleSheet.Name.EndsWith("PageEditor.css") || styleSheet.StyleSheet.Name.EndsWith("Dock.css")))
        {
          this.AddStyleResourceToPage(this.GetLinkControl(styleSheet.Url, (string) null));
          return true;
        }
      }
      return false;
    }
  }
}
