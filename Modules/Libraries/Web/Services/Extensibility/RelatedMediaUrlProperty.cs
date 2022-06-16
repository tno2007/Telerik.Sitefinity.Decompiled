// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility.RelatedMediaUrlProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility
{
  /// <summary>
  /// A calculated property for retrieving URLs of Related <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" /> items.
  /// </summary>
  public class RelatedMediaUrlProperty : MediaUrlProperty
  {
    /// <summary>The field name parameter key.</summary>
    protected const string FieldName = "fieldName";
    private string fieldName;
    private bool multiple;
    private string mediaName;

    /// <inheritdoc />
    public override Type ReturnType => this.multiple ? typeof (IEnumerable<string>) : typeof (string);

    /// <inheritdoc />
    public override void Initialize(NameValueCollection parameters, Type parentType)
    {
      base.Initialize(parameters, parentType);
      string parameter = parameters["fieldName"];
      this.fieldName = !string.IsNullOrEmpty(parameter) ? parameter : throw new ArgumentNullException("The calculated property {0} does not specify a valid fieldName param".Arrange((object) typeof (RelatedMediaUrlProperty).Name));
      if (!(TypeDescriptor.GetProperties(this.ParentType).Find(this.fieldName, false) is RelatedDataPropertyDescriptor propertyDescriptor))
        return;
      this.multiple = propertyDescriptor.MetaField.AllowMultipleRelations;
      if (!typeof (MediaContent).IsAssignableFrom(propertyDescriptor.RelatedType))
        return;
      this.mediaName = propertyDescriptor.RelatedType.Name;
    }

    /// <inheritdoc />
    public override IEnumerable<VocabularyAnnotation> GetAnnotations()
    {
      List<VocabularyAnnotation> annotations = new List<VocabularyAnnotation>(base.GetAnnotations());
      if (this.mediaName != null)
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation("Telerik.Sitefinity.V1", "Media", (object) this.mediaName);
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
      foreach (object key in items)
      {
        IQueryable<IDataItem> relatedItems = key.GetRelatedItems(this.fieldName);
        object obj = (object) null;
        if (!this.multiple)
        {
          if (relatedItems.FirstOrDefault<IDataItem>() is MediaContent media1)
            obj = (object) this.GetUrl(media1);
        }
        else
        {
          List<string> stringList = new List<string>();
          foreach (MediaContent media2 in (IEnumerable<IDataItem>) relatedItems)
            stringList.Add(this.GetUrl(media2));
          obj = (object) stringList;
        }
        values.Add(key, obj);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
