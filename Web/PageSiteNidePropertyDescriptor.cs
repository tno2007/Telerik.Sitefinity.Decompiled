// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageSiteNidePropertyDescriptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;

namespace Telerik.Sitefinity.Web
{
  internal class PageSiteNidePropertyDescriptor : PropertyDescriptor
  {
    internal PageSiteNidePropertyDescriptor(string name)
      : base(name, (Attribute[]) null)
    {
    }

    internal PageSiteNidePropertyDescriptor(string name, Attribute[] attr)
      : base(name, attr)
    {
    }

    public override Type ComponentType => typeof (PageSiteNode);

    public override bool IsReadOnly => true;

    public override Type PropertyType => typeof (object);

    public override bool CanResetValue(object component) => false;

    public override object GetValue(object component) => component is PageSiteNode pageSiteNode ? pageSiteNode.GetCustomFieldValue(this.Name) : (object) null;

    public override void ResetValue(object component) => throw new NotImplementedException();

    public override void SetValue(object component, object value) => throw new NotImplementedException();

    public override bool ShouldSerializeValue(object component) => false;
  }
}
