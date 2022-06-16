﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowDefinitionList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Workflow.UI
{
  /// <summary>Control panel control for the workflow module.</summary>
  public class WorkflowDefinitionList : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.WorkflowDefinitionList.ascx");
    public const string workflowDefinitionListScript = "Telerik.Sitefinity.Workflow.Scripts.WorkflowDefinitionList.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? WorkflowDefinitionList.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    public virtual ItemsGrid workflowDefinitionsGrid => this.Container.GetControl<ItemsGrid>(nameof (workflowDefinitionsGrid), true);

    public virtual DecisionScreen decisionScreenEmpty => this.Container.GetControl<DecisionScreen>(nameof (decisionScreenEmpty), true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.Controls.Add((Control) new UserPreferences());

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (WorkflowDefinitionList).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("workflowDefinitionsGrid", this.workflowDefinitionsGrid.ClientID);
      controlDescriptor.AddComponentProperty("decisionScreenEmpty", this.decisionScreenEmpty.ClientID);
      controlDescriptor.AddProperty("isFiltering", (object) false);
      controlDescriptor.AddProperty("isDBPMode", (object) SystemManager.IsDBPMode);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = this.GetType().Assembly.FullName;
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Workflow.Scripts.WorkflowDefinitionList.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
