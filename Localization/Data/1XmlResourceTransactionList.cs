// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Data.XmlResourceEntryCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Telerik.Sitefinity.Localization.Data
{
  /// <summary>
  /// Used to for group and search operations on a collection of <see cref="!:XmlResoureceEntry" />
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class XmlResourceEntryCollection : KeyedCollection<string, XmlResourceEntry>
  {
    protected override string GetKeyForItem(XmlResourceEntry item) => ResourceEntry.GetUniqueKey(item.ClassId, item.Key, item.Culture);

    internal bool TryGetValue(string key, out XmlResourceEntry item)
    {
      if (this.Dictionary != null)
        return this.Dictionary.TryGetValue(key, out item);
      item = (XmlResourceEntry) null;
      return false;
    }
  }
}
