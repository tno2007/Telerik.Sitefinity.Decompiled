// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Folders.FolderChildrenDateCreated
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Folders
{
  internal class FolderChildrenDateCreated
  {
    internal FolderChildrenDateCreated(Guid id, DateTime date)
    {
      this.Id = id;
      this.Date = date;
    }

    internal FolderChildrenDateCreated(Guid id, DateTime date, Guid ownerId)
      : this(id, date)
    {
      this.OwnerId = ownerId;
    }

    /// <summary>Gets or sets folder id</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets children date created</summary>
    public DateTime Date { get; set; }

    /// <summary>Gets or sets the owner of the child item.</summary>
    public Guid OwnerId { get; set; }
  }
}
