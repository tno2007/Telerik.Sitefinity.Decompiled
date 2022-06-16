// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.DefinitionFacadeInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// General information about facades. Usually passed via individual facade constructors
  /// </summary>
  public class DefinitionFacadeInfo : IDefinitionFacadeInfo
  {
    private bool resClassIdManuallySet;
    private bool fieldDisplayModeManuallySet;
    private string resourceClassId;
    private Telerik.Sitefinity.Web.UI.Fields.Enums.FieldDisplayMode? fieldDisplayMode;
    private Func<string> getResourceClassIdCallback;
    private Func<Telerik.Sitefinity.Web.UI.Fields.Enums.FieldDisplayMode?> getFieldDisplayModeCallback;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DefinitionFacadeInfo" /> class.
    /// </summary>
    public DefinitionFacadeInfo()
      : this((Func<string>) null, (Func<Telerik.Sitefinity.Web.UI.Fields.Enums.FieldDisplayMode?>) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DefinitionFacadeInfo" /> class.
    /// </summary>
    /// <param name="getResourceClassIdCallback">The callback for getting resource class id.</param>
    /// <param name="getFieldDisplayModeCallback">The callback for getting field display mode.</param>
    public DefinitionFacadeInfo(
      Func<string> getResourceClassIdCallback,
      Func<Telerik.Sitefinity.Web.UI.Fields.Enums.FieldDisplayMode?> getFieldDisplayModeCallback)
    {
      this.getFieldDisplayModeCallback = getFieldDisplayModeCallback;
      this.getResourceClassIdCallback = getResourceClassIdCallback;
      this.fieldDisplayModeManuallySet = false;
      this.resClassIdManuallySet = false;
    }

    /// <inheritdoc />
    public string ModuleName { get; set; }

    /// <inheritdoc />
    public string DefinitionName { get; set; }

    /// <inheritdoc />
    public Type ContentType { get; set; }

    /// <inheritdoc />
    public string ViewName { get; set; }

    /// <inheritdoc />
    public string SectionName { get; set; }

    /// <inheritdoc />
    public string ResourceClassId
    {
      get => !this.resClassIdManuallySet && this.getResourceClassIdCallback != null ? this.getResourceClassIdCallback() : this.resourceClassId;
      set
      {
        this.resClassIdManuallySet = true;
        this.resourceClassId = value;
      }
    }

    /// <inheritdoc />
    public Telerik.Sitefinity.Web.UI.Fields.Enums.FieldDisplayMode? FieldDisplayMode
    {
      get => !this.fieldDisplayModeManuallySet && this.getResourceClassIdCallback != null ? this.getFieldDisplayModeCallback() : this.fieldDisplayMode;
      set
      {
        this.fieldDisplayModeManuallySet = true;
        this.fieldDisplayMode = value;
      }
    }
  }
}
