// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.UI.Basic.RevisionHistoryBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Versioning.Cleaner;
using Telerik.Sitefinity.Versioning.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Versioning.Web.UI.Basic
{
  /// <summary>The basic settings view of the revision history.</summary>
  public class RevisionHistoryBasicSettingsView : FieldControl
  {
    internal const string ScriptFileName = "Telerik.Sitefinity.Versioning.Web.UI.Scripts.RevisionHistoryBasicSettingsView.js";
    private const string NameOfLayoutTemplate = "Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.RevisionHistoryBasicSettingsView.ascx";
    private const string RevisionHistoryPrefix = "RevisionHistory";

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      string str = VersioningCleanerTask.GetNextExecuteTime().ToString("dd MMM yyy hh:mm tt UTC");
      bool flag1 = false;
      bool flag2 = false;
      string empty = string.Empty;
      ScheduledTaskData scheduledTaskData = SchedulingManager.GetManager().GetTaskData().FirstOrDefault<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => x.Key == VersioningCleanerTask.RevisionHistoryCleanerTaskKey));
      if (scheduledTaskData != null && scheduledTaskData.Status == TaskStatus.Pending)
      {
        flag1 = true;
        flag2 = scheduledTaskData.IsRunning;
        empty = scheduledTaskData.ExecuteTime.ToString("dd MMM yyy hh:mm tt UTC");
      }
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("settingsPanel", (object) this.SettingsPanel.ClientID);
      controlDescriptor.AddProperty("scheduledTaskInfoPanel", (object) this.ScheduledTaskInfoPanel.ClientID);
      controlDescriptor.AddComponentProperty("versionsNumber", this.VersionsNumber.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.Container.GetControl<ClientLabelManager>("labelManager", true).ClientID);
      controlDescriptor.AddComponentProperty("limitStatusChoiceField", this.LimitStatusChoiceField.ClientID);
      controlDescriptor.AddElementProperty("settingsStatusLabel", this.SettingsStatusLabel.ClientID);
      controlDescriptor.AddProperty("nextTaskExecuteTimeFormatted", (object) str);
      controlDescriptor.AddProperty("currentTaskExecuteTimeFormatted", (object) empty);
      controlDescriptor.AddProperty("isTaskScheduled", (object) flag1);
      controlDescriptor.AddProperty("isTaskRunning", (object) flag2);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Versioning.Web.UI.Scripts.RevisionHistoryBasicSettingsView.js", typeof (RevisionHistoryBasicSettingsView).Assembly.FullName)
    };

    /// <summary>
    /// Gets the control that stores the number of versions to be kept.
    /// </summary>
    protected virtual TextField VersionsNumber => this.Container.GetControl<TextField>("versionsNumberField", false);

    /// <summary>Gets the control that stores time to keep selector.</summary>
    protected virtual ChoiceField TimeToKeep => this.Container.GetControl<ChoiceField>("timeToKeepDropdown", false);

    /// <summary>
    /// Gets the reference to revision history settings panel.
    /// </summary>
    protected internal Panel SettingsPanel => this.Container.GetControl<Panel>("settingsPanel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the scheduled task information panel.</summary>
    /// <value>The scheduled task information panel.</value>
    protected internal Panel ScheduledTaskInfoPanel => this.Container.GetControl<Panel>("scheduledTaskInfoPanel", this.DisplayMode == FieldDisplayMode.Read);

    /// <summary>
    /// Gets the reference to the span control SettingsStatusLabel
    /// </summary>
    protected virtual HtmlGenericControl SettingsStatusLabel => this.Container.GetControl<HtmlGenericControl>("settingsStatusLabel", true);

    /// <summary>Gets the reference to the limit status choice field.</summary>
    protected internal ChoiceField LimitStatusChoiceField => this.Container.GetControl<ChoiceField>("limitStatusChoiceField", true);

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.RevisionHistoryBasicSettingsView.ascx") : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      VersionConfig versionConfig = Config.Get<VersionConfig>();
      foreach (TimeToKeepEnum timeToKeepEnum in Enum.GetValues(typeof (TimeToKeepEnum)))
      {
        ChoiceItem choiceItem = new ChoiceItem();
        choiceItem.Text = Res.Get<Labels>("RevisionHistory" + timeToKeepEnum.ToString());
        choiceItem.Value = timeToKeepEnum.GetHashCode().ToString();
        choiceItem.Enabled = true;
        if (timeToKeepEnum == versionConfig.Cleaner.TimeToKeep)
          choiceItem.Selected = true;
        this.TimeToKeep.Choices.Add(choiceItem);
      }
      this.LimitStatusChoiceField.ChoiceControl.Items.Add(new ListItem()
      {
        Text = Res.Get<Labels>().Unlimited,
        Value = "false",
        Enabled = true,
        Selected = true
      });
      this.LimitStatusChoiceField.ChoiceControl.Items.Add(new ListItem()
      {
        Text = Res.Get<Labels>().Limited,
        Value = "true",
        Enabled = true
      });
    }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
