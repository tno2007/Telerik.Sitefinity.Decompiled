// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Configuration.WorkflowElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Workflow.Configuration
{
  public class WorkflowElement : ConfigElement, IModuleDependentItem
  {
    public WorkflowElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal WorkflowElement()
      : base(false)
    {
    }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    [ConfigurationProperty("contentType", IsKey = true, IsRequired = true)]
    public string ContentType
    {
      get => (string) this["contentType"];
      set => this["contentType"] = (object) value;
    }

    /// <summary>Gets or sets the service URL.</summary>
    /// <value>The service URL.</value>
    [ConfigurationProperty("serviceUrl", IsRequired = true)]
    public string ServiceUrl
    {
      get
      {
        string defaultWorkflowUrl = (string) this["serviceUrl"];
        if (defaultWorkflowUrl.IsNullOrEmpty())
        {
          Type type = TypeResolutionService.ResolveType(this.ContentType, false);
          if (type != (Type) null)
            defaultWorkflowUrl = WorkflowConfig.GetDefaultWorkflowUrl(type);
        }
        return defaultWorkflowUrl;
      }
      set => this["serviceUrl"] = (object) value;
    }

    /// <summary>Gets or sets the title of the view.</summary>
    /// <value>The title of the view.</value>
    [ConfigurationProperty("title", DefaultValue = "", IsRequired = true)]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The resource class pageId.</value>
    [ConfigurationProperty("resourceClassId", DefaultValue = "", IsRequired = true)]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the module name the toolbox item depends on.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("moduleName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemModuleNameDescription", Title = "ToolboxItemModuleNameTitle")]
    public string ModuleName
    {
      get => (string) this["moduleName"];
      set => this["moduleName"] = (object) value;
    }

    protected override void OnPropertiesInitialized() => base.OnPropertiesInitialized();

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string contentType = "contentType";
      public const string serviceUrl = "serviceUrl";
      public const string ResourceClassId = "resourceClassId";
      public const string Title = "title";
      public const string moduleName = "moduleName";
    }
  }
}
