// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.GroupCreateRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>GroupCreateRequest</c> Represents the <see cref="T:Telerik.Sitefinity.Services.Comments.IGroup" /> object that should be created.
  /// </summary>
  public class GroupCreateRequest
  {
    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IGroup" /> object key.
    /// </summary>
    /// <value>The group key.</value>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the name of the <see cref="T:Telerik.Sitefinity.Services.Comments.IGroup" /> object.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the <see cref="T:Telerik.Sitefinity.Services.Comments.IGroup" /> object.
    /// </summary>
    /// <value>The description.</value>
    public string Description { get; set; }
  }
}
