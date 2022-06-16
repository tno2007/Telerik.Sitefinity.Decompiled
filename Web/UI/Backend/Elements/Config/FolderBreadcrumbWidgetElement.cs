// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.FolderBreadcrumbWidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Represents a configuration element for the FolderBreadcrumbWidget.
  /// </summary>
  public class FolderBreadcrumbWidgetElement : 
    WidgetElement,
    IFolderBreadcrumbWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.FolderBreadcrumbWidgetElement" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    public FolderBreadcrumbWidgetElement(ConfigElement parentElement)
      : base(parentElement)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new FolderBreadcrumbWidgetDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the type of the manager that is going to be used to get folders.
    /// </summary>
    [ConfigurationProperty("managerType")]
    [TypeConverter(typeof (StringTypeConverter))]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FolderBreadcrumbWidgetManagerTypeDescription", Title = "FolderBreadcrumbWidgetManagerTypeCaption")]
    public Type ManagerType
    {
      get => (Type) this["managerType"];
      set => this["managerType"] = (object) value;
    }

    /// <summary>Gets or sets the navigation page id.</summary>
    [ConfigurationProperty("navigationPageId")]
    [TypeConverter(typeof (GuidConverter))]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FolderBreadcrumbWidgetNavigationPageIdDescription", Title = "FolderBreadcrumbWidgetNavigationPageIdCaption")]
    public Guid NavigationPageId
    {
      get => (Guid) this["navigationPageId"];
      set => this["navigationPageId"] = (object) value;
    }

    /// <summary>Gets or sets the root page id.</summary>
    [ConfigurationProperty("rootPageId")]
    [TypeConverter(typeof (GuidConverter))]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FolderBreadcrumbWidgetRootPageIdDescription", Title = "FolderBreadcrumbWidgetRootPageIdCaption")]
    public Guid RootPageId
    {
      get => (Guid) this["rootPageId"];
      set => this["rootPageId"] = (object) value;
    }

    /// <summary>Gets or sets the title for the root link.</summary>
    [ConfigurationProperty("rootTitle")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FolderBreadcrumbWidgetRootTitleDescription", Title = "FolderBreadcrumbWidgetRootTitleCaption")]
    public string RootTitle
    {
      get => (string) this["rootTitle"];
      set => this["rootTitle"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to append the root item URL.
    /// </summary>
    [ConfigurationProperty("appendRootUrl")]
    [TypeConverter(typeof (BooleanConverter))]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FolderBreadcrumbWidgetAppendRootUrlDescription", Title = "FolderBreadcrumbWidgetAppendRootUrlCaption")]
    public bool AppendRootUrl
    {
      get => (bool) this["appendRootUrl"];
      set => this["appendRootUrl"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct FoldersBreadcrumbWidgetProps
    {
      public const string managerType = "managerType";
      public const string navigationPageId = "navigationPageId";
      public const string rootPageId = "rootPageId";
      public const string rootTitle = "rootTitle";
      public const string appendRootUrl = "appendRootUrl";
    }
  }
}
