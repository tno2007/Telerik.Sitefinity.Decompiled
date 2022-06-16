// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.DialogDefinitionExtentions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>DialogDefinitionExtentions</summary>
  public static class DialogDefinitionExtentions
  {
    private static readonly string dialogBaseUrl = "~/Sitefinity/Dialog/";

    /// <summary>
    /// Automatically resolves the navigateUrl of the dialog dependig on it's name
    /// </summary>
    /// <param name="dialogDefinition">The dialog definition.</param>
    /// <returns></returns>
    public static string GetNavigateUrl(
      this IDialogDefinition dialogDefinition,
      string dialogName,
      string parameters)
    {
      if (string.IsNullOrEmpty(dialogName))
        throw new ArgumentNullException("A name is required to construct the URL for the dialog");
      string empty = string.Empty;
      string str1 = VirtualPathUtility.Combine(VirtualPathUtility.ToAbsolute(DialogDefinitionExtentions.dialogBaseUrl), dialogName);
      if (parameters != null && parameters.IndexOf("?") != 0)
        parameters += "?";
      string str2 = parameters;
      return str1 + str2;
    }
  }
}
