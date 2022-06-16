// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowScheduleDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Workflow.UI
{
  /// <summary>
  /// Dialog for scheduling publish, unpublish or both actions on sitefinity item.
  /// </summary>
  public class WorkflowScheduleDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.WorkflowScheduleDialog.ascx");
    internal const string dialogScript = "Telerik.Sitefinity.Workflow.Scripts.WorkflowScheduleDialog.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.UI.WorkflowScheduleDialog" /> class.
    /// </summary>
    public WorkflowScheduleDialog() => this.LayoutTemplatePath = WorkflowScheduleDialog.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (WorkflowScheduleDialog).FullName;

    /// <summary>Gets the reference to the done selecting button.</summary>
    protected virtual LinkButton ScheduleButton => this.Container.GetControl<LinkButton>("saveButton", true);

    /// <summary>Represents the date picker for "Published on date"</summary>
    protected DateField PublishedOnDateField => this.Container.GetControl<DateField>("publishedOnDateField", true);

    /// <summary>Represents the date picker for "expires on date"</summary>
    protected DateField UnpublishOnDateField => this.Container.GetControl<DateField>("unpublishOnDateField", true);

    /// <summary>
    /// Represents the container for "unpublish on" section elements
    /// </summary>
    protected virtual HtmlGenericControl UnpublishOnSection => this.Container.GetControl<HtmlGenericControl>("unpublishOnSection", true);

    /// <summary>
    /// Represents the button that expands/collapses the "unpublish on" section.
    /// </summary>
    protected virtual HtmlAnchor UnpublishOnExpander => this.Container.GetControl<HtmlAnchor>("unpublishOnExpander", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      this.Controls.Add((Control) new FormManager());
      base.OnInit(e);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("scheduleButton", this.ScheduleButton.ClientID);
      controlDescriptor.AddComponentProperty("publishedOnDateField", this.PublishedOnDateField.ClientID);
      controlDescriptor.AddComponentProperty("unpublishOnDateField", this.UnpublishOnDateField.ClientID);
      controlDescriptor.AddElementProperty("unpublishOnSection", this.UnpublishOnSection.ClientID);
      controlDescriptor.AddElementProperty("unpublishOnExpander", this.UnpublishOnExpander.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Workflow.Scripts.WorkflowScheduleDialog.js", typeof (WorkflowScheduleDialog).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
