// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DefaultControlUtilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Default implementation of the control utilities class.
  /// </summary>
  public class DefaultControlUtilities : IControlUtilities
  {
    /// <summary>
    /// Converts the layout template name, which is a name of the embedded resource, to a
    /// virtual path that points to Telerik.Sitefinity.Resources assembly.
    /// </summary>
    /// <param name="layoutTemplateName"></param>
    /// <returns>The virtual path of the resources.</returns>
    public string ToVppPath(string layoutTemplateName)
    {
      if (string.IsNullOrEmpty(layoutTemplateName))
        throw new ArgumentNullException(nameof (layoutTemplateName));
      return "~/SFRes/" + layoutTemplateName;
    }
  }
}
