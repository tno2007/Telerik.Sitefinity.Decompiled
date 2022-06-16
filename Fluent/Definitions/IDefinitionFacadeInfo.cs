// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.IDefinitionFacadeInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// General information about facades. Usually passed via individual facade constructors
  /// </summary>
  public interface IDefinitionFacadeInfo
  {
    /// <summary>
    /// Type of content passed to the base facade (e.g. content view)
    /// </summary>
    Type ContentType { get; }

    /// <summary>Name of the definition</summary>
    string DefinitionName { get; }

    /// <summary>Name of the module</summary>
    string ModuleName { get; }

    /// <summary>Name of the section</summary>
    string SectionName { get; }

    /// <summary>Name of the view</summary>
    string ViewName { get; }

    /// <summary>
    /// Display mode (e.g. read, write or null for default; default varies by config elements)
    /// </summary>
    Telerik.Sitefinity.Web.UI.Fields.Enums.FieldDisplayMode? FieldDisplayMode { get; }

    /// <summary>Resource class ID</summary>
    string ResourceClassId { get; }
  }
}
