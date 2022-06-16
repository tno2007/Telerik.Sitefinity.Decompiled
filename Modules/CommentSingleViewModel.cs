// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.CommentSingleViewModel`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Modules
{
  [DataContract]
  public class CommentSingleViewModel<TContentManager, TContentProvider> : 
    SingleContentViewModelBase<Comment, TContentManager, TContentProvider>
    where TContentManager : ContentManagerBase<TContentProvider>
    where TContentProvider : ContentDataProviderBase
  {
    public override void LoadFromModel(Comment model, TContentManager manager)
    {
      base.TransferToModel(model, manager);
      this.CommentedItemID = model.CommentedItemID;
      this.CommentedItemType = model.CommentedItemType;
      this.Content = new LstringSingleViewModel(model.Content);
      this.Email = model.Email;
      this.IpAddress = model.IpAddress;
      this.AuthorName.TransferToLstring(model.AuthorName);
      this.CommentStatus = model.CommentStatus.ToString();
      this.Website = model.Website;
      this.ParentGroupIds.Clear();
      foreach (Guid parentGroupId in (IEnumerable<Guid>) model.ParentGroupIds)
        this.ParentGroupIds.Add(parentGroupId);
    }

    public override Comment TransferToModel(Comment model, TContentManager manager)
    {
      base.TransferToModel(model, manager);
      model.CommentedItemID = this.CommentedItemID;
      model.CommentedItemType = this.CommentedItemType;
      this.Content.TransferToLstring(model.Content);
      model.Email = this.Email;
      model.IpAddress = this.IpAddress;
      this.AuthorName.TransferToLstring(model.AuthorName);
      model.CommentStatus = (Telerik.Sitefinity.GenericContent.Model.CommentStatus) Enum.Parse(typeof (Telerik.Sitefinity.GenericContent.Model.CommentStatus), this.CommentStatus);
      model.Website = this.Website;
      model.ParentGroupIds.Clear();
      foreach (Guid parentGroupId in (IEnumerable<Guid>) this.ParentGroupIds)
        model.ParentGroupIds.Add(parentGroupId);
      return model;
    }

    [DataMember]
    public Guid CommentedItemID { get; set; }

    [DataMember]
    public string CommentedItemType { get; set; }

    [DataMember]
    public LstringSingleViewModel Content { get; set; }

    [DataMember]
    public string Email { get; set; }

    [DataMember]
    public string IpAddress { get; set; }

    [DataMember]
    public LstringSingleViewModel AuthorName { get; set; }

    [DataMember]
    public string CommentStatus { get; set; }

    [DataMember]
    public string Website { get; set; }

    [DataMember]
    public IList<Guid> ParentGroupIds { get; set; }
  }
}
