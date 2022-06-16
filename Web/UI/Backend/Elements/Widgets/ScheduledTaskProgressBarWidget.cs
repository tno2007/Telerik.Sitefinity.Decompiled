// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ScheduledTaskProgressBarWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  internal class ScheduledTaskProgressBarWidget : SimpleScriptView, IWidget
  {
    internal const string ViewScript = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.ScheduledTaskProgressBarWidget.js";
    internal const string ClientManagerScript = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";
    internal static readonly string LayoutTemplate = "Telerik.Sitefinity.Resources.Templates.Backend.Widgets.ScheduledTaskProgressBarWidget.ascx";
    private Guid taskId;
    private int checkInterval = 1000;
    private IWidgetDefinition definition;

    IWidgetDefinition IWidget.Definition
    {
      get => this.definition;
      set => this.definition = value;
    }

    void IWidget.Configure(IWidgetDefinition definition) => this.definition = definition;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ControlUtilities.ToVppPath(ScheduledTaskProgressBarWidget.LayoutTemplate) : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    internal Guid TaskId
    {
      get => this.taskId;
      set => this.taskId = value;
    }

    internal int CheckInterval
    {
      get => this.checkInterval;
      set => this.checkInterval = value;
    }

    internal HtmlControl TaskDescription => this.Container.GetControl<HtmlControl>("taskDescription", true);

    internal HtmlControl TaskStatus => this.Container.GetControl<HtmlControl>("taskStatus", false);

    internal HtmlControl ErrorDetailsLink => this.Container.GetControl<HtmlControl>("errorDetailsLink", true);

    internal HtmlControl ErrorDetailsMessage => this.Container.GetControl<HtmlControl>("errorDetailsMessage", true);

    internal HtmlControl TaskProgressBar => this.Container.GetControl<HtmlControl>("taskProgressBar", true);

    internal HtmlControl TaskProgressReport => this.Container.GetControl<HtmlControl>("taskProgressReport", true);

    internal HtmlControl TaskCommand => this.Container.GetControl<HtmlControl>("taskCommand", true);

    internal HtmlControl Wrapper => this.Container.GetControl<HtmlControl>("wrapper", true);

    internal HtmlControl ErrorDetailsPanel => this.Container.GetControl<HtmlControl>("errorDetailsPanel", true);

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddProperty("_taskId", (object) this.TaskId);
      controlDescriptor.AddProperty("_serviceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/SchedulingService.svc"));
      controlDescriptor.AddProperty("_checkInterval", (object) this.CheckInterval);
      controlDescriptor.AddProperty("_name", this.definition != null ? (object) this.definition.Name : (object) string.Empty);
      controlDescriptor.AddElementProperty("taskDescription", this.TaskDescription.ClientID);
      controlDescriptor.AddElementProperty("errorDetailsLink", this.ErrorDetailsLink.ClientID);
      controlDescriptor.AddElementProperty("errorDetailsMessage", this.ErrorDetailsMessage.ClientID);
      controlDescriptor.AddElementProperty("errorDetailsPanel", this.ErrorDetailsPanel.ClientID);
      controlDescriptor.AddElementProperty("taskProgressBar", this.TaskProgressBar.ClientID);
      controlDescriptor.AddElementProperty("taskProgressReport", this.TaskProgressReport.ClientID);
      controlDescriptor.AddElementProperty("taskCommand", this.TaskCommand.ClientID);
      controlDescriptor.AddElementProperty("wrapper", this.Wrapper.ClientID);
      if (this.TaskStatus != null)
        controlDescriptor.AddElementProperty("taskStatus", this.TaskStatus.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = typeof (ScheduledTaskProgressBarWidget).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.ScheduledTaskProgressBarWidget.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskStatuses.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskCommands.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    protected override void InitializeControls(GenericContainer container)
    {
    }

    private class ControlNames
    {
      internal const string TaskDescription = "taskDescription";
      internal const string TaskStatus = "taskStatus";
      internal const string ErrorDetailsLink = "errorDetailsLink";
      internal const string ErrorDetailsMessage = "errorDetailsMessage";
      internal const string ErrorDetailsPanel = "errorDetailsPanel";
      internal const string TaskProgressBar = "taskProgressBar";
      internal const string TaskProgressReport = "taskProgressReport";
      internal const string TaskCommand = "taskCommand";
      internal const string Wrapper = "wrapper";
    }
  }
}
