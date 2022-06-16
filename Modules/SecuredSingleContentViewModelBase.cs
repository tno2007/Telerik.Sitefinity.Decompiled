// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.SecuredSingleContentViewModelBase`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules
{
  [DataContract]
  public abstract class SecuredSingleContentViewModelBase<TContent, TContentManager, TContentProvider> : 
    SingleContentViewModelBase<TContent, TContentManager, TContentProvider>
    where TContent : Content, ISecuredObject
    where TContentManager : ContentManagerBase<TContentProvider>
    where TContentProvider : ContentDataProviderBase
  {
    public override void LoadFromModel(TContent model, TContentManager manager)
    {
      base.LoadFromModel(model, manager);
      this.InheritsPermissions = model.InheritsPermissions;
    }

    public override TContent TransferToModel(TContent model, TContentManager manager)
    {
      base.TransferToModel(model, manager);
      model.InheritsPermissions = this.InheritsPermissions;
      return model;
    }

    [DataMember]
    public bool InheritsPermissions { get; set; }
  }
}
