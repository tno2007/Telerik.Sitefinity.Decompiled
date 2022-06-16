// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.TextEditorUploadToolSetDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  public class TextEditorUploadToolSetDialog : AjaxDialogBase
  {
    internal const string AjaxUploadJs = "Telerik.Sitefinity.Resources.Scripts.ajaxupload.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.TextEditorUploadToolSetDialog.ascx");

    public TextEditorUploadToolSetDialog() => this.LayoutTemplatePath = TextEditorUploadToolSetDialog.layoutTemplatePath;

    protected override string LayoutTemplateName => (string) null;

    protected override void InitializeControls(GenericContainer container)
    {
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      typeof (TextEditorUploadToolSetDialog).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.ajaxupload.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name)
      };
    }

    /// <summary>The Toolset to load</summary>
    public string ToolSetName { get; set; }
  }
}
