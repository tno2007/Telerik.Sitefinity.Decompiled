// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.PublishingXslTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Publishing.Web
{
  /// <summary>
  /// Responsible for transforming xml data retrieved via an instance of type <see cref="T:Telerik.Sitefinity.Publishing.Web.IFeedFormatter" /> to html string.
  /// </summary>
  public class PublishingXslTranslator : IPublishingXslTranslator
  {
    private readonly IFeedFormatter feedFormatter;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.PublishingXslTranslator" /> class.
    /// </summary>
    /// <param name="feedFormatter">The feed formatter.</param>
    public PublishingXslTranslator(IFeedFormatter feedFormatter) => this.feedFormatter = feedFormatter;

    /// <summary>Gets the transformed data as HTML string.</summary>
    /// <param name="pipe">The pipe.</param>
    /// <returns></returns>
    public virtual string GetTransformedHtml(IPipe pipe) => this.GetTransformedHtml(this.feedFormatter.GetFeedXml(pipe));

    /// <summary>Gets the transformed HTML as byte array.</summary>
    /// <param name="pipe">The pipe.</param>
    /// <returns></returns>
    public virtual byte[] GetTransformedHtmlAsByteArray(IPipe pipe) => PublishingXslTranslator.StringToByteArray(this.GetTransformedHtml(pipe));

    /// <summary>Gets the transformed HTML.</summary>
    /// <param name="xml">The XML.</param>
    /// <returns></returns>
    public virtual string GetTransformedHtml(string xml)
    {
      string transformedHtml = string.Empty;
      using (Stream xslStream = this.GetXslStream())
      {
        using (XmlReader xsl = XmlReader.Create(xslStream))
        {
          transformedHtml = this.Transform(xml, xsl);
          xslStream.Close();
        }
      }
      return transformedHtml;
    }

    /// <summary>Transforms an XML string using as XSL transform.</summary>
    /// <param name="xml">The XML.</param>
    /// <param name="xsl">The XSL.</param>
    /// <returns></returns>
    public virtual string Transform(string xml, XmlReader xsl)
    {
      XPathDocument input = new XPathDocument((TextReader) new StringReader(xml));
      string empty = string.Empty;
      XslCompiledTransform compiledTransform = new XslCompiledTransform();
      compiledTransform.Load(xsl);
      using (StringWriter w = new StringWriter())
      {
        using (XmlTextWriter results = new XmlTextWriter((TextWriter) w))
        {
          compiledTransform.Transform((IXPathNavigable) input, (XsltArgumentList) null, (XmlWriter) results);
          results.Close();
        }
        return w.ToString();
      }
    }

    /// <summary>Gets the XSL virtual path.</summary>
    /// <returns></returns>
    protected virtual string GetXslVirtualPath() => ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Rss.feed.xsl");

    /// <summary>Gets the XSL stream.</summary>
    /// <returns></returns>
    protected internal virtual Stream GetXslStream() => VirtualPathManager.OpenFile(this.GetXslVirtualPath());

    /// <summary>Converts a string to byte array.</summary>
    /// <param name="str">The STR.</param>
    /// <returns></returns>
    private static byte[] StringToByteArray(string str) => new UTF8Encoding().GetBytes(str);
  }
}
