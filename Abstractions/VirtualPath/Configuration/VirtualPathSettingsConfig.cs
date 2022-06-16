// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.Configuration.VirtualPathSettingsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.Abstractions.VirtualPath.Configuration
{
  /// <summary>
  /// Represents the configuration section which defines the virtual path definitions for the embedded resources.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "VirtualPathSettingsDescription", Title = "VirtualPathSettingsTitle")]
  public class VirtualPathSettingsConfig : ConfigSection
  {
    /// <summary>Returns a collection of registered virtual paths.</summary>
    [ConfigurationProperty("virtualPaths")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "VirtualPathsDescription", Title = "VirtualPathsTitle")]
    [ConfigurationCollection(typeof (VirtualPathElement), AddItemName = "add")]
    public ConfigElementDictionary<string, VirtualPathElement> VirtualPaths => (ConfigElementDictionary<string, VirtualPathElement>) this["virtualPaths"];

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      if (this.VirtualPaths.Count != 0)
        return;
      bool fileSystemResources = ControlUtilities.useFileSystemResources;
      this.VirtualPaths.Add(new VirtualPathElement((ConfigElement) this.VirtualPaths)
      {
        VirtualPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.SitefinityDefault.aspx"),
        ResourceLocation = "Telerik.Sitefinity.Resources.SitefinityDefault.aspx, Telerik.Sitefinity",
        ResolverName = "EmbeddedResourceResolver"
      });
      this.VirtualPaths.Add(new VirtualPathElement((ConfigElement) this.VirtualPaths)
      {
        VirtualPath = WorkflowConfig.anyContentWorkflow,
        ResolverName = "EmbeddedResourceResolver",
        ResourceLocation = "Telerik.Sitefinity.Workflow.Workflows.AnyContentApprovalWorkflow.xamlx, Telerik.Sitefinity"
      });
      this.VirtualPaths.Add(new VirtualPathElement((ConfigElement) this.VirtualPaths)
      {
        VirtualPath = WorkflowConfig.anyMediaContentWorkflow,
        ResolverName = "EmbeddedResourceResolver",
        ResourceLocation = "Telerik.Sitefinity.Workflow.Workflows.AnyMediaContentApprovalWorkflow.xamlx, Telerik.Sitefinity"
      });
      this.VirtualPaths.Add(new VirtualPathElement((ConfigElement) this.VirtualPaths)
      {
        VirtualPath = WorkflowConfig.pagesWorkflow,
        ResolverName = "EmbeddedResourceResolver",
        ResourceLocation = "Telerik.Sitefinity.Workflow.Workflows.PagesApprovalWorkflow.xamlx, Telerik.Sitefinity"
      });
      this.VirtualPaths.Add(new VirtualPathElement((ConfigElement) this.VirtualPaths)
      {
        VirtualPath = WorkflowConfig.approvalWorkflow,
        ResolverName = "EmbeddedResourceResolver",
        ResourceLocation = "Telerik.Sitefinity.Workflow.Workflows.ApprovalWorkflow.xamlx, Telerik.Sitefinity"
      });
      this.VirtualPaths.Add(new VirtualPathElement((ConfigElement) this.VirtualPaths)
      {
        VirtualPath = "~/SFRes/*",
        ResolverName = fileSystemResources ? "DebugFileSystemResolver" : "EmbeddedResourceResolver",
        ResourceLocation = "Telerik.Sitefinity.Resources"
      });
      this.VirtualPaths.Add(new VirtualPathElement((ConfigElement) this.VirtualPaths)
      {
        VirtualPath = "~/SFMVCPageService/*",
        ResolverName = "PureMvcPageResolver",
        ResourceLocation = ""
      });
      this.VirtualPaths.Add(new VirtualPathElement((ConfigElement) this.VirtualPaths)
      {
        VirtualPath = "~/SFPageService/*",
        ResolverName = "PageResolver",
        ResourceLocation = ""
      });
      this.VirtualPaths.Add(new VirtualPathElement((ConfigElement) this.VirtualPaths)
      {
        VirtualPath = ControlPresentationResolver.RootPath,
        ResolverName = fileSystemResources ? "DebugFileSystemResolver" : "ControlPresentationResolver",
        ResourceLocation = ""
      });
      this.VirtualPaths.Add(new VirtualPathElement((ConfigElement) this.VirtualPaths)
      {
        VirtualPath = "~/Res/*",
        ResolverName = fileSystemResources ? "DebugFileSystemResolver" : "EmbeddedResourceResolver",
        ResourceLocation = "Telerik.Sitefinity"
      });
      this.VirtualPaths.Add(new VirtualPathElement((ConfigElement) this.VirtualPaths)
      {
        VirtualPath = "~/ExtRes/*",
        ResolverName = fileSystemResources ? "DebugFileSystemResolver" : "EmbeddedResourceResolver",
        ResourceLocation = "Telerik.Sitefinity.Resources"
      });
    }
  }
}
