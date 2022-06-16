// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IHasContainerTypeExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Extension methods which extend <see cref="T:Telerik.Sitefinity.Web.UI.IHasContainerType" /> with helper methods.
  /// </summary>
  internal static class IHasContainerTypeExtensions
  {
    /// <summary>
    /// Determines whether the control is editable through Inline editing.
    /// </summary>
    /// <param name="container">The container.</param>
    /// <returns></returns>
    public static bool IsEditable(this IHasContainerType container) => (uint) container.ContainerType.GetCustomAttributes(typeof (EditableControlsContainerAttribute), false).Length > 0U;
  }
}
