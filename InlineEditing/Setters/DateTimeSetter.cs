// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Setters.DateTimeSetter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services.InlineEditing;

namespace Telerik.Sitefinity.InlineEditing.Setters
{
  internal class DateTimeSetter : PropertySetterBase
  {
    public override Type GetPropertyType() => typeof (DateTime);

    public override void Set(object item, FieldValueModel field, PropertyDescriptor property)
    {
      DateTime dateTime = DateTime.SpecifyKind(DateTime.Parse(field.Value as string), DateTimeKind.Unspecified);
      TimeZoneInfo userTimeZone = UserManager.GetManager().GetUserTimeZone();
      field.Value = (object) TimeZoneInfo.ConvertTimeToUtc(dateTime, userTimeZone);
      base.Set(item, field, property);
    }
  }
}
