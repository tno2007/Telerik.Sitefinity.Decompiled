// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility.ChildrenCountProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility
{
  /// <summary>A property that determine if a page has children</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class ChildrenCountProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (int);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<Guid, IFolder> dictionary = items.OfType<IFolder>().ToDictionary<IFolder, Guid, IFolder>((Func<IFolder, Guid>) (x => x.Id), (Func<IFolder, IFolder>) (y => y));
      List<Guid> currentFolderIds = dictionary.Keys.ToList<Guid>();
      IEnumerable<IGrouping<Guid, \u003C\u003Ef__AnonymousType16<Guid>>> groupings = ((manager as LibrariesManager).Provider as IFolderOAProvider).GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (x => x.ParentId == new Guid?() && currentFolderIds.Contains(x.RootId) || x.ParentId != new Guid?() && currentFolderIds.Contains(x.ParentId.Value))).Select(x => new
      {
        ParentId = x.ParentId ?? x.RootId
      }).ToList().GroupBy(x => x.ParentId);
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (IGrouping<Guid, \u003C\u003Ef__AnonymousType16<Guid>> source in groupings)
      {
        if (dictionary.ContainsKey(source.Key))
          values.Add((object) dictionary[source.Key], (object) source.Count());
      }
      foreach (KeyValuePair<Guid, IFolder> keyValuePair in dictionary)
      {
        if (!values.ContainsKey((object) keyValuePair.Value))
          values.Add((object) keyValuePair.Value, (object) 0);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
