// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.LicensedModuleInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Telerik.Sitefinity.Licensing
{
  /// <summary>
  /// Keeps information like Id and name about a licensed module
  /// </summary>
  public class LicensedModuleInfo
  {
    private string sid;

    /// <summary>Gets or sets the module id.</summary>
    /// <value>The module unique id.</value>
    public Guid Id { get; internal set; }

    /// <summary>Gets or sets the module name.</summary>
    /// <value>The module user friendly name.</value>
    public string Name { get; internal set; }

    /// <summary>
    /// Gets the module id in string format and caches it for performance reasons to avoid conversions and creation of new string objects.
    /// </summary>
    /// <value>The module id in string format</value>
    internal string Sid
    {
      get
      {
        if (this.sid == null)
          this.sid = !(this.Id != Guid.Empty) ? string.Empty : this.Id.ToString();
        return this.sid;
      }
    }

    internal LicensedModuleInfo Clone() => new LicensedModuleInfo()
    {
      Id = this.Id,
      Name = this.Name,
      sid = this.sid
    };

    internal void LoadXml(string xml) => this.LoadXml(XElement.Load(XmlReader.Create((TextReader) new StringReader(xml))));

    internal void LoadXml(XElement xml)
    {
      XElement xelement1 = xml.Descendants((XName) "Id").FirstOrDefault<XElement>();
      if (xelement1 != null)
        this.Id = new Guid(xelement1.Value);
      XElement xelement2 = xml.Descendants((XName) "Name").FirstOrDefault<XElement>();
      if (xelement2 == null)
        return;
      this.Name = xelement2.Value;
    }

    protected internal XElement ToXmlElement()
    {
      XElement xmlElement = new XElement((XName) "Module");
      xmlElement.Add((object) new XElement((XName) "Id", (object) this.Id));
      xmlElement.Add((object) new XElement((XName) "Name", (object) this.Name));
      return xmlElement;
    }

    protected internal string ToXmlString() => this.ToXmlElement().ToString(SaveOptions.DisableFormatting);
  }
}
