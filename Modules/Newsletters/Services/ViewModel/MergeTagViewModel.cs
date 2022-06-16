// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MergeTagViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>ViewModel class for the merge tag type.</summary>
  [DataContract]
  public class MergeTagViewModel
  {
    /// <summary>
    /// Gets or sets the title of the merge tag that will be displayed to the users.
    /// </summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the name of the property used to resolve the merge tag.
    /// </summary>
    [DataMember]
    public string PropertyName { get; set; }

    /// <summary>
    /// Gets or sets the name of the property declaring type used to resolve the merge tag.
    /// </summary>
    [DataMember]
    public string DeclaringTypeName { get; set; }

    /// <summary>Gets the composed merge tag.</summary>
    [DataMember]
    public string ComposedTag { get; set; }
  }
}
