// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ItemLists.TreeTableItemHeader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.ItemLists
{
  public class TreeTableItemHeader
  {
    public TreeTableItemHeader(string title, string cssClass, int width, bool disableSorting)
    {
      this.Title = title;
      this.CssClass = cssClass;
      this.Width = width;
      this.DisableSorting = disableSorting;
    }

    public string Title { get; private set; }

    public string CssClass { get; private set; }

    public int Width { get; private set; }

    public bool DisableSorting { get; private set; }
  }
}
