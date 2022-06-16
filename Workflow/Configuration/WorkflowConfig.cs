// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Configuration.WorkflowConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities.Statements;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Workflow.Data;

namespace Telerik.Sitefinity.Workflow.Configuration
{
  public class WorkflowConfig : ModuleConfigBase
  {
    public static string rootURL = "~/DefaultWorkflows";
    public static string anyContentWorkflow = WorkflowConfig.rootURL + "/AnyContentApprovalWorkflow.xamlx";
    public static string anyMediaContentWorkflow = WorkflowConfig.rootURL + "/AnyMediaContentApprovalWorkflow.xamlx";
    public static string pagesWorkflow = WorkflowConfig.rootURL + "/PagesApprovalWorkflow.xamlx";
    public static string approvalWorkflow = WorkflowConfig.rootURL + "/ApprovalWorkflow.xamlx";
    private const int windowsAzureExternalEndpointBindingDefaultTimeout = 300000;

    /// <summary>Gets the visualizations.</summary>
    /// <value>The visualizations.</value>
    [ConfigurationProperty("visualizations")]
    [ConfigurationCollection(typeof (ActivityVisualization), AddItemName = "add")]
    public ConfigElementDictionary<string, ActivityVisualization> Visualizations => (ConfigElementDictionary<string, ActivityVisualization>) this["visualizations"];

    /// <summary>Gets the approval workflows.</summary>
    /// <value>The approval workflows.</value>
    [ConfigurationProperty("workflowsTypes")]
    public ConfigElementDictionary<string, WorkflowElement> Workflows => (ConfigElementDictionary<string, WorkflowElement>) this["workflowsTypes"];

    /// <summary>Gets or sets the workflow binding timeouts.</summary>
    /// <value>The binding timeouts.</value>
    [ConfigurationProperty("bindingTimeouts")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BindingTimeoutsConfigDescription", Title = "BindingTimeoutsConfigCaption")]
    public virtual BindingTimeoutsElememet BindingTimeouts
    {
      get => (BindingTimeoutsElememet) this["bindingTimeouts"];
      set => this["bindingTimeouts"] = (object) value;
    }

    /// <summary>
    /// When set to <c>true</c> all requests to the workflow service are sent to the public/input endpoint, thus going through the
    /// load balancer and possibly landing to a different instance. This is required to workaround the inability to set the <c>Host</c>
    /// header in the WCF request, when multiple sites per instance are sharing the internal endpoint.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WorkflowUseExternalEndpointOnAzureDescription", Title = "WorkflowUseExternalEndpointOnAzureTitle")]
    [ConfigurationProperty("useExternalEndpointOnWindowsAzure", DefaultValue = true)]
    public virtual bool UseExternalEndpointOnWindowsAzure
    {
      get => (bool) this["useExternalEndpointOnWindowsAzure"];
      set => this["useExternalEndpointOnWindowsAzure"] = (object) value;
    }

    /// <summary>
    /// Workflow binding timeouts for the case when the requests to the workflow service are sent to the public/input endpoint.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WindowsAzureExternalEndpointBindingTimeoutsDescription", Title = "WindowsAzureExternalEndpointBindingTimeoutsTitle")]
    [ConfigurationProperty("windowsAzureExternalEndpointBindingTimeouts")]
    public virtual BindingTimeoutsElememet WindowsAzureExternalEndpointBindingTimeouts
    {
      get => (BindingTimeoutsElememet) this["windowsAzureExternalEndpointBindingTimeouts"];
      set => this["windowsAzureExternalEndpointBindingTimeouts"] = (object) value;
    }

    [ObjectInfo(typeof (ConfigDescriptions), Description = "RunWorkflowAsServiceDescription", Title = "RunWorkflowAsServiceTitle")]
    [ConfigurationProperty("runWorkflowAsService", DefaultValue = false)]
    [Obsolete("Won't be supported any more.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool RunWorkflowAsService
    {
      get => (bool) this["runWorkflowAsService"];
      set => this["runWorkflowAsService"] = (object) value;
    }

    public static string GetDefaultWorkflowUrl(Type type)
    {
      if (typeof (PageNode).IsAssignableFrom(type))
        return WorkflowConfig.pagesWorkflow;
      if (typeof (MediaContent).IsAssignableFrom(type))
        return WorkflowConfig.anyMediaContentWorkflow;
      if (typeof (Content).IsAssignableFrom(type))
        return WorkflowConfig.anyContentWorkflow;
      if (typeof (DynamicContent).IsAssignableFrom(type) || SystemManager.IsModuleEnabled("Ecommerce") && TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product").IsAssignableFrom(type))
        return WorkflowConfig.approvalWorkflow;
      throw new ArgumentException("Type : {0} is not supported".Arrange((object) type.Name));
    }

    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.Visualizations.Add(new ActivityVisualization()
      {
        ActivityType = typeof (Assign).FullName,
        Visible = true
      });
      this.Workflows.Add(new WorkflowElement()
      {
        ContentType = typeof (PageNode).FullName,
        ServiceUrl = WorkflowConfig.GetDefaultWorkflowUrl(typeof (PageNode)),
        Title = "ModuleTitle",
        ResourceClassId = typeof (PageResources).Name
      });
      this.Workflows.Add(new WorkflowElement()
      {
        ContentType = typeof (Video).FullName,
        ServiceUrl = WorkflowConfig.GetDefaultWorkflowUrl(typeof (Video)),
        Title = "ModuleTitle",
        ResourceClassId = typeof (VideosResources).Name,
        ModuleName = "Libraries"
      });
      this.Workflows.Add(new WorkflowElement()
      {
        ContentType = typeof (Image).FullName,
        ServiceUrl = WorkflowConfig.GetDefaultWorkflowUrl(typeof (Image)),
        Title = "ModuleTitle",
        ResourceClassId = typeof (ImagesResources).Name,
        ModuleName = "Libraries"
      });
      this.Workflows.Add(new WorkflowElement()
      {
        ContentType = typeof (Document).FullName,
        ServiceUrl = WorkflowConfig.GetDefaultWorkflowUrl(typeof (Document)),
        Title = "ModuleTitle",
        ResourceClassId = typeof (DocumentsResources).Name,
        ModuleName = "Libraries"
      });
      this.WindowsAzureExternalEndpointBindingTimeouts.AllPropertiesTimeoutDefaultValue = new int?(300000);
    }

    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Description = "A provider that stores news data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessWorkflowProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/Workflow"
          }
        }
      });
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string visualizations = "visualizations";
    }
  }
}
