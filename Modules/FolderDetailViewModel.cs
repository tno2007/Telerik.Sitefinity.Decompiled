// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.FolderDetailViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Modules
{
  [DataContract]
  public class FolderDetailViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.FolderDetailViewModel" /> class.
    /// </summary>
    public FolderDetailViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.FolderDetailViewModel" /> class.
    /// </summary>
    /// <param name="folder">The folder.</param>
    internal FolderDetailViewModel(Folder folder)
    {
      this.Id = folder.Id;
      this.Title = folder.Title;
      this.Description = folder.Description;
      this.ParentId = folder.ParentId.HasValue ? folder.ParentId.Value : folder.RootId;
    }

    /// <summary>Gets or sets the id.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the title.</summary>
    [DataMember]
    public Lstring Title { get; set; }

    /// <summary>Gets or sets the description.</summary>
    [DataMember]
    public Lstring Description { get; set; }

    /// <summary>Gets or sets the parent id.</summary>
    [DataMember]
    public Guid ParentId { get; set; }
  }
}
