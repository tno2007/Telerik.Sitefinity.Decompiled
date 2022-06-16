// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.SingleContentViewModelBase`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Versioning.Web.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules
{
  [DataContract]
  public abstract class SingleContentViewModelBase<TContent, TContentManager, TContentProvider>
    where TContent : Content
    where TContentManager : ContentManagerBase<TContentProvider>
    where TContentProvider : ContentDataProviderBase
  {
    private IDictionary<string, object> dynamicFields;
    private IList<CommentSingleViewModel<TContentManager, TContentProvider>> comments;

    /// <summary>Transfers to model.</summary>
    /// <param name="model">The model.</param>
    /// <param name="manager">The manager.</param>
    /// <returns></returns>
    public virtual TContent TransferToModel(TContent model, TContentManager manager)
    {
      model.Visible = this.Visible;
      this.Title.TransferToLstring(model.Title);
      this.Description.TransferToLstring(model.Description);
      this.UrlName.TransferToLstring(model.UrlName);
      model.Owner = this.Owner;
      model.ViewsCount = this.ViewsCount;
      model.VotesSum = this.VotesSum;
      model.VotesCount = this.VotesCount;
      model.DateCreated = this.DateCreated;
      model.PublicationDate = this.PublicationDate;
      model.ExpirationDate = this.ExpirationDate;
      model.Version = this.Version;
      model.Status = StatusConverter.ToModel(this.Status);
      model.Comments.Clear();
      foreach (CommentSingleViewModel<TContentManager, TContentProvider> comment1 in (IEnumerable<CommentSingleViewModel<TContentManager, TContentProvider>>) this.Comments)
      {
        Comment comment2 = manager.CreateComment((ICommentable) model);
        Comment model1 = comment2;
        TContentManager manager1 = manager;
        comment1.TransferToModel(model1, manager1);
        model.Comments.Add(comment2);
      }
      MetaType metaType = MetadataManager.GetManager().GetMetaType(model.GetType());
      if (metaType != null)
      {
        foreach (MetaField field in (IEnumerable<MetaField>) metaType.Fields)
        {
          object obj = (object) null;
          if (this.DynamicFields.TryGetValue(field.FieldName, out obj))
            model.SetValue(field.FieldName, obj);
        }
      }
      return model;
    }

    /// <summary>Loads from model.</summary>
    /// <param name="model">The model.</param>
    /// <param name="manager">The manager.</param>
    public virtual void LoadFromModel(TContent model, TContentManager manager)
    {
      this.Visible = model.Visible;
      this.Title = new LstringSingleViewModel(model.Title);
      this.Description = new LstringSingleViewModel(model.Description);
      this.UrlName = new LstringSingleViewModel(model.UrlName);
      this.Owner = model.Owner;
      this.ViewsCount = model.ViewsCount;
      this.VotesSum = model.VotesSum;
      this.VotesCount = model.VotesCount;
      this.DateCreated = model.DateCreated;
      this.PublicationDate = model.PublicationDate;
      this.ExpirationDate = model.ExpirationDate;
      this.Version = model.Version;
      this.Status = StatusConverter.FromModel(model.Status);
      this.Comments.Clear();
      foreach (Comment comment in (IEnumerable<Comment>) model.Comments)
      {
        CommentSingleViewModel<TContentManager, TContentProvider> commentSingleViewModel = new CommentSingleViewModel<TContentManager, TContentProvider>();
        commentSingleViewModel.LoadFromModel(comment, manager);
        this.Comments.Add(commentSingleViewModel);
      }
      MetaType metaType = MetadataManager.GetManager().GetMetaType(model.GetType());
      if (metaType == null)
        return;
      foreach (MetaField field in (IEnumerable<MetaField>) metaType.Fields)
        this.DynamicFields.Add(field.FieldName, model.GetValue(field.FieldName));
    }

    /// <summary>Gets or sets the id.</summary>
    /// <value>The id.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Modules.SingleContentViewModelBase`3" /> is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool Visible { get; set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [DataMember]
    public LstringSingleViewModel Title { get; set; }

    /// <summary>Gets or sets the description.</summary>
    /// <value>The description.</value>
    [DataMember]
    public LstringSingleViewModel Description { get; set; }

    /// <summary>Gets or sets the URL name.</summary>
    /// <value>The URL name.</value>
    [DataMember]
    public LstringSingleViewModel UrlName { get; set; }

    /// <summary>Gets or sets the owner.</summary>
    /// <value>The owner.</value>
    [DataMember]
    public Guid Owner { get; set; }

    /// <summary>Gets or sets the views count.</summary>
    /// <value>The views count.</value>
    [DataMember]
    public int ViewsCount { get; set; }

    /// <summary>Gets or sets the votes sum.</summary>
    /// <value>The votes sum.</value>
    [DataMember]
    public Decimal VotesSum { get; set; }

    /// <summary>Gets or sets the votes count.</summary>
    /// <value>The votes count.</value>
    [DataMember]
    public uint VotesCount { get; set; }

    [DataMember]
    public DateTime DateCreated { get; set; }

    /// <summary>Gets or sets the publication date.</summary>
    /// <value>The publication date.</value>
    [DataMember]
    public DateTime PublicationDate { get; set; }

    /// <summary>Gets or sets the expiration date.</summary>
    /// <value>The expiration date.</value>
    [DataMember]
    public DateTime? ExpirationDate { get; set; }

    /// <summary>Gets or sets the version.</summary>
    /// <value>The version.</value>
    [DataMember]
    public int Version { get; set; }

    /// <summary>Gets or sets the status.</summary>
    /// <value>The status.</value>
    [DataMember]
    public string Status { get; set; }

    /// <summary>Gets or sets the dynamic fields.</summary>
    /// <value>The dynamic fields.</value>
    [DataMember]
    public IDictionary<string, object> DynamicFields
    {
      get
      {
        if (this.dynamicFields == null)
          this.dynamicFields = (IDictionary<string, object>) new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        return this.dynamicFields;
      }
      set => this.dynamicFields = value;
    }

    /// <summary>Gets or sets the comments.</summary>
    /// <value>The comments.</value>
    [DataMember]
    public IList<CommentSingleViewModel<TContentManager, TContentProvider>> Comments
    {
      get
      {
        if (this.comments == null)
          this.comments = (IList<CommentSingleViewModel<TContentManager, TContentProvider>>) new List<CommentSingleViewModel<TContentManager, TContentProvider>>();
        return this.comments;
      }
      set => this.comments = value;
    }

    /// <summary>Gets or sets the lifecycle status.</summary>
    /// <value>The lifecycle status.</value>
    [DataMember]
    public WcfContentLifecycleStatus LifecycleStatus { get; set; }

    /// <summary>Gets or sets the version info.</summary>
    /// <value>The version info.</value>
    [DataMember]
    public WcfChange VersionInfo { get; set; }
  }
}
