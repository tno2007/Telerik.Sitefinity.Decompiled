// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.LockContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Renderer.Editor
{
  /// <summary>
  /// Class containing information for locking the current object.
  /// </summary>
  public class LockContext : DefaultEditorContext, ILockContext, IEditorContext
  {
    /// <summary>Gets or sets the version.</summary>
    [DataMember]
    public int Version { get; set; }
  }
}
