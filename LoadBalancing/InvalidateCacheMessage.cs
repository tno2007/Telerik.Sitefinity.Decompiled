// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.InvalidateCacheMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// Data transfer object for sending cache invalidation message.
  /// </summary>
  public class InvalidateCacheMessage : SystemMessageBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.InvalidateCacheMessage" /> class.
    /// </summary>
    public InvalidateCacheMessage() => this.Key = "invalidateCacheKey";

    public InvalidateCacheMessage(IEnumerable<CacheDependencyKey> items, string connection = null)
      : this()
    {
      this.ExpiredItems = items.ToList<CacheDependencyKey>();
      this.Connection = connection;
    }

    public virtual List<CacheDependencyKey> ExpiredItems { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.InvalidateCacheMessage" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public InvalidateCacheMessage(string data) => this.Deserialzie(data);

    /// <summary>Gets or sets the message data.</summary>
    /// <value>The message data.</value>
    public override string MessageData
    {
      get => this.Serialize();
      set => this.Deserialzie(value);
    }

    protected IEnumerable<CacheDependencyKey> GetFilteredNotifiedObjects() => this.ExpiredItems.Distinct<CacheDependencyKey>((IEqualityComparer<CacheDependencyKey>) new InvalidateCacheMessage.CacheDependencyKeyEqualityComparer());

    private string Serialize()
    {
      XDocument xdocument = new XDocument();
      using (XmlWriter writer = xdocument.CreateWriter())
      {
        writer.WriteStartElement(nameof (InvalidateCacheMessage));
        foreach (CacheDependencyKey filteredNotifiedObject in this.GetFilteredNotifiedObjects())
        {
          if (filteredNotifiedObject.Key != null || filteredNotifiedObject.Type != (Type) null)
          {
            writer.WriteStartElement("NotifiedObject");
            if (filteredNotifiedObject.Key != null)
            {
              writer.WriteStartAttribute("Key");
              writer.WriteString(filteredNotifiedObject.Key);
              writer.WriteEndAttribute();
            }
            if (filteredNotifiedObject.Type != (Type) null)
            {
              writer.WriteStartAttribute("Type");
              writer.WriteString(filteredNotifiedObject.Type.GetResolvableTypeName());
              writer.WriteEndAttribute();
            }
            writer.WriteEndElement();
          }
        }
        writer.WriteEndElement();
        writer.Flush();
      }
      return xdocument.ToString();
    }

    private void Deserialzie(string value)
    {
      using (TextReader textReader = (TextReader) new StringReader(value))
      {
        IEnumerable<XElement> xelements = XDocument.Load(textReader).Root.Elements((XName) "NotifiedObject");
        this.ExpiredItems = new List<CacheDependencyKey>();
        foreach (XElement xelement in xelements)
        {
          XAttribute xattribute1 = xelement.Attribute((XName) "Key");
          XAttribute xattribute2 = xelement.Attribute((XName) "Type");
          if (xattribute1 != null || xattribute2 != null)
          {
            CacheDependencyKey cacheDependencyKey = new CacheDependencyKey();
            if (xattribute2 != null && xattribute2.Value != null)
            {
              Type type = TypeResolutionService.ResolveType(xattribute2.Value, false);
              if (type == (Type) null)
                type = Type.GetType(xattribute2.Value);
              if (type != (Type) null)
                cacheDependencyKey.Type = type;
            }
            if (xattribute1 != null && xattribute1.Value != null)
              cacheDependencyKey.Key = xattribute1.Value;
            this.ExpiredItems.Add(cacheDependencyKey);
          }
        }
      }
    }

    private class CacheDependencyKeyEqualityComparer : IEqualityComparer<CacheDependencyKey>
    {
      public bool Equals(CacheDependencyKey x, CacheDependencyKey y) => x.Type == y.Type && x.Key == y.Key;

      public int GetHashCode(CacheDependencyKey item) => ((item.Type == (Type) null ? string.Empty : item.Type.FullName) + "-" + item.Key).GetHashCode();
    }
  }
}
