// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.Extensions.LightNavigationControlTemplate.LightNavigationControlTemplateExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.NavigationControls.Extensions.LightNavigationControlTemplate
{
  public static class LightNavigationControlTemplateExtensions
  {
    /// <summary>
    /// Gets the CSS class selected from the control designer.
    /// </summary>
    /// <param name="control">The template of the LightNavigationControl.</param>
    public static string GetCssClass(this Control control)
    {
      if (control.Parent.Parent is LightNavigationControl parent)
        return parent.CssClass;
      throw new ArgumentException("GetCssClass extension should be used only in the widget templates of LightNavigationControl.");
    }
  }
}
