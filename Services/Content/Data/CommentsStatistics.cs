// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Content.Data.CommentsStatistics
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Services.Content.Data
{
  /// <summary>A number of statistics for comments</summary>
  [DataContract(Name = "CommentsStatistics")]
  public class CommentsStatistics : IExtensibleDataObject
  {
    /// <summary>Retrieves the number of comments today</summary>
    [DataMember]
    public int NumberOfCommentsToday { get; set; }

    /// <summary>Retrieves the number of comments marked as spam</summary>
    [DataMember]
    public int NumberOfSpamComments { get; set; }

    /// <summary>Retrieves the number of comments marked as published</summary>
    [DataMember]
    public int NumberOfPublishedComments { get; set; }

    /// <summary>Retrieves the number of comments marked as hidden</summary>
    [DataMember]
    public int NumberOfHiddenComments { get; set; }

    /// <summary>Retrieves the number of all comments for this module</summary>
    [DataMember]
    public int NumberOfAllComments { get; set; }

    /// <summary>
    /// The filter to set on the client side to retrieve today's comments
    /// </summary>
    [DataMember]
    public string TodayFilter { get; set; }

    /// <summary>Gets or sets the structure that contains extra data.</summary>
    /// <value></value>
    /// <returns>An <see cref="T:System.Runtime.Serialization.ExtensionDataObject" /> that contains data that is not recognized as belonging to the data contract.</returns>
    public ExtensionDataObject ExtensionData { get; set; }
  }
}
