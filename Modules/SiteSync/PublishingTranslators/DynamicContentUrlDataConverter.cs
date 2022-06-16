// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.DynamicContentUrlDataConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Xml.Linq;
using Telerik.Sitefinity.DynamicModules.Model;

namespace Telerik.Sitefinity.SiteSync
{
  internal class DynamicContentUrlDataConverter : UrlDataConverter
  {
    protected override void AddElements(XElement root, object obj)
    {
      base.AddElements(root, obj);
      DynamicContentUrlData dynamicContentUrlData = (DynamicContentUrlData) obj;
      root.Add((object) new XElement((XName) "ItemType", (object) dynamicContentUrlData.ItemType));
    }

    public override object Deserialize(string str, Type type, object settings)
    {
      DynamicContentUrlData dynamicContentUrlData = (DynamicContentUrlData) base.Deserialize(str, type, settings);
      dynamicContentUrlData.ItemType = XDocument.Parse(str).Element((XName) type.FullName).Element((XName) "ItemType").Value;
      return (object) dynamicContentUrlData;
    }
  }
}
