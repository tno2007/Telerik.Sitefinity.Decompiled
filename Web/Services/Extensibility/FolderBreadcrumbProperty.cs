// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.FolderBreadcrumbProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A property that represents the folder's breadcrumb</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class FolderBreadcrumbProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (BreadcrumbItem[]);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      LibrariesManager librariesManager = manager as LibrariesManager;
      List<Folder> list = items.OfType<Folder>().ToList<Folder>();
      Dictionary<Guid, List<BreadcrumbItem>> dictionary1 = new Dictionary<Guid, List<BreadcrumbItem>>();
      Dictionary<Guid, string> dictionary2 = new Dictionary<Guid, string>();
      foreach (Folder key in list)
      {
        List<BreadcrumbItem> breadcrumbItemList = new List<BreadcrumbItem>();
        bool flag = false;
        for (Folder parent = key.Parent; parent != null; parent = parent.Parent)
        {
          breadcrumbItemList.Add(new BreadcrumbItem()
          {
            Title = (string) parent.Title,
            FolderId = parent.Id
          });
          if (dictionary1.ContainsKey(parent.Id))
          {
            breadcrumbItemList.AddRange((IEnumerable<BreadcrumbItem>) dictionary1[parent.Id]);
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          if (key.RootId != Guid.Empty && !dictionary2.ContainsKey(key.RootId))
          {
            Lstring title = librariesManager.GetLibrary(key.RootId).Title;
            dictionary2.Add(key.RootId, (string) title);
          }
          string str = (string) null;
          if (dictionary2.TryGetValue(key.RootId, out str))
            breadcrumbItemList.Add(new BreadcrumbItem()
            {
              Title = str,
              FolderId = key.RootId
            });
        }
        dictionary1.Add(key.Id, breadcrumbItemList);
        values.Add((object) key, (object) ((IEnumerable<BreadcrumbItem>) breadcrumbItemList.ToArray()).Reverse<BreadcrumbItem>());
      }
      return (IDictionary<object, object>) values;
    }
  }
}
