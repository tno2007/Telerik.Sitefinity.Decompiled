// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.SurrogateDescriptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Descriptors;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>Provides type description for surrogates</summary>
  public class SurrogateDescriptor : CustomTypeDescriptor
  {
    private AttributeCollection attributes;
    private PropertyDescriptorCollection properties;

    /// <summary>Get all attributes applied to a type</summary>
    /// <param name="type">Type to check</param>
    /// <returns>Array of attributes</returns>
    private static Attribute[] GetAllAttributes(Type type) => type.GetCustomAttributes(true).Cast<Attribute>().ToArray<Attribute>();

    /// <summary>
    /// Creates a new instance of the custom surrogate type descriptor
    /// </summary>
    /// <param name="instance">Instance of the type to describe</param>
    /// <param name="type">CLR type to describe</param>
    public SurrogateDescriptor(Type type)
    {
      this.attributes = new AttributeCollection(SurrogateDescriptor.GetAllAttributes(type));
      this.properties = new PropertyDescriptorCollection((PropertyDescriptor[]) ((IEnumerable<PropertyInfo>) type.GetProperties()).Select<PropertyInfo, DataPropertyDescriptor>((Func<PropertyInfo, DataPropertyDescriptor>) (p => new DataPropertyDescriptor(p, SurrogateDescriptor.GetAllAttributes(p.PropertyType)))).ToArray<DataPropertyDescriptor>());
    }

    /// <summary>
    /// Returns a collection of property descriptors for the object represented by this type descriptor.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.
    /// </returns>
    public override PropertyDescriptorCollection GetProperties() => this.properties;

    /// <summary>
    /// Returns a collection of custom attributes for the type represented by this type descriptor.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the type. The default is <see cref="F:System.ComponentModel.AttributeCollection.Empty" />.
    /// </returns>
    public override AttributeCollection GetAttributes() => this.attributes;
  }
}
