// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.CopyDirection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// This enumeration is used to specify behavior when copying controls between control collections.
  /// </summary>
  public enum CopyDirection
  {
    /// <summary>
    /// This does not handle any links between objects, controls from the target are deleted and
    /// controls from the source are copied to the target as new controls.
    /// </summary>
    Unspecified,
    /// <summary>
    /// This behaves as Unspecified except it sets the OriginalControlId of the newly created controls
    /// to the Id of the source controls.
    /// </summary>
    OriginalToCopy,
    /// <summary>
    /// This tries to find the source control in the target collection by using the OriginalControlId of
    /// the source controls. If a target control is found, it is updated with data from the source,
    /// if not - the source control is copied as new.
    /// </summary>
    CopyToOriginal,
  }
}
