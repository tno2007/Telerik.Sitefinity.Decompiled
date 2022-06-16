// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.GenericCollectionPropertyDescriptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  public class GenericCollectionPropertyDescriptor : PropertyDescriptor
  {
    private object collectionItem;
    private Type componentType;

    public GenericCollectionPropertyDescriptor(
      object collectionItem,
      string propName,
      Type componentType,
      Attribute[] attributes)
      : base(propName, attributes)
    {
      this.collectionItem = collectionItem;
      this.componentType = componentType;
    }

    public override bool CanResetValue(object component) => true;

    public override Type ComponentType => this.componentType;

    public override object GetValue(object component) => this.collectionItem;

    public override bool IsReadOnly => true;

    public override Type PropertyType => this.collectionItem.GetType();

    public override void ResetValue(object component) => throw new NotImplementedException();

    public override void SetValue(object component, object value) => throw new NotImplementedException();

    public override bool ShouldSerializeValue(object component) => throw new NotImplementedException();
  }
}
