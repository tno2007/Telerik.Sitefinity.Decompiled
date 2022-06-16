// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.GoalsField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>Field control for goals</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Web.UI.Fields.FieldControl" />
  public class GoalsField : FieldControl
  {
    private const string ControlScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.GoalsField.js";
    private static readonly string LayoutTemplatePathValue = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.GoalsField.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.GoalsField" /> class.
    /// </summary>
    public GoalsField() => this.LayoutTemplatePath = GoalsField.LayoutTemplatePathValue;

    /// <summary>Gets or sets the conversions service URL</summary>
    public string ConversionsServiceUrl { get; set; }

    /// <summary>Gets the goal selector.</summary>
    /// <value>The goal selector.</value>
    protected GoalSelector GoalSelector => this.Container.GetControl<GoalSelector>("goalSelector", false);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.GoalSelector == null)
        return;
      this.GoalSelector.ConversionsServiceUrl = this.ConversionsServiceUrl;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().LastOrDefault<ScriptDescriptor>() as ScriptControlDescriptor;
      if (this.GoalSelector != null)
        controlDescriptor.AddComponentProperty("goalSelector", this.GoalSelector.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.GoalsField.js", typeof (PageVariationsField).Assembly.FullName)
    }.ToArray();
  }
}
