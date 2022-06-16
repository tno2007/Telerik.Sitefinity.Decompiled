// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.ClickStatViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>Represents the view model for link click statistics.</summary>
  [DataContract]
  public class ClickStatViewModel
  {
    /// <summary>Gets or sets the URL.</summary>
    [DataMember]
    public string Url { get; set; }

    /// <summary>Gets or sets the click count.</summary>
    [DataMember]
    public int Clicks { get; set; }
  }
}
