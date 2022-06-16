// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.WrapperObjectTypeDescriptorProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// Provides supplemental metadata to the <see cref="T:System.ComponentModel.TypeDescriptor" />.
  /// </summary>
  internal class WrapperObjectTypeDescriptorProvider : TypeDescriptionProvider
  {
    private TypeDescriptionProvider parentProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.WrapperObjectTypeDescriptorProvider" /> class.
    /// </summary>
    public WrapperObjectTypeDescriptorProvider()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.WrapperObjectTypeDescriptorProvider" /> class.
    /// </summary>
    /// <param name="parent">The parent type description provider.</param>
    public WrapperObjectTypeDescriptorProvider(TypeDescriptionProvider parent)
      : base(parent)
    {
      this.parentProvider = parent;
    }

    /// <summary>
    /// Gets a custom type descriptor for the given type and object.
    /// </summary>
    /// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
    /// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
    /// <returns>
    /// An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.
    /// </returns>
    public override ICustomTypeDescriptor GetTypeDescriptor(
      Type objectType,
      object instance)
    {
      return (ICustomTypeDescriptor) new WrapperObjectTypeDescriptor(instance);
    }
  }
}
