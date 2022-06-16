// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility.MediaUrlProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility
{
  /// <summary>
  /// A calculated property for retrieving URLs of <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" /> items.
  /// </summary>
  public class MediaUrlProperty : UrlProperty
  {
    /// <summary>The thumbnail name parameter key.</summary>
    protected internal const string Thumbnail = "thumbnail";
    /// <summary>The absolute URL parameter key.</summary>
    protected const string Absolute = "absolute";
    /// <summary>The Media annotation value name.</summary>
    protected const string Media = "Media";
    private string thumbnailName;
    private bool absolute = true;

    /// <inheritdoc />
    public override void Initialize(NameValueCollection parameters, Type parentType)
    {
      base.Initialize(parameters, parentType);
      this.thumbnailName = parameters["thumbnail"];
      string parameter = parameters["absolute"];
      if (string.IsNullOrEmpty(parameter))
        return;
      this.absolute = bool.Parse(parameter);
    }

    /// <inheritdoc />
    public override IEnumerable<VocabularyAnnotation> GetAnnotations()
    {
      List<VocabularyAnnotation> annotations = new List<VocabularyAnnotation>(base.GetAnnotations());
      if (typeof (MediaContent).IsAssignableFrom(this.ParentType))
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation("Telerik.Sitefinity.V1", "Media", (object) this.ParentType.Name);
        annotations.Add(vocabularyAnnotation);
      }
      return (IEnumerable<VocabularyAnnotation>) annotations;
    }

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (MediaContent mediaContent in items)
      {
        string url = this.GetUrl(mediaContent);
        values.Add((object) mediaContent, (object) url);
      }
      return (IDictionary<object, object>) values;
    }

    /// <summary>
    /// Gets the url of the <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" />.
    /// </summary>
    /// <param name="media">The media item.</param>
    /// <returns>The url of the item.</returns>
    protected string GetUrl(MediaContent media)
    {
      string url = (string) null;
      if (media != null && !string.IsNullOrEmpty(media.MediaUrl))
      {
        NameValueCollection queryString = HttpUtility.ParseQueryString(new Uri(media.MediaUrl).Query);
        if (queryString.Keys.Contains("Version"))
        {
          string s = queryString["Version"];
          url = media.ResolveVersionMediaUrl(int.Parse(s), absolute: this.absolute);
        }
        else
          url = string.IsNullOrEmpty(this.thumbnailName) ? media.ResolveMediaUrl(this.absolute, (CultureInfo) null) : media.ResolveThumbnailUrl(this.thumbnailName, this.absolute);
      }
      return url;
    }
  }
}
