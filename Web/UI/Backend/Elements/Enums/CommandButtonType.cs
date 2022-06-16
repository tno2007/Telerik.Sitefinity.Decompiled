// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Enums.CommandButtonType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Enums
{
  /// <summary>Enumeration of possible command button types.</summary>
  public enum CommandButtonType
  {
    /// <summary>
    /// Indicates that the CommandButtonType has not been set. Use this value if
    /// the property value should be determined on a different place in chain
    /// of responsibility.
    /// </summary>
    NotSet,
    /// <summary>Standard Sitefinity backend button</summary>
    Standard,
    /// <summary>Sitefinity backend cancel button</summary>
    Cancel,
    /// <summary>Sitefinity backend create button</summary>
    Create,
    /// <summary>Sitefinity backend save button</summary>
    Save,
    /// <summary>Sitefinity backend Save and continue button</summary>
    SaveAndContinue,
    /// <summary>Sitefinity backend Simple Link button without CSS</summary>
    SimpleLinkButton,
  }
}
