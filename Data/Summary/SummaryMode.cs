// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Summary.SummaryMode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.Summary
{
  /// <summary>
  /// Defines identifiers that indicate the summary modes used by ContenView.
  /// </summary>
  public enum SummaryMode
  {
    /// <summary>Indicates that the whole text should be displayed.</summary>
    None,
    /// <summary>
    /// Indicates that the summary should be created by cropping the specified number of words.
    /// </summary>
    Words,
    /// <summary>
    /// Indicates that the summary should be created by cropping the specified number of sentences.
    /// </summary>
    Sentences,
    /// <summary>
    /// Indicates that the summary should be created by cropping the specified number of paragraphs.
    /// </summary>
    Paragraphs,
    /// <summary>
    /// Indicates that the content of a specified meta field will be used for summery.
    /// </summary>
    MetaField,
  }
}
