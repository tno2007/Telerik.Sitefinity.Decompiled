// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GenericCLCManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;

namespace Telerik.Sitefinity
{
  internal class GenericCLCManager
  {
    private object manager;
    private ILifecycleDecorator lifecycle;

    public GenericCLCManager(object mgr)
    {
      this.manager = mgr;
      if (!(this.manager is ILifecycleManager manager))
        return;
      this.lifecycle = manager.Lifecycle;
    }

    public Content CheckIn(Content item)
    {
      if (this.lifecycle != null)
        return (Content) this.lifecycle.CheckIn((ILifecycleDataItem) item);
      return (Content) this.manager.GetType().GetMethod(nameof (CheckIn), new Type[1]
      {
        item.GetType()
      }).Invoke(this.manager, new object[1]{ (object) item });
    }

    public Content CheckOut(Content item)
    {
      if (this.lifecycle != null)
        return (Content) this.lifecycle.CheckOut((ILifecycleDataItem) item);
      return (Content) this.manager.GetType().GetMethod(nameof (CheckOut), new Type[1]
      {
        item.GetType()
      }).Invoke(this.manager, new object[1]{ (object) item });
    }

    public Content Publish(Content item)
    {
      if (this.lifecycle != null)
        return (Content) this.lifecycle.Publish((ILifecycleDataItem) item);
      return (Content) this.manager.GetType().GetMethod(nameof (Publish), new Type[1]
      {
        item.GetType()
      }).Invoke(this.manager, new object[1]{ (object) item });
    }
  }
}
