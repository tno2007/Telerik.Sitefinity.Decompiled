// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Data.XmlResouceEntryContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using Telerik.Sitefinity.Xml;

namespace Telerik.Sitefinity.Localization.Data
{
  /// <summary>
  /// Used to track changes on a xml resource entry collection.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class XmlResouceEntryContext : XmlResourceEntryCollection
  {
    protected override void RemoveItem(int index) => this[index].DataStatus = DataItemStatus.Deleted;
  }
}
