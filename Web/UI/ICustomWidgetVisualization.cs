// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ICustomWidgetVisualization
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;

namespace Telerik.Sitefinity.Web.UI
{
  public interface ICustomWidgetVisualization
  {
    [Browsable(false)]
    bool IsEmpty { get; }

    [Browsable(false)]
    string EmptyLinkText { get; }
  }
}
