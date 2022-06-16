// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Content.Data.RatingResult
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Services.Content.Data
{
  /// <summary>
  /// Defines the data returned by the content service when a rating is being set
  /// </summary>
  [DataContract]
  public class RatingResult : IExtensibleDataObject
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Content.Data.RatingResult" /> class.
    /// </summary>
    /// <param name="votesCount">Total number of votes.</param>
    /// <param name="average">The average rating.</param>
    /// <param name="subtitleMessage">Message to be shown at the rating control's subtitle</param>
    public RatingResult(uint votesCount, Decimal average, string subtitleMessage)
    {
      this.VotesCount = votesCount;
      this.Average = average;
      this.SubtitleMessage = subtitleMessage;
    }

    /// <summary>Total number of votes.</summary>
    [DataMember]
    public uint VotesCount { get; private set; }

    /// <summary>The average rating.</summary>
    [DataMember]
    public Decimal Average { get; private set; }

    /// <summary>Message to be shown at the rating control's subtitle</summary>
    [DataMember]
    public string SubtitleMessage { get; private set; }

    /// <summary>Gets or sets the structure that contains extra data.</summary>
    /// <value></value>
    /// <returns>An <see cref="T:System.Runtime.Serialization.ExtensionDataObject" /> that contains data that is not recognized as belonging to the data contract.</returns>
    public ExtensionDataObject ExtensionData { get; set; }
  }
}
