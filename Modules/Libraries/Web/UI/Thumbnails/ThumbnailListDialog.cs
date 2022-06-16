// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailListDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails
{
  public class ThumbnailListDialog : AjaxDialogBase
  {
    internal const string viewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailListDialog.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.Thumbnails.ThumbnailListDialog.ascx");

    /// <summary>Gets the type of the client component.</summary>
    public override string ClientComponentType => typeof (ThumbnailListDialog).FullName;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ThumbnailListDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the back link.</summary>
    protected virtual HyperLink BackLink => this.Container.GetControl<HyperLink>("backLink", true);

    /// <summary>Gets the title label.</summary>
    protected virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>Gets the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets a reference to the ul element that will be transformed into thumbnails list.
    /// </summary>
    protected virtual HtmlContainerControl List => this.Container.GetControl<HtmlContainerControl>("thumbnailList", true);

    /// <summary>Gets a reference to the embed dialog.</summary>
    protected virtual RadWindow EmbedDialog => this.Container.GetControl<RadWindow>("embedDialog", true);

    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.JQuery | ScriptRef.KendoAll | ScriptRef.TelerikSitefinity;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("backLink", this.BackLink.ClientID);
      controlDescriptor.AddElementProperty("titleLabel", this.TitleLabel.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("list", this.List.ClientID);
      string str = this.Page.ResolveUrl("~/Sitefinity/Services/ThumbnailService.svc");
      controlDescriptor.AddProperty("webServiceUrl", (object) str);
      controlDescriptor.AddComponentProperty("embedDialog", this.EmbedDialog.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new System.Collections.Generic.List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailListDialog.js", typeof (ThumbnailListDialog).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.core.js", typeof (KendoView).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.form.js", typeof (KendoView).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.templates.js", typeof (KendoView).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.validation.js", typeof (KendoView).Assembly.FullName)
    };
  }
}
