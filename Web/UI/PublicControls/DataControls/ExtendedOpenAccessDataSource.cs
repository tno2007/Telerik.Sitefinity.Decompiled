// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.ExtendedOpenAccessDataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.OpenAccess;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  public class ExtendedOpenAccessDataSource : OpenAccessDataSource, ICustomWidgetVisualization
  {
    public bool IsEmpty => string.IsNullOrEmpty(this.CommandText) && string.IsNullOrEmpty(this.ContextTypeName) && string.IsNullOrEmpty(this.ObjectContextProvider);

    public string EmptyLinkText => Res.Get<PublicControlsResources>().SetOpenAccessData;
  }
}
