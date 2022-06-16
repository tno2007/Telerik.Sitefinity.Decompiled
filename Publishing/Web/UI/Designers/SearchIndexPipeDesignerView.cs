// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Designers.SearchIndexPipeDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Publishing.Web.UI.Designers
{
  public class SearchIndexPipeDesignerView : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.SearchIndexExportDesignerView.ascx");
    private string providerName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.UI.Designers.SearchIndexPipeDesignerView" /> class.
    /// </summary>
    public SearchIndexPipeDesignerView() => this.LayoutTemplatePath = SearchIndexPipeDesignerView.layoutTemplatePath;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container) => this.providerName = PublishingManager.GetProviderNameFromQueryString();

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.SearchIndexExportPipeDesignerView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;
  }
}
