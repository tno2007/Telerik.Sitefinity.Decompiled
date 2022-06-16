// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.IPublishingXslTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Xml;

namespace Telerik.Sitefinity.Publishing.Web
{
  public interface IPublishingXslTranslator
  {
    /// <summary>Gets the transformed data as HTML string.</summary>
    /// <param name="pipe">The pipe.</param>
    /// <returns></returns>
    string GetTransformedHtml(IPipe pipe);

    /// <summary>Gets the transformed HTML as byte array.</summary>
    /// <param name="pipe">The pipe.</param>
    /// <returns></returns>
    byte[] GetTransformedHtmlAsByteArray(IPipe pipe);

    /// <summary>Gets the transformed HTML.</summary>
    /// <param name="xml">The XML.</param>
    /// <returns></returns>
    string GetTransformedHtml(string xml);

    /// <summary>Transforms an XML string using as XSL transform.</summary>
    /// <param name="xml">The XML.</param>
    /// <param name="xsl">The XSL.</param>
    /// <returns></returns>
    string Transform(string xml, XmlReader xsl);
  }
}
