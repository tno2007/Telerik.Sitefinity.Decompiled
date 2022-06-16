// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.PropertyBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal class PropertyBuilder : ObjectBuilder
  {
    private PropertyDescriptor propertyDescriptor;
    private PropertyBuilder.PropertyType propertyType;
    private string encodedHtml;
    private string innerHtml;

    internal PropertyBuilder(PropertyDescriptor descriptor, ObjectBuilder parent)
      : base(parent)
    {
      PersistenceModeAttribute persistenceModeAttribute = descriptor != null ? (PersistenceModeAttribute) descriptor.Attributes[typeof (PersistenceModeAttribute)] : throw new ArgumentNullException(nameof (descriptor));
      if (persistenceModeAttribute == null || persistenceModeAttribute.Mode == PersistenceMode.Attribute)
        throw new FormatException("Invalid Property.");
      this.propertyDescriptor = descriptor;
      if (typeof (ITemplate).IsAssignableFrom(descriptor.PropertyType))
        this.propertyType = PropertyBuilder.PropertyType.Template;
      else if (persistenceModeAttribute.Mode == PersistenceMode.EncodedInnerDefaultProperty)
        this.propertyType = PropertyBuilder.PropertyType.EncodedHtml;
      else if (persistenceModeAttribute.Mode == PersistenceMode.InnerDefaultProperty)
        this.propertyType = PropertyBuilder.PropertyType.InnerDefault;
      else if (typeof (IList).IsAssignableFrom(descriptor.PropertyType))
        this.propertyType = PropertyBuilder.PropertyType.List;
      else
        this.propertyType = PropertyBuilder.PropertyType.Other;
    }

    internal override Type ObjectType
    {
      get => this.Descriptor != null ? this.Descriptor.PropertyType : base.ObjectType;
      set => base.ObjectType = value;
    }

    internal override string TagName
    {
      get => this.Descriptor != null ? this.Descriptor.Name : base.TagName;
      set => base.TagName = value;
    }

    internal void ParseProperty(string html)
    {
      switch (this.propertyType)
      {
        case PropertyBuilder.PropertyType.List:
          CollectionBuilder collectionBuilder = new CollectionBuilder(html, this);
          break;
        case PropertyBuilder.PropertyType.Template:
          TemplateBuilder templateBuilder = new TemplateBuilder(html, this);
          break;
        case PropertyBuilder.PropertyType.EncodedHtml:
          this.encodedHtml = HttpUtility.HtmlEncode(html);
          break;
        case PropertyBuilder.PropertyType.InnerDefault:
          this.innerHtml = html;
          break;
        default:
          PropertiesBuilder propertiesBuilder = new PropertiesBuilder(html, (ObjectBuilder) this);
          break;
      }
    }

    internal PropertyDescriptor Descriptor => this.propertyDescriptor;

    internal override object CreateObject(Control bindingContainer) => throw new NotSupportedException();

    internal void SetProperty(object component, Control bindingContainer)
    {
      ObjectBuilder objectBuilder = (ObjectBuilder) null;
      if (this.ChildBuilders.Count > 0)
        objectBuilder = this.ChildBuilders[0];
      if (this.propertyType == PropertyBuilder.PropertyType.EncodedHtml)
      {
        string empty = string.Empty;
        if (this.Descriptor.GetValue(component) != null)
          empty = this.Descriptor.GetValue(component).ToString();
        this.Descriptor.SetValue(component, (object) (empty + this.encodedHtml));
      }
      else if (this.propertyType == PropertyBuilder.PropertyType.InnerDefault)
      {
        string empty = string.Empty;
        if (this.Descriptor.GetValue(component) != null)
          empty = this.Descriptor.GetValue(component).ToString();
        string str = empty + this.innerHtml;
        this.Descriptor.SetValue(component, (object) str);
      }
      else if (this.propertyType == PropertyBuilder.PropertyType.Template && objectBuilder != null)
      {
        if (!(objectBuilder.CreateObject(bindingContainer) is ITemplate template))
          return;
        this.Descriptor.SetValue(component, (object) template);
      }
      else
      {
        object component1 = this.Descriptor.GetValue(component);
        TypeDescriptor.GetProperties(component1);
        foreach (Attribute attribute in this.Attributes)
          attribute.SetAttribute(component1, bindingContainer);
        if (objectBuilder == null)
          return;
        if (this.propertyType == PropertyBuilder.PropertyType.List)
        {
          ((CollectionBuilder) objectBuilder).SetCollection((IList) component1, bindingContainer);
        }
        else
        {
          if (!(objectBuilder is PropertiesBuilder propertiesBuilder))
            return;
          propertiesBuilder.SetProperties(component1, bindingContainer);
        }
      }
    }

    private enum PropertyType
    {
      List,
      Template,
      EncodedHtml,
      InnerDefault,
      Other,
    }
  }
}
