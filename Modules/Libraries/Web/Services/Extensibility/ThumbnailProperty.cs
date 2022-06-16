// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility.ThumbnailProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility
{
  /// <summary>A property for retrieving page locked status</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class ThumbnailProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (IList<ThumbnailModel>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      if (items == null)
        return (IDictionary<object, object>) values;
      foreach (MediaContent key in items)
      {
        IEnumerable<ThumbnailModel> thumbnailModels = key.Thumbnails.Select<Thumbnail, ThumbnailModel>((Func<Thumbnail, ThumbnailModel>) (t => new ThumbnailModel()
        {
          Title = t.Name,
          Width = t.Width,
          Url = this.GetUrl(t)
        }));
        values.Add((object) key, (object) thumbnailModels);
      }
      return (IDictionary<object, object>) values;
    }

    private string GetUrl(Thumbnail item) => item.Parent != null ? DataExtensions.AppSettings.GetThumbnailUrl(item.Parent, item.Name) : string.Empty;
  }
}
