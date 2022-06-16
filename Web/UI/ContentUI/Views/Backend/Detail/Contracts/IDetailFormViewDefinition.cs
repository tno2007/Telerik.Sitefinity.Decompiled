// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts.DetailFormViewDefinitionExtentions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts
{
  /// <summary>
  /// Class with extension methods for the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts.IDetailFormViewDefinition" /> interface.
  /// </summary>
  public static class DetailFormViewDefinitionExtentions
  {
    /// <summary>
    /// If the passed showToolbar value is not set automatically resolves whether to show or not the
    /// top toolbar depending on the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Enums.FieldDisplayMode" /> of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts.IDetailFormViewDefinition" />
    /// </summary>
    /// <param name="def">The <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts.IDetailFormViewDefinition" /> defenition </param>
    /// <param name="showToolbar">The resolved value. If it's value is set t</param>
    /// <returns></returns>
    public static bool? GetShowTopToolbar(this IDetailFormViewDefinition def, bool? showToolbar)
    {
      if (showToolbar.HasValue)
        return showToolbar;
      return def.DisplayMode == FieldDisplayMode.Write ? new bool?(true) : new bool?(false);
    }
  }
}
