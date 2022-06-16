// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.ControlTemplates.Web.UI
{
  /// <summary>Dialog for selecting users or roles.</summary>
  public class EmailTemplateEditor : ControlTemplateEditor
  {
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.UserProfiles.EmailTemplateEditor.ascx");
    internal const string emailTemplateEditorScript = "Telerik.Sitefinity.Modules.ControlTemplates.Web.Scripts.EmailTemplateEditor.js";

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => EmailTemplateEditor.layoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (EmailTemplateEditor).FullName;

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.ControlTemplates.Web.Scripts.EmailTemplateEditor.js", typeof (EmailTemplateEditor).Assembly.FullName)
    };
  }
}
