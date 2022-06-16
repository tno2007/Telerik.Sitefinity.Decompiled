// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IndexRenderModeAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Define the default behaviour of a control when it is indexed
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, Inherited = true)]
  public class IndexRenderModeAttribute : Attribute
  {
    private readonly IndexRenderModes mode;

    /// <summary>
    /// Define the default behaviour of a control when it is indexed
    /// </summary>
    /// <param name="mode">Control behaviour when it is rendered</param>
    public IndexRenderModeAttribute(IndexRenderModes mode) => this.mode = mode;

    /// <summary>
    /// Control behaviour when it is rendered (e.g. defines whether or not the control's content should be indexed)
    /// </summary>
    public IndexRenderModes Mode => this.mode;
  }
}
