// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DialogBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents a base implementation of the Sitefinity dialog
  /// </summary>
  public abstract class DialogBase : SimpleView
  {
    private Collection<string> dialogParameters;

    /// <summary>
    /// Gets or sets the list of parameters passed to the dialog.
    /// </summary>
    /// <value>The dialog parameters.</value>
    public Collection<string> DialogParameters => this.dialogParameters;

    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      this.Controls.Add((Control) new UserPreferences());
    }

    /// <summary>Loads the route parameters.</summary>
    /// <param name="param">The param.</param>
    public void LoadRouteParameters(string param)
    {
      Collection<string> collection = new Collection<string>();
      char[] separator = new char[1]{ '/' };
      foreach (string str in param.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        collection.Add(str);
      this.dialogParameters = collection;
    }
  }
}
