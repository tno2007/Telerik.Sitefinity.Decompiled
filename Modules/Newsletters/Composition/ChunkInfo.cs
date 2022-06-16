// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Composition.ChunkInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Newsletters.Composition
{
  /// <summary>
  /// This class represents the information about html chunk.
  /// </summary>
  public class ChunkInfo
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Composition.ChunkInfo" />.
    /// </summary>
    /// <param name="tagName">The name of the tag being represented.</param>
    public ChunkInfo(string tagName)
    {
      this.TagName = tagName;
      this.IsArtificial = false;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Composition.ChunkInfo" />.
    /// </summary>
    /// <param name="tagName">The name of the tag being represented.</param>
    /// <param name="isArtificial">Indicates weather chunk existed in the original html. True if no; otherwise false.</param>
    public ChunkInfo(string tagName, bool isArtificial)
    {
      this.TagName = tagName;
      this.IsArtificial = isArtificial;
    }

    /// <summary>Gets the name of the tag.</summary>
    public string TagName { get; private set; }

    /// <summary>Determines if chunk is artificial.</summary>
    public bool IsArtificial { get; private set; }
  }
}
