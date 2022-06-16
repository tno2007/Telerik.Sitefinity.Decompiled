// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.MailingListSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>A control for selecting a mailing list.</summary>
  public class MailingListSelector : KendoWindow
  {
    internal new const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.MailingListSelector.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.MailingListSelector.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.MailingListSelector" /> class.
    /// </summary>
    public MailingListSelector() => this.LayoutTemplatePath = MailingListSelector.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets a reference to the outer div.</summary>
    protected override HtmlContainerControl OuterDiv => this.Container.GetControl<HtmlContainerControl>("listsSelectorWindow", true);

    /// <summary>
    /// Gets a reference to the mail list selector cancel button.
    /// </summary>
    protected virtual LinkButton CancelSelectingButton => this.Container.GetControl<LinkButton>("lnkCancelSelecting", true);

    /// <summary>
    /// Gets a reference to the mail list done selecting button.
    /// </summary>
    protected virtual LinkButton DoneSelectingButton => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    /// <summary>Gets a reference to the mailing lists selector.</summary>
    protected virtual FlatSelector ListsSelector => this.Container.GetControl<FlatSelector>("listsSelector", true);

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("lnkCancelSelecting", this.CancelSelectingButton.ClientID);
      controlDescriptor.AddElementProperty("lnkDoneSelecting", this.DoneSelectingButton.ClientID);
      controlDescriptor.AddComponentProperty("listsSelector", this.ListsSelector.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.MailingListSelector.js", typeof (MailingListSelector).Assembly.FullName)
    };
  }
}
