// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IControlDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>interface for control designer views</summary>
  public interface IControlDesignerView
  {
    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    string ViewName { get; }

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    string ViewTitle { get; }

    /// <summary>Initializes the view.</summary>
    void InitView(ControlDesignerBase parentDesigner);
  }
}
