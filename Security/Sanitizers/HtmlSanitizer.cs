// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Sanitizers.HtmlSanitizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using AngleSharp;
using AngleSharp.Dom;
using Ganss.XSS;
using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Security.Sanitizers
{
  /// <inheritdoc />
  public class HtmlSanitizer : IHtmlSanitizer
  {
    private HtmlSanitizer.GanssHtmlSanitizer sanitizer = new HtmlSanitizer.GanssHtmlSanitizer();

    /// <summary>Gets the internal sanitizer object being used.</summary>
    /// <value>The sanitizer object.</value>
    protected object SanitizerObject => (object) this.sanitizer;

    /// <summary>
    /// Gets the allowed HTTP schemes such as "http" and "https".
    /// </summary>
    /// <value>The allowed HTTP schemes.</value>
    protected ISet<string> AllowedSchemes => this.sanitizer.AllowedSchemes;

    /// <summary>
    /// Gets the allowed HTML tag names such as "a" and "div".
    /// </summary>
    /// <value>The allowed tag names.</value>
    protected ISet<string> AllowedTags => this.sanitizer.AllowedTags;

    /// <summary>
    /// Gets the HTML attributes that can contain a URI such as "href".
    /// </summary>
    /// <value>The URI attributes.</value>
    protected ISet<string> UriAttributes => this.sanitizer.UriAttributes;

    /// <summary>
    /// Gets the allowed CSS properties such as "font" and "margin".
    /// </summary>
    /// <value>The allowed CSS properties.</value>
    protected ISet<string> AllowedCssProperties => this.sanitizer.AllowedCssProperties;

    /// <summary>
    /// Gets the allowed HTML attributes such as "href" and "alt".
    /// </summary>
    /// <value>The allowed HTML attributes.</value>
    protected ISet<string> AllowedAttributes => this.sanitizer.AllowedAttributes;

    /// <inheritdoc />
    public HtmlSanitizer()
    {
      this.sanitizer.AllowDataAttributes = true;
      this.AllowedAttributes.Add("controls");
      this.AllowedAttributes.Add("class");
      this.AllowedAttributes.Add("id");
      this.AllowedAttributes.Add("sfref");
      this.AllowedTags.Add("video");
      this.AllowedTags.Add("audio");
      this.AllowedTags.Add("source");
      this.AllowedTags.Add("svg");
      this.AllowedTags.Add("use");
      this.AllowedSchemes.Add("mailto");
    }

    /// <inheritdoc />
    public string Sanitize(string html) => html != null ? this.sanitizer.Sanitize(html, "", (IMarkupFormatter) null) : html;

    /// <inheritdoc />
    public string SanitizeUrl(string url) => url != null ? this.sanitizer.SanitizeUrl(url) : url;

    private class GanssHtmlSanitizer : Ganss.XSS.HtmlSanitizer
    {
      private const string IframeNodeName = "iframe";

      public GanssHtmlSanitizer()
        : base()
      {
        this.AllowedTags.Add("iframe");
        this.PostProcessNode += new EventHandler<PostProcessNodeEventArgs>(this.GanssHtmlSanitizer_PostProcessNode);
        this.RemovingAttribute += new EventHandler<RemovingAttributeEventArgs>(this.GanssHtmlSanitizer_RemovingAttribute);
      }

      private void GanssHtmlSanitizer_PostProcessNode(object sender, PostProcessNodeEventArgs e)
      {
        if (string.Compare(e.Node.NodeName, "iframe", true) != 0 || !(e.Node is IElement node))
          return;
        if (!node.HasAttribute("sandbox"))
          node.SetAttribute("sandbox", "allow-scripts allow-same-origin");
        string attribute = node.GetAttribute("src");
        node.SetAttribute("src", this.SanitizeUrl(attribute));
      }

      private void GanssHtmlSanitizer_RemovingAttribute(object sender, RemovingAttributeEventArgs e)
      {
        if (!e.Attribute.Name.StartsWith("sfdi-", StringComparison.OrdinalIgnoreCase))
          return;
        e.Cancel = true;
      }

      /// <inheritdoc />
      public string SanitizeUrl(string url) => this.SanitizeUrl(url, (string) null);
    }
  }
}
