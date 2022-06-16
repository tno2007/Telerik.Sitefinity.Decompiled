// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.LicenseCustomerInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Telerik.Sitefinity.Licensing
{
  /// <summary>
  /// Keeps identification and contact information about the customer the license was issued to
  /// </summary>
  public class LicenseCustomerInfo
  {
    /// <summary>Gets or sets the pageId.</summary>
    /// <value>The pageId.</value>
    public string Id { get; set; }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>Gets or sets the email.</summary>
    /// <value>The email.</value>
    public string Email { get; set; }

    /// <summary>Gets or sets the company name.</summary>
    /// <value>The company name.</value>
    public string CompanyName { get; set; }

    internal void LoadXml(string xml) => this.LoadXml(XElement.Load(XmlReader.Create((TextReader) new StringReader(xml))));

    internal void LoadXml(XElement xml)
    {
      XElement xelement1 = xml.Descendants((XName) "Id").FirstOrDefault<XElement>();
      if (xelement1 != null)
        this.Id = xelement1.Value;
      XElement xelement2 = xml.Descendants((XName) "Name").FirstOrDefault<XElement>();
      if (xelement2 != null)
        this.Name = xelement2.Value;
      XElement xelement3 = xml.Descendants((XName) "Email").FirstOrDefault<XElement>();
      if (xelement3 != null)
        this.Email = xelement3.Value;
      XElement xelement4 = xml.Descendants((XName) "CompanyName").FirstOrDefault<XElement>();
      if (xelement4 == null)
        return;
      this.CompanyName = xelement4.Value;
    }

    protected internal XElement ToXmlElement()
    {
      XElement xmlElement = new XElement((XName) "Customer");
      xmlElement.Add((object) new XElement((XName) "Id", (object) this.Id));
      xmlElement.Add((object) new XElement((XName) "Name", (object) this.Name));
      xmlElement.Add((object) new XElement((XName) "Email", (object) this.Email));
      xmlElement.Add((object) new XElement((XName) "CompanyName", (object) this.CompanyName));
      return xmlElement;
    }

    protected internal string ToXmlString() => this.ToXmlElement().ToString(SaveOptions.DisableFormatting);
  }
}
