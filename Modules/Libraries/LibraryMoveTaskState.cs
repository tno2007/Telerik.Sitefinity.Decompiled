// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibraryMoveTaskState
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// Used to persist the state of a Library Move Task to allow task resume.
  /// </summary>
  [Serializable]
  internal class LibraryMoveTaskState
  {
    /// <summary>
    /// Used to persist the state of a Library Move Task to allow task resume.
    /// </summary>
    public LibraryMoveTaskState()
    {
    }

    /// <summary>
    /// Used to persist the state of a Library Move Task to allow task resume.
    /// </summary>
    /// <param name="libraryMoveTask">The library move task.</param>
    public LibraryMoveTaskState(LibraryMoveTask libraryMoveTask)
    {
      this.ItemId = libraryMoveTask.ItemId;
      this.ParentId = libraryMoveTask.ParentId;
      this.LibraryProvider = libraryMoveTask.LibraryProvider;
      this.NewItemId = libraryMoveTask.NewItemId;
      this.ItemIDs = libraryMoveTask.ItemIDs;
      this.LibraryTitle = libraryMoveTask.LibraryTitle;
      this.LibraryType = libraryMoveTask.LibraryType;
    }

    /// <summary>Gets or sets the id of the item that is to be moved.</summary>
    public Guid ItemId { get; set; }

    /// <summary>Gets or sets the id of the item that is to be moved.</summary>
    public Guid[] ItemIDs { get; set; }

    /// <summary>Gets or sets the target parent id.</summary>
    public Guid ParentId { get; set; }

    /// <summary>Gets or sets the library provider.</summary>
    public string LibraryProvider { get; set; }

    /// <summary>Gets or sets the id of the item that is created.</summary>
    public Guid NewItemId { get; set; }

    /// <summary>
    /// Gets or sets the library title if we are moving a single library only.
    /// </summary>
    public string LibraryTitle { get; set; }

    /// <summary>Gets or sets the type of the library.</summary>
    /// <value>The type of the library.</value>
    public string LibraryType { get; set; }
  }
}
