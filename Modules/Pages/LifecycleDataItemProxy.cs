// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.LifecycleDataItemProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages
{
  internal class LifecycleDataItemProxy : ILifecycleDataItem, IOwnership, IDataItem
  {
    private PageDataProxy proxy;

    public LifecycleDataItemProxy(PageDataProxy proxy) => this.proxy = proxy;

    public ContentLifecycleStatus Status
    {
      get => this.proxy.Status;
      set => throw new NotImplementedException();
    }

    public IList<Telerik.Sitefinity.Lifecycle.LanguageData> LanguageData => throw new NotImplementedException();

    public int Version
    {
      get => this.proxy.Version;
      set => throw new NotImplementedException();
    }

    public bool Visible
    {
      get => this.proxy.Visible;
      set => throw new NotImplementedException();
    }

    public IList<string> PublishedTranslations => this.proxy.PublishedTranslations;

    public Guid Owner
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public Guid Id => this.proxy.Id;

    public object Transaction
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public object Provider
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public DateTime LastModified
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public string ApplicationName
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }
  }
}
