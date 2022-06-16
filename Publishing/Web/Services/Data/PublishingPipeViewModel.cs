// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.Services.Data.PublishingPipeViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Publishing.Web.Services.Data
{
  [DataContract(Name = "PublishingPipeViewModel", Namespace = "Telerik.Sitefinity.Publishing.Web.Services.Data")]
  public class PublishingPipeViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.Services.Data.PublishingPipeViewModel" /> class.
    /// </summary>
    public PublishingPipeViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.Services.Data.PublishingPipeViewModel" /> class.
    /// </summary>
    /// <param name="pipeID">The pipe ID.</param>
    /// <param name="pipeTitle">The pipe title.</param>
    public PublishingPipeViewModel(Guid pipeID, string pipeTitle)
    {
      this.ID = pipeID;
      this.Title = pipeTitle;
    }

    /// <summary>Gets or sets the pipe ID.</summary>
    /// <value>The pipe ID.</value>
    [DataMember]
    public Guid ID { get; set; }

    /// <summary>Gets or sets the pipe title.</summary>
    /// <value>The pipe title.</value>
    [DataMember]
    public string Title { get; set; }
  }
}
