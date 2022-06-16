// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Data.XmlResourceEntry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Telerik.Sitefinity.Xml;

namespace Telerik.Sitefinity.Localization.Data
{
  /// <summary>Represents resource entry that is stored in XML file.</summary>
  [DataContract]
  public sealed class XmlResourceEntry : ResourceEntry
  {
    private DataItemStatus status;
    private bool trackingStarted;

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Data.XmlResourceEntry" />
    /// </summary>
    /// <param name="classId">The key of the resource set this entry belongs to.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized.
    /// </param>
    /// <param name="key">The key by which this entry is accessed.</param>
    public XmlResourceEntry(string classId, CultureInfo culture, string key)
      : base(classId, culture, key)
    {
      this.trackingStarted = true;
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" />
    /// </summary>
    /// <param name="classId">The key of the resource set this entry belongs to.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized.
    /// </param>
    /// <param name="element">XML element containing resource entry data.</param>
    public XmlResourceEntry(string classId, CultureInfo culture, XElement element)
      : base(classId, culture, (string) null)
    {
      this.Init(element);
      this.trackingStarted = true;
    }

    /// <summary>Gets or sets the value for this entry.</summary>
    public override string Value
    {
      get => string.IsNullOrEmpty(base.Value) ? string.Empty : base.Value;
      set
      {
        string oldValue = base.Value;
        base.Value = value;
        this.TrackPropertyChanged(oldValue, value);
      }
    }

    /// <summary>Gets or sets description for this entry.</summary>
    public override string Description
    {
      get => string.IsNullOrEmpty(base.Description) ? string.Empty : base.Description;
      set
      {
        string description = base.Description;
        base.Description = value;
        this.TrackPropertyChanged(description, value);
      }
    }

    /// <summary>Get or sets data item status.</summary>
    public DataItemStatus DataStatus
    {
      get => this.status;
      set => this.status = value;
    }

    private void Init(XElement element)
    {
      this.Key = (string) element.Attribute((XName) "name");
      this.Value = (string) element.Element((XName) "value");
      this.Description = (string) element.Element((XName) "comment");
      if (element.Element((XName) "builtIn") == null)
        this.BuiltIn = false;
      else
        this.BuiltIn = (bool) element.Element((XName) "builtIn");
      string s = (string) element.Attribute((XName) "lastModified");
      if (string.IsNullOrEmpty(s))
        s = (string) element.Element((XName) "lastModified");
      this.LastModified = string.IsNullOrEmpty(s) ? DateTime.MinValue : DateTime.Parse(s);
    }

    private void TrackPropertyChanged(string oldValue, string newValue)
    {
      if (!(oldValue != newValue))
        return;
      switch (this.DataStatus)
      {
        case DataItemStatus.Undefined:
        case DataItemStatus.Loaded:
          this.DataStatus = DataItemStatus.Changed;
          break;
      }
    }

    private XElement ToXElement()
    {
      XElement xelement = new XElement((XName) "data", (object) new XAttribute((XName) "name", (object) this.Key));
      XElement content1 = xelement.Element((XName) "value");
      if (content1 == null)
      {
        content1 = new XElement((XName) "value");
        xelement.Add((object) content1);
      }
      content1.Value = this.Value;
      XElement content2 = xelement.Element((XName) "comment");
      if (content2 == null)
      {
        content2 = new XElement((XName) "comment");
        xelement.Add((object) content2);
      }
      content2.Value = this.Description;
      XAttribute xattribute = xelement.Attribute((XName) "lastModified");
      if (xattribute == null)
      {
        XAttribute content3 = new XAttribute((XName) "lastModified", (object) this.LastModified.ToString("s"));
        xelement.Add((object) content3);
      }
      else
        xattribute.Value = this.LastModified.ToString("s");
      return xelement;
    }

    /// <summary>Gets or sets the XML element that stores the data.</summary>
    /// <value>The XML element.</value>
    [Obsolete]
    public XElement XmlElement
    {
      get => this.ToXElement();
      set => this.Init(value);
    }
  }
}
