// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ExtendedDataItem`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Wraps a <typeparamref name="TDataItem" /> object and implements <see cref="T:System.ComponentModel.ICustomTypeDescriptor" />
  /// that first looks up the properties of the wrapped object and then those of <c>this</c>.
  /// </summary>
  /// <example>
  /// Having the following definitions
  /// <code>
  /// public class Post
  /// {
  ///     public string Title { get; set; }
  ///     public string Url { get; set; }
  /// 
  ///     public string Age { get; set; }
  /// }
  /// 
  /// internal class PostDataItem : ExtendedDataItem&lt;Post&gt;
  /// {
  ///     public PageDataItem(Page forum)
  ///         : base(forum)
  ///     {
  ///     }
  /// 
  ///     public string Age { get; set; }
  /// }
  /// </code>
  /// in a data binding expression you can use <see cref="!:DataBinder.Eval" /> for both <c>Post</c>'s and
  /// <c>PostDataItem</c>'s properties, the former having precedence.
  /// </example>
  /// <typeparam name="TDataItem">The type of the wrapped object.</typeparam>
  internal class ExtendedDataItem<TDataItem> : CustomTypeDescriptor
  {
    private ISet<string> dataItemPropNames;
    private PropertyDescriptorCollection combinedProperties;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ExtendedDataItem" /> class.
    /// </summary>
    public ExtendedDataItem()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ExtendedDataItem" /> class.
    /// </summary>
    /// <param name="dataItem">The wrapped data item.</param>
    public ExtendedDataItem(TDataItem dataItem) => this.DataItem = dataItem;

    /// <summary>The wrapped data item instance.</summary>
    public TDataItem DataItem { get; set; }

    /// <inheritdoc />
    public override PropertyDescriptorCollection GetProperties() => this.GetProperties((Attribute[]) null);

    /// <inheritdoc />
    public override PropertyDescriptorCollection GetProperties(
      Attribute[] attributes)
    {
      if (this.combinedProperties == null)
      {
        PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties((object) this.DataItem);
        int count1 = properties1.Count;
        PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties((object) this, true);
        int count2 = properties2.Count;
        PropertyDescriptor[] properties3 = new PropertyDescriptor[count2 + count1];
        HashSet<string> stringSet = new HashSet<string>();
        for (int index = 0; index < count1; ++index)
        {
          PropertyDescriptor propertyDescriptor = properties1[index];
          properties3[index] = propertyDescriptor;
          stringSet.Add(propertyDescriptor.Name);
        }
        for (int index = 0; index < count2; ++index)
          properties3[count1 + index] = properties2[index];
        this.combinedProperties = new PropertyDescriptorCollection(properties3);
        this.dataItemPropNames = (ISet<string>) stringSet;
      }
      return this.combinedProperties;
    }

    /// <inheritdoc />
    public override object GetPropertyOwner(PropertyDescriptor prop) => this.dataItemPropNames.Contains(prop.Name) ? (object) this.DataItem : (object) this;
  }
}
