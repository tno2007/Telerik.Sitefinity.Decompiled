// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Enums.ContentLinkChangeState
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.UI.Fields.Enums
{
  /// <summary>
  /// Represents the state of the updated related data content link
  /// </summary>
  [DataContract]
  public enum ContentLinkChangeState
  {
    /// <summary>Related item is added to related field</summary>
    Added,
    /// <summary>Related item is removed from related field</summary>
    Removed,
    /// <summary>Related item in related field is updated</summary>
    Updated,
  }
}
