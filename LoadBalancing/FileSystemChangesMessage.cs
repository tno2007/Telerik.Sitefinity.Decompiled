// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.FileSystemChangesMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// NLB sync. message representing changes on the file system.
  /// </summary>
  public class FileSystemChangesMessage : SystemMessageBase
  {
    private const string eltChanges = "FileSystemChanges";
    private const string eltChange = "Change";
    private const string attrType = "Type";
    private const string attrItemType = "ItemType";
    private const string attrPath = "Path";
    private const string attrTimestamp = "Timestamp";
    private const string dateTimeFormat = "s";

    /// <summary>Creates a new instance.</summary>
    public FileSystemChangesMessage() => this.Key = "FileSystemChanges";

    /// <summary>Creates a new instance from a colleciton of changes.</summary>
    public FileSystemChangesMessage(params FileSystemChange[] changes)
      : this()
    {
      this.Changes = (IEnumerable<FileSystemChange>) changes;
    }

    /// <summary>
    /// Creates a new instance by deserializing an XML representation.
    /// </summary>
    /// <param name="data">XML of a serialized <see cref="T:Telerik.Sitefinity.LoadBalancing.FileSystemChangesMessage" />.</param>
    public FileSystemChangesMessage(string data)
      : this()
    {
      this.Deserialize(data);
    }

    /// <summary>A collection of file system changes.</summary>
    public virtual IEnumerable<FileSystemChange> Changes { get; set; }

    /// <inheritdoc />
    public override string MessageData
    {
      get => this.Serialize();
      set => this.Deserialize(value);
    }

    /// <summary>Returns an XML representation of the object.</summary>
    public string Serialize()
    {
      XDocument xdocument = new XDocument(new object[1]
      {
        (object) new XElement((XName) "Change")
      });
      foreach (FileSystemChange change in this.Changes)
        xdocument.Root.Add((object) new XElement((XName) "FileSystemChanges", new object[4]
        {
          (object) new XAttribute((XName) "Type", (object) change.Type),
          (object) new XAttribute((XName) "ItemType", (object) change.ItemType),
          (object) new XAttribute((XName) "Path", (object) change.Path),
          (object) new XAttribute((XName) "Timestamp", (object) change.Timestamp.ToString("s"))
        }));
      return xdocument.ToString();
    }

    private void Deserialize(string data)
    {
      List<FileSystemChange> fileSystemChangeList = new List<FileSystemChange>();
      using (StringReader stringReader = new StringReader(data))
      {
        foreach (XElement element in XDocument.Load((TextReader) stringReader).Root.Elements())
          fileSystemChangeList.Add(new FileSystemChange()
          {
            Type = (FileSystemChangeType) Enum.Parse(typeof (FileSystemChangeType), element.Attribute((XName) "Type").Value),
            ItemType = (FileSystemItemType) Enum.Parse(typeof (FileSystemItemType), element.Attribute((XName) "ItemType").Value),
            Path = element.Attribute((XName) "Path").Value,
            Timestamp = DateTime.Parse(element.Attribute((XName) "Timestamp").Value)
          });
      }
      this.Changes = (IEnumerable<FileSystemChange>) fileSystemChangeList;
    }
  }
}
