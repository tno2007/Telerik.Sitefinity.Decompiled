// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Setters.TrackedListSetter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Services.InlineEditing;

namespace Telerik.Sitefinity.InlineEditing.Setters
{
  internal class TrackedListSetter : PropertySetterBase
  {
    public override Type GetPropertyType() => typeof (TrackedList<Guid>);

    public override void Set(object item, FieldValueModel field, PropertyDescriptor property)
    {
      if (field.ComplexValue == null)
        return;
      IOrganizable organizable = item as IOrganizable;
      field.Value = (object) ((IEnumerable<FieldValueModel>) field.ComplexValue).Select<FieldValueModel, Guid>((Func<FieldValueModel, Guid>) (x => Guid.Parse(x.Value.ToString()))).ToArray<Guid>();
      organizable.Organizer.Clear(field.Name);
      organizable.Organizer.AddTaxa(field.Name, field.Value as Guid[]);
    }
  }
}
