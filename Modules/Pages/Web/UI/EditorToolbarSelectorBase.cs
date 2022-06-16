// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.EditorToolbarSelectorBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Editor Tool bar selector base class</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Web.UI.Kendo.KendoView" />
  /// <seealso cref="T:System.Web.UI.IScriptControl" />
  public abstract class EditorToolbarSelectorBase : KendoView, IScriptControl
  {
    internal const string IEditorToolbarSelectorScript = "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.IEditorToolbarSelector.js";

    /// <summary>Gets the supported media types.</summary>
    /// <value>The supported media types.</value>
    public abstract IEnumerable<DesignMediaType> SupportedMediaTypes { get; }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.IEditorToolbarSelector.js", typeof (EditorToolbarSelectorBase).Assembly.FullName)
    }.ToArray();
  }
}
